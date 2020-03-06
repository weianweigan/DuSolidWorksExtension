using Xunit;
using Du.SolidWorks.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Du.XUnitSolidWorks;
using System.IO;

namespace Du.SolidWorks.Extension.Tests
{
    public class ModelDocDuExtensionTests:IClassFixture<SldWorksFixture>
    {
        private  readonly XUnitSolidWorks.IXUnitSolidWorks _unitSolidWorks;

        public ModelDocDuExtensionTests(SldWorksFixture sldWorksFixture)
        {
            _unitSolidWorks = sldWorksFixture;

        }

        [Theory()]
        [InlineData("SolidWorksDoc\\OpenInvisibleDocClientTestOne.SLDASM")]
        public void CurrentCompTest(string fileName)
        {
            string FilePathName = SWPath.ExtensionDllPath + "\\" + fileName;
            Assert.True(File.Exists(FilePathName));
            _unitSolidWorks.SwApp.CloseAllDocuments(true);
            Assert.True(_unitSolidWorks.SwApp.GetFirstDocument() == null, "无法关闭所有文档");

            _unitSolidWorks.SwApp.OpenInvisibleDocClient(FilePathName,false);

            var comp = _unitSolidWorks.SwApp.ActiveDocEx().CurrentComp();
            Assert.NotNull(comp);
        }

    }
}