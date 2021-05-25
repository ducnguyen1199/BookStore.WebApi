using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using BookStore.Core.ViewModel;
using System.Threading.Tasks;

namespace BookStore.Application.IService
{
	public interface IAuthorService
	{
		Task<ListItemResponse<AuthorViewModel>> Get();
		Task Add(AuthorFilterModel filter);
		Task Delete(int id);
		Task Update(int idAuthor, AuthorFilterModel filter);
	}
}
