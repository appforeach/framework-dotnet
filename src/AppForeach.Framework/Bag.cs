using System;
using System.Collections.Generic;

namespace AppForeach.Framework
{
    public class Bag
    {
        private readonly Dictionary<Type, object> store;

        public Bag()
        {
            store = new Dictionary<Type, object>();
        }

        public T Get<T>() where T : class, new()
        {
            T item;

            if (!store.ContainsKey(typeof(T)))
            {
                item = new T();
                store.Add(typeof(T), item);
            }
            else
            {
                item = (T)store[typeof(T)];
            }

            return item;
        }

        public void Set<T>(T item) where T : class, new()
        {
            store[typeof(T)] = item;
        }
    }
}
