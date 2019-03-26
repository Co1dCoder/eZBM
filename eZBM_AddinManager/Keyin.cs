/*
MDL LOAD "D:\GithubProjects\eZBM\bin\eZBM_AddinManager.dll"
*/
using System.Windows.Forms;

namespace eZBMCE.AddinManager
{
    /// <summary>Keyin to function mapping.</summary>
    internal sealed class Keyin
    {
        /*------------------------------------------------------------------------------------**/
        /// <summary>Keyin to function mapping.
        /// Calls ManagedToolsExample.StartPlaceGroupedHoleTool to start place tool</summary>
        /// <author>BentleySystems</author>
        /*--------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdLoadAddinManager(string unparsed)
        {
            //var loader = new cmd_AddinManagerLoader();
            //loader.LoadAddinManager();
            cmd_AddinManagerLoader.LoadAddinManager();
        }
        public static void LastExternalCommand(string unparsed)
        {
            cmd_AddinManagerLoader.LastExternalCommand();
        }
    }
}