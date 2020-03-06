using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
//using stdole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
namespace Du.SolidWorks.Extension
{
    public static class SldWorksExtension
    {
        /// <summary>
        /// 对内存中所有文档进行操作
        /// </summary>
        /// <param name="swApp"></param>
        /// <param name="action"></param>
        public static void UsingOpenDoc(this SldWorks swApp, Action<IModelDoc2> action)
        {
            var docs = (swApp.GetDocuments() as Object[]).Cast<IModelDoc2>();

            if (docs  == null)
            {
                return;
            }

            foreach (var item in docs)
            {
                action?.Invoke(item);
            }
        }

        /// <summary>
        /// 判断此文档是否打开
        /// </summary>
        /// <param name="app"></param>
        /// <param name="DocFilePathName"></param>
        /// <returns></returns>
        public static bool HasDocOpened(this SldWorks app,string DocFilePathName)
        {
            bool exist = false;
            app.UsingOpenDoc(d =>
            {
                if (d.GetPathName() == DocFilePathName)
                {
                    exist = true;
                }
            }
            );
            return exist;
        }

        /// <summary>
        /// 对活动文档进行操作
        /// </summary>
        /// <param name="swApp"></param>
        /// <param name="action"></param>
        public static void UsingActiveDoc(this SldWorks swApp, Action<IModelDoc2> action)
        {
            if (swApp.ActiveDoc != null)
            {
                action?.Invoke((IModelDoc2)swApp.ActiveDoc);
            }
        }

        /// <summary>
        /// 以不可见模式打开文档  Open a document invisibly. It will not be shown to the user but you will be
        /// able to interact with it through the API as if it is loaded.
        /// </summary>
        /// <param name="sldWorks"></param>
        /// <param name="toolFile"></param>
        /// <returns></returns>
        public static ModelDoc2 OpenInvisibleReadOnly(this ISldWorks sldWorks, string toolFile, bool visible = false, swDocumentTypes_e type = swDocumentTypes_e.swDocPART)
        {
            try
            {
                if (!visible)
                    sldWorks.DocumentVisible(false, (int)type);
                var spec = (IDocumentSpecification)sldWorks.GetOpenDocSpec(toolFile);
                if (!visible)
                {
                    spec.Silent = true;
                    spec.ReadOnly = true;
                }
                var doc = sldWorks.OpenDoc7(spec);

                doc.Visible = visible;
                return doc;
            }
            finally
            {
                if (!visible)
                    sldWorks.DocumentVisible
                        (true,
                            (int)
                                type);

            }
        }

        /// <summary>
        /// 静默模式打开隐藏文件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static ModelDoc2 OpenInvisibleDocClient(this SldWorks app, string filePath, bool Hidden = true)
        {
            int Errors = -1, Warning = -1;
            var type = app.FileType(filePath);
            try
            {
                if (Hidden) app.DocumentVisible(false, (int)type);

                ModelDoc2 doc = app.OpenDoc6(filePath, type.SWToInt(),  swOpenDocOptions_e.swOpenDocOptions_Silent.SWToInt(),
                    "",ref Errors,ref Warning) as ModelDoc2;

                if (doc == null)
                {
                    throw new Exception(string.Format("errors:{0},warings{1}",
                        Errors.CastObj<swFileLoadError_e>().ToString(),
                        Warning.CastObj<swFileLoadWarning_e>().ToString()));
                }

                return doc;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                app.DocumentVisible(true, (int)type);
            }
        }

        public static swDocumentTypes_e FileType(this SldWorks app,string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            swDocumentTypes_e type = swDocumentTypes_e.swDocPART;
            switch (extension)
            {
                case ".sldprt":
                    type = swDocumentTypes_e.swDocPART;
                    break;
                case ".sldasm":
                    type = swDocumentTypes_e.swDocASSEMBLY;
                    break;
                case ".slddrw":
                    type = swDocumentTypes_e.swDocDRAWING;
                    break;
                default:
                    throw new FileFormatException("不支持的文件类型:" + extension);
            }
            return type;
        }
        /// <summary>
        /// 静默加载 Iges文件 Tries to load an iges file invisibly. Throws an exception if it doesn't work.
        /// </summary>
        /// <param name="sldWorks"></param>
        /// <param name="igesFile"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        public static ModelDoc2 LoadIgesInvisible(this ISldWorks sldWorks, string igesFile, bool visible = false)
        {
            var swDocPart = (int)swDocumentTypes_e.swDocPART;

            try
            {
                if (!visible)
                    sldWorks.DocumentVisible(false, swDocPart);

                ImportIgesData swImportData =
                    (ImportIgesData)sldWorks.GetImportFileData(igesFile);

                int err = 0;
                var newDoc = sldWorks.LoadFile4(igesFile, "r", swImportData, ref err);
                if (err != 0)
                    throw new Exception(@"Unable to load file {igesFile");

                return newDoc;
            }
            finally
            {
                if (!visible)
                    sldWorks.DocumentVisible
                        (true,
                            swDocPart);

            }
        }

        /// <summary>
        /// 后台创建文档
        /// </summary>
        /// <param name="sldWorks"></param>
        /// <param name="hidden"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ModelDoc2 CreateDocument
            (this ISldWorks sldWorks, bool hidden = true, swDocumentTypes_e type = swDocumentTypes_e.swDocPART)
        {
            try
            {

                if (hidden)
                    sldWorks.DocumentVisible(false, (int)type);

                string templatePath = string.Empty;

                switch (type)
                {
                    case swDocumentTypes_e.swDocPART:
                        templatePath = sldWorks.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart);
                        break;
                    case swDocumentTypes_e.swDocASSEMBLY:
                        templatePath = sldWorks.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateAssembly);
                        break;
                    case swDocumentTypes_e.swDocDRAWING:
                        templatePath = sldWorks.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateDrawing);
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrEmpty(templatePath))
                {
                    throw new FileNotFoundException(string.Format("未找到此种类型的模板文件:{0}", type.ToString()));
                }

                if (!File.Exists(templatePath))
                {
                    templatePath =
                        Path.GetDirectoryName(templatePath) + "\\" +
                        Path.GetFileName(templatePath).
                        Replace("零件", "gb_part").
                        Replace("装配体", "gb_assembly").Replace("工程图", "gb_a1");
                }

                if (!File.Exists(templatePath))
                {
                    throw new FileNotFoundException("无法找到SolidWorks文件--" + templatePath);
                }

                //TODO:设置替换字符串
                var doc = (ModelDoc2)sldWorks.NewDocument(templatePath, (int)swDwgPaperSizes_e.swDwgPaperA4size, 1, 1);
                if (hidden)
                {
                    doc.Visible = false;
                    sldWorks.DocumentVisible(false, (int)type);

                }
                /*
                ModelView myModelView = null;
                myModelView = ((ModelView)(doc.ActiveView));
                myModelView.FrameLeft = 0;
                myModelView.FrameTop = 0;
                myModelView = ((ModelView)(doc.ActiveView));

                myModelView.FrameState = ((int)(swWindowState_e.swWindowMinimized));
                */
                return doc;
            }
            finally
            {
                if (hidden)
                    sldWorks.DocumentVisible
                        (true,
                            (int)
                                type);

            }
        }

        
        public static ModelDoc2 ActiveDocEx(this SldWorks app)
        {
            return app.ActiveDoc as ModelDoc2;
        }

        /// <summary>
        /// 通过模板创建新零件或者新项目
        /// </summary>
        /// <param name="app"></param>
        /// <param name="TemFilePath">*.asmdot 或者 *.prtdot</param>
        /// <returns></returns>
        public static ModelDoc2 NewPartOrAssembly(this SldWorks app, string TemFilePath)
        {
            return app.NewDocument(TemFilePath, (int)swDwgPaperSizes_e.swDwgPaperA4size, 100, 100) as ModelDoc2;
        }


        //public static BitmapImage GetPreViewBitMapImage(this SldWorks app,string fileName,string configName)
        //{
        //    var data = app.GetPreviewBitmap(fileName, configName);
        //    if (data == null)
        //    {
        //        return null;
        //    }
        //    return IPictureDispHelper.GetBitmapFromImage(IPictureDispHelper.PictureDispToImage((IPictureDisp)data));
        //}
    }
}
