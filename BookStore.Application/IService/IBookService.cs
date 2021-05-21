using BookStore.Core.Shared;
using BookStore.Core.ViewModel;
using System.Threading.Tasks;

namespace BookStore.Application.IService
{
	public interface IBookService
	{
		Task<ListItemResponse<BookViewModel>> GetAll(string KeyWord, int? IdCategory, int? IdAuthor, int? OrderBy, int Skip, int Offset);
	}
}
