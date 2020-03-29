using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{
    /// <summary>
    /// 描述了点法向量
    /// </summary>
    public class PointDirection3
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="direction">向量</param>
        /// <param name="point">坐标</param>
        public PointDirection3(Vector3 direction, Vector3 point)
        {
            Direction = direction;
            Point = point;
        }

        public PointDirection3 Transform(MathTransform tran,MathUtility uti = null)
        {
            if (uti == null)
            {
                uti = MathUtil.swMathUtility;
            }
            var vec = Direction.ToSwMathVector(uti).IMultiplyTransform(tran);
            var point = Point.ToSwMathPoint(uti).IMultiplyTransform(tran);
            return new PointDirection3(vec.ToVector3(), point.ToVector3());
        }

        /// <summary>
        /// 方向
        /// </summary>
        public Vector3 Direction { get;  set; }

        /// <summary>
        /// 坐标点
        /// </summary>
        public Vector3 Point { get;  set; }
    }
}
