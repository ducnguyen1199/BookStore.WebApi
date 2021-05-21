using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Entity
{
	public class FavoriteBook
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int IdUser { get; set; }

		//[ForeignKey(nameof(IdUser))]
		[Required]
		public int IdBook { get; set; }

		[ForeignKey(nameof(IdBook))]
		public virtual Book Book { get; set; }
	}
}
