using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Entity
{
	public class DetailOrder
	{
		[Required]
		public int IdOrder { get; set; }
		[ForeignKey(nameof(IdOrder))]
		public virtual Order Order { get; set; }

		[Required]
		public int IdBook { get; set; }
		[ForeignKey(nameof(IdBook))]
		public virtual Book Book { get; set; }

		[Required]
		public int Quantity { get; set; }
		[Required]
		public int Subtotal { get; set; }
	}
}
