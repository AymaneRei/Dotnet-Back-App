using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Models;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRole(Role role)
        {
            _context.Add(role);

            return Save();
        }

        public bool DeleteRole(Role role)
        {
            _context.Remove(role);
            return Save();
        }

        public Role GetRole(long roleId)
        {
            return _context.Roles.Where(r => r.Id == roleId).FirstOrDefault();
        }

        public Role GetRole(string name)
        {
            return _context.Roles.Where(r => r.Name == name).FirstOrDefault();
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.OrderBy(r => r.Id).ToList();
        }

        public Role GetRoleTrimToUpper(Role roleCreate)
        {
            return GetRoles().Where(r => r.Name.Trim().ToUpper() == roleCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool RoleExists(long roleId)
        {
            return _context.Roles.Any(r => r.Id == roleId);
        }

        public bool RoleExists(string name)
        {
            return _context.Roles.Any(r => r.Name == name);
        }

        public bool UpdateRole(Role role)
        {
            _context.Update(role);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

