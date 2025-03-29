using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FeatureRequestPortal.MyFeatures
{
    public class MyFeatureAppService :
    CrudAppService<
        MyFeature, 
        MyFeatureDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateMyFeatureDto>, 
    IMyFeatureAppService 
    {
        public MyFeatureAppService(IRepository<MyFeature, Guid> repository)
        : base(repository)
        {

        }
    }
}
