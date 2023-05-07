using System;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IUserRepository
	{
        ICollection<UserEntity> GetUsers();
        UserEntity GetUser(long userId);
        UserEntity GetUser(string name);
        UserEntity GetUserTrimToUpper(UserRequestDto userCreate);
        public ICollection<Role> GetRoleByUser(long userId);
        bool UserExists(long userId);
        bool UserExists(string username);
        bool CreateUser(string roleName, UserEntity user);
        bool UpdateUser(UserEntity user);
        bool DeleteUser(UserEntity user);
        bool Save();
    }
}

