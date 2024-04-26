using System.ComponentModel.DataAnnotations;

namespace MVC_FirstProject.PL.ViewModels.Account
{
	public class ResetPasswordViewModel
	{

		[Required(ErrorMessage = "New Password Is Required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }


		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password does not match with New Password")]
		public string ConfirmPassword { get; set; }
	}
}
