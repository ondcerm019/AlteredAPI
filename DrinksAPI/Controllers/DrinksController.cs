#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrinksAPI.Data;
using DrinksAPI.Models;
using DrinksAPI.ViewModels;

namespace DrinksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrinksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drink>>> GetDrinks()
        {
            return Ok(await _context.Drinks.ToListAsync());
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drink>> GetDrink(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            return Ok(drink);
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(int id, DrinkVM drinkvm)
        {
            var drink = new Drink { Id = id, Name = drinkvm.Name };
            _context.Entry(drink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(drink);
        }

        // POST: api/Drinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Drink>> PostDrink(DrinkVM drinkvm)
        {
            var drink = new Drink { Name = drinkvm.Name };
            _context.Drinks.Add(drink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrink", new { id = drink.Id }, drink);
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrink(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();

            return Ok(drink);
        }




        [HttpGet("{id}/restaurants")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetDrinkRestaurants(int id)
        {
            if (!DrinkExists(id))
            {
                return NotFound();
            }

            var restaurants = await _context.rdRelation.Include(e => e.Restaurant).Where(e => e.DrinkId == id).Select(e => new { Id = e.RestaurantId, e.Restaurant.Name, e.Cost }).ToListAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}/restaurants/averageCost")]
        public async Task<ActionResult<double?>> GetDrinkRestaurantAverageCost(int id)
        {
            if (!DrinkExists(id))
            {
                return NotFound();
            }

            try
            {
                return await _context.rdRelation.Where(e => e.DrinkId == id).AverageAsync(e => e.Cost);
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet("{id}/restaurants/count")]
        public async Task<ActionResult<int>> GetDrinkRestaurantCount(int id)
        {
            if (!DrinkExists(id))
            {
                return NotFound();
            }

            var restaurantcount = await _context.rdRelation.Where(e => e.DrinkId == id).CountAsync();
            return Ok(restaurantcount);
        }




        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.Id == id);
        }

        private bool RestaurantDrinkExists(int restaurantId, int drinkId)
        {
            return _context.rdRelation.Any(e => e.RestaurantId == restaurantId && e.DrinkId == drinkId);
        }
    }
}
