using System;

namespace FeatureRequestPortal.MyFeatures
{
    public class UpdateFeatureScoreDto
    {
        public Guid FeatureId { get; set; }
        public string ScoreType { get; set; }
    }
}
