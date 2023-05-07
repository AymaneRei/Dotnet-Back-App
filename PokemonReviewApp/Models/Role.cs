using System;
using Microsoft.EntityFrameworkCore;

namespace PokemonReviewApp.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Role
	{
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; }
    }
}

