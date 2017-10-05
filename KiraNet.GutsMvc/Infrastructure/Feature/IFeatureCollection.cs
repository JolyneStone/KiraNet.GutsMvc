using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 用于描述一组特定对象的特性
    /// 注：该接口的实现类可看作是一个Dictionary<Type, object>，Key表示特定对象，Value表示该对象的实现类
    /// </summary>
    public interface IFeatureCollection:IEnumerable<KeyValuePair<Type,object>>
    {
        TFeature Get<TFeature>();
        IFeatureCollection Set<TFeature>(TFeature instance);
        IServiceCollection Services { get; } 
    }
}