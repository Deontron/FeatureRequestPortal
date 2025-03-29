using System;
using System.Threading.Tasks;
using FeatureRequestPortal.MyFeatures;
using Microsoft.AspNetCore.Mvc;

namespace FeatureRequestPortal.Web.Pages.MyFeatures;

public class EditModalModel : FeatureRequestPortalPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateUpdateMyFeatureDto MyFeature { get; set; }

    private readonly IMyFeatureAppService _myFeatureAppService;

    public EditModalModel(IMyFeatureAppService myFeatureAppService)
    {
        _myFeatureAppService = myFeatureAppService;
    }

    public async Task OnGetAsync()
    {
        var myFeatureDto = await _myFeatureAppService.GetAsync(Id);
        MyFeature = ObjectMapper.Map<MyFeatureDto, CreateUpdateMyFeatureDto>(myFeatureDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _myFeatureAppService.UpdateAsync(Id, MyFeature);
        return NoContent();
    }
}
