using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class ICurveExtension
    {
        public static ICurve CreateTrimmedCurve(this ICurve curve, double[] start, double[] end)
        {
            return curve.CreateTrimmedCurve2(start[0], start[1], start[2], end[0], end[1], end[2]);
        }

        public static PointParam ClosestPointOn(this ICurve curve, double x, double y, double z)
        {
            var array = curve
                .GetClosestPointOn(x, y, z)
                as double[];

            var point = array.ToVector3();
            var param = array[3];
            return new PointParam(point, point);
        }

        public static PointParam ClosestPointOn(this ICurve curve, Vector3 v) => curve.ClosestPointOn(v.X, v.Y, v.Z);


    }

    public class PointParam
    {
        public Vector3 point;
        public Vector3 param;

        public PointParam(Vector3 point, Vector3 param)
        {
            this.point = point;
            this.param = param;
        }
    }
}
