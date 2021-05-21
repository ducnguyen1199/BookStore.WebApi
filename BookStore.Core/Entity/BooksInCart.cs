using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Entity
{
	public class BooksInCart
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public DateTime DateUpdated { get; set; }
		[Required]
		public double SubTotal { get; set; }

		[Required]
		public int IdBook { get; set; }
		[ForeignKey(nameof(IdBook))]
		public virtual Book Book { get; set; }

		[Required]
		public int IdUser { get; set; }
		[ForeignKey(nameof(IdUser))]
		public virtual User User { get; set; }
	}
}
