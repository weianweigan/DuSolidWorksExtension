using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class IFeatureExtension
    {
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

        public static T GetSpecFeatData<T>(this IFeature feature)
        {
#if DEBUG
            Debug.Print(typeof(T).Name + " => (通过GetSpecificFeature2())" + feature.GetTypeName2());
#else

#endif
            return (T)feature.GetSpecificFeature2();
        }

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

    }
}
