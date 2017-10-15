namespace KiraNet.GutsMvc.Filter
{
    /// <summary>
    /// 表示过滤器的应用范围
    /// </summary>
    public enum FilterScope
    {
        Action = 30,
        Controller = 20,
        First = 0,
        Global = 10,
        Last = 100,
    }
}