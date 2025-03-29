using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace FeatureRequestPortal.MyFeatures
{
    public class MyFeature : AuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }

        public MyFeatureCategory Category { get; set; }

        public DateTime PublishDate { get; set; }

        public string Description { get; set; }
    }
}
