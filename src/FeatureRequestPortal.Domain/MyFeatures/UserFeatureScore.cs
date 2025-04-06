using System;
using Volo.Abp.Domain.Entities;

namespace FeatureRequestPortal.MyFeatures
{
    public class UserFeatureScore : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid FeatureId { get; set; }
        public string ScoreType { get; set; } 
    }
}
