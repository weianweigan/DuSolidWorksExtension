using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class ISketchLineExtension
    {

        /// <summary>
        /// 获取<see cref="Vector3"/> 类型的起点
        /// </summary>
        /// <param name="line"><see cref="ISketchLine"/></param>
        /// <returns><see cref="Vector3"/></returns>
        public static Vector3 GetStartPointEx(this ISketchLine line)
        {
            var startPoint = line.GetStartPoint2() as ISketchPoint;
            return new Vector3(startPoint.X, startPoint.Y, startPoint.Z);
        }

        /// <summary>
        /// 获取<see cref="Vector3"/> 类型的终点
        /// </summary>
        /// <param name="line"><see cref="ISketchLine"/></param>
        /// <returns><see cref="Vector3"/></returns>
        public static Vector3 GetEndPointEx(this ISketchLine line)
        {
            var endPoint = line.IGetEndPoint2() as ISketchPoint;
            return new Vector3(endPoint.X, endPoint.Y, endPoint.Z);
        }

        /// <summary>
        /// 获取起点和终点
        /// </summary>
        /// <param name="line"><see cref="ISketchLine"/></param>
        /// <param name="startPoint">起点 <see cref="Vector3"/></param>
        /// <param name="endPoint">终点 <see cref="Vector3"/></param>
        public static void GetPoints(this ISketchLine line, out Vector3 startPoint,out Vector3 endPoint)
        {
            startPoint = line.GetStartPointEx();
            endPoint = line.GetEndPointEx();
        }


        /// <summary>
        /// 获取起点
        /// </summary>
        /// <param name="line"><see cref="ISketchLine"/></param>
        /// <returns><see cref="ISketchPoint"/></returns>
        public static ISketchPoint GetStartPoint2Ex(this ISketchLine line)
        {
            return line.GetStartPoint2() as ISketchPoint;
        }

        /// <summary>
        /// 获取终点
        /// </summary>
        /// <param name="line"><see cref="ISketchLine"/></param>
        /// <returns><see cref="ISketchPoint"/></returns>
        public static ISketchPoint GetEndPoint2Ex(this ISketchLine line)
        {
            return line.GetEndPointEx() as ISketchPoint;
        }

        /// <summary>
        /// 等分直线,获取等分后的点, ex: 三等分返回两个点
        /// </summary>
        /// <param name="line"></param>
        /// <param name="count"></param>
        /// <param name="spare">在两头减去的内容</param>
        /// <returns></returns>
        public static IEnumerable<Vector3> DivLine(this ISketchLine line,int count,double spare = 0)
        {
            List<Vector3> points = new List<Vector3>();

            line.GetPoints(out Vector3 sPoint, out var ePoint);

            double length = sPoint.Distance(ePoint) - spare *2;
            if (length < 0) throw new ArgumentOutOfRangeException($"spare: {spare} is large than Length of the Line");
            var Direction = (ePoint - sPoint).Unit();

            for (int i = 1; i < count; i++)
            {
                points.Add(sPoint + (Direction.Scale((i * length) / count +spare )));
            }

            return points;
        }

        public static Vector3 MiddlePoint(this ISketchLine line)
        {
            line.GetPoints(out Vector3 sPoint, out var ePoint);
            return new Vector3(
                (sPoint.X + ePoint.X) / 2,
                (sPoint.Y + ePoint.Y) / 2,
                (sPoint.Z + ePoint.Z) / 2
                );
        }

    }


    //public static class ISketchPointExtension
    //{
    //    public static ISketchPoint 
    //}
}
