 using System;
namespace PokemonReviewApp.Models
{
	public class Reviewer
	{
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
        public ICollection<Review> Reviews { get; set; }
	}
}

