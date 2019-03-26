/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/DgnEC/DgnECExample/WriteInstanceOnElementTool.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.ECObjects.Schema;
using Bentley.ECObjects.Instance;

namespace DgnECExample
{
    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    class WriteInstanceOnElementTool : DgnElementSetTool
    {

        private static WriteInstanceForm m_writeInstanceForm = null;

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        private WriteInstanceOnElementTool() : base()
        {
            if (null == m_writeInstanceForm)
            {
                m_writeInstanceForm = new WriteInstanceForm();
                m_writeInstanceForm.Show();
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Exit tool.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void ExitTool()
        {
            if (null != m_writeInstanceForm)
            {
                m_writeInstanceForm.Close();
                m_writeInstanceForm = null;
            }
            base.ExitTool();
        }

        /*---------------------------------------------------------------------------------**//**
        * Write instance on the element on first data button if it lies on some element.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override bool OnDataButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
        {
            //Locate an element.
            Bentley.DgnPlatformNET.HitPath hitPath = DoLocate(ev, true, 1);

            //If an element is located write an instance on it.
            if (null != hitPath)
            {
                InstanceInfoToWrite instanceInfo = m_writeInstanceForm.GetSelectedInstanceInfoToWrite();
                FindInstancesScope scope = FindInstancesScope.CreateScope(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModel(), new FindInstancesScopeOption());
                IECSchema schema = DgnECManager.Manager.LocateSchemaInScope(scope, instanceInfo.m_schemaName, 1, 0, SchemaMatchType.Exact);

                Bentley.DgnPlatformNET.Elements.Element element = hitPath.GetHeadElement();

                ECClass calss1 = schema.GetClass(instanceInfo.m_className) as ECClass;
                DgnECInstanceEnabler instanceEnabler = DgnECManager.Manager.ObtainInstanceEnabler(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnFile(), calss1);
                StandaloneECDInstance instance = instanceEnabler.SharedWipInstance;
                foreach (PropertyInfoToWrite pInfo in instanceInfo.m_properties)
                {
                    SetInstancePropertyValue(ref instance, pInfo);
                }
                instanceEnabler.CreateInstanceOnElement(element, instance, false);
            }

            return true;
        }

        /*---------------------------------------------------------------------------------**//**
        * Set instance property value based on type of the property.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void SetInstancePropertyValue(ref StandaloneECDInstance instance, PropertyInfoToWrite pInfo)
        {
            string type = pInfo.m_type.ToLower();
            switch (type)
            {
                case "string":
                    instance.MemoryBuffer.SetStringValue(pInfo.m_propertyName, -1, pInfo.GetValueAsString());
                    break;
                case "boolean":
                    instance.MemoryBuffer.SetBooleanValue(pInfo.m_propertyName, -1, pInfo.GetValueAsBoolean());
                    break;
                case "int":
                    instance.MemoryBuffer.SetIntegerValue(pInfo.m_propertyName, -1, pInfo.GetValueAsInt());
                    break;
                case "double":
                    instance.MemoryBuffer.SetDoubleValue(pInfo.m_propertyName, -1, pInfo.GetValueAsDouble());
                    break;
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Instance was written in OnDataButton method, just return Error from this method.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public override Bentley.DgnPlatformNET.StatusInt OnElementModify(Bentley.DgnPlatformNET.Elements.Element element)
        {
            return Bentley.DgnPlatformNET.StatusInt.Error;
        }

        /*---------------------------------------------------------------------------------**//**
        * Restart tool
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnRestartTool()
        {
            InstallNewInstance();
        }


        /*---------------------------------------------------------------------------------**//**
        * Restart the tool on reset.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
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
            WriteInstanceOnElementTool instanceTool = new WriteInstanceOnElementTool();
            instanceTool.InstallTool();
        }
    }
}
