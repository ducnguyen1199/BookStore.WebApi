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
	public class OrderService: IOrderService
	{
		private readonly IOrderReponsitory _orderReponsitory;
		private readonly IMapper _mapper;
		public OrderService(IOrderReponsitory orderReponsitory, IMapper mapper)
		{
			_orderReponsitory = orderReponsitory;
			_mapper = mapper;
		}

		public async Task AddOrder(OrderFilterModel filter)
		{
			Order order = new Order();
			order.IdUser = filter.IdUser;
			order.Address = filter.Address;
			await _orderReponsitory.AddOrder(order);
			await _orderReponsitory.Commit();
		}

		public async Task<ListItemResponse<OrderViewModel>> GetListOrder()
		{
			ListItemResponse<Order> orders = await _orderReponsitory.GetListOrder();
			return new ListItemResponse<OrderViewModel>()
			{
				Data = orders.Data.Select(o => _mapper.Map<OrderViewModel>(o)),
				Count = orders.Count
			};
		}
	}
}
