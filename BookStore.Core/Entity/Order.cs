using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Entity
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Address { get; set; }
		public double Surcharge { get; set; }
		public double Discount { get; set; }
		[Required]
		public double Total { get; set; }

		[Required]
		public DateTime DateCreated { get; set; }

		[Required]
		public int IdUser { get; set; }
		[ForeignKey(nameof(IdUser))]
		public virtual User User { get; set; }

		public virtual ICollection<DetailOrder> DetailOrders { get; } = new List<DetailOrder>();
	}
}
