using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Entities
{

		public class Status
        {
			public int code { get; set; }
			public string message { get; set; }
        }

		public static class HttpResponse
        {
			public static Status setStatus(int httpStatusCode, string message)
			{
				Status res = new Status
				{
					code = httpStatusCode,
					message = message
				};

				return res;
			}

		}
	
}
