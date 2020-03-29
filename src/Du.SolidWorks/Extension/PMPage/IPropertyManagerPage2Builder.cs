using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public class IPropertyManagerPage2Builder
    {
        public IPropertyManagerPage2 Page;

        public IPropertyManagerPage2Builder(IPropertyManagerPage2 page)
        {
            Page = page;
        }

        public  IPropertyManagerPageGroup CreateGroupBoxEx( int id, string Caption,
            IEnumerable<swAddGroupBoxOptions_e> options = null)
        {
            options = options ??
                      new[]
                      {
                          swAddGroupBoxOptions_e.swGroupBoxOptions_Expanded,
                          swAddGroupBoxOptions_e.swGroupBoxOptions_Visible
                      };
            var optionsShort = (short)PropertyManagerPage2Extension.CombineToInt(options, v => (short)v);

            return Page.AddGroupBox(id, Caption, optionsShort) as PropertyManagerPageGroup;
        }

        /// <summary>
        /// 添加WPF控件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <param name="uiElementConfig"></param>
        /// <returns></returns>

        public IPropertyManagerPageWindowFromHandle AddWPFUserControl(int id,
            string caption,
            string tip,
            swPropertyManagerPageControlLeftAlign_e leftAlign,
            IEnumerable<swAddControlOptions_e> options,
            Action<IPropertyManagerPageWindowFromHandleWrapper> uiElementConfig)
        {
            short controlType = (int)swPropertyManagerPageControlType_e.swControlType_WindowFromHandle;

            var WpfPMPWindow = (IPropertyManagerPageWindowFromHandle)Page.AddControl2(id,
                controlType, caption,
                (short)leftAlign, PropertyManagerPage2Extension.CombineOptions(options), tip);

            if (uiElementConfig != null && WpfPMPWindow != null)
            {
                uiElementConfig(new IPropertyManagerPageWindowFromHandleWrapper(WpfPMPWindow));
            }
            return WpfPMPWindow;
        }
    }
}
