using System;
namespace PokemonReviewApp.Models
{
    public class Country
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Owner> Owners { get; set; }
    }
}

