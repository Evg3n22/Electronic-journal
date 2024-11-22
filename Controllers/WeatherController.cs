using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading;
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

        public IActionResult GetWeather(double latitude, double longitude)
        {
            string apiKey = "c5c314cd2c712d87b92764d4edad2bae";

            var tcs = new TaskCompletionSource<IActionResult>();

            ThreadPool.QueueUserWorkItem(async _ =>
            {
                try
                {
                    string url = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric&lang=ru";

                    HttpClient httpClient = _httpClientFactory.CreateClient();

                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Перетворення англійских назв погоди на українські
                        responseBody = ReplaceEnglishWeatherNamesWithUkrainian(responseBody);

                        // Встановлення кодировки UTF-8 для відповіді
                        tcs.SetResult(Content(responseBody, "application/json; charset=utf-8"));
                    }
                    else
                    {
                        tcs.SetResult(StatusCode((int)response.StatusCode, response.ReasonPhrase));
                    }

                    httpClient.Dispose();
                }
                catch (Exception ex)
                {
                    tcs.SetResult(StatusCode(500, ex.Message));
                }
            });

            return tcs.Task.Result;
        }

        private string ReplaceEnglishWeatherNamesWithUkrainian(string responseBody)
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
