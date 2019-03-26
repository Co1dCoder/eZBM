/*--------------------------------------------------------------------------------------+
|   Program.cs
|   
|   Main entry class establishing a connection to the MicroStation host.
|
+--------------------------------------------------------------------------------------*/

/*
output path: C:\Softwares\Civil Engineering\Bentley\MicroStation CE\MicroStation\Mdlapps 
output path: D:\GithubProjects\eZBM\eZBMCE\bin

MDL LOAD "eZBM_AddinManager.dll"
MDL LOAD "D:\GithubProjects\eZBM\bin\eZBM_AddinManager.dll"
* 
* 
*/

using System;
using System.Diagnostics;
using BM = Bentley.MstnPlatformNET;

namespace eZBMCE.AddinManager
{
    /// <summary>
    /// Main entry point class for this addin application.
    /// When loading an AddIn MicroStation looks for a class
    /// derived from AddIn.
    /// </summary>
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    internal sealed class ZfyPlugin : BM.AddIn
    {
        public static ZfyPlugin Addin = null;

        private ZfyPlugin(IntPtr mdlDesc) : base(mdlDesc)
        {
            Addin = this;
        }

        /// <summary>
        /// Initializes the AddIn. Called by the AddIn loader after
        /// it has created the instance of this AddIn class
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns>0 on success</returns>
        protected override int Run(string[] commandLine)
        {
            // Register event handlers
            ReloadEvent += eZBMCE_ReloadEvent;
            UnloadedEvent += eZBMCE_UnloadedEvent;
            //TODO: Add application initialization code here
            //  MessageBox.Show(@"Hello World : Program eZBM_AddinManager" + MdlTaskId);
            //
            return 0;
        }

        ///<summary>
        /// Handles MDL ONUNLOAD requests when the application Is being unloaded.
        /// </summary>
        /// <param name="eventArgs"></param>
        private void eZBMCE_ReloadEvent(BM.AddIn sender, ReloadEventArgs eventArgs)
        {
            //TODO: add specific handling for this event here
        }

        #region Addin 卸载时的事件处理

        /// <summary>
        /// Handles MicroStation UNLOADED event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void eZBMCE_UnloadedEvent(BM.AddIn sender, UnloadedEventArgs eventArgs)
        {
            AddinTerminate();
            //TODO: add specific handling for this event here
        }

        /// <summary>
        /// Handles MDL ONUNLOAD requests when the application is being unloaded.
        /// </summary>
        /// <param name="eventArgs"></param>
        protected override void OnUnloading(UnloadingEventArgs eventArgs)
        {
            base.OnUnloading(eventArgs);
        }


        /// <summary> 在Addin插件卸载过程中，是否已经将AddinManager窗口中的插件路径保存在mySettings中。 </summary>
        /// <remarks>因为在Addin插件卸载过程中，可能会多次进入此Terminate函数</remarks>
        private bool _hasValidNodesInfoSaved = false;

        private void AddinTerminate()
        {
            try
            {
                form_AddinManager frm = form_AddinManager.GetUniqueForm();
                var nodesInfo = frm.NodesInfo;
                var count = nodesInfo.Count;
                if (!_hasValidNodesInfoSaved)
                {
                    AssemblyInfoDllManager.SaveAssemblyInfosToSettings(nodesInfo);
                    if (count > 0)
                    {
                        _hasValidNodesInfoSaved = true;
                    }
                }
                //
            }
            catch (Exception ex)
            {
                Debug.Print("AddinManager 插件关闭时出错： \n\r" + ex.Message + "\n\r" + ex.StackTrace);
            }
        }

        #endregion
    }
}