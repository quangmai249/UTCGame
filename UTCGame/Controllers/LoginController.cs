using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace UTCGame.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public async Task Login()
		{
			await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
				new AuthenticationProperties
				{
					RedirectUri = Url.Action("GoogleResponse")
				});
		}

		public async Task<IActionResult> GoogleResponse()
		{
			var res = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			var claims = res.Principal.Identities.FirstOrDefault().Claims.Select(claims => new
			{
				claims.Issuer,
				claims.OriginalIssuer,
				claims.Type,
				claims.Value
			});

			if (HttpContext.User is ClaimsPrincipal principal)
			{
				if (principal.FindFirstValue(ClaimTypes.Email) == "quangmai249@gmail.com"
					&& principal.FindFirstValue(ClaimTypes.NameIdentifier) == "113888549758620092930")
				{
					return RedirectToAction("Index", "Home", new { area = "Admin" });
				}
			}
			return RedirectToAction(nameof(Index));
			//return Json(claims);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return View(nameof(Index));
		}
	}
}
