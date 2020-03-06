using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public abstract class MacroFeatureDataExtension<TFeatComSerivce,TData>
    {
        /// <summary>
        /// 特征名
        /// </summary>
        public abstract string FeatName { get; set; }
        
        /// <summary>
        /// 宏特征服务名字
        /// </summary>
        public string ServicePath { get {return typeof(TFeatComSerivce).FullName; } }

        /// <summary>
        /// 需要保存的数据
        /// </summary>
        public TData Data { get; set; }
    }
}
