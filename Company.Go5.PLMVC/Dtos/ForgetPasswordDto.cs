using System.ComponentModel.DataAnnotations;

namespace Company.Go5.PLMVC.Dtos
{
    public class ForgetPasswordDto
    {


        [Required(ErrorMessage = "Email is required ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}