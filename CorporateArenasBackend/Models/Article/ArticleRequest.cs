using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.Article
{
    public class ArticleRequest
    {
        [Required] public string Title { get; set; }

        [Required] public string Body { get; set; }
        
        [Required] public bool IsDraft { get; set; }
    }
}