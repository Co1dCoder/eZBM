/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/ManagedFenceExample/FenceByElementTool.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.MstnPlatformNET;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;

namespace ManagedFenceExample
{
    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    class FenceByElementTool : DgnElementSetTool
    {

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private FenceByElementTool() : base()
        {

        }

        /*---------------------------------------------------------------------------------**//**
        * When the tool is install, show a prompt to user to select an element.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnPostInstall()
        {
            NotificationManager.OutputPrompt("Select an element to define fence from");
        }

        /*---------------------------------------------------------------------------------**//**
        * If the data point lies on an element, define a fence and exit the tool.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override bool OnDataButton(DgnButtonEvent ev)
        {
            HitPath hitPath = DoLocate(ev, true, 1);

            if (null != hitPath)
            {
                Element element = hitPath.GetHeadElement();
                Viewport activeViewPort = Session.GetActiveViewport();
                if (StatusInt.Success == FenceManager.DefineByElement(element, activeViewPort))
                {
                    Session.Instance.Keyin("UPDATE VIEW EXTENDED");
                }
                base.ExitTool();
            }

            return true;
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
            FenceByElementTool fenceTool = new FenceByElementTool();
            fenceTool.InstallTool();
        }
    }
}
