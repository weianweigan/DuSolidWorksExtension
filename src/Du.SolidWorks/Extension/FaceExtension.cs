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
    /// IFace2 的扩展方法
    /// </summary>
    public static class FaceExtension
    {
        /// <summary>
        /// 获取面的外轮廓
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static Range3Double GetBoxTs(this IFace2 face)
        {
            var box = (double[])face.GetBox();
            return new Range3Double(
                box[0], box[1], box[2],
                box[3], box[4], box[5]);
        }

        /// <summary>
        /// 获取两个面之间的距离
        /// </summary>
        /// <param name="entity0">第一个面</param>
        /// <param name="entity1">第二个面</param>
        /// <param name="posacast">第一个面上的位置</param>
        /// <param name="posbcast">第二个面上的位置</param>
        /// <returns></returns>
        public static bool GetDistance(this IFace2 entity0, IFace2 entity1, out double[] posacast, out double[] posbcast)
        {
            var bounds = entity1.GetUVBounds() as double[];

            var param = new[] { bounds[0], bounds[2], bounds[1], bounds[3] };

            object posa, posb;
            double distance;
            var result = ((IEntity)entity0).GetDistance(entity1, true, param, out posa, out posb, out distance);
            posacast = posa as double[];
            posbcast = posb as double[];
            return result == 0;
        }

        /// <summary>
        /// 将面网格化
        /// </summary>
        /// <param name="face">IFace2 interface</param>
        /// <param name="noConversion">True prohibits conversion to user units from system units, false does not</param>
        /// <returns></returns>
        private static double[][] GetTessTrianglesTs(this IFace2 face, bool noConversion)
        {
            var data = (double[])face.GetTessTriangles(noConversion);
            return data.Select((value, index) => new { value, index })
                .GroupBy(x => x.index / 3, x => x.value) // a group is a point of the triangle
                .Select(x => x.ToArray())
                .ToArray();
        }

        /// <summary>
        /// 使用设置的单位
        /// </summary>
        /// <param name="face">IFace2 interface</param>
        /// <returns></returns>
        public static double[][] GetTessTrianglesNoConversion(this IFace2 face) => face.GetTessTrianglesTs(true);

        /// <summary>
        /// 使用系统单位
        /// </summary>
        /// <param name="face">IFace2 interface</param>
        /// <returns></returns>
        public static double[][] GetTessTrianglesAllowConversion(this IFace2 face) => face.GetTessTrianglesTs(false);

        /// <summary>
        /// 取面上随机点
        /// </summary>
        /// <param name="face"></param>
        /// <param name="seed">随机种子 默认为16435425</param>
        /// <returns></returns>
        public static Vector3 RandomPointOnFace(this IFace2 face,int seed = 16435425)
        {
            Random random = new Random(seed);

            var point = face.GetClosestPointOn(random.NextDouble(), random.NextDouble(), random.NextDouble());

            var pointData = point as double[];

            return  new Vector3(pointData);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="face"><see cref="IFace2"/></param>
        /// <param name="pointInput"></param>
        /// <returns></returns>
        public static double ClosestDistance(this IFace2 face,Vector3 pointInput)
        {
            var point = face.GetClosestPointOn(pointInput.X,pointInput.Y,pointInput.Z);

            var pointData = point as double[];

            return pointInput.Distance(new Vector3(pointData));
        }
    }

    /// <summary>
    /// 使用两个对角点表示3D空间的范围
    /// </summary>
    public class Range3Double
    {
        /// <summary>
        /// 初始化构造点
        /// </summary>
        /// <param name="data">大于6个长度的参数,小于6个抛出异常</param>
        public Range3Double(params double[] data)
        {
            if (data.Length != 6)
            {
                throw new Exception("范围需要6个长度");
            }
            Corner1 = new Vector3(data[0], data[1], data[2]);
            Corner2 = new Vector3(data[3], data[4], data[5]);

        }

        /// <summary>
        /// corner of rectangle
        /// </summary>
        public Vector3 Corner1 { get; set; }

        /// <summary>
        /// another corner of rectangle
        /// </summary>
        public Vector3 Corner2 { get; set; }
    }
}
