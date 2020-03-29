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
    /// FeatureManagerExtension 扩展类
    /// </summary>
    public static class FeatureManagerExtension
    {
        /// <summary>
        /// 插入宏特征
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="featMgr">特征管理器</param>
        /// <param name="featureName">特征名</param>
        /// <param name="parameterNames">参数名</param>
        /// <param name="parameterTypes">特征类型</param>
        /// <param name="parameterValues">特征值</param>
        /// <param name="opts">选项</param>
        /// <param name="editBodies">编辑实体</param>
        /// <returns></returns>
        private static Feature InsertMacroFeature<T>(
            this IFeatureManager featMgr, string featureName, 
            string[] parameterNames, int[] parameterTypes,
            string[] parameterValues, 
            swMacroFeatureOptions_e opts, IBody2[] editBodies = null)
        {
            var macroFeature = featMgr.InsertMacroFeature3(
                featureName,
                typeof(T).FullName,
                null,
                parameterNames,
                parameterTypes,
                parameterValues,
                null,
                null,
                editBodies,
                null,
                (int)opts);

            if (macroFeature == null)
            {
                #if DEBUG
                var message = GetFeatureInsertError(typeof(T), parameterNames, parameterTypes, parameterValues);
                #else
                var message = "";
                #endif
                throw new Exception($"Unable to create feature {typeof(T).FullName}. {message}");
            }
            return macroFeature;
        }

        /// <summary>
        /// 检查宏特征参数是否错误
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameterNames"></param>
        /// <param name="parameterTypes"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public static object GetFeatureInsertError(Type type, string[] parameterNames, 
            int[] parameterTypes, string[] parameterValues)
        {
            if (type.GetConstructor(Type.EmptyTypes) == null)
            {
                return $"Type {type.FullName} doesn't have a public, parameterless constructor.";
            }
            if (parameterNames.Length != parameterTypes.Length || parameterNames.Length != parameterValues.Length)
            {
                var parameterNamesString = string.Join(", ", parameterNames);
                var parameterTypesString = string.Join(", ", parameterTypes);
                var parameterValuesString = string.Join(", ", parameterValues);
                return $"`parameterNames`() {parameterNamesString}, `parameterTypes`({parameterTypesString}) and " +
                       $"`parameterValues`({parameterValuesString}) don't have the same length.";
            }
            return $"Unknown reason. Ensure {type.FullName} lives in the addin dll. If you can fix it and there was another reason, please add a hint in {typeof(FeatureManagerExtension).FullName}::{nameof(GetFeatureInsertError)}.";
        }


        /// <summary>
        /// 插入宏特征扩展方法
        /// </summary>
        /// <typeparam name="TFeature"></typeparam>
        /// <param name="featMgr"></param>
        /// <param name="featureName"></param>
        /// <param name="opts"></param>
        /// <param name="data"></param>
        /// <param name="editBodies"></param>
        /// <returns></returns>
        public static Feature InsertMacroFeature<TFeature>
            (this IFeatureManager featMgr, string featureName, 
            swMacroFeatureOptions_e opts, object data, IBody2[] editBodies)
        {
            return featMgr.InsertMacroFeature<TFeature>(featureName,
                IMacroFeatureDataExtension.GetFeatureDataNames(),
                IMacroFeatureDataExtension.GetFeatureDataTypes(),
                IMacroFeatureDataExtension.GetFeatureDataValues(data),
                opts,
                editBodies);
        }

        /// <summary>
        /// 获取所有特征
        /// </summary>
        /// <param name="featMgr"><see cref="IFeatureManager"/></param>
        /// <param name="TopOnly">是否只获取顶级特征</param>
        /// <returns>特征</returns>
        public static IEnumerable<IFeature> GetFeaturesEx(this IFeatureManager featMgr,bool TopOnly = false)
        {
            var objarray = featMgr.GetFeatures(TopOnly) as object[];
            var feats = objarray.Cast<IFeature>();
            if (feats != null)
            {
                return feats;
            }
            else
            {
                throw new FeatMgrFeatsNotFoundException("无特征集返回");
            }
        }

        /// <summary>
        /// 插入基准面
        /// <para> Mark: 0 = First reference entity </para>
        /// <para> Mark: 1 = Second reference entity </para>
        /// <para> Mark: 2 = Third reference entity </para>
        /// </summary>
        /// <param name="featMgr"><see cref="IFeatureManager"/> Interface</param>
        /// <param name="firstRef">第一个参考</param>
        /// <param name="secondRef">第二个参考</param>
        /// <param name="thirdRef">第三个参考</param>
        /// <returns>调用此方法前,需要将参考对象提前选取 
        /// Mark: 0 = First reference entity 
        /// Mark: 1 = Second reference entity 
        /// Mark: 2 = Third reference entity
        /// </returns>
        public static IFeature InsertRefPlaneEx(this IFeatureManager featMgr,KeyValuePair<swRefPlaneReferenceConstraints_e,double> firstRef, KeyValuePair<swRefPlaneReferenceConstraints_e, double> secondRef, KeyValuePair<swRefPlaneReferenceConstraints_e, double> thirdRef)
        {
            return featMgr.InsertRefPlane(firstRef.Key.SWToInt(), firstRef.Value, secondRef.Key.SWToInt(), secondRef.Value, thirdRef.Key.SWToInt(), thirdRef.Value) as IFeature;
        }

        /// <summary>
        /// 插入基准面
        /// <para> Mark: 0 = First reference entity </para>
        /// <para> Mark: 1 = Second reference entity </para>
        /// <para> Mark: 2 = Third reference entity </para>
        /// </summary>
        /// <param name="featMgr"><see cref="IFeatureManager"/> Interface</param>
        /// <param name="firstRef">第一个参考</param>
        /// <param name="secondRef">第二个参考</param>
        /// <param name="thirdRef">第三个参考</param>
        /// <returns>调用此方法前,需要将参考对象提前选取 
        /// Mark: 0 = First reference entity 
        /// Mark: 1 = Second reference entity 
        /// Mark: 2 = Third reference entity
        /// </returns>
        public static IFeature InsertRefPlaneEx(this IFeatureManager featMgr, KeyValuePair<int, double> firstRef, KeyValuePair<int, double> secondRef, KeyValuePair<int, double> thirdRef)
        {
            return featMgr.InsertRefPlane(firstRef.Key.SWToInt(), firstRef.Value, secondRef.Key.SWToInt(), secondRef.Value, thirdRef.Key.SWToInt(), thirdRef.Value) as IFeature;
        }

        /// <summary>
        /// 插入扫描特征
        /// 需要提前选中轮廓路径 和 引导线(如果有的话)
        /// <para>轮廓Mark: <see cref="ProtrusionSwept3Params.ProfileSelectionMark"/> = 1</para>
        /// <para>路径Mark: <see cref="ProtrusionSwept3Params.SweepPathMark"/> = 4</para> 
        /// <para> 引导线Mark: <see cref="ProtrusionSwept3Params.GuideCurveMark"/> = 2</para>
        /// <para>应先选中草图轮廓,再选中路径</para>
        /// </summary>
        /// <param name="featMgr">IFeatureManager Interface</param>
        /// <param name="params">扫描参数 <see cref="ProtrusionSwept3Params"/></param>
        /// <returns></returns>
        public static IFeature InsertProtrusionSwept3Ex(this IFeatureManager featMgr, ProtrusionSwept3Params @params)
        {
            return featMgr.InsertProtrusionSwept3(@params.Propagate, @params.Alignment, @params.TwistCtrlOption.SWToShort(), @params.KeepTangency, @params.BAdvancedSmoothing, @params.StartMatchingType.SWToShort(), @params.EndMatchingType.SWToShort(), @params.IsThinBody, @params.Thickness1, @params.Thickness2, @params.ThinType.SWToShort(), @params.PathAlign, @params.Merge, @params.UseFeatScope, @params.UseAutoSelect, @params.TwistAngle, @params.BMergeSmoothFaces);
        }
    }
    public class FeatMgrFeatsNotFoundException:Exception
    {
        public FeatMgrFeatsNotFoundException(string Message):base(Message)
        {

        }
    }
}
