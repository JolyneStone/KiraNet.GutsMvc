using System;
using System.IO;
using System.Text;

namespace KiraNet.GutsMvc.View
{
    public class HelperResult
    {
        public HelperResult(Action<StringBuilder> action)
        {
            WriteAction = action;
        }

        public Action<StringBuilder> WriteAction { get; }

        public void WriteTo(StringBuilder writer)
        {
            WriteAction(writer);
        }
        //public HelperResult(Action<TextWriter> action)
        //{
        //    WriteAction = action;
        //}

        //public Action<TextWriter> WriteAction { get; }

        //public void WriteTo(TextWriter writer)
        //{
        //    WriteAction(writer);
        //}
    }
}