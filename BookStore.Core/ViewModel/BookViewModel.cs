using System;

namespace BookStore.Core.ViewModel
{
	public class BookViewModel
	{
		public int Id { get; set; }
		public int IdCategory { get; set; }
		public int IdAuthor { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public string Image { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
