using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// 使用一个代理的封装类来实现对接口的代理,这样可以解决Com-interop
    /// 要求一个实现类的需求.
    /// </summary>
    [ComVisible(true)]
    public class SwCalloutHandlerWrapper:ISwCalloutHandler
    {

        private readonly ISwCalloutHandler _Implementation;

        private readonly Func<ICallout, int, string, bool> _func;

        public SwCalloutHandlerWrapper(ISwCalloutHandler implementation)
        {
            _Implementation = implementation;
        }

        public SwCalloutHandlerWrapper(Func<ICallout, int, string, bool> func)
        {
            _func = func;
        }

        public bool OnStringValueChanged(object pManipulator, int RowID, string Text)
        {
            if (_Implementation != null)
            {
                return _Implementation.OnStringValueChanged(pManipulator, RowID, Text);
            }
            if (_func != null)
            {
                return _func((ICallout)pManipulator, RowID, Text);
            }
            return false;
        }
    }

    /// <summary>
    /// 通过func委托创建对Callout事件响应的封装类
    /// </summary>
    public class SwCalloutHandlerWrapperHelper
    {
        /// <summary>
        /// 私有静态变量防止被垃圾回收
        /// </summary>
        private static SwCalloutHandlerWrapper calloutHelper;
        /// <summary>
        /// 通过传入一个委托来创建一个对Callout的事件响应封装类--只能创建一个,在内存中只保存一个实例引用,多个会发生位置错误
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static SwCalloutHandlerWrapper CreateWrapper(Func<ICallout,int,string,bool> func)
        {
            return calloutHelper = new SwCalloutHandlerWrapper(func);
        }
    }
}
