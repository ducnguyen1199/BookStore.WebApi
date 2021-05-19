using BookStore.Core.Entity;
using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using System.Threading.Tasks;
using TopCV.Core.Repository;

namespace BookStore.Core.Repository
{
	public interface IBookReponsitory: IRepository<Book>
	{
		Task Add(Book book);
		Task Delete(int id);
		Task<ListItemResponse<Book>> Get(BookFilterModel data );
		
	}
}
