using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data;
using SimpleBlog.Framework;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    [AllowAnonymous]
    public class ContentsController : Controller
    {
        private readonly UnitOfWork _db;
        public ContentsController(UnitOfWork db)
        {
            _db = db;
        }
        public async Task<IActionResult> Details(string? token)
        {
            if (string.IsNullOrEmpty(token)) return BadRequest();

            IContent content = await _db.Contents.GetInfoByToken(token);
            if (content == null) return NotFound();

            if (content is BlogPost post)
            {
                return BlogPostView(post);
            }
            else if (content is BlogCagtegory category)
            {
                return await BlogCategoryView(category);
            }

            // else
            return BadRequest();
        }

        private IActionResult BlogPostView(BlogPost post)
        {
            return View(nameof(BlogPostView), post);
        }

        private async Task<IActionResult> BlogCategoryView(BlogCagtegory category)
        {
            if (category == null)
            {
                return NotFound();
            }

            if (category.Posts == null || category.Posts.Count == 0)
            {
                category.Posts = await _db.BlogPosts
                                    .GetAll(p => p.CategoryId == category.Id && p.ActiveVersion)
                                    .OrderByDescending(p => p.Id)
                                    .Take(10)
                                    .ToListAsync()
                                    ;
            }

            return View(nameof(BlogCategoryView), category);
        }
    }
}
