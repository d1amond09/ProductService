namespace ProductService.Application.Common.Interfaces;

public interface ICurrentUserService
{
	Guid? UserId { get; }
	string? UserName { get; }
	string? UserEmail { get; }

	IReadOnlyCollection<string> UserRoles { get; }

	bool IsInRole(string roleName);
}
