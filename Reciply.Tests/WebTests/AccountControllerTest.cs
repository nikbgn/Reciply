namespace Reciply.Tests.WebTests
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Moq;
	using Reciply.Contracts;
	using Reciply.Controllers;
	using Reciply.Data.Models;

	public class AccountControllerTest
	{
		private readonly Mock<UserManager<User>> userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
		private readonly Mock<SignInManager<User>> signInManagerMock = new Mock<SignInManager<User>>();
		private readonly Mock<ILogger<AccountController>> loggerMock = new Mock<ILogger<AccountController>>();

		[Fact]
		public async Task AccessDenied_Should_Return_View()
		{
			var accountController = new AccountController
				(userManagerMock.Object,
				null,
				loggerMock.Object);

			var result = accountController.AccessDenied();
			Assert.IsType<ViewResult>(result);

		}

		[Fact]
		public async Task GET_Register_Should_Return_View()
		{
			var accountController = new AccountController
				(userManagerMock.Object,
				null,
				loggerMock.Object);

			var result = accountController.Register();
			Assert.IsType<ViewResult>(result);

		}

		[Fact]
		public async Task GET_Login_Should_Return_View()
		{
			var accountController = new AccountController
				(userManagerMock.Object,
				null,
				loggerMock.Object);

			var result = accountController.Login();
			Assert.IsType<ViewResult>(result);

		}

		[Fact]
		public async Task Manage_Should_Return_View()
		{
			var accountController = new AccountController
				(userManagerMock.Object,
				null,
				loggerMock.Object);

			var result = accountController.Manage();
			Assert.IsType<ViewResult>(result);

		}

		[Fact]
		public async Task GET_ChangeEmail_Should_Return_View()
		{
			var accountController = new AccountController
				(userManagerMock.Object,
				null,
				loggerMock.Object);

			var result = accountController.ChangeEmail();
			Assert.IsType<ViewResult>(result);

		}

		[Fact]
		public async Task GET_ChangePassword_Should_Return_View()
		{
			var accountController = new AccountController
				(userManagerMock.Object,
				null,
				loggerMock.Object);

			var result = accountController.ChangePassword();
			Assert.IsType<ViewResult>(result);

		}


	}
}
