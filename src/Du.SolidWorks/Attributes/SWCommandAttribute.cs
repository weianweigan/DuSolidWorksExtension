using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Attributes
{

    public class SWCommandAttribute:Attribute
    {
        /// <summary>
        /// 命令名
        /// </summary>
        public  string Name { get; set; }


        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        

        /// <summary>
        /// 位置 默认为-1 往后排列
        /// </summary>
        public int Postion { get; set; } = -1;

        public string Hint { get; set; }

        /// <summary>
        /// ToolTip
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// 菜单选项,默认为 swMenuItem | swToolbarItem
        /// </summary>
        /// <remarks>
        /// swCommandItemType_e.swMenuItem | swCommandItemType_e.swToolbarItem;
        /// </remarks>
        public swCommandItemType_e MenuTBOption { get; set; } = swCommandItemType_e.swMenuItem | swCommandItemType_e.swToolbarItem;

        public int ImageListIndex { get; set; } = -1;

        /// <summary>
        /// 显示布局 默认为 TextHorizontal
        /// </summary>
        /// <remarks>
        /// swCommandTabButtonTextDisplay_e.swCommandTabButton_TextHorizontal;
        /// </remarks>
        public swCommandTabButtonTextDisplay_e TextType { get; set; } = swCommandTabButtonTextDisplay_e.swCommandTabButton_TextHorizontal;
    
        /// <summary>
        /// 使用最多的默认值,Name,Hint,ToolTip都为name,居中布局,菜单和ToolBar都显示
        /// </summary>
        /// <param name="name"></param>
        /// <param name="imageList"></param>
        public SWCommandAttribute(string name,int imageList)
        {
            Name = name;
            Hint = name;
            ToolTip = name;
            ImageListIndex = imageList;
        }
    }
}
