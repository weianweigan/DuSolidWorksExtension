using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class MathTransformExtension
    {
        /// <summary>
        /// 获取位置
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Vector3 GetPostion(this MathTransform transform)
        {
            var data = transform.ArrayData as double[];
            return new Vector3(data[9], data[10], data[11]);
        }

        /// <summary>
        /// 获取X轴
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Vector3 GetXAxis(this MathTransform transform)
        {
            var data = transform.ArrayData as double[];
            return new Vector3(data[0], data[1], data[2]);
        }

        /// <summary>
        /// 获取Y轴
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Vector3 GetYAxis(this MathTransform transform)
        {
            var data = transform.ArrayData as double[];
            return new Vector3(data[3], data[4], data[5]);
        }

        /// <summary>
        /// 获取Z轴
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Vector3 GetZAxis(this MathTransform transform)
        {
            var data = transform.ArrayData as double[];
            return new Vector3(data[6], data[7], data[8]);
        }

        public static System.Numerics.Matrix4x4 ToMatrix4X4(this MathTransform math)
        {
            float[] farray = new float[16];
            double[] array = math.ArrayData as double[];
            for (int i = 0; i < array.Length; i++)
            {
                farray[i] =Convert.ToSingle(array[i]);
            }
            return new System.Numerics.Matrix4x4(farray[0], farray[1], farray[2], farray[3],
                                                 farray[4], farray[5], farray[6], farray[7],
                                                 farray[8], farray[9], farray[10], farray[11],
                                                 farray[12], farray[13], farray[14], farray[15]
                                                 );
        }
    }
}
