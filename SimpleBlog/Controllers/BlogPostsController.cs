using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Builders;
using SimpleBlog.Data;
using SimpleBlog.Dtos.BlogPostDtos;
using SimpleBlog.Framework;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    [Authorize]
    public class BlogPostsController : Controller
    {
        private readonly UnitOfWork _db;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        public BlogPostsController(UnitOfWork db, UserManager<User> userManager, IWebHostEnvironment env)
        {
            _db = db;
            _userManager = userManager;
            _env = env;
        }

        // GET: BlogPosts
        public async Task<IActionResult> Index(int p = 1, int c = 10, string q = "")
        {
            PageList<BlogPost> applicationDbContext = await _db.BlogPosts.GetPagedAsync(p, c, q);
            return View(applicationDbContext);
        }

        public async Task<IActionResult> History(int id)
        {
            var currentVersion = await _db.BlogPosts.GetAsync(id);
            if (currentVersion == null) return NotFound();

            List<BlogPost> history = await _db.BlogPosts
                .GetAll(p => p.ContentId == currentVersion.ContentId)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return View(history);
        }

        // GET: BlogPosts/Details/5
        [Obsolete("use ContentController instead")]
        public async Task<IActionResult> Details(int? id)
        {
            throw new NotImplementedException();
        }

        // GET: BlogPosts/Create
        public async Task<IActionResult> Create()
        {
            await UseBlogCategoriesDropdown();
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBlogPostDto blogPost, IFormFile? image)
        {

            var content = new Content()
            {
                EntityType = ContentEntityType.BlogPost,
                Token = blogPost.Token,
            };

            if (await _db.Contents.GetAll(c => c.Token == content.Token).AnyAsync())
            {
                ModelState.AddModelError(nameof(blogPost.Token), "Duplicated token");
            }

            if (ModelState.IsValid)
            {
                await _db.Contents.CreateAsync(content);
                await _db.SaveChangesAsync();
                var userId = _userManager.GetUserId(User);

                var builder = new ContentBuilder()
                    .NewBlogPostBuilder()
                    .SetTitle(blogPost.Title)
                    .SetBody(blogPost.Body)
                    .SetCategoryId(blogPost.CategoryId)
                    .SetActiveVersion(true)
                    .SetContentId(content.Id)
                    .SetAuthorId(userId);

                if (image != null)
                {
                    string imageUrl = await _db.BlogPosts.SaveImage(_env, image);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        builder.SetImage(imageUrl);
                    }
                }

                var post = builder.Build();

                await _db.BlogPosts.CreateAsync(post);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await UseBlogCategoriesDropdown(blogPost.CategoryId);
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _db.BlogPosts.GetAsync(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            UseBlogCategoriesDropdown(post.CategoryId);
            var dto = new UpdateBlogPostDto(post.Id, post.ContentId, post.Title, post.Content.Token, post.Body, post.CategoryId);
            return View(dto);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateBlogPostDto blogPost, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _db.BlogPosts.UpdateAsync(blogPost, _env, image);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            UseBlogCategoriesDropdown(blogPost.CategoryId);
            return View(blogPost);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateActiveVersion(int id)
        {
            await _db.BlogPosts.UpdateActiveVersion(id);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(History), new { id });
        }
        // GET: BlogPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _db.BlogPosts.GetAsync(id.Value);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _db.BlogPosts.GetAsync(id);
            if (blogPost != null)
            {
                await _db.BlogPosts.DeleteAsync(blogPost.Id);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _db.BlogPosts.GetAll(e => e.Id == id).Any();
        }

        private async Task UseBlogCategoriesDropdown(int? selectedItemId = null)
        {
            var list = await _db.BlogCategories.GetAll()
                        .Select(c => new SelectListItem(c.GetTitle(), c.GetId().ToString(), c.GetId() == selectedItemId))
                        .ToListAsync();

            ViewBag.Categories = list;
        }
    }
}
