/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/ManagedFenceExample/FenceByElementTool.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.Cif.LinearGeometry;
using Bentley.MstnPlatformNET;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using eZBMCE.AddinManager;

namespace ManagedFenceExample
{

    class DgnElementSetToolTestZengfy : DgnElementSetTool
    {
        #region ---   Fields

        private readonly DocumentModifier _docMdf;
        private DPoint3d? _firstPt;


        #endregion

        #region ---   构造函数与命令启动

        public static void InstallNewInstance(DocumentModifier docMdf)
        {
            var fenceTool = new DgnElementSetToolTestZengfy(docMdf);
            fenceTool.InstallTool();
        }

        /// <summary> 构造函数  </summary>
        public DgnElementSetToolTestZengfy(DocumentModifier docMdf) : base()
        {
            _docMdf = docMdf;
            docMdf.WriteMessageLineNow($"构造函数");
        }

        #endregion

        #region ---   设置选项

        /// <summary> Called to ensure that the preferred ElemSource is satisfied.  </summary>
        ///<remarks>For example, if no active fence and ElemSource is USES_FENCE_Required false will be returned and the tool install is aborted.</remarks>
        /// <returns>false to disallow tool execution. </returns>
        protected override bool OnInstall()
        {
            _docMdf.WriteCurrentMethodName();
            return base.OnInstall();
        }

        protected override void OnPostInstall()
        {
            _docMdf.WriteCurrentMethodName();
            base.OnPostInstall();
        }

        protected override UsesSelection AllowSelection()
        {
          
            _docMdf.WriteCurrentMethodName();
            return UsesSelection.None;
        }

        protected override UsesDragSelect AllowDragSelect()
        {
            _docMdf.WriteCurrentMethodName();
            return UsesDragSelect.Box;
        }

        protected override bool NeedAcceptPoint()
        {
            return true;
        }

        #endregion

        #region ---   界面提示

        protected override void SetupAndPromptForNextAction()
        {
            _docMdf.WriteCurrentMethodName();
            base.SetupAndPromptForNextAction();
        }

        #endregion

        #region ---   鼠标动作

        protected override bool OnDataButton(DgnButtonEvent ev)
        {
            var hitPath = DoLocate(ev, newSearch: true, complexComponent: 1);
            if (_firstPt == null)
            {
                if (hitPath != null)
                {
                    var ele = hitPath.GetHeadElement();
                    _docMdf.WriteMessageLineNow($"选择的元素{ele.TypeName}");

                    if (ele is LineElement)
                    {
                        var line = ele as LineElement;
                        DPoint3d pt;
                        line.GetCurveVector().GetStartPoint(out pt);
                        _firstPt = pt;
                        BeginDynamics();
                    }
                }
            }
            else
            {
                var line = CreateLine(_firstPt.Value, ev.Point);
                line.AddToModel();
            }

            return true;
        }

        #endregion

        protected override void OnDynamicFrame(DgnButtonEvent ev)
        {
            var secPt = ev.Point;
            var dPoint3D = _firstPt;
            LineElement line = CreateLine(dPoint3D.Value, secPt);

            ElementPropertiesSetter eps = new ElementPropertiesSetter();
            eps.SetColor(4);
            eps.Apply(line);
            //
            var reDraw = new RedrawElems();
            reDraw.SetDynamicsViewsFromActiveViewSet(_docMdf.ActiveViewport);
            reDraw.DrawMode = DgnDrawMode.TempDraw;
            reDraw.DrawPurpose = DrawPurpose.Dynamics;
            reDraw.DoRedraw(line);
        }

        #region ---   命令开始与重启

        /// <summary> 此命令必须重载 </summary>
        protected override void OnRestartTool()
        {
            _docMdf.WriteCurrentMethodName();
            InstallNewInstance(_docMdf);
        }

        /// <summary> 此命令必须重载：Called for each element to allow tool to apply it's modification.  </summary>
        /// <returns>SUCCESS if modify was ok and element should be replaced, deleted, or copied added to the file. 
        /// ERROR to reject change or if change completely handled by tool. </returns>
        public override StatusInt OnElementModify(Element element)
        {
            _docMdf.WriteCurrentMethodName();
            return StatusInt.Success;
        }

        /// <summary> Called when a reset is entered. </summary>
        /// <remarks>If SOURCE_Pick and no elements have been identified, the Auto-Locate reset to find the next element is called.
        /// If an element is identified, reset will cycle to the next element, if there is no next element _OnReinitialize is called.
        /// For SOURCE_Fence and SOURCE_Selection set _ExitTool is called.</remarks>
        /// <returns>true if _ExitTool or OnReinitalize was called and tool object has been freed.</returns>
        protected override bool OnResetButton(DgnButtonEvent ev)
        {
            _docMdf.WriteCurrentMethodName();
            ExitTool();
            return true;
        }

        #endregion

        #region ---   模型元素操作

        public LineElement CreateLine(DPoint3d pt1, DPoint3d pt2)
        {
            DSegment3d s = new DSegment3d(pt1, pt2);
            var line = new LineElement(_docMdf.DgnModel, null, s);
            return line;
        }

        #endregion

    }
}
