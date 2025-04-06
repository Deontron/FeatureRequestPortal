using System;
using Volo.Abp.Application.Dtos;

namespace FeatureRequestPortal.MyFeatures
{
    public class MyCommentDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public Guid FeatureRequestId { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
