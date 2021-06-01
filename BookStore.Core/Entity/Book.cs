using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Entity
{
	public class Book
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public double Price { get; set; }
		[Required]
		public string Image { get; set; }
		[Required]
		public DateTime DateCreated { get; set; }


		[Required]
		public int IdCategory { get; set; }
		[ForeignKey(nameof(IdCategory))]
		public virtual Category Category { get; set; }

		[Required]
		public int IdAuthor { get; set; }
		[ForeignKey(nameof(IdAuthor))]
		public virtual Author Author { get; set; }

		[InverseProperty(nameof(FavoriteBook.Book))]
		public virtual ICollection<FavoriteBook> FavoriteBooks { get; } = new List<FavoriteBook>();
		[InverseProperty(nameof(DetailOrder.Book))]
		public virtual ICollection<DetailOrder> DetailOrders { get; } = new List<DetailOrder>();
	}
}
