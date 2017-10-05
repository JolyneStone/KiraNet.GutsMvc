using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;

namespace KiraNet.GutsMVC
{
    public class FeatureCollection : IFeatureCollection
    {
        public FeatureCollection() { }


        public FeatureCollection(IServiceCollection servers)
        {
            Services = servers;
        }
        private readonly ConcurrentDictionary<Type, object> _features = new ConcurrentDictionary<Type, object>();

        public IServiceCollection Services { get; }

        public TFeature Get<TFeature>() =>
            _features.TryGetValue(typeof(TFeature), out var feature) ?
            (TFeature)feature :
            default(TFeature);

        public IFeatureCollection Set<TFeature>(TFeature instance)
        {
            _features.AddOrUpdate(
                typeof(TFeature),
                instance,
                (_, v) => v);
            return this;
        }

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
        {
            if(_features!=null)
            {
                foreach(var feature in _features)
                {
                    yield return feature;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
