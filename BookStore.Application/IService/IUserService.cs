using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using BookStore.Core.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.IService
{
	public interface IUserService
	{
		Task<ListItemResponse<UserViewModel>> Get();
		Task<UserViewModel> GetById(int idUser);
		Task<UserViewModel> UpdateInfoUser(int id, UserUpdateModel filter);
		Task<UserViewModel> UpdateAvatar(int idUser, string avatar);
		Task Like(int idUser, int idBook);
		Task UnLike(int id);
		Task<BooksInCartViewModel> AddBookIntoCart(BookInCartFilterModel filter);
		Task DeleteBookFromCart(List<int> arr);
		Task<ICollection<FavoriteViewModel>> GetBookLiked(int idUser);
		Task<ICollection<BooksInCartViewModel>> GetBookInCart(int idUser);
		Task<BooksInCartViewModel> UpdateBookInCart(int id, int quantity);

	}
}
