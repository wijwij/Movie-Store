using System;
using System.Linq;
using System.Security.Claims;
using MovieStore.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Http;

namespace MovieStore.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        /**
         * It's only necessary to use IHttpContextAccessor when you need access to the HttpContext inside a service.
         * [From ASP.NET Core docs]
         */
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? Id => Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        
        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string Email => _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public string RemoteIpAddress => _httpContextAccessor.HttpContext.Request.Host.Host + ":" +
                                         _httpContextAccessor.HttpContext.Request.Host.Port;
    }
}