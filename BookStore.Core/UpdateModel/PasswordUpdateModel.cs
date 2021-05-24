namespace BookStore.Core.UpdateModel
{
	public class PasswordUpdateModel
	{
		public string UserName { get; set; }
		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
