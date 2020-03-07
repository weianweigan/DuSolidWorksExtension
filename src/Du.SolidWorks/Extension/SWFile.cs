using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// 包含有SolidWorks各类文件扩展名
    /// </summary>
    public static class SWFile
    {
        /// <summary>
        /// .sldprt 零件扩展名
        /// </summary>
        public const string Part = ".sldprt";

        /// <summary>
        /// .sldasm 装配体扩展名
        /// </summary>
        public const string Assembly = ".sldasm";

        /// <summary>
        /// .slddrw 工程图扩展名
        /// </summary>
        public const string Drawing = ".slddrw";

        /// <summary>
        /// .asmdot 装配体模板扩展名
        /// </summary>
        public const string AssemblyTempalte = ".asmdot";

        /// <summary>
        /// .drwdot 工程图扩展名
        /// </summary>
        public const string DrawingTemplate = ".drwdot";

        /// <summary>
        /// 零件模板
        /// .prtdot
        /// </summary>
        public const string PartTempalte = ".prtdot";

        /// <summary>
        /// 判断是否是零件或者装配体
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsPartOrAssembly(string filePath)
        {
            var type = SWPath.GetFileType(filePath);
            return (type == swDocumentTypes_e.swDocPART || type == swDocumentTypes_e.swDocASSEMBLY) ? true : false;
        }
    }

    /// <summary>
    /// 一些路径的扩展方法
    /// </summary>
    public static class SWPath
    {
        /// <summary>
        /// 判断是否是零件或者装配体
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool IsPartOrAssembly(string filePath)
        {
            return SWFile.IsPartOrAssembly(filePath);
        }

        /// <summary>
        /// Du.SolidWorks 程序集执行路径
        /// </summary>
        public static string ExtensionDllPath
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// 获取SolidWorks的文件类型
        /// .sldprt .sldasm .slddrw
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static swDocumentTypes_e GetFileType( string filePath)
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
        /// 在程序集执行路径使用Guid生成一个零件文件路径
        /// *.sldpart *.sldasm *.slddrw
        /// </summary>
        /// <param name="docType">需要生成的文件类型</param>
        /// <returns></returns>
        public static string CreateATemFilePath(swDocumentTypes_e docType)
        {
            string FilePath = ExtensionDllPath;
            switch (docType)
            {
                case swDocumentTypes_e.swDocPART:
                    FilePath = FilePath + "\\" + Guid.NewGuid().ToString() + SWFile.Part;
                    break;
                case swDocumentTypes_e.swDocASSEMBLY:
                    FilePath = FilePath + "\\" + Guid.NewGuid().ToString() + SWFile.Assembly;
                    break;
                case swDocumentTypes_e.swDocDRAWING:
                    FilePath = FilePath + "\\" + Guid.NewGuid().ToString() + SWFile.Drawing;
                    break;
                default:
                    throw new FileFormatException("不支持的文件类型:" + docType.ToString());
            }
            return FilePath;
        }
    }
}
