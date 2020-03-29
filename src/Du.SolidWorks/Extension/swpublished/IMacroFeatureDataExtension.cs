using Newtonsoft.Json;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class IMacroFeatureDataExtension
    {
        private const string FeatureDataKey = "Main";

        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="macroFeatureData"></param>
        /// <returns></returns>
        public static T Read<T>(this IMacroFeatureData macroFeatureData)
        {
            string data;
            macroFeatureData.GetStringByName(FeatureDataKey, out data);
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// 将数据序列化到宏特征上
        /// </summary>
        /// <param name="macroFeatureData"></param>
        /// <param name="data"></param>
        public static void Write(this IMacroFeatureData macroFeatureData, object data)
        {
            macroFeatureData.SetStringByName(FeatureDataKey, JsonConvert.SerializeObject(data));
        }


        /// <summary>
        /// 获取选中对象,通过类型和标记
        /// Return selected objects filtered by type and mark.
        /// </summary>
        /// <param name="selMgr"></param>
        /// <param name="filter">(type,mark)=>bool</param>
        /// <returns></returns>
        public static IEnumerable<object> GetSelectedObjects(this IMacroFeatureData selMgr, Func<swSelectType_e, int, bool> filter)
        {
            {
                object objects;
                object objectTypes;
                object marks;
                object drViews;
                object componentXForms;
                selMgr.GetSelections3(out objects, out objectTypes, out marks, out drViews, out componentXForms);

                var objectsArray = (object[])objects;
                var typesArray = (swSelectType_e[])objectTypes;
                var marksArray = (int[])marks;

                var i = 0;
                foreach (var item in objectsArray)
                {
                    if (filter(typesArray[i], marksArray[i]))
                    {
                        yield return item;
                    }
                    i++;
                }

            }
        }


        /// <summary>
        /// 获取特征数据名
        /// </summary>
        /// <returns></returns>
        public static string[] GetFeatureDataNames() => new[] { FeatureDataKey };
        /// <summary>
        /// 特征数据类型
        /// </summary>
        /// <returns></returns>
        public static int[] GetFeatureDataTypes() => new[] { (int)swMacroFeatureParamType_e.swMacroFeatureParamTypeString };
        /// <summary>
        /// 特征数据值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[] GetFeatureDataValues(object data) => new[] { JsonConvert.SerializeObject(data) };

    }
}
