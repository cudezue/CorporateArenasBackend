namespace CorporateArenasBackend.Data.Models
{
    public class TrafficUpdateComment: Comment
    {
        public int TrafficUpdateId { get; set; }

        public TrafficUpdate TrafficUpdate { get; set; }
    }
}