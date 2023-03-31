using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EVSDAL;
using EVSDAL.Models;
using EVSBLL;
using EVSBLL.BusinessObjects;

namespace EVSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }



        [HttpPut]
        public async Task<ActionResult<ContactBO>> CreateContact([FromBody] ContactBO contactBO)
        {
            return await Task.Run(() => _service.CreateContact(contactBO));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactBO>> GetContact(int id)
        {
            return await Task.Run(() => _service.GetContact(id));
        }

        [HttpPost]
        public async Task<ActionResult<ContactBO>> UpdateContact([FromBody] ContactBO contactBO)
        {
            return await Task.Run(() => _service.UpdateContact(contactBO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await Task.Run(() => _service.DeleteContact(id));

            return NoContent();
        }
    }
}
