using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
	public interface IUserReponsitory :IRepository 
	{
		Task<ListItemResponse<User>> Get();
		Task<User> GetByUserName(string username);
		Task Like(int idUser, int idBook);
		Task UnLike(int idUser, int idBook);
		Task AddBookIntoCart(BookInCartFilterModel filter);
		Task DeleteBookFromCart(List<int> arr);
		Task<User> GetDetail(int id, DetailType type);
	}
}
