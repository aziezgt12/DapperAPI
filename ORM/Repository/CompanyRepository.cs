using Dapper;
using ORM.Context;
using ORM.Contracts;
using ORM.Dtos;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Company";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = "SELECT * FROM Company WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });

                return company;
            }
        }

        public async Task<Company> CreateCompany(CompanyDTO company)
        {
            var query = "INSERT INTO Company (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };

                return createdCompany;
            }
        }

        //public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        //{
        //	var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";

        //	var parameters = new DynamicParameters();
        //	parameters.Add("Id", id, DbType.Int32);
        //	parameters.Add("Name", company.Name, DbType.String);
        //	parameters.Add("Address", company.Address, DbType.String);
        //	parameters.Add("Country", company.Country, DbType.String);

        //	using (var connection = _context.CreateConnection())
        //	{
        //		await connection.ExecuteAsync(query, parameters);
        //	}
        //}

        //public async Task DeleteCompany(int id)
        //{
        //	var query = "DELETE FROM Companies WHERE Id = @Id";

        //	using (var connection = _context.CreateConnection())
        //	{
        //		await connection.ExecuteAsync(query, new { id });
        //	}
        //}

        //public async Task<Company> GetCompanyByEmployeeId(int id)
        //{
        //	var procedureName = "ShowCompanyForProvidedEmployeeId";
        //	var parameters = new DynamicParameters();
        //	parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        //	using (var connection = _context.CreateConnection())
        //	{
        //		var company = await connection.QueryFirstOrDefaultAsync<Company>
        //			(procedureName, parameters, commandType: CommandType.StoredProcedure);

        //		return company;
        //	}
        //}

        public async Task<Company> GetCompanyEmployeesMultipleResults(int id)
        {
            var query = "SELECT * FROM Company WHERE Id = @Id;" +
                        "SELECT * FROM Employee WHERE CompanyId = @Id;" +
                        "SELECT * FROM Employee WHERE CompanyId = @Id";

            using (var connection = _context.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { id }))
            {
                var company = await multi.ReadSingleOrDefaultAsync<Company>();
                if (company != null)
                    company.Employees = (await multi.ReadAsync<Employee>()).ToList();

                return company;
            }
        }

        public async Task<List<Company>> GetCompaniesEmployeesMultipleMapping()
        {


            var query = "select * from company c left join employee e on c.id = e.companyid";

            using (var connection = _context.CreateConnection())
            {

                var companydict = new Dictionary<int, Company>();

                var companies = await connection.QueryAsync<Company, Employee, Company>(
                    query, (company, employees) =>
                    {
                        if (!companydict.TryGetValue(company.Id, out var currentcompany))
                        {
                            currentcompany = company;

                            companydict.Add(currentcompany.Id, currentcompany);

                        }

                        currentcompany.Employees.Add(employees);
                        return currentcompany;
                    }
                );

                return companies.Distinct().ToList();
            }

        }




        //public async Task CreateMultipleCompanies(List<CompanyForCreationDto> companies)
        //{
        //	var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)";

        //	using (var connection = _context.CreateConnection())
        //	{
        //		connection.Open();

        //		using (var transaction = connection.BeginTransaction())
        //		{
        //			foreach (var company in companies)
        //			{
        //				var parameters = new DynamicParameters();
        //				parameters.Add("Name", company.Name, DbType.String);
        //				parameters.Add("Address", company.Address, DbType.String);
        //				parameters.Add("Country", company.Country, DbType.String);

        //				await connection.ExecuteAsync(query, parameters, transaction: transaction);
        //				//throw new Exception();
        //			}

        //			transaction.Commit();
        //		}
        //	}
        //}
    }
}
