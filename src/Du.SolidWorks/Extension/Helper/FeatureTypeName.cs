using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public class FeatureTypeName
    {

        #region Assembly
        /// <summary>
        /// 在位配合
        /// interface: IMate
        /// </summary>
        public const string MateInPlane = "MateInPlace";

        /// <summary>
        /// 爆炸线草图特征
        /// interface: ISketch
        /// </summary>
        public const string ExplodeLineProfileFeature = "ExplodeLineProfileFeature";

        /// <summary>
        /// 上下文特征
        /// interface: IFeature
        /// </summary>
        public const string InContextFeatHolder = "InContextFeatHolder";

        /// <summary>
        /// 重合配合
        /// interface: IMate
        /// </summary>
        public const string MateCoincident = "MateCoincident";

        /// <summary>
        /// 组件特征
        /// interface:IComponent2
        /// </summary>
        public const string Component2 = "Reference";

        /// <summary>
        /// 配合文件夹
        /// </summary>
        public const string MateGroup = "MateGroup";
        #endregion

        //TODO
        #region Body

        #endregion

        #region Reference Geometry
        /// <summary>
        /// 基准面
        /// interface: IRefPlane
        /// </summary>
        public const string RefPlane = "RefPlane";

        /// <summary>
        /// 基准轴
        /// interface: IRefAxis
        /// </summary>
        public const string RefAxis = "RefAxis";

        /// <summary>
        /// 参考坐标系
        /// interface: ICoordinateSystemFeatureData
        /// </summary>
        public const string CoordSys = "CoordSys";

        /// <summary>
        /// 原点坐标系
        /// interface: None
        /// </summary>
        public const string OrignSys = "OriginProfileFeature";

        /// <summary>
        /// 宏特征
        /// interface: IMacroFeatureData
        /// </summary>
        public const string MacroFeature = "MacroFeature";
        #endregion

        #region Drawing

        #endregion

        #region Folder

        #endregion

        #region Miscellaneous

        #endregion

        #region Mold

        #endregion

        #region Motion and Simulation

        #endregion

        #region Scenes,Lights,and Cameras

        #endregion

        #region Sheet Metal

        /// <summary>
        /// 基本法兰特征 <see cref="IBaseFlangeFeatureData"/>
        /// </summary>
        public const string SMBaseFlange = nameof(SMBaseFlange);

        /// <summary>
        /// <see cref="IBreakCornerFeatureData"/>
        /// </summary>
        public const string BreakCorner = nameof(BreakCorner);

        /// <summary>
        /// <see cref="IBreakCornerFeatureData"/>
        /// </summary>
        public const string CornerTrim = nameof(CornerTrim);

        /// <summary>
        /// <see cref="ICrossBreakFeatureData"/>
        /// </summary>
        public const string CrossBreak = nameof(CrossBreak);

        /// <summary>
        /// <see cref="IFlatPatternFeatureData"/>
        /// </summary>
        public const string EdgeFlange = nameof(EdgeFlange);

        /// <summary>
        /// <see cref="IFlatPatternFeatureData"/>
        /// </summary>
        public const string FlatPattern = nameof(FlatPattern);

        /// <summary>
        /// <see cref="IBendsFeatureData"/>
        /// </summary>
        public const string FlattenBends = nameof(FlattenBends);

        #endregion

        #region Sketch

        /// <summary>
        /// 草图特征
        /// interface ISketch
        /// </summary>
        public const string Sketch = "ProfileFeature";

        /// <summary>
        /// 草图特征
        /// interface ISketch
        /// </summary>
        public const string ProfileFeature = nameof(ProfileFeature);

        /// <summary>
        /// 草图点 ,用在选中原点时
        /// </summary>
        public const string EXTSKETCHPOINT =nameof(EXTSKETCHPOINT) ;

        #endregion

        #region Surface

        #endregion

        #region Weldment

        #endregion

    }


}
