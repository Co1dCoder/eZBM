using System;
using System.Windows.Forms;
using eZBMCE.AddinManager;

namespace eZBMCE.AddinManager
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    [Bentley.MstnPlatformNET.AddIn(MdlTaskID = "eZBMCEAddinManager")]
    public class cmd_AddinManagerLoader
    {
        // The CommandMethod attribute can be applied to any public  member 
        // function of any public class.
        // The function should take no arguments and return nothing.
        // If the method is an intance member then the enclosing class is 
        // intantiated for each document. If the member is a static member then
        // the enclosing class is NOT intantiated.
        //
        // NOTE: CommandMethod has overloads where you can provide helpid and
        // context menu.

        /// <summary> 整个AddinManager工具首次被加载 </summary>
        private static bool _addinManagerFirstLoaded = true;

        // Modal Command with localized name
        public static void LoadAddinManager() // This method can have any name
        {
            form_AddinManager frm = form_AddinManager.GetUniqueForm();
            if (_addinManagerFirstLoaded)
            {
                // 将上次插件卸载时保存的程序集数据加载进来
                var nodesInfo = AssemblyInfoDllManager.GetInfosFromSettings();
                frm.RefreshTreeView(nodesInfo);
                //
                _addinManagerFirstLoaded = false;
            }
            else
            {
            }
            frm.WindowState = FormWindowState.Normal; // 取消其最小化
            // MessageBox.Show(Enum.GetName(typeof(FormWindowState), frm.WindowState) + frm.Visible);
            if (!frm.Visible)
            {
                frm.Show(null);
            }
            //Application.ShowModelessDialog(null, frm);
            //Application.ShowModalDialog(frm);
        }

        // Modal Command with localized name
        public static void LastExternalCommand() // This method can have any name
        {
            ExCommandExecutor.InvokeCurrentExternalCommand();
        }

    }
}
