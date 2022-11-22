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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAll()
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "select * from tblEmployee a join tblJobs b on a.Empl";
                var empdict = new Dictionary<int, Employee>();

                var result = await connection.QueryAsync<Employee, Jobs, Employee>(
                    sql, (emp, job) =>
                    {
                        if (!empdict.TryGetValue(emp.EmployeeId, out var currEmp))
                        {
                            currEmp = emp;

                            empdict.Add(currEmp.EmployeeId, currEmp);

                        }

                        currEmp.Job.Add(job);
                        return currEmp;
                    }
                );

                return result.Distinct().ToList();
            }
        }

        public Task Save(EmployeeDTO param)
        {
            throw new NotImplementedException();
        }

    }
}
