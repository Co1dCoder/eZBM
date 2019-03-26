/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/Examples/QQChecker.cs $
|
|  $Copyright: (c) 2018 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.MstnPlatformNET;
using System.Windows.Forms;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.SDK;
using Bentley.CifNET.LinearGeometry;

namespace ManagedSDKExample
    {
    class QQChecker : DgnElementSetTool
        {
        ConsensusConnection m_con;

        public QQChecker() : base()
            {
            }

        protected override void OnRestartTool()
            {
            InstallNewInstance();
            }

        protected override void ExitTool()
            {
            if (m_con != null)
                m_con.Dispose();
            m_con = null;
            base.ExitTool();
            }


        protected override void OnPostInstall()
            {
            base.BeginPickElements();
            Bentley.DgnPlatformNET.AccuSnap.LocateEnabled = true;
            Bentley.DgnPlatformNET.AccuSnap.SnapEnabled = true;
            m_con = new ConsensusConnection(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef());
            NotificationManager.OutputPrompt("Select an alignment to report.");
            base.OnPostInstall();
            }


        protected override bool OnDataButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
            {
            Bentley.DgnPlatformNET.HitPath hitPath = DoLocate(ev, true, 1);
            if (hitPath == null)
                return false;

            Element el = hitPath.GetCursorElement();
            
            if (el == null)
                return false;
            
            Alignment al = (el.ParentElement == null) ? Alignment.CreateFromElement(m_con, el) : Alignment.CreateFromElement(m_con, el.ParentElement);

            if (al == null)
                {
                if (el.ElementType == MSElementType.LineString)
                    TerrainReport3dLineString(el);
                else
                    return false;
                }
            else
                TerrainReportProfile(al);

            return true;
            }

        public override Bentley.DgnPlatformNET.StatusInt OnElementModify(Bentley.DgnPlatformNET.Elements.Element element)
            {
            return Bentley.DgnPlatformNET.StatusInt.Error;
            }

        protected override bool OnResetButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
            {
            ExitTool();
            return true;
            }


        public static void InstallNewInstance()
            {
            QQChecker tool = new QQChecker();
            tool.InstallTool();
            }

        /*------------------------------------------------------------------------------------**/
        /* Creates report of difference between terrain and alignment elevations.
        /*--------------+---------------+---------------+---------------+---------------+------*/
        public void TerrainReportProfile(Alignment al)
            {
            ConsensusConnection sdkCon = new ConsensusConnection(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef());

            if (sdkCon == null)
                return;

            GeometricModel geomModel = sdkCon.GetActiveGeometricModel();
            if (geomModel == null)
                return;

            SurfaceEntity activeSurface = geomModel.ActiveSurface;

            ReportResultsForm report = new ReportResultsForm();
            report.SetReportName("QQ String Checker Report");

            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("File Name", geomModel.DgnModel.GetDgnFile().GetFileName());
            header.Add("Model Name", geomModel.DgnModel.ModelName);
            string surfaceName = "No Active Terrain";
            if (activeSurface != null)
                {
                surfaceName = string.IsNullOrEmpty(activeSurface.Name) ? "Unnamed" : activeSurface.Name;
                }
            header.Add("Active Terrain Name", surfaceName);
            report.PrintValueTable(header);

            StringBuilder sb = new StringBuilder();

            StationFormatSettings settings = StationFormatSettings.GetStationFormatSettingsForModel(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef() as DgnModel);
            StationingFormatter formatter = new StationingFormatter(al);
            Profile activeProfile = al.ActiveProfile;

            sb.Clear();

            sb.Append("Horizontal Alignment: ");
            sb.Append(string.IsNullOrEmpty(al.Name) ? "Unnamed" : al.Name);
            sb.Append(report.NewLineCharacter);
            sb.Append("Active Vertical Alignment Name: ");
            if (activeProfile != null)
                {
                sb.Append(string.IsNullOrEmpty(activeProfile.Name) ? "Unnamed" : activeProfile.Name);
                }
            else
                {
                sb.Append("No Active Vertical Alignment");
                }
            report.AddHeader(sb.ToString());

            List<ReportData> dataContainer = new List<ReportData>();
            ReportData data;
            int pointCount = 0;

            //row headers
            data.newRow = false;
            data.type = ReportDataType.header;
            data.data = "PT#";
            dataContainer.Add(data);
            data.data = "X";
            dataContainer.Add(data);
            data.data = "Y";
            dataContainer.Add(data);
            data.data = "Length";
            dataContainer.Add(data);
            data.data = "Check Elevation";
            dataContainer.Add(data);
            data.data = "Terrain Elevation";
            dataContainer.Add(data);
            data.data = "Elevation Difference";
            dataContainer.Add(data);

            if (activeSurface != null && activeSurface is TerrainSurface)
                {
                TerrainSurface surface = activeSurface as TerrainSurface;

                List<LinearElement> segments = new List<LinearElement>();
                if (al.LinearGeometry is LinearComplex)
                    {
                    LinearComplex lc = (al.LinearGeometry as LinearComplex);
                    foreach (LinearElement le in lc.GetSubLinearElements())
                        {
                        segments.Add(le);
                        }
                    }
                else if (al.LinearGeometry is LineString)
                    {
                    LineString ls = (al.LinearGeometry as LineString);
                    Bentley.GeometryNET.DPoint3d[] vertices = ls.GetVertices();
                    for (int i = 0; i < vertices.Length - 1; i++)
                        {
                        segments.Add(new Line3d(vertices[i], vertices[i + 1]));
                        }
                    }
                else
                    {
                    segments.Add(al.LinearGeometry);
                    }

                //do first point before loop, only have to worry about second point in each linear element
                Bentley.TerrainModelNET.DTMDrapedPoint start = surface.DTM?.DrapePoint(segments[0].StartPoint.Coordinates);
                string xCoordinate = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(segments[0].StartPoint.Coordinates.X));
                string yCoordinate = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(segments[0].StartPoint.Coordinates.Y));
                string length = "";
                string terrainElevation = "";
                if (start != null)
                    {
                    switch (start.Code)
                        {
                        case Bentley.TerrainModelNET.DTMDrapedPointCode.PointOrSide:
                        case Bentley.TerrainModelNET.DTMDrapedPointCode.Triangle:
                            terrainElevation = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(start.Coordinates.Z));
                            break;
                        case Bentley.TerrainModelNET.DTMDrapedPointCode.External:
                            terrainElevation = "<font color=\"ff0000\"><em>External</em></font>";
                            break;
                        case Bentley.TerrainModelNET.DTMDrapedPointCode.Void:
                            terrainElevation = "<font color=\"ff0000\"><em>Void</em></font>";
                            break;
                        default:
                            break;
                        }
                    }

                double elevation = Double.NaN;
                double distanceAlong = Double.NaN;
                string checkElevation = "";
                string elevationDifference = "";
                if (al.ActiveProfile != null)
                    {
                    distanceAlong = segments[0].StartPoint.DistanceAlong;
                    elevation = al.ActiveProfile.ProfileGeometry.GetPointAtX(distanceAlong).Coordinates.Y;
                    checkElevation = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(elevation));
                    if (start != null && (start.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.PointOrSide || start.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.Triangle))
                        elevationDifference = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(start.Coordinates.Z - elevation));
                    }
                pointCount++;

                //create first data row
                data.data = pointCount.ToString();
                data.newRow = true;
                data.type = ReportDataType.data;
                dataContainer.Add(data);
                data.data = xCoordinate;
                data.newRow = false;
                dataContainer.Add(data);
                data.data = yCoordinate;
                dataContainer.Add(data);
                data.data = length;
                dataContainer.Add(data);
                data.data = checkElevation;
                dataContainer.Add(data);
                data.data = terrainElevation;
                dataContainer.Add(data);
                data.data = elevationDifference;
                dataContainer.Add(data);

                foreach (LinearElement le in segments)
                    {
                    Bentley.TerrainModelNET.DTMDrapedPoint end = surface.DTM?.DrapePoint(le.EndPoint.Coordinates);
                    xCoordinate = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(le.EndPoint.Coordinates.X));
                    yCoordinate = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(le.EndPoint.Coordinates.Y));
                    length = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(le.Length));
                    terrainElevation = "";
                    if (end != null)
                        {
                        switch (end.Code)
                            {
                            case Bentley.TerrainModelNET.DTMDrapedPointCode.PointOrSide:
                            case Bentley.TerrainModelNET.DTMDrapedPointCode.Triangle:
                                terrainElevation = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(end.Coordinates.Z));
                                break;
                            case Bentley.TerrainModelNET.DTMDrapedPointCode.External:
                                terrainElevation = "<font color=\"ff0000\"><em>External</em></font>";
                                break;
                            case Bentley.TerrainModelNET.DTMDrapedPointCode.Void:
                                terrainElevation = "<font color=\"ff0000\"><em>Void</em></font>";
                                break;
                            default:
                                break;
                            }
                        }

                    elevation = Double.NaN;
                    checkElevation = "";
                    elevationDifference = "";
                    if (al.ActiveProfile != null)
                        {
                        distanceAlong += le.EndPoint.DistanceAlong;
                        elevation = al.ActiveProfile.ProfileGeometry.GetPointAtX(distanceAlong).Coordinates.Y;
                        checkElevation = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(elevation));
                        if (end != null && (end.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.PointOrSide || end.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.Triangle))
                            elevationDifference = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(end.Coordinates.Z - elevation));
                        }
                    pointCount++;

                    //create data row
                    data.data = pointCount.ToString();
                    data.newRow = true;
                    data.type = ReportDataType.data;
                    dataContainer.Add(data);
                    data.data = xCoordinate;
                    data.newRow = false;
                    dataContainer.Add(data);
                    data.data = yCoordinate;
                    dataContainer.Add(data);
                    data.data = length;
                    dataContainer.Add(data);
                    data.data = checkElevation;
                    dataContainer.Add(data);
                    data.data = terrainElevation;
                    dataContainer.Add(data);
                    data.data = elevationDifference;
                    dataContainer.Add(data);
                    }
                }
            report.PrintReportData(dataContainer);
            report.Finished();
            }

        public void TerrainReport3dLineString(Element el)
            {
            ConsensusConnection sdkCon = new ConsensusConnection(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef());

            if (sdkCon == null)
                return;

            GeometricModel geomModel = sdkCon.GetActiveGeometricModel();
            if (geomModel == null)
                return;

            SurfaceEntity activeSurface = geomModel.ActiveSurface;

            ReportResultsForm report = new ReportResultsForm();
            report.SetReportName("QQ String Checker Report");

            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("File Name", geomModel.DgnModel.GetDgnFile().GetFileName());
            header.Add("Model Name", geomModel.DgnModel.ModelName);
            string surfaceName = "No Active Terrain";
            if (activeSurface != null)
                {
                surfaceName = string.IsNullOrEmpty(activeSurface.Name) ? "Unnamed" : activeSurface.Name;
                }
            header.Add("Active Terrain Name", surfaceName);
            report.PrintValueTable(header);

            StringBuilder sb = new StringBuilder();

            sb.Clear();

            sb.Append("Horizontal Alignment: ");
            sb.Append("Unnamed");
            sb.Append(report.NewLineCharacter);
            sb.Append("Active Vertical Alignment Name: ");
            sb.Append("No Active Vertical Alignment");
            report.AddHeader(sb.ToString());

            List<ReportData> dataContainer = new List<ReportData>();
            ReportData data;
            int pointCount = 0;

            //row headers
            data.newRow = false;
            data.type = ReportDataType.header;
            data.data = "PT#";
            dataContainer.Add(data);
            data.data = "X";
            dataContainer.Add(data);
            data.data = "Y";
            dataContainer.Add(data);
            data.data = "Length";
            dataContainer.Add(data);
            data.data = "Check Elevation";
            dataContainer.Add(data);
            data.data = "Terrain Elevation";
            dataContainer.Add(data);
            data.data = "Elevation Difference";
            dataContainer.Add(data);

            if (activeSurface != null && activeSurface is TerrainSurface && el.ElementType == MSElementType.LineString)
                {
                TerrainSurface surface = activeSurface as TerrainSurface;

                List<LinearElement> segments = new List<LinearElement>();

                Bentley.DgnPlatformNET.Elements.LineStringElement lineString = el as Bentley.DgnPlatformNET.Elements.LineStringElement;
                List<Bentley.GeometryNET.DPoint3d> pointList = new List<Bentley.GeometryNET.DPoint3d>();
                Bentley.GeometryNET.CurvePrimitive curvePrim = lineString.GetCurveVector().GetPrimitive(0);
                curvePrim.TryGetLineString(pointList);

                for (int i = 0; i < pointList.Count - 1; i++)
                    {
                    Bentley.GeometryNET.DPoint3d point1 = pointList[i];
                    Bentley.GeometryNET.DPoint3d point2 = pointList[i + 1];
                    point1.Set(GeometryHelper.ConvertUORToMeter(point1.X), GeometryHelper.ConvertUORToMeter(point1.Y), GeometryHelper.ConvertUORToMeter(point1.Z));
                    point2.Set(GeometryHelper.ConvertUORToMeter(point2.X), GeometryHelper.ConvertUORToMeter(point2.Y), GeometryHelper.ConvertUORToMeter(point2.Z));
                    segments.Add(new Line3d(point1, point2));
                    }

                //do first point before loop, only have to worry about second point in each linear element
                Bentley.TerrainModelNET.DTMDrapedPoint start = surface.DTM?.DrapePoint(segments[0].StartPoint.Coordinates);
                string xCoordinate = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(segments[0].StartPoint.Coordinates.X));
                string yCoordinate = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(segments[0].StartPoint.Coordinates.Y));
                string length = "";
                string terrainElevation = "";
                if (start != null)
                    {
                    switch (start.Code)
                        {
                        case Bentley.TerrainModelNET.DTMDrapedPointCode.PointOrSide:
                        case Bentley.TerrainModelNET.DTMDrapedPointCode.Triangle:
                            terrainElevation = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(start.Coordinates.Z));
                            break;
                        case Bentley.TerrainModelNET.DTMDrapedPointCode.External:
                            terrainElevation = "<font color=\"ff0000\"><em>External</em></font>";
                            break;
                        case Bentley.TerrainModelNET.DTMDrapedPointCode.Void:
                            terrainElevation = "<font color=\"ff0000\"><em>Void</em></font>";
                            break;
                        default:
                            break;
                        }
                    }

                double elevation = Double.NaN;
                double distanceAlong = Double.NaN;
                string checkElevation = "";
                string elevationDifference = "";
                distanceAlong = segments[0].StartPoint.DistanceAlong;
                elevation = segments[0].StartPoint.Coordinates.Z;
                checkElevation = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(elevation));
                if (start != null && (start.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.PointOrSide || start.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.Triangle))
                    elevationDifference = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(start.Coordinates.Z - elevation));
                pointCount++;

                //create first data row
                data.data = pointCount.ToString();
                data.newRow = true;
                data.type = ReportDataType.data;
                dataContainer.Add(data);
                data.data = xCoordinate;
                data.newRow = false;
                dataContainer.Add(data);
                data.data = yCoordinate;
                dataContainer.Add(data);
                data.data = length;
                dataContainer.Add(data);
                data.data = checkElevation;
                dataContainer.Add(data);
                data.data = terrainElevation;
                dataContainer.Add(data);
                data.data = elevationDifference;
                dataContainer.Add(data);

                foreach (LinearElement le in segments)
                    {
                    Bentley.TerrainModelNET.DTMDrapedPoint end = surface.DTM?.DrapePoint(le.EndPoint.Coordinates);
                    xCoordinate = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(le.EndPoint.Coordinates.X));
                    yCoordinate = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(le.EndPoint.Coordinates.Y));
                    length = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(le.Length));
                    terrainElevation = "";
                    if (end != null)
                        {
                        switch (end.Code)
                            {
                            case Bentley.TerrainModelNET.DTMDrapedPointCode.PointOrSide:
                            case Bentley.TerrainModelNET.DTMDrapedPointCode.Triangle:
                                terrainElevation = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(end.Coordinates.Z));
                                break;
                            case Bentley.TerrainModelNET.DTMDrapedPointCode.External:
                                terrainElevation = "<font color=\"ff0000\"><em>External</em></font>";
                                break;
                            case Bentley.TerrainModelNET.DTMDrapedPointCode.Void:
                                terrainElevation = "<font color=\"ff0000\"><em>Void</em></font>";
                                break;
                            default:
                                break;
                            }
                        }

                    elevation = Double.NaN;
                    checkElevation = "";
                    elevationDifference = "";
                    distanceAlong += le.EndPoint.DistanceAlong;
                    elevation = le.EndPoint.Coordinates.Z;
                    checkElevation = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(elevation));
                    if (end != null && (end.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.PointOrSide || end.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.Triangle))
                        elevationDifference = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(end.Coordinates.Z - elevation));
                    pointCount++;

                    //create data row
                    data.data = pointCount.ToString();
                    data.newRow = true;
                    data.type = ReportDataType.data;
                    dataContainer.Add(data);
                    data.data = xCoordinate;
                    data.newRow = false;
                    dataContainer.Add(data);
                    data.data = yCoordinate;
                    dataContainer.Add(data);
                    data.data = length;
                    dataContainer.Add(data);
                    data.data = checkElevation;
                    dataContainer.Add(data);
                    data.data = terrainElevation;
                    dataContainer.Add(data);
                    data.data = elevationDifference;
                    dataContainer.Add(data);
                    }
                }
            report.PrintReportData(dataContainer);
            report.Finished();
            }
        }
    }
