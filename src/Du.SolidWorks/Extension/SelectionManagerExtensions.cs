using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public class ObjectSelection
    {
        public ObjectSelection(object @object, swSelectType_e type, int mark, int index)
        {
            Object = @object;
            Index = index;
            Type = type;
            Mark = mark;
        }
        /// <summary>
        /// 选中对象
        /// </summary>
        public object Object { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// 类型
        /// </summary>
        public swSelectType_e Type { get; }
        /// <summary>
        /// 标记
        /// </summary>
        public int Mark { get; }

        public bool TryGetT<T>(out T TObj)
        {
            TObj = default;
            switch (Type)
            {
                case swSelectType_e.swSelNOTHING:
                    break;
                case swSelectType_e.swSelEDGES:
                    break;
                case swSelectType_e.swSelFACES:
                    break;
                case swSelectType_e.swSelVERTICES:
                    break;
                case swSelectType_e.swSelDATUMPLANES:
                    if (typeof(T).Name == nameof(IFeature))
                    {
                        TObj = (T)Object;
                    }
                    break;
                case swSelectType_e.swSelDATUMAXES:
                    if (typeof(T).Name == nameof(IFeature))
                    {
                        TObj = (T)Object;
                    }
                    break;
                case swSelectType_e.swSelDATUMPOINTS:
                    break;
                case swSelectType_e.swSelOLEITEMS:
                    break;
                case swSelectType_e.swSelATTRIBUTES:
                    break;
                case swSelectType_e.swSelSKETCHES:
                    break;
                case swSelectType_e.swSelSKETCHSEGS:
                    break;
                case swSelectType_e.swSelSKETCHPOINTS:
                    break;
                case swSelectType_e.swSelDRAWINGVIEWS:
                    break;
                case swSelectType_e.swSelGTOLS:
                    break;
                case swSelectType_e.swSelDIMENSIONS:
                    break;
                case swSelectType_e.swSelNOTES:
                    break;
                case swSelectType_e.swSelSECTIONLINES:
                    break;
                case swSelectType_e.swSelDETAILCIRCLES:
                    break;
                case swSelectType_e.swSelSECTIONTEXT:
                    break;
                case swSelectType_e.swSelSHEETS:
                    break;
                case swSelectType_e.swSelCOMPONENTS:
                    break;
                case swSelectType_e.swSelMATES:
                    break;
                case swSelectType_e.swSelBODYFEATURES:
                    break;
                case swSelectType_e.swSelREFCURVES:
                    break;
                case swSelectType_e.swSelEXTSKETCHSEGS:
                    break;
                case swSelectType_e.swSelEXTSKETCHPOINTS:
                    break;
                case swSelectType_e.swSelHELIX:
                    break;
               
                case swSelectType_e.swSelREFSURFACES:
                    break;
                case swSelectType_e.swSelCENTERMARKS:
                    break;
                case swSelectType_e.swSelINCONTEXTFEAT:
                    break;
                case swSelectType_e.swSelMATEGROUP:
                    break;
                case swSelectType_e.swSelBREAKLINES:
                    break;
                case swSelectType_e.swSelINCONTEXTFEATS:
                    break;
                case swSelectType_e.swSelMATEGROUPS:
                    break;
                case swSelectType_e.swSelSKETCHTEXT:
                    break;
                case swSelectType_e.swSelSFSYMBOLS:
                    break;
                case swSelectType_e.swSelDATUMTAGS:
                    break;
                case swSelectType_e.swSelCOMPPATTERN:
                    break;
                case swSelectType_e.swSelWELDS:
                    break;
                case swSelectType_e.swSelCTHREADS:
                    break;
                case swSelectType_e.swSelDTMTARGS:
                    break;
                case swSelectType_e.swSelPOINTREFS:
                    break;
                case swSelectType_e.swSelDCABINETS:
                    break;
                case swSelectType_e.swSelEXPLVIEWS:
                    break;
                case swSelectType_e.swSelEXPLSTEPS:
                    break;
                case swSelectType_e.swSelEXPLLINES:
                    break;
                case swSelectType_e.swSelSILHOUETTES:
                    break;
                case swSelectType_e.swSelCONFIGURATIONS:
                    break;
                case swSelectType_e.swSelOBJHANDLES:
                    break;
                case swSelectType_e.swSelARROWS:
                    break;
                case swSelectType_e.swSelZONES:
                    break;
                case swSelectType_e.swSelREFEDGES:
                    break;
                case swSelectType_e.swSelREFFACES:
                    break;
                case swSelectType_e.swSelREFSILHOUETTE:
                    break;
                case swSelectType_e.swSelBOMS:
                    break;
                case swSelectType_e.swSelEQNFOLDER:
                    break;
                case swSelectType_e.swSelSKETCHHATCH:
                    break;
                case swSelectType_e.swSelIMPORTFOLDER:
                    break;
                case swSelectType_e.swSelVIEWERHYPERLINK:
                    break;
                case swSelectType_e.swSelMIDPOINTS:
                    break;
                case swSelectType_e.swSelCUSTOMSYMBOLS:
                    break;
                case swSelectType_e.swSelCOORDSYS:
                    break;
                case swSelectType_e.swSelDATUMLINES:
                    break;
                case swSelectType_e.swSelROUTECURVES:
                    break;
                case swSelectType_e.swSelBOMTEMPS:
                    break;
                case swSelectType_e.swSelROUTEPOINTS:
                    break;
                case swSelectType_e.swSelCONNECTIONPOINTS:
                    break;
                case swSelectType_e.swSelROUTESWEEPS:
                    break;
                case swSelectType_e.swSelPOSGROUP:
                    break;
                case swSelectType_e.swSelBROWSERITEM:
                    break;
                case swSelectType_e.swSelFABRICATEDROUTE:
                    break;
                case swSelectType_e.swSelSKETCHPOINTFEAT:
                    break;
                case swSelectType_e.swSelEMPTYSPACE:
                    break;
               
                case swSelectType_e.swSelLIGHTS:
                    break;
                case swSelectType_e.swSelWIREBODIES:
                    break;
                case swSelectType_e.swSelSURFACEBODIES:
                    break;
                case swSelectType_e.swSelSOLIDBODIES:
                    break;
                case swSelectType_e.swSelFRAMEPOINT:
                    break;
                case swSelectType_e.swSelSURFBODIESFIRST:
                    break;
                case swSelectType_e.swSelMANIPULATORS:
                    break;
                case swSelectType_e.swSelPICTUREBODIES:
                    break;
                case swSelectType_e.swSelSOLIDBODIESFIRST:
                    break;
                case swSelectType_e.swSelLEADERS:
                    break;
                case swSelectType_e.swSelSKETCHBITMAP:
                    break;
                case swSelectType_e.swSelDOWELSYMS:
                    break;
                case swSelectType_e.swSelEXTSKETCHTEXT:
                    break;
                case swSelectType_e.swSelBLOCKINST:
                    break;
                case swSelectType_e.swSelFTRFOLDER:
                    break;
                case swSelectType_e.swSelSKETCHREGION:
                    break;
                case swSelectType_e.swSelSKETCHCONTOUR:
                    break;
                case swSelectType_e.swSelBOMFEATURES:
                    break;
                case swSelectType_e.swSelANNOTATIONTABLES:
                    break;
                case swSelectType_e.swSelBLOCKDEF:
                    break;
                case swSelectType_e.swSelCENTERMARKSYMS:
                    break;
                case swSelectType_e.swSelSIMULATION:
                    break;
                case swSelectType_e.swSelSIMELEMENT:
                    break;
                case swSelectType_e.swSelCENTERLINES:
                    break;
                case swSelectType_e.swSelHOLETABLEFEATS:
                    break;
                case swSelectType_e.swSelHOLETABLEAXES:
                    break;
                case swSelectType_e.swSelWELDMENT:
                    break;
                case swSelectType_e.swSelSUBWELDFOLDER:
                    break;
                case swSelectType_e.swSelEXCLUDEMANIPULATORS:
                    break;
                case swSelectType_e.swSelREVISIONTABLE:
                    break;
                case swSelectType_e.swSelSUBSKETCHINST:
                    break;
                case swSelectType_e.swSelWELDMENTTABLEFEATS:
                    break;
                case swSelectType_e.swSelBODYFOLDER:
                    break;
                case swSelectType_e.swSelREVISIONTABLEFEAT:
                    break;
                case swSelectType_e.swSelSUBATOMFOLDER:
                    break;
                case swSelectType_e.swSelWELDBEADS:
                    break;
                case swSelectType_e.swSelEMBEDLINKDOC:
                    break;
                case swSelectType_e.swSelJOURNAL:
                    break;
                case swSelectType_e.swSelDOCSFOLDER:
                    break;
                case swSelectType_e.swSelCOMMENTSFOLDER:
                    break;
                case swSelectType_e.swSelCOMMENT:
                    break;
                case swSelectType_e.swSelSWIFTANNOTATIONS:
                    break;
                case swSelectType_e.swSelSWIFTFEATURES:
                    break;
                case swSelectType_e.swSelCAMERAS:
                    break;
                case swSelectType_e.swSelMATESUPPLEMENT:
                    break;
                case swSelectType_e.swSelANNOTATIONVIEW:
                    break;
                case swSelectType_e.swSelGENERALTABLEFEAT:
                    break;
                case swSelectType_e.swSelDISPLAYSTATE:
                    break;
                case swSelectType_e.swSelSUBSKETCHDEF:
                    break;
                case swSelectType_e.swSelSWIFTSCHEMA:
                    break;
                case swSelectType_e.swSelTITLEBLOCK:
                    break;
                case swSelectType_e.swSelTITLEBLOCKTABLEFEAT:
                    break;
                case swSelectType_e.swSelOBJGROUP:
                    break;
                case swSelectType_e.swSelPLANESECTIONS:
                    break;
                case swSelectType_e.swSelCOSMETICWELDS:
                    break;
                case swSelectType_e.SwSelMAGNETICLINES:
                    break;
                case swSelectType_e.swSelREVISIONCLOUDS:
                    break;
                case swSelectType_e.swSelBorder:
                    break;
                case swSelectType_e.swSelEVERYTHING:
                    break;
                case swSelectType_e.swSelLOCATIONS:
                    break;
                case swSelectType_e.swSelUNSUPPORTED:
                    break;
                default:
                    throw new Exception(typeof(T).Name + "展示不能转换");
                    
            }
            return false;
        }
    }
    public static class SelectionManagerExtensions
    {

        public const int AnyMark = -1;

        public const int StartIndex = 1;

        /// <summary>
        /// 获取选中对象
        /// </summary>
        /// <param name="selMgr"></param>
        /// <returns></returns>
        public static IEnumerable<ObjectSelection> GetObjectSelections(this ISelectionMgr selMgr)
        {
            return GetAllSelectedObjectsEx(selMgr);

            //var count = selMgr.GetSelectedObjectCount2(AnyMark);
            //selMgr.EnableContourSelection = true;
            //return Enumerable.Range(StartIndex, count)
            //    .Select(index =>
            //    {
            //        var type = (swSelectType_e)selMgr.GetSelectedObjectType3(index, AnyMark);
            //        var mark = selMgr.GetSelectedObjectMark(index);
            //        var obj = selMgr.GetSelectedObject6(index, AnyMark);


            //        return new ObjectSelection(obj, type, mark, index);
            //    });
        }

        /// <summary>
        /// 获取特定的选中对象
        /// </summary>
        /// <param name="selMgr"></param>
        /// <param name="filter"> <![CDATA[(type,mark)=>bool]]> if null then the default is true </param>
        /// <returns></returns>
        public static IReadOnlyList<object> GetSelectedObjects(this ISelectionMgr selMgr, Func<swSelectType_e, int, bool> filter)
        {
            return selMgr.GetObjectSelections()
                .Where(o => filter(o.Type, o.Mark))
                .Select(o => o.Object)
                .ToList();
        }

        /// <summary>
        /// 获取所有选中对象
        /// </summary>
        /// <param name="selMgr"></param>
        /// <returns></returns>
        public static IReadOnlyList<object> GetAllSelectedObjects(this ISelectionMgr selMgr)
        {
            return selMgr.GetSelectedObjects((t, m) => true);
        }

        /// <summary>
        /// 获取所有的选择对象
        /// </summary>
        /// <param name="selMgr"></param>
        /// <param name="Mark"></param>
        /// <returns></returns>
        public static List<ObjectSelection> GetAllSelectedObjectsEx(this ISelectionMgr selMgr)
        {
            var count = selMgr.GetSelectedObjectCount();
            List<ObjectSelection> objList = new List<ObjectSelection>();
            for (int i = 1; i < count+1; i++)
            {
                int selMark = selMgr.GetSelectedObjectMark(i);
                swSelectType_e swSelectType_E = (swSelectType_e)selMgr.GetSelectedObjectType3(i, AnyMark);
                object obj = selMgr.GetSelectedObject6(i, AnyMark);
                objList.Add(new ObjectSelection(obj, swSelectType_E, selMark, i));
            }
            return objList;
        }


        /// <summary>
        /// 通过委托来创建一个Callout--单实例,不支持使用此方法创建两个
        /// </summary>
        /// <param name="seleMgr"></param>
        /// <param name="RowsNum"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Callout CreateCallOut(this ISelectionMgr seleMgr,int RowsNum,Func<ICallout,int,string,bool> func)
        {
            return seleMgr.CreateCallout2(RowsNum, SwCalloutHandlerWrapperHelper.CreateWrapper(func));
        }

    }
}
