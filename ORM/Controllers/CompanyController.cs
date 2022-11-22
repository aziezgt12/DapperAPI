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
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;

        public CompaniesController(ICompanyRepository companyRepo) => _companyRepo = companyRepo;

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepo.GetCompanies();
                if (companies == null)
                {
                    var httpResponse = HttpResponse.setStatus(404, "Not Found");
                    return NotFound(new { Status = httpResponse });
                }
                else
                {
                    var httpResponse = HttpResponse.setStatus(200, "OK");
                    return Ok(new { Status = httpResponse, response = companies });
                }



                //return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return BadRequest(HttpResponse.setStatus(400, "false"));
            }
        }


        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompany(id);
                if (company == null)
                {
                    var httpResponse = HttpResponse.setStatus(404, "Not Found");
                    return NotFound(new { Status = httpResponse });
                }
                else
                {
                    var httpResponse = HttpResponse.setStatus(200, "OK");
                    return Ok(new { Status = httpResponse, response = company });
                }
            }
            catch (Exception ex)
            {
                //log error
                return BadRequest(HttpResponse.setStatus(400, "false"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDTO company)
        {
            try
            {
                var createdCompany = await _companyRepo.CreateCompany(company);
                if (createdCompany is null)
                
                    return BadRequest(HttpResponse.setStatus(400, "false"));
                
                
                return  CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, HttpResponse.setStatus(201, "Created"));

                
            }
            catch (Exception ex)
            {
                //log error
                var httpResponse = HttpResponse.setStatus(400, ex.Message);
                return BadRequest(httpResponse);
            }
        }

        //	[HttpPut("{id}")]
        //	public async Task<IActionResult> UpdateCompany(int id, CompanyForUpdateDto company)
        //	{
        //		try
        //		{
        //			var dbCompany = await _companyRepo.GetCompany(id);
        //			if (dbCompany == null)
        //				return NotFound();

        //			await _companyRepo.UpdateCompany(id, company);
        //			return NoContent();
        //		}
        //		catch (Exception ex)
        //		{
        //			//log error
        //			return StatusCode(500, ex.Message);
        //		}
        //	}

        //	[HttpDelete("{id}")]
        //	public async Task<IActionResult> DeleteCompany(int id)
        //	{
        //		try
        //		{
        //			var dbCompany = await _companyRepo.GetCompany(id);
        //			if (dbCompany == null)
        //				return NotFound();

        //			await _companyRepo.DeleteCompany(id);
        //			return NoContent();
        //		}
        //		catch (Exception ex)
        //		{
        //			//log error
        //			return StatusCode(500, ex.Message);
        //		}
        //	}

        //	[HttpGet("ByEmployeeId/{id}")]
        //	public async Task<IActionResult> GetCompanyForEmployee(int id)
        //	{
        //		try
        //		{
        //			var company = await _companyRepo.GetCompanyByEmployeeId(id);
        //			if (company == null)
        //				return NotFound();

        //			return Ok(company);
        //		}
        //		catch (Exception ex)
        //		{
        //			//log error
        //			return StatusCode(500, ex.Message);
        //		}
        //	}

        [HttpGet("{id}/MultipleResult")]
        public async Task<IActionResult> GetCompanyEmployeesMultipleResult(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompanyEmployeesMultipleResults(id);
                if (company == null)
                    return NotFound();

                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("MultipleMapping")]
        public async Task<IActionResult> GetCompaniesEmployeesMultipleMapping()
        {
            try
            {
                var company = await _companyRepo.GetCompaniesEmployeesMultipleMapping();

                if (company == null)
                {
                    var httpResponse = HttpResponse.setStatus(404, "Not Found");
                    return NotFound(new { Status = httpResponse });
                }
                else
                {
                    var httpResponse = HttpResponse.setStatus(200, "OK");
                    return Ok(new { Status = httpResponse, response = company });
                }
            }
            catch (Exception ex)
            {
                //log error
                var httpResponse = HttpResponse.setStatus(400, ex.Message);
                return BadRequest(httpResponse);
            }
        }

        //	[HttpPost("multiple")]
        //	public async Task<IActionResult> CreateCompany(List<CompanyForCreationDto> companies)
        //	{
        //		try
        //		{
        //			await _companyRepo.CreateMultipleCompanies(companies);
        //			return StatusCode(201);
        //		}
        //		catch (Exception ex)
        //		{
        //			//log error
        //			return StatusCode(500, ex.Message);
        //		}
        //	}
        //}
    }
}
