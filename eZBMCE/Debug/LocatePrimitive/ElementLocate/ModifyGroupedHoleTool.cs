/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/ManagedToolsExample/ModifyGroupedHoleTool.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using System.Collections.Generic;
using eZBMCE.AddinManager;

namespace ManagedToolsExample
{

    class ModifyGroupedHoleTool : DgnElementSetTool
    {
        private DocumentModifier _docMdf;

        private ModifyGroupedHoleTool(DocumentModifier docMdf) : base()
        {
            _docMdf = docMdf;
            _docMdf.WriteMessageLineNow(@"构造函数");
        }

        #region ---   选项

        protected virtual UsesDragSelect _AllowDragSelect()
        {
            return UsesDragSelect.Box;
        }

        protected virtual bool _DoGroups()
        {
            return true;
        }
       

        #endregion

        protected override bool OnDataButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
        {
            _docMdf.WriteMessageLineNow(@"OnDataButton");
            //Locate an element.
            Bentley.DgnPlatformNET.HitPath hitPath = DoLocate(ev, true, 1);

            //If an element is located and the element is a grouped hole.
            //If element has a solid fill, remove it.
            //Else add a solid fill.
            if (null != hitPath)
            {
                int numHoles = PlaceGroupedHoleForm.GetNumberOfHoles();
                Bentley.DgnPlatformNET.Elements.Element element = hitPath.GetHeadElement();
                _docMdf.WriteMessageLineNow("选择的对象为：" + element.TypeName);

                if (element is GroupedHoleElement)
                {
                    GroupedHoleElement groupedHoleElement = element as GroupedHoleElement;

                    uint fillColor;
                    bool alwaysFilled;
                    if (groupedHoleElement.GetSolidFill(out fillColor, out alwaysFilled))
                    {
                        groupedHoleElement.RemoveAreaFill();
                    }
                    else
                    {
                        groupedHoleElement.AddSolidFill((uint)Bentley.MstnPlatformNET.Settings.GetActiveFillColor().Index, false);
                    }

                    //Element must be replaced in model.
                    groupedHoleElement.ReplaceInModel(groupedHoleElement);
                }
            }

            return true;
        }


        public override Bentley.DgnPlatformNET.StatusInt OnElementModify(Bentley.DgnPlatformNET.Elements.Element element)
        {
          
            _docMdf.WriteMessageLineNow(@"OnElementModify");
            return Bentley.DgnPlatformNET.StatusInt.Error;
        }

        /*---------------------------------------------------------------------------------**//**
        * Restart tool
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override void OnRestartTool()
        {
            _docMdf.WriteMessageLineNow(@"OnRestartTool");
            InstallNewInstance(_docMdf);
        }

        protected override bool OnResetButton(DgnButtonEvent ev)
        {
            _docMdf.WriteMessageLineNow(@"OnResetButton");
            // ExitTool();
            ExitTool();
            return true;
        }

        protected override void OnCleanup()
        {
            _docMdf.WriteMessageLineNow(@"OnCleanup");
            base.OnCleanup();
            _docMdf.WriteMessageLineNow(@"base.OnCleanup");
        }

        protected override void ExitTool()
        {
            _docMdf.WriteMessageLineNow(@"ExitTool");
            base.ExitTool();
        }


        /*---------------------------------------------------------------------------------**//**
        * Static method to initialize this tool from any class.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static void InstallNewInstance(DocumentModifier docMdf)
        {
            docMdf.WriteMessageLineNow(@"InstallNewInstance");
            ModifyGroupedHoleTool modifyTool = new ModifyGroupedHoleTool(docMdf);
            var res = modifyTool.AllowDragSelect();
            var a = modifyTool.AllowSelection();
            modifyTool.InstallTool();
        }
    }
}
