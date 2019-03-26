/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/ManagedSDKExample.cs $
|
|  $Copyright: (c) 2018 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Bentley.DgnPlatformNET;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.LinearGeometry;

namespace ManagedSDKExample
    {
    /*=====================================================================================**/
    /* Required | Implementation of Addin Class            
    /*=====================================================================================**/
    [Bentley.MstnPlatformNET.AddInAttribute(MdlTaskID = "ManagedSDKExample")]
    public sealed class ManagedSDKExample : Bentley.MstnPlatformNET.AddIn
        {
        private static ManagedSDKExample s_managedSDKExample = null;

        public ManagedSDKExample(System.IntPtr mdlDesc)
            : base(mdlDesc)
            {
            s_managedSDKExample = this;
            }

        protected override int Run(string[] commandLine)
            {
            return 0;
            }

        internal static ManagedSDKExample Instance()
            {
            return s_managedSDKExample;
            }

        /*=====================================================================================**/
        /* Functions Mapped to Commands           
        /*=====================================================================================**/

        /* MANAGEDSDKEXAMPLE -> REPORT -> HORIZONTALALIGNMENTS
        /* MANAGEDSDKEXAMPLE -> REPORT -> HORIZONTALALIGNMENTS -> ASANNOTATION
        /*------------------------------------------------------------------------------------**/
        internal void HorizontalAlignmentReport(bool reportAsAnnotation)
            {
            HorizontalAlignmentReporter hRep = new HorizontalAlignmentReporter();

            if (reportAsAnnotation)
                hRep.AnnotateAllAlignments();
            else
                hRep.ReportAllAlignments();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> HORIZONTALALIGNMENTS -> ITEMTYPES
        /*--------------+---------------+---------------+---------------+---------------+------*/
        internal void HorizontalAlignmentItemTypes()
            {
            HorizontalAlignmentReporter hRep = new HorizontalAlignmentReporter();
            hRep.ReportAllAlignmentsItemTypes();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> HORIZONTALALIGNMENTS -> PICKALIGNMENT
        /*--------------+---------------+---------------+---------------+---------------+------*/
        internal void PickAlignment()
            {
            PickAlignmentTool.InstallNewInstance();
            }
        
        /* MANAGEDSDKEXAMPLE -> REPORT -> STATIONELEVATIONS
        /*------------------------------------------------------------------------------------**/
        internal void StationElevationsReport()
            {
            VerticalAlignmentReporter vRep = new VerticalAlignmentReporter();
            vRep.StationElevationsReport();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> PROFILEELEMENTS
        /*------------------------------------------------------------------------------------**/
        internal void ProfileElementReport()
            {
            VerticalAlignmentReporter vRep = new VerticalAlignmentReporter();
            vRep.ProfileElementReport();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> ACTIVESTATIONELEVATION
        /*------------------------------------------------------------------------------------**/
        internal void ActiveStationElevationAtInterval()
            {
            VerticalAlignmentReporter vRep = new VerticalAlignmentReporter();
            vRep.ActiveStationElevationAtIntervalReport();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> CORRIDORS
        /*------------------------------------------------------------------------------------**/
        internal void CorridorReport()
            {
            CorridorReporter cRep = new CorridorReporter();
            cRep.ReportAllCorridors();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> STATIONOFFSETELEVATION
        /*------------------------------------------------------------------------------------**/
        internal void StationOffsetElevation()
            {
            TerrainReporter tRep = new TerrainReporter();
            tRep.StationOffsetElevation();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> DRAPELINEATINTERVAL
        /*------------------------------------------------------------------------------------**/
        internal void DrapeLineAtInterval()
            {
            TerrainReporter tRep = new TerrainReporter();
            tRep.DrapeLineAtInterval();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> QQCHECKER
        /*------------------------------------------------------------------------------------**/
        internal void QQCheckerReport()
            {
            QQChecker.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> XSCUTPOINTS
        /* MANAGEDSDKEXAMPLE -> REPORT -> XSCUTPOINTS -> PICKCORRIDOR
        /*------------------------------------------------------------------------------------**/
        internal void XSCutPointsReportPickCorridor()
            {
            XSCutPointReporter.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> XSCUTPOINTS -> ALLCORRIDORS
        /*------------------------------------------------------------------------------------**/
        internal void XSCutPointsReportAllCorridors()
            {
            XSCutPointReporter cpRep = new XSCutPointReporter();
            cpRep.ReportAllXSCutPointsAllCorridors();
            }



        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS
        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> LINE
        /*------------------------------------------------------------------------------------**/
        internal void HorizontalAlignmentsCreator()
            {
            HorizontalAlignmentCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> ARC
        /*------------------------------------------------------------------------------------**/
        internal void ArcCreators()
            {
            ArcCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> SPIRAL
        /*------------------------------------------------------------------------------------**/
        internal void SpiralCreators()
            {
            SpiralCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> COMPLEX ALIGNMENT
        /*------------------------------------------------------------------------------------**/
        internal void ComplexAlignmentCreators()
            {
            ComplexAlignmentCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> PI ALIGNMENT
        /*------------------------------------------------------------------------------------**/
        internal void PIAlignmentCreators()
            {
            PIAlignmentCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> CURB RETURNS
        /*------------------------------------------------------------------------------------**/
        internal void CurbReturnsCreators()
            {
            HorizontalCurbReturns.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> VERTICALALIGNMENTS
        /* MANAGEDSDKEXAMPLE -> CREATE -> VERTICALALIGNMENTS -> LINE
        /*------------------------------------------------------------------------------------**/
        internal void VerticalLineCreators()
            {
            VerticalAlignmentCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> VERTICALALIGNMENTS -> PARABOLA
        /*------------------------------------------------------------------------------------**/
        internal void VerticalParabolaCreators()
            {
            VerticalParabolaCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> VERTICALALIGNMENTS -> COMPLEX ALIGNMENT
        /*------------------------------------------------------------------------------------**/
        internal void VerticalComplexAlignmentCreators()
            {
            VerticalComplexAlignmentCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> CORRIDORITEMS -> COMPLEX ALIGNMENT
        /*------------------------------------------------------------------------------------**/
        internal void CorridorItemsComplexAlignmentCreators()
            {
            CorridorItemsComplexAlignmentCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> CORRIDORITEMS -> FEATUREDEFINITION
        /*------------------------------------------------------------------------------------**/
        internal void CorridorItemsFeatureDefinitionSetter()
            {
            CorridorItemsFeatureDefinitionSet.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> CORRIDORITEMS -> PROFILE
        /*------------------------------------------------------------------------------------**/
        internal void CorridorItemsProfileCreators()
            {
            CorridorItemsProfileCreator.InstallNewInstance();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> CORRIDORITEMS -> CORRIDOR
        /*------------------------------------------------------------------------------------**/
        internal void CorridorItemsCorridorCreators()
            {
            CorridorItemsCorridorCreator.InstallNewInstance();
            }

        }
    }
