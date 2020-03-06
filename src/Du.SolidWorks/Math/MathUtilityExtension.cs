using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{
    public static class MathUtilityExtension
    {
        public static MathPoint PointEx(this MathUtility math, double[] value)
        {
            return math.CreatePoint(value) as MathPoint;
        }

        public static MathTransform ToSwMatrix(this MathUtility math, Matrix4x4 m)
        {
            return math.CreateTransform(new double[] { m.M11, m.M12, m.M13, m.M14 }) as MathTransform;
        }

        public static MathVector ZAxis(this MathUtility math)
        {
            return math.CreateVector(new double[] { 0, 0, 1 }) as MathVector;
        }

        public static MathVector XAxis(this MathUtility math)
        {
            return math.CreateVector(new double[] { 1, 0, 0 }) as MathVector;
        }

        public static MathVector YAxis(this MathUtility math)
        {
            return math.CreateVector(new double[] { 0, 1, 0 }) as MathVector;
        }
    }
}
