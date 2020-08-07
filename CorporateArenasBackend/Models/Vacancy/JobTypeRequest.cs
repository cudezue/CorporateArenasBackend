using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.Vacancy
{
    public class JobTypeRequest
    {
        [Required] public string Name { get; set; }
    }
}