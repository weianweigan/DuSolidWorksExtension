using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
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
        /// 获取边起点到终点的长度
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        /// <remarks>非直线的话不可以使用此长度</remarks>
        public static double GetVectorLength(this IEdge edge)
        {
            return edge.GetEdge3().Delta.Length();
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        /// <remarks>非直线的话不可以使用此长度</remarks>
        //public static double GetLength(this IEdge edge)
        //{
        //    var curcve = edge.GetCurve() as Curve;
        //    curcve.GetLength3()
        //}
    }
}
