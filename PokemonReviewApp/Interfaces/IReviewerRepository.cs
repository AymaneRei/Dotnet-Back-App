using System;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewerRepository
	{
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(long reviewerId);
        ICollection<Review> GetReviewsByReviewer(long reviewerId);
        bool ReviewerExists(long reviewerId);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}