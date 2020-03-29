using System;
using System.Reflection;
using SolidWorks.Interop.swconst;
using Du.SolidWorks.Extension;

namespace Du.SolidWorks
{
    /// <summary>
    /// 组数据
    /// </summary>
    public struct GroupData
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Tip
        /// </summary>
        public string ToolTip { get; set; }
        /// <summary>
        /// Hint
        /// </summary>
        public string Hint { get; set; }
        
        /// <summary>
        /// style
        /// </summary>
        public Type[] TabStyles { get; set; }
        
        /// <summary>
        /// Icons
        /// </summary>
        public string[] Icons { get; set; }
        
        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly IconAssembly { get; set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public int ShowInDocumentType { get; set; } 
    }

    /// <summary>
    /// 按钮类型
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// 按钮
        /// </summary>
        Command,
        /// <summary>
        /// 下拉按钮
        /// </summary>
        FlyoutCommand,

        /// <summary>
        /// 组按钮
        /// </summary>
        GroupCommand
    }
}