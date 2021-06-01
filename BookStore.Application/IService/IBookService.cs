using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using BookStore.Core.ViewModel;
using System.Threading.Tasks;

namespace BookStore.Application.IService
{
	public interface IBookService
	{
		Task<BookViewModel> Add(NewBookFilterModel filter);
		Task<ListItemResponse<BookViewModel>> GetAll(BookFilterModel filter);
		Task<BookViewModel> GetDetail(int id);
		Task<ListItemResponse<BookViewModel>> GetTrending();
		Task Delete(int id);
		Task Update(int id, BookUpdateModel data);
	}
}
