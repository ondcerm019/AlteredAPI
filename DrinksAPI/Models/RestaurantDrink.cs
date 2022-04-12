using System.ComponentModel.DataAnnotations.Schema;

namespace DrinksAPI.Models
{
    public class RestaurantDrink
    {
        public int RestaurantId { get; set; }
        public int DrinkId { get; set; }
        public double Cost { get; set; }

        //Navigation properties
        public Drink? Drink { get; set; }
        public Restaurant? Restaurant { get; set; }
    }
}
