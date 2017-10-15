using System.Collections.Generic;

namespace KiraNet.GutsMvc.ModelValid
{
    public interface IModelState
    {
        IEnumerable<ModelWrapper> ModelWrappers { get; set; }
        ModelValidDictionary ModelValidDictionary { get; set; }
        bool IsValid { get; }
    }
}
