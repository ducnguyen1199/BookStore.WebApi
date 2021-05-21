using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.FilterModel
{
	public class BookInCartFilterModel
	{
		public int IdBook { get; set; }
		public int IdUser { get; set; }
		public int Quantity { get; set; }
		
	}
}
