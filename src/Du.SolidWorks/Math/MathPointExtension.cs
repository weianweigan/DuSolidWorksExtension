using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{
    public static class MathPointExtension
    {
        /// <summary>
        /// 转换到Vector3
        /// </summary>
        /// <param name="mathPoint"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this MathPoint mathPoint)
        {
            return new Vector3((double[])mathPoint.ArrayData);
        }

        /// <summary>
        /// 计算在某轴上的投影
        /// </summary>
        /// <param name="point"></param>
        /// <param name="origin"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static MathPoint Project(this IMathPoint point, IMathPoint origin, IMathVector axis)
        {
            var a = (IMathVector)point.Subtract(origin);
            var t = a.Project(axis);
            var v = (MathVector)axis.Scale(t);
            return (MathPoint)origin.AddVector(v);
        }

        public static MathVector SubtractTs(this IMathPoint a, IMathPoint b)
        {
            return (MathVector)a.Subtract(b);
        }

        public static MathVector SubtractTs(this IMathVector a, IMathVector b)
        {
            return (MathVector)a.Subtract(b);
        }

        /// <summary>
        /// 此点在 XY Plane 中的角度
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double Angle2D(this IMathPoint p)
        {
            var pData = p.ArrayData as double[];
            var angle = System.Math.Atan2(pData[1], pData[0]);
            return angle < 0.0 ? angle + 2 * System.Math.PI : angle;
        }
    }
}
