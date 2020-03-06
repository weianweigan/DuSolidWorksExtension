using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{
    public static class MathVectorExtension
    {

        /// <summary>
        /// a X b => 投影到 b
        /// gives the multiplier for b which would be the projection of a on b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Project(this IMathVector a, IMathVector b)
        {
            return a.Dot(b) / (b.Dot(b));
        }


        public static Vector3 ToVector3(this MathVector mathvector)
        {
            return new Vector3((double[])mathvector.ArrayData);
        }

        public static double LengthOfProjectionXY(this double[] vector)
        {
            return System.Math.Sqrt(vector.Take(2).Sum(c => System.Math.Pow(c, 2)));
        }

        /// <summary>
        /// 两个向量之间的角度
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <returns></returns>
        //public static double AngleBetweenVectors(this IMathVector v0, IMathVector v1)
        //{
        //    if (((double[])v0.ArrayData).Length == ((double[])v1.ArrayData).Length)
        //    {
        //        var sign = System.Math.Sign(((IMathVector)(v0.Cross(v1))).ArrayData as double[2]));
        //        var ret = System.Math.Acos(v0.Dot(v1) / (v0.GetLength() * v1.GetLength()));
        //        return sign * ret;
        //    }
        //    throw new Exception("Vectors must have the same dimension!");
        //}
    }
}
