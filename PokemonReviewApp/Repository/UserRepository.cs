using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(string roleName, UserEntity user)
        {
            var role = _context.Roles.Where(a => a.Name == roleName).FirstOrDefault();

            var userRole = new UserRole()
            {
                User = user,
                Role = role,
            };

            _context.Add(userRole);

            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(UserEntity user)
        {
            _context.Remove(user);
            return Save();
        }

        public UserEntity GetUser(long userId)
        {
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public UserEntity GetUser(string name)
        {
            return _context.Users.Where(u => u.Username == name).FirstOrDefault();
        }

        public ICollection<UserEntity> GetUsers()
        {
            return _context.Users.ToList();
        }

        public UserEntity GetUserTrimToUpper(UserRequestDto userCreate)
        {
            return GetUsers().Where(c => c.Username.Trim().ToUpper() == userCreate.Username.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public ICollection<Role> GetRoleByUser(long userId)
        {
            var roles = _context.UserRoles.Where(ur => ur.UserId == userId).Select(ur => ur.Role).ToList(); ;

            return roles;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(UserEntity user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(long userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }
    }
}

