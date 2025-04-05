using FeatureRequestPortal.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FeatureRequestPortal.MyFeatures
{
    [RemoteService(IsEnabled = true)]
    public class MyFeatureAppService :
        CrudAppService<
            MyFeature,
            MyFeatureDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateMyFeatureDto>,
        IMyFeatureAppService
    {
        private readonly IRepository<MyFeature, Guid> _featureRepository;

        public MyFeatureAppService(IRepository<MyFeature, Guid> repository)
        : base(repository)
        {
            _featureRepository = repository;

            GetPolicyName = FeatureRequestPortalPermissions.MyFeatures.Default;
            //CreatePolicyName = FeatureRequestPortalPermissions.MyFeatures.Create;
            UpdatePolicyName = FeatureRequestPortalPermissions.MyFeatures.Edit;
            DeletePolicyName = FeatureRequestPortalPermissions.MyFeatures.Delete;
        }

        public async Task<List<MyFeatureDto>> GetAllFeaturesAsync()
        {
            var features = await _featureRepository.GetListAsync();
            var featureDtos = new List<MyFeatureDto>();

            foreach (var feature in features)
            {
                featureDtos.Add(new MyFeatureDto
                {
                    Id = feature.Id,
                    Title = feature.Title,
                    Description = feature.Description,
                    CreatorId = feature.CreatorId
                });
            }

            return featureDtos;
        }

        public async Task<object> UpdateFeatureScoreAsync(UpdateFeatureScoreDto input)
        {
            var feature = await _featureRepository.FirstOrDefaultAsync(f => f.Id == input.FeatureId);
            if (feature == null)
            {
                return null;
            }

            if (input.ScoreType == "like")
            {
                feature.Point++;
            }
            else if (input.ScoreType == "dislike")
            {
                feature.Point--;
            }
            else
            {
                return null;
            }

            await _featureRepository.UpdateAsync(feature);

            return new
            {
                message = "Puan başarıyla güncellendi.",
                point = feature.Point
            };
        }
    }
}
