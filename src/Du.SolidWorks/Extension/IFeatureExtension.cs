using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{

    /// <summary>
    /// Extension Methods for <see cref="IFeature"/>
    /// </summary>
    public static class IFeatureExtension
    {
        /// <summary>
        /// 获取特定的特征数据 (*Feat)Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static T GetDefinition<T>(this IFeature feature)
        {
#if DEBUG
            Debug.Print(typeof(T).Name+ " => (通过GetDefinition())"+ feature.GetTypeName2());
#else

#endif
            if (!typeof(T).Name.Contains("Data"))
            {
                throw new Exception(string.Format("请确认您的类型{0}是否正确,包含Data的接口需要使用GetSpecFeatData<T>方法", typeof(T).Name));
            }

            return (T)feature.GetDefinition();
        }

        /// <summary>
        /// 获取特定类型的对象 <see cref="ISketch"/> <see cref="IRefPlane"/> etc...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static T GetSpecFeat<T>(this IFeature feature)
        {
#if DEBUG
            Debug.Print(typeof(T).Name + " => (通过GetSpecificFeature2())" + feature.GetTypeName2());
#else

#endif
            return (T)feature.GetSpecificFeature2();
        }

        /// <summary>
        /// 获取所有尺寸
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static List<DisplayDimension> GetDisplayDimensions(this IFeature feature)
        {
            List<DisplayDimension> dimensions = new List<DisplayDimension>();

            var dimension = feature.GetFirstDisplayDimension().CastObj<DisplayDimension>();

            while (dimension != null)
            {
                dimensions.Add(dimension);
                var nextdim = feature.GetNextDisplayDimension(dimension).CastObj<DisplayDimension>();
                dimension = nextdim;
            }

            return dimensions;
        }

        /// <summary>
        /// 通过名字调用 SelectByID2
        /// </summary>
        /// <param name="feat"></param>
        /// <param name="extension"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="append"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public static bool SelectByName(this IFeature feat,IModelDocExtension extension,string name,string type,bool append,int mark = 0)
        {
            return extension.SelectByID2(name, type, 0, 0, 0, append, mark, null, 0);
        }
    }
}
