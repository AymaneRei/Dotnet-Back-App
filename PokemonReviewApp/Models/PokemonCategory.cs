using System;
namespace PokemonReviewApp.Models
{
	public class PokemonCategory
	{
		public long PokemonId { get; set; }
		public long CategoryId { get; set; }
		public Pokemon Pokemon { get; set; }
		public Category Category { get; set; }
	}
}

