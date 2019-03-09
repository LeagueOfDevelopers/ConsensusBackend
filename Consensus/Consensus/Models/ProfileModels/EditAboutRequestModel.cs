using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.ProfileModels
{
    public class EditAboutRequestModel
    {
        [Required]
        public string NewAbout { get; set; }
    }
}
