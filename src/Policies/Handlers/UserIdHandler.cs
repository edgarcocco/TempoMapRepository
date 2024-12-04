using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol;
using System.Security.Claims;
using TempoMapRepository.Models.Domain;
using TempoMapRepository.Models.Identity;
using TempoMapRepository.Policies.Requirements;

namespace TempoMapRepository.Policies.Handlers
{
    public class UserIdHandler : AuthorizationHandler<UserIdRequirement, string>
    {
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       UserIdRequirement requirement,
                                                       string id)
        {
            var userId = context.User.FindFirst(
           c => c.Type == ClaimTypes.NameIdentifier);
            if (userId?.Value == id)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
