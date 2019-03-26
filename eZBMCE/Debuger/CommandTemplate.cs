using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using eZBMCE.AddinManager;
using eZBMCE.Utilities;
using Application = Bentley.Interop.MicroStationDGN.Application;
using BM = Bentley.MstnPlatformNET;
using BMI = Bentley.MstnPlatformNET.InteropServices;
using Element = Bentley.DgnPlatformNET.Elements.Element;

/*
 output path: C:\Softwares\Civil Engineering\Bentley\MicroStation CE\MicroStation\Mdlapps 
 output path: D:\GithubProjects\eZBM\eZBMCE\bin

 MDL LOAD "eZBMCE.dll"
 MDL LOAD "D:\GithubProjects\eZBM\eZBMCE\bin\eZBMCE.dll"
 * 
 * 
 */

namespace eZBMCE.Debug
{
    /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
    [EcDescription(CommandDescription)]
    [Bentley.MstnPlatformNET.AddIn(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    public class CommandTemplate : IBMExCommand
    {

        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"TemplateCmd";
        private const string CommandDescription = @"命令模板";
        private const string CommandText = CommandDescription;

        /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
        /// <remarks>调试完成后直接在 Bentley 中加载插件并调用</remarks>
        //[CommandMethod(eZConstants.eZGroupCommnad, CommandName,
        //    CommandFlags.Interruptible | CommandFlags.UsePickSet | CommandFlags.NoBlockEditor)
        //, DisplayName(CommandText), Description(CommandDescription)
        //, RibbonItem(CommandText, CommandDescription, eZConstants.ImageDirectory + "HighFill_32.png")]

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void TemplateCmd()
        {
            AddinManagerDebuger.ExecuteCommand(DoSomething);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new CommandTemplate();
            return AddinManagerDebuger.DebugInAddinManager(s.DoSomething,  ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;
        /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
        public AddinManagerDebuger.ExternalCmdResult DoSomething(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            var comApp = Bentley.MstnPlatformNET.InteropServices.Utilities.ComApp;
            // var pl = AddinManagerDebuger.PickObject<Curve>(docMdf.acEditor);
            var app = docMdf.Application;
            var sele = Utilities.Utils.GetSelection(comApp);
            var eCom = sele[0];
            var eNative = eCom.AsNativeElement(docMdf.DgnModel);
            docMdf.WriteMessageLineNow( 3, 4, 5, 3, 2);
            docMdf.WriteMessageLineNow( 3, 4, 5, 4, 4, 4, 4, 4);
            docMdf.WriteLineIntoDebuger(1, 2, 3);
            var length = 0.0;

            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        #region --- 界面操作

        #endregion

    }
}