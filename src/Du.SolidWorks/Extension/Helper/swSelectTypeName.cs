using SolidWorks.Interop.swconst;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{

    /// <summary>
    /// 选中类型名 <see cref="IModelDocExtension.SelectByID2(string, string, double, double, double, bool, int, Callout, int)"/> Type Parame
    /// </summary>
    /// <remarks>
    /// 并未实现完全
    /// </remarks>
    public class swSelectTypeName
    {
        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="IEdge"/>
        /// </summary>
        public const string swSelEDGES = "EDGE";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="IFace2"/>
        /// </summary>
        public const string swSelFACES = "FACE";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="IVertex"/>
        /// </summary>
        public const string swSelVERTICES = "VERTEX";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="IFeature"/> 
        /// </summary>
        /// <remarks>
        /// IFeature::GetSpecificFeature2 <seealso cref="IRefPlane"/>
        /// </remarks>
        public const string swSelDATUMPLANES = "PLANE";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="IFeature"/>
        /// </summary>
        /// <remarks>
        /// IFeature::GetSpecificFeature2 <seealso cref="IRefAxis"/>
        /// </remarks>
        public const string swSelDATUMAXES = "AXIS";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="IFeature"/>
        /// </summary>
        /// <remarks>
        /// IFeature::GetSpecificFeature2 No specific interface
        /// </remarks>
        public const string swSelDATUMPOINTS = "DATUMPOINT";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Not Support
        /// </summary>
        public const string swSelOLEITEMS = "OLEITEM";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="IFeature"/>
        /// </summary>
        /// <remarks>
        /// IFeature::GetSpecificFeature2 <seealso cref="IAttribute"/>
        /// </remarks>
        public const string swSelATTRIBUTES = "swSelATTRIBUTES";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="IFeature"/>
        ///  IFeature::GetSpecificFeature2 <seealso cref="ISketch"/> <![CDATA[]]>
        /// </summary>
        public const string swSelSKETCHES = "SKETCH";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="ISketchSegment"/>
        /// </summary>
        public const string swSelSKETCHSEGS = "SKETCHSEGMENT";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="ISketchPoint"/>
        /// </summary>
        public const string swSelSKETCHPOINTS = "SKETCHPOINT";

        /// <summary>
        /// ISelectionMgr::GetSelectedObject6 Return <see cref="IView"/>
        /// </summary>
        public const string swSelDRAWINGVIEWS = "DRAWINGVIEW";

        /// <summary>
        /// <see cref="IGtol"/>
        /// </summary>
        public const string swSelGTOLS = "GTOL";

        /// <summary>
        /// <see cref="IDisplayDimension"/>
        /// </summary>
        public const string swSelDIMENSIONS = "DIMENSION";

        /// <summary>
        /// <see cref="INote"/>
        /// </summary>
        public const string swSelNOTES = "NOTE";

        /// <summary>
        /// <see cref="IFeature"/> GetSpecificFeature2 <seealso cref="IDrSection"/>
        /// </summary>
        public const string swSelSECTIONLINES = "SECTIONLINE";

        /// <summary>
        /// <see cref="IFeature"/> GetSpecificFeature2 <seealso cref="IDetailCircle"/>
        /// </summary>
        public const string swSelDETAILCIRCLES = "DETAILCIRCLE";

        /// <summary>
        /// No Select interface And No SpecificData
        /// </summary>
        public const string swSelSECTIONTEXT = "SECTIONTEXT";

        /// <summary>
        /// <see cref="ISheet"/>
        /// </summary>
        public const string swSelSHEETS = "Sheet";

        /// <summary>
        /// <see cref="IComponent2"/>
        /// </summary>
        public const string swSelCOMPONENTS = "COMPONENT";

        /// <summary>
        /// <see cref="IFeature"/> <seealso cref="IMate2"/>
        /// </summary>
        public const string swSelMATES = "MATE";

        /// <summary>
        /// <see cref="IFeature"/>
        /// </summary>
        public const string swSelBODYFEATURES = "BODYFEATURE";

        /// <summary>
        /// <see cref="IFeature"/> <seealso cref="IReferenceCurve"/>
        /// </summary>
        public const string swSelREFCURVES = "REFCURVE";

        /// <summary>
        /// <see cref="ISketchSegment"/>
        /// </summary>
        public const string swSelEXTSKETCHSEGS = "EXTSKETCHSEGMENT";

        /// <summary>
        /// <see cref="ISketchPoint"/>
        /// </summary>
        public const string swSelEXTSKETCHPOINTS = "EXTSKETCHPOINT";

        /// <summary>
        /// No Select Interface And No SpecificData
        /// </summary>
        public const string swSelHELIX = "HELIX";

        /// <summary>
        /// <see cref="IFeature"/> <seealso cref="IReferenceCurve"/>
        /// </summary>
        public const string swSelREFERENCECURVES = "REFERENCECURVES";

        /// <summary>
        ///  no interface or data
        /// </summary>
        public const string swSelREFSURFACES = "REFSURFACE";

        /// <summary>
        ///  no interface or data
        /// </summary>
        public const string swSelCENTERMARKS = "CENTERMARKS";

        /// <summary>
        /// no interface or data
        /// </summary>
        public const string swSelINCONTEXTFEAT = "INCONTEXTFEAT";

        /// <summary>
        /// no interface or data
        /// </summary>
        public const string swSelMATEGROUP = "MATEGROUP";

        /// <summary>
        /// <see cref="IBreakLine"/>
        /// </summary>
        public const string swSelBREAKLINES = "BREAKLINE";

        /// <summary>
        /// 
        /// </summary>
        public const string swSelINCONTEXTFEATS = "INCONTEXTFEATS";

        /// <summary>
        /// 草图文字
        /// </summary>
        public const string swSelSKETCHTEXT = "SKETCHTEXT";

        /// <summary>
        /// 配合组
        /// </summary>
        public const string swSelMATEGROUPS = "MATEGROUPS";

        /// <summary>
        /// <see cref="ISFSymbol"/>
        /// </summary>
        public const string swSelSFSYMBOLS = "SFSYMBOL";

        /// <summary>
        /// <see cref="IDatumTag"/>
        /// </summary>
        public const string swSelDATUMTAGS = "DATUMTAG";

        /// <summary>
        /// 组件阵列
        /// </summary>
        public const string swSelCOMPPATTERN = "COMPPATTERN";

        /// <summary>
        /// <see cref="IWeldSymbol"/>
        /// </summary>
        public const string swSelWELDS = "WELD";

        /// <summary>
        /// See <see cref="Feature"/> Or ICThread4 || SeeAlso SpecificData <seealso cref="ICosmeticThreadFeatureData"/>
        /// </summary>
        public const string swSelCTHREADS = "CTHREAD";

        /// <summary>
        /// <see cref="IDatumTargetSym"/>
        /// </summary>
        public const string swSelDTMTARGS = "DTMTARG";

        /// <summary>
        /// 参考点
        /// </summary>
        public const string swSelPOINTREFS = "POINTREF";

        /// <summary>
        /// 
        /// </summary>
        public const string swSelDCABINETS = "DCABINET";

        /// <summary>
        /// 爆炸步骤
        /// </summary>
        public const string swSelEXPLSTEPS = "EXPLODESTEPS";

        /// <summary>
        /// See <see cref="ISilhouetteEdge"/>
        /// </summary>
        public const string swSelSILHOUETTES = "SILHOUETTE";

        /// <summary>
        /// See <see cref="IFeature"/> SeeAlso <seealso cref="IConfiguration"/>
        /// </summary>
        public const string swSelCONFIGURATIONS = "CONFIGURATIONS";

        /// <summary>
        /// nothing
        /// </summary>
        public const string swSelOBJHANDLES = "";

        /// <summary>
        /// <see cref="IProjectionArrow"/>
        /// </summary>
        public const string swSelARROWS = "VIEWARROW";

        /// <summary>
        /// 通过类型获取类型名 <see cref="swSelectType_e.swSelEDGES"/> => "EDGE" 
        /// </summary>
        /// <remarks>
        /// 并未实现完全
        /// </remarks>
        /// <param name="swSelectType"></param>
        /// <returns></returns>
        public string GetSelectedTypeName(swSelectType_e
             swSelectType)
        {
            string value = string.Empty;

            switch (swSelectType)
            {
                case swSelectType_e.swSelNOTHING:
                    break;
                case swSelectType_e.swSelEDGES:
                    value = swSelEDGES;
                    break;
                case swSelectType_e.swSelFACES:
                    value = swSelFACES;
                    break;
                case swSelectType_e.swSelVERTICES:
                    value = swSelVERTICES;
                    break;
                case swSelectType_e.swSelDATUMPLANES:
                    value = swSelDATUMPLANES;
                    break;
                case swSelectType_e.swSelDATUMAXES:
                    value = swSelDATUMAXES;
                    break;
                case swSelectType_e.swSelDATUMPOINTS:
                    value = swSelDATUMPOINTS;
                    break;
                case swSelectType_e.swSelOLEITEMS:
                    value = swSelOLEITEMS;
                    break;
                case swSelectType_e.swSelATTRIBUTES:
                    value = swSelATTRIBUTES;
                    break;
                case swSelectType_e.swSelSKETCHES:
                    value = swSelSKETCHES;
                    break;
                case swSelectType_e.swSelSKETCHSEGS:
                    value = swSelSKETCHSEGS;
                    break;
                case swSelectType_e.swSelSKETCHPOINTS:
                    value = swSelSKETCHPOINTS;
                    break;
                case swSelectType_e.swSelDRAWINGVIEWS:
                    value = swSelDRAWINGVIEWS;
                    break;
                case swSelectType_e.swSelGTOLS:
                    value = swSelGTOLS;
                    break;
                case swSelectType_e.swSelDIMENSIONS:
                    value = swSelDIMENSIONS;
                    break;
                case swSelectType_e.swSelNOTES:
                    value = swSelNOTES;
                    break;
                case swSelectType_e.swSelSECTIONLINES:
                    value = swSelSECTIONLINES;
                    break;
                case swSelectType_e.swSelDETAILCIRCLES:
                    value = swSelDETAILCIRCLES;
                    break;
                case swSelectType_e.swSelSECTIONTEXT:
                    value = swSelSECTIONTEXT;
                    break;
                case swSelectType_e.swSelSHEETS:
                    value = swSelSHEETS;
                    break;
                case swSelectType_e.swSelCOMPONENTS:
                    value = swSelCOMPONENTS;
                    break;
                case swSelectType_e.swSelMATES:
                    value = swSelMATES;
                    break;
                case swSelectType_e.swSelBODYFEATURES:
                    value = swSelBODYFEATURES;
                    break;
                case swSelectType_e.swSelREFCURVES:
                    value = swSelREFCURVES;
                    break;
                case swSelectType_e.swSelEXTSKETCHSEGS:
                    value = swSelEXTSKETCHSEGS;
                    break;
                case swSelectType_e.swSelEXTSKETCHPOINTS:
                    value = swSelEXTSKETCHPOINTS;
                    break;
                case swSelectType_e.swSelHELIX:
                    value = swSelHELIX;
                    break;
                //case swSelectType_e.swSelREFERENCECURVES:
                //   break;
                case swSelectType_e.swSelREFSURFACES:
                    value = swSelREFSURFACES;
                    break;
                case swSelectType_e.swSelCENTERMARKS:
                    value = swSelCENTERMARKS;
                    break;
                case swSelectType_e.swSelINCONTEXTFEAT:
                    value = swSelINCONTEXTFEAT;
                    break;
                case swSelectType_e.swSelMATEGROUP:
                    value = swSelMATEGROUP;
                    break;
                case swSelectType_e.swSelBREAKLINES:
                    value = swSelBREAKLINES;
                    break;
                case swSelectType_e.swSelINCONTEXTFEATS:
                    value = swSelINCONTEXTFEATS;
                    break;
                case swSelectType_e.swSelMATEGROUPS:
                    value = swSelMATEGROUPS;
                    break;
                case swSelectType_e.swSelSKETCHTEXT:
                    value = swSelSKETCHTEXT;
                    break;
                case swSelectType_e.swSelSFSYMBOLS:
                    value = swSelSFSYMBOLS;
                    break;
                case swSelectType_e.swSelDATUMTAGS:
                    value = swSelDATUMTAGS;
                    break;
                case swSelectType_e.swSelCOMPPATTERN:
                    value = swSelCOMPPATTERN;
                    break;
                case swSelectType_e.swSelWELDS:
                    value = swSelWELDS;
                    break;
                case swSelectType_e.swSelCTHREADS:
                    value = swSelCTHREADS;
                    break;
                case swSelectType_e.swSelDTMTARGS:
                    value = swSelDTMTARGS;
                    break;
                case swSelectType_e.swSelPOINTREFS:
                    value = swSelPOINTREFS;

                    break;
                case swSelectType_e.swSelDCABINETS:
                    value = swSelDCABINETS;
                    break;
                //case swSelectType_e.swSelEXPLVIEWS:
                //    value = swSelEXPLVIEWS;
                //    break;
                case swSelectType_e.swSelEXPLSTEPS:
                    value = swSelEXPLSTEPS;
                    break;
                case swSelectType_e.swSelEXPLLINES:
                    //value = swSelEXPLLINES;
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
                //case swSelectType_e.swSelCOMPSDONTOVERRIDE:
                //    break;
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
                //case swSelectType_e.swSelHOLESERIES:
                //    break;
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
                //case swSelectType_e.swSelPUNCHTABLEFEATS:
                //    break;
                case swSelectType_e.swSelREVISIONCLOUDS:
                    break;
                case swSelectType_e.swSelBorder:
                    break;
                //case swSelectType_e.swSelSELECTIONSETFOLDER:
                //    break;
                //case swSelectType_e.swSelSELECTIONSETNODE:
                //    break;
                case swSelectType_e.swSelEVERYTHING:
                    break;
                case swSelectType_e.swSelLOCATIONS:
                    break;
                case swSelectType_e.swSelUNSUPPORTED:
                    break;
                default:
                    throw new InvalidOperationException($"not support type :{swSelectType.ToString()}");
            }
            return value;
        }
    }
}
