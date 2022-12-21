namespace Reciply.Models.Account
{
	using System.ComponentModel.DataAnnotations;

	public class LoginViewModel
	{
		[Required(ErrorMessage = "Filling this field is mandatory!")]
		[EmailAddress(ErrorMessage = "Please use a valid email!")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Filling this field is mandatory!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[UIHint("hidden")]
		public string? ReturnUrl { get; set; }
	}
}
