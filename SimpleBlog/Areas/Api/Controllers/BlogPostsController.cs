using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data;
using SimpleBlog.Dtos.BlogPostDtos;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly UnitOfWork _db;

        public BlogPostsController(UnitOfWork db)
        {
            _db = db;
        }

        // GET: api/BlogPosts
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BlogPostDto>>> GetBlogPosts(int p = 1, int c = 10)
        {
            Framework.PageList<BlogPost> items = await _db.BlogPosts.GetPagedAsync(p, c, string.Empty);
            var result = items.ToList().Select(t => t.AsDto()).ToList();

            return result;
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BlogPostDto>> GetBlogPost(int id)
        {
            var post = await _db.BlogPosts.GetAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var dto = post.AsDto();
            return dto;
        }

    }
}
