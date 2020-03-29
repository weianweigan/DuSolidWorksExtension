using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// Extension Methods for <see cref="IView"/> interface 
    /// </summary>
    public static class ViewExtension
    {
        /// <summary>
        /// 获取视图垂直向量在模型空间的表达
        /// </summary>
        /// <param name="view"></param>
        /// <param name="mathUtility"></param>
        /// <returns></returns>
        public static MathVector GetZAxis(this View view,MathUtility mathUtility)
        {
           return Vector3.UnitZ.ToSwMathPoint(mathUtility).MultiplyTransform(view.ModelToViewTransform.Inverse()) as MathVector;
        }

        /// <summary>
        /// 获取视图X在模型空间的表达
        /// </summary>
        /// <param name="view"></param>
        /// <param name="mathUtility"></param>
        /// <returns></returns>
        public static MathVector GetXAxis(this View view, MathUtility mathUtility)
        {
            return Vector3.UnitX.ToSwMathPoint(mathUtility).MultiplyTransform(view.ModelToViewTransform.Inverse()) as MathVector;
        }

        /// <summary>
        /// 获取视图X在模型空间的表达
        /// </summary>
        /// <param name="view"></param>
        /// <param name="mathUtility"></param>
        /// <returns></returns>
        public static MathVector GetYAxis(this View view, MathUtility mathUtility)
        {
            return Vector3.UnitY.ToSwMathPoint(mathUtility).MultiplyTransform(view.ModelToViewTransform.Inverse()) as MathVector;
        }

        /// <summary>
        /// 获取视图垂直向量在模型空间的表达
        /// </summary>
        /// <param name="view"></param>
        /// <param name="mathUtility"></param>
        /// <returns></returns>
        public static Vector3 GetZAxisVec(this View view, MathUtility mathUtility)
        {
            return view.GetZAxis(mathUtility).ToVector3();
        }

        
    }
}
