using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.ViewModel
{
	public class AuthorViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDay { get; set; }
		public string Story { get; set; }
	}
}
