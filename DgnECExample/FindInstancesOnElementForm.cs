/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/DgnEC/DgnECExample/FindInstancesOnElementForm.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET.DgnEC;
using Bentley.EC.Persistence.Query;
using Bentley.ECObjects.Instance;
using Bentley.ECObjects.Schema;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DgnECExample
{
    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    public partial class FindInstancesOnElementForm : Form
    {

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        public FindInstancesOnElementForm()
        {
            InitializeComponent();
        }

        /*---------------------------------------------------------------------------------**//**
        * Constructor for finding all instances.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public FindInstancesOnElementForm(bool allInstances) : this()
        {
            DgnECManager manager = DgnECManager.Manager;
            var model = Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModel();
            FindInstancesScope scope = FindInstancesScope.CreateScope(model, new FindInstancesScopeOption(DgnECHostType.Element, true));
            ECQuery query = new ECQuery(GetSearchClasses());
            query.SelectClause.SelectAllProperties = true;
            manager.FindInstances(scope, query);

            using (DgnECInstanceCollection ecInstances = manager.FindInstances(scope, query))
            {
                PopulateInstances(ecInstances);
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Get all classes from all schemas the user created in this session.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static IECClass[] GetSearchClasses()
        {
            List<IECClass> classes = new List<IECClass>();
            foreach (string schemaName in DgnECExample.Instance().GetImportedSchemas())
            {
                FindInstancesScope scope = FindInstancesScope.CreateScope(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModel(), new FindInstancesScopeOption());
                IECSchema schema = DgnECManager.Manager.LocateSchemaInScope(scope, schemaName, 1, 0, SchemaMatchType.Exact);

                foreach (IECClass ecClass in schema.GetClasses())
                {
                    classes.Add(ecClass);
                }
            }
            return classes.ToArray();
        }

        /*---------------------------------------------------------------------------------**//**
        * Add instances to the list.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public void PopulateInstances(DgnECInstanceCollection ecInstances)
        {
            LstInstances.Items.Clear();
            foreach (IDgnECInstance instance in ecInstances)
            {
                ListViewItem itemClass = new ListViewItem(instance.ClassDefinition.Name);
                itemClass.SubItems.Add("");
                itemClass.SubItems.Add("");
                itemClass.SubItems.Add("");
                LstInstances.Items.Add(itemClass);

                string className = instance.ClassDefinition.Name;
                foreach (IECProperty prop in instance.ClassDefinition.Properties(false))
                {
                    ListViewItem itemProp = new ListViewItem("");
                    itemProp.SubItems.Add(prop.Name);
                    itemProp.SubItems.Add(prop.Type.Name);

                    string type = prop.Type.Name.ToLower();
                    IECPropertyValue propValue = instance.GetPropertyValue(prop.Name);

                    switch (type)
                    {
                        case "string":
                            itemProp.SubItems.Add((null != propValue) ? propValue.StringValue : "");
                            break;
                        case "boolean":
                            itemProp.SubItems.Add((null != propValue) ? propValue.StringValue : "");
                            break;
                        case "int":
                            itemProp.SubItems.Add((null != propValue) ? propValue.IntValue.ToString() : "");
                            break;
                        case "double":
                            itemProp.SubItems.Add((null != propValue) ? propValue.DoubleValue.ToString() : "");
                            break;
                    }
                    LstInstances.Items.Add(itemProp);
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Close this form.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
