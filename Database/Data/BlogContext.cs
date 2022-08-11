using System;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
	public class BlogContext: DbContext
	{
		public BlogContext(DbContextOptions<BlogContext> options)
			:base(options)
        {

        }

		public DbSet<Blog> Blogs { get; set; }
		public DbSet<BlogPost> BlogPosts { get; set; }
	}
}

