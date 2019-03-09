using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.ProfileModels
{
    public class EditNameRequestModel
    {
        [Required]
        public string NewName { get; set; }
    }
}
