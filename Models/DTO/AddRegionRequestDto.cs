using System.ComponentModel.DataAnnotations;

namespace IndiaWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        //-------------With Model Validation also implemented in RegionsController-----------------------------
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a Minimum of 3 Characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a Maximum of 3 Characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name should not be more than 100 Characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }


        /*--------------Without Model Validation-------------
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
        */
    }
}
