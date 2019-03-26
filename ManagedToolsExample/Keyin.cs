/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/ManagedToolsExample/Keyin.cs $

MDL LOAD "D:\GithubProjects\eZBM\bin\ManagedToolsExample.dll"

|
|  $Copyright: (c) 2017 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

namespace ManagedToolsExample
{

    /*====================================================================================**/
    /// <author>BentleySystems</author>
    /*==============+===============+===============+===============+===============+======*/
    public sealed class Keyin
    {
        /*------------------------------------------------------------------------------------**/
        /// <summary>Keyin to function mapping.
        /// Calls ManagedToolsExample.StartPlaceGroupedHoleTool to start place tool</summary>
        /// <author>BentleySystems</author>
        /*--------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdStartPlaceGroupedHoleTool(string unparsed)
        {
            ManagedToolsExample.Instance().StartPlaceGroupedHoleTool();
        }


        /*------------------------------------------------------------------------------------**/
        /// <summary>Keyin to function mapping.
        /// Calls ManagedToolsExample.StartModifyGroupedHoleTool to start modify tool</summary>
        /// <author>BentleySystems</author>
        /*--------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdModifyPlaceGroupedHoleTool(string unparsed)
        {
            ManagedToolsExample.Instance().StartModifyGroupedHoleTool();
        }

        /*------------------------------------------------------------------------------------**/
        /// <summary>Keyin to function mapping.
        /// Calls ManagedToolsExample.StartPlaceGroupedHoleTool to start place tool</summary>
        /// <author>BentleySystems</author>
        /*--------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdStartBlendTool(string unparsed)
        {
            ManagedToolsExample.Instance().StartBlendTool(0);
        }

    }
}
