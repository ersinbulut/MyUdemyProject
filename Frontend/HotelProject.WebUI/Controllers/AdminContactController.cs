﻿using HotelProject.WebUI.Dtos.ContactDto;
using HotelProject.WebUI.Dtos.SendMessageDto;
using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
			var responseMessage = await client.GetAsync("http://localhost:61639/api/Contact");

            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("http://localhost:61639/api/Contact/GetContactCount");

			var client3 = _httpClientFactory.CreateClient();
			var responseMessage3 = await client2.GetAsync("http://localhost:61639/api/SendMessage/GetSendMessageCount");

			if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<InboxContactDto>>(jsonData);
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                ViewBag.contactCount = jsonData2;

				var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
				ViewBag.sendMessageCount = jsonData3;
				return View(values);
            }
            return View();
		}

		public async Task<IActionResult> Sendbox()
		{
			var client = _httpClientFactory.CreateClient();
			var resonseMessage = await client.GetAsync("http://localhost:61639/api/SendMessage");
			if (resonseMessage.IsSuccessStatusCode)
			{
				var jsonData = await resonseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultSendboxDto>>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpGet]
		public IActionResult AddSendMessage()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddSendMessage(CreateSendMessage createSendMessage)
		{
			createSendMessage.SenderMail = "admin@gmail.com";
			createSendMessage.SenderName = "admin";
			createSendMessage.Date =DateTime.Parse(DateTime.Now.ToShortDateString());
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createSendMessage);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("http://localhost:61639/api/SendMessage", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("SendBox");
			}
			return View();
		}

		public async Task<PartialViewResult> SideBarAdminContactPartial()
        {
            var client = _httpClientFactory.CreateClient();
            var resonseMessage2 = await client.GetAsync("http://localhost:61639/api/GetContactCount");
            if (resonseMessage2.IsSuccessStatusCode)
            {
                var jsonData = await resonseMessage2.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<InboxContactDto>>(jsonData);
				var jsonData2 = await resonseMessage2.Content.ReadAsStringAsync();
                ViewBag.contactCount = jsonData2;
            }
            return PartialView();
        }

		public PartialViewResult SideBarAdminContactCategoryPartial()
		{
			return PartialView();
		}

		public async Task<IActionResult> MessageDetailsBySendbox(int id)
		{
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:61639/api/SendMessage/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetMessageByIDDto>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> MessageDetailsByInbox(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:61639/api/Contact/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<InboxContactDto>(jsonData);
                return View(values);
            }
            return View();
        }


        public async Task<IActionResult> GetContactCount()
        {
            var client = _httpClientFactory.CreateClient();
            var resonseMessage = await client.GetAsync("http://localhost:61639/api/GetContactCount");
            if (resonseMessage.IsSuccessStatusCode)
            {
                var jsonData = await resonseMessage.Content.ReadAsStringAsync();
				ViewBag.data = jsonData;
                return View();
            }
            return View();
        }
    }
}
