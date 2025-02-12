
namespace AppForeach.Framework.Logging
{
    public struct FrameworkLogEventId
    {
        public FrameworkLogEventId(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}
