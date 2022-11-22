using ORM.Dtos;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Contracts
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetAll();
        public Task Save(EmployeeDTO param);

    }
}
