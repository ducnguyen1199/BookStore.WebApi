using BookStore.Core.Entity;
using System;
using System.Collections.Generic;

namespace BookStore.Core.ViewModel
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public string Address { get; set; }
		public DateTime DateCreated { get; set; }
		public int IdUser { get; set; }
		public virtual ICollection<DetailOrderViewModel> DetailOrders { get; } = new List<DetailOrderViewModel>();
	}
}
