using ApplicationCore.Interfaces;
using IdentityModel;
using System.Security.Claims;

namespace Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Subject);

        public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Name);

        public string? FirstName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.GivenName);

        public string? LastName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.FamilyName);

        public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Email);
    }
}
