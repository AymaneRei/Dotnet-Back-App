using System;
namespace PokemonReviewApp.Models
{
	public class Review
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public decimal Rating { get; set; }
		public Reviewer Reviewer { get; set; }
		public Pokemon Pokemon { get; set; }
	}
}

