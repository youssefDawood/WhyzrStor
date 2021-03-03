using WhyzrStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace WhyzrStore.Permissions
{
    public class WhyzrStorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(WhyzrStorePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(WhyzrStorePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<WhyzrStoreResource>(name);
        }
    }
}
