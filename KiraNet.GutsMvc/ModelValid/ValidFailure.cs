using System;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.ModelValid
{
    public class ValidFailure
    {
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorMessageResourceName { get; set; }
        public Type ErrorMessageResourceType { get; set; }
        public string ErrorMessageString { get; set; }
        public IEnumerable<string> MemberNames { get; set; }
        public string Name { get; set; }
    }
}
