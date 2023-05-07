using System;
namespace PokemonReviewApp.Dto
{
	public class UserUpdateRequestDto
	{
        public long Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}

