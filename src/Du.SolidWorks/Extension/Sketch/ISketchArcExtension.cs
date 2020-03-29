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
    /// <see cref="ISketchArc"/> 接口的扩展方法
    /// </summary>
    public static class ISketchArcExtension
    {
        /// <summary>
        /// 获取起点
        /// </summary>
        /// <param name="arc"><see cref="ISketchArc"/> Interface</param>
        /// <returns></returns>
        public static Vector3 GetStartPoint2Ex(this ISketchArc arc)
        {
            var point = arc.GetStartPoint2() as ISketchPoint;

            return new Vector3(point.X, point.Y, point.Z);
        }
    }
}
