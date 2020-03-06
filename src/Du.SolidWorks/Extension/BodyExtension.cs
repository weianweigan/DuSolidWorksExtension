using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// 实体接口扩展方法 Body Interface Extension methods
    /// </summary>
    public static class BodyExtension
    {

        /// <summary>
        /// 获取网格化对象
        /// </summary>
        /// <param name="body"></param>
        /// <param name="faceList"></param>
        /// <returns></returns>
        public static ITessellation GetTess(IBody2 body, IFace2[] faceList)
        {
            var tess = (ITessellation)body.GetTessellation(faceList);
            tess.NeedFaceFacetMap = true;
            tess.NeedVertexParams = true;
            tess.NeedVertexNormal = true;
            tess.ImprovedQuality = true;
            tess.NeedEdgeFinMap = true;
            tess.CurveChordTolerance = 0.001 / 10;
            tess.SurfacePlaneTolerance = 0.001 / 10;
            tess.MatchType = (int)swTesselationMatchType_e.swTesselationMatchFacetTopology;
            tess.Tessellate();
            return tess;

        }

        public static IBody2 Add(this IEnumerable<IBody2> bodies)
        {
            return bodies.Aggregate((a, acc) => a.Add(acc).Bodies[0]);
        }
        public static OperationsResult Add(this IBody2 body, IBody2 tool)
        {
            return body.OperationsTs(swBodyOperationType_e.SWBODYADD, tool);
        }

        /// <summary>
        /// Perform Add, Cut, Intersect operations on solid bodies. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="type"></param>
        /// <param name="tool"></param>
        /// <returns></returns>
        public static OperationsResult OperationsTs(this IBody2 body, swBodyOperationType_e type, IBody2 tool)
        {
            if (body == null) throw new ArgumentNullException(nameof(body));
            if (tool == null) throw new ArgumentNullException(nameof(tool));

            tool = tool.CopyTs();
            body = body.CopyTs();

            int error;
            var objects = (object[])body.Operations2((int)type, tool, out error);
            if (objects == null)
                return new OperationsResult(error, new IBody2[] { });

            var bodies = error == 0 ? objects.Cast<IBody2>().ToArray() : new IBody2[] { };
            return new OperationsResult(error, bodies);
        }

        public static IBody2 CopyTs(this IBody2 tool)
        {
            return (IBody2)tool.Copy();
        }

    
    }

    /// <summary>
    /// Result for OperationsTs call.
    /// </summary>
    public class OperationsResult
    {
        /// <summary>
        /// If 0 then there is no error
        /// </summary>
        public int Error { get; }

        /// <summary>
        /// If error == 0 then this contains the resulting bodies from the operations
        /// </summary>
        public IBody2[] Bodies { get; }

        public OperationsResult(int error, IBody2[] bodies)
        {
            Error = error;
            Bodies = bodies;
        }
    }
}
