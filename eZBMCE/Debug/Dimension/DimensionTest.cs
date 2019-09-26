using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.DgnPlatformNET.Elements;
using Bentley.EC.Persistence.Query;
using Bentley.ECObjects;
using Bentley.ECObjects.Instance;
using Bentley.ECObjects.Json;
using Bentley.ECObjects.Schema;
using Bentley.GeometryNET;
using eZBMCE.AddinManager;
using eZBMCE.Debug.EcObject;
using eZBMCE.LocatePrimitive;
using eZBMCE.Utilities;
using ManagedToolsExample;
using Application = Bentley.Interop.MicroStationDGN.Application;
using BM = Bentley.MstnPlatformNET;
using BMI = Bentley.MstnPlatformNET.InteropServices;
using Element = Bentley.DgnPlatformNET.Elements.Element;


namespace eZBMCE.Debug
{
    /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
    [EcDescription(CommandDescription)]
    [Bentley.MstnPlatformNET.AddIn(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    public class DimensionTest : IBMExCommand
    {

        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"TestDimension";

        private const string CommandDescription = @"快速调试";

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void TestDimension()
        {
            AddinManagerDebuger.ExecuteCommand(TestDimension);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new DimensionTest();
            return AddinManagerDebuger.DebugInAddinManager(s.TestDimension, ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
        public AddinManagerDebuger.ExternalCmdResult TestDimension(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            // Add your code here

            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        #region --- 界面操作
        public class CreateDimensionCallbacks : DimensionCreateData
        {
            private DimensionStyle m_dimStyle;
            private DgnTextStyle m_textStyle;
            private Symbology m_symbology;
            private LevelId m_levelId;
            private DirectionFormatter m_directionFormatter;

            public CreateDimensionCallbacks(DimensionStyle dimStyle, DgnTextStyle textStyle, Symbology symb, LevelId levelId, DirectionFormatter formatter)
            {
                m_dimStyle = dimStyle;
                m_textStyle = textStyle;
                m_symbology = symb;
                m_levelId = levelId;
                m_directionFormatter = formatter;
            }
            public override DimensionStyle GetDimensionStyle()
            {
                return m_dimStyle;
            }
            public override DgnTextStyle GetTextStyle()
            {
                return m_textStyle;
            }
            public override Symbology GetSymbology()
            {
                return m_symbology;
            }
            public override LevelId GetLevelId()
            {
                return m_levelId;
            }
            public override int GetViewNumber()
            {
                return 0;
            }
            public override DMatrix3d GetDimensionRotation()
            {
                return DMatrix3d.Identity;
            }
            public override DMatrix3d GetViewRotation()
            {
                return DMatrix3d.Identity;
            }
            public override DirectionFormatter GetDirectionFormatter()
            {
                return m_directionFormatter;
            }
        }

        class CreateElement
        {
            public static void LinearAndAngularDimension(string unparsed)
            {
                DgnFile oFile = BM.Session.Instance.GetActiveDgnFile();
                DgnModel oModel = BM.Session.Instance.GetActiveDgnModel();
                double uorPerMast = oModel.GetModelInfo().UorPerMaster;

                DimensionStyle dimStyle = new DimensionStyle("DimStyle", oFile);
                dimStyle.SetBooleanProp(true, DimStyleProp.Placement_UseStyleAnnotationScale_BOOLINT);
                dimStyle.SetDoubleProp(1, DimStyleProp.Placement_AnnotationScale_DOUBLE);
                dimStyle.SetBooleanProp(true, DimStyleProp.Text_OverrideHeight_BOOLINT);
                dimStyle.SetDistanceProp(0.5 * uorPerMast, DimStyleProp.Text_Height_DISTANCE, oModel);
                dimStyle.SetBooleanProp(true, DimStyleProp.Text_OverrideWidth_BOOLINT);
                dimStyle.SetDistanceProp(0.4 * uorPerMast, DimStyleProp.Text_Width_DISTANCE, oModel);
                dimStyle.SetBooleanProp(true, DimStyleProp.General_UseMinLeader_BOOLINT);
                dimStyle.SetDoubleProp(0.01, DimStyleProp.Terminator_MinLeader_DOUBLE);
                dimStyle.SetBooleanProp(true, DimStyleProp.Value_AngleMeasure_BOOLINT);
                dimStyle.SetAccuracyProp((byte)AnglePrecision.Use1Place, DimStyleProp.Value_AnglePrecision_INTEGER);
                dimStyle.Add(oFile);

                DgnTextStyle textStyle = new DgnTextStyle("TestStyle", oFile);
                LevelId lvlId = BM.Settings.GetLevelIdFromName("Default");
                CreateDimensionCallbacks callbacks = new CreateDimensionCallbacks(dimStyle, textStyle, new Symbology(), lvlId, null);
                DimensionElement oDim = new DimensionElement(oModel, callbacks, DimensionType.SizeArrow);
                if (oDim.IsValid)
                {
                    DPoint3d pt = new DPoint3d(0, -17 * uorPerMast, 0);

                    AssociativePoint associativePoint = new AssociativePoint();
                    associativePoint = null;

                    oDim.InsertPoint(pt, associativePoint, dimStyle, -1);
                    pt.X += 3 * uorPerMast;
                    oDim.InsertPoint(pt, null, dimStyle, -1);
                    oDim.SetHeight(uorPerMast);
                    oDim.AddToModel();
                }
                oDim = new DimensionElement(oModel, callbacks, DimensionType.AngleSize);
                if (oDim.IsValid)
                {
                    DPoint3d pt = new DPoint3d(7 * uorPerMast, -13 * uorPerMast, 0);
                    oDim.InsertPoint(pt, null, dimStyle, -1);
                    pt = DPoint3d.FromXY(5 * uorPerMast, -15 * uorPerMast);
                    oDim.InsertPoint(pt, null, dimStyle, -1);
                    pt = DPoint3d.FromXY(9 * uorPerMast, -15 * uorPerMast);
                    oDim.InsertPoint(pt, null, dimStyle, -1);
                    oDim.SetHeight(uorPerMast);
                    oDim.AddToModel();
                }
            }
        }
        #endregion
    }
}