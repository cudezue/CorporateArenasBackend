using System.Collections.Generic;

namespace CorporateArenasBackend.Models.Vacancy
{
    public class JobCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public ICollection<VacancyDto> Vacancies { get; set; }
    }
}