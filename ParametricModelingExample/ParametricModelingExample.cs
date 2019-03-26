/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Constraints/ParametricModelingExample/ParametricModelingExample.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.MstnPlatformNET;
using Bentley.ECObjects.Instance;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.GeometryNET;
using Bentley.GeometryNET.Common;
using Bentley.DgnPlatformNET.Constraint2d;
using Bentley.DgnPlatformNET.Elements;
using System.Diagnostics;
namespace Bentley
    {
    namespace ParametricModelingExample
        {
        /// <summary>
        /// Constraint2dNetDemoAddin
        /// </summary>
        /// 
/*=================================================================================**//**
* @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
        [Bentley.MstnPlatformNET.AddInAttribute (MdlTaskID = "Bentley.ParametricModelingExample")]
        internal sealed class ParametricModelingExampleAddIn : AddIn
            {
            private static ParametricModelingExampleAddIn s_dgnParametricModelingExampleAddIn = null;

            #region AddIn Functions

            /// <summary>
            /// 
            /// </summary>
            /// <param name="mdlDesc"></param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            public ParametricModelingExampleAddIn (System.IntPtr mdlDesc) 
                : base (mdlDesc)
                {
                s_dgnParametricModelingExampleAddIn = this;
                }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="commandLine"></param>
            /// <returns></returns>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            protected override int Run (string[] commandLine)
                {
                return 0;
                }

            // <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            internal static ParametricModelingExampleAddIn Instance ()
                {
                return s_dgnParametricModelingExampleAddIn;
                }

            #endregion

            #region Variable Functions

            /// <summary>
            /// Get definiton model.
            /// </summary>
            /// <param name="modelName">Definition model name.</param>
            /// <param name="createIfNotFound">Create a new definition model if the model does not exist.</param>
            /// <returns>Returns definition model.</returns>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private DgnModel GetDefinitionModel (string modelName = null, bool createIfNotFound = true)
                {
                if (null == modelName)
                    return Session.Instance.GetActiveDgnModel ();
                
                DgnFile activeFile = Session.Instance.GetActiveDgnFile();
                if (null == activeFile)
                    return null;

                StatusInt loadStatus;
                DgnModel defModel = activeFile.LoadRootModelById (out loadStatus, activeFile.FindModelIdByName (modelName), true, true, false);
                if (null == defModel)
                    {
                    DgnModelStatus status;
                    defModel = activeFile.CreateNewModel(out status, modelName, DgnModelType.Normal, true, null);
                    }

               return defModel;
               }

            /// <summary>
            /// Create variable with double value
            /// </summary>
            /// <param name="EditParameterDefinitions">Parameter definitons</param>
            /// <param name="varName">Variable name.</param>
            /// <param name="value">Variable value.</param>
            /// <param name="type">Variable type.</param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private void CreateVariable (EditParameterDefinitions defs, string varName, double value, ParameterType type)
                {
                ParameterDefinition v = defs.FindByLabel (varName);

                if (null != v)
                    {
                    //variable exist.
                    MessageCenter.Instance.ShowErrorMessage (string.Format (Properties.Resource.Error_ParameterExist, varName), null, false);
                    return; 
                    }

                defs.Add (varName, type, value, false);
                return;
                }

            /// <summary>
            /// Create variable with integer value
            /// </summary>
            /// <param name="EditParameterDefinitions">Parameter definitons</param>
            /// <param name="varName">Variable name.</param>
            /// <param name="value">Variable value.</param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private void CreateVariable (EditParameterDefinitions defs, string varName, int value)
                {
                ParameterDefinition v = defs.FindByLabel (varName);

                if (null != v)
                    {
                    //variable exist.
                    MessageCenter.Instance.ShowErrorMessage (string.Format (Properties.Resource.Error_ParameterExist, varName), null, false);
                    return; 
                    }

                defs.Add (varName, value, false);
                return;
                }

            /// <summary>
            /// Create variable with boolean value
            /// </summary>
            /// <param name="EditParameterDefinitions">Parameter definitons</param>
            /// <param name="varName">Variable name.</param>
            /// <param name="value">Variable value.</param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private void CreateVariable (EditParameterDefinitions defs, string varName, bool value)
                {
                ParameterDefinition v = defs.FindByLabel (varName);

                if (null != v)
                    {
                    //variable exist.
                    MessageCenter.Instance.ShowErrorMessage (string.Format (Properties.Resource.Error_ParameterExist, varName), null, false);
                    return; 
                    }

                defs.Add (varName, value, false);
                return;
                }

            /// <summary>
            /// Set expression for variable
            /// </summary>
            /// <param name="v">Target variable</param>
            /// <param name="expression">Expression string.</param>
            /// <param name="defs">Parameter definitions.</param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private void SetExpressionForVariable (ParameterDefinition v, string expression, EditParameterDefinitions defs)
                {
                if (null == v)
                    return;

                ParameterExpressionParser parser = new ParameterExpressionParser();

                ParseParameterExpressionResult result = parser.Parse (expression, v, defs);
                if (ParameterExpressionStatus.Success == result.Status)
                    defs.SetExpressionForParameter (v.AccessString, result.ParsedExpression);
                else
                    MessageCenter.Instance.ShowErrorMessage (Properties.Resource.Error_InvalidExpression, null, false);

                return;
                }

            /// <summary>
            /// Remove varible from model
            /// </summary>
            /// <param name="EditParameterDefinitions">Parameter definitons</param>
            /// <param name="varName">Variable name.</param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private void RemoveVariable (EditParameterDefinitions defs, string varName)
                {
                ParameterDefinition v = defs.FindByLabel (varName);
                if (null == v)
                    {
                    MessageCenter.Instance.ShowErrorMessage (string.Format (Properties.Resource.Error_ParameterNotFound, varName), null, false);
                    return;
                    }

                defs.Remove (v.AccessString);
                return;
                }

            /// <summary>
            /// Obtain save file location
            /// </summary>
            /// <returns>Returns file location.</returns>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private string ObtainExportFileLocation()
                {
                var dlg = new SaveFileDialog();
                dlg.Filter = Properties.Resource.FileType_Csv + "|*.csv";
                dlg.Title = Properties.Resource.Title_Export;
                if (DialogResult.OK == dlg.ShowDialog())
                    return dlg.FileName;

                return null;
                }

            /// <summary>
            /// Obtain open file location
            /// </summary>
            /// <returns>Returns file location.</returns>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private string ObtainImportFileLocation()
                {
                var dlg = new OpenFileDialog();
                dlg.Filter = Properties.Resource.FileType_Csv + "|*.csv";
                dlg.Title = Properties.Resource.Title_Import;
                if (DialogResult.OK == dlg.ShowDialog())
                    return dlg.FileName;
            
                return null;
                }

            /// <summary>
            /// Active model
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            [DllImport ("ustation.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            internal static extern StatusInt mdlModelRef_activateAndDisplay (IntPtr newModelRef);

            #endregion

            #region Geometry functions

            /// <summary>
            /// Create line element.
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private LineElement CreateLine(DSegment3d segment, string modelName = null)
                {
                DgnModel model = GetDefinitionModel (modelName, true);
                if (null == model)
                    return null;

                LineElement lineElement = new Bentley.DgnPlatformNET.Elements.LineElement(model, null, segment);
                //lineElement.AddToModel();
                return lineElement;
                }

            /// <summary>
            /// Create line string element.
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private LineStringElement CreateLineString (DPoint3d[] points, string modelName = null)
                {
                DgnModel model = GetDefinitionModel (modelName, true);
                if (null == model)
                    return null;

                LineStringElement lineString = new LineStringElement(model, null, points);

                return lineString;
                }

            /// <summary>
            /// Create ellipse element.
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private Bentley.DgnPlatformNET.Elements.EllipseElement CreateEllipse(DPoint3d center, double major, double minor, string modelName = null)
                {
                DgnModel model = GetDefinitionModel(modelName, true);
                if (null == model)
                    return null;

                Bentley.DgnPlatformNET.Elements.EllipseElement ellipse = new DgnPlatformNET.Elements.EllipseElement(model, null, center, major, minor, 0.0);

                return ellipse;
                }

            /// <summary>
            /// Create arc element.
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private Bentley.DgnPlatformNET.Elements.ArcElement CreateArc(DPoint3d center, double axis1, double axis2, double rotation, double start, double sweep, string modelName = null)
                {
                DgnModel model = GetDefinitionModel(modelName, true);
                if (null == model)
                    return null;

                Bentley.DgnPlatformNET.Elements.ArcElement arc = new DgnPlatformNET.Elements.ArcElement(model, null, center, axis1, axis2, rotation, start, sweep);

                return arc;
                }

            /// <summary>
            /// Create complex shape element.
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            private Bentley.DgnPlatformNET.Elements.ComplexShapeElement CreateComplexShape(DPoint3d[] points, DPoint3d center, bool isHole, string modelName = null)
            {
                DgnModel modelRef = GetDefinitionModel(modelName, true);
                if (null == modelRef)
                    return null;

                //creaet header
                ComplexShapeElement shapeEl = new ComplexShapeElement (modelRef, null);

                LineStringElement lineStringEl = CreateLineString(points, null);
                if (DgnPlatformNET.BentleyStatus.Success != shapeEl.AddComponentElement(lineStringEl))
                    return null;

                double radius = Math.Abs(points[2].X - points[1].X) / 2.0;
                ArcElement ehArc = CreateArc (center, radius, radius, 0, Math.PI, Math.PI);
                if (DgnPlatformNET.BentleyStatus.Success != shapeEl.AddComponentElement(ehArc))
                    return null;

                if (DgnPlatformNET.BentleyStatus.Success != shapeEl.AddComponentComplete())
                    return null;

                return shapeEl;
            }
            
            #endregion

            #region Examples
            
            /// <summary>
            /// Example: Add Variables
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            internal void ExampleAddVariables()
                {
                //1. Get active model.
                DgnModel model = GetDefinitionModel (null, true);
                if (null == model)
                    return;

                //2. Get parameter definitons.
                EditParameterDefinitions defs = EditParameterDefinitions.GetForModel (model);

                //3. Create variable
                string varName = "var";
                double value = 15000.0;
                CreateVariable (defs, varName, value, ParameterType.Distance);

                return;
                }

            /// <summary>
            /// Example: Add Expressions
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            internal void ExampleAddExpressions()
                {
                //1. Get active model.
                DgnModel model = GetDefinitionModel (null, true);
                if (null == model)
                    return;

                //2. Get parameter definitons.
                EditParameterDefinitions defs = EditParameterDefinitions.GetForModel (model);

                //3. Create varialbe with expression
                string labelR = "Radius1";
                string labelX = "X";
                string labelY = "Y";
                string labelZ = "Z";

                double r = 10.0;
                double x = 10000.0;
                double y = 15000.0;
                double z = 20000.0;

                string exp1 = "10 * sqrt (16)";
                string exp2 = "2 * Y";

                ParameterDefinition paramDefR, paramDefX, paramDefY, paramDefZ;
                if (null == (paramDefR = defs.FindByLabel (labelR)))
                    {
                    CreateVariable (defs, labelR, r, ParameterType.Distance);
                    System.Diagnostics.Debug.Assert (null != (paramDefR = defs.FindByLabel (labelR)));
                    }

                if (null == (paramDefX = defs.FindByLabel (labelX)))
                    {
                    CreateVariable (defs, labelX, x, ParameterType.Scalar);
                    System.Diagnostics.Debug.Assert (null != (paramDefX = defs.FindByLabel (labelX)));
                    }

                if (null == (paramDefY = defs.FindByLabel (labelY)))
                    {
                    CreateVariable (defs, labelY, y, ParameterType.Scalar);
                    System.Diagnostics.Debug.Assert (null != (paramDefY = defs.FindByLabel (labelY)));
                    }

                if (null == (paramDefZ = defs.FindByLabel (labelZ)))
                    {
                    CreateVariable (defs, labelZ, z, ParameterType.Scalar);
                    System.Diagnostics.Debug.Assert (null != (paramDefZ = defs.FindByLabel (labelZ)));
                    }

                SetExpressionForVariable (paramDefR, exp1, defs);
                SetExpressionForVariable (paramDefZ, exp2, defs);

                }

            /// <summary>
            /// Example: Add Variations
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            internal void ExampleAddVariations()
                {
                //1. Get active model.
                DgnModel model = GetDefinitionModel (null, true);
                if (null == model)
                    return;

                //2. Get definition model handler and parameter definitons.
                var hdlr = DgnComponentDefinitionHandler.GetForModel (model).DefinitionModelHandler;
                if (null == hdlr)
                    return;

                EditParameterDefinitions defs = EditParameterDefinitions.GetForModel (model);

                //3. Create variations
                var variations = hdlr.ParameterSets;
                ParameterSet set1, set2;
                if (null == (set1 = variations.FirstOrDefault (x => x.Name == "set1")))
                    set1 = hdlr.CreateParameterSet ("set1", "my set", null);

                if (null == (set2 = variations.FirstOrDefault (x => x.Name == "set2")))
                    set2 = hdlr.CreateParameterSet ("set2", "my set", null);

                //4. Set values
                ParameterDefinition v = defs.FindByLabel ("Y");
                if (null == v)
                    return;
                
                IDgnECInstance props = set1.Properties as IDgnECInstance;
                IECPropertyValue pv = props.FindPropertyValue (v.AccessString, false, false, false, true/*includeAdhoc*/);
                pv.NativeValue = 30000.0;

                props.WriteChanges ();

                return;
                }

            /// <summary>
            /// Example: Remove Variable
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            internal void ExampleRemoveVariable()
                {
                //1. Get active model.
                DgnModel model = GetDefinitionModel (null, true);
                if (null == model)
                    return;

                //2. Get parameter definitons.
                EditParameterDefinitions defs = EditParameterDefinitions.GetForModel (model);

                //3. Remove variable Z
                string varName = "Z";
                ParameterDefinition paramDefZ;
                if (null != (paramDefZ = defs.FindByLabel (varName)))
                    defs.Remove (paramDefZ.AccessString);

                return;
                }

            /// <summary>
            /// Example: Remove Variation
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            internal void ExampleRemoveVariation()
                {
                //1. Get active model.
                DgnModel model = GetDefinitionModel (null, true);
                if (null == model)
                    return;

                //2. Get definition model handler.
                var hdlr = DgnComponentDefinitionHandler.GetForModel (model).DefinitionModelHandler;
                if (null == hdlr)
                    return;

                //3. Remove variation
                var variations = hdlr.ParameterSets;
                ParameterSet set = variations.FirstOrDefault (x => x.Name == "set2");
                if (null != set)
                    hdlr.DeleteParameterSet (set);

                return;
                }

            /// <summary>
            /// Example: Import Variables
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            internal void ExampleImportVariables()
                {
                //1. Get import model
                DgnModel model = GetDefinitionModel ("VARIABLEIMPORT_MODEL", true);
                if (null == model)
                    return;

                //2. Get parameter definitons.
                EditParameterDefinitions defs = EditParameterDefinitions.GetForModel (model);

                //3. Get import csv file name.
                string fileName = ObtainImportFileLocation();
                if (null == fileName)
                    return;

                //4. Import
                ParameterCsvSectionPresenceFlags flags = new ParameterCsvSectionPresenceFlags();
                flags.SetAll();

                ParameterCsvReadContext context = new ParameterCsvReadContext (flags);
                context.SchemaSearchFiles = new DgnLibIterator (DgnLibSelector.ElementStyles);
                defs.ReadFromCsv (fileName, context);

                return;
                }

            /// <summary>
            /// Example: Export Variables
            /// </summary>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            internal void ExampleExportVariables()
                {
                //1. Get active model.
                DgnModel model = GetDefinitionModel (null, true);
                if (null == model)
                    return;

                //2. Get parameter definitons.
                EditParameterDefinitions defs = EditParameterDefinitions.GetForModel (model);

                //3. Get export csv file name.
                string fileName = ObtainExportFileLocation();
                if (null == fileName)
                    return;

                //4. Export
                ParameterCsvSectionPresenceFlags flags = new ParameterCsvSectionPresenceFlags();
                flags.SetAll();

                defs.WriteToCsv (fileName, flags);

                return;
                }

            #endregion

            }
        }
    }
  