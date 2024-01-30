using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTTest.Models;

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

            var brewResponse = new BrewResponse
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTime.Now,
            };

            return Ok(brewResponse);
        }
    }
}
