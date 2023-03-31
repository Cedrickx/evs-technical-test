using EVSBLL.BusinessObjects;
using EVSDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSBLL
{
    public interface ICompanyService
    {
        CompanyBO CreateCompany(CompanyBO companyBO);
        CompanyBO UpdateCompany(CompanyBO companyBO);
        List<CompanyBO> GetCompanies();
        List<CompanyBO> GetCompanies(List<int> ids);
        CompanyBO GetCompany(int id);
    }
}
