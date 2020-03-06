using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class PropertyManagerPage2Extension
    {
        /// <summary>
        /// 添加GroupBox
        /// </summary>
        /// <param name="page"></param>
        /// <param name="id">id</param>
        /// <param name="Caption">标题</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public static PropertyManagerPageGroup CreateGroupBoxEx(this PropertyManagerPage2 page,int id,string Caption,
            IEnumerable<swAddGroupBoxOptions_e> options = null)
        {
            options = options ??
                      new[]
                      {
                          swAddGroupBoxOptions_e.swGroupBoxOptions_Expanded,
                          swAddGroupBoxOptions_e.swGroupBoxOptions_Visible
                      };
            var optionsShort = (short)CombineToInt(options, v => (short)v);

            return page.AddGroupBox(id, Caption, optionsShort) as PropertyManagerPageGroup;
        }

        


        internal static int CombineOptions(IEnumerable<swAddControlOptions_e> options)
        {
            return CombineToInt(options, v => (int)v);
        }

        internal static int CombineToInt<T>(IEnumerable<T> leftAlign, Func<T, int> fn)
        {
            return leftAlign?.Aggregate(0, (acc, v) => acc | fn(v))
                ?? ((int)swAddControlOptions_e.swControlOptions_Enabled | (int)swAddControlOptions_e.swControlOptions_Visible);
        }
    }

    
}
