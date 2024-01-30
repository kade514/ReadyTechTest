using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RTTest.Models;
using static System.Net.WebRequestMethods;

namespace RTTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeItemsController : ControllerBase
    {
        private static int _numCalls = 0;

        // GET: api/BrewCoffee
        [HttpGet("brew-coffee")]
        public async Task<ActionResult<BrewResponse>> BrewCoffee()
        {
            //fail on april first
            if (DateTime.Now.Month.Equals(4) && DateTime.Now.Day.Equals(1))
            {
                return StatusCode(418, "I'm a teapot");
            }

            //track number of times this task is called and fail on every fifth call.
            _numCalls++;

            if (_numCalls % 5 == 0)
            {
                _numCalls = 0;
                return StatusCode(503);
            }
            
            //hardcoded melbourne since it's not specified in the docs that it shouldn't be.
            var url = "http://api.weatherapi.com/v1/current.json?key=d53b376451cf43a9bec11853243001&q=Melbourne&aqi=no";
            WeatherResponse weather = null;

            using (var client = new HttpClient())
            { 
                var response = client.GetAsync(url);

                if (response.Result.RequestMessage == null)
                {
                    return BadRequest("response was null");
                }

                var content = await response.Result.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content))
                    return BadRequest("Empty weather data");

                weather = JsonConvert.DeserializeObject<WeatherResponse>(content);
            }

            var brewResponse = new BrewResponse
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTime.Now,
            };

            if (weather != null && weather.current.temp_c >= 30)
            {
                brewResponse.Message = "Your refreshing iced coffee is ready";
            }


            return StatusCode(200, brewResponse);
        }
    }
}
