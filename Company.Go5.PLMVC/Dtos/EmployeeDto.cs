using System.ComponentModel;
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
        [Phone(ErrorMessage = "Phone number is not valid")] 
        public string Phone { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}$", ErrorMessage = "Invalid format. Expected format like : 123-street-city-country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }


    }
}
