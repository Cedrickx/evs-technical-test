using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVSDAL.Models;

namespace EVSBLL.BusinessObjects
{
    public class CompanyBO : IBusinessObject<Company, CompanyBO>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string HeadQuarters { get; set; } = null!;

        public List<string> Branches { get; set; } = new List<string>();

        public string TVANumber { get; set; } = null!;


        public Company Create()
        {
            return new Company
            {
                Id = Id,
                Name = Name,
                HeadQuarters = HeadQuarters,
                Branches = Branches.ToList(),
                TVANumber = TVANumber
            };
        }

        public CompanyBO GetFrom(Company entity)
        {
            return new CompanyBO
            {
                Id = entity.Id,
                Name = entity.Name,
                HeadQuarters = entity.HeadQuarters,
                Branches = entity.Branches.ToList(),
                TVANumber = entity.TVANumber
            };
        }
    }
}
