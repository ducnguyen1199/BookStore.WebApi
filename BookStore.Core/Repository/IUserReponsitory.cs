﻿using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
	public interface IUserReponsitory :IRepository 
	{
		Task<ListItemResponse<User>> Get();
		Task<User> GetByUserName(string username);
		Task<User> GetDetail(int id, DetailType type);
		Task<User> UpdateInfoUser(int id,UserUpdateModel filter);
		Task<User> UpdateAvatar(int id, string avatar);
		Task UpdatePassword(int id, string newHashPassword);
		Task<int> Like(int idUser, int idBook);
		Task UnLike(int id);
		Task<FavoriteBook> GetBookLiked(int idUser, int idBook);
		Task<BooksInCart> AddBookIntoCart(BookInCartFilterModel filter);
		Task DeleteBookFromCart(List<int> arr);
		Task<BooksInCart> UpdateBookInCart(int id, int quantity);
		Task<BooksInCart> GetById(int id);
	}
}
