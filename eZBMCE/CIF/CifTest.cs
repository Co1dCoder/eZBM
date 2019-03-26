using System.Collections.Generic;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.SDK;
using Bentley.DgnPlatformNET;
using eZBMCE.AddinManager;
using TrainingManagedEditSDKExamples;
using BM = Bentley.MstnPlatformNET;
using BMI = Bentley.MstnPlatformNET.InteropServices;
using BIM = Bentley.Interop.MicroStationDGN;

namespace eZBMCE.Debug.Cif
{
    /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    public class CifTest : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"CifFastDebug";

        private const string CommandDescription = @"Cif 快速调试123";

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void CifFastDebug()
        {
            AddinManagerDebuger.ExecuteCommand(CifFastDebug);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new CifTest();
            return AddinManagerDebuger.DebugInAddinManager(s.CifFastDebug, ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> 遍历廊道 </summary>
        public AddinManagerDebuger.ExternalCmdResult CifFastDebug(DocumentModifier docMdf)
        {
            _docMdf = docMdf;

            CorridorManipulator.CorrdorManipulation(_docMdf);

            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        private static GeometricModel GetGeometricModel()
        {
            // ORD Update4 SDK 中的构造方法：
            // Bentley.CifNET.SDK.ConsensusConnection sdkCon = ConsensusConnection.Create(BM.Session.Instance.GetActiveDgnModelRef());

            // ORD Update5 SDK 中的构造方法：
            ConsensusConnection sdkCon = new ConsensusConnection(BM.Session.Instance.GetActiveDgnModelRef());

            if (sdkCon == null)
            {
                return null;
            }
            GeometricModel geometricModel = sdkCon.GetActiveGeometricModel(); //.GetActive();
            return geometricModel;
        }

        private void CreateAlignment()
        {
        }
    }
}