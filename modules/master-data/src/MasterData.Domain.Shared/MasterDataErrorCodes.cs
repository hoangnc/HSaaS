namespace MasterData
{
    public static class MasterDataErrorCodes
    {
        //Add your business exception error codes here...

        public static class DocumentType
        {
            public const string CodeExists = "Dt:010001";
        }

        public static class Module
        {
            public const string CodeExists = "Dt:020001";
        }

        public static class UserDepartment
        {
            public const string UserNameExists = "Ud:030001";
        }
    }
}
