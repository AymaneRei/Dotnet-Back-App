using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using AutoMapper;
using PokemonReviewApp.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        public IActionResult GetRoles()
        {
            var roles = _roleRepository.GetRoles();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult GetRoles(long roleId)
        {
            if (!_roleRepository.RoleExists(roleId))
            {
                return NotFound();
            }

            var role = _roleRepository.GetRole(roleId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(role);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRole([FromBody] Role roleCreate)
        {
            if (roleCreate == null)
                return BadRequest(ModelState);

            var roles = _roleRepository.GetRoleTrimToUpper(roleCreate);

            if (roles != null)
            {
                ModelState.AddModelError("", "Role already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_roleRepository.CreateRole(roleCreate))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRole(long roleId, [FromBody] Role updatedRole)
        {
            if (updatedRole == null)
                return BadRequest(ModelState);

            if (roleId != updatedRole.Id)
                return BadRequest(ModelState);

            if (!_roleRepository.RoleExists(roleId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_roleRepository.UpdateRole(updatedRole))
            {
                ModelState.AddModelError("", "Something went wrong when updating Role");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRole(long roleId)
        {
            if (!_roleRepository.RoleExists(roleId))
            {
                return NotFound();
            }

            var roleToDelete = _roleRepository.GetRole(roleId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_roleRepository.DeleteRole(roleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting Role");
            }

            return NoContent();
        }
    }
}

