using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace BookStore.Core.ViewModel
{
	public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public IEnumerable<int> RoleIds { get; set; }
    }
}
