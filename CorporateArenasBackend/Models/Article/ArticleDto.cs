using System;
using System.Collections.Generic;

namespace CorporateArenasBackend.Models.Article
{
    public class ArticleDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Body { get; set; }

        public DateTime? PublishedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<ArticleCommentDto> Comments { get; set; }
    }
}