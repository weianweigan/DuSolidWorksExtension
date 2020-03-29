using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
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
    /// Handler for ArrowManipluator
    /// </summary>
    [ComVisible(true)]
    public class ArrowManipulatorHandler : SwManipulatorHandler2
    {
        private int doneonce;

        const int lenFact = 1;


        #region Event

        /// <summary>
        /// 反转方向
        /// </summary>
        public event Action<DragArrowManipulator> DirectionFlipped;

        /// <summary>
        /// 鼠标左击
        /// </summary>
        public event Func<DragArrowManipulator, bool> HandleLmbSelected;

        /// <summary>
        /// 鼠标右击
        /// </summary>
        public event Action<DragArrowManipulator, swDragArrowManipulatorOptions_e> HandleRmbSelected;

        /// <summary>
        /// 选中
        /// </summary>
        public event Action<DragArrowManipulator, swDragArrowManipulatorOptions_e> HandleSelected;

        /// <summary>
        /// 更新拖到
        /// </summary>
        public event Action<DragArrowManipulator, swDragArrowManipulatorOptions_e, IMathPoint> UpdateDrag;

        /// <summary>
        /// 停止拖动
        /// </summary>
        public event Action<DragArrowManipulator, swDragArrowManipulatorOptions_e> EndDrag;

        /// <summary>
        /// 停止并且无拖动
        /// </summary>
        public event Action<DragArrowManipulator, swDragArrowManipulatorOptions_e> EndNoDrag;

        /// <summary>
        /// 删除
        /// </summary>
        public event Func<DragArrowManipulator, bool> Delete;

        /// <summary>
        /// 焦点
        /// </summary>
        public event Action<DragArrowManipulator, swDragArrowManipulatorOptions_e> ItemSetFocus;

        /// <summary>
        /// Double值改变
        /// </summary>
        public event Func<DragArrowManipulator, swDragArrowManipulatorOptions_e, double, bool> DoubelValueChanged;

        /// <summary>
        /// 字符值改变
        /// </summary>
        public event Func<DragArrowManipulator, swDragArrowManipulatorOptions_e, string, bool> StringValueChanged;

        #endregion

        #region .ctor

        /// <summary>
        /// .ctor
        /// </summary>
        public ArrowManipulatorHandler()
        {
        }

        #endregion

        #region Handler

        /// <summary>
        /// 改变方向
        /// </summary>
        /// <param name="pManipulator"></param>
        public virtual void OnDirectionFlipped(object pManipulator)
        {
            DirectionFlipped?.Invoke(pManipulator as DragArrowManipulator);
        }

        public virtual bool OnHandleLmbSelected(object pManipulator)
        {
            return HandleLmbSelected?.Invoke(pManipulator as DragArrowManipulator) == false ? false : true;
        }

        public virtual void OnHandleSelected(object pManipulator, int handleIndex)
        {
            HandleSelected?.Invoke(pManipulator as DragArrowManipulator, handleIndex.CastObj<swDragArrowManipulatorOptions_e>());
        }

        public virtual void OnUpdateDrag(object pManipulator, int handleIndex, object newPosMathPt)
        {
            UpdateDrag(pManipulator as DragArrowManipulator, handleIndex.CastObj<swDragArrowManipulatorOptions_e>(), newPosMathPt as IMathPoint);
        }

        public virtual void OnEndDrag(object pManipulator, int handleIndex)
        {
            DragArrowManipulator swTmpManipulator = default(DragArrowManipulator);
            swTmpManipulator = (DragArrowManipulator)pManipulator;
            swTmpManipulator.FixedLength = false;
            doneonce = doneonce + 1;

            EndDrag?.Invoke(pManipulator as DragArrowManipulator, handleIndex.CastObj<swDragArrowManipulatorOptions_e>());
        }

        public void OnEndNoDrag(object pManipulator, int handleIndex)
        {
            EndNoDrag?.Invoke(pManipulator as DragArrowManipulator, handleIndex.CastObj<swDragArrowManipulatorOptions_e>());
        }

        public virtual void OnHandleRmbSelected(object pManipulator, int handleIndex)
        {
            HandleRmbSelected?.Invoke(pManipulator as DragArrowManipulator, handleIndex.CastObj<swDragArrowManipulatorOptions_e>());
        }

        public virtual bool OnDelete(object pManipulator)
        {
            return Delete?.Invoke(pManipulator as DragArrowManipulator) == false ? false : true;
        }

        public virtual bool OnDoubleValueChanged(object pManipulator, int handleIndex, ref double Value)
        {
            doneonce = doneonce + 1;
            
            DragArrowManipulator swTmpManipulator = default(DragArrowManipulator);
            swTmpManipulator = (DragArrowManipulator)pManipulator;
            //Update origin
            MathPoint swMathPoint = default(MathPoint);
            swMathPoint = swTmpManipulator.Origin;
            double[] varMathPt = null;
            varMathPt = (double[])swMathPoint.ArrayData;
            varMathPt[1] = varMathPt[1] + lenFact / 1000;
            swMathPoint.ArrayData = varMathPt;
            if ((doneonce == 1))
            {
                swTmpManipulator.FixedLength = true;
            }
            swTmpManipulator.Origin = swMathPoint;

            swTmpManipulator.Update();
            return true;

            return DoubelValueChanged?.Invoke(pManipulator as DragArrowManipulator, handleIndex.CastObj<swDragArrowManipulatorOptions_e>(), Value) == false ? false : true;
        }

        public virtual bool OnStringValueChanged(object pManipulator, int handleIndex, ref string Value)
        {
            return StringValueChanged?.Invoke(pManipulator as DragArrowManipulator, handleIndex.CastObj<swDragArrowManipulatorOptions_e>(), Value) == false ? false : true;
        }

        public virtual void OnItemSetFocus(object pManipulator, int handleIndex)
        {
            ItemSetFocus?.Invoke(pManipulator as DragArrowManipulator, handleIndex.CastObj<swDragArrowManipulatorOptions_e>());
        }

        #endregion
    }


}
