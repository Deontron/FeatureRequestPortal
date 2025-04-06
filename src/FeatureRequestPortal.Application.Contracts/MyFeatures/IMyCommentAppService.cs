using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace FeatureRequestPortal.MyFeatures
{
    public interface IMyCommentAppService : IApplicationService
    {
        Task<MyCommentDto> CreateAsync(CreateMyCommentDto input);

        Task<List<MyCommentDto>> GetCommentsByFeatureRequestIdAsync(Guid featureRequestId);

        Task<MyCommentDto> UpdateAsync(Guid id, UpdateMyCommentDto input);

        Task DeleteAsync(Guid id);
    }
}
