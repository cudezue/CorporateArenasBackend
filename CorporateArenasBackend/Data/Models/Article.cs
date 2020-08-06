using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CorporateArenasBackend.Models.Article;

namespace CorporateArenasBackend.Data.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Body { get; set; }

        public string Slug { get; set; }

        public DateTime? PublishedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public ICollection<ArticleComment> Comments { get; set; }
    }
}