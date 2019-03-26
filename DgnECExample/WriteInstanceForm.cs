/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/DgnEC/DgnECExample/WriteInstanceForm.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET.DgnEC;
using Bentley.ECObjects.Schema;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DgnECExample
{

    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    public partial class WriteInstanceForm : Form
    {

        private InstanceInfoToWrite m_instanceInfo;

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        public WriteInstanceForm()
        {
            InitializeComponent();
            m_instanceInfo = null;
            LoadImportedSchemas();
        }

        /*---------------------------------------------------------------------------------**//**
        * List imported schemas in this session.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void LoadImportedSchemas()
        {
            foreach (string schema in DgnECExample.Instance().GetImportedSchemas())
            {
                CmbImportedSchemas.Items.Add(schema);
            }

            if (CmbImportedSchemas.Items.Count > 0)
                CmbImportedSchemas.SelectedIndex = 0;
        }

        /*---------------------------------------------------------------------------------**//**
        * List classes in the selected schema.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void CmbImportedSchemas_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            CmbClasses.Items.Clear();
            DGProperties.Rows.Clear();
            string schemaName = CmbImportedSchemas.SelectedItem.ToString();

            FindInstancesScope scope = FindInstancesScope.CreateScope(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModel(), new FindInstancesScopeOption());
            IECSchema schema = DgnECManager.Manager.LocateSchemaInScope(scope, schemaName, 1, 0, SchemaMatchType.Exact);

            foreach (IECClass ecClass in schema.GetClasses())
            {
                CmbClasses.Items.Add(ecClass.Name);
            }

            if (CmbClasses.Items.Count > 0)
                CmbClasses.SelectedIndex = 0;
        }

        /*---------------------------------------------------------------------------------**//**
        * Get information about instance to write.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public InstanceInfoToWrite GetSelectedInstanceInfoToWrite()
        {
            if (CmbImportedSchemas.SelectedIndex > -1 && CmbClasses.SelectedIndex > -1)
            {
                m_instanceInfo = new InstanceInfoToWrite();
                m_instanceInfo.m_schemaName = CmbImportedSchemas.SelectedItem.ToString();
                m_instanceInfo.m_className = CmbClasses.SelectedItem.ToString();

                for (int i = 0; i < DGProperties.Rows.Count - 1; i++)
                {
                    DataGridViewRow row = DGProperties.Rows[i];
                    PropertyInfoToWrite pInfo = new PropertyInfoToWrite();
                    pInfo.m_propertyName = row.Cells[0].Value.ToString();
                    pInfo.m_type = row.Cells[1].Value.ToString();
                    pInfo.m_value = row.Cells[2].Value.ToString();
                    m_instanceInfo.m_properties.Add(pInfo);
                }
            }
            return m_instanceInfo;
        }

        /*---------------------------------------------------------------------------------**//**
        * Populate properties of the selected class.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void CmbClasses_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DGProperties.Rows.Clear();
            if (CmbClasses.SelectedIndex != -1)
            {
                string schemaName = CmbImportedSchemas.SelectedItem.ToString();
                string className = CmbClasses.SelectedItem.ToString();

                FindInstancesScope scope = FindInstancesScope.CreateScope(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModel(), new FindInstancesScopeOption());
                IECSchema schema = DgnECManager.Manager.LocateSchemaInScope(scope, schemaName, 1, 0, SchemaMatchType.Exact);

                IECClass ecClass = schema.GetClass(className);

                foreach (IECProperty property in ecClass.Properties(false))
                {
                    int index = DGProperties.Rows.Add();
                    DGProperties.Rows[index].Cells[0].Value = property.Name;
                    DGProperties.Rows[index].Cells[1].Value = property.Type.Name;
                    DGProperties.Rows[index].Cells[2].Value = "";
                }
            }
        }
    }

    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    public class PropertyInfoToWrite
    {
        public string m_propertyName { get; set; }
        public string m_type { get; set; }
        public object m_value { get; set; }

        /*---------------------------------------------------------------------------------**//**
        * Get property value as string.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public string GetValueAsString()
        {
            if (null == m_value)
                return "";

            return m_value.ToString();
        }

        /*---------------------------------------------------------------------------------**//**
        * Get property value as boolean.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public bool GetValueAsBoolean()
        {
            if (null == m_value)
                return false;
            try
            {
                bool boolValue = bool.Parse(m_value.ToString());
                return boolValue;
            }
            catch
            {
                return false;
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Get property value as int.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public int GetValueAsInt()
        {
            if (null == m_value)
                return 0;
            try
            {
                int intValue = int.Parse(m_value.ToString());
                return intValue;
            }
            catch
            {
                return 0;
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Get property value as double.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public double GetValueAsDouble()
        {
            if (null == m_value)
                return 0.0;
            try
            {
                double doubleValue = double.Parse(m_value.ToString());
                return doubleValue;
            }
            catch
            {
                return 0.0;
            }
        }
    }

    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    public class InstanceInfoToWrite
    {
        public string m_schemaName { get; set; }
        public string m_className { get; set; }
        public List<PropertyInfoToWrite> m_properties { get; set; }

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public InstanceInfoToWrite()
        {
            m_properties = new List<PropertyInfoToWrite>();
        }
    }
}
