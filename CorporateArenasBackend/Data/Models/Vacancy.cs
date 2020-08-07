using System;
using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class Vacancy
    {
        public int Id { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Description { get; set; }

        [Required] public string Location { get; set; }

        [Required] public string Employee { get; set; }

        [Required] public int JobCategoryId { get; set; }

        [Required] public int JobTypeId { get; set; }

        public DateTime CreatedAt { get; set; }

        public JobCategory JobCategory { get; set; }

        public JobType JobType { get; set; }
    }
}