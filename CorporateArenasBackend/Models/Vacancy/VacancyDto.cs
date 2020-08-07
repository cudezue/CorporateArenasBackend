using System;

namespace CorporateArenasBackend.Models.Vacancy
{
    public class VacancyDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Employee { get; set; }

        public DateTime CreatedAt { get; set; }

        public JobCategoryDto JobCategory { get; set; }

        public JobTypeDto JobType { get; set; }
    }
}