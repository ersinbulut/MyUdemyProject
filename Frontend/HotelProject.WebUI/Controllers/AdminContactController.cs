using HotelProject.WebUI.Dtos.ContactDto;
using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class AdminContactController : Controller
    {
		private readonly IHttpClientFactory _httpClientFactory;

		public AdminContactController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Inbox()
		{
			var client = _httpClientFactory.CreateClient();
			var resonseMessage = await client.GetAsync("http://localhost:61639/api/Contact");
			if (resonseMessage.IsSuccessStatusCode)
			{
				var jsonData = await resonseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<InboxContactDto>>(jsonData);
				return View(values);
			}
			return View();
		}

        public PartialViewResult SideBarAdminContactPartial()
        {
            return PartialView();
        }

		public PartialViewResult SideBarAdminContactCategoryPartial()
		{
			return PartialView();
		}
	}
}
