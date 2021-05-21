using System;

namespace BookStore.Core.ViewModel
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public string Address { get; set; }
		public DateTime DateCreated { get; set; }
		public int IdUser { get; set; }
		//public virtual ICollection<DetailOrder> DetailOrders { get; } = new List<DetailOrder>();
	}
}
