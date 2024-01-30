﻿using System;
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
        private readonly CoffeeContext _context;

        public CoffeeItemsController(CoffeeContext context)
        {
            _context = context;
        }

        // GET: api/CoffeeItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoffeeItem>>> GetCoffeeItems()
        {
            return await _context.CoffeeItems.ToListAsync();
        }

        // GET: api/CoffeeItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoffeeItem>> GetCoffeeItem(long id)
        {
            var coffeeItem = await _context.CoffeeItems.FindAsync(id);

            if (coffeeItem == null)
            {
                return NotFound();
            }

            return coffeeItem;
        }

        // PUT: api/CoffeeItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoffeeItem(long id, CoffeeItem coffeeItem)
        {
            if (id != coffeeItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(coffeeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoffeeItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CoffeeItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CoffeeItem>> PostCoffeeItem(CoffeeItem coffeeItem)
        {
            coffeeItem.BrewTime = DateTime.UtcNow;
            _context.CoffeeItems.Add(coffeeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCoffeeItem), new { id = coffeeItem.Id }, coffeeItem);
        }

        // DELETE: api/CoffeeItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffeeItem(long id)
        {
            var coffeeItem = await _context.CoffeeItems.FindAsync(id);
            if (coffeeItem == null)
            {
                return NotFound();
            }

            _context.CoffeeItems.Remove(coffeeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoffeeItemExists(long id)
        {
            return _context.CoffeeItems.Any(e => e.Id == id);
        }
    }
}