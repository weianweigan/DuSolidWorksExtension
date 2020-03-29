using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public class ProtrusionSwept3Params
    {
        private ProtrusionSwept3Params()
        {
        }

        /// <summary>
        /// 使用默认参数,需要选择扫描轮廓草图和扫描路径
        /// </summary>
        /// <returns></returns>
        public static ProtrusionSwept3Params GetNormalProtrusionSwept3Params()
        {
            return new ProtrusionSwept3Params();
        }

        /// <summary>
        /// 生成默认参数的薄壁扫描参数
        /// </summary>
        /// <param name="thinType"></param>
        /// <param name="thickness1"></param>
        /// <param name="thinkness2"></param>
        /// <returns></returns>
        public static ProtrusionSwept3Params GetThinProtrusionSwept3Params(swThinWallType_e thinType,double thickness1,double thinkness2)
        {
            return new ProtrusionSwept3Params()
            {
                IsThinBody = true,
                ThinType = thinType
            ,
                Thickness1 = thickness1
            ,
                Thickness2 = thinkness2
            };
        }

        #region SelectionMark

        /// <summary>
        /// 草图轮廓标记
        /// </summary>
        public const int ProfileSelectionMark = 1;

        /// <summary>
        /// 引导线标记
        /// </summary>
        public const int GuideCurveMark = 2;

        /// <summary>
        /// 扫描路径标记
        /// </summary>
        public const int SweepPathMark = 4;

        #endregion

        /// <summary>
        /// 是否传播到下一个相切边
        /// </summary>
        /// <remarks>默认为False True propagates the swept protrusion to the next tangent edge, false does not</remarks>
        public bool Propagate { get; set; } = false;

        /// <summary>
        /// 是否与结束端面对其
        /// </summary>
        /// <remarks>
        /// True causes the swept protrusion to go through the end faces if the curve used for the sweep goes from one face to another or from one edge to another, false causes the swept protrusion to begin and end perpendicular to the sweep curve and it cannot break through the two end faces of the body
        /// </remarks>
        public bool Alignment { get; set; } = false;

        /// <summary>
        /// 轮廓方位和轮廓扭转参数
        /// </summary>
        /// <remarks>Twist control options as defined by <see cref="swTwistControlType_e"/> </remarks>
        public swTwistControlType_e TwistCtrlOption { get; set; } = swTwistControlType_e.swTwistControlConstantTwistAlongPath;
        
        /// <summary>
        /// 是否合并切面
        /// </summary>
        /// <remarks>If the sweep section has tangent segments, then True to cause the corresponding surfaces in the resulting sweep to be tangent, false to not</remarks>
        public bool KeepTangency { get; set; } = false;

        /// <summary>
        /// 是否合并光滑的面
        /// </summary>
        public bool BAdvancedSmoothing { get; set; } = true;

        /// <summary>
        /// 起始处相切类型,默认为None
        /// </summary>
        /// <remarks>
        /// Tangency type as defined by <see cref="swTangencyType_e"/>
        /// </remarks>
        public swTangencyType_e StartMatchingType { get; set; } = swTangencyType_e.swTangencyNone;

        /// <summary>
        /// 结束处相切类型
        /// </summary>
        /// <remarks>
        /// Tangency type as defined by <see cref="swTangencyType_e"/>
        /// </remarks>
        public swTangencyType_e EndMatchingType { get; set; } = swTangencyType_e.swTangencyNone;

        #region 薄壁特征

        /// <summary>
        /// 是否是薄壁特征
        /// </summary>
        /// <remarks>
        /// True if this feature is a thin body, false if not
        /// </remarks>
        public bool IsThinBody { get; set; } = false;

        /// <summary>
        /// 方向1的厚度
        /// </summary>
        public double Thickness1 { get; set; } 

        /// <summary>
        /// 方向2的厚度
        /// </summary>
        public double Thickness2 { get; set; }

        /// <summary>
        /// 薄壁特征类型
        /// </summary>
        public swThinWallType_e ThinType { get; set; } = swThinWallType_e.swThinWallOneDirection;

        #endregion

        /// <summary>
        /// The PathAlign argument is available when TwistCtrlOption is set to 0 (follow path) and can take one of these values:
        /// 0 = None; no correction (default)
        ///2 = Direction vector; a plane, planar face, or line defines the path
        ///3 = All faces; includes neighboring faces
        /// </summary>
        public short PathAlign { get; set; } = 0;

        /// <summary>
        /// 是否合并实体
        /// </summary>
        public bool Merge { get; set; } = true;

        /// <summary>
        /// True if the feature only affects selected bodies, false if the feature affects all 默认为false
        /// </summary>
        public bool UseFeatScope { get; set; } = false;

        /// <summary>
        /// True to automatically select all bodies and have the feature affect those bodies,false to select the bodies the feature affects(see Remarks)
        /// </summary>
        /// <remarks>When UseAutoSelect is false, the user must select the bodies that the feature will affect.</remarks>
        public bool UseAutoSelect { get; set; } = true;

        /// <summary>
        /// If <see cref="TwistCtrlOption"/>  set to swTwistControlConstantTwistAlongPath, then specify end twist angle 当<see cref="TwistCtrlOption"/>设置为<see cref="swTwistControlType_e.swTwistControlConstantTwistAlongPath"/> 时设置扭转角度
        /// </summary>
        public double TwistAngle { get; set; }

        /// <summary>
        /// True to merge smooth faces, false to not 是否合并光滑面
        /// </summary>
        public bool BMergeSmoothFaces { get; set; }
    }
}
