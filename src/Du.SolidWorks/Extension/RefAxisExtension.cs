using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class RefAxisExtension
    {
        public static Edge3 GetEdge3(this IRefAxis axis)
        {
            var axisParams = axis.GetRefAxisParams() as double[];
            return new Edge3(new Vector3((double)axisParams[0], (double)axisParams[1], (double)axisParams[2])
                , new Vector3((double)axisParams[3], (double)axisParams[4], (double)axisParams[5]));
        }
    }
}
