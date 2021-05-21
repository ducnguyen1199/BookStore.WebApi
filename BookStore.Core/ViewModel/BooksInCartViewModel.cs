using System;

namespace BookStore.Core.ViewModel
{
	public class BooksInCartViewModel
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public double SubTotal { get; set; }
		public DateTime DateUpdated { get; set; }
		public BookViewModel Book { get; set; }
	}
}
