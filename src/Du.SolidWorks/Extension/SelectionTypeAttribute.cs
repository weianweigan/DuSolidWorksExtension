using System;
using SolidWorks.Interop.swconst;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// 选择类型属性
    /// </summary>
    public class SelectionBoxAttribute : Attribute
    {
        public swSelectType_e[] Fileter { get; }

        public double Height { get; }
        public SelectionBoxAttribute(params swSelectType_e[] fileter)
        {
            Fileter = fileter;
        }

        public SelectionBoxAttribute(double height, params swSelectType_e[] fileter)
        {
            Height = height;
            Fileter = fileter;
        }
    }

    /// <summary>
    /// PMP控件属性
    /// </summary>
    public class PMPageControlAttribute: Attribute
    {
        public string Caption { get; }

        public string Tip { get; }


        public PMPageControlAttribute(string caption,string tip)
        {

        }
    }
}