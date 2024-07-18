namespace AppForeach.Framework.Hosting.Web
{
    public static class FrameworkWebApplication
    {
        public static FrameworkWebApplicationBuilder CreateBuilder(string[] args)
        {
            return new FrameworkWebApplicationBuilder(args);
        }
    }
}
