/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/DgnEC/DgnECExample/DgnECExample.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

/*--------------------------------------------------------------------------------------+
 * This exmaple demonstrates the following
 * 1. Finding EC Schemas in a DGN file
 * 2. Creating and Importing EC Schemas in a DGN file
 * 3. Writing instance of some EC Class on some element.
 * 4. Finding EC Class instances on some element.
 * 5. Finding EC Class instances on all elements.
 * 
 * Finding EC Schemas in a DGN file will find all schemas imported in the DGN file.
 * 
 * The other four features are limited to schemas the user will create and import graphically using this sample.
 * 
 * This sample allows crating of simple schemas and then importing them into the file.
 * One can create schemas with only top level classes and properties only of type String, Boolean, int and double.
 * 
 * Instances can only be created and found of the classes of the above mentioned schemas.
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.ECObjects.Schema;
using Bentley.ECObjects.XML;
using System.Collections.Generic;

namespace DgnECExample
{

    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    [Bentley.MstnPlatformNET.AddInAttribute(MdlTaskID = "DgnECExample")]
    public sealed class DgnECExample : Bentley.MstnPlatformNET.AddIn
    {

        private List<string> m_allSchemas;

        private static DgnECExample s_dgnECExampleAddin = null;

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        public DgnECExample(System.IntPtr mdlDesc)
            : base(mdlDesc)
        {
            m_allSchemas = new List<string>();
            s_dgnECExampleAddin = this;
        }

        /*---------------------------------------------------------------------------------**//**
        * Required Run method of AddIn class.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override int Run(string[] commandLine)
        {
            return 0;
        }

        /*---------------------------------------------------------------------------------**//**
        * Returns an instance of the Addin class.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        internal static DgnECExample Instance()
        {
            return s_dgnECExampleAddin;
        }

        /*---------------------------------------------------------------------------------**//**
        * Adds the name of the new schema that is imported to the list.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        internal void AddSchema(string importedSchema)
        {
            m_allSchemas.Add(importedSchema);
        }

        /*---------------------------------------------------------------------------------**//**
        * Returns the list of imported schemas.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        internal List<string> GetImportedSchemas()
        {
            return m_allSchemas;
        }

        /*---------------------------------------------------------------------------------**//**
        * Find schemas.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        internal void FindSchemas()
        {
            SchemaListForm schemaForm = new SchemaListForm();
            schemaForm.Show();
        }

        /*---------------------------------------------------------------------------------**//**
        * Find instances.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        internal void FindAllInstances()
        {
            FindInstancesOnElementForm instancesForm = new FindInstancesOnElementForm(true);
            instancesForm.Show();
        }

        /*---------------------------------------------------------------------------------**//**
        * Import schemas.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        internal void ImportSchemas()
        {
            CreateAndImportSchemaForm importForm = new CreateAndImportSchemaForm();
            importForm.Show();
        }

        /*---------------------------------------------------------------------------------**//**
        * Write instance on selected element.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        internal void WriteInstanceOnElement()
        {
            WriteInstanceOnElementTool.InstallNewInstance();
        }

        /*---------------------------------------------------------------------------------**//**
        * Find instances on selected element.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        internal void FindAllInstancesOnElement()
        {
            FindInstancesOnElementTool.InstallNewInstance();
        }
    }
}
