using System;

namespace Database.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public Blog Blog { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Content { get; set; }
    }
}