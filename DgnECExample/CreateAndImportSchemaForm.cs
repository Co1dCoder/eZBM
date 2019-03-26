/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/DgnEC/DgnECExample/CreateAndImportSchemaForm.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.ECObjects;
using Bentley.ECObjects.Schema;
using Bentley.ECObjects.XML;
using System;
using System.Windows.Forms;

namespace DgnECExample
{

    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    public partial class CreateAndImportSchemaForm : Form
    {

        private string m_currentClass;

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        public CreateAndImportSchemaForm()
        {
            InitializeComponent();
            CmbType.SelectedIndex = 0;
        }

        /*---------------------------------------------------------------------------------**//**
        * Close the form.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*---------------------------------------------------------------------------------**//**
        * User does not want this schema and wants to create another one.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to discard this schema?", "MicroStation SDK", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                TSchema.Nodes.Clear();
                GrpSchema.Enabled = true;
                GrpClasses.Enabled = false;
                GrpProperties.Enabled = false;
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Set schema name.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnCreateSchema_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtSchemaName.Text))
            {
                TSchema.Nodes.Add(TxtSchemaName.Text);
                GrpSchema.Enabled = false;
                GrpClasses.Enabled = true;
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Add a new class to schema.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnAddClass_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtClassName.Text) && TSchema.Nodes.Count > 0)
            {
                TSchema.Nodes[0].Nodes.Add(TxtClassName.Text);
                TSchema.Nodes[0].Expand();
                m_currentClass = TxtClassName.Text;
                GrpProperties.Enabled = true;
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Add a new property to the selected class.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnAddProperty_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtPropertyName.Text))
            {
                if (TSchema.SelectedNode != null && TSchema.SelectedNode.Text == m_currentClass)
                {
                    TreeNode node = new TreeNode(TxtPropertyName.Text);
                    node.Tag = CmbType.SelectedItem.ToString();
                    TSchema.SelectedNode.Nodes.Add(node);
                    TSchema.SelectedNode.Expand();
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Create the schema and then import it into the file.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnCreateAndImportSchema_Click(object sender, EventArgs e)
        {
            if (TSchema.Nodes.Count > 0 && TSchema.Nodes[0].Nodes.Count > 0)
            {
                ECSchema schema = new ECSchema(TSchema.Nodes[0].Text, 1, 0, "DgnECExample");

                foreach (TreeNode classNode in TSchema.Nodes[0].Nodes)
                {
                    ECClass someClass = new ECClass(classNode.Text);

                    foreach (TreeNode propNode in classNode.Nodes)
                    {
                        ECProperty someProp = new ECProperty(propNode.Text, GetTypeFromString(propNode.Tag.ToString()));
                        someClass.Add(someProp);
                    }
                    schema.AddClass(someClass);
                }

                if (SchemaImportStatus.Success == DgnECManager.Manager.ImportSchema(schema, Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnFile(), new ImportSchemaOptions()))
                {
                    DgnECExample.Instance().AddSchema(schema.Name);
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Get IECType from string.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private IECType GetTypeFromString(string typeName)
        {
            switch (typeName)
            {
                case "String":
                    return ECObjects.StringType;
                case "Boolean":
                    return ECObjects.BooleanType;
                case "Integer":
                    return ECObjects.IntegerType;
                case "Double":
                    return ECObjects.DoubleType;
            }
            return ECObjects.StringType;
        }
    }
}
