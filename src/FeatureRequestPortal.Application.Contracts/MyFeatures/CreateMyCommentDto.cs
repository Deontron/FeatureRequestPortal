using System;
using System.ComponentModel.DataAnnotations;

namespace FeatureRequestPortal.MyFeatures
{
    public class CreateMyCommentDto
    {
        [Required]
        public Guid FeatureRequestId { get; set; }
        [Required]
        public string Content { get; set; } = String.Empty;
    }
}
