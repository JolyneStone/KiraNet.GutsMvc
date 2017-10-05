using System.Collections.Generic;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 表示服务器监听地址
    /// </summary>
    public interface IServerAddressesFeature
    {
        ICollection<string> Addresses { get; }
    }
}
