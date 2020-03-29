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
    /// <summary>
    /// Extension methods for <see cref="ISldWorks"/> Interface
    /// </summary>
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

            if (docs == null)
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
        public static bool HasDocOpened(this SldWorks app, string DocFilePathName)
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
        /// <param name="visible">是否对用户可见</param>
        /// <param name="type"><see cref="swDocumentTypes_e"/> SolidWorks 文件类型 ,默认为零件</param>
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
        /// <param name="app"><see cref="ISldWorks"/></param>
        /// <param name="filePath">文件路径</param>
        /// <param name="Hidden">是否隐藏 默认隐藏</param>
        /// <returns></returns>
        public static ModelDoc2 OpenInvisibleDocClient(this SldWorks app, string filePath, bool Hidden = true)
        {
            int Errors = -1, Warning = -1;
            var type = app.FileType(filePath);
            try
            {
                if (Hidden) app.DocumentVisible(false, (int)type);

                ModelDoc2 doc = app.OpenDoc6(filePath, type.SWToInt(), swOpenDocOptions_e.swOpenDocOptions_Silent.SWToInt(),
                    "", ref Errors, ref Warning) as ModelDoc2;

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

        /// <summary>
        /// 判断文件类型
        /// </summary>
        /// <param name="app"><see cref="ISldWorks"/></param>
        /// <param name="filePath">文件全路径</param>
        /// <returns></returns>
        public static swDocumentTypes_e FileType(this SldWorks app, string filePath)
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
        /// 根据模板创建文档
        /// </summary>
        /// <param name="sldWorks"><see cref="ISldWorks"/></param>
        /// <param name="templatePath">模板路径 支持的文件类型为<para>.prtdot</para><para>.asmdot</para><para>.drwdot</para></param>
        /// <param name="hidden">是否对用户可见 默认为不可见</param>
        /// <returns><see cref="ModelDoc2"/></returns>
        public static ModelDoc2 CreateDocument
            (this ISldWorks sldWorks, string templatePath,bool hidden = true)
        {
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException(string.Format("未找到此种类型的模板文件"));
            }
            var type = SWPath.GetTemFileType(templatePath);
            try
            {

                if (hidden)
                    sldWorks.DocumentVisible(false, (int)type);

                //TODO:设置替换字符串
                var doc = (ModelDoc2)sldWorks.NewDocument(templatePath, (int)swDwgPaperSizes_e.swDwgPaperA4size, 1, 1);

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

        /// <summary>
        /// 以默认设置的模板创建文档
        /// </summary>
        /// <param name="sldWorks"><see cref="ISldWorks"/></param>
        /// <param name="type"><see cref="swDocumentTypes_e"/></param>
        /// <returns></returns>
        public static IModelDoc2 CreateDocument(this ISldWorks sldWorks, swDocumentTypes_e type)
        {
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

            return sldWorks.CreateDocument(templatePath);
        }

        /// <summary>
        /// 将 <see cref="IModelDoc2"/> 转换为 IModelDoc2
        /// </summary>
        /// <param name="app"><see cref="ISldWorks"/></param>
        /// <returns></returns>
        public static IModelDoc2 ActiveDocEx(this SldWorks app)
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


        /// <summary>
        /// 设置一个选项后执行动作,执行完后设置回系统默认值
        /// </summary>
        /// <param name="app">ISldwork interface</param>
        /// <param name="toggleSetting">需要设置的枚举值</param>
        /// <param name="value">值</param>
        /// <param name="action">执行的动作</param>
        public static void WithToggleState(this ISldWorks app, swUserPreferenceToggle_e toggleSetting, bool value, Action action)
        {
            try
            {
                bool fileLockState = app.GetUserPreferenceToggle(toggleSetting.SWToInt());
                app.SetUserPreferenceToggle(swUserPreferenceToggle_e.swLockRecentDocumentsList.SWToInt(), true);
                action?.Invoke();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                app.SetUserPreferenceToggle(toggleSetting.SWToInt(), value);
            }
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
