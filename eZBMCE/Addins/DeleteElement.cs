using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.GeometryModel.SDK.Edit;
using Bentley.CifNET.LinearGeometry;
using Bentley.CifNET.Objects;
using Bentley.CifNET.SDK;
using Bentley.CifNET.SDK.Edit;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.DgnPlatformNET.Elements;
using Bentley.ECObjects.Schema;
using Bentley.GeometryNET;
using eZBMCE.AddinManager;
using eZBMCE.Addins;
using BM = Bentley.MstnPlatformNET;
using CircularArc = Bentley.CifNET.LinearGeometry.CircularArc;
using Utils = eZBMCE.Utilities.Utils;

namespace eZBMCE.Addins
{
    /// <summary> 通过 analyze element deletebyid *** 强行删除元素 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    public class DeleteElement : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"DeleteElement";

        private const string CommandDescription = @"通过 analyze element deletebyid *** 强行删除元素";

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void DeleteElementById()
        {
            AddinManagerDebuger.ExecuteCommand(DeleteElementById);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new DeleteElement();
            return AddinManagerDebuger.DebugInAddinManager(s.DeleteElementById, ref errorMessage, ref elementSet);
        }

        /// <summary> 在 mdl load 中进行调用和调试，而不用 AddinManager 进行调试 </summary>
        public static void DeleteElementById_mdlLoad()
        {
            var cifTest = new DeleteElement();
            using (var docMdf = new DocumentModifier(true))
            {
                var ret = cifTest.DeleteElementById(docMdf);
            }
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> 遍历廊道 </summary>
        public AddinManagerDebuger.ExternalCmdResult DeleteElementById(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            //ManagedSDKExample.PIAlignmentCreator.ShowMessage();
            var selection = Utils.GetSelection();
            var session = BM.Session.Instance;
            foreach (var ele in selection)
            {
                bool canBeDeleted = true;
                if (canBeDeleted)
                {
                    var id = (long)ele.ElementId;
                    session.Keyin($"analyze element deletebyid {id}");
                }
            }
            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }
    }
}