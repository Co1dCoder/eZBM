using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.DgnPlatformNET.Elements;
using Bentley.EC.Persistence.Query;
using Bentley.ECObjects;
using Bentley.ECObjects.Instance;
using Bentley.ECObjects.Json;
using Bentley.ECObjects.Schema;
using eZBMCE.AddinManager;
using eZBMCE.Debug.EcObject;
using eZBMCE.LocatePrimitive;
using eZBMCE.Utilities;
using ManagedToolsExample;
using Application = Bentley.Interop.MicroStationDGN.Application;
using BM = Bentley.MstnPlatformNET;
using BMI = Bentley.MstnPlatformNET.InteropServices;
using Element = Bentley.DgnPlatformNET.Elements.Element;


namespace eZBMCE.Debug
{
    /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
    [EcDescription(CommandDescription)]
    [Bentley.MstnPlatformNET.AddIn(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    public class FastDebug : IBMExCommand
    {

        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"DebugFast";

        private const string CommandDescription = @"快速调试";

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void DebugFast()
        {
            AddinManagerDebuger.ExecuteCommand(DebugFast);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new FastDebug();
            return AddinManagerDebuger.DebugInAddinManager(s.DebugFast,  ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
        public AddinManagerDebuger.ExternalCmdResult DebugFast(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            // Add your code here
      
            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        #region --- 界面操作

        #endregion
    }
}