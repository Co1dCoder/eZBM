/*--------------------------------------------------------------------------------------+
|   Program.cs
|   
|   Main entry class establishing a connection to the MicroStation host.
|
+--------------------------------------------------------------------------------------*/

using System;
using System.Resources;
using System.Windows.Forms;
using Bentley.MstnPlatformNET;
using BM = Bentley.MstnPlatformNET;

namespace eZBMCE.AddinInitialize
{
    /// <summary>
    /// Main entry point class for this addin application.
    /// When loading an AddIn MicroStation looks for a class
    /// derived from AddIn.
    /// </summary>
    [BM.AddIn(MdlTaskID = "eZBMCE")]
    internal sealed class Program : BM.AddIn
    {
        public static Program Addin = null;

        private Program(System.IntPtr mdlDesc) : base(mdlDesc)
        {
            Addin = this;
        }
    
        /// <summary>
        /// Initializes the AddIn. Called by the AddIn loader after
        /// it has created the instance of this AddIn class
        /// </remarks>
        /// <param name="commandLine"></param>
        /// <returns>0 on success</returns>
        protected override int Run(string[] commandLine)
        {
            // Register event handlers
            ReloadEvent += eZBMCE_ReloadEvent;
            UnloadedEvent += eZBMCE_UnloadedEvent;
            //TODO: Add application initialization code here
            MessageBox.Show(@"eZBMCE: Hello World");
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

        /// <summary>
        /// Handles MicroStation UNLOADED event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void eZBMCE_UnloadedEvent(BM.AddIn sender, UnloadedEventArgs eventArgs)
        {
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



        #region Properties               
    private static ResourceManager resourceManager
        {
            get { return Properties.Resources.ResourceManager; }
        }
        #endregion
    }
}