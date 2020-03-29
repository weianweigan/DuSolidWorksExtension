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
    /// Extension Methods for <see cref="IEdge"/> Interface
    /// </summary>
    public static class EdgeExtension
    {

        /// <summary>
        /// 转换为Edge3
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public static Edge3 GetEdge3(this IEdge edge)
        {
            var vertexS = edge.GetStartVertex() as Vertex;
            var vertexE = edge.GetEndVertex() as Vertex;

            return new Edge3(
                ((double[])vertexS.GetPoint()).ToVector3(),
                ((double[])vertexE.GetPoint()).ToVector3());
        }

        /// <summary>
        /// 获取边起点到终点的长度，按照直线计算
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        /// <remarks>非直线的话不可以使用此长度</remarks>
        public static double GetVectorLength(this IEdge edge)
        {
            return edge.GetEdge3().Delta.Length();
        }


        //public static double GetLength(this IEdge edge)
        //{
        //    var curcve = edge.GetCurve() as Curve;
        //    curcve.GetLength3()
        //}


        /// <summary>
            /// 判断边是否是圆
            /// </summary>
            /// <param name="edge">IEdge interface</param>
            /// <returns></returns>
        public static bool IsCirlce(this IEdge edge)
        {
            return edge.IGetCurve().IsCircle();
        }

        /// <summary>
        /// 判断是否是直线
        /// </summary>
        /// <param name="edge">IEdge Interface</param>
        /// <returns></returns>
        public static bool IsLine(this IEdge edge)
        {
            return edge.IGetCurve().IsLine();
        }

        /// <summary>
        /// 获取圆形边的参数 return value: Center ,Axis ,Radius
        /// </summary>
        /// <param name="edge">IEdge Interface</param>
        /// <returns>Tuple Item1:Center point Item2 : Axis Point Item3 :Radius</returns>
        public static Tuple<Vector3,Vector3,double> GetCirlceParams(this IEdge edge)
        {
            var curve = edge.IGetCurve();
            if (curve.IsCircle())
            {
                double[] param = curve.GetCircleParams();
                return new Tuple<Vector3, Vector3, double>(
                    new Vector3(param[0], param[1], param[2]), 
                    new Vector3(param[3], param[4], param[5]), 
                    param[6]);
            }
            else
            {
                throw new InvalidOperationException($"this Edge is not a circle ,so you cannot use this method to get circel params");
            }
        }

        /// <summary>
        /// 作为实体接口使用
        /// </summary>
        /// <param name="edge">IEdge Interface</param>
        /// <returns></returns>
        /// <remarks>
        /// 此方法将IEdge接口强制转换为IEntity接口使用
        /// </remarks>
        public static IEntity AsIEntity(this IEdge edge)
        {
            return edge as IEntity;
        }

        /// <summary>
        /// 选中边线
        /// </summary>
        /// <param name="edge">IEdge Interface</param>
        /// <param name="Append">true to Append</param>
        /// <param name="Mark">set selection mark</param>
        /// <returns></returns>
        /// <remarks>
        /// 将IEdge 转换为 IEntity 然后调用IEntity的 Select2方法
        /// </remarks>
        public static bool Select2(this IEdge edge,bool Append,int Mark)
        {
            return edge.AsIEntity().Select2(Append, Mark);
        }

        /// <summary>
        /// 获取和某点的最近点
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double DistaceWithPoint(this IEdge edge,Vector3 point)
        {
            var facePoint = edge.GetClosestPointOn(point.X, point.Y, point.Z);
            return point.Distance(new Vector3(facePoint));
        }
    }
}
