using WhyzrStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace WhyzrStore.Permissions
{
    public class WhyzrStorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var whyzrStoreGroup = context.AddGroup(WhyzrStorePermissions.GroupName);

            var branchesPermission = whyzrStoreGroup
                .AddPermission(WhyzrStorePermissions.Branches.Defult,
                L("Permission:Branch"));
            branchesPermission.AddChild(WhyzrStorePermissions.Branches.Create, L("Permission:Branches.Create"));
            branchesPermission.AddChild(WhyzrStorePermissions.Branches.Edit, L("Permission:Branches.Edit"));
            branchesPermission.AddChild(WhyzrStorePermissions.Branches.Delete, L("Permission:Branches.Delete"));
            var warehousesPermission = whyzrStoreGroup
                         .AddPermission(WhyzrStorePermissions.Warehouses.Defult,
                         L("Permission:Warehouses"));
            warehousesPermission.AddChild(WhyzrStorePermissions.Warehouses.Create, L("Permission:Warehouses.Create"));
            warehousesPermission.AddChild(WhyzrStorePermissions.Warehouses.Edit, L("Permission:Warehouses.Edit"));
            warehousesPermission.AddChild(WhyzrStorePermissions.Warehouses.Delete, L("Permission:Warehouses.Delete"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<WhyzrStoreResource>(name);
        }
    }
}
