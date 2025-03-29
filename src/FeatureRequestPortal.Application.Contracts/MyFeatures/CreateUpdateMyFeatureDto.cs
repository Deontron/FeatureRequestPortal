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
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
