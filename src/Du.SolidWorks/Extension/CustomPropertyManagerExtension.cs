using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class CustomPropertyManagerExtension
    {
        /// <summary>
        /// 获取所有属性
        /// </summary>
        /// <param name="cusManager"></param>
        /// <returns></returns>
        public static List<CustomPropertyModel> GetAllProperty(this CustomPropertyManager cusManager)
        {
            List<CustomPropertyModel> models = new List<CustomPropertyModel>();
            object PropNames = default, PropTypes = default, PropValues = default, Resolved = default;
            int count = cusManager.GetAll2(ref PropNames,ref PropTypes,ref PropValues,ref Resolved);
            for (int i = 0; i < count; i++)
            {
                models.Add(new CustomPropertyModel(
                    name: ((object[])PropNames)[i] as string,
                    swCustomInfoType:(swCustomInfoType_e)((object[])PropTypes)[i] ,
                    value: ((object[])PropValues)[i] as string,
                    evalValue: ((object[])Resolved)[i] as string
                    ));
            }
            return models;
        }
        /// <summary>
        /// 获取单个属性
        /// </summary>
        /// <param name="cusManager"></param>
        /// <param name="Name"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public static CustomPropertyModel GetProperty(this CustomPropertyManager cusManager,string Name,bool useCache = true)
        {
            string value = string.Empty, reslveValue = string.Empty;
            bool wasResloved;
            cusManager.Get5(Name, useCache, out value, out reslveValue,out wasResloved);
            if (string.IsNullOrEmpty(value))
            {
                return new CustomPropertyModel(Name, swCustomInfoType_e.swCustomInfoUnknown, value, reslveValue)
                {
                    WasResloved = wasResloved
                };
            }
            else
            {
                return null;
            }
        }
    
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="cusManager"></param>
        /// <param name="customPropertyModel"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static swCustomInfoAddResult_e AddProperty(this CustomPropertyManager cusManager,CustomPropertyModel customPropertyModel, swCustomPropertyAddOption_e option = swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd)
        {
           return  (swCustomInfoAddResult_e)cusManager.
                Add3(customPropertyModel.Name,
                    (int)customPropertyModel.SwCustomInfoType,
                    customPropertyModel.Value ?? customPropertyModel.EvalValue, 
                    (int)option
                );
        }

        /// <summary>
        /// 批量添加属性
        /// </summary>
        /// <param name="cusManager"></param>
        /// <param name="Models"></param>
        /// <param name="option"></param>
        public static void AddProperty(this CustomPropertyManager cusManager, List<CustomPropertyModel> Models, swCustomPropertyAddOption_e option = swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd)
        {
            foreach (var customPropertyModel in Models)
            {
                var result = (swCustomInfoAddResult_e)cusManager.
                 Add3(customPropertyModel.Name,
                     (int)customPropertyModel.SwCustomInfoType,
                     customPropertyModel.Value ?? customPropertyModel.EvalValue,
                     (int)option
                 );
                if (result != swCustomInfoAddResult_e.swCustomInfoAddResult_AddedOrChanged)
                {
                    throw new Exception("添加属性失败,"+customPropertyModel.Name+":" + result.ToString());
                }
            }
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="customProperty"></param>
        /// <param name="Name">属性名</param>
        /// <param name="Value">属性值</param>
        /// <returns></returns>
        public static swCustomInfoSetResult_e SetProperty(this CustomPropertyManager customProperty ,string Name,string Value)
        {
            return (swCustomInfoSetResult_e)customProperty.Set2(Name, Value);
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="customProperty"></param>
        /// <param name="models">属性列表(属性必须存在)</param>
        public static void SetProperty(this CustomPropertyManager customProperty, List<CustomPropertyModel> models)
        {
            try
            {
                foreach (var item in models)
                {
                    var result = customProperty.SetProperty(item.Name, item.Value ?? item.EvalValue);
                    if (result != swCustomInfoSetResult_e.swCustomInfoSetResult_OK)
                    {
                        throw new Exception("设置属性错误:" + result.ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class CustomPropertyModel
    {
        public CustomPropertyModel(string name, swCustomInfoType_e swCustomInfoType, string value, string evalValue)
        {
            Name = name;
            SwCustomInfoType = swCustomInfoType;
            Value = value;
            EvalValue = evalValue;
            
        }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public swCustomInfoType_e SwCustomInfoType { get; set; }
        /// <summary>
        /// 原始值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 求解后的值
        /// </summary>
        public string EvalValue { get; set; }
        /// <summary>
        /// 是否被求解
        /// </summary>
        public bool WasResloved { get; set; } = true;
    }
}
