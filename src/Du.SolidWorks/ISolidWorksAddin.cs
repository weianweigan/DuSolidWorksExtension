using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks
{
    /// <summary>
    /// SolidWorks 插件接口
    /// </summary>
    public interface ISolidWorksAddin
    {
        /// <summary>
        /// ISolidWorks Interface
        /// </summary>
        SldWorks SwApp { get; }
        
        /// <summary>
        /// IMathUtility Interface
        /// </summary>
        MathUtility swMath { get; }

        /// <summary>
        /// SolidWorks 所在路径
        /// </summary>
        string SldWorksPath { get; }

        /// <summary>
        /// 插件执行路径
        /// </summary>
        string AddinExcutePath { get; }

    }

    /// <summary>
    /// Interface for DI SolidWorks Framework 用在依赖注入框架中的接口
    /// </summary>
    public interface ISWAddinCommand
    {
        /// <summary>
        /// 所有命令的实例
        /// </summary>
        Dictionary<string, object> CommandInstances { get; set; }
        
        /// <summary>
        /// 组的ID
        /// </summary>
        List<int> CmdGroupIDs { get; set; }

        /// <summary>
        /// 添加弹出命令
        /// </summary>
        /// <param name="flyoutGroup"><see cref="IFlyoutGroup"/> Interface</param>
        /// <param name="guid"><see cref="Guid"/></param>
        /// <param name="name">名称</param>
        /// <param name="hint">提示</param>
        /// <param name="imageListIndex">图标位置</param>
        /// <param name="commandType">按钮类型<see cref="Type"/></param>
        /// <returns></returns>
        int AddFlyoutCommand(IFlyoutGroup flyoutGroup, string guid, string name, string hint, int imageListIndex, Type commandType);

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="cmdGroup"><see cref="ICommandGroup"/></param>
        /// <param name="guid"><see cref="Guid"/></param>
        /// <param name="name">名称</param>
        /// <param name="postion">位置 默认为 -1</param>
        /// <param name="hint">提示</param>
        /// <param name="tooltip">提示</param>
        /// <param name="imageListIndex">图片位置</param>
        /// <param name="commandIndex">按钮位置</param>
        /// <param name="menuOption">菜单选项</param>
        /// <returns></returns>
        int AddSWCommand(ICommandGroup cmdGroup, string guid, string name, int postion, string hint, string tooltip, int imageListIndex, int commandIndex, int menuOption);

        /// <summary>
        /// 弹出菜单组
        /// </summary>
        /// <param name="commandIndex">按钮索引</param>
        /// <param name="guid"><see cref="Guid"/></param>
        /// <param name="title">名称</param>
        /// <param name="tooltip">提示</param>
        /// <param name="hint">提示</param>
        /// <param name="icons">图标</param>
        /// <returns></returns>
        IFlyoutGroup CreatSWFlyoutGroup(int commandIndex, string guid, string title, string tooltip, string hint, string[] icons);

    }
}
