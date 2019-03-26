/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/ManagedToolsExample/PlaceGroupedHoleTool.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
/*---------- 事件的触发顺序与相关命令 ----------------------------------------------------------------------------+

OnRestartTool
7、ExitTool
6、OnResetButton <DPoint3d xyz="163156.762214451,621.689775525279,0"/> 0

OnPostInstall
PlaceGroupedHoleTool 0 0
InstallNewInstance
OnRestartTool
OnDataButton<DPoint3d xyz="163156.762214451,621.689775525279,0"/> 0
OnDynamicFrame<DPoint3d xyz="163156.762214451,621.689775525279,0"/> 0
OnDynamicFrame<DPoint3d xyz="163156.762214451,312.567560763411,0"/> 0
OnDataButton<DPoint3d xyz="158832.940724807,-4942.51009018843,0"/> 0
OnPostInstall
PlaceGroupedHoleTool 0 0
InstallNewInstance

5、OnRestartTool
4、OnDataButton<DPoint3d xyz="147824.844177191,9860.65882692851,0"/> 0
3、OnDynamicFrame<DPoint3d xyz="147824.844177191,9860.65882692851,0"/> 0
3、OnDynamicFrame<DPoint3d xyz="147824.844177191,9759.72096088382,0"/> 0
2、OnDataButton<DPoint3d xyz="141874.862418847,3097.82180193407,0"/> 0
1、OnPostInstall
PlaceGroupedHoleTool 0 0
InstallNewInstance
+--------------------------------------------------------------------------------------*/

using System.Collections.Generic;

using Bentley.GeometryNET;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.MstnPlatformNET;
using System.Windows.Forms;
using eZBMCE.AddinManager;

namespace ManagedToolsExample
{
    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    class PlaceGroupedHoleTool : DgnPrimitiveTool
    {
        private readonly DocumentModifier _docMdf;

        private List<DPoint3d> m_points;

        private static PlaceGroupedHoleForm m_groupHoleForm = null;
        private static MessageCenter MCenter = MessageCenter.Instance;
        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public PlaceGroupedHoleTool(DocumentModifier docMdf, int toolId, int prompt) : base(toolId, prompt)
        {
            _docMdf = docMdf;
            _docMdf.WriteMessageLineNow($"构造函数 {toolId} {prompt}");
            m_points = new List<DPoint3d>();

            //Load options form.
            if (null == m_groupHoleForm)
            {
                m_groupHoleForm = new PlaceGroupedHoleForm();
                m_groupHoleForm.Show();
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Restart tool.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnRestartTool()
        {
            _docMdf.WriteMessageLineNow($"OnRestartTool");
            InstallNewInstance(_docMdf);
        }

        /*---------------------------------------------------------------------------------**//**
        * Exit tool.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void ExitTool()
        {
            _docMdf.WriteMessageLineNow($"ExitTool");
            if (null != m_groupHoleForm)
            {
                m_groupHoleForm.Close();
                m_groupHoleForm = null;
            }
            base.ExitTool();
        }

        /*---------------------------------------------------------------------------------**//**
        * Enable snapping.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnPostInstall()
        {
            _docMdf.WriteMessageLineNow($"OnPostInstall");
            Bentley.DgnPlatformNET.AccuSnap.SnapEnabled = true;
            base.OnPostInstall();
        }

        /*---------------------------------------------------------------------------------**//**
        * Get start and opposite points from data buttons in this method.
/// Start dynamics on first data point.
/// Place the grouped hole on second data point.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override bool OnDataButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
        {
            _docMdf.WriteMessageLineNow($"OnDataButton {ev.Point} {ev.KeyModifiers}");
            if (0 == m_points.Count)
                BeginDynamics();
            m_points.Add(ev.Point); // Save current data point location.

            if (m_points.Count < 2)
                return false;

            Element element = GroupedHoleHelper.CreateElement(m_points[0], m_points[1]);
            if (null != element)
                element.AddToModel(); // Add new element to active model.

            base.OnReinitialize();
            
            return true;
        }

        /*---------------------------------------------------------------------------------**//**
        * Temporarly show how the element will look like.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnDynamicFrame(Bentley.DgnPlatformNET.DgnButtonEvent ev)
        {

            _docMdf.WriteMessageLineNow($"OnDynamicFrame {ev.Point} {ev.KeyModifiers}");
            Element element = GroupedHoleHelper.CreateElement(m_points[0], ev.Point);
            if (null == element)
                return;

            RedrawElems redrawElems = new RedrawElems();
            redrawElems.SetDynamicsViewsFromActiveViewSet(Bentley.MstnPlatformNET.Session.GetActiveViewport());
            redrawElems.DrawMode = DgnDrawMode.TempDraw;
            redrawElems.DrawPurpose = DrawPurpose.Dynamics;

            redrawElems.DoRedraw(element);
        }

        /*---------------------------------------------------------------------------------**//**
        * Restart the tool on reset.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override bool OnResetButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
        {
            _docMdf.WriteMessageLineNow($"OnResetButton {ev.Point} {ev.KeyModifiers}");
            ExitTool();
            return true;
        }

        /*---------------------------------------------------------------------------------**//**
        * Static method to initialize this tool from any class.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static void InstallNewInstance(DocumentModifier docMdf)
        {
            docMdf.WriteMessageLineNow($"InstallNewInstance");
            PlaceGroupedHoleTool groupedHoleTool = new PlaceGroupedHoleTool(docMdf, 0, 0);
            groupedHoleTool.InstallTool();
        }
    }
}
