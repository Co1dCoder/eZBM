/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/Examples/SuperElevationAssign.cs $
|
|  $Copyright: (c) 2019 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using Bentley.MstnPlatformNET;
using Bentley.CifNET.SDK.Edit;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.GeometryModel.SDK.Edit;
using System.Windows.Forms;

using BCGSE = Bentley.CifNET.GeometryModel.SDK.Edit;


namespace ManagedSDKExample
{
    class SuperElevationAssign : DgnElementSetTool
    {
        /*----------------------------------------------------------------------------------------------**/
        /* Write Function | The user is prompted to select a horizontal alignment. Pop up the interface and
         *  enter the correct data the SuperElevations can be assigned successful.
        /*--------------+---------------+---------------+---------------+---------------+----------------*/

        protected override void OnPostInstall()
        {
            base.BeginPickElements();
            AccuSnap.LocateEnabled = true;
            AccuSnap.SnapEnabled = false;
            base.OnPostInstall();
            NotificationManager.OutputPrompt("Select a horizontal alignment.");
            BeginDynamics();
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

            //checks that the cursor element is a civil element
            if (el.ElementType != MSElementType.Line && el.ElementType != MSElementType.LineString && el.ElementType != MSElementType.ComplexString)
            {
                cantAcceptReason = "This is not a civil element.";
                return false;
            }

            Bentley.CifNET.SDK.ConsensusConnection con = new Bentley.CifNET.SDK.ConsensusConnection(el.DgnModel);

            Alignment al = (el.ParentElement == null) ? Alignment.CreateFromElement(con, el) : Alignment.CreateFromElement(con, el.ParentElement);

            if (al == null)
            {
                cantAcceptReason = "This is not a civil element.";
                return false;
            }

            cantAcceptReason = String.Empty;
            return true;
        }

        public static void InstallNewInstance()
        {
            SuperElevationAssign cmd = new SuperElevationAssign();
            cmd.InstallTool();
        }

        protected override bool OnDataButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
        {
            HitPath hitPath = DoLocate(ev, true, 1);
            if (hitPath == null)
                return false;

            Element element = hitPath.GetHeadElement();
            if (element == null)
                return false;

            ConsensusConnectionEdit con = ConsensusConnectionEdit.GetActive();
            Bentley.CifNET.GeometryModel.SDK.Alignment alignment = Bentley.CifNET.GeometryModel.SDK.Alignment.CreateFromElement(con, element);
            if (alignment == null)
            {
                NotificationManager.OutputPrompt("This is not a horizontal alignment.");
                return false;
            }

            BCGSE.CorridorEdit superElevationCorridor = null;
            foreach (Bentley.CifNET.GeometryModel.SDK.Corridor corridor in con.GetActiveGeometricModel().Corridors)
            {
                if (corridor != null)
                {
                    superElevationCorridor = corridor as BCGSE.CorridorEdit;
                    if (superElevationCorridor.CorridorAlignment == alignment)
                        break;
                }
            }

            BCGSE.SuperElevationSectionEdit assignSuperElevationSection = null;
            foreach (Bentley.CifNET.GeometryModel.SDK.SuperElevationSection superElevationSection in con.GetActiveGeometricModel().SuperElevationSections)
            {
                if (superElevationSection != null)
                {
                    List<SuperElevation> superElevationsl = new List<SuperElevation>(superElevationSection.SuperElevations);
                    if (superElevationSection.Alignment == alignment && superElevationsl != null && superElevationsl.Count > 0)
                    {
                        assignSuperElevationSection = superElevationSection as BCGSE.SuperElevationSectionEdit;
                        break;
                    }
                }
            }

            if (assignSuperElevationSection == null)
            {
                NotificationManager.OutputPrompt("The alignment not have SuperElevation Section");
                return false;
            }
            con.StartTransientMode();
            BCGSE.SuperElevationEdit assignSuperElevation = null;
            List<SuperElevation> superElevations = new List<SuperElevation>(assignSuperElevationSection.SuperElevations);
            if (superElevations.Count > 0)
            {
                assignSuperElevation = superElevations[0] as BCGSE.SuperElevationEdit;
            }
            else
            {
                NotificationManager.OutputPrompt("The SuperElevation Section not have SuperElevation Lane");
                return false;
            }

            double alignmentLength = alignment.LinearGeometry.Length;
            IDictionary<string, double> superPointsFromTemplate = superElevationCorridor.GetSuperPointsFromTemplate(0, alignmentLength);

            Utility.SuperElevationAssignForm form = new Utility.SuperElevationAssignForm(superPointsFromTemplate, alignmentLength);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                AssignSuperElevations(form, superElevationCorridor, assignSuperElevation);
            }
            con.PersistTransients();
            EndDynamics();
            NotificationManager.OutputPrompt("Command complete. Select a new horizontal alignment or pick element selection tool to exit command.");
            return true;
        }

        protected override bool OnResetButton(DgnButtonEvent ev)
        {
            InstallNewInstance();
            return true;
        }

        protected override void OnRestartTool()
        {
            InstallNewInstance();
        }

        public override StatusInt OnElementModify(Element element)
        {
            return Bentley.DgnPlatformNET.StatusInt.Error;
        }

        private void AssignSuperElevations(Utility.SuperElevationAssignForm form,
            BCGSE.CorridorEdit superElevationCorridor, BCGSE.SuperElevationEdit assignSuperElevation)
        {
            string superPoint = form.getSuperPoint();
            string pivotPoint = form.getPivotPoint();
            double startDistance = form.getStartDistance();
            double endDistance = form.getEndDistance();
            int priority = form.getPriority();

            Bentley.CifNET.GeometryModel.SDK.PointControl pointControl = assignSuperElevation.AddPointControl(superPoint, pivotPoint, startDistance, endDistance, priority, superElevationCorridor);
        }
    }
}

