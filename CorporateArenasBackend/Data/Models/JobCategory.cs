using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class JobCategory
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; }
        
        [Required] public string Description { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; }
    }
}