using Xunit;
using Du.SolidWorks.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Du.XUnitSolidWorks;
using System.Diagnostics;
using SolidWorks.Interop.swconst;
using System.IO;
using SolidWorks.Interop.sldworks;
using System.Threading;

namespace Du.SolidWorks.Extension.Tests
{
    public class SldWorksExtensionTests:IClassFixture<SldWorksFixture>
    {
        private readonly IXUnitSolidWorks _unitSolidWorks;

        private int OpendDocCount = 0;

        [Fact()]
        public void TemTest()
        {
           var doc = _unitSolidWorks.SwApp.OpenInvisibleDocClient(@"C:\Users\weigan\Desktop\装配体2.SLDASM", false);
            doc.GetFirstToLastFeats().ForEach(p => Debug.Print(p.Name +"--"+ p.GetTypeName()));

            var featLast = doc.GetFirstToLastFeats(true).Where(p => p.GetTypeName2() == FeatureTypeName.Component2).Select(p => p).Last();

            var Comp = featLast.GetSpecificFeature2() as Component2;
            Debug.Print(Comp.Name2);
        }

        public SldWorksExtensionTests(SldWorksFixture unitSolidWorks)
        {
            _unitSolidWorks = unitSolidWorks;
            _unitSolidWorks.SLDWorksDisposeOption = SLDWorksDisposeOption.NoAction;

            while (!_unitSolidWorks.SwApp.StartupProcessCompleted)
            {
                Thread.Sleep(1000);
            }

            _unitSolidWorks.SwApp.FrameWidth = 1000;
            _unitSolidWorks.SwApp.FrameHeight = 500;
            _unitSolidWorks.SwApp.TaskPaneIsPinned = false;
            
        }

        private void CreateTemDoc(int count)
        {
            _unitSolidWorks.SwApp.CloseAllDocuments(true);
            for (int i = 0; i < count; i++)
            {
                _unitSolidWorks.SwApp.CreateDocument(false, swDocumentTypes_e.swDocPART);
            }
            OpendDocCount = _unitSolidWorks.SwApp.GetDocumentCount();
            if (count != OpendDocCount)
            {
                throw new Exception("无法创建需要数量的文档");
            }
        }

        [Fact(DisplayName ="遍历所有打开的文档并作操作")]
        public void UsingOpenDocTest()
        {
            CreateTemDoc(5);
            int count = 0;
            _unitSolidWorks.SwApp.UsingOpenDoc(doc =>
            {
                if (doc != null)
                {
                    Debug.Print(doc.GetTitle());
                    count++;
                }
                else
                {
                    throw new NullReferenceException("遍历到空的文档,Method:" + nameof(SldWorksExtension.UsingOpenDoc));
                }
            }
            );
            Assert.Equal(OpendDocCount, count);

            _unitSolidWorks.SwApp.CloseAllDocuments(true);
        }

        [Fact(DisplayName ="对活动文档操作")]
        public void UsingActiveDocTest()
        {
            _unitSolidWorks.SwApp.CloseAllDocuments(true);
            CreateTemDoc(1);
            string docTitle = string.Empty;
            _unitSolidWorks.SwApp.UsingActiveDoc(doc =>
            {
                docTitle = doc?.GetTitle();
            });
            Assert.True(!string.IsNullOrEmpty(docTitle));
        }

        [Theory(DisplayName ="后台打开只读文件")]
        [InlineData("SolidWorksDoc\\OpenInvisibleDocClientTestOne.SLDASM")]
        public void OpenInvisibleReadOnlyTest(string fileName)
        {
            string FilePathName = SWPath.ExtensionDllPath + "\\" + fileName;
            Assert.True(File.Exists(FilePathName));
            _unitSolidWorks.SwApp.CloseAllDocuments(true);
            Assert.True(_unitSolidWorks.SwApp.GetFirstDocument() == null, "无法关闭所有文档");

            _unitSolidWorks.SwApp.OpenInvisibleReadOnly(FilePathName);

            var doc = _unitSolidWorks.SwApp.GetFirstDocument() as IModelDoc2;
            Assert.NotNull(doc);
            Assert.True(doc.GetPathName() == FilePathName, "内存中不是需要的文档" + doc.GetPathName());
            Assert.True(!doc.Visible, string.Format("{0}文档可见", doc.GetPathName()));
            Assert.True(doc.IsOpenedReadOnly(), "文档不是只读");

            _unitSolidWorks.SwApp.CloseAllDocuments(true);
        }

        [Theory(DisplayName ="后台打开文档")]
        [InlineData("SolidWorksDoc\\OpenInvisibleDocClientTestOne.SLDASM")]
        public void OpenInvisibleDocClientTest(string fileName)
        {
            string FilePathName = SWPath.ExtensionDllPath + "\\" + fileName;
            Assert.True(File.Exists(FilePathName));
            _unitSolidWorks.SwApp.CloseAllDocuments(true);
            Assert.True(_unitSolidWorks.SwApp.GetFirstDocument() == null, "无法关闭所有文档");

            _unitSolidWorks.SwApp.OpenInvisibleDocClient(FilePathName);

            var doc = _unitSolidWorks.SwApp.GetFirstDocument() as ModelDoc2;
            Assert.NotNull(doc);
            Assert.True(doc.GetPathName() == FilePathName,"内存中不是需要的文档"+doc.GetPathName());

            _unitSolidWorks.SwApp.CloseAllDocuments(true);
        }

        [Theory(DisplayName ="获取文件类型")]
        //[InlineData(swDocumentTypes_e.swDocPART)]
        [InlineData(swDocumentTypes_e.swDocASSEMBLY)]
        public void FileTypeTest(swDocumentTypes_e type)
        {
            var doc = _unitSolidWorks.SwApp.CreateDocument(false, type);
            Assert.NotNull(doc);
            doc.SaveAs(SWPath.CreateATemFilePath(type));
            Assert.Equal((int)_unitSolidWorks.SwApp.FileType(doc.GetPathName()),(int)type);

            _unitSolidWorks.SwApp.CloseAllDocuments(true);
        }

        [Theory(DisplayName ="后台加载Iges文件",Skip ="LoadFile4 error 1: General Error")]
        [InlineData("SolidWorksDoc\\igesTest.IGS")]
        public void LoadIgesInvisibleTest(string fileName)
        {
            string FilePathName = SWPath.ExtensionDllPath + "\\" + fileName;
            Assert.True(File.Exists(FilePathName));
            _unitSolidWorks.SwApp.CloseAllDocuments(true);
            Assert.True(_unitSolidWorks.SwApp.GetFirstDocument() == null, "无法关闭所有文档");

            _unitSolidWorks.SwApp.LoadIgesInvisible(FilePathName);

            var doc = _unitSolidWorks.SwApp.GetFirstDocument() as ModelDoc2;
            Assert.NotNull(doc);
            Assert.True(doc.GetPathName() == FilePathName, "内存中不是需要的文档" + doc.GetPathName());
            Assert.True(!doc.Visible);

            _unitSolidWorks.SwApp.CloseAllDocuments(true);
        }

        [Fact(DisplayName = "创建不可见文档")]
        public void CreateDocumentTest()
        {
            _unitSolidWorks.SwApp.CloseAllDocuments(true);
            var partDoc = _unitSolidWorks.SwApp.CreateDocument(true, swDocumentTypes_e.swDocPART);
            Assert.NotNull(partDoc);
            Assert.True(!partDoc.Visible);

            _unitSolidWorks.SwApp.CloseAllDocuments(true);
        }

        [Fact(DisplayName ="将活动文档转换为ModelDoc2")]
        public void ActiveDocExTest()
        {
            _unitSolidWorks.SwApp.CloseAllDocuments(true);

            CreateTemDoc(1);
            var doc = _unitSolidWorks.SwApp.ActiveDocEx();
            Assert.NotNull(doc);
            Assert.True(doc is ModelDoc2);
        }
    }
}