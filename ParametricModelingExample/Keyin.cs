/*-------------------------------------------------------------------------------------+
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
namespace Bentley
{
    namespace ParametricModelingExample
    {
        /// <summary>
        /// Keyins Class
        /// </summary>
        /// 
/*=================================================================================**//**
* @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
        public sealed class Keyin
        {

            /// <summary>
            /// Calls add variables example
            /// </summary>
            /// <param name="unparsed"></param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            public static void CmdAddVariables(string unparsed)
            {
                ParametricModelingExampleAddIn.Instance().ExampleAddVariables();
            }

            /// <summary>
            /// Calls add expressions example
            /// </summary>
            /// <param name="unparsed"></param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            public static void CmdAddExpressions(string unparsed)
            {
                ParametricModelingExampleAddIn.Instance().ExampleAddExpressions();
            }

            /// <summary>
            /// Calls add variations example
            /// </summary>
            /// <param name="unparsed"></param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            public static void CmdAddVariations(string unparsed)
            {
                ParametricModelingExampleAddIn.Instance().ExampleAddVariations();
            }

            /// <summary>
            /// Calls remove variable example
            /// </summary>
            /// <param name="unparsed"></param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            public static void CmdRemoveVariable(string unparsed)
            {
                ParametricModelingExampleAddIn.Instance().ExampleRemoveVariable();
            }

            /// <summary>
            /// Calls remove variation example
            /// </summary>
            /// <param name="unparsed"></param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            public static void CmdRemoveVariation(string unparsed)
            {
                ParametricModelingExampleAddIn.Instance().ExampleRemoveVariation();
            }

            /// <summary>
            /// Calls import variables example
            /// </summary>
            /// <param name="unparsed"></param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            public static void CmdImportVariables(string unparsed)
            {
                ParametricModelingExampleAddIn.Instance().ExampleImportVariables();
            }

            /// <summary>
            /// Calls export variables example
            /// </summary>
            /// <param name="unparsed"></param>
            /// 
/*---------------------------------------------------------------------------------**//**
* @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
            public static void CmdExportVariables(string unparsed)
            {
                ParametricModelingExampleAddIn.Instance().ExampleExportVariables();
            }


        }
    }
}

