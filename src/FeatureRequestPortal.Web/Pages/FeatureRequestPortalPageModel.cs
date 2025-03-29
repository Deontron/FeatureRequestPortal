using FeatureRequestPortal.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace FeatureRequestPortal.Web.Pages;

public abstract class FeatureRequestPortalPageModel : AbpPageModel
{
    protected FeatureRequestPortalPageModel()
    {
        LocalizationResourceType = typeof(FeatureRequestPortalResource);
    }
}
