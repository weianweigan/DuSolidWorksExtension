using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// Extension Methods for <see cref="IModelViewManager"/>
    /// </summary>
    public static class IModelViewManagerExtension
    {

        /// <summary>
        /// 创建箭头操纵器
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="handler"></param>
        /// <param name="manipulator"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool Create(IModelDoc2 doc, ref ArrowManipulatorHandler handler,out Manipulator manipulator, Action<DragArrowManipulator> setting = null)
        {
            var swModelViewMgr = doc.ModelViewManager;
            manipulator = swModelViewMgr.CreateManipulator(swManipulatorType_e.swDragArrowManipulator.SWToInt(), handler);
            var DragArrow = manipulator?.GetSpecificManipulator() as DragArrowManipulator;

            if (DragArrow == null)
            {
                return false;
            }

            if (setting != null)
            {
                setting.Invoke(DragArrow);
            }
            else
            {
                DragArrow.AllowFlip = false;
                DragArrow.ShowRuler = true;
                DragArrow.ShowOppositeDirection = true;
                DragArrow.Length = 0.02;
                DragArrow.LengthOppositeDirection = 0.01;
            }

            return true;
        }
    }
}
