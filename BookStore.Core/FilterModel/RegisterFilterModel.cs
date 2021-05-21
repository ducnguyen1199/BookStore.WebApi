using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.FilterModel
{
	public class RegisterFilterModel
	{
		public string UserName { get; set; }

		public string Password { get; set; }
		public string FullName { get; set; }

		public DateTime BirthDay { get; set; }

		public string Email { get; set; }
		public string PhoneNumber { get; set; }

	}
}
