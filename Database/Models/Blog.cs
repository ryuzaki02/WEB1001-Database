using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
	public class Blog
	{
		public int BlogId { get; set; }
        [Required]
        [Url]
		public string DomainName { get; set; }
        [EmailAddress]
		public string Author { get; set; }
		public DateTime CreatedDate { get; private set; } = DateTime.Now;
		[Required]
		public string Title { get; set; }
		public List<BlogPost> Posts { get; set; }
	}
}

