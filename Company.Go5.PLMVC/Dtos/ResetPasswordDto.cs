using System.ComponentModel.DataAnnotations;

namespace Company.Go5.PLMVC.Dtos
{
    public class ResetPasswordDto
    {

        [Required(ErrorMessage = " Password is required ")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword  is required ")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "confirm password must match password ")]
        public string ConfirmPassword { get; set; }


    }
}
