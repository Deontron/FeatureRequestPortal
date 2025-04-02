using System.Threading.Tasks;
using FeatureRequestPortal.Localization;
using FeatureRequestPortal.Permissions;
using FeatureRequestPortal.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

namespace FeatureRequestPortal.Web.Menus;

public class FeatureRequestPortalMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<FeatureRequestPortalResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                FeatureRequestPortalMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "FeatureRequestPortal",
                l["Menu:FeatureRequestPortal"],
                icon: "fa fa-book"
                ).AddItem(
                    new ApplicationMenuItem(
                    "FeatureRequestPortal.MyFeatures",
                    l["Menu:MyFeatures"],
                    url: "/MyFeatures"
                )
            ).AddItem(
                    new ApplicationMenuItem(
                    "FeatureRequestPortal.FeatureManagement",
                    l["Menu:MyFeaturesManaging"],
                    url: "/FeatureManagement"
                ))
        );


        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);

        return Task.CompletedTask;
    }
}
