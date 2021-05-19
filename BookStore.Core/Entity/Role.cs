using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookStore.Core.Entity
{
	public class Role : IdentityRole<int>
	{
		public virtual ICollection<IdentityUserRole<int>> UserRoles { get; } = new List<IdentityUserRole<int>>();
	}
}
