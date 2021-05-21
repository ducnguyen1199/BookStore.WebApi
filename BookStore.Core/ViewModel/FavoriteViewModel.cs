namespace BookStore.Core.ViewModel
{
	public class FavoriteViewModel
	{
		public int Id { get; set; }
		public virtual BookViewModel Book { get; set; }
	}
}
