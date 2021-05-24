using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using BookStore.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.IService
{
	public interface IUserService
	{
		Task<ListItemResponse<UserViewModel>> Get();
		Task<UserViewModel> GetById(int idUser);
		Task<UserViewModel> UpdateInfoUser(UserUpdateModel filter);

		Task Like(int idUser, int idBook);
		Task UnLike(int idUser, int idBook);
		Task AddBookIntoCart(BookInCartFilterModel filter);
		Task DeleteBookFromCart(List<int> arr);
		Task<ICollection<FavoriteViewModel>> GetBookLiked(int idUser);
		Task<ICollection<BooksInCartViewModel>> GetBookInCart(int idUser);
		Task<BooksInCartViewModel> UpdateBookInCart(int id, int quantity);

	}
}
