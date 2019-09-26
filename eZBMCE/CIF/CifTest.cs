using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Bentley.CifNET;
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
using Bentley.CifNET.CadSystem;
using Bentley.CifNET.Objects.Presentation;

namespace eZBMCE.Cif
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

        private const string CommandDescription = @"根据两个圆创建出卵形曲线";

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

        /// <summary> 在 mdl load 中进行调用和调试，而不用 AddinManager 进行调试 </summary>
        public static void Test_Cif()
        {
            var cifTest = new CifTest();
            using (var docMdf = new DocumentModifier(true))
            {
                var ret = cifTest.CifFastDebug(docMdf);
            }
        }

        #endregion

        private DocumentModifier _docMdf;

        private IObjectSpaceManager m_objectSpaceManager;
        /// <summary> 遍历廊道 </summary>
        public AddinManagerDebuger.ExternalCmdResult CifFastDebug(DocumentModifier docMdf)
        {
            _docMdf = docMdf;

            SuperElevation se;
            SuperElevationEdit sed;

            // 监听 ORD 的元素改变事件
            Bentley.CifNET.CadSystem.IObjectSpaceManager m_objectSpaceManager =
                Bentley.CifNET.ServiceManager.Instance.GetService<IObjectSpaceManager>();
            m_objectSpaceManager.ObjectSpace.ObjectChanged += ObjectSpaceOnObjectChanged;

            // 获取控制视口的 IViewManager
            Bentley.CifNET.Objects.Presentation.IViewManager m_viewManager =
                Bentley.CifNET.ServiceManager.Instance.GetService<IViewManager>();


            return AddinManagerDebuger.ExternalCmdResult.Commit;

            var selection = Utils.GetSelection();
            Element ele = selection.Length == 0 ? null : selection.First();

            // 创建缓和曲线
            Bentley.CifNET.SDK.Edit.ConsensusConnectionEdit conEdit = ConsensusConnectionEdit.GetActive();
            GeometricModel geoModel = conEdit.GetActiveGeometricModel();

            BSplineCurveElement sp1 = CreateClothoid1(geoModel.DgnModel);
            sp1.AddToModel();
            // return AddinManagerDebuger.ExternalCmdResult.Commit;

            conEdit.StartTransientMode();
            // 在2D模型中提取一条Alignment
            var alignId = 4029L;
            var alignToModify = AlignmentEdit.CreateFromElement(conEdit, geoModel.DgnModel.FindElementById(new ElementId(ref alignId)));

            var line1 = new Line(new DPoint3d(0, 0, 0), new DPoint3d(10, 10, 0));
            var line2 = new Line(new DPoint3d(0, 0, 0), new DPoint3d(10, 10, 0));

            // 在模型中画出新几何，用来对比
            var align2D1 = AlignmentEdit.CreateByLinearElement(conEdit, line1, createRuledAlignment: true);

            BSpline bs = BSpline.CreateBSplineWithEndConditions(
                points: null,
                splineParameters: new BSplineParameters(startTangent: 0, endTangent: 1));

            var cv = new CurveVector(CurveVector.BoundaryType.Open);
            MSBsplineCurve sp2 = null;
            cv.Add(sp1.GetCurveVector().First());

            LinearElement le = LinearElement.CreateFromCurveVector(cv, false);
            // 修改既有Alignment的几何
            alignToModify.LinearGeometry = line2;

            conEdit.PersistTransients();
            // ms 信息提取
            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        private void ObjectSpaceOnObjectChanged(object sender, ObjectChangeEventArgs e)
        {
            MessageBox.Show("进入 ObjectSpaceOnObjectChanged");
            var info = e.ObjectChangeInfo;
            _docMdf.WriteMessageLineNow(info.Action, info.CLRObject,
                info.IsOnApplicationData, info.IsOnRelationship,
                info.ObjectKey,
                info.ObjectType,
                info.TransactionNumber,
                info.RelationshipChangeInfo);
            m_objectSpaceManager.ObjectSpace.ObjectChanged -= ObjectSpaceOnObjectChanged;
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

        private BSplineCurveElement CreateClothoid1(DgnModel model)
        {
            var uorPerMaster = model.GetModelInfo().UorPerMaster;
            DPoint3d[] controlPoints = new DPoint3d[]
            {
                new DPoint3d(0,0,0)*uorPerMaster,
                new DPoint3d(1,0,0)*uorPerMaster,
                new DPoint3d(1,2,0)*uorPerMaster,
                new DPoint3d(3,7,0)*uorPerMaster,
                new DPoint3d(4,8,0)*uorPerMaster,
            };
            DVector3d startTang = new DVector3d(1, 0, 0);
            DVector3d endTang = new DVector3d(1, 2, 0);

            // MSBsplineCurve sp = MSBsplineCurve.CreateFromPoints(points);  // 点直接相连，中间线性连接，即对应Order = 2
            MSBsplineCurve sp = MSBsplineCurve.CreateFromInterpolatePoints(
                points: controlPoints,  // 曲线实际经过的控制点（不是pole）
                parametrization: 1,  // 
                sTangent: ref startTang, eTangent: ref endTang, // 缓和曲线的起点切向与终点切向
                endControl: true, keepTanMag: false, // 这两个值组合使用，才能使 sTangent 与 eTangent 生效
                order: 4 // 曲线阶数，一般取 3 或 4，取4可以使曲线经过 controlPoints
                );
            //_docMdf.WriteLinesIntoDebuger(startTang, endTang);
            //MSInterpolationCurve spInterp  = MSInterpolationCurve.CreateFromPointsAndEndTangents();
            BSplineCurveElement spEle = new BSplineCurveElement(model, null, sp);
            ReadBSplineCurveElement(model, spEle);

            return spEle;
        }

        private void ReadBSplineCurveElement(DgnModel model, BSplineCurveElement bSpline)
        {
            _docMdf.WriteLineIntoDebuger("读取 BSplineCurveElement 信息");
            CurvePrimitive c1 = bSpline.GetCurveVector().First();
            MSBsplineCurve spCurve = c1.GetBsplineCurve();

            _docMdf.WriteLineIntoDebuger("Knots count = ", spCurve.KnotCount);
            var f1 = spCurve.KnotToFraction(0.3);
            _docMdf.WriteLineIntoDebuger("spCurve.Fraction 0.3= ", f1);
            foreach (var VARIABLE in spCurve.Knots)
            {
                _docMdf.WriteLineIntoDebuger(VARIABLE);
            }
            _docMdf.WriteLineIntoDebuger("Weights");
            DPoint3d pt;
            DVector3d dir;
            for (double i = 0; i <= 1.0; i += 0.2)
            {
                spCurve.FractionToPoint(out pt, out dir, i);
                _docMdf.WriteLineIntoDebuger("fraction", i, pt, dir);
            }

            _docMdf.WriteLineIntoDebuger("Poles.Count = ", spCurve.PoleCount);
            CreateLineString(model, spCurve.Poles);
        }
        
        private CurvePrimitive CreateClothoid2()
        {
            CurvePrimitive curve = CurvePrimitive.CreateSpiralBearingRadiusBearingRadius(
                transitionType: 10,
                startRadians: 0.5,
                startRadius: 50,
                endRadians: 0.5,
                endRadius: 100,
                frame: DTransform3d.Identity,
                fractionA: 0,
                fractionB: 1
                );
            return curve;
        }

        private void CreateLineString(DgnModel model, IEnumerable<DPoint3d> points)
        {
            LineStringElement ls = new LineStringElement(model, null, points.ToArray());
            ls.AddToModel();
        }

    }
}