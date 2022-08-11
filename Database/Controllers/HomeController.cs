using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Database.Models;
using Database.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Database.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContext _db;

        public HomeController(ILogger<HomeController> logger, BlogContext context)
        {
            _logger = logger;
            _db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //_db.Blogs.Add(new Blog() { DomainName = "example.com", Author = "me", Title = "First Blog" });
            //_db.SaveChanges();
            return View(new Blog());
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> CreateBlog([FromForm] Blog blog)
        {
            if (ModelState.IsValid)
            {
                _db.Add<Blog>(blog);
                await _db.SaveChangesAsync();
            }
            return View("Index", blog);
        }

        public async Task<IActionResult> ListBlogs()
        {
            IEnumerable<Blog> blogs = default;
            blogs = _db.Blogs.ToList();
            return View(blogs);
        }

        //public IEnumerable<SelectListItem> BlogList { get; set; } = default;

        [HttpGet]        
        public async Task<IActionResult> CreateBlogPost()
        {
            ViewData["BlogList"] = _db.Blogs.Select(b => new SelectListItem(b.Title, b.BlogId.ToString())).ToList();
            return View(new BlogPost());
        }

        [HttpPost]
        [ActionName("CreateBlogPost")]
        public async Task<IActionResult> CreateBlogPost2([FromForm] BlogPost post)
        {
            var blog = _db.Blogs.Where(b => b.BlogId == post.Blog.BlogId).FirstOrDefault();
            if (blog != null)
            {
                post.Blog = blog;
                ModelState.ClearValidationState("Blog");
                this.TryValidateModel(post);
            }
            if (ModelState.IsValid)
            {
                _db.Add<BlogPost>(post);
                await _db.SaveChangesAsync();
            }
            ViewData["BlogList"] = _db.Blogs.Select(b => new SelectListItem(b.Title, b.BlogId.ToString())).ToList();
            return View(new BlogPost());
        }

        [HttpGet]
        public async Task<IActionResult> DeletePost([FromQuery] int id)
        {
            BlogPost post = _db.BlogPosts.Where(bp => bp.BlogPostId == id).FirstOrDefault();
            return View(post);
        }

        [HttpPost]
        [ActionName("DeletePost")]
        public async Task<IActionResult> DeletePost2([FromQuery] int BlogPostId)
        {
            BlogPost post = _db.BlogPosts.Where(bp => bp.BlogPostId == BlogPostId).FirstOrDefault();
            _db.Remove<BlogPost>(post);
            await _db.SaveChangesAsync();
            return View(post);
        }

        [HttpGet]
        public IActionResult UpdatePost([FromQuery] int postId)
        {
            ViewData["BlogList"] = _db.Blogs.Select(b => new SelectListItem(b.Title, b.BlogId.ToString())).ToList();
            BlogPost post = _db.BlogPosts.Include(p => p.Blog).Where(bp => bp.BlogPostId == postId).FirstOrDefault(); ;
            return View(post);
        }

        [HttpPost]
        [ActionName("UpdatePost")]
        public async Task<IActionResult> UpdatePost2([FromForm] BlogPost post)
        {
            var blog = _db.Blogs.Where(b => b.BlogId == post.Blog.BlogId).FirstOrDefault();
            if (blog != null)
            {
                post.Blog = blog;
                ModelState.ClearValidationState("Blog");
                this.TryValidateModel(post);
            }
            if (ModelState.IsValid)
            {
                _db.Update<BlogPost>(post);
                await _db.SaveChangesAsync();
            }
            ViewData["BlogList"] = _db.Blogs.Select(b => new SelectListItem(b.Title, b.BlogId.ToString())).ToList();
            return View(post);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

