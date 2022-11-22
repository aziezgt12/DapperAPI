using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Entities
{
	public class Employee
	{
		public int EmployeeId { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirht { get; set; }
		public string Gender { get; set; }
		public string Address { get; set; }
		public List<Jobs> Job { get; set; } = new List<Jobs>();
	}

	public class Jobs
	{
		public int JobId { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public decimal Sallary { get; set; }
		public bool Status { get; set; }

	}
}
