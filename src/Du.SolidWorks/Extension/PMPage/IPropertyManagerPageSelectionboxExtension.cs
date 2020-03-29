using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class IPropertyManagerPageSelectionboxExtension
    {
        /// <summary>
        /// 设置选择过滤器,控件默认高度为 20*过滤类型个数
        /// </summary>
        /// <param name="seleBox"></param>
        /// <param name="filters"></param>
        public static void SetSelectionbox(this IPropertyManagerPageSelectionbox seleBox,IEnumerable<swSelectType_e> filters,int mark = 0,bool SingleEntity = true)
        {
            var array = filters.Cast<int>().ToArray();

            seleBox.Height = (short)(15 * array.Length);
            seleBox.Mark = mark;
            seleBox.SingleEntityOnly = SingleEntity;

            seleBox.SetSelectionFilters(array);
        }
    }
}
