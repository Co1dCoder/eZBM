/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/ManagedFenceExample/FenceByPointsTool.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.MstnPlatformNET;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using System.Collections.Generic;
using Bentley.GeometryNET;

namespace ManagedFenceExample
{
    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    class FenceByPointsTool : DgnElementSetTool
    {

        private List<DPoint3d> m_points;

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private FenceByPointsTool() : base()
        {
            m_points = new List<DPoint3d>();
        }

        /*---------------------------------------------------------------------------------**//**
        * When the tool is install, show a prompt to user to enter some points.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnPostInstall()
        {
            NotificationManager.OutputPrompt("Enter data points to define fence. Reset to complete");
            AccuSnap.SnapEnabled = true;
            AccuSnap.LocateEnabled = true;
        }

        /*---------------------------------------------------------------------------------**//**
        * Reset to complete Fence creation.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override bool OnResetButton(DgnButtonEvent ev)
        {
            if (m_points.Count > 2)
            {
                m_points.Add(m_points[0]);
                Viewport activeViewPort = Session.GetActiveViewport();
                if (StatusInt.Success == FenceManager.DefineByPoints(m_points.ToArray(), activeViewPort))
                {
                    Session.Instance.Keyin("UPDATE VIEW EXTENDED");
                }
                m_points.Clear();
            }

            return true;
        }

        /*---------------------------------------------------------------------------------**//**
        * Collect data points
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override bool OnDataButton(DgnButtonEvent ev)
        {
            m_points.Add(ev.Point);

            base.BeginDynamics();

            return false;
        }

        /*---------------------------------------------------------------------------------**//**
        * Show dynamics to the user how the fence will look like
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnDynamicFrame(DgnButtonEvent ev)
        {
            if (m_points.Count > 0)
            {
                DPoint3d[] points = new DPoint3d[m_points.Count + 1];
                for (int i = 0; i < m_points.Count; i++)
                    points[i] = m_points[i];
                points[points.Length - 1] = ev.Point;

                LineStringElement element = new LineStringElement(Session.Instance.GetActiveDgnModel(), null, points);

                RedrawElems redrawElems = new RedrawElems();
                redrawElems.SetDynamicsViewsFromActiveViewSet(Bentley.MstnPlatformNET.Session.GetActiveViewport());
                redrawElems.DrawMode = DgnDrawMode.TempDraw;
                redrawElems.DrawPurpose = DrawPurpose.Dynamics;

                redrawElems.DoRedraw(element);
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * No modification is required so return Error.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public override StatusInt OnElementModify(Element element)
        {
            return StatusInt.Error;
        }

        /*---------------------------------------------------------------------------------**//**
        * Restart tool.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnRestartTool()
        {
            InstallNewInstance();
        }

        /*---------------------------------------------------------------------------------**//**
        * Static method to be called from AddIn class.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static void InstallNewInstance()
        {
            FenceByPointsTool fenceTool = new FenceByPointsTool();
            fenceTool.InstallTool();
        }
    }
}
