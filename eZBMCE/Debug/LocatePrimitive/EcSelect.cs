using System.Collections.Generic;
using Bentley.DgnPlatformNET;
using Bentley.Interop.MicroStationDGN;
using eZBMCE.AddinManager;
using ManagedFenceExample;
using ManagedToolsExample;
using BM = Bentley.MstnPlatformNET;

namespace eZBMCE.Debug
{
    /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    internal sealed class EcSelect : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"SelectElemlents";

        private const string CommandDescription = @"在界面中选择元素";
        private const string CommandText = CommandDescription;

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void SelectElemlents()
        {
            AddinManagerDebuger.ExecuteCommand(SelectElemlents);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute( ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new EcSelect();
            return AddinManagerDebuger.DebugInAddinManager(s.SelectElemlents,  ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> <seealso cref="CommandDescription"/> </summary>
        public AddinManagerDebuger.ExternalCmdResult SelectElemlents(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            docMdf.WriteMessageLineNow($"命令开始");
            DgnElementSetToolTestZengfy.InstallNewInstance(docMdf);
            // FenceByElementTool.InstallNewInstance();
            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }
    }
}