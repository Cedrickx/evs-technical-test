using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EVSDAL.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string HeadQuarters { get; set; } = null!;

        [Required]
        public List<string> Branches { get; set; } = null!;

        [Required]
        [MinLength(15)]
        [MaxLength(15)]
        public string TVANumber { get; set; } = null!;
    }
}
