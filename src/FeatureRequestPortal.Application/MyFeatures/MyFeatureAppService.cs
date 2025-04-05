using FeatureRequestPortal.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

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
        private readonly IRepository<UserFeatureScore, Guid> _userFeatureScoreRepository;
        private readonly ICurrentUser _currentUser;

        public MyFeatureAppService(IRepository<MyFeature, Guid> repository, IRepository<UserFeatureScore, Guid> userFeatureScoreRepository,
        ICurrentUser currentUser)
        : base(repository)
        {
            _featureRepository = repository;
            _userFeatureScoreRepository = userFeatureScoreRepository;
            _currentUser = currentUser;

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

            var userScore = await _userFeatureScoreRepository.FirstOrDefaultAsync(u => u.UserId == _currentUser.Id && u.FeatureId == input.FeatureId);

            if (userScore != null)
            {
                if (userScore.ScoreType == input.ScoreType)
                {
                    if (input.ScoreType == "like")
                    {
                        feature.Point--;  // Puanı azalt
                        userScore.ScoreType = "none";  // Skor türünü kaldır
                        await _userFeatureScoreRepository.UpdateAsync(userScore);  // Güncelleme
                        await _featureRepository.UpdateAsync(feature);  // Özellik puanını güncelle
                        return new
                        {
                            message = "Like işlemi iptal edildi.",
                            point = feature.Point
                        };
                    }
                    else if (input.ScoreType == "dislike")
                    {
                        feature.Point++;
                        userScore.ScoreType = "none";
                        await _userFeatureScoreRepository.UpdateAsync(userScore);
                        await _featureRepository.UpdateAsync(feature);
                        return new
                        {
                            message = "Dislike işlemi iptal edildi.",
                            point = feature.Point
                        };
                    }
                }
                else
                {
                    if (userScore.ScoreType == "like" && input.ScoreType == "dislike")
                    {
                        feature.Point--;
                    }
                    else if (userScore.ScoreType == "dislike" && input.ScoreType == "like")
                    {
                        feature.Point++;
                    }

                    userScore.ScoreType = input.ScoreType;
                    await _userFeatureScoreRepository.UpdateAsync(userScore);
                }
            }
            else
            {
                var newUserFeatureScore = new UserFeatureScore
                {
                    UserId = (Guid)_currentUser.Id,
                    FeatureId = input.FeatureId,
                    ScoreType = input.ScoreType
                };
                await _userFeatureScoreRepository.InsertAsync(newUserFeatureScore);
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


        public async Task<UserFeatureScoreDto> GetUserFeatureScoreAsync(Guid featureId)
        {
            var userFeatureScore = await _userFeatureScoreRepository
                .FirstOrDefaultAsync(u => u.UserId == _currentUser.Id && u.FeatureId == featureId);

            if (userFeatureScore == null)
            {
                return new UserFeatureScoreDto { ScoreType = "none" };
            }

            return new UserFeatureScoreDto
            {
                ScoreType = userFeatureScore.ScoreType
            };
        }
    }
}
