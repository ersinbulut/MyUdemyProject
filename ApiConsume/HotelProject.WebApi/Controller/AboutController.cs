using HotelProject.BusinessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebApi.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class AboutController : ControllerBase
	{
		private readonly IAboutService _aboutService;

		public AboutController(IAboutService aboutService)
		{
			_aboutService = aboutService;
		}

		[HttpGet]
		public IActionResult RoomList()
		{
			var values = _aboutService.TGetList();
			return Ok(values);
		}
		[HttpPost]
		public IActionResult AddRoom(About about)
		{
			_aboutService.TInsert(about);
			return Ok();
		}
		[HttpDelete]
		public IActionResult DeleteRoom(int id)
		{
			var values = _aboutService.TGetByID(id);
			_aboutService.TDelete(values);
			return Ok();
		}
		[HttpPut]
		public IActionResult UpdateRoom(About about)
		{
			_aboutService.TUpdate(about);
			return Ok();
		}
		[HttpGet("{id}")]
		public IActionResult GetRoom(int id)
		{
			var values = _aboutService.TGetByID(id);
			return Ok(values);
		}
	}
}
