using System;


namespace BookStore.Core.UpdateModel
{
	public class UserUpdateModel
	{
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
    }
}
