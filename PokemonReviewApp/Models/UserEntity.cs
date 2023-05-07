using System;
using Microsoft.EntityFrameworkCore;

namespace PokemonReviewApp.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class UserEntity
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; }

    }
}

