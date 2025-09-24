using System.ComponentModel.DataAnnotations;

namespace Company.Go5.PLMVC.Dtos
{
    public class DepartmentDto
    {

        //client side validation 
        [Required(ErrorMessage = "code is required ")]
        [Required(ErrorMessage ="code is required ")]
        public string Code { get; set; }

        [Required(ErrorMessage = "name is required ")]

        public string Name { get; set; }

        [Required(ErrorMessage = "create at  is required ")]

        public DateTime CreateAt { get; set; }
    }
}
