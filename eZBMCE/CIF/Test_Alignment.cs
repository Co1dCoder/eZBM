using System.Collections.Generic;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.GeometryModel.SDK.Edit;
using Bentley.CifNET.LinearGeometry;
using Bentley.CifNET.SDK;
using Bentley.CifNET.SDK.Edit;
using Bentley.DgnPlatformNET;
using Bentley.GeometryNET;
using eZBMCE.AddinManager;
using BM = Bentley.MstnPlatformNET;
using BMI = Bentley.MstnPlatformNET.InteropServices;
using BIM = Bentley.Interop.MicroStationDGN;
using CircularArc = Bentley.CifNET.LinearGeometry.CircularArc;

namespace eZBMCE.Cif
{
    /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    public class Test_Alignment : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"TestAlignment";

        private const string CommandDescription = @"路线创建与修改测试";

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void TestAlignment()
        {
            AddinManagerDebuger.ExecuteCommand(TestAlignment);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new Test_Alignment();
            return AddinManagerDebuger.DebugInAddinManager(s.TestAlignment, ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> 路线创建与修改测试 </summary>
        public AddinManagerDebuger.ExternalCmdResult TestAlignment(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            AlignmentModify();

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

        private void AlignmentModify()
        {
            //ConsensusConnectionEdit allows the persistence of civil objects to the DGN file
            ConsensusConnectionEdit conEdit = ConsensusConnectionEdit.GetActive();

            //create SDK.Edit alignment objects from native objects
            // Bentley.CifNET.GeometryModel.SDK.Edit.AlignmentEdit al = AlignmentEdit.CreateByLinearElement(con, complexAlign, true);
            GeometricModel geoModel = conEdit.GetActiveGeometricModel();

            string aliFeatureName = "GeomBL_Test2";
            Alignment ali = geoModel.GetAlignmentByName(aliFeatureName); // 从模型中索引
            AlignmentEdit aliEdit = ali as AlignmentEdit; // 返回 null，无法进行直接转换

            aliEdit = Alignment.CreateFromElement(conEdit, ali.Element) as AlignmentEdit; // 成功转换，此时并未创建一个新的路线
            _docMdf.WriteMessageLineNow("aliEdit 2== null?", aliEdit == null, aliEdit.FeatureName,
                aliEdit.Element.ElementId);

            // 根据某既有路线创建出一个新的路线，注意，此路线此时还没有Name与Feature Name
            AlignmentEdit aliEdit3 = AlignmentEdit.CreateByLinearElement(conEdit, ali.LinearGeometry, true);
            aliEdit3.SetFeatureDefinition(@"Alignment\Geom_Baseline_Ramp"); // 可以成功指定特征定义
            // aliEdit3.Name = "da"; // 但是无法指定特征名称
            _docMdf.WriteMessageLineNow("aliEdit 3== null?", aliEdit3 == null, aliEdit3.Element.ElementId, aliEdit3.Name,
                aliEdit3.FeatureName);

            conEdit.StartTransientMode();

            //persists objects created in persist mode
            conEdit.PersistTransients();
            // 修改交点圆弧半径
            LinearComplex le = aliEdit3.LinearGeometry as LinearComplex;

            LinearElement subLe = le.GetSubLinearElementAtIndex(0);
            if (subLe is Line)
            {
                var line = subLe as Line;

                _docMdf.WriteMessageLineNow("line.1   ");
                LinearElement transformedLine = line.Transform(DTransform3d.FromTranslation(0, -20, 0));
                AlignmentEdit alal = AlignmentEdit.CreateByLinearElement(conEdit, transformedLine, true);
                aliEdit3.Element.DeleteFromModel();
            }
            else if (subLe is CircularArc)
            {
                var arc = subLe as CircularArc;

                _docMdf.WriteMessageLineNow("No.1   ", arc.CenterPoint);

                arc.CenterPoint.SetComponent(1, arc.CenterPoint.Y - 123);
                _docMdf.WriteMessageLineNow("No.2   ", arc.CenterPoint);
            }
        }
    }
}