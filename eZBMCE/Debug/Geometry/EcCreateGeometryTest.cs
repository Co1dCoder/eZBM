using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.ECObjects.Lightweight;
using Bentley.GeometryNET;
using Bentley.Interop.MicroStationDGN;
using eZBMCE.AddinManager;
using eZBMCE.Utilities;
using ManagedFenceExample;
using ManagedToolsExample;
using BM = Bentley.MstnPlatformNET;
using ComplexStringElement = Bentley.DgnPlatformNET.Elements.ComplexStringElement;
using ConeElement = Bentley.DgnPlatformNET.Elements.ConeElement;
using LineElement = Bentley.DgnPlatformNET.Elements.LineElement;
using ViewGroup = Bentley.DgnPlatformNET.ViewGroup;

namespace eZBMCE.Debug.Geometry
{
    /// <summary> 几何模型的信息提取与几何元素的创建 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    internal sealed class EcCreateGeometryTest : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"TestCreateGeometry";

        private const string CommandDescription = @"几何元素的创建";
        private const string CommandText = CommandDescription;

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void TestCreateGeometry()
        {
            AddinManagerDebuger.ExecuteCommand(TestCreateGeometry);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(  ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new EcCreateGeometryTest();
            return AddinManagerDebuger.DebugInAddinManager(s.TestCreateGeometry,  ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> <seealso cref="CommandDescription"/> </summary>
        public AddinManagerDebuger.ExternalCmdResult TestCreateGeometry(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            docMdf.WriteMessageLineNow($"几何测试命令开始");

            //
            DgnModel model3d = docMdf.DgnFile.GetLoadedModelsCollection()[1];
            ViewGroup vg = docMdf.DgnFile.GetViewGroups().GetActive();
            Viewport vp = docMdf.ActiveViewport;
            docMdf.WriteMessageLineNow(vg.Name, model3d.ModelName,"屏", vp.ScreenNumber);

            const int viewNum = 0;
            var viewinfo = vg.GetViewInformation(viewNum);
            var vpInfo = vg.GetViewportInformation(viewNum);

            viewinfo.SetBackgroundColor(new RgbColorDef(255, 255, 0));

            viewinfo.SetRootModel(model3d);
            vp.SynchWithViewInformation(true, true);
            vg.SynchViewDisplay(viewNum, true, true, true);
            docMdf.WriteMessageLineNow("成功 ViewGroup");

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