using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using ProductService.Application.Common.Interfaces;

namespace ProductService.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
	private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

	public Guid? UserId
	{
		get
		{
			var userIdClaim = User?.FindFirstValue(ClaimTypes.NameIdentifier)
                          ?? User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

			return Guid.TryParse(userIdClaim, out var userId) 
				? userId 
				: null;
		}
	}

	public string? UserEmail => User?.FindFirstValue(ClaimTypes.Email);
	public string? UserName => User?.FindFirstValue(ClaimTypes.Name) 
		?? User?.FindFirstValue(JwtRegisteredClaimNames.Name);

	public IReadOnlyCollection<string> UserRoles => User?.FindAll(ClaimTypes.Role)
		.Select(c => c.Value)
		.ToList().AsReadOnly()
		?? (IReadOnlyCollection<string>)[];

	public bool IsInRole(string roleName) => User?.IsInRole(roleName) ?? false;
}

