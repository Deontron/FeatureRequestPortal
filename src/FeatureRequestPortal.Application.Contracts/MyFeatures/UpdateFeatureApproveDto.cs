using System;

namespace FeatureRequestPortal.MyFeatures
{
    public class UpdateFeatureApproveDto
    {
        public Guid FeatureId { get; set; }
        public bool IsApproved { get; set; }
    }
}
