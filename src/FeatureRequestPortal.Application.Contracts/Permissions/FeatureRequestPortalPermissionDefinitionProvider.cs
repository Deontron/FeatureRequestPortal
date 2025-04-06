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
        myFeaturesPermission.AddChild(FeatureRequestPortalPermissions.MyFeatures.Manage, L("Permission:MyFeatures.Manage"));

        var myCommentPermission = featureRequestPortalGroup.AddPermission(FeatureRequestPortalPermissions.MyComment.Default, L("Permission:MyComment"));
        myCommentPermission.AddChild(FeatureRequestPortalPermissions.MyComment.Create, L("Permission:MyComment.Create"));
        myCommentPermission.AddChild(FeatureRequestPortalPermissions.MyComment.Delete, L("Permission:MyComment.Delete"));
        myCommentPermission.AddChild(FeatureRequestPortalPermissions.MyComment.Edit, L("Permission:MyComment.Edit"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(FeatureRequestPortalPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FeatureRequestPortalResource>(name);
    }
}
