/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/DgnEC/DgnECExample/SchemaListForm.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET.DgnEC;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DgnECExample
{
    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    public partial class SchemaListForm : Form
    {

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        public SchemaListForm()
        {
            InitializeComponent();
            FindSchemas();
        }

        /*---------------------------------------------------------------------------------**//**
        * Find schemas and list them.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void FindSchemas()
        {
            DgnECManager manager = DgnECManager.Manager;
            var mdl = Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModel();
            List<string> schemas = (List<string>)manager.DiscoverSchemasForModel(mdl, ReferencedModelScopeOption.All, false);

            if (null == schemas)
                return;

            foreach (string schema in schemas)
            {
                LstSchemas.Items.Add(schema);
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Closes this form.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
