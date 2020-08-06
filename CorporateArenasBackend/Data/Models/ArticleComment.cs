namespace CorporateArenasBackend.Data.Models
{
    public class ArticleComment: Comment
    {
        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}