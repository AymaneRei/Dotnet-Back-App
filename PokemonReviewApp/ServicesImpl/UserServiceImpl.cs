
using System;
using PokemonReviewApp.Services;
using System.Security.Claims;

namespace PokemonReviewApp.ServicesImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserServiceImpl(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMyName()
        {
            var result = string.Empty;

            if (_httpContextAccessor.HttpContext is not null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }

            return result;
        }
    }
}

