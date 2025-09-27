using System.ComponentModel.DataAnnotations;

namespace Company.Go5.PLMVC.Dtos
{
    public class EmployeeDto
    {
        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }

        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress,ErrorMessage ="type is not valid for email")]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public DateTime HiringDate { get; set; }


    }
}
