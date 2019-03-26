/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/Keyin.cs $
|
|  $Copyright: (c) 2018 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

namespace ManagedSDKExample
    {
    /*=====================================================================================**/
    /* Required | Keyin Class            
    /*=====================================================================================**/
    // Interface between CommandTable.xml and ManagedSDKExample
    public sealed class Keyin
        {

        /* MANAGEDSDKEXAMPLE -> REPORT -> HORIZONTALALIGNMENTS
        /*------------------------------------------------------------------------------------**/
        public static void CmdHorizontalAlignmentReport(string unparsed)
            {
            ManagedSDKExample.Instance().HorizontalAlignmentReport(false);
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> HORIZONTALALIGNMENTS -> ASANNOTATION
        /*------------------------------------------------------------------------------------**/
        public static void CmdHorizontalAlignmentAnnotation(string unparsed)
            {
            ManagedSDKExample.Instance().HorizontalAlignmentReport(true);
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> HORIZONTALALIGNMENTS -> ITEMTYPES
        /*------------------------------------------------------------------------------------**/
        public static void CmdHorizontalAlignmentItemTypes(string unparsed)
            {
            ManagedSDKExample.Instance().HorizontalAlignmentItemTypes();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> HORIZONTALALIGNMENTS -> PICKALIGNMENT
        /*--------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdPickAlignment(string unparsed)
            {
            ManagedSDKExample.Instance().PickAlignment();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> STATIONELEVATIONS
        /*------------------------------------------------------------------------------------**/
        public static void CmdStationElevationsReport(string unparsed)
            {
            ManagedSDKExample.Instance().StationElevationsReport();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> PROFILEELEMENTS
        /*------------------------------------------------------------------------------------**/
        public static void CmdProfileElementReport(string unparsed)
            {
            ManagedSDKExample.Instance().ProfileElementReport();
            }


        /* MANAGEDSDKEXAMPLE -> REPORT -> ACTIVESTATIONELEVATION
        /*------------------------------------------------------------------------------------**/
        public static void CmdActiveStationElevationAtInterval(string unparsed)
            {
            ManagedSDKExample.Instance().ActiveStationElevationAtInterval();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> CORRIDORS
        /*------------------------------------------------------------------------------------**/
        public static void CmdCorridorReport(string unparsed)
            {
            ManagedSDKExample.Instance().CorridorReport();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> STATIONOFFSETELEVATION
        /*------------------------------------------------------------------------------------**/
        public static void CmdStationOffsetElevation(string unparsed)
            {
            ManagedSDKExample.Instance().StationOffsetElevation();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> DRAPELINEATINTERVAL
        /*------------------------------------------------------------------------------------**/
        public static void CmdDrapeLineAtInterval(string unparsed)
            {
            ManagedSDKExample.Instance().DrapeLineAtInterval();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> QQCHECKER
        /*------------------------------------------------------------------------------------**/
        public static void CmdQQChecker(string unparsed)
            {
            ManagedSDKExample.Instance().QQCheckerReport();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> XSCUTPOINTREPORT -> PICKCORRIDOR
        /*------------------------------------------------------------------------------------**/
        public static void CmdXSCutPointReportPickCorridor(string unparsed)
            {
            ManagedSDKExample.Instance().XSCutPointsReportPickCorridor();
            }

        /* MANAGEDSDKEXAMPLE -> REPORT -> XSCUTPOINTREPORT -> ALLCORRIDORS
        /*------------------------------------------------------------------------------------**/
        public static void CmdXSCutPointReportAllCorridors(string unparsed)
            {
            ManagedSDKExample.Instance().XSCutPointsReportAllCorridors();
            }



        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS
        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> LINE
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateHorizontalAlignmentsLine(string unparsed)
            {
            ManagedSDKExample.Instance().HorizontalAlignmentsCreator();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> ARC
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateHorizontalAlignmentsArc(string unparsed)
            {
            ManagedSDKExample.Instance().ArcCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> SPIRAL
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateHorizontalAlignmentsSpiral(string unparsed)
            {
            ManagedSDKExample.Instance().SpiralCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> COMPLEX ALIGNMENT
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateHorizontalAlignmentsComplexAlignment(string unparsed)
            {
            ManagedSDKExample.Instance().ComplexAlignmentCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> PI ALIGNMENT
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateHorizontalAlignmentsPIAlignment(string unparsed)
            {
            ManagedSDKExample.Instance().PIAlignmentCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> HORIZONTALALIGNMENTS -> CURB RETURNS
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateHorizontalAlignmentsCurbReturns(string unparsed)
            {
            ManagedSDKExample.Instance().CurbReturnsCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> VERTICALALIGNMENTS
        /* MANAGEDSDKEXAMPLE -> CREATE -> VERTICALALIGNMENTS -> LINE
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateVerticalAlignmentsLine(string unparsed)
            {
            ManagedSDKExample.Instance().VerticalLineCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> VERTICALALIGNMENTS -> PARABOLA
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateVerticalAlignmentsParabola(string unparsed)
            {
            ManagedSDKExample.Instance().VerticalParabolaCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> VERTICALALIGNMENTS -> COMPLEX ALIGNMENT
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateVerticalAlignmentsComplexAlignment(string unparsed)
            {
            ManagedSDKExample.Instance().VerticalComplexAlignmentCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> CORRIDORITEMS -> COMPLEX ALIGNMENT
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateCorridorItemsComplexAlignment(string unparsed)
            {
            ManagedSDKExample.Instance().CorridorItemsComplexAlignmentCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> CORRIDORITEMS -> SETFEATUREDEFINITION
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateCorridorItemsSetFeatureDefinition(string unparsed)
            {
            ManagedSDKExample.Instance().CorridorItemsFeatureDefinitionSetter();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> CORRIDORITEMS -> PROFILE
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateCorridorItemsProfile(string unparsed)
            {
            ManagedSDKExample.Instance().CorridorItemsProfileCreators();
            }

        /* MANAGEDSDKEXAMPLE -> CREATE -> CORRIDORITEMS -> CORRIDOR
        /*------------------------------------------------------------------------------------**/
        public static void CmdCreateCorridorItemsCorridor(string unparsed)
            {
            ManagedSDKExample.Instance().CorridorItemsCorridorCreators();
            }

        }
    }

