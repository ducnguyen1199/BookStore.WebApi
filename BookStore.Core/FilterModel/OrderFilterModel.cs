﻿namespace BookStore.Core.FilterModel
{
	public class OrderFilterModel
	{
		public int IdUser { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public double Surcharge { get; set; }
		public double Discount { get; set; }
	}
}
