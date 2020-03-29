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
    /// Extension Methods for <see cref="IModelDocExtension"/>
    /// </summary>
    public static class ModelDocExtensionExtension
    {
        /// <summary>
        /// 重新对文档命名
        /// </summary>
        /// <param name="docExtension"><see cref="IModelDocExtension"/></param>
        /// <param name="newName">新名称</param>
        /// <param name="throwError">是否抛出异常</param>
        /// <returns></returns>
        public static swRenameDocumentError_e RenameDocumentEx(this IModelDocExtension docExtension,string newName,bool throwError = true)
        {
            var result = docExtension.RenameDocument(newName).CastObj<swRenameDocumentError_e>();
            if (throwError && result != swRenameDocumentError_e.swRenameDocumentError_None)
            {
                throw new InvalidOperationException($"Cannot rename the document with new Name:{newName},Error type: {result.ToString()}");
            }
            return result;
        }

    }
}
