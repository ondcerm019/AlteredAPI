using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrinksAPI.Models
{
    public class RestaurantDrink
    {
        public int RestaurantId { get; set; }
        public int DrinkId { get; set; }
        public double Cost { get; set; }

        //Navigation properties
        [JsonIgnore]
        public Drink? Drink { get; set; }
        [JsonIgnore]
        public Restaurant? Restaurant { get; set; }
    }
}
