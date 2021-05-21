namespace BookStore.Core.ViewModel
{
	public class DetailOrderViewModel
	{
		public int Quantity { get; set; }
		public double Subtotal { get; set; }
		public BookViewModel Book { get; set; }
	}
}
