using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swpublished;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// 宏特征状态
    /// </summary>
    public enum StateEnum { Insert, Edit }

    /// <summary>
    /// 宏特征基类
    /// </summary>
    /// <typeparam name="TMacroFeature"></typeparam>
    /// <typeparam name="TData"></typeparam>
    [ComVisible(false)]
    public abstract class DuMacroFeatBase<TMacroFeature, TData>: ISwComFeature
        where TData:new()
        where TMacroFeature : DuMacroFeatBase<TMacroFeature, TData>
    {
        /// <summary>
        /// 私有变量,防止GC Collect
        /// </summary>
        protected IPropertyManagerPage2 _EditPage;

        public abstract string FeatureName { get; }

        protected abstract swMacroFeatureOptions_e FeatureOptions { get; }

        public abstract IPropertyManagerPage2 GetPropertyPage(object app, object modelDoc, object feature);

        public abstract bool RebuildNotify(object app, object modelDoc, object feature);

        public abstract bool EditFeat(object app, object modelDoc, object feature);
        #region ISwComFeature

        public object Edit(object app, object modelDoc, object feature)
        {
            return EditFeat(app, modelDoc, feature);
            
        }

        public object Regenerate(object app, object modelDoc, object feature)
        {
            return RebuildNotify(app, modelDoc, feature);
        }

        public object Security(object app, object modelDoc, object feature)
        {
            return true;
        }

        #endregion
    }
}
