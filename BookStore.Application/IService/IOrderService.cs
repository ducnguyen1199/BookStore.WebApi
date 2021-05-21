using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using BookStore.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.IService
{
	public interface IOrderService
	{
		Task<ICollection<OrderViewModel>> GetBooksInOrder(int idUser);
		Task<int> AddOrder(OrderFilterModel filter);
		Task<ListItemResponse<OrderViewModel>> GetListOrder();
		Task AddBooksIntoOrder(OrderFilterModel filter);
		Task Delete(int id);
	}
}
