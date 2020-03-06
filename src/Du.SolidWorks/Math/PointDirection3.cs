using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{
    public class PointDirection3
    {

        public PointDirection3(Vector3 direction, Vector3 point)
        {
            Direction = direction;
            Point = point;
        }

        public PointDirection3 Transform(MathTransform tran,MathUtility uti)
        {
           var vec = Direction.ToSwMathVector(uti).IMultiplyTransform(tran);
            var point = Point.ToSwMathPoint(uti).IMultiplyTransform(tran);
            return new PointDirection3(vec.ToVector3(), point.ToVector3());
        }

        public Vector3 Direction { get;  set; }
        public Vector3 Point { get;  set; }
    }
}
