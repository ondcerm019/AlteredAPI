using System.Text.Json.Serialization;

namespace DrinksAPI.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //Navigation properties
        [JsonIgnore]
        public ICollection<RestaurantDrink>? Restaurants { get; set; }
    }
}
