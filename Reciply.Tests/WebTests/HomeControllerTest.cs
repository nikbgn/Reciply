namespace Reciply.Tests.HomeControllerTests
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	using Moq;

	using Reciply.Contracts;
	using Reciply.Controllers;

	public class HomeControllerTest
	{
		private readonly Mock<ILogger<HomeController>> _mockHomeController = new Mock<ILogger<HomeController>>();
		private readonly Mock<IRecipeService> _mockRecipeService = new Mock<IRecipeService>();
		
		[Fact]
		public async Task Index_Should_Return_View()
		{
			var homeController = new HomeController(_mockHomeController.Object, _mockRecipeService.Object);

			var result = await homeController.Index();

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public void About_Should_Return_View()
		{
			var homeController = new HomeController(_mockHomeController.Object, _mockRecipeService.Object);

			var result =  homeController.About();

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
		}
	}



}
