using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.ProfileModels
{
    public class EditEmailRequestModel
    {
        [Required]
        public string NewEmail { get; set; }
    }
}
