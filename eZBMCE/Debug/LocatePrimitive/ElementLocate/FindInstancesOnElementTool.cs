/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/DgnEC/DgnECExample/FindInstancesOnElementTool.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.ECObjects.Schema;
using Bentley.ECObjects.Instance;
using Bentley.EC.Persistence.Query;

namespace DgnECExample
{

    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    class FindInstancesOnElementTool : DgnElementSetTool
    {
        private static Form m_findInstancesForm = null;
        // private static FindInstancesOnElementForm m_findInstancesForm = null;

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        private FindInstancesOnElementTool() : base()
        {
            ShowInstancesForm();
        }

        /*---------------------------------------------------------------------------------**//**
        * Display instances form if it is closed.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        private void ShowInstancesForm()
        {
            if (null == m_findInstancesForm)
            {
                m_findInstancesForm = new FindInstancesOnElementForm();
                m_findInstancesForm.Show();
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Exit tool.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        protected override void ExitTool()
        {
            if (null != m_findInstancesForm)
            {
                m_findInstancesForm.Close();
                m_findInstancesForm = null;
            }
            base.ExitTool();
        }

        /*---------------------------------------------------------------------------------**//**
        * Find instances on element on first data button if it lies on some element.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        protected override bool OnDataButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
        {
            //Locate an element.
            Bentley.DgnPlatformNET.HitPath hitPath = DoLocate(ev, true, 1);

            //If an element is located read instances on it.
            if (null != hitPath)
            {

                DgnECManager manager = DgnECManager.Manager;

                FindInstancesScope scope = FindInstancesScope.CreateScope(hitPath.GetHeadElement(), new FindInstancesScopeOption(DgnECHostType.Element, true));
                ECQuery query = new ECQuery(FindInstancesOnElementForm.GetSearchClasses());
                query.SelectClause.SelectAllProperties = true;
                DgnECInstanceCollection ecInstances = manager.FindInstances(scope, query);

                using (ecInstances)
                {
                    ShowInstancesForm();
                    m_findInstancesForm.PopulateInstances(ecInstances);
                }
            }

            return true;
        }

        /*---------------------------------------------------------------------------------**//**
        * Instances were read in OnDataButton method, just return Error from this method.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        public override Bentley.DgnPlatformNET.StatusInt OnElementModify(Bentley.DgnPlatformNET.Elements.Element element)
        {
            return Bentley.DgnPlatformNET.StatusInt.Error;
        }

        /*---------------------------------------------------------------------------------**//**
        * Restart tool
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnRestartTool()
        {
            InstallNewInstance();
        }


        /*---------------------------------------------------------------------------------**//**
        * Restart the tool on reset.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        protected override bool OnResetButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
        {
            ExitTool();
            return true;
        }

        /*---------------------------------------------------------------------------------**//**
        * Static method to initialize this tool from any class.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static void InstallNewInstance()
        {
            FindInstancesOnElementTool instancesTool = new FindInstancesOnElementTool();
            instancesTool.InstallTool();
        }
    }
}
