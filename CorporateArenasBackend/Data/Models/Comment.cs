using System;
using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Body { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}