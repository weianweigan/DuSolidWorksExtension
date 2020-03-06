using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Attributes
{
    public class SWFlyoutCommandAttribute:Attribute
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string ToolTip { get; set; }

        public string Hint { get; set; }

        public Type[] Commands { get; set; }

        public int TextType { get; set; } = (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow | (int)swCommandTabButtonFlyoutStyle_e.swCommandTabButton_ActionFlyout;

        public swCommandFlyoutStyle_e FlyoutType { get; set; } = swCommandFlyoutStyle_e.swCommandFlyoutStyle_Favorite;


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
