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


    }
    public class FeatMgrFeatsNotFoundException:Exception
    {
        public FeatMgrFeatsNotFoundException(string Message):base(Message)
        {

        }
    }
}
