using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class Matrix4X4ExtensionsSw
    {
        public static MathTransform ToSwTransform(this Matrix4x4 m, MathUtility math = null) =>
            (math ?? MathUtil.swMathUtility).ToSwMatrix(m);
    }
}
