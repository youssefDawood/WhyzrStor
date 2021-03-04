namespace WhyzrStore.Permissions
{
    public static class WhyzrStorePermissions
    {
        public const string GroupName = "WhyzrStore";
        public static class Branches
        {
            public const string Defult = GroupName + ".Branches";
            public const string Create = Defult + ".Create";
            public const string Edit   = Defult + ".Edit";
            public const string Delete = Defult + ".Delete";
        }
        public static class Warehouses
        {
            public const string Defult = GroupName + ".Warehouses";
            public const string Create = Defult + ".Create";
            public const string Edit = Defult + ".Edit";
            public const string Delete = Defult + ".Delete";
        }
    }
}