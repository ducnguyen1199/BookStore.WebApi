﻿using System;
using System.Collections.Generic;

namespace BookStore.Core.ViewModel
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public DateTime DateCreated { get; set; }
		public int IdUser { get; set; }
		public double Surcharge { get; set; }
		public double Discount { get; set; }
		public double Total { get; set; }
		public virtual ICollection<DetailOrderViewModel> DetailOrders { get; } = new List<DetailOrderViewModel>();
	}
}
