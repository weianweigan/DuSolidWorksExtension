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

        /// <summary>
        /// 判断一个值是否在两个值之间，并返回中间的值
        /// </summary>
        /// <param name="f"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static double Clamp(double f, double low, double high)
        {
            return (f < low) ? low : (f > high) ? high : f;
        }

        public static bool DoubleEqual(double a,double b,double eps)
        {
            return System.Math.Abs(a - b) < eps; //判断是否相等 
        }

    }
}