using EVSDAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSBLL.BusinessObjects
{
    public class ContactBO : IBusinessObject<Contact, ContactBO>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public List<int> Companies { get; set; } = null!;

        public ContactType ContactType { get; set; }

        public string TVANumber { get; set; } = null!;


        public Contact Create()
        {
            return new Contact
            {
                Id = Id,
                Name = Name,
                Address = Address,
                ContactType = ContactType,
                TVANumber = TVANumber,
            };
        }

        public ContactBO GetFrom(Contact entity)
        {
            return new ContactBO
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Companies = entity.Companies.Select(x => x.Id).ToList(),
                ContactType = entity.ContactType,
                TVANumber = entity.TVANumber,
            };
        }
    }
}
