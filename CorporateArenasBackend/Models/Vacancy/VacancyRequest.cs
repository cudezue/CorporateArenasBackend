using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.Vacancy
{
    public class VacancyRequest
    {
        [Required] public string Title { get; set; }

        [Required] public string Description { get; set; }

        [Required] public string Location { get; set; }

        [Required] public string Employee { get; set; }

        [Required] public int JobCategoryId { get; set; }

        [Required] public int JobTypeId { get; set; }
    }
}