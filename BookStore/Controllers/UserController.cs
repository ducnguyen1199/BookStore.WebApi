using AutoMapper;
using BookStore.Core.Entity;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using BookStore.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using BookStore.Application.IService;
using BookStore.Core.FilterModel;
using System.Collections.Generic;

namespace BookStore.Controllers
{
	[Route("api/[Controller]")]
	[ApiController]
	public class UserController: ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IUserReponsitory _userReponsitory;
		private readonly IMapper _mapper;
		public UserController(IUserReponsitory userReponsitory, IMapper mapper, IUserService userService)
		{
			_userReponsitory = userReponsitory;
			_mapper = mapper;
			_userService = userService;
		}
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _userService.Get());
		}
		[HttpGet("{idUser}")]
		public async Task<IActionResult> GetById(int idUser)
		{
			var user = await _userService.GetById(idUser);
			return Ok(user);
		}

		[HttpGet("Like/{idUser}")]
		public async Task<IActionResult> GetBookLiked(int idUser)
		{
			var bookTest = await _userService.GetBookLiked(idUser);
			return Ok(bookTest);
		}
		[HttpPost("like")]
		public async Task<IActionResult> Like(int idUser, int idBook)
		{
			await _userService.Like(idUser, idBook);
			return Ok();
		}
		[HttpDelete("UnLike")]
		public async Task<IActionResult> UnLike(int idUser, int idBook)
		{
			await _userService.UnLike(idUser, idBook);
			return Ok();
		}

		[HttpGet("BooksInCart/{idUser}")]
		public async Task<IActionResult> GetBooksInCart(int idUser)
		{
			var bookTest = await _userService.GetBookInCart(idUser);
			return Ok(bookTest);
		}
		[HttpPost("AddBookIntoCart")]
		public async Task<IActionResult> AddBookIntoCart(BookInCartFilterModel fitler)
		{
			await _userService.AddBookIntoCart(fitler);
			return Ok();
		}
		[HttpDelete("DeleteBookFromCart")]
		public async Task<IActionResult> DeleteBookIntoCart([FromBody] List<int> arr)
		{
			await _userService.DeleteBookFromCart(arr);
			return Ok();
		}
	}
}
