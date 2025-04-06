
using System;
using Volo.Abp.Domain.Entities;

namespace FeatureRequestPortal.MyFeatures
{
    public class MyComment : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid FeatureRequestId { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
