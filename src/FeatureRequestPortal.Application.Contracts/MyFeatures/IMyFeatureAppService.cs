using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FeatureRequestPortal.MyFeatures
{
    public interface IMyFeatureAppService :
    ICrudAppService< //Defines CRUD methods
        MyFeatureDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        UpdateMyFeatureDto,
        CreateMyFeatureDto> //Used to create/update a book
    {
        public Task<object> UpdateFeatureScoreAsync(UpdateFeatureScoreDto input);
        public Task<UserFeatureScoreDto> GetUserFeatureScoreAsync(Guid featureId);
        public Task ApproveFeatureAsync(UpdateFeatureApproveDto input);
        Task<MyFeatureDto> GetFeatureDetailsAsync(Guid id);
    }
}
