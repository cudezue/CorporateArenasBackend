using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.Article
{
    public class ArticleCommentRequest
    {
        [Required] public string Name { get; set; }

        [Required] public string Body { get; set; }
    }
}