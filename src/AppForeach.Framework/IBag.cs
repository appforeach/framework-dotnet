namespace AppForeach.Framework
{
    public interface IBag
    {
        T Get<T>() where T : class, new();
    }
}
