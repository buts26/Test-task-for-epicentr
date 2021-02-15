namespace ProjectManagement.API.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Version = "v1";

        public const string Base = "api/" + Version;

        public static class User
        {
            public const string Get = Base + "/user/get";
            public const string Create = Base + "/user/create";
            public const string Update = Base + "/user/update";
            public const string Delete = Base + "/user/delete";
        }

        public static class Project
        {
            public const string Get = Base + "/project/get";
            public const string Create = Base + "/project/create";
            public const string Update = Base + "/project/update";
            public const string Delete = Base + "/project/delete";
        }
    }
}
