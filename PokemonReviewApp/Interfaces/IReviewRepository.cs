using System;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewRepository
	{
        ICollection<Review> GetReviews();
        Review GetReview(long reviewId);
        ICollection<Review> GetReviewsOfAPokemon(long pokemonId);
        bool ReviewExists(long reviewId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();
    }
}

