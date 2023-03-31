using EVSBLL.BusinessObjects;
using EVSDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSBLL
{
    public interface ITVAService
    {
        bool IsValid(string tvaNumber);
    }
}
