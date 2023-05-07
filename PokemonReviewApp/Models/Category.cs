﻿using System;
namespace PokemonReviewApp.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
    }
}
