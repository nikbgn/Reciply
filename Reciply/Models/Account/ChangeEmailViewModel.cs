namespace Reciply.Models.Account
{
	using System.ComponentModel.DataAnnotations;

	public class ChangeEmailViewModel
	{
		[Required(ErrorMessage = "Filling this field is mandatory!")]
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "New Email")]
		public string NewEmail { get; set; }
	}
}
