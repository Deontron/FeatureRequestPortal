using FeatureRequestPortal.MyFeatures;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace FeatureRequestPortal
{
    public class FeatureRequestPortalDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<MyFeature, Guid> _myFeatureRepository;

        public FeatureRequestPortalDataSeederContributor(IRepository<MyFeature, Guid> myFeatureRepository)
        {
            _myFeatureRepository = myFeatureRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _myFeatureRepository.GetCountAsync() <= 0)
            {
                await _myFeatureRepository.InsertAsync(
                    new MyFeature
                    {
                        Title = "1984",
                        Category = MyFeatureCategory.Update,
                        Description = "19.84f",
                        IsApproved = false
                    },
                    autoSave: true
                );

                await _myFeatureRepository.InsertAsync(
                    new MyFeature
                    {
                        Title = "2002",
                        Category = MyFeatureCategory.New,
                        Description = "19.84f",
                        IsApproved = true
                    },
                    autoSave: true
                );
            }
        }
    }
}
