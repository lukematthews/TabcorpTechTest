namespace TabcorpTechTest.Constants
{
    public class SecurityRoles
    {
        public const String Admin = "Admin";
        public const String User = "User";
        public const String Reports = "Reports";

        public static string Format(string[] roles) => String.Join(",", roles);
    }
}
