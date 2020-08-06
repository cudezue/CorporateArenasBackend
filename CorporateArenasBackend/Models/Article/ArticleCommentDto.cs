using System;

namespace CorporateArenasBackend.Models.Article
{
    public class ArticleCommentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}