using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.GeometryModel.SDK.Edit;
using Bentley.CifNET.LinearGeometry;
using Bentley.CifNET.Objects;
using Bentley.CifNET.SDK;
using Bentley.CifNET.SDK.Edit;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using eZBMCE.AddinManager;
using BM = Bentley.MstnPlatformNET;
using BMI = Bentley.MstnPlatformNET.InteropServices;
using BIM = Bentley.Interop.MicroStationDGN;
using CircularArc = Bentley.CifNET.LinearGeometry.CircularArc;

namespace eZBMCE.Cif.TemplateLib
{
    /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    public class Test_GetXSCutPoints : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"TestGetXSCutPoints";

        private const string CommandDescription = @"从廊道中提取横断面信息测试";

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void TestGetXSCutPoints()
        {
            AddinManagerDebuger.ExecuteCommand(TestGetXSCutPoints);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new Test_GetXSCutPoints();
            return AddinManagerDebuger.DebugInAddinManager(s.TestGetXSCutPoints, ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> 遍历廊道 </summary>
        public AddinManagerDebuger.ExternalCmdResult TestGetXSCutPoints(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            // Bentley.DgnPlatformNET.Create.BodyFromSweep();

            ConsensusConnection sdkCon = new ConsensusConnection(BM.Session.Instance.GetActiveDgnModelRef());
            if (sdkCon == null)
            {
                return AddinManagerDebuger.ExternalCmdResult.Commit;
            }
            GeometricModel geoMdl = sdkCon.GetActiveGeometricModel(); //.GetActive();

            //Corridor corrEdit = geoMdl.Corridors.FirstOrDefault(r => r.Name == "GeomBL_4#公路1");
            Corridor corr = geoMdl.Corridors.FirstOrDefault(r => r.Name == "GeomBL");
            if (corr == null)
            {
                MessageBox.Show("没找到 Corridor");
            }
            // 提取既有 Corridor 中指定桩号位置的横断面信息
            XSCutPoint[] pts = corr.GetXSCutPoints(distAlong: 50, leftWidth: 100, rightWidth:100, leftOffset: 0);
            //XSCutPoint[] pts3 = corr.GetXSCutPoints(distAlong: 11, leftWidth: 500, rightWidth: 500, leftOffset: 0);
            if (pts == null)
            {
                MessageBox.Show($"未提取到 GetXSCutPoints");
                return AddinManagerDebuger.ExternalCmdResult.Cancel;
            }
            if (pts != null || pts.Length > 0)
            {
                foreach (XSCutPoint pt in pts)
                {
                    string value = $"pts2.PointName= {pt.PointName}, pts2.PointFeatureName= {pt.PointFeatureName}, pointOnPlanCorr = {pt.PointOnPlan}, pointcorr2 = {pt.Point}";
                    // MessageBox.Show(value);
                    docMdf.WriteLineIntoDebuger(value);
                }
            }

            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }
    }
}