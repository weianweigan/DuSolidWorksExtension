using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Du.SolidWorks.Extension;
using Du.SolidWorks.Math;
using SolidWorks.Interop.swconst;
using System.IO;

namespace Du.SolidWorks.Extension
{
    public static class Component2Extension
    {
        public static Vector3 GetPostion(this Component2 comp)
        {
            return comp.Transform2.GetPostion();
        }

        /// <summary>
        /// 获取组件的子组件
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static IEnumerable<Component2> GetChildrenEx(this Component2 comp)
        {
            return comp.GetChildren().CastObj<object[]>().Cast<Component2>();
        }

        public static void UsingAllBody(this Component2 comp,Action<Body2> action,swBodyType_e bodyType_E,bool VisbleOnly = false)
        {
            var bodys = comp.GetBodies2((int)bodyType_E) as Body2[];

            foreach (var item in bodys)
            {
                if (VisbleOnly && item.Visible)
                {
                    action(item);
                }
                else
                {
                    action(item);
                }
            }
        }

        public static void UsingChildren(this Component2 comp,Action<Component2> action)
        {
            var child = comp.GetChildren() as Component2[];
            if (child != null)
            {
                foreach (var item in child)
                {
                    action(item);
                }
            }
        }

        public static void UsingCompModelDoc2(this Component2 comp,Action<ModelDoc2> action)
        {
            ModelDoc2 swModel = comp.GetModelDoc2() as ModelDoc2;
            if (swModel != null)
            {
                action(swModel);
            }
        }

        /// <summary>
        /// 获取组件顶层的所有特征
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static IList<Feature> GetCompTopFeatures(this Component2 comp)
        {
            List<Feature> features = new List<Feature>();
            var feat = comp.FirstFeature();
            while (feat != null)
            {
                features.Add(feat);
                feat = feat.GetNextFeature() as Feature;
            }
            return features;
        }
        
        /// <summary>
        /// 用类型名过滤特征--只获取第一级特征
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public static IList<Feature> GetCompTopFeaturesWithTypeName(this Component2 comp,string TypeName)
        {
            List<Feature> features = new List<Feature>();
            var feat = comp.FirstFeature();
            while (feat != null)
            {
                if (feat.GetTypeName2() == TypeName)
                {
                    features.Add(feat);
                }
                feat = feat.GetNextFeature() as Feature;
            }
            return features;
        }

        /// <summary>
        /// 获取特定的特征
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public static IList<Feature> TakeCompTopFeaturesWhile(this Component2 comp, Func<Feature,bool> func)
        {
            List<Feature> features = new List<Feature>();
            var feat = comp.FirstFeature();
            while (feat != null)
            {
                if (func != null && func(feat))
                {
                    features.Add(feat);
                }
                feat = feat.GetNextFeature() as Feature;
            }
            return features;
        }
    
        /// <summary>
        /// 直接返回转换类型的ModelDoc对象
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static ModelDoc2 GetModelDoc2Ex(this Component2 comp)
        {
            try
            {
                return comp.GetModelDoc2() as ModelDoc2;
            }
            catch (NullReferenceException)
            {
                throw;
            }
        }

        /// <summary>
        /// 编辑零部件,自动区分装配体还是零件
        /// </summary>
        /// <param name="comp">组件</param>
        /// <param name="doc">顶级文档</param>
        /// <param name="silent">静默编辑--提高速度</param>
        /// <param name="readOnly">只读</param>
        /// <returns></returns>
        public static swEditPartCommandStatus_e Edit(this Component2 comp,AssemblyDoc doc,bool silent = true,bool readOnly = false)
        {
            var type = comp.GetCompType();
            int info = -1;
            switch (type)
            {
                case swDocumentTypes_e.swDocPART:
                    if (comp.Select2(false, -1)) doc.EditPart2(silent, readOnly, ref info);
                    break;
                case swDocumentTypes_e.swDocASSEMBLY:
                    if (comp.Select2(false, -1)) doc.EditAssembly();
                    info = 0;
                    break;
                default:
                    throw new FileFormatException(string.Format("can not edit component width type of:{0} ", type.ToString()));
            }
            swEditPartCommandStatus_e state = (swEditPartCommandStatus_e)info;
            return state;
        }

        /// <summary>
        /// 获取组件的类型
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static swDocumentTypes_e GetCompType(this Component2 comp)
        {
            string extension = Path.GetExtension(comp.GetPathName());
            switch (extension.ToLower())
            {
                case ".sldprt":
                    return swDocumentTypes_e.swDocPART;
                case ".sldasm":
                    return swDocumentTypes_e.swDocASSEMBLY;
                default:
                    throw new FileFormatException(string.Format("can not get :{0} 's type,The component's filepath is {1}", comp.Name2,comp.GetPathName()));
            }
        }

        /// <summary>
        /// 设置是否固定
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="doc">文档</param>
        /// <param name="IsFix">true为固定,false为浮动</param>
        public static void SetFixStatus(this Component2 comp,AssemblyDoc doc,bool IsFix)
        {
            if (IsFix)
            {
                if (comp.IsFixed())
                {
                    return;
                }
                else
                {
                    comp.SelectFor(() => doc.FixComponent());
                }
            }
            else
            {
                if (comp.IsFixed())
                {
                    comp.SelectFor(() => doc.UnfixComponent());
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 选择后执行特定动作
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="action"></param>
        /// <param name="AfterDeSelect">执行完动作是否清除选择状态</param>
        public static void SelectFor(this Component2 comp, Action<Component2> action, bool AfterDeSelect = true)
        {
            comp.Select2(false, -1);
            action?.Invoke(comp);
            if (AfterDeSelect) comp.DeSelect();
        }

        /// <summary>
        /// 选择后执行特定动作
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="action"></param>
        /// <param name="AfterDeSelect">执行完动作是否清除选择状态</param>
        public static void SelectFor(this Component2 comp, Action action, bool AfterDeSelect = true)
        {
            comp.Select2(false, -1);
            action?.Invoke();
            if (AfterDeSelect) comp.DeSelect();
        }
    }
}
