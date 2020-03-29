using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Attributes
{
    /// <summary>
    /// Attribute
    /// </summary>
    public class SWFlyoutCommandAttribute:Attribute
    {
        /// <summary>
        /// Guid
        /// </summary>
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();

        /// <summary>
        /// Title
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
        /// Commands
        /// </summary>
        public Type[] Commands { get; set; }

        /// <summary>
        /// <see cref="swCommandFlyoutStyle_e"/>  <see cref="swCommandTabButtonTextDisplay_e"/>
        /// </summary>
        public int TextType { get; set; } = (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow | (int)swCommandTabButtonFlyoutStyle_e.swCommandTabButton_ActionFlyout;

        /// <summary>
        /// <see cref="swCommandFlyoutStyle_e"/>
        /// </summary>
        public swCommandFlyoutStyle_e FlyoutType { get; set; } = swCommandFlyoutStyle_e.swCommandFlyoutStyle_Favorite;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cmdTitle"></param>
        /// <param name="commands"></param>
        public SWFlyoutCommandAttribute(string cmdTitle,params Type[] commands)
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
    }
}
