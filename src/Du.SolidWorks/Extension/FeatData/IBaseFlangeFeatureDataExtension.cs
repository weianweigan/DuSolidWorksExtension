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
    /// Extension Methods for <see cref="IBaseFlangeFeatureData"/>
    /// </summary>
    public static class IBaseFlangeFeatureDataExtension
    {
        /// <summary>
        /// 获取实际宽度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double GetWidth(this IBaseFlangeFeatureData data)
        {
            if (data.OffsetDirections == 1)
            {
                return data.D1OffsetDistance;
            }
            else
            {
                return data.D1OffsetDistance + data.D2OffsetDistance;
            }
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        /// <param name="twoDirectionDiv">如果实例方向,则对称设置</param>
        public static void SetWdith(this IBaseFlangeFeatureData data,double value,bool twoDirectionDiv = true)
        {
            if (data.OffsetDirections == 1)
            {
                data.D1OffsetDistance = value;
            }
            else
            {
                if (twoDirectionDiv)
                {
                    data.D1OffsetDistance = value / 2;
                    data.D2OffsetDistance = value / 2;
                }
                else
                {
                    throw new InvalidOperationException($"当前基本法兰薄片特征两个方向,无法设置一个方向宽度");
                }
            }
        }

        /// <summary>
        /// 设置厚度
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        public static void SetThickness(this IBaseFlangeFeatureData data,double value)
        {
            data.Thickness = value;
        }

        /// <summary>
        /// 方向1的类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static swFlangeOffsetTypes_e GetD1OffSetType(this IBaseFlangeFeatureData data)
        {
            return data.D1OffsetType.CastObj<swFlangeOffsetTypes_e>();
        }


        /// <summary>
        /// 方向2的类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static swFlangeOffsetTypes_e GetD2OffSetType(this IBaseFlangeFeatureData data)
        {
            return data.D1OffsetType.CastObj<swFlangeOffsetTypes_e>();
        }
    }
}
