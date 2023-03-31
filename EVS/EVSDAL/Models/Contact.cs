using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSDAL.Models
{
    public enum ContactType
    {
        Employee = 1,
        Freelancer = 2
    }

    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public List<Company> Companies { get; set; } = null!;

        [Required]
        public ContactType ContactType { get; set;} = ContactType.Employee;

        [MinLength(15)]
        [MaxLength(15)]
        public string? TVANumber { get; set; }
    }
}
