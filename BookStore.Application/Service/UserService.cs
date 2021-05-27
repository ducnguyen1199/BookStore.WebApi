using AutoMapper;
using BookStore.Application.IService;
using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using BookStore.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Service
{
	public class UserService : IUserService
	{
		private readonly IUserReponsitory _userReponsitory;
		private readonly IMapper _mapper;

		public UserService(IUserReponsitory userReponsitory, IMapper mapper)
		{
			_userReponsitory = userReponsitory;
			_mapper = mapper;
		}
		public async Task<ListItemResponse<UserViewModel>> Get()
		{
			ListItemResponse<User> users = await _userReponsitory.Get();

			return new ListItemResponse<UserViewModel>
			{
				Data = users.Data.Select(u => _mapper.Map<UserViewModel>(u)),
				Count = users.Count
			};
		}
		public async Task<UserViewModel> GetById(int idUser)
		{
			User user = await _userReponsitory.GetDetail(idUser, DetailType.ById);
			return _mapper.Map<UserViewModel>(user);
		}
		public async Task<UserViewModel> UpdateInfoUser(int id, UserUpdateModel filter)
		{
			User user = await _userReponsitory.UpdateInfoUser(id, filter);
			await _userReponsitory.Commit();
			return _mapper.Map<UserViewModel>(user);
		}
		public async Task<UserViewModel> UpdateAvatar(int idUser, string avatar)
		{
			User user = await _userReponsitory.UpdateAvatar(idUser, avatar);
			await _userReponsitory.Commit();
			return _mapper.Map<UserViewModel>(user);
		}
		public async Task Like(int idUser, int idBook)
		{
			await _userReponsitory.Like(idUser, idBook);
			await _userReponsitory.Commit();
		}
		public async Task UnLike(int id)
		{
			await _userReponsitory.UnLike(id);
			await _userReponsitory.Commit();
		}
		public async Task<ICollection<FavoriteViewModel>> GetBookLiked(int idUser)
		{
			User user = await _userReponsitory.GetDetail(idUser, DetailType.Like);
			ICollection<FavoriteBook> favoriteBooks = user.FavoriteBooks;
			return _mapper.Map<ICollection<FavoriteViewModel>>(favoriteBooks);
		}
		public async Task<ICollection<BooksInCartViewModel>> GetBookInCart(int idUser)
		{
			User user = await _userReponsitory.GetDetail(idUser, DetailType.BooksIntoCart);
			ICollection<BooksInCart> booksInCarts = user.BooksInCarts;
			return _mapper.Map<ICollection<BooksInCartViewModel>>(booksInCarts);
		}
		public async Task<BooksInCartViewModel> AddBookIntoCart(BookInCartFilterModel filter)
		{
			BooksInCart booksInCart = await _userReponsitory.AddBookIntoCart(filter);
			await _userReponsitory.Commit();
			booksInCart = await _userReponsitory.GetById(booksInCart.Id);
			return _mapper.Map<BooksInCartViewModel>(booksInCart);
		}
		public async Task DeleteBookFromCart(List<int> arr)
		{
			await _userReponsitory.DeleteBookFromCart(arr);
			await _userReponsitory.Commit();
		}
		public async Task<BooksInCartViewModel> UpdateBookInCart(int id, int quantity)
		{
			BooksInCart booksInCart =await _userReponsitory.UpdateBookInCart(id, quantity);
			await _userReponsitory.Commit();
			return _mapper.Map<BooksInCartViewModel>(booksInCart);

		}
	}
}
