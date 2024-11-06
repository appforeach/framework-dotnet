namespace AppForeach.Framework.Hosting
{
    public static class FrameworkApplication
    {
        public static FrameworkApplicationBuilder CreateBuilder(string[] args)
        {
            return new FrameworkApplicationBuilder(args);
        }
    }
}
