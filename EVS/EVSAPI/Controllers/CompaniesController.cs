using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EVSDAL;
using EVSDAL.Models;
using EVSBLL.BusinessObjects;
using EVSBLL;

namespace EVSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompaniesController(ICompanyService service)
        {
            _service = service;
        }


        [HttpPut]
        public async Task<ActionResult<CompanyBO>> CreateCompany(CompanyBO company)
        {
            return await Task.Run(() => _service.CreateCompany(company));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyBO>> GetCompany(int id)
        {
            return await Task.Run(() => _service.GetCompany(id));
        }

        [HttpPost]
        public async Task<ActionResult<CompanyBO>> UpdateCompany(CompanyBO company)
        {
            return await Task.Run(() => _service.UpdateCompany(company));
        }

    }
}
