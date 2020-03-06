using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class PropertyManagerPageGroupExtension
    {
        /// <summary>
        /// 添加label控件
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="Caption"></param>
        /// <param name="tip"></param>
        /// <param name="align"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private static PropertyManagerPageGroup AddLabel(this PropertyManagerPageGroup group,int id,string Caption,string tip,
            swPropertyManagerPageControlLeftAlign_e align = swPropertyManagerPageControlLeftAlign_e.swControlAlign_LeftEdge, 
            swAddControlOptions_e options = swAddControlOptions_e.swControlOptions_Enabled |
                      swAddControlOptions_e.swControlOptions_Visible)
        {
            short controlType = (short)swPropertyManagerPageControlType_e.swControlType_Label;

            group.AddControl2(id, controlType, Caption, (short)align, (int)options, tip);

            return group;
        }

        /// <summary>
        /// 添加数字框
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="Caption"></param>
        /// <param name="tip"></param>
        /// <param name="align"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private static PropertyManagerPageGroup AddNumberBox(this PropertyManagerPageGroup group, int id, string Caption, string tip,
            swPropertyManagerPageControlLeftAlign_e align = swPropertyManagerPageControlLeftAlign_e.swControlAlign_LeftEdge,
            swAddControlOptions_e options = swAddControlOptions_e.swControlOptions_Enabled |
                      swAddControlOptions_e.swControlOptions_Visible)
        {
            short controlType = (short)swPropertyManagerPageControlType_e.swControlType_Numberbox;

            group.AddControl2(id, controlType, Caption, (short)align, (int)options, tip);

            return group;
        }


        public static PropertyManagerPageGroup AddControlEx<T>(this PropertyManagerPageGroup group,ref PMPageControl options)
        {
            if (options == null)
            {
                throw new NullReferenceException("参数为空");
            }

            options.PMPageObject = (T)group.AddControl2(options.ID,(short)options.ControlType, options.Caption, (short)options.Align, (int)options.Options, options.tip);

            return group;
        }

        /// <summary>
        ///  创建文本框
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageTextbox
            CreateTextBox(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string tip,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null)
        {
            return AddControl<IPropertyManagerPageTextbox>(@group, id, caption, tip, leftAlign, options);
        }

        /// <summary>
        /// 创建复选框
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageCheckbox
            CreateCheckBox(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string tip,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null
            )
        {
            return AddControl<IPropertyManagerPageCheckbox>(@group, id, caption, tip, leftAlign, options);
        }

        /// <summary>
        /// 创建按钮
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageButton
            CreateButton(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string tip,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null
            )
        {
            return AddControl<IPropertyManagerPageButton>(@group, id, caption, tip, leftAlign, options);
        }

        /// <summary>
        /// 创建选择框
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageSelectionbox
            CreateSelectionBox(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string tip,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null
            )
        {
            return AddControl<IPropertyManagerPageSelectionbox>(@group, id, caption, tip, leftAlign, options);
        }

        /// <summary>
        /// 创建文本
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageLabel
            CreateLabel(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string tip,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null
            )
        {
            return AddControl<IPropertyManagerPageLabel>(@group, id, caption, tip, leftAlign, options);
        }

        /// <summary>
        /// 创建复选框
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageCombobox
            CreateComboBox(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string tip,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null
            )
        {
            return AddControl<IPropertyManagerPageCombobox>(@group, id, caption, tip, leftAlign, options);
        }

        /// <summary>
        /// 创建列表
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageListbox
            CreateListBox(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string tip,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null
            )
        {
            return AddControl<IPropertyManagerPageListbox>(@group, id, caption, tip, leftAlign, options);
        }

        /// <summary>
        /// 创建数字框
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageNumberbox
            CreateNumberBox(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string tip,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null
            )
        {
            return AddControl<IPropertyManagerPageNumberbox>(@group, id, caption, tip, leftAlign, options);
        }

        /// <summary>
        /// 创建单选框
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="optionGroup"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageOption
            CreateOption(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string optionGroup,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null
            )
        {
            return AddControl<IPropertyManagerPageOption>(@group, id, caption, optionGroup, leftAlign, options);
        }

        /// <summary>
        /// 创建滑动条
        /// </summary>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="optionGroup"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPropertyManagerPageSlider
            CreateSlider(this IPropertyManagerPageGroup group,
                int id,
                string caption,
                string optionGroup,
                swPropertyManagerPageControlLeftAlign_e leftAlign = 0,
                IEnumerable<swAddControlOptions_e> options = null
            )
        {
            return AddControl<IPropertyManagerPageSlider>(@group, id, caption, optionGroup, leftAlign, options);
        }

        /// <summary>
        ///  泛型添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <param name="tip"></param>
        /// <param name="leftAlign"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static T AddControl<T>(this IPropertyManagerPageGroup @group,
            int id,
            string caption,
            string tip,
            swPropertyManagerPageControlLeftAlign_e leftAlign,
            IEnumerable<swAddControlOptions_e> options)
        {
            swPropertyManagerPageControlType_e typeE;
            switch (typeof(T).Name)
            {
                case nameof(IPropertyManagerPageTextbox):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Textbox;
                    break;
                case nameof(IPropertyManagerPageCheckbox):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Checkbox;
                    break;
                case nameof(IPropertyManagerPageButton):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Button;
                    break;
                case nameof(IPropertyManagerPageSelectionbox):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Selectionbox;
                    break;
                case nameof(IPropertyManagerPageCombobox):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Combobox;
                    break;
                case nameof(IPropertyManagerPageListbox):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Listbox;
                    break;
                case nameof(IPropertyManagerPageNumberbox):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Numberbox;
                    break;
                case nameof(IPropertyManagerPageOption):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Option;
                    break;
                case nameof(IPropertyManagerPageLabel):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Label;
                    break;
                case nameof(IPropertyManagerPageSlider):
                    typeE = swPropertyManagerPageControlType_e.swControlType_Slider;
                    break;
                case nameof(IPropertyManagerPageBitmapButton):
                    typeE = swPropertyManagerPageControlType_e.swControlType_BitmapButton;
                    break;
                case nameof(IPropertyManagerPageWindowFromHandle):
                    throw new ArgumentException($"请使用AddWPFUserControl添加用户控件{typeof(T).Name}");
                default:
                    throw new ArgumentException($"无法处理此种{typeof(T).Name}控件，请加成是否为接口");
            }
            return (T)@group
                .AddControl2(id,
                    (short)typeE,
                    caption,
                    (short)leftAlign,
                    PropertyManagerPage2Extension.CombineOptions(options),
                    tip);
        }

        public static IPropertyManagerPageWindowFromHandle AddWPFUserControl(this IPropertyManagerPageGroup @group,
            int id,
            string caption,
            string tip,
            swPropertyManagerPageControlLeftAlign_e leftAlign,
            IEnumerable<swAddControlOptions_e> options,
            Action<IPropertyManagerPageWindowFromHandleWrapper> uiElementConfig)
        {
            short controlType = (int)swPropertyManagerPageControlType_e.swControlType_WindowFromHandle;

            var WpfPMPWindow = (IPropertyManagerPageWindowFromHandle)@group.AddControl2(id, 
                controlType, caption, 
                (short)leftAlign, PropertyManagerPage2Extension.CombineOptions(options), tip);

            if (uiElementConfig != null && WpfPMPWindow != null)
            {
                uiElementConfig(new IPropertyManagerPageWindowFromHandleWrapper(WpfPMPWindow));
            }
            return WpfPMPWindow;
        }

    }
    public class IPropertyManagerPageWindowFromHandleWrapper: IPropertyManagerPageWindowFromHandle
    {
        private readonly IPropertyManagerPageWindowFromHandle handler;
        public IPropertyManagerPageWindowFromHandleWrapper(IPropertyManagerPageWindowFromHandle winformhandler)
        {
            handler = winformhandler;
        }
        public bool SetWindowHandle(int WindowHandle)
        {
            return handler.SetWindowHandle(WindowHandle);
        }

        public bool SetWindowHandlex64(long WindowHandle)
        {
           return  handler.SetWindowHandlex64(WindowHandle);
        }

        public int Height {
            get
            {
                return handler.Height;
            }
            set
            {
                handler.Height = value;
            }
        }
    }
    public class PMPageControl
    {
        private static int Pid = 133;
        public PMPageControl(int iD, string caption, string hint,
            swPropertyManagerPageControlType_e controlType, 
            swPropertyManagerPageControlLeftAlign_e align, 
            swAddControlOptions_e options)
        {
            ControlType = controlType;
            Align = align;
            Options = options;
            ID = iD;
            Caption = caption;
            tip = hint;
        }

        public PMPageControl(swPropertyManagerPageControlType_e controlType)
        {
            ControlType = controlType;
            Align = swPropertyManagerPageControlLeftAlign_e.swControlAlign_LeftEdge;
            Options = swAddControlOptions_e.swControlOptions_Enabled |
                          swAddControlOptions_e.swControlOptions_Visible;
            ID = Pid;
            Pid++;
            Caption = "";
            tip = "";
        }

        public int ID { get; set; }

        public string Caption { get; set; }

        public string tip { get; set; }

        public swPropertyManagerPageControlType_e ControlType { get; set; }

        public swPropertyManagerPageControlLeftAlign_e Align { get; set; }

        public swAddControlOptions_e Options { get; set; }
        
        public dynamic PMPageObject { get; set; }
    }
}
