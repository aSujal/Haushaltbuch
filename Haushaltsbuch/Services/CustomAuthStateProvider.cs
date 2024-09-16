using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace Haushaltsbuch.Services;
public class CustomAuthStateProvider : AuthenticationStateProvider
{
	private readonly IJSRuntime _jsRuntime;

	public CustomAuthStateProvider(IJSRuntime jsRuntime)
	{
		_jsRuntime = jsRuntime;
	}

	public override Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
		{
			new Claim(ClaimTypes.Name, "TestUser"),
			new Claim(ClaimTypes.Role, "Admin")
		}, "fake"));

		return Task.FromResult(new AuthenticationState(user));
	}

	public void NotifyUserAuthentication(string username)
	{
		var identity = new ClaimsIdentity(new[]
		{
			new Claim(ClaimTypes.Name, username),
			new Claim(ClaimTypes.Role, "User")
		}, "fake");

		var user = new ClaimsPrincipal(identity);
		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
	}

	public void NotifyUserLogout()
	{
		var identity = new ClaimsIdentity();
		var user = new ClaimsPrincipal(identity);
		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
	}
}

