using System.Threading.Tasks;
using FeatureRequestPortal.MyFeatures;
using Microsoft.AspNetCore.Mvc;

namespace FeatureRequestPortal.Web.Pages.MyFeatures
{
    public class CreateModalModel : FeatureRequestPortalPageModel
    {
        [BindProperty]
        public CreateMyFeatureDto MyFeature { get; set; }

        private readonly IMyFeatureAppService _featureAppService;

        public CreateModalModel(IMyFeatureAppService featureAppService)
        {
            _featureAppService = featureAppService;
        }

        public void OnGet()
        {
            MyFeature = new CreateMyFeatureDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _featureAppService.CreateAsync(new UpdateMyFeatureDto
            {
                Title = MyFeature.Title,
                Category = MyFeature.Category,
                Description = MyFeature.Description,
            });
            return NoContent();
        }
    }
}
