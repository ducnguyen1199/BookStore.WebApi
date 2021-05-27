using AutoMapper;
using BookStore.Application.IService;
using BookStore.Core.Entity;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using BookStore.Core.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Service
{
	public class AuthorService : IAuthorService
	{
		private readonly IAuthorReponsitory _authorReponsitory;
		private readonly IMapper _mapper;
		public AuthorService(IAuthorReponsitory authorReponsitory, IMapper mapper)
		{
			_authorReponsitory = authorReponsitory;
			_mapper = mapper;
		}

		public async Task<AuthorViewModel> Add(AuthorFilterModel filter)
		{
			Author author = await _authorReponsitory.Add(_mapper.Map<Author>(filter));
			await _authorReponsitory.Commit();
			return _mapper.Map<AuthorViewModel>(author);
		}

		public async Task Delete(int id)
		{
			await _authorReponsitory.Delete(id);
			await _authorReponsitory.Commit();
		}

		public async Task<ListItemResponse<AuthorViewModel>> Get()
		{
			ListItemResponse<Author> authors = await _authorReponsitory.Get();
			return new ListItemResponse<AuthorViewModel>
			{
				Data = authors.Data.Select(u => _mapper.Map<AuthorViewModel>(u)),
				Count = authors.Count
			};
		}
		public async Task<AuthorViewModel> Get(int id)
		{
			Author authors = await _authorReponsitory.Get(id);
			return _mapper.Map<AuthorViewModel>(authors);
		}

		public async Task Update(int idAuthor, AuthorFilterModel filter)
		{
			await _authorReponsitory.Update(idAuthor, _mapper.Map<Author>(filter));
			await _authorReponsitory.Commit();
		}
		
		
	}
}
