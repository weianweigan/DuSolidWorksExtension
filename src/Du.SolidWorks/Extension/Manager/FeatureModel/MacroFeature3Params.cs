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
    /// <summary>
    /// 宏特征参数类
    /// </summary>
    public class MacroFeature3Params
    {
        private MacroFeature3Params()
        {

        }

        /// <summary>
        /// 生成宏特征参数
        /// </summary>
        /// <typeparam name="TService">标注为ComVisble[]的回调函数类</typeparam>
        /// <typeparam name="TData">数据类</typeparam>
        /// <param name="baseName">特征名称</param>
        /// <param name="dataName">数据名称</param>
        /// <param name="data">数据</param>
        /// <param name="iconFiles">图标文件</param>
        /// <param name="options">选项</param>
        /// <returns></returns>
        public static MacroFeature3Params GetNormalParams<TService, TData>(string baseName, string dataName, TData data, Tuple<string, string, string> iconFiles, int options)
        {
             var result = new MacroFeature3Params()
            {
                BaseName = baseName,
                ProgId = typeof(TService).FullName,
                IconFiles = iconFiles,
                Options = options
            };
            result.ParamNamesAndValues.Add(dataName, JsonConvert.SerializeObject(data));
            return result;
        }

        /// <summary>
        /// 特征名字 像 拉伸1 草图1等
        /// </summary>
        public string BaseName { get; set; }

        /// <summary>
        /// 程序集全名称
        /// </summary>
        public string ProgId { get; set; }

        /// <summary>
        /// Null for Default;Vaild only for VBA
        /// </summary>
        public string MacroMethods { get; set; } = null;


        /// <summary>
        /// 参数名和参数值
        /// </summary>
        public Dictionary<string, string> ParamNamesAndValues { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// 尺寸类型,支持类型
        /// <see cref="swDimensionType_e.swAngularDimension"/>
        /// <see cref="swDimensionType_e.swLinearDimension"/>
        /// <see cref="swDimensionType_e.swRadialDimension"/>
        /// </summary>
        public Dictionary<swDimensionType_e, double> DimTypesAndCountValues { get; set; } = new Dictionary<swDimensionType_e, double>();


        public IBody2[] EditBodies { get; set; } = null;

        /// <summary>
        /// 图标按钮位置
        /// Images must be Windows bitmap (*.bmp) format and be 16 pixels wide X 18 pixels high
        ///Either the full pathname or only the filename can be used; for example, c:\bitmaps\icon1.bmp or icon1.bmp.
        ///Item1: regular icon
        ///Item2: suppressed icon 
        ///Item3: highlighted icon
        /// </summary>
        public Tuple<string,string,string> IconFiles { get; set; }

        public int Options { get; set; } = (int)swMacroFeatureOptions_e.swMacroFeatureByDefault;
    }
}
