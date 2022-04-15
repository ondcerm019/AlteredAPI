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
    public class RestaurantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return Ok(await _context.Restaurants.ToListAsync());
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, RestaurantVM restaurantvm)
        {
            var restaurant = new Restaurant { Id = id, Name = restaurantvm.Name };
            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(restaurant);
        }

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(RestaurantVM restaurantvm)
        {
            var restaurant = new Restaurant { Name = restaurantvm.Name };
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return Ok(restaurant);
        }

        





        [HttpGet("{id}/drinks")]
        public async Task<ActionResult<IEnumerable<Drink>>> GetRestaurantDrinks(int id)
        {
            var drinks = await _context.rdRelation.Include(e => e.Drink).Where(e => e.RestaurantId == id).Select(e => new { Id = e.DrinkId, e.Drink.Name, e.Cost }).ToListAsync();
            return Ok(drinks);
        }

        [HttpPost("{restaurantId}/drinks/{drinkId}")]
        public async Task<ActionResult<RestaurantDrink>> PostRestaurantDrink(int restaurantId, int drinkId, RestaurantDrinkVM relvm)
        {
            if (!RestaurantExists(restaurantId) || !DrinkExists(drinkId))
            {
                return NotFound();
            }

            var rel = new RestaurantDrink { RestaurantId = restaurantId, DrinkId = drinkId, Cost = relvm.Cost };
            _context.rdRelation.Add(rel);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantDrinkExists(restaurantId, drinkId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(rel);
        }

        [HttpPut("{restaurantId}/drinks/{drinkId}")]
        public async Task<IActionResult> PutRestaurantDrink(int restaurantId, int drinkId, RestaurantDrinkVM relvm)
        {
            if (!RestaurantExists(restaurantId) || !DrinkExists(drinkId))
            {
                return NotFound();
            }

            var rel = new RestaurantDrink { RestaurantId = restaurantId, DrinkId = drinkId, Cost = relvm.Cost };
            _context.Entry(rel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantDrinkExists(restaurantId, drinkId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(rel);
        }

        [HttpDelete("{restaurantId}/drinks/{drinkId}")]
        public async Task<IActionResult> DeleteRestaurantDrink(int restaurantId, int drinkId)
        {
            var rel = await _context.rdRelation.FindAsync(restaurantId, drinkId);
            if (rel == null)
            {
                return NotFound();
            }

            _context.rdRelation.Remove(rel);
            await _context.SaveChangesAsync();

            return Ok(rel);
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
