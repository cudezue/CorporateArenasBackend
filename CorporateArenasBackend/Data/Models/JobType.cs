using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class JobType
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; }
    }
}