using System.ComponentModel.DataAnnotations;

namespace giri_webdev_livedemo.Models
{
    /// <summary>
    /// JUNE-22-2015
    /// </summary>
    public class ValidationModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(6)]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please enter only alphabets.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(6)]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please enter only alphabets.")]
        public string LastName { get; set; }

        [Required]
        [Range(19, 60)]
        public int Age { get; set; }

        [Required]
        [MaxLength(10)]
        public string Designation { get; set; }

        [Required]
        [MaxLength(10)]
        public string Department { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}