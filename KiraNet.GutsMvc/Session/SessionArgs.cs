using System;

namespace KiraNet.GutsMvc
{
    public class SessionArgs : EventArgs
    {
        public SessionArgs(Session e)
        {
            Value = e;
        }

        public SessionArgs(Session e, bool result)
        {
            Value = e;
            Result = result;
        }

        public Session Value { get; set; }
        public bool Result { get; set; }
    }
}
