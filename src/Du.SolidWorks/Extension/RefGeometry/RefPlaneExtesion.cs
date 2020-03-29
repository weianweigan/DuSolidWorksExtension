using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class RefPlaneExtesion
    {
        /// <summary>
        /// 获取基准面的法向量
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="math"></param>
        /// <returns></returns>
        public static double[] Normal(this IRefPlane plane, IMathUtility math)
        {
            var transform = plane.Transform;
            var normalWorld = math.CreateVector(new[] { 0.0, 0, 1 }) as MathVector;
            return ((MathTransform)normalWorld.MultiplyTransform(transform)).ArrayData as double[];
        }

        /// <summary>
        /// 获取基准面原点
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="math"></param>
        /// <returns></returns>
        public static double[] Origin(this IRefPlane plane, IMathUtility math)
        {
            var transform = plane.Transform;
            var originWorld = math.CreatePoint(new[] { 0.0, 0, 0 }) as MathVector;
            return ((MathTransform)originWorld.MultiplyTransform(transform)).ArrayData as double[];
        }

        /// <summary>
        /// 获取平面的点法式
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="math"></param>
        /// <returns></returns>
        public static PointDirection3 GetPointDirection3(this IRefPlane plane,IMathUtility math)
        {
            return new PointDirection3(new Vector3(plane.Normal(math)), new Vector3(plane.Origin(math)));
        }
    }
}
