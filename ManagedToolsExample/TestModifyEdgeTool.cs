///*--------------------------------------------------------------------------------------+
//|
//|     $Source: MstnExamples/Elements/ManagedToolsExample/TestModifyEdgeTool.cs $
//|
//|  $Copyright: (c) 2017 Bentley Systems, Incorporated. All rights reserved. $
//|
//+--------------------------------------------------------------------------------------*/

//using System.Collections.Generic;

//using Bentley.GeometryNET;
//using Bentley.DgnPlatformNET;
//using Bentley.DgnPlatformNET.Elements;
//using Bentley.MstnPlatformNET;

//using System.Windows.Forms;
//using Bentley.DgnPlatformNET.SmartFeature;
//namespace ManagedToolsExample
//{
//    /*=================================================================================**//**
//    * @bsiclass                                                               Bentley Systems
//+===============+===============+===============+===============+===============+======*/
//    class TestModifyEdgeTool : LocateSubEntityTool
//    {

//        private static TestModifyEdgeToolForm m_blendEdgeForm = null;

//        protected override bool CollectCurves()
//        {
//            return false;
//        }
//        protected override bool CollectSurfaces()
//        {
//            return false;
//        }


//        /*---------------------------------------------------------------------------------**//**
//        * @bsimethod                                                              Bentley Systems
//+---------------+---------------+---------------+---------------+---------------+------*/
//        protected override BentleyStatus OnProcessSolidPrimitive(ref SolidPrimitive geomPtr, DisplayPath path)
//        {
//            return BentleyStatus.Error;
//        }
//        protected override BentleyStatus OnProcessPolyface(ref PolyfaceHeader geomPtr, DisplayPath path)
//        {
//            return BentleyStatus.Success;
//        } // Don't convert a closed mesh to a BRep (and don't collect), can be expensive for large meshes...


//        /*---------------------------------------------------------------------------------**//**
//        * @bsimethod                                                              Bentley Systems
//+---------------+---------------+---------------+---------------+---------------+------*/
//        public override SubEntity.SubEntityType GetSubEntityTypeMask() { return SubEntity.SubEntityType.SubEntityType_Edge; }

//        /*---------------------------------------------------------------------------------**//**
//        * @bsimethod                                                              Bentley Systems
//+---------------+---------------+---------------+---------------+---------------+------*/
//        public override bool RequireSubEntitySupport() { return true; } // Require solid w/at least 1 edge...

//        /*---------------------------------------------------------------------------------**//**
//        * @bsimethod                                                              Bentley Systems
//+---------------+---------------+---------------+---------------+---------------+------*/
//        public override bool AcceptIdentifiesSubEntity() { return true; } // Solid accept point may also accept first edge...

//        /*---------------------------------------------------------------------------------**//**
//         * Return true if this element should be accepted for the modify operation.
//         * @bsimethod                                                              Bentley Systems
//         +---------------+---------------+---------------+---------------+---------------+------*/
//        protected override bool IsElementValidForOperation(Element element, HitPath path, out string cantAcceptReason)
//        {
//            // Base class implementation returns true if geometry cache isn't empty, which in this case means the cache contains at least 1 BRep solid.
//            // To be valid for modification element should be fully represented by a single solid; reject if there are multiple solid bodies or missing geometry.
//            // NOTE: Simple count test is sufficient (w/o also checking TryGetAsBRep) as override of _Collect and _OnProcess methods have tool only collecting BRep solids.
//            return (base.IsElementValidForOperation(element, path, out cantAcceptReason) && 1 == GetElementGraphicsCacheCount(element) && !IsGeometryMissing(element));
//        }


//        /*---------------------------------------------------------------------------------**//**
//        * Constructor.
//        * @bsimethod                                                              Bentley Systems
///*--------------+---------------+---------------+---------------+---------------+------*/
//        public TestModifyEdgeTool() : base()
//        {
//            //Load options form.
//            if (null == m_blendEdgeForm)
//            {
//                m_blendEdgeForm = new TestModifyEdgeToolForm();
//                m_blendEdgeForm.Show();
//            }
//        }
//        /*---------------------------------------------------------------------------------**//**
//        * Constructor. added to support different smart feature operations in future
//        * @bsimethod                                                              Bentley Systems
///*--------------+---------------+---------------+---------------+---------------+------*/
//        public TestModifyEdgeTool(int toolId, int promptId) : base(toolId, promptId)
//        {
//            //Load options form.
//            if (null == m_blendEdgeForm)
//            {
//                m_blendEdgeForm = new TestModifyEdgeToolForm();
//                m_blendEdgeForm.Show();
//            }
//        }

//        /*---------------------------------------------------------------------------------**//**
//        * Restart tool.
//        * @bsimethod                                                              Bentley Systems
///*--------------+---------------+---------------+---------------+---------------+------*/
//        protected override void OnRestartTool()
//        {
//            InstallNewInstance(this.ToolId);
//        }

//        /*---------------------------------------------------------------------------------**//**
//        * Exit tool.
//        * @bsimethod                                                              Bentley Systems
///*--------------+---------------+---------------+---------------+---------------+------*/
//        protected override void ExitTool()
//        {
//            if (null != m_blendEdgeForm)
//            {
//                m_blendEdgeForm.Close();
//                m_blendEdgeForm = null;
//            }
//            base.ExitTool();
//        }

//        /*---------------------------------------------------------------------------------**//**
//        * Enable snapping.
//        * @bsimethod                                                              Bentley Systems
///*--------------+---------------+---------------+---------------+---------------+------*/
//        public override void OnPostInstall()
//        {
//            base.OnPostInstall();
//        }



//        /*---------------------------------------------------------------------------------**//**
//        * Restart the tool on reset.
//        * @bsimethod                                                              Bentley Systems
///*--------------+---------------+---------------+---------------+---------------+------*/
//        public override bool OnResetButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
//        {
//            ExitTool();
//            return true;
//        }

//        /*---------------------------------------------------------------------------------**//**
//        * Overriding some _OnElementModifyMethod  to create Smart Feature blend Node. 
//        * It performs the blend operation on selected edges and saves the data into DGN file
//        * @bsimethod                                                              Bentley Systems
//+---------------+---------------+---------------+---------------+---------------+------*/
//        public override StatusInt OnElementModify(Element eeh)
//        {
//            SmartFeatureNode featureNode = GetSmartFeatureNode(eeh);
//            Element newEeh = null;
//            Bentley.DgnPlatformNET.StatusInt status = SmartFeatureElement.AppendAndWriteSmartFeatureChild(ref newEeh, eeh, GetDestinationModelRef(), eeh, featureNode);
//            return status;
//        }

//        /*---------------------------------------------------------------------------------**//**
//        * It returns blend feature node
//        * @bsimethod                                                              Bentley Systems
//+---------------+---------------+---------------+---------------+---------------+------*/
//        private SmartFeatureNode GetSmartFeatureNode(Element eeh)
//        {

//            // Get selected edges by user
//            List<SubEntity> edgeList = new List<SubEntity>();
//            SubEntity[] edges = edgeList.ToArray();
//            GetAcceptedSubEntities(ref edges);

//            // Launch dialogue and take value   
//            double radius = TestModifyEdgeToolForm.GetBlendRadius();

//            // Convert user unit into master unit
//            DgnModelRef modelRef = GetDestinationModelRef();
//            DgnModel model = modelRef.GetDgnModel();
//            radius = model.GetModelInfo().UorPerMeter * radius;

//            bool isSmoothEdges = TestModifyEdgeToolForm.GetTangencyOption();
//            SmartFeatureNode featureNode = FeatureCreate.CreateBlendFeature(edges, radius, isSmoothEdges);
//            return featureNode;
//        }

//        /*---------------------------------------------------------------------------------**//**
//        * Static method to initialize this tool from any class.
//        * @bsimethod                                                              Bentley Systems
///*--------------+---------------+---------------+---------------+---------------+------*/
//        public static void InstallNewInstance(int toolId)
//        {
//            TestModifyEdgeTool testmodEdgeTool = new TestModifyEdgeTool(toolId, 0);
//            testmodEdgeTool.InstallTool();
//        }
//    }
//}