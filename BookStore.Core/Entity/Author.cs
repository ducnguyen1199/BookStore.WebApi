using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Entity
{
	public class Author
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required] 
		public DateTime BirthDay { get; set; }
		[Required]
		public string Story { get; set; }
		public string Avatar { get; set; }

		[InverseProperty(nameof(Book.Author))]
		public virtual ICollection<Book> Books { get; } = new List<Book>();
	}
}
