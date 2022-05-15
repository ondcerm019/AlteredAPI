using DrinksAPI.ViewModels;

namespace DrinksAPI.Models
{
    public class RootObject
    {
        public IList<RDrink>? Drinks { get; set; }
        public IList<RRestaurant>? Restaurants { get; set; }
    }

    public class RDrink
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IList<RelObj>? Restaurants { get; set; }
    }

    public class RRestaurant
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IList<RelObj>? Drinks { get; set; }
    }

    public class RelObj
    {
        public int Id { get; set; }
        public double Cost { get; set; }
    }
}
