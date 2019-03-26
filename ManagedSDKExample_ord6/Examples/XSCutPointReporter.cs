/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/Examples/XSCutPointReporter.cs $
|
|  $Copyright: (c) 2018 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.Geometry;
using Bentley.CifNET.SDK;

namespace ManagedSDKExample
    {
    class XSCutPointReporter : DgnElementSetTool
        {
        private bool ALTERNATE_FORMAT = false;
        private ConsensusConnection m_con;

        public XSCutPointReporter() : base()
            {
            }

        /*----------------------------------------------------------------------------------------------**/
        /* Corridor Feature Report | For each corridor, reports the corridor's features.
        /*--------------+---------------+---------------+---------------+---------------+----------------*/
        internal void ReportAllXSCutPointsAllCorridors()
            {
            ConsensusConnection sdkCon = new ConsensusConnection(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef());
            if(sdkCon == null)
                return;

            GeometricModel geomModel = sdkCon.GetActiveGeometricModel();
            if (geomModel == null)
                return;

            ReportResultsForm xscpReport = new ReportResultsForm();
            xscpReport.SetReportName("Corridor Cut Points Report");

            //ask user for widths and increment
            Utility.XSCutPointForm form = new Utility.XSCutPointForm();
            form.SetStartStationNotVisible();
            form.SetEndStationNotVisible();
            form.ShowDialog();

            if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                bool foundCorridors = false;
                foreach (Corridor cor in geomModel.Corridors)
                    {
                    foundCorridors = true;

                    if (cor.Name != null)
                        xscpReport.AddHeader("Corridor: " + cor.Name);
                    else
                        xscpReport.AddHeader("Corridor");

                    double length = cor.CorridorAlignment.LinearGeometry.Length;
                    AddXSCutPoints(xscpReport, cor, form.GetLeftWidth(), form.GetRighttWidth(), form.GetIncrement(), 0.0, length, true);

                    xscpReport.NewParagraph();
                    }
                xscpReport.NewLine();

                //Corridors reside in the geometric model which is attached to the 2D plan model. Therefore, if no corridors are found it may
                //indicate that the active view is not a 2D plan model. Inform the user to pick an appropriate view before starting the command.
                if (!foundCorridors)
                    xscpReport.AddLine("No corridors to display. Verify that a window with a 2D view is selected before running the command.");

                xscpReport.Finished();
                }
            }

        /*----------------------------------------------------------------------------------------------**/
        /* Corridor Feature Report | For a single corridor selected by the user, reports the corridor's features.
        /*--------------+---------------+---------------+---------------+---------------+----------------*/
        internal void ReportAllXSCutPoints(Corridor cor, double leftWidth, double rightWidth, double increment, double startStation, double endStation)
            {
            //report formatting
            ReportResultsForm xscpReport = new ReportResultsForm();
            xscpReport.SetReportName("Corridor Cut Points Report");

            if (cor.Name != null)
                xscpReport.AddHeader("Corridor: " + cor.Name);
            else
                xscpReport.AddHeader("Corridor");

            //get report information
            AddXSCutPoints(xscpReport, cor, leftWidth, rightWidth, increment, startStation, endStation, false);

            //report formatting
            xscpReport.NewParagraph();
            xscpReport.NewLine();

            xscpReport.Finished();
            }

        /*----------------------------------------------------------------------------------------------**/
        /* Utility Function | Gets cut point information.
        /*--------------+---------------+---------------+---------------+---------------+----------------*/
        private void AddXSCutPoints(ReportResultsForm xscpReport, Corridor cor, double leftWidth, double rightWidth, double increment, double startStation, double endStation, bool allCorridors)
            {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            double station;

            // ORD does all computations in meters. 
            // Therefore, input must be converted from Master units to Meters
            leftWidth = GeometryHelper.ConvertMasterToMeter(leftWidth);
            rightWidth = GeometryHelper.ConvertMasterToMeter(rightWidth);
            increment = GeometryHelper.ConvertMasterToMeter(increment);
            if (!allCorridors)
                {
                startStation = GeometryHelper.ConvertMasterToMeter(startStation);
                endStation = GeometryHelper.ConvertMasterToMeter(endStation);
                }

            ConsensusItemCache.SetCaching(true);

            //alternative format
            if (ALTERNATE_FORMAT)
                {
                for (station = startStation; station < endStation; station += increment)
                    {
                    if (station == 0)
                        station = 0.000001;
                    XSCutPoint[] points = cor.GetXSCutPoints(station, leftWidth, rightWidth, -leftWidth);
                    xscpReport.AddSubheader("Station " + GeometryHelper.GetStationFromDistanceAlong(cor.CorridorAlignment, station));
                    if (points != null)
                        {
                        foreach (XSCutPoint pt in points)
                            {
                            properties.Add("FTR Name", pt.PointName);
                            properties.Add("FTR Defintion Name", pt.PointFeatureName);
                            properties.Add("Offset", GeometryHelper.FormatDistance(pt.Point.X));
                            properties.Add("Elevation", GeometryHelper.FormatDistance(pt.Point.Y));
                            properties.Add("X", GeometryHelper.FormatDistance(pt.PointOnPlan.X));
                            properties.Add("Y", GeometryHelper.FormatDistance(pt.PointOnPlan.Y));
                            properties.Add("Z", GeometryHelper.FormatDistance(pt.PointOnPlan.Z));
                            xscpReport.PrintValueTable(properties);
                            properties.Clear();
                            }
                        }
                    if (station == 0.000001)
                        station = 0.0;
                    }
                }

            //adds point information
            else
                {
                for (station = startStation; station < endStation; station += increment)
                    {
                    if (station == 0)
                        station = 0.000001;
                    XSCutPoint[] points = cor.GetXSCutPoints(station, leftWidth, rightWidth, -leftWidth);
                    Array.Sort(points, (first, second) => first.Point.X.CompareTo(second.Point.X));
                    xscpReport.AddSubheader("Station " + GeometryHelper.GetStationFromDistanceAlong(cor.CorridorAlignment, station));

                    if (points != null)
                        {
                        properties.Clear();
                        properties["FTR Name"] = "|";
                        properties["FTR Definition Name"] = "|";
                        properties["Offset"] = "|";
                        properties["Elevation"] = "|";
                        properties["X"] = "|";
                        properties["Y"] = "|";
                        properties["Z"] = "|";

                        foreach (XSCutPoint pt in points)
                            {
                            properties["FTR Name"] += pt.PointName + "|";
                            properties["FTR Definition Name"] += getCleanDefName(pt.PointFeatureName) + "|";
                            properties["Offset"] += GeometryHelper.FormatDistance(pt.Point.X) + "|";
                            properties["Elevation"] += GeometryHelper.FormatDistance(pt.Point.Y) + "|";
                            properties["X"] += GeometryHelper.FormatDistance(pt.PointOnPlan.X) + "|";
                            properties["Y"] += GeometryHelper.FormatDistance(pt.PointOnPlan.Z) + "|";
                            properties["Z"] += GeometryHelper.FormatDistance(pt.PointOnPlan.X) + "|";
                            }
                        }
                    xscpReport.PrintValueTable(properties);

                    if (station == 0.000001)
                        station = 0.0;
                    }
                ConsensusItemCache.SetCaching(false);
                }
            }

        protected override void OnPostInstall()
            {
            base.BeginPickElements();
            AccuSnap.LocateEnabled = true;
            AccuSnap.SnapEnabled = true;
            m_con = new ConsensusConnection(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef());
            base.OnPostInstall();
            NotificationManager.OutputPrompt("Identify corridor.");
            }

        protected override bool OnPostLocate(HitPath path, out string cantAcceptReason)
            {
            //checks that hitpath is not null
            if (path == null)
                {
                cantAcceptReason = "HitPath is null.";
                return false;
                }

            //checks that the cursor element is not null
            Element el = path.GetCursorElement();
            if (el == null)
                {
                cantAcceptReason = "There is no element at cursor.";
                return false;
                }

            //checks that the cursor element is a corridor
            if (!(el.ElementType == (MSElementType)106))
                {
                cantAcceptReason = "This element is not a corridor.";
                return false;
                }
            Corridor cor = (el.ParentElement == null) ? Corridor.CreateFromElement(m_con, el) : Corridor.CreateFromElement(m_con, el.ParentElement);
            if (cor == null)
                {
                cantAcceptReason = "This element is not a corridor.";
                return false;
                }

            cantAcceptReason = String.Empty;
            return true;
            }

        protected override bool OnDataButton(DgnButtonEvent ev)
            {
            //gets corridor from the cursor element from hitpath
            HitPath hitPath = DoLocate(ev, true, 1);
            if (hitPath == null)
                return false;

            Element el = hitPath.GetCursorElement();
            if (el == null)
                return false;
            
            Corridor cor = (el.ParentElement == null) ? Corridor.CreateFromElement(m_con, el) : Corridor.CreateFromElement(m_con, el.ParentElement);
            
            if (cor == null)
                {
                NotificationManager.OutputPrompt("XSCutPoints only works with corridors.");
                return false;
                }
            else
                {
                //ask user for widths and increment
                Utility.XSCutPointForm form = new Utility.XSCutPointForm(cor);
                form.ShowDialog();
                if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                    ReportAllXSCutPoints(cor, form.GetLeftWidth(), form.GetRighttWidth(), form.GetIncrement(), form.GetStartStation(), form.GetEndStation());
                    }
                
                return true;
                }
            }

        private string getCleanDefName(string filePath)
            {
            int index = filePath.LastIndexOf('\\');

            if (index < 0)
                {
                return filePath;
                }

            return filePath.Substring(index + 1);
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

        public override StatusInt OnElementModify(Element element)
            {
            return StatusInt.Error;
            }

        public static void InstallNewInstance()
            {
            XSCutPointReporter tool = new XSCutPointReporter();
            tool.InstallTool();
            }
        }
    }
