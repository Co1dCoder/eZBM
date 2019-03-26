/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/Examples/TerrainReporter.cs $
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
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.SDK;
using Bentley.CifNET.LinearGeometry;

namespace ManagedSDKExample
    {
    class TerrainReporter
        {
        /*------------------------------------------------------------------------------------**/
        /* Reads alignments and creates report of terrain elevations at an interval.
        /*--------------+---------------+---------------+---------------+---------------+------*/
        internal void StationOffsetElevation()
            {
            List<string> labels = new List<string>() { "STATION", "OFFSET", "NORTHING", "EASTING", "TERRAIN ELEVATION" };
            SortedDictionary<double, string> data = new SortedDictionary<double, string>();
            double interval = GeometryHelper.ConvertMasterToMeter(100.0);
            double offset = GeometryHelper.ConvertMasterToMeter(50.0);
            StringBuilder sb = new StringBuilder();

            ConsensusConnection sdkCon = new ConsensusConnection(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef());

            if (sdkCon == null)
                return;

            GeometricModel geomModel = sdkCon.GetActiveGeometricModel();
            if (geomModel == null)
                return;

            SurfaceEntity activeSurface = geomModel.ActiveSurface;

            ReportResultsForm report = new ReportResultsForm();
            report.SetReportName("Station Offset Northing Easting Elevation Interval Report at an offset");

            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("File Name", geomModel.DgnModel.GetDgnFile().GetFileName());
            header.Add("Model Name", geomModel.DgnModel.ModelName);
            string surfaceName = "No Active Terrain";
            if (activeSurface != null)
                {
                surfaceName = string.IsNullOrEmpty(activeSurface.Name) ? "Unnamed" : activeSurface.Name;
                }
            header.Add("Active Terrain Name", surfaceName);
            header.Add("Interval", "100");
            header.Add("Offset", "50");
            report.PrintValueTable(header);

            StationFormatSettings settings = StationFormatSettings.GetStationFormatSettingsForModel(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef() as DgnModel);
            foreach (Alignment al in geomModel.Alignments)
                {
                if (!al.IsFinalElement)
                    {
                    continue;
                    }

                StationingFormatter formatter = new StationingFormatter(al);
                Profile activeProfile = al.ActiveProfile;

                data.Clear();
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

                for (double station = 0.0; station < al.LinearGeometry.Length; station += interval)
                    {
                    if (!data.ContainsKey(station))
                        {
                        LinearPoint point = al.LinearGeometry.GetPointAtDistanceOffset(station, offset);

                        string stationValue = "";
                        formatter.FormatStation(ref stationValue, station, settings);
                        string offsetValue = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(offset));
                        string northingValue = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(point.Coordinates.Y));
                        string eastingValue = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(point.Coordinates.X));
                        string elevationValue = "No Terrain";
                        if (activeSurface != null)
                            {
                            if (activeSurface is TerrainSurface)
                                {
                                TerrainSurface surface = activeSurface as TerrainSurface;
                                Bentley.TerrainModelNET.DTMDrapedPoint drapedPoint = surface.DTM?.DrapePoint(point.Coordinates);
                                if (drapedPoint != null)
                                    {
                                    if (drapedPoint.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.PointOrSide ||
                                        drapedPoint.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.Triangle)
                                        {
                                        elevationValue = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(drapedPoint.Coordinates.Z));
                                        }
                                    else if (drapedPoint.Code == Bentley.TerrainModelNET.DTMDrapedPointCode.Void)
                                        {
                                        elevationValue = "In void";
                                        }
                                    else
                                        {
                                        elevationValue = "Outside terrain";
                                        }
                                    }
                                }
                            }

                        data.Add(station, string.Format("{0}|{1}|{2}|{3}|{4}", stationValue, offsetValue, northingValue, eastingValue, elevationValue));
                        }
                    }

                report.PrintValueTable(labels, data.Values.ToList());
                }
            report.Finished();
            }

        /*------------------------------------------------------------------------------------**/
        /* Reads alignments and creates report of draped cross-section lines at given offsets and intervals.
        /*--------------+---------------+---------------+---------------+---------------+------*/
        internal void DrapeLineAtInterval()
            {
            ConsensusConnection sdkCon = new ConsensusConnection(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef());

            if (sdkCon == null)
                return;

            GeometricModel geomModel = sdkCon.GetActiveGeometricModel();
            if (geomModel == null)
                return;              

            SurfaceEntity activeSurface = geomModel.ActiveSurface;

            ReportResultsForm report = new ReportResultsForm();
            report.SetReportName("Drape Line Interval Report");

            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("File Name", geomModel.DgnModel.GetDgnFile().GetFileName());
            header.Add("Model Name", geomModel.DgnModel.ModelName);
            string surfaceName = "No Active Terrain";
            if (activeSurface != null)
                {
                surfaceName = string.IsNullOrEmpty(activeSurface.Name) ? "Unnamed" : activeSurface.Name;
                }
            header.Add("Active Terrain Name", surfaceName);
            header.Add("Interval", "100");
            header.Add("Left Offset", "50");
            header.Add("Right Offset", "50");
            report.PrintValueTable(header);

            StringBuilder sb = new StringBuilder();

            StationFormatSettings settings = StationFormatSettings.GetStationFormatSettingsForModel(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef() as DgnModel);
            foreach (Alignment al in geomModel.Alignments)
                {
                if (!al.IsFinalElement)
                    {
                    continue;
                    }

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

                double interval = GeometryHelper.ConvertMasterToMeter(100.0);
                double offset = GeometryHelper.ConvertMasterToMeter(50.0);
                Dictionary<string, string> data = new Dictionary<string, string>();
                for (double station = 0.0; station < al.LinearGeometry.Length; station += interval)
                    {
                    data.Clear();

                    LinearPoint pointOnAlignment = al.LinearGeometry.GetPointAtDistanceOffset(station, 0.0);
                    LinearPoint point1 = al.LinearGeometry.GetPointAtDistanceOffset(station, -offset);
                    LinearPoint point2 = al.LinearGeometry.GetPointAtDistanceOffset(station, offset);

                    string stationValue = "";
                    formatter.FormatStation(ref stationValue, station, settings);
                    data["Station"] = stationValue + "|";
                    data["Offset"] = "|";
                    data["Elevation"] = "|";
                    data["Easting (X)"] = "|";
                    data["Northing (Y)"] = "|";

                    if (activeSurface != null)
                        {
                        if (activeSurface is TerrainSurface)
                            {
                            TerrainSurface surface = activeSurface as TerrainSurface;
                            Bentley.TerrainModelNET.DTMDrapedLinearElement linearElement = surface.DTM?.DrapeLinearPoints(new List<Bentley.GeometryNET.DPoint3d>() { point1.Coordinates, point2.Coordinates });
                            if (linearElement != null)
                                {
                                foreach (var pt in linearElement)
                                    {
                                    string typeValue = "";
                                    switch (pt.Code)
                                        {
                                        case Bentley.TerrainModelNET.DTMDrapedLinearElementPointCode.External:
                                            typeValue = "<font color=\"ff0000\"><em>External</em></font>";
                                            break;
                                        case Bentley.TerrainModelNET.DTMDrapedLinearElementPointCode.Void:
                                            typeValue = "<font color=\"ff0000\"><em>Void</em></font>";
                                            break;
                                        default:
                                            break;
                                        }

                                    Bentley.GeometryNET.DVector3d vector = Bentley.GeometryNET.DPoint3d.Subtract(pointOnAlignment.Coordinates, pt.Coordinates);
                                    double pointOffset = vector.MagnitudeXY * Math.Sign(vector.AngleXY.Degrees);
                                    string offsetValue = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(pointOffset));
                                    string elevationValue = (string.IsNullOrEmpty(typeValue)) ? GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(pt.Coordinates.Z)) : "";
                                    string eastingValue = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(pt.Coordinates.X));
                                    string northingValue = GeometryHelper.FormatNumber(GeometryHelper.ConvertMeterToMaster(pt.Coordinates.Y));

                                    data["Station"] += typeValue + "|";
                                    data["Offset"] += offsetValue + "|";
                                    data["Elevation"] += elevationValue + "|";
                                    data["Easting (X)"] += eastingValue + "|";
                                    data["Northing (Y)"] += northingValue + "|";
                                    }
                                }
                            }
                        }
                    report.PrintValueTable(data);
                    }
                }
            report.Finished();
            }
        }
    }