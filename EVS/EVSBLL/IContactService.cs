using EVSBLL.BusinessObjects;
using EVSDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSBLL
{
    public interface IContactService
    {
        ContactBO CreateContact(ContactBO contactBO);
        ContactBO UpdateContact(ContactBO contactBO);
        void DeleteContact(int id);
        ContactBO GetContact(int id);
    }
}
