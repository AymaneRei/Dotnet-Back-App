using System;
namespace PokemonReviewApp.Models
{
	public class UserRole
	{
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public UserEntity User { get; set; }
        public Role Role { get; set; }
    }
}

