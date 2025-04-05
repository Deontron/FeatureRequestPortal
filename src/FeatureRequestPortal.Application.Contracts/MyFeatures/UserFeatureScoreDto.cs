using System;
using Volo.Abp.Application.Dtos;

namespace FeatureRequestPortal.MyFeatures
{
    public class UserFeatureScoreDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public Guid FeatureId { get; set; }
        public string ScoreType { get; set; }  // like / dislike
    }
}