/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/Examples/SuperElevationCreator.cs $
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
    class SuperElevationCreator : DgnElementSetTool
    {
        /*----------------------------------------------------------------------------------------------**/
        /* Write Function | The user is prompted to select a horizontal alignment. Pop up the interface and
         *  enter the correct data the SuperElevations can be created successful.
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
            SuperElevationCreator cmd = new SuperElevationCreator();
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

            FeatureDefinitionManager fdm = FeatureDefinitionManager.Instance;
            ConsensusConnectionEdit con = ConsensusConnectionEdit.GetActive();
            IEnumerable<string> featureNames = fdm.GetFeatureDefinitions(con, "SuperElevation");

            Bentley.CifNET.GeometryModel.SDK.Alignment alignment = Bentley.CifNET.GeometryModel.SDK.Alignment.CreateFromElement(con, element);
            if (alignment == null)
            {
                NotificationManager.OutputPrompt("This is not a horizontal alignment.");
                return false;
            }

            double alignmentLength = alignment.LinearGeometry.Length;
            Utility.SuperElevationPlaceForm form = new Utility.SuperElevationPlaceForm(featureNames, alignmentLength);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                CreateSuperElevations(alignment, form);
            }
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

        private void CreateSuperElevations(Bentley.CifNET.GeometryModel.SDK.Alignment alignment, Utility.SuperElevationPlaceForm form)
        {

            ConsensusConnectionEdit con = ConsensusConnectionEdit.GetActive();
            con.StartTransientMode();
            //Create SuperElevation Sections
            string sectionName = form.getSectionName();
            double startDistance = form.getStartDistance();
            double endDistance = form.getEndDistance();
            string featureType = form.getFeatureType();
            BCGSE.SuperElevationSectionEdit superEleSection = BCGSE.SuperElevationSectionEdit.CreateSingleSuperElevationSection(sectionName, startDistance, endDistance, alignment);
            if (superEleSection == null)
                return;
            //Set FeatureDefinition to the SuperElevation Section
            superEleSection.SetFeatureDefinition(featureType);

            //Create a SuperElevation Lane
            SuperElevationType superElevationType = form.getSuperElevationType();
            Side superElevationSide = form.getSuperElevationSide();
            string laneName = form.getLaneName();
            double insideEdgeOffset = form.getInsideEdgeOffset();
            double width = form.getWidth();
            double slope = form.getSlope();
            double laneStartDistance = form.getLaneStartDistance();
            double laneEndDistance = form.getLaneEndDistance();

            BCGSE.SuperElevationEdit superElevation = BCGSE.SuperElevationEdit.CreateSuperElevation(superElevationType,
                laneName, superElevationSide, insideEdgeOffset, width, slope, laneStartDistance, laneEndDistance, superEleSection);

            //Create four SuperElevation Transitions
            double t1Distance = form.getT1Distance();
            double t1Slope = form.getT1Slope();
            PivotEdgeType t1PivotEdgeTyp = form.getT1PivotEdgeType();
            SuperElevationTransitionType t1TransitionType = form.getT1TransitionType();
            RDSuperPointType t1SuperPointType = form.getT1SuperPointType();
            double t1NonLinearCurveLength = form.getT1NonLinearCurveLength();

            BCGSE.SuperElevationTransitionEdit superElevationTransition1 = BCGSE.SuperElevationTransitionEdit.CreateSuperElevationTransition(
                t1Distance, t1Slope, t1PivotEdgeTyp, t1TransitionType,
                                        t1SuperPointType, t1NonLinearCurveLength, superElevation);

            double t2Distance = form.getT2Distance();
            double t2Slope = form.getT2Slope();
            PivotEdgeType t2PivotEdgeTyp = form.getT2PivotEdgeType();
            SuperElevationTransitionType t2TransitionType = form.getT2TransitionType();
            RDSuperPointType t2SuperPointType = form.getT2SuperPointType();
            double t2NonLinearCurveLength = form.getT2NonLinearCurveLength();

            BCGSE.SuperElevationTransitionEdit superElevationTransition2 = BCGSE.SuperElevationTransitionEdit.CreateSuperElevationTransition(
                t2Distance, t2Slope, t2PivotEdgeTyp, t2TransitionType,
                                        t2SuperPointType, t2NonLinearCurveLength, superElevation);

            double t3Distance = form.getT3Distance();
            double t3Slope = form.getT3Slope();
            PivotEdgeType t3PivotEdgeTyp = form.getT3PivotEdgeType();
            SuperElevationTransitionType t3TransitionType = form.getT3TransitionType();
            RDSuperPointType t3SuperPointType = form.getT3SuperPointType();
            double t3NonLinearCurveLength = form.getT3NonLinearCurveLength();

            BCGSE.SuperElevationTransitionEdit superElevationTransition3 = BCGSE.SuperElevationTransitionEdit.CreateSuperElevationTransition(
                t3Distance, t3Slope, t3PivotEdgeTyp, t3TransitionType,
                                        t3SuperPointType, t3NonLinearCurveLength, superElevation);

            //Set Constraint
            superElevationTransition2.SetSlopeConstraint(SuperElevationSlopeConstraintType.CrossSlope, superElevationTransition1, superElevationTransition3, superElevation);

            con.PersistTransients();
        }
    }
}

