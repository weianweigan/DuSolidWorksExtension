using CodeProject.ObjectPool;
using SolidWorks.Interop.sldworks;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace Du.XUnitSolidWorks
{
    /// <summary>
    /// SolidWorks程序池
    /// </summary>
    /// <description>
    /// 优化调用SolidWorks的速度
    /// 开发环境配置
    /// 1、引用 Dll
    ///     .NET 4.0 项目在引用里添加 C:\Program Files\SOLIDWORKS\SOLIDWORKS\api\redist\SolidWorks.Interop.sldworks.dll
    ///     .NET 2.0/3.5 在引用里添加 C:\Program Files\SOLIDWORKS\SOLIDWORKS\api\redist\CLR2\SolidWorks.Interop.sldworks.dll
    /// 2、Dll 设置
    ///     .NET 4.0 项目推荐进行如下操作：dll 右键属性，嵌入互操作类型=false，防止出现不可预知的问题
    /// 3、添加枚举库引用
    ///     SOLIDWORKS 2018 Commands type library
    ///     SOLIDWORKS 2018 Constant type library
    /// </description>
    public class SolidWorksPool
    {

        /// <summary>
        /// 包装对象池
        /// </summary>
        public ObjectPool<PooledObjectWrapper<SldWorks>> Pool;

        #region 成员属性及构造器

        private static string SW_PATH;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        public SolidWorksPool(string SWPath = SWConfig.SolidWorksEXE)
        {
            SW_PATH = SWPath;
            // 初始化对象池
            Pool = new ObjectPool<PooledObjectWrapper<SldWorks>>(() =>
                new PooledObjectWrapper<SldWorks>(ExeSolidWorks())
                { // 创建资源
                    OnReleaseResources = CloseSolidWorks, // 资源释放，然后关闭连接池
                    OnResetState = ResetSolidWorks // 资源重置，然后放入连接池
                });

            Pool.MaximumPoolSize = 2;

            //设置池容量大小为CPU逻辑核心数
            //Pool.MaximumPoolSize = System.Environment.ProcessorCount;
        }

        #endregion

        #region 私有工具方法

        /// <summary>
        /// 创建绑定上下文
        /// DllImport 用于引入外部非托管(Native)Dll
        /// </summary>
        /// <param name="reserved"></param>
        /// <param name="ppbc">绑定上下文</param>
        /// <returns></returns>
        [DllImport("ole32.dll")]
        private static extern int CreateBindCtx(uint reserved, out IBindCtx ppbc);

        /// <summary>
        /// exe 启动 SolidWorks 图形界面程序，可以启动多个SolidWorks
        /// 注：
        ///     打开多个 SolidWorks 时，从第二个开始，日志文件有冲突警告，但不影响程序使用，知识需要人去点击确定操作才能继续，
        ///     HKEY_CURRENT_USER\Software\Solidworks\SolidWorks 2014\ExtReferences：SolidWorks Journal Folders 
        ///     上述注册表键值下的 SolidWorksPerformance.log 和 swxJRNL.swj 是 SolidWorks 运行后打开锁定的
        /// </summary>
        /// <param name="timeoutSec">超时，秒</param>
        /// <returns>SldWorks对象</returns>
        public SldWorks ExeSolidWorks(int timeoutSec = 30, string swPath = SWConfig.SolidWorksEXE)
        {
            // 获取超时，秒
            TimeSpan timeout = TimeSpan.FromSeconds(timeoutSec);

            // 当前时间
            DateTime startTime = DateTime.Now;

            // 参数，B表示Batch模式，不锁定日志文件，避免第二个之后的进程提示日志锁定
            string arguments = "/B";

            // 开始启动程序，并将程序绑定到对应的Process，比较耗时
            Process process = Process.Start(swPath, arguments);

            // 循环赋值
            SldWorks sldWorks = null;
            while (sldWorks == null)
            {
                // 超时抛异常
                if (DateTime.Now - startTime > timeout)
                {
                    throw new TimeoutException();
                }
                Thread.Sleep(1000);

                sldWorks = GetSolidWorksFromProcess(process.Id);
            }

            return sldWorks;
        }

        /// <summary>
        /// exe 异步启动 SolidWorks
        /// </summary>
        /// <param name="timeoutSec">超时，秒</param>
        /// <returns></returns>
        private async Task<SldWorks> ExeSolidWorksAsync(int timeoutSec = 30)
        {
            return await Task.Run(() =>
            {
                return ExeSolidWorks(timeoutSec);
            });
        }

        /// <summary>
        /// COM启动SolidWorks，只能启动一个SolidWorks进程
        /// </summary>
        /// <returns>SldWorks</returns>
        private SldWorks DllSolidWorks()
        {
            // 标识符
            string programId = "SldWorks.Application";

            // 获取指定标识符的类型，true：获取错误时抛出异常
            Type programType = Type.GetTypeFromProgID(programId, true);

            // 创建实例，如果SolidWorks没有运行，则新建
            SldWorks sldWorks = Activator.CreateInstance(programType) as SldWorks;

            // 设置是否显示图形界面
            sldWorks.Visible = false;

            // 连接ROT(运行对象表)中的SolidWorks进程，如果SolidWorks没有启动，会抛出异常
            // SldWorks sldWorks = Marshal.GetActiveObject(programId) as SldWorks;
            // sldWorks.Visible = true;

            return sldWorks;
        }

        /// <summary>
        /// new 启动 SolidWorks，和Dll方式原理相同，只能起一个
        /// </summary>
        /// <returns>SldWorks</returns>
        private SldWorks NewSolidWorks()
        {
            SldWorks sldWorks = new SldWorks();
            sldWorks.Visible = false;
            return sldWorks;

        }

        /// <summary>
        /// 从 ROT 中获取 SolidWorks 程序
        /// </summary>
        /// <param name="processId">进程 id</param>
        /// <returns>SldWorks</returns>
        private SldWorks GetSolidWorksFromProcess(int processId)
        {
            // 进程名字
            string monikerName = "SolidWorks_PID_" + processId.ToString();

            // 绑定上下文
            IBindCtx context = null;

            // 运行时对象表 Running Object Table
            IRunningObjectTable rot = null;

            // 运行时对象名称集合
            IEnumMoniker monikers = null;

            try
            {
                // 创建绑定上下文
                CreateBindCtx(0, out context);

                // 获取运行时对象表 Running Object Table
                context.GetRunningObjectTable(out rot);

                // 列出运行时对象名称
                rot.EnumRunning(out monikers);

                // 
                IMoniker[] moniker = new IMoniker[1];

                while (monikers.Next(1, moniker, IntPtr.Zero) == 0)
                {
                    // 当前名字
                    IMoniker curMoniker = moniker.First();

                    // 显示名称
                    string name = null;

                    if (curMoniker != null)
                    {
                        try
                        {
                            // 获取显示名称
                            curMoniker.GetDisplayName(context, null, out name);
                        }
                        catch (UnauthorizedAccessException)
                        {

                        }
                    }

                    if (string.Equals(monikerName, name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // 从ROT获取对象并返回
                        rot.GetObject(curMoniker, out object app);

                        SldWorks sldWorks = app as SldWorks;

                        // 这一句不起作用
                        sldWorks.Visible = false;

                        return sldWorks;

                    }
                }
            }
            finally
            {
                if (monikers != null)
                {
                    Marshal.ReleaseComObject(monikers);
                }

                if (rot != null)
                {
                    Marshal.ReleaseComObject(rot);
                }

                if (context != null)
                {
                    Marshal.ReleaseComObject(context);
                }
            }

            return null;
        }

        /// <summary>
        /// 关闭SldWorks对象，对象池销毁时调用
        /// </summary>
        /// <param name="sldWorks">要关闭的对象</param>
        private void CloseSolidWorks(SldWorks sldWorks)
        {
            // 关闭 SldWorks，释放所占用的系统资源
            sldWorks.ExitApp();
            //IDisposable disp = sldWorks as IDisposable;
            //disp.Dispose();
        }

        /// <summary>
        /// 重置SldWorks对象，放回连接池时调用
        /// </summary>
        /// <param name="sldWorks">要放入池中的对象</param>
        private void ResetSolidWorks(SldWorks sldWorks)
        {
            // 重置SldWorks对象的操作，便于下次使用
            sldWorks.CloseAllDocuments(true);
            //sldWorks.Visible = false;
        }

        #endregion
    }
}
