using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.ProfileModels
{
    public class EditAvatarRequestModel
    {
        [Required]
        public string NewAvatar { get; set; }
    }
}
