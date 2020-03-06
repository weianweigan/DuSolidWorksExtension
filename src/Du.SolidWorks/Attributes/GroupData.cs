using System;
using System.Reflection;

namespace Du.SolidWorks
{
    public struct GroupData
    {

        public string Title { get; set; }
        public string ToolTip { get; set; }
        public string Hint { get; set; }
        public Type[] TabStyles { get; set; }
        public string[] Icons { get; set; }
        public Assembly IconAssembly { get; set; }
    }

    public enum CommandType
    {
        Command,
        FlyoutCommand
    }
}