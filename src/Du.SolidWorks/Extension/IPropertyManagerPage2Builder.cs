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
        /// 使用反射添加
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete]
        public IPropertyManagerPage2Builder AddSelectedObject(ref Object obj)
        {
            //使用反射获取属性
            var ObjType = typeof(TestObj);
            var propertys = ObjType.GetProperties();

            //安装catery属性分组
            var groups = propertys.GroupBy(p => p.CustomAttributes?.
            Where(attr => attr.AttributeType == typeof(CategoryAttribute))
            .Select(attr => attr.AttributeType.GetProperty("Category").GetValue(p)));

            //添加组
            foreach (var item in groups)
            {
                
            }
            return null;
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

    public class TestObj
    {
        
        public string Name { get; set; }

        [SelectionBox(40,swSelectType_e.swSelFACES)]
        public List<ObjectSelection> Selections { get; set; }
    }
}
