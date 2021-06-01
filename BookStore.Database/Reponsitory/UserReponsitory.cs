using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Database.Reponsitory
{
	public class UserReponsitory : IUserReponsitory
	{
		private readonly ApplicationDbContext _context;
		public UserReponsitory(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Commit() => await _context.SaveChangesAsync();
		public async Task<ListItemResponse<User>> Get()
		{
			IQueryable<User> users = _context.Users;
			return new ListItemResponse<User>
			{
				Data = await users.ToListAsync(),
				Count = await users.CountAsync()
			};
		}
		public async Task<User> GetByUserName(string username)
		{
			User user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.UserName == username);
			return user != null ? user : null;
		}
		public async Task<User> GetDetail(int id, DetailType type) {
			if(type == DetailType.ById) return await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == id);
			IQueryable<User> users = _context.Users;
			switch (type)
			{
				case DetailType.Like:
					users = users.Include(u => u.FavoriteBooks).ThenInclude(fb => fb.Book.Author)
						.Include(u => u.FavoriteBooks).ThenInclude(fb => fb.Book.Category);
					break;
				case DetailType.BooksIntoCart:
					users = users.Include(u => u.BooksInCarts).ThenInclude(fb => fb.Book.Author)
						.Include(u => u.FavoriteBooks).ThenInclude(fb => fb.Book.Category);
					break;
				case DetailType.BooksIntoOrder:
					users = users.Include(u => u.Orders).ThenInclude(o => o.DetailOrders).ThenInclude(fb => fb.Book.Author)
						.Include(u => u.FavoriteBooks).ThenInclude(fb => fb.Book.Category);
					break;
				default:
					break;
			}
			return await users.FirstOrDefaultAsync(u => u.Id == id);
		}
		public async Task<User> UpdateInfoUser(int id, UserUpdateModel filter)
		{
			User user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == id);
			user.Email = filter.Email;
			user.BirthDay = filter.BirthDay;
			user.FullName = filter.FullName;
			user.PhoneNumber = filter.PhoneNumber;
			return user;
		}
		public async Task<User> UpdateAvatar(int id, string avatar)
		{
			User user = await _context.Users.FindAsync(id);
			user.Avatar = "https://localhost:44369/avatars/"+ avatar;
			return user;
		}
		public async Task UpdatePassword(int id, string newPasswordHash)
		{
			User user = await _context.Users.FindAsync(id);
			user.PasswordHash = newPasswordHash;
		}
		public async Task<int> Like(int idUser, int idBook)
		{
			FavoriteBook fb = await _context.FavoriteBooks.FirstOrDefaultAsync(f => f.IdBook == idBook && f.IdUser == idUser);
			if (fb != null) return -1;
			FavoriteBook nfb = new FavoriteBook();
			nfb.IdBook = idBook;
			nfb.IdUser = idUser;

			await _context.FavoriteBooks.AddAsync(nfb);
			return nfb.Id;
		}
		public async Task UnLike(int id)
		{
			FavoriteBook check = await _context.FavoriteBooks.FindAsync(id);
			if (check != null) _context.FavoriteBooks.Remove(check);
		}
		public async Task<FavoriteBook> GetBookLiked(int idUser,int idBook) => await _context.FavoriteBooks.Include(f => f.Book.Author).Include(f=> f.Book.Category).FirstOrDefaultAsync(f => f.IdBook ==idBook && f.IdUser == idUser);
		public async Task<BooksInCart> AddBookIntoCart(BookInCartFilterModel filter)
		{
			Book book = await _context.Books.FindAsync(filter.IdBook);
			BooksInCart booksInCart = await _context.BooksInCarts.FirstOrDefaultAsync(b => b.IdBook == filter.IdBook && b.IdUser == filter.IdUser);
			if(booksInCart != null)
			{
				booksInCart.Quantity++;
				booksInCart.SubTotal = booksInCart.Quantity * book.Price;
				return booksInCart;
			}
			BooksInCart newBook =new BooksInCart();
			newBook.IdBook = filter.IdBook;
			newBook.IdUser = filter.IdUser;
			newBook.Quantity = filter.Quantity;
			newBook.SubTotal = filter.Quantity * book.Price;

			await _context.BooksInCarts.AddAsync(newBook);
			return newBook;
		}
		public async Task<BooksInCart> GetById(int id)
		{
			return await _context.BooksInCarts.Include(b => b.Book.Author).Include(b => b.Book.Category).FirstOrDefaultAsync(b => b.Id == id);
		}
		public async Task DeleteBookFromCart(List<int> arr) {
			var books = await _context.BooksInCarts.Where(b => arr.Any(id => b.Id == id)).ToListAsync();
			_context.BooksInCarts.RemoveRange(books);
		}
		public async Task<BooksInCart> UpdateBookInCart(int id, int quantity)
		{
			BooksInCart booksInCart = await _context.BooksInCarts.Include(b => b.Book).FirstOrDefaultAsync(b => b.Id == id);
			booksInCart.Quantity = quantity;
			booksInCart.SubTotal = booksInCart.Book.Price * quantity;
			booksInCart.DateUpdated = DateTime.Now;
			return booksInCart;
		}
	}
}
