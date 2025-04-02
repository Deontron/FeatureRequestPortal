using System;
using Volo.Abp.Application.Dtos;

namespace FeatureRequestPortal.MyFeatures
{
    public class MyFeatureDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public MyFeatureCategory Category { get; set; }

        public DateTime PublishDate { get; set; }

        public string Description { get; set; }

        public bool IsApproved { get; set; }
    }
}
