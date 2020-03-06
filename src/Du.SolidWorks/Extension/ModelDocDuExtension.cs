using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class ModelDocDuExtension
    {
        /// <summary>
        /// 执行操作然后关闭文档
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="sldWorks"></param>
        /// <param name="run"></param>
        public static void Using(this IModelDoc2 doc, ISldWorks sldWorks, Action<IModelDoc2> run)
        {
            run.Invoke(doc);
            sldWorks.CloseDoc(doc.GetTitle());
        }

        public static Component2 CurrentComp(this IModelDoc2 doc)
        {
            //Part.Extension.SelectByID2("装配体2.SLDASM", "COMPONENT", 0, 0, 0, False, 0, Nothing, 0)
            bool isSelec = doc.Extension.SelectByID2(doc.GetTitle(), "COMPONENT", 0, 0, 0, false, 0, null, 0);
            return isSelec ?
            doc.GetSelectedObjectsFromModel().FirstOrDefault()?.CastObj<Component2>() : null;
        }

        /// <summary>
        /// 获取所有的基准面
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static IEnumerable<IRefPlane> GetPlanes(this IModelDoc2 doc)
        {
            var feats = doc.FeatureManager
                .GetFeatures(false)
                 as IFeature[];
            return feats.Select(f => f.GetSpecificFeature2() as IRefPlane);
        }

        /// <summary>
        /// 获取所有的某种类型的特征
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static IEnumerable<IFeature> GetFeats(this IModelDoc2 doc)
        {
            var feats = doc.FeatureManager
                .GetFeatures(false)
                  as IFeature[];
            return feats;
        }

        /// <summary>
        /// 获取文档类型
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static swDocumentTypes_e GetTypeEx(this IModelDoc2 doc)
        {
            return (swDocumentTypes_e)doc.GetType();
        }

        /// <summary>
        /// 转换为零件文档
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static PartDoc AsPartDoc(this IModelDoc2 doc)
        {
            if (doc.GetTypeEx() != swDocumentTypes_e.swDocPART)
            {
                throw new Exception("错误的类型,无法转换为PartDoc");
            }
            return (PartDoc)doc;
        }

        /// <summary>
        /// 转换为装配体
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static AssemblyDoc AsAssemblyDoc(this IModelDoc2 doc)
        {
            if (doc.GetTypeEx() != swDocumentTypes_e.swDocASSEMBLY)
            {
                throw new Exception("错误的类型,无法转换为PartDoc");
            }
            return (AssemblyDoc)doc;
        }

        public static void UIUpdateEnable (this ModelDoc2 swModel, bool IsEnable)
        {
            swModel.FeatureManager.EnableFeatureTree = IsEnable;
            swModel.FeatureManager.EnableFeatureTreeWindow = IsEnable;
            ModelView modelView = (ModelView)swModel.ActiveView;
            if (modelView != null)
            {
                modelView.EnableGraphicsUpdate = IsEnable;
            }
        }

        /// <summary>
        /// 获取文档类型
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static swDocumentTypes_e GetTypeEx(this AssemblyDoc doc)
        {
            return doc.GetType().CastObj<swDocumentTypes_e>();
        }

        public static bool IsSpecType(this IModelDoc2 doc,swDocumentTypes_e type)
        {
            return doc.GetTypeEx() == type;
        }


        //public static bool TemSaveAsFor(this ModelDoc2 doc,string NewFilePathName)
        //{
        //    int errors = 0, warnings = 0;
        //    bool success = doc.Extension.SaveAs(NewFilePathName, swSaveAsVersion_e.swSaveAsCurrentVersion.EnumCastToInt(),
        //        (swSaveAsOptions_e.swSaveAsOptions_Copy | swSaveAsOptions_e.swSaveAsOptions_Silent).EnumCastToInt(),
        //        null, ref errors, ref warnings);
        //    if (!success)
        //    {
        //        throw new Exception(nameof(TemSaveAsFor) + "-" + errors.CastObj<swFileSaveError_e>().ToString());
        //    }
        //    return true;
        //}

            /// <summary>
            /// 另存为并加载到内存,然后关闭源文档
            /// </summary>
            /// <param name="doc"></param>
            /// <param name="app"></param>
            /// <param name="NewFilePathName"></param>
            /// <returns></returns>
        public static ModelDoc2 TemSaveAsFor(this IModelDoc2 doc,SldWorks app, string NewFilePathName)
        {
            int errors = 0, warnings = 0;
            bool success = doc.Extension.SaveAs(NewFilePathName, swSaveAsVersion_e.swSaveAsCurrentVersion.SWToInt(),
                (swSaveAsOptions_e.swSaveAsOptions_Copy | swSaveAsOptions_e.swSaveAsOptions_Silent).SWToInt(),
                null, ref errors, ref warnings);
            if (!success)
            {
                throw new Exception(nameof(TemSaveAsFor) + "-" + errors.CastObj<swFileSaveError_e>().ToString());
            }
            app.CloseDoc(doc.GetTitle());
            return app.OpenInvisibleDocClient(NewFilePathName);
        }

        /// <summary>
        /// 按顺序遍历特征
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="TopOnly"></param>
        /// <returns></returns>
        public static List<IFeature> GetFirstToLastFeats(this IModelDoc2 doc,bool TopOnly = true)
        {
            List<IFeature> feats = new List<IFeature>();

            var Feat = doc.FirstFeature() as IFeature;

            while (Feat != null)
            {
                feats.Add(Feat);
                if (!TopOnly)
                {
                    var SubFeat = Feat.GetFirstSubFeature() as IFeature;
                    while (SubFeat != null)
                    {
                        feats.Add(SubFeat);
                        SubFeat = SubFeat.GetNextSubFeature() as IFeature;
                    }
                }
                Feat = Feat.GetNextFeature() as IFeature;
            }

            return feats;
        }

        public static IEnumerable<object> GetSelectedObjectsFromModel(this IModelDoc2 doc)
        {
            int count = doc.ISelectionManager.GetSelectedObjectCount2(0);
            List<object> objs = new List<object>();

            for (int i = 1; i < count+1; i++)
            {
                var obj = doc.ISelectionManager.GetSelectedObject6(i, 0);
                objs.Add(obj);
            }

            return objs;

            //return SelectionDataExtensions.GetSelectionsFromModel(model)
            //    .SelectMany(data => data.GetObjects(doc));
        }

        /// <summary>
        /// Get an entity from its persist id.
        /// 
        /// Because we should use the same version of `GetPersistReference` and `GetObjectFromPersistReference` resp.
        /// and the persist reference is saved with the models we should never ever use another version of the two methods.
        /// For more details see http://help.solidworks.com/2016/english/api/sldworksapi/SolidWorks.interop.sldworks~SolidWorks.interop.sldworks.IModelDocExtension~GetPersistReference3.html#remarksSection
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="persistId"></param>
        /// <returns></returns>
        public static object GetObjectFromPersistReference(this IModelDoc2 doc, byte[] persistId)
        {
            int errorCode;
            var @object = doc.Extension.GetObjectByPersistReference3(persistId, out errorCode);
            var result = (swPersistReferencedObjectStates_e)errorCode;
            if (result != swPersistReferencedObjectStates_e.swPersistReferencedObject_Ok)
            {
                throw new Exception($"GetObjectByPersistReference3 returned {result}");
            }
            return @object;
        }

        /// <summary>
        /// 从一个给定的屏幕上的X,Y变换到视图模型中的点和方向
        /// From a given X,Y screen coordinate return the model
        /// coordinates and the direction of looking.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static PointDirection3 ScreenToView(this IModelDoc2 doc,MathUtility math ,int x, int y)
        {
            var view = doc.ActiveView as IModelView;
            var t = view.Transform.Inverse() as MathTransform;

            var eye = math.PointEx(new[] { x, y, 0.0 });

            var look = math.ZAxis();

            eye = eye.MultiplyTransform(t) as MathPoint;
            look = look.MultiplyTransform(t) as MathVector;

            return new PointDirection3(eye.ToVector3(), look.ToVector3());
        }

        /// <summary>
        /// 空间点变换到屏幕
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="math"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Vector2 ViewToScreen(this IModelDoc2 doc, MathUtility math , Vector3 point)
        {

            var view = doc.ActiveView as IModelView;
            var t = view.Transform as MathTransform;
            var mathPoint = point.ToSwMathPoint(math);
            mathPoint = mathPoint.MultiplyTransform(t) as MathPoint;
            var v3 = mathPoint.ToVector3();
            return new Vector2(v3.X, v3.Y);
        }
        /// <summary>
        /// 方程式尺寸类型
        /// </summary>
        public enum EquationsDimensionType
        {
            /// <summary>
            /// 长度
            /// </summary>
            Length,
            /// <summary>
            /// 角度
            /// </summary>
            Angle
        }

        /// <summary>
        /// 获取实体PID
        /// 
        /// Because we should use the same version of `GetPersistReference` and `GetObjectFromPersistReference` resp.
        /// and the persist reference is saved with the models we should never ever use another version of the two methods.
        /// For more details see http://help.solidworks.com/2016/english/api/sldworksapi/SolidWorks.interop.sldworks~SolidWorks.interop.sldworks.IModelDocExtension~GetPersistReference3.html#remarksSection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelDoc"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] GetPersistReference<T>(this IModelDoc2 modelDoc, T obj)
        {
            return modelDoc.Extension.GetPersistReference3(obj) as byte[];
        }

        /// <summary>
        /// Doesn't work when intersecting with wire bodies. 
        /// </summary>
        /// <param name="modelDoc"></param>
        /// <param name="ray"></param>
        /// <param name="bodies"></param>
        /// <param name="hitRadius"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static List<RayIntersection> GetRayIntersections(this IModelDoc2 modelDoc, PointDirection3 ray, IBody2[] bodies, double hitRadius, double offset)
        {
            var icount = modelDoc.RayIntersections
                (BodiesIn: bodies
                , BasePointsIn: ray.Point.ToDoubles()
                , VectorsIn: ray.Direction.ToDoubles()
                , Options: (int)(swRayPtsOpts_e.swRayPtsOptsENTRY_EXIT | swRayPtsOpts_e.swRayPtsOptsNORMALS |
                                  swRayPtsOpts_e.swRayPtsOptsTOPOLS | swRayPtsOpts_e.swRayPtsOptsUNBLOCK)
                , HitRadius: hitRadius
                , Offset: offset);
            var result = modelDoc.GetRayIntersectionsPoints() as double[];

            const int fields = 9;
            return Enumerable.Range(0, icount)
                .Select(i =>
                {
                    var baseOffset = i * fields;

                    var bodyIndex = result[baseOffset + 0];
                    var rayIndex = result[baseOffset + 1];
                    var intersectionType = result[baseOffset + 2];
                    var x = result[baseOffset + 3];
                    var y = result[baseOffset + 4];
                    var z = result[baseOffset + 5];
                    var nx = result[baseOffset + 6];
                    var ny = result[baseOffset + 7];
                    var nz = result[baseOffset + 8];

                    return new RayIntersection(
                        bodies[(int)bodyIndex],
                        (int)rayIndex,
                        (swRayPtsResults_e)intersectionType,
                        new double[] { x, y, z }.ToVector3(),
                        new double[] { nx, ny, nz }.ToVector3()
                        );
                }).ToList();
        }

        /// <summary>
        /// 射线选择类
        /// </summary>
        public class RayIntersection
        {
            public RayIntersection(IBody2 body, int rayIndex, swRayPtsResults_e intersectionType, Vector3 hitPoint, Vector3 normals)
            {
                Body = body;
                RayIndex = rayIndex;
                IntersectionType = intersectionType;
                HitPoint = hitPoint;
                Normals = normals;
            }

            public IBody2 Body { get; }
            public int RayIndex { get; }
            public swRayPtsResults_e IntersectionType { get; }
            public Vector3 HitPoint { get; }
            public Vector3 Normals { get; }

        }

        /// <summary>
        /// 获取方程式中所有的全局变量变转换为字典
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        [Obsolete]
        public static Dictionary<int,int /*SwEq*/> GetGlobals(this IModelDoc2 doc)
        {
            var swEqnMgr = doc.GetEquationMgr();
            Debug.Assert(swEqnMgr != null, "Couldn't get equation manager");

            return null;
            //var p = ModelDocExtensions.SwEwParser;
            //return Enumerable.Range(0, swEqnMgr.GetCount())
            //    .Select
            //    (i =>
            //    {
            //        var str = swEqnMgr.Equation[i];
            //        var r = p.Parse(str);
            //        if (r.IsFaulted)
            //        {
            //            Debug.WriteLine($"Can't parse variable {str}");
            //            return null;

            //        }
            //        return Some(new { i, r.Reply.Result });
            //    })
            //    .WhereIsSome()
            //    .ToDictionary(v => v.i, v => v.Result);
        }

        /// <summary>
        /// Look-up a configuration based on it's ID
        /// </summary>
        /// <param name="modelDoc"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Configuration GetConfigurationFromID(this IModelDoc2 modelDoc, int ID)
        {
            return modelDoc
                .GetConfigurations()
                .FirstOrDefault(config => config.GetID() == ID);
        }
        /// <summary>
        /// Get all configurations
        /// </summary>
        /// <param name="modelDoc"></param>
        /// <returns></returns>
        public static IEnumerable<Configuration> GetConfigurations(this IModelDoc2 modelDoc)
        {
            var configs = modelDoc
            .GetConfigurationNames()
            as string[];

            return  configs.Select(name => (Configuration)modelDoc.GetConfigurationByName(name));
        }

        #region data



        #endregion

        #region 草图

        public static SketchManager CreateSketch(this IModelDoc2 doc)
        {
            doc.InsertSketch();
            return doc.SketchManager;
        }

        #endregion

        #region 尺寸

        /// <summary>
        /// 获取所有的特征尺寸
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<KeyValuePair<Feature,DisplayDimension>> GetModelDispalyDimensions(this ModelDoc2 doc,bool TopOnly = true)
        {
            var listResult = new List<KeyValuePair<Feature, DisplayDimension>>();

            var feats = doc.FeatureManager.GetFeaturesEx(TopOnly);

            foreach (var feat in feats)
            {
                var dims = feat.GetDisplayDimensions();
                foreach (var dim in dims)
                {
                    listResult.Add(new KeyValuePair<Feature, DisplayDimension>(feat.CastObj<Feature>(), dim));
                }
            }
            return listResult;
        }


        #endregion
    }
}
