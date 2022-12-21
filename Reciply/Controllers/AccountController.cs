namespace Reciply.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Reciply.Data.Models;
	using Reciply.Extensions;
	using Reciply.Models.Account;

	[Authorize]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;
		private readonly ILogger<AccountController> logger;

		public AccountController(
			UserManager<User> _userManager,
			SignInManager<User> _signInManager,
			ILogger<AccountController> _logger)
		{
			userManager = _userManager;
			signInManager = _signInManager;
			logger = _logger;
		}


		[HttpGet]
		[AllowAnonymous]
		public IActionResult AccessDenied()
		{
			return View();
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			if (User?.Identity?.IsAuthenticated ?? false)
			{
				return RedirectToAction("Index", "Home");
			}

			var model = new RegisterViewModel();
			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var newUser = new User()
			{
				Email = model.Email,
				UserName = model.Email
			};

			var result = await userManager.CreateAsync(newUser, model.Password);


			if (result.Succeeded)
			{
				await signInManager.SignInAsync(newUser, isPersistent: false);
				return RedirectToAction("Index", "Home");
			}

			foreach (var item in result.Errors)
			{
				ModelState.AddModelError("", item.Description);
			}

			return View(model);
		}


		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login(string? returnUrl = null)
		{

			if (User?.Identity?.IsAuthenticated ?? false)
			{
				return RedirectToAction("Index", "Home");
			}

			var model = new LoginViewModel()
			{
				ReturnUrl = returnUrl
			};
			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await userManager.FindByEmailAsync(model.Email);

			if (user != null)
			{
				var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					if (model.ReturnUrl != null) return Redirect(model.ReturnUrl);
					return RedirectToAction("Index", "Home");
				}
			}
			ModelState.AddModelError("", "Login failed...");

			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			try
			{
				await signInManager.SignOutAsync();
				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex) { logger.LogError($"ERROR MESSAGE: {ex.Message}"); return BadRequest(); }
		}

		[HttpGet]
		public IActionResult Manage()
		{
			return View();
		}


		[HttpGet]
		public IActionResult ChangeEmail()
		{
			var model = new ChangeEmailViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var currentUser = await userManager.FindByIdAsync(User.Id());
				var token = await userManager.GenerateChangeEmailTokenAsync(currentUser, model.NewEmail);
				await userManager.ChangeEmailAsync(currentUser, model.NewEmail, token);
			}
			catch (Exception ex) { logger.LogError($"ERROR MESSAGE: {ex.Message}"); return BadRequest(); }

			return RedirectToAction(nameof(Manage));
		}


		[HttpGet]
		public IActionResult ChangePassword()
		{
			var model = new ChangePasswordViewModel();
			return View(model);
		}

		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			try
			{
				var currentUser = await userManager.FindByIdAsync(User.Id());
				await userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.Password);
			}
			catch (Exception ex) { logger.LogError($"ERROR MESSAGE: {ex.Message}"); return BadRequest(); }
			return RedirectToAction(nameof(Manage));
		}


	}
}
