using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.ECObjects.Lightweight; 
using Bentley.GeometryNET;
using Bentley.Interop.MicroStationDGN;
using eZBMCE.AddinManager;
using eZBMCE.Utilities;
using ManagedFenceExample;
using ManagedToolsExample;
using MSDIAddin;
using BM = Bentley.MstnPlatformNET;
using ComplexStringElement = Bentley.DgnPlatformNET.Elements.ComplexStringElement;
using ConeElement = Bentley.DgnPlatformNET.Elements.ConeElement;
using Element = Bentley.DgnPlatformNET.Elements.Element;
using LineElement = Bentley.DgnPlatformNET.Elements.LineElement;
using ViewGroup = Bentley.DgnPlatformNET.ViewGroup;

namespace eZBMCE.Debug.Geometry
{
    /// <summary> 对图层进行管理 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    internal sealed class EcLevelManager : AddinManager.IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"TestCreateGeometry";

        private const string CommandDescription = @"对图层进行管理";
        private const string CommandText = CommandDescription;

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void TestCreateGeometry()
        {
            AddinManagerDebuger.ExecuteCommand(ManageLevels);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,ref IList<ElementId> elementSet)
        {
            var s = new EcLevelManager();
            return AddinManagerDebuger.DebugInAddinManager(s.ManageLevels, ref errorMessage, ref elementSet);
        }

        #endregion

        public static DocumentModifier _docMdf;

        /// <summary> <seealso cref="CommandDescription"/> </summary>
        public AddinManagerDebuger.ExternalCmdResult ManageLevels(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            docMdf.WriteMessageLineNow($"几何测试命令开始");
            var ele1 = Utils.GetSelection()[0];
            docMdf.WriteMessageLineNow(ele1.ElementId);

            MSDIAddinClass.MstnControlDemo(null);
            //ulong id = ele1.ElementId;

            //var f = new Form1(ele1.ElementId);
            //f.Show();

            return AddinManagerDebuger.ExternalCmdResult.Commit;

        }

        public AddinManagerDebuger.ExternalCmdResult ManageLevels1(DocumentModifier docMdf)
        {
            //
            var file = docMdf.DgnFile;
            var model = docMdf.DgnModel;
            var attaches = model.GetDgnAttachments();
            foreach (var attamt in attaches)
            {
                var m1 = attamt.AsDgnModel(); // 返回 null
                var m2 = attamt.GetDgnModel(); // 返回 null
                var m3 = attamt.GetParentModelRef();
                DgnDocumentMoniker dm = attamt.GetAttachMoniker();
                docMdf.WriteLineIntoDebuger(
                    "file name", attamt.AttachFileName,
                    "model Name", attamt.AttachModelName,
                    "ParentModelRef", attamt.GetParent().IsDgnAttachment,
                    "Nest", attamt.NestDepth, attamt.IsDirectDgnAttachment, attamt.IsNestedDgnAttachment,
                    // "Moniker" , dm.ParentSearchPath,
                    "FullPath", dm.SavedFileName
                    );

            }
            Element ele = null;

            return AddinManagerDebuger.ExternalCmdResult.Cancel;

            file.GetLoadedModelsCollection();

            FileLevelCache level1 = file.GetLevelCache();
            LevelCache level2 = model.GetLevelCache();
            FileLevelCache level3 = model.GetFileLevelCache();

            docMdf.WriteMessageLineNow("成功 ViewGroup");

            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }
        private static DgnAttachmentCollection GetAttachments(DgnModel model)
        {
            return model.GetDgnAttachments();
        }


    }
}