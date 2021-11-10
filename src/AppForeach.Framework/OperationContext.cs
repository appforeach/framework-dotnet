﻿
namespace AppForeach.Framework
{
    public class OperationContext : IOperationContext
    {
        public OperationContext()
        {
            State = new Bag();
        }

        public string OperationName { get; set; }

        public bool IsCommand { get; set; }

        public object Input { get; set; }

        public IBag Configuration { get; set; }

        public IBag State { get; set; }
    }
}
