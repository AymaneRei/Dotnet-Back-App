using System;
namespace PokemonReviewApp.Models
{
	public class PokemonOwner
	{
        public long PokemonId { get; set; }
        public long OwnerId { get; set; }
        public Pokemon Pokemon { get; set; }
        public Owner Owner { get; set; }
	}
}

