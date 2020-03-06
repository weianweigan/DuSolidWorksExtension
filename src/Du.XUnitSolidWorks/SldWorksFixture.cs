using SolidWorks.Interop.sldworks;
using System;
using System.Diagnostics;

namespace Du.XUnitSolidWorks
{
    public enum SLDWorksDisposeOption
    {
        CloseAllDocument,
        CloseAllDocumentAndExit,
        NoAction,
        WithActionClose
    }
    public class SldWorksFixture : IDisposable, IXUnitSolidWorks
    {
        private SldWorks _swapp;

        public SldWorks SwApp { get {
                if (_swapp == null)
                {
                    Init();
                }
                return _swapp;
            } private set { _swapp = value; } }

        public MathUtility swMath { get; private set; }

        public SLDWorksDisposeOption SLDWorksDisposeOption { get; set; }

        public Action<SldWorks> CloseAction { get; set; }

        public SldWorksFixture()
        {

        }

        private void Init()
        {
            if (SWConfig.SWObjectPools == null)
            {
                SWConfig.SWObjectPools = new SolidWorksPool();
            }
            Debug.Print("新建对象池");
            var OuterSwApp = SWConfig.SWObjectPools.Pool.GetObject().InternalResource;
            Debug.Print("从对象池中解析对象");
            if (OuterSwApp == null)
            {
                throw new CanNotConnectToSolidWorksException("无法使用批处理连接SolidWorks");
            }
            //外部程序
            SwApp = OuterSwApp;
            swMath = SwApp.GetMathUtility() as MathUtility;
            //绑定到插件内部
        }

        public void Dispose()
        {
            switch (SLDWorksDisposeOption)
            {
                case SLDWorksDisposeOption.CloseAllDocument:
                    SwApp?.CloseAllDocuments(true);
                    break;
                case SLDWorksDisposeOption.CloseAllDocumentAndExit:
                    SwApp?.CloseAllDocuments(true);
                    SwApp?.ExitApp();
                    break;
                case SLDWorksDisposeOption.NoAction:
                    break;
                case SLDWorksDisposeOption.WithActionClose:
                    CloseAction?.Invoke(SwApp);
                    break;
                default:
                    break;
            }
        }
    }
}
