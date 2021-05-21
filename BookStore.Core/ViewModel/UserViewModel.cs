using BookStore.Core.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual ICollection<IdentityUserRole<int>> UserRoles { get; } = new List<IdentityUserRole<int>>();
    }
}
