using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Dtos
{
    public class CompanyDTO
    {
		[Required, StringLength(200, MinimumLength = 10)]
		public string Name { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string Country { get; set; }
	}
}
