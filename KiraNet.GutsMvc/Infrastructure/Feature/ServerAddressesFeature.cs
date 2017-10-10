using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KiraNet.GutsMvc
{
    public class ServerAddressesFeature:IServerAddressesFeature
    {
        public ICollection<string> Addresses { get; } = new Collection<string>();
    }
}
