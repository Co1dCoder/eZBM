using System.Collections.Generic;
using System.Linq;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using Bentley.Interop.MicroStationDGN;
using eZBMCE.AddinManager;
using ManagedFenceExample;
using ManagedToolsExample;
using BM = Bentley.MstnPlatformNET;
using ComplexStringElement = Bentley.DgnPlatformNET.Elements.ComplexStringElement;

namespace eZBMCE.Debug.Geometry
{
    /// <summary> 几何模型的信息提取与几何元素的创建 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    internal sealed class EcGeometryTest : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"TestGeometry";

        private const string CommandDescription = @"几何模型的信息提取与几何元素的创建";
        private const string CommandText = CommandDescription;

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void TestGeometry()
        {
            AddinManagerDebuger.ExecuteCommand(TestGeometry);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute( ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new EcGeometryTest();
            return AddinManagerDebuger.DebugInAddinManager(s.TestGeometry,  ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> <seealso cref="CommandDescription"/> </summary>
        public AddinManagerDebuger.ExternalCmdResult TestGeometry(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            docMdf.WriteMessageLineNow($"几何测试命令开始");
            //
            var eles = Utilities.Utils.GetSelection();
            if (eles.Length == 0) return AddinManagerDebuger.ExternalCmdResult.Cancel;
            var ele = eles[0];
            var cts = ele as MeshHeaderElement;

            PolyfaceConstruction pc = new PolyfaceConstruction();
            // 提取 LineStringElement 对象的几何信息
            PolyfaceHeader ph = cts.GetMeshData();
            docMdf.WriteMessageLineNow(ph.Point.Count());

            // 构造网格

            var mesh = new MeshHeaderElement(docMdf.DgnModel, null, ph);
            mesh.AddToModel();

            mesh.ApplyTransform(new TransformInfo(new DTransform3d(DMatrix3d.Scale(1, 2, 3))));
            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }


        private static Bentley.DgnPlatformNET.Elements.ShapeElement CreateShapeElement(DgnModel model, DPoint3d start, DPoint3d opposite)
        {
            DPoint3d[] points = new DPoint3d[5];
            points[0] = points[1] = points[3] = points[4] = start;
            points[2] = opposite;

            points[1].X = opposite.X;
            points[3].Y = opposite.Y;

            var shape = new Bentley.DgnPlatformNET.Elements.ShapeElement(model, null, points);

            return shape;
        }

    }
}