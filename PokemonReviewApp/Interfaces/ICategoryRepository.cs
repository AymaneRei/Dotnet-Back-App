using System;
using PokemonReviewApp.Models;
namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(long categoryId);
        ICollection<Pokemon> GetPokemonByCategory(long categoryId);
        bool CategoryExists(long categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}