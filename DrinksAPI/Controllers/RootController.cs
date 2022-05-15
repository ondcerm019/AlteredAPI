using DrinksAPI.Data;
using DrinksAPI.Models;
using DrinksAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrinksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RootController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RootController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RootObject>>> GetDrinks()
        {
            return Ok(new RootObject {
                Drinks = await _context.Drinks.Include(d => d.Restaurants)
                .ThenInclude(d => d.Restaurant)
                .Select(d => new RDrink { Id = d.Id, Name = d.Name, Restaurants = d.Restaurants.Select(r => new RelObj { Id = r.RestaurantId, Cost = r.Cost }).ToList() }).ToListAsync(),
                Restaurants = await _context.Restaurants.Include(r => r.Drinks).ThenInclude(r => r.Drink).Select(r => new RRestaurant { Id = r.Id, Name = r.Name, Drinks = r.Drinks.Select(d => new RelObj { Id = d.DrinkId, Cost = d.Cost }).ToList() }).ToListAsync()
                });
        }
    }
}

