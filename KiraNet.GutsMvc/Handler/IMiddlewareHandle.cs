﻿namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 中间件处理接口
    /// </summary>
    public interface IMiddlewareHandle
    {
        void MiddlewareExecute(HttpContext httpContext);
    }
}
