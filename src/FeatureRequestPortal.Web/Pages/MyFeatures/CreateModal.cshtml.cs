using System.Threading.Tasks;
using FeatureRequestPortal.MyFeatures;
using Microsoft.AspNetCore.Mvc;

namespace FeatureRequestPortal.Web.Pages.MyFeatures
{
    public class CreateModalModel : FeatureRequestPortalPageModel
    {
        [BindProperty]
        public CreateUpdateMyFeatureDto MyFeature { get; set; }

        private readonly IMyFeatureAppService _featureAppService;

        public CreateModalModel(IMyFeatureAppService featureAppService)
        {
            _featureAppService = featureAppService;
        }

        public void OnGet()
        {
            MyFeature = new CreateUpdateMyFeatureDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _featureAppService.CreateAsync(MyFeature);
            return NoContent();
        }
    }
}
