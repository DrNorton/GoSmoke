using System.ComponentModel.DataAnnotations;

namespace GoSmokeBackend.Controllers.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
 
    }
}
