using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.Vacancy
{
    public class JobCategoryRequest
    {
        [Required] public string Name { get; set; }
        
        [Required] public string Description { get; set; }
    }
}