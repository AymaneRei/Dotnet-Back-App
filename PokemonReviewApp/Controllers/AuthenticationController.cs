using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;
using PokemonReviewApp.Services;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;
using Microsoft.AspNetCore.Identity;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration configuration, IUserService userService,
            IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _configuration = configuration;
            _userService = userService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet("myName"), Authorize]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userService.GetMyName());

            //var userName = User?.Identity?.Name;
            //var roleClaims = User?.FindAll(ClaimTypes.Role);
            //var roles = roleClaims?.Select(c => c.Value).ToList();
            //var roles2 = User?.Claims
            //    .Where(c => c.Type == ClaimTypes.Role)
            //    .Select(c => c.Value)
            //    .ToList();
            //return Ok(new { userName, roles, roles2 });
        }

        [HttpGet, Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserResponseDto>))]
        public IActionResult Getusers()
        {
            var users = _mapper.Map<List<UserResponseDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{username}"), Authorize]
        [ProducesResponseType(200, Type = typeof(UserResponseDto))]
        [ProducesResponseType(400)]
        public IActionResult Getusers(string username)
        {
            if (!_userRepository.UserExists(username))
            {
                return NotFound();
            }

            var user = _mapper.Map<UserResponseDto>(_userRepository.GetUser(username));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpPost("register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult<UserResponseDto> Register(UserRequestDto request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            if (request == null)
                return BadRequest(ModelState);

            var users = _userRepository.GetUserTrimToUpper(request);

            if (users != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserEntity user = new UserEntity();
            var roleUser = _roleRepository.GetRole("User");

            user.Username = request.Username;
            user.PasswordHash = passwordHash;

            if (!_userRepository.CreateUser(roleUser.Name, user))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            var userResponse = _mapper.Map<UserResponseDto>(user);

            return Ok(userResponse);
        }

        [HttpPost("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult<TokenResponseDto> Login(UserRequestDto request)
        {
            if (request == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var user = _userRepository.GetUser(request.Username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            TokenResponseDto tokenResponseDto = new TokenResponseDto();
            tokenResponseDto.access_token = token;

            return Ok(tokenResponseDto);
        }

        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
            foreach (var role in _userRepository.GetRoleByUser(user.Id))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpPut("{username}"), Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(string username, [FromBody] UserUpdateRequestDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (username != updatedUser.Username)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(username))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);

            UserEntity user = new UserEntity();

            user.Id = updatedUser.Id;
            user.Username = updatedUser.Username;
            user.PasswordHash = passwordHash;

            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong when updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{username}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(string username)
        {
            if (!_userRepository.UserExists(username))
            {
                return NotFound();
            }

            var userToDelete = _userRepository.GetUser(username);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting user");
            }

            return NoContent();
        }
    }
}

