using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class ModelerExtension
    {
        /// <summary>
        /// 创建点0 到 点1 的直线
        /// </summary>
        /// <param name="modeler"></param>
        /// <param name="edge"></param>
        /// <returns></returns>
        public static ICurve CreateTrimmedLine(this IModeler modeler, Edge3 edge)
        {
            Debug.Assert(edge.Delta.Length() > double.Epsilon);
            var startPoint = new[] { (double)edge.A.X, (double)edge.A.Y, (double)edge.A.Z };
            var dir = edge.Delta.Unit();
            var direction = new[] { (double)dir.X, (double)dir.Y, (double)dir.Z };
            var line = (ICurve)modeler.CreateLine(startPoint, direction);
            Debug.Assert(line != null, "line != null");
            var pp0 = line.GetClosestPointOn(edge.A.X, edge.A.Y, edge.A.Z) as double[];
            var pp1 = line.GetClosestPointOn(edge.B.X, edge.B.Y, edge.B.Z) as double[];
            line = line.CreateTrimmedCurve2(pp0[0], pp0[1], pp0[2], pp1[0], pp1[1], pp1[2]);
            //var trimmedLine = line.CreateTrimmedCurve(pp0[3], pp1[3]);
            //Debug.Assert(trimmedLine != null, "line != null");
            return line;
        }

        /// <summary>
        /// 创建直线
        /// </summary>
        /// <param name="modeler">造型接口</param>
        /// <param name="p0">起点</param>
        /// <param name="p1">终点</param>
        /// <returns></returns>
        public static ICurve CreateTrimmedLine(this IModeler modeler, MathPoint p0, MathPoint p1)
        {
            return CreateTrimmedLine(modeler,new Edge3( ((double[])p0.ArrayData).ToVector3(), ((double[])p1.ArrayData).ToVector3()));
        }

        /// <summary>
        /// 创建直线
        /// </summary>
        /// <param name="modeler">造型接口</param>
        /// <param name="math">数学计算接口</param>
        /// <param name="p0">起点</param>
        /// <param name="v0">方向</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static ICurve CreateTrimmedLine(this IModeler modeler,MathUtility math ,Vector3 p0, Vector3 v0, double length)
        {
            v0 = v0.Unit().Scale(length);
            var p1 = p0.Add(v0);
            return CreateTrimmedLine(modeler, p0.ToSwMathPoint(math), p1.ToSwMathPoint(math));
        }

        /// <summary>
        /// 生成锥体
        /// </summary>
        /// <param name="m">造型接口</param>
        /// <param name="center">中心</param>
        /// <param name="axis">旋转轴</param>
        /// <param name="baseRadius">底部半径</param>
        /// <param name="topRadius">顶部半径</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static IBody2 CreateCone
            (this IModeler m, Vector3 center, Vector3 axis, double baseRadius, double topRadius, double height)
        {
            var array = center.ToDoubles().Concat(axis.ToDoubles()).Concat(new[] { baseRadius, topRadius, height }).ToArray();
            return (IBody2)m.CreateBodyFromCone(array);
        }

        /// <summary>
        /// 生成圆柱体
        /// </summary>
        /// <param name="m">造型接口</param>
        /// <param name="radius">半径</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static IBody2 CreateCylinder
            (this IModeler m, double radius, double height)
        {
            var center = Vector3.Zero;
            var axis = Vector3.UnitZ;
            return CreateBodyFromCylTs(m, center, axis, radius, height);
        }

        /// <summary>
        /// 指定位置创建圆柱体
        /// </summary>
        /// <param name="modeler">造型接口</param>
        /// <param name="xyz">坐标</param>
        /// <param name="axis">轴心</param>
        /// <param name="radius">半径</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static IBody2 CreateBodyFromCylTs(this IModeler modeler, Vector3 xyz, Vector3 axis, double radius, double length)
        {
            var array = xyz
                .ToDoubles()
                .Concat(axis.ToDoubles())
                .Concat(new[] { radius, length })
                .ToArray();

            return (IBody2)modeler.CreateBodyFromCyl(array);
        }

        /// <summary>
        /// 创建长方体
        /// </summary>
        /// <param name="modeler"></param>
        /// <param name="center"></param>
        /// <param name="axis"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static IBody2 CreateBox
            (this IModeler modeler
            , Vector3 center
            , Vector3 axis
            , double width
            , double length
            , double height
            )
        {
            var array = new List<double>();
            array.AddRange(center.ToDoubles());
            array.AddRange(axis.ToDoubles());
            array.Add(width);
            array.Add(length);
            array.Add(height);
            return modeler.CreateBodyFromBox3(array.ToArray());
        }

        /// <summary>
        /// 创建长方体
        /// </summary>
        /// <param name="modeler"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static IBody2 CreateBox(this IModeler modeler,
            double width,
            double length,
            double height) => modeler.CreateBox
                (Vector3.Zero,
                    Vector3.UnitZ,
                    width,
                    length,
                    height);

        /// <summary>
        /// 将点合成线
        /// </summary>
        /// <param name="modeler"></param>
        /// <param name="points"></param>
        /// <param name="chordTolerance"></param>
        /// <param name="simplify"></param>
        /// <param name="closedCurve"></param>
        /// <returns></returns>
        public static Curve InterpolatePointsToCurve
           (this IModeler modeler
           , List<Vector3> points
           , double chordTolerance
           , bool simplify = true
           , bool closedCurve = false)
        {
            //points = CurveExtensions.FilterOutShortLines(points, 1e-5).ToList();

            if (closedCurve)
            {
                points.Add(points.First());
            }

            List<ICurve> lines = new List<ICurve>();

            for (int i = 0; i < points.Count -1; i++)
            {
                modeler.CreateTrimmedLine(points[i].ToSwMathPoint(), points[i+1].ToSwMathPoint());
            }

            var curve = modeler.MergeCurves(lines);
            if (simplify)
                curve = curve.SimplifyBCurve(chordTolerance);
            return curve;
        }

        public static IBody2 CreateSphereBody(this IModeler modeler, Vector3 center, double radius)
        {
            var axis = Vector3.UnitZ;
            var refaxis = Vector3.UnitY;
            var sphere = (ISurface)modeler.CreateSphericalSurface2(center.ToDoubles(), axis.ToDoubles(), refaxis.ToDoubles(), radius);

            var swSurfPara = sphere.Parameterization2();

            var uvrange = new double[]
            {
                swSurfPara.UMin,
                swSurfPara.UMax,
                swSurfPara.VMin,
                swSurfPara.VMax
            };


            var sphereBody = (IBody2)modeler.CreateSheetFromSurface(sphere, uvrange);
            return sphereBody;
        }

        /// <summary>
        /// Create a surface from a plane definition
        /// </summary>
        /// <param name="activeModeler"></param>
        /// <param name="plane"></param>
        /// <returns></returns>
        //public static ISurface CreatePlanarSurface(this IModeler activeModeler, PointDirection3 plane)
        //{
        //    var planeDirection = plane.Direction;
        //    var vRef = planeDirection.Orthogonal();
        //    var mathVector = planeDirection;
        //    var vRootPoint = plane.Point;
        //    return (ISurface)activeModeler.CreatePlanarSurface2(vRootPoint.ToDoubles(), mathVector.ToDoubles(), vRef.ToDoubles());
        //}
    }

    public class Edge3
    {
        public Edge3(Vector3 startPoint,Vector3 endPoint)
        {
            A = startPoint;
            B = endPoint;
            Delta = new Delat(new Vector3(A.X - B.X, A.Y - B.Y, A.Z - B.Z));
        }
        public Vector3 A { get; set; }
        public Vector3 B { get; set; }
        public Delat Delta { get; set; }
    }

    public class Delat
    {
        public Vector3 _Vector;
        public Delat(Vector3 vector3)
        {
            _Vector = vector3;
        }
        public double Length()
        {
            return _Vector.Length;
        }

        public Vector3 Unit()
        {
            return _Vector.Unit();
        }
    }
}
