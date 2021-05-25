using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using BookStore.Core.ViewModel;
using System.Threading.Tasks;

namespace BookStore.Application.IService
{
	public interface IBookService
	{
		Task Add(NewBookFilterModel filter);
		Task<ListItemResponse<BookViewModel>> GetAll(string KeyWord, int? IdCategory, int? IdAuthor, int? OrderBy, int Skip, int Offset);
		Task<BookViewModel> GetDetail(int id);
		Task  Delete(int id);
		Task Update(int id, BookUpdateModel data);
	}
}
