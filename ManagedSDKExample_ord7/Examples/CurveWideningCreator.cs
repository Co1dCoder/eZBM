/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/Examples/CurveWideningCreator.cs $
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

using BCGSE = Bentley.CifNET.GeometryModel.SDK.Edit;


namespace ManagedSDKExample
{
    class CurveWideningCreator : DgnElementSetTool
    {
        /*----------------------------------------------------------------------------------------------**/
        /* Write Function | The user is prompted to select a horizontal alignment. Pop up the interface and
         *  enter the correct data the CurveWidenings can be created successful.
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
            CurveWideningCreator cmd = new CurveWideningCreator();
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

            Bentley.CifNET.SDK.Edit.ConsensusConnectionEdit con = ConsensusConnectionEdit.GetActive();
            Bentley.CifNET.GeometryModel.SDK.Corridor curveWideningCorridor = null;

            Bentley.CifNET.GeometryModel.SDK.Alignment alignment = Bentley.CifNET.GeometryModel.SDK.Alignment.CreateFromElement(con, element);
            if (alignment == null)
            {
                NotificationManager.OutputPrompt("This is not a horizontal alignment.");
                return false;
            }

            foreach (Bentley.CifNET.GeometryModel.SDK.Corridor corridor in con.GetActiveGeometricModel().Corridors)
            {
                if (corridor.CorridorAlignment == alignment)
                {
                    curveWideningCorridor = corridor;
                    break;
                }
            }
            if (curveWideningCorridor == null)
            {
                NotificationManager.OutputPrompt("The selected alignment not have corridor");
                return false;
            }


            con.StartTransientMode();
            double alignmentLength = alignment.LinearGeometry.Length;
            IDictionary<string, double> curveWideningPointsFromTemplate = curveWideningCorridor.GetSuperPointsFromTemplate(0, alignmentLength);
            if (curveWideningPointsFromTemplate.Count == 0)
            {
                NotificationManager.OutputPrompt("The template not have template points");
                return false;
            }

            Utility.CurveWideningPlaceForm form = new Utility.CurveWideningPlaceForm(curveWideningPointsFromTemplate, alignmentLength);
            System.Windows.Forms.DialogResult result = form.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                CreateCurveWidenings(element, form, curveWideningCorridor);
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

        private void CreateCurveWidenings(Element element, Utility.CurveWideningPlaceForm form,
            Bentley.CifNET.GeometryModel.SDK.Corridor curveWideningCorridor)
        {

            double startDistance = form.getStartDistance();
            double endDistance = form.getEndDistance(); ;
            string pointName = form.getPointName();

            BCGSE.CurveWideningParameter curveWideningParameter = new BCGSE.CurveWideningParameter(pointName, startDistance, endDistance);

            curveWideningParameter.Description = form.getDescription();
            curveWideningParameter.Enabled = form.getEnabled();
            curveWideningParameter.Overlap = form.getOverlap();
            curveWideningParameter.Priority = form.getPriority();
            curveWideningParameter.UseSpiralLengthForTransition = form.getUseSpiralLengthForTransition();

            curveWideningParameter.WideningTableFileName = form.getWideningTableFileName();

            Bentley.CifNET.GeometryModel.SDK.Edit.CorridorEdit corridorEdit = curveWideningCorridor as Bentley.CifNET.GeometryModel.SDK.Edit.CorridorEdit;

            BCGSE.CurveWideningEdit curveWideningEdit = corridorEdit.AddCurveWidening(curveWideningParameter);
        }
    }
}

