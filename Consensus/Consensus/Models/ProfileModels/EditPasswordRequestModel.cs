using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.ProfileModels
{
    public class EditPasswordRequestModel
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
