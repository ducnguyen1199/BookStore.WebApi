using System;

namespace BookStore.Core.ViewModel
{
	public class AuthorViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDay { get; set; }
		public string Story { get; set; }
		public string Avatar { get; set; }
		public int BookCount { get; set; }
	}
}
