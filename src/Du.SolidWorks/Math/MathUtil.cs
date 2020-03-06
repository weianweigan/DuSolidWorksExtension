using SolidWorks.Interop.sldworks;
using System;

namespace Du.SolidWorks.Math
{
    public class MathUtil
    {
        public static MathUtility swMathUtility { get; set; }

        public const double Epsilon = 2.2204460492503131e-016;

        /// <summary>
        /// 每弧度代表的角度
        /// </summary>
        public const double Rad2Deg = (180.0 / System.Math.PI);
        public const double ZeroTolerance = 1e-08;

        public static double Clamp(double f, double low, double high)
        {
            return (f < low) ? low : (f > high) ? high : f;
        }
    }
}