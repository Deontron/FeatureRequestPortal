namespace FeatureRequestPortal.Permissions;

public static class FeatureRequestPortalPermissions
{
    public const string GroupName = "FeatureRequestPortal";



    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class MyFeatures
    {
        public const string Default = GroupName + ".MyFeatures";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
