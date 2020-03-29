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
    /// <see cref="ISketch"/> 的扩展方法
    /// </summary>
    public static class ISketchExtension
    {
        /// <summary>
        /// 获取所有的草图实体
        /// </summary>
        /// <param name="ske"><see cref="ISketch"/> Interface</param>
        /// <returns></returns>
        public static IEnumerable<ISketchSegment> GetSketchSegmentsEx(this ISketch ske)
        {
            return (ske.GetSketchSegments() as object[]).Cast<ISketchSegment>();
        }

        /// <summary>
        /// 将 <see cref="ISketch"/> 转换为 IFeature
        /// </summary>
        /// <param name="sketch"><see cref="IFeature"/></param>
        /// <returns></returns>
        public static IFeature AsFeature(this ISketch sketch)
        {
            return sketch as IFeature;
        }

        /// <summary>
        /// 检测草图可用的特征
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_BASEEXTRUDE"/>:基本拉伸特征
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_BASEEXTRUDETHIN"/>: 基本薄壁拉伸特征/>
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_BOSSREVOLVE"/>:基本旋转特征/>
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_LOFTSECTION"/>:放样草图块/>
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_LOFTGUIDE"/>:放样引导线草图/>
        /// </summary>
        /// <param name="ske"><see cref="ISketch"/></param>
        /// <param name="featProfileType"><see cref="swSketchCheckFeatureProfileUsage_e"/> Interface</param>
        /// <param name="openCount">开环数量</param>
        /// <param name="closedCount">闭环数量</param>
        /// <returns><see cref="swSketchCheckFeatureStatus_e"/></returns>
        public static swSketchCheckFeatureStatus_e CheckFeatureUseEx(this ISketch ske,swSketchCheckFeatureProfileUsage_e  featProfileType,ref int openCount,ref int closedCount)
        {
            return ske.CheckFeatureUse(featProfileType.SWToInt(),ref openCount, ref closedCount).CastObj<swSketchCheckFeatureStatus_e>();
        }

        /// <summary>
        /// 检测草图可用的特征
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_BASEEXTRUDE"/>:基本拉伸特征
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_BASEEXTRUDETHIN"/>: 基本薄壁拉伸特征
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_BOSSREVOLVE"/>:基本旋转特征
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_LOFTSECTION"/>:放样草图块
        /// <see cref="swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_LOFTGUIDE"/>:放样引导线草图
        /// </summary>
        /// <param name="ske"><see cref="ISketch"/> Interface</param>
        /// <param name="featProfileType"><see cref="swSketchCheckFeatureProfileUsage_e"/></param>
        /// <returns>Tuple result
        /// Item1: <see cref="swSketchCheckFeatureStatus_e"/>
        /// Item2: <see cref="int"/> OpenCount 开环数量
        /// Item3: <see cref="int"/> ClosedCount 闭环数量
        /// </returns>
        public static Tuple<swSketchCheckFeatureStatus_e,int,int> CheckFeatureUseEx(this ISketch ske, swSketchCheckFeatureProfileUsage_e featProfileType)
        {
            int openCount = 0;
            int closedCount = 0;
            var status =  ske.CheckFeatureUse(featProfileType.SWToInt(),ref openCount, ref
                closedCount).CastObj<swSketchCheckFeatureStatus_e>();
            return new Tuple<swSketchCheckFeatureStatus_e, int, int>(status, openCount, closedCount);
        }

        public static object GetReferenceEntityEx(this ISketch ske, out swSelectType_e entityType)
        {
            int entityType_int=0;
            var obj = ske.GetReferenceEntity(ref entityType_int);
            entityType = entityType_int.CastObj<swSelectType_e>();
            return obj;
        }
    }

}

