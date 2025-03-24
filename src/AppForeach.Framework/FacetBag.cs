using System;
using System.Collections.Generic;

namespace AppForeach.Framework
{
    public class FacetBag
    {
        private readonly Dictionary<Type, object> store;

        public FacetBag()
        {
            store = new Dictionary<Type, object>();
        }

        public FacetBag(FacetBag parent)
        {
            store = new Dictionary<Type, object>(parent.store);
        }

        public TFacet Set<TFacet>(TFacet facet)
            where TFacet : class, new()
        {
            store[typeof(TFacet)] = facet;
            return facet;
        }

        public TFacet TryGet<TFacet>()
        {
            if(store.ContainsKey(typeof(TFacet)))
            {
                return (TFacet)store[typeof(TFacet)];
            }
            else
            {
                return default(TFacet);
            }
        }

        public FacetBag Combine(FacetBag bag)
        {
            FacetBag combined = new FacetBag(this);

            foreach (var kvp in bag.store)
            {
                combined.store[kvp.Key] = kvp.Value;
            }

            return combined;
        }
    }
}
