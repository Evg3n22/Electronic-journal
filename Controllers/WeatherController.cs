using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElectronicJournal.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetWeather(double latitude, double longitude)
        {
            string apiKey = "c5c314cd2c712d87b92764d4edad2bae";

            HttpClient httpClient = _httpClientFactory.CreateClient();

            try
            {
                string url = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric&lang=ru";
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Перетворення англійских назв погоди на українські
                    responseBody = ReplaceEnglishWeatherNamesWithRussian(responseBody);

                    // Встановлення кодировки UTF-8 для відповіді
                    Response.ContentType = "application/json; charset=utf-8";

                    return Content(responseBody, "application/json");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                httpClient.Dispose();
            }
        }

        private string ReplaceEnglishWeatherNamesWithRussian(string responseBody)
        {
            // Заміняю англійські назви погоди на українські
            responseBody = responseBody.Replace("Thunderstorm", "Гроза");
            responseBody = responseBody.Replace("Drizzle", "Мряка");
            responseBody = responseBody.Replace("Rain", "Дощ");
            responseBody = responseBody.Replace("Snow", "Сніг");
            responseBody = responseBody.Replace("Mist", "Туман");
            responseBody = responseBody.Replace("Smoke", "Дим");
            responseBody = responseBody.Replace("Haze", "Мгла");
            responseBody = responseBody.Replace("Dust", "Пил");
            responseBody = responseBody.Replace("Fog", "Туман");
            responseBody = responseBody.Replace("Sand", "Пісок");
            responseBody = responseBody.Replace("Ash", "Попіл");
            responseBody = responseBody.Replace("Squall", "Шквал");
            responseBody = responseBody.Replace("Tornado", "Торнадо");
            responseBody = responseBody.Replace("Clear", "Ясно");
            responseBody = responseBody.Replace("Clouds", "Хмари");

            return responseBody;
        }


    }
}
