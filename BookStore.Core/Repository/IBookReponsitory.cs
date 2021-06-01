using BookStore.Core.Entity;
using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
	public interface IBookReponsitory: IRepository 
	{
		Task<Book> Add(Book book);
		Task Delete(int id);
		Task<ListItemResponse<Book>> Get(BookFilterModel data );
		Task<ListItemResponse<Book>> GetTrending();
		Task<Book> GetDetail(int idUser);
		Task Update(int id, BookUpdateModel data);
	}
}
