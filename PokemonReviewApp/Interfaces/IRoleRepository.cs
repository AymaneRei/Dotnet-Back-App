using System;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IRoleRepository
	{
        ICollection<Role> GetRoles();
        Role GetRole(long roleId);
        Role GetRole(string name);
        Role GetRoleTrimToUpper(Role roleCreate);
        bool RoleExists(long roleId);
        bool RoleExists(string name);
        bool CreateRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(Role role);
        bool Save();
    }
}

