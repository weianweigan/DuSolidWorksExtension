using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class ISketchSegmentExtension
    {
        /// <summary>
        /// 获取草图元素的类型
        /// </summary>
        /// <param name="sketchSegment"><see cref="ISketchSegment"/> interface</param>
        /// <returns><see cref="swSketchSegments_e"/></returns>
        public static swSketchSegments_e GetTypeEx(this ISketchSegment sketchSegment)
        {
            return (swSketchSegments_e)sketchSegment.GetType();
        }


    }
}
