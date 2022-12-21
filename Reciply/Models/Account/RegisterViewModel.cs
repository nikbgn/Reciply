namespace Reciply.Models.Account
{
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Filling this field is required!")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Filling this field is required!")]
		[Compare(nameof(PasswordRepeat))]
		[DataType(DataType.Password)]
		[DisplayName("New password")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Filling this field is required!")]
		[DataType(DataType.Password)]
		[DisplayName("Password repeat")]
		public string PasswordRepeat { get; set; }

	}
}
