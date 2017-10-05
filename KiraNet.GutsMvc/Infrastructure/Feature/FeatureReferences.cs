using System;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 对Feature对象的一个引用，用来保存对应的Feature实例
    /// </summary>
    /// <typeparam name="TCache"></typeparam>
    //public struct FeatureReferences<TCache>
    //{
    //    public FeatureReferences(IFeatureCollection collection)
    //    {
    //        Collection = collection;
    //        Cache = default(TCache);
    //    }

    //    public IFeatureCollection Collection { get; private set; }

    //    /// <summary>
    //    /// Cache是一个公用字段
    //    /// 因为当调用Fetch时，可以防止使用属性的引用传递
    //    /// </summary>
    //    public TCache Cache;

    //    /// <summary>
    //    /// 获取feature
    //    /// </summary>
    //    /// <typeparam name="TFeature"></typeparam>
    //    /// <typeparam name="TState"></typeparam>
    //    /// <param name="cached"></param>
    //    /// <param name="state"></param>
    //    /// <param name="factory"></param>
    //    /// <returns></returns>
    //    public TFeature Fetch<TFeature, TState>(
    //        ref TFeature cached,
    //        TState state,
    //        Func<TState, TFeature> factory) where TFeature : class
    //    {
    //        var flush = false;

    //        return cached ?? UpdateCached(ref cached, state, factory, flush);
    //    }

    //    /// <summary>
    //    /// 更新和清理缓存
    //    /// </summary>
    //    /// <typeparam name="TFeature"></typeparam>
    //    /// <typeparam name="TState"></typeparam>
    //    /// <param name="cached"></param>
    //    /// <param name="state"></param>
    //    /// <param name="factory"></param>
    //    /// <param name="flush"></param>
    //    /// <returns></returns>
    //    private TFeature UpdateCached<TFeature, TState>(ref TFeature cached, TState state, Func<TState, TFeature> factory, bool flush) where TFeature : class
    //    {
    //        if (flush)
    //        {
    //            // Collection已经被清除，Cache也应该被清除
    //            Cache = default(TCache);
    //        }

    //        cached = Collection.Get<TFeature>();
    //        if (cached == null)
    //        {
    //            // 用工厂委托来创建Cache
    //            cached = factory(state);
    //            // 添加到Collection中
    //            Collection.Set(cached);
    //        }

    //        return cached;
    //    }

    //    public TFeature Fetch<TFeature>(ref TFeature cached, Func<IFeatureCollection, TFeature> factory)
    //        where TFeature : class 
    //        => Fetch(ref cached, Collection, factory);
    //}
}
