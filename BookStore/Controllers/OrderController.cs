using AutoMapper;
using BookStore.Application.IService;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderReponsitory _orderReponsitory;
		private readonly IMapper _mapper;
		private readonly IOrderService _orderService;
		public OrderController(IOrderReponsitory orderReponsitory, IMapper mapper, IOrderService orderService)
		{
			_orderReponsitory = orderReponsitory;
			_mapper = mapper;
			_orderService = orderService;
		}
		[HttpGet]
		public async Task<IActionResult> GetListOrder()
		{
			return Ok(await _orderService.GetListOrder());
		}
		[HttpPost]
		public async Task<IActionResult> AddOrder([FromBody] OrderFilterModel filter)
		{
			await _orderService.AddOrder(filter);
			return Ok();
		}
	}
	
}
