using BookStore.Core.Entity;
using BookStore.Core.FilterModel;
using BookStore.Core.Shared;
using BookStore.Core.ViewModel;
using System.Threading.Tasks;

namespace BookStore.Application.IService
{
	public interface IOrderService
	{
		Task AddOrder(OrderFilterModel filter);
		Task<ListItemResponse<OrderViewModel>> GetListOrder();
	}
}
