using System;
using System.ComponentModel.DataAnnotations;

namespace FeatureRequestPortal.MyFeatures
{
    public class CreateUpdateMyFeatureDto
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public MyFeatureCategory Category { get; set; } = MyFeatureCategory.Undefined;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
