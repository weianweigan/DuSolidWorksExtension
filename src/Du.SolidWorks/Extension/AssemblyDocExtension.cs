using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public  enum MateAlignExtension
    {
        /// <summary>
        /// 同向配合
        /// </summary>
        SameAlign,
        /// <summary>
        /// 反向配合
        /// </summary>
        AntiAlign
    }
    public static class AssemblyDocExtension
    {
        /// <summary>
        /// 面重合配合
        /// </summary>
        /// <typeparam name="TFaceOne"></typeparam>
        /// <typeparam name="TFaceTwo"></typeparam>
        /// <param name="doc"></param>
        /// <param name="faceOne"></param>
        /// <param name="faceTwo"></param>
        /// <param name="mateAlign"></param>
        /// <returns></returns>
        public static Mate2 AddFaceCoinMate<TFaceOne,TFaceTwo>(this IAssemblyDoc doc,TFaceOne faceOne,TFaceTwo faceTwo, MateAlignExtension mateAlign)
        {
            int mateError = 0;
            Mate2 swMate = default;

            SelectEntityOrFeature(faceOne, false);
            SelectEntityOrFeature(faceTwo, true);

            switch (mateAlign)
            {
                case MateAlignExtension.SameAlign:
                    swMate = doc.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignALIGNED, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                    break;
                case MateAlignExtension.AntiAlign:
                    swMate = doc.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignANTI_ALIGNED, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                    break;
                default:
                    break;
            }
            return swMate;
        }

        /// <summary>
        /// 面重合配合
        /// </summary>
        /// <typeparam name="TFaceOne"></typeparam>
        /// <typeparam name="TFaceTwo"></typeparam>
        /// <param name="doc"></param>
        /// <param name="faceOne"></param>
        /// <param name="faceTwo"></param>
        /// <param name="mateAlign"></param>
        /// <param name="dis">距离，正负代表方向</param>
        /// <returns></returns>
        public static Mate2 AddFaceDisMate<TFaceOne, TFaceTwo>(this IAssemblyDoc doc, TFaceOne faceOne, TFaceTwo faceTwo, MateAlignExtension mateAlign,double dis)
        {
            int mateError = 0;
            Mate2 swMate = default;

            SelectEntityOrFeature(faceOne, false);
            SelectEntityOrFeature(faceTwo, true);

            switch (mateAlign)
            {
                case MateAlignExtension.SameAlign:
                    if (dis > 0)
                    {
                        swMate = doc.AddMate5((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignALIGNED, false,dis,dis,dis, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                    }
                    else
                    {
                        swMate = doc.AddMate5((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignALIGNED, true, -dis,-dis,-dis, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                    }
                    break;
                case MateAlignExtension.AntiAlign:
                    if (dis > 0)
                    {
                        swMate = doc.AddMate5((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignANTI_ALIGNED, false, dis, dis, dis, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                    }
                    else
                    {
                        swMate = doc.AddMate5((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignANTI_ALIGNED, true, -dis, -dis, -dis, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                    }
                    break;
                default:
                    break;
            }
            return swMate;
        }


        public static Mate2 AddAxisCoinMate<TAxisOne, TAxisTwo>(this AssemblyDoc doc,TAxisOne axisOne, TAxisTwo compAxisTwo, MathUtility mathUtility = null,Component2 comp = null)
        {
            int mateError = 0;
            Mate2 swMate = default;

            SelectEntityOrFeature<TAxisOne>(axisOne,false);
            SelectEntityOrFeature<TAxisTwo>(compAxisTwo,true);

            swMate = doc.AddMate5((int)swMateType_e.swMateCOINCIDENT, AxisAlign(axisOne, compAxisTwo,mathUtility,comp).SWToInt(), false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);

            return swMate;
        }

        /// <summary>
        /// 判断两个轴是否同向
        /// </summary>
        /// <typeparam name="TAxisOne"></typeparam>
        /// <typeparam name="TAxisTwo"></typeparam>
        /// <param name="axisOne"></param>
        /// <param name="axisTwo"></param>
        /// <returns>
        /// 相同返回 swMateAnlignALIGNED
        /// 不同返回 swMateAlignANTI_ALIGNED
        /// 无法计算默认返回 swMateAlignALIGNED
        /// </returns>
        private static swMateAlign_e AxisAlign<TAxisOne, TAxisTwo>(TAxisOne axisOne, TAxisTwo axisTwo,MathUtility mathUtility = null,Component2 comp = null)
        {
            if ((typeof(TAxisOne).Name == nameof(IFeature) || typeof(TAxisOne).Name == nameof(Feature)) && 
                (typeof(TAxisTwo).Name == nameof(IFeature) || typeof(TAxisTwo).Name == nameof(Feature)))
            {

                var axisDataOne = axisOne.CastObj<IFeature>()?.GetSpecFeatData<RefAxis>();
                var axisDataTwo = axisTwo.CastObj<IFeature>()?.GetSpecFeatData<RefAxis>();
                if (axisOne != null && axisTwo != null)
                {
                    if (mathUtility == null)
                    {
                        return axisDataOne.GetEdge3().Delta._Vector.
                        IsSameDirection(axisDataTwo.GetEdge3().Delta._Vector) ?
                        swMateAlign_e.swMateAlignALIGNED : swMateAlign_e.swMateAlignANTI_ALIGNED;
                    }
                    else
                    {
                        return axisDataOne.GetEdge3().Delta._Vector.
                        IsSameDirection(axisDataTwo.GetEdge3().Delta._Vector.ToSwMathVector(mathUtility).IMultiplyTransform(comp.Transform2).ToVector3()) ?
                        swMateAlign_e.swMateAlignALIGNED : swMateAlign_e.swMateAlignANTI_ALIGNED;
                    }
                }
            }
            Debug.Print("无法确定轴方向");
            return swMateAlign_e.swMateAlignALIGNED;
        }

        /// <summary>
        /// 选择特征或者实体
        /// 支持的类型 IFace;Face;Entity;Feature;IFeature
        /// </summary>
        /// <typeparam name="TObj"></typeparam>
        /// <param name="face"></param>
        /// <param name="Append"></param>
        /// <param name="mark"></param>
        private static void SelectEntityOrFeature<TObj>(TObj face,bool Append,int mark = -1)
        {
            switch (typeof(TObj).Name)
            {
                case nameof(IFace):
                    ((Entity)((IFace)face)).Select2(Append, mark);
                    break;
                case nameof(Face):
                    ((Entity)((Face)face)).Select2(Append, mark);
                    break;
                case nameof(Entity):
                    ((Entity)face).Select2(Append, mark);
                    break;
                case nameof(Feature):
                    ((Feature)((Face)face)).Select2(Append, mark);
                    break;
                case nameof(IFeature):
                    ((IFeature)face).Select2(Append, mark);
                    break;
                default:
                    throw new Exception(nameof(face) + ":类型不存在");
            }
        }

        /// <summary>
        /// action for each of component in assemblydoc
        /// </summary>
        /// <param name="doc">assemblydoc instance</param>
        /// <param name="action">action you need todo</param>
        /// <param name="topOnly">toponly option</param>
        public static void UsingAllComponents(this  AssemblyDoc doc,Action<Component2> action,bool topOnly = false)
        {
            var comps = doc.GetComponents(topOnly) as Object[];
            foreach (Component2 comp in comps)
            {
                action(comp);
            }
        }
        
        /// <summary>
        /// action for specific type components
        /// </summary>
        /// <param name="doc">assemblydoc instance</param>
        /// <param name="action">action you need todo</param>
        /// <param name="documentTypes">the doc type whick define with type of <see cref="swDocumentTypes_e"/></param>
        /// <param name="topOnly"></param>
        public static void UsingAllComponents(this  AssemblyDoc doc,Action<Component2> action,swDocumentTypes_e documentTypes,bool topOnly = false)
        {
            var comps = doc.GetComponents(topOnly) as Object[];
            foreach (Component2 comp in comps)
            {
                if (comp.GetCompType() == documentTypes)
                {
                    action(comp);
                }
            }
        }
    }
}
