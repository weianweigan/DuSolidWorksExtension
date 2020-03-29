using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// ILoop2 接口的扩展方法
    /// </summary>
    public static class ILoop2Extension
    {

        /// <summary>
        /// 获取所有的边
        /// </summary>
        /// <param name="loop">ILoop2 Interface</param>
        /// <returns></returns>
        public static IEnumerable<IEdge> GetEdgesEx(this ILoop2 loop)
        {
            return (loop.GetEdges() as object[]).Cast<IEdge>();
        }

        /// <summary>
        /// 获取环中所有的圆边
        /// </summary>
        /// <param name="loop"></param>
        /// <returns></returns>
        public static IEnumerable<IEdge> GetCircleEdge(this ILoop2 loop)
        {
            return loop.GetEdgesEx().Where(edge => edge.IsCirlce());
        }

        /// <summary>
        /// 获取环中所有的直线边
        /// </summary>
        /// <param name="loop"></param>
        /// <returns></returns>
        public static IEnumerable<IEdge> GetLineEdge(this ILoop2 loop)
        {
            return loop.GetEdgesEx().Where(edge => edge.IsLine());
        }

        /// <summary>
        /// 如果此环只包含一个Edge，并且这个Edge是个圆弧，返回True，负责返回false
        /// </summary>
        /// <param name="loop">ILoop2 Interface</param>
        /// <param name="edge"></param>
        /// <returns></returns>
        public static bool TryGetOneCircleEdge(this ILoop2 loop,out IEdge edge)
        {
            var edges = loop.GetEdgesEx();

            edge = edges.FirstOrDefault();

            if (edges.Count() == 1 && edges.First().IsCirlce())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
