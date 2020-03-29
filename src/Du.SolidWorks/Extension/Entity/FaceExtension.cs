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
    /// Extension Methods for <see cref="IFace2"/>
    /// </summary>
    public static class FaceExtension
    {
        /// <summary>
        /// 获取面的包络盒
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
        /// 转换为实体
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static IEntity AsEnitity(this IFace2 face)
        {
            return (IEntity)face;
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
        /// 获取面的法向量
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static Vector3 GetNormal(this IFace2 face)
        {
            return new Vector3(face.Normal as double[]);
        }

        /// <summary>
        /// 将面网格化
        /// </summary>
        /// <param name="face">IFace Interface</param>
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
        /// 将面网格化,使用用户单位
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static double[][] GetTessTrianglesNoConversion(this IFace2 face) => face.GetTessTrianglesTs(true);

        /// <summary>
        /// 将面网格化 使用系统单位 (米)
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static double[][] GetTessTrianglesAllowConversion(this IFace2 face) => face.GetTessTrianglesTs(false);

        /// <summary>
        /// 获取面上所有环
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static IEnumerable<ILoop2> GetLoopsEx(this IFace2 face)
        {
            return (face.GetLoops() as object[]).Cast<ILoop2>();
        }

        /// <summary>
        /// 获取外环
        /// </summary>
        /// <param name="face">IFace Interface</param>
        /// <returns></returns>
        public static ILoop2 GetOuterLoop(this IFace2 face)
        {
            return face.GetLoopsEx().Where(loop => loop.IsOuter()).FirstOrDefault();
        }

        /// <summary>
        /// 如果外环是个圆,获取和外环同圆心的内环圆
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static bool TryGetInnerLoop(this IFace2 face,out  ILoop2 innerLoop)
        {
            var outloop = face.GetOuterLoop();
            innerLoop = outloop;
            if (outloop.TryGetOneCircleEdge(out IEdge edge))
            {
                var circlePoint = edge.GetCirlceParams().Item1;
                innerLoop = face.GetLoopsWithoutOuterLoop().Where(l => 
                     l.TryGetOneCircleEdge(out IEdge Inneredge) ? (IsSameVector3(Inneredge.GetCirlceParams().Item1 , circlePoint) ? true : false ): false
                ).FirstOrDefault();
                return innerLoop != null ? true : false;
            }
            return false;
        }

        private static bool  IsSameVector3(Vector3 left, Vector3 right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return (MathUtil.DoubleEqual(left.X, right.X, 1e-5) &&
                MathUtil.DoubleEqual(left.Y, right.Y, 1e-5) &&
                MathUtil.DoubleEqual(left.Z, right.Z, 1e-5));
        }

        /// <summary>
        /// 获取除了外环的所有环
        /// </summary>
        /// <param name="face">IFace Interface</param>
        /// <returns></returns>
        public static IEnumerable<ILoop2> GetLoopsWithoutOuterLoop(this IFace2 face)
        {
            return face.GetLoopsEx().Where(loop => !loop.IsOuter());
        }

        /// <summary>
        /// 获取面所在的特征
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static IFeature GetFeatureEx(this IFace2 face)
        {
            return face.GetFeature() as IFeature;
        }

        /// <summary>
        /// 求面于某点的距离
        /// </summary>
        /// <param name="face"><see cref="IFace2"/></param>
        /// <param name="point"><see cref="Vector3"/></param>
        /// <returns></returns>
        public static double DistanceWithPoint(this IFace2 face,Vector3 point)
        {
            var facePoint = face.GetClosestPointOn(point.X, point.Y, point.Z);
            return point.Distance(new Vector3(facePoint));
        }
    }

    /// <summary>
    /// 描述由两个点组成的包络盒
    /// </summary>
    public class Range3Double
    {
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
