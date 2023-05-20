using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using RapidApiConsume.Models;
using System.Linq;

namespace RapidApiConsume.Controllers
{
    public class BookingByCityController : Controller
    {
        public async Task<IActionResult> Index(string cityID)
        {
            if (!string.IsNullOrEmpty(cityID))
            {
           
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/search?checkin_date=2023-05-24&dest_type=city&units=metric&checkout_date=2023-05-27&adults_number=2&order_by=popularity&dest_id={cityID}8&filter_by_currency=EUR&locale=en-gb&room_number=1&children_number=2&children_ages=5%2C0&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&page_number=0&include_adjacency=true"),
                    Headers =
    {
        { "X-RapidAPI-Key", "1946b52275mshf1840b95d04a272p1d9d52jsn7faad28fdcab" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<BookingApiViewModel>(body);
                    return View(values.Property1.ToList());
                }
            }
            else
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/hotels/search?checkin_date=2023-05-24&dest_type=city&units=metric&checkout_date=2023-05-27&adults_number=2&order_by=popularity&dest_id=-1456928&filter_by_currency=EUR&locale=en-gb&room_number=1&children_number=2&children_ages=5%2C0&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&page_number=0&include_adjacency=true"),
                    Headers =
    {
        { "X-RapidAPI-Key", "1946b52275mshf1840b95d04a272p1d9d52jsn7faad28fdcab" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<BookingApiViewModel>(body);
                    return View(values.Property1.ToList());
                }
            }
        }
    }
}
