using SolidWorks.Interop.swconst;
using System.Collections.Generic;

/*
用于承载SolidWorks选中集合的持久化

*/

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// 可选中对象数据集
    /// </summary>
    public class SelectionData
    {
        public List<ObjectId> ObjectIds { get; set; }
    }

    /// <summary>
    /// SolidWorks对象id
    /// </summary>
    public class ObjectId
    {
        /// <summary>
        /// SolidWorks Object PID
        /// </summary>
        /// <remarks>
        /// SolidWorks 唯一ID识别码
        /// </remarks>
        public byte[] Data { get;set; }

        public swSelectType_e SelectionType { get; set; }
    }
}