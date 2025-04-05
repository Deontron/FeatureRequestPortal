using FeatureRequestPortal.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace FeatureRequestPortal.Permissions;

public class FeatureRequestPortalPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var featureRequestPortalGroup = context.AddGroup(FeatureRequestPortalPermissions.GroupName, L("Permission:FeatureRequestPortal"));

        var myFeaturesPermission = featureRequestPortalGroup.AddPermission(FeatureRequestPortalPermissions.MyFeatures.Default, L("Permission:MyFeatures"));
        myFeaturesPermission.AddChild(FeatureRequestPortalPermissions.MyFeatures.Create, L("Permission:MyFeatures.Create"));
        myFeaturesPermission.AddChild(FeatureRequestPortalPermissions.MyFeatures.Edit, L("Permission:MyFeatures.Edit"));
        myFeaturesPermission.AddChild(FeatureRequestPortalPermissions.MyFeatures.Delete, L("Permission:MyFeatures.Delete"));
        myFeaturesPermission.AddChild(FeatureRequestPortalPermissions.MyFeatures.Approve, L("Permission:MyFeatures.Approve"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(FeatureRequestPortalPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FeatureRequestPortalResource>(name);
    }
}
