using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using Microsoft.EntityFrameworkCore;
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
			User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
			return user != null ? user : null;
		}
		public async Task<User> GetDetail(int id, DetailType type) {
			if(type == DetailType.ById) return await _context.Users.FindAsync(id);
			IQueryable<User> users = _context.Users;
			switch (type)
			{
				case DetailType.Like:
					users = users.Include(u => u.FavoriteBooks).ThenInclude(fb => fb.Book);
					break;
				case DetailType.BooksIntoCart:
					users = users.Include(u => u.BooksInCarts).ThenInclude(b => b.Book);
					break;
				default:
					break;
			}
			return await users.FirstOrDefaultAsync(u => u.Id == id);
		}
		public async Task Like(int idUser, int idBook)
		{
			FavoriteBook fb = new FavoriteBook();
			fb.IdBook = idBook;
			fb.IdUser = idUser;
			await _context.FavoriteBooks.AddAsync(fb);
		}
		public async Task UnLike(int idUser, int idBook)
		{
			FavoriteBook check = await _context.FavoriteBooks.FirstOrDefaultAsync(fb => fb.IdUser == idUser && fb.IdBook == idBook);
			if (check != null) _context.FavoriteBooks.Remove(check);
		}
		public async Task AddBookIntoCart(BookInCartFilterModel filter)
		{
			Book book = await _context.Books.FindAsync(filter.IdBook);

			BooksInCart booksInCart =new BooksInCart();
			booksInCart.IdBook = filter.IdBook;
			booksInCart.IdUser = filter.IdUser;
			booksInCart.Quantity = filter.Quantity;
			booksInCart.SubTotal = filter.Quantity * book.Price;

			await _context.BooksInCarts.AddAsync(booksInCart);
		}
		public async Task DeleteBookFromCart(int id) {
			BooksInCart booksInCart = await _context.BooksInCarts.FindAsync(id);
			_context.BooksInCarts.Remove(booksInCart); 
		}
	}
}
