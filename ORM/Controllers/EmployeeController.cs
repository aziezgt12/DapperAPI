using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ORM.Contracts;
using ORM.Dtos;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository param)
        {
            _employeeRepo = param;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _employeeRepo.GetAll();
                if (result== null)
                {
                    var httpResponse = HttpResponse.setStatus(404, "Not Found");
                    return NotFound(new { Status = httpResponse });
                }
                else
                {
                    var httpResponse = HttpResponse.setStatus(200, "OK");
                    return Ok(new { Status = httpResponse, response = result});
                }
            }
            catch (Exception ex)
            {
                //log error
                return BadRequest(HttpResponse.setStatus(400, ex.Message));
            }
        }



       

    }
}
