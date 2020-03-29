using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class SWGroupCommandAttribute:Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cmdTitle"></param>
        /// <param name="commands"></param>
        public SWGroupCommandAttribute(string cmdTitle, params Type[] commands)
        {
            Title = cmdTitle;
            ToolTip = cmdTitle;
            Hint = cmdTitle;

            if (commands == null || commands.Length == 0)
            {
                throw new NullReferenceException($"{nameof(SWFlyoutCommandAttribute)}'s Flyout Group Commands Cannot be Null Or No Commands");
            }

            Commands = commands;
        }

        /// <summary>
        /// guid
        /// </summary>
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();

        /// <summary>
        /// title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// tooltip
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// hint
        /// </summary>
        public string Hint { get; set; }

        /// <summary>
        /// commands
        /// </summary>
        public Type[] Commands { get; set; }

        /// <summary>
        /// texttype
        /// </summary>
        public int TextType { get; set; } = (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow;
    }
}
