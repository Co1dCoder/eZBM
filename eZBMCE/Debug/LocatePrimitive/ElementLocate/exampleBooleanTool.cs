using System;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using System.Collections.Generic;
using Bentley.GeometryNET.Basic;
using eZBMCE.AddinManager;

public class TestBooleanTool : ElementGraphicsTool
{
    enum ToolOperation
    {
        OP_BooleanUnion = 0,
        OP_BooleanDifference = 1,
        OP_BooleanIntersection = 2,
        OP_SewSheets = 3,
    };


    ToolOperation m_operation;

    /*---------------------------------------------------------------------------------**//**
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
    TestBooleanTool(int cmdName, ToolOperation operation)
    {
        //SetCmdName(cmdName, 0);
        m_operation = operation;
    }

    /*---------------------------------------------------------------------------------**//**
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
    protected virtual bool _IsModifyOriginal() { return true; } // This is a modify tool, the original elements are consumed/deleted...
    protected virtual bool _DoGroups() { return false; } // Don't automatically include members of named and graphic groups...
    protected virtual bool _WantDynamics() { return false; }
    // protected virtual bool NeedAcceptPoint() { return SOURCE_Pick == _GetElemSource(); }
    protected virtual bool _WantAccuSnap() { return false; }
    protected virtual UsesDragSelect _AllowDragSelect() { return UsesDragSelect.Box; }

    /*---------------------------------------------------------------------------------**//**
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
    protected virtual bool _CollectCurves() { return false; } // Tool does not support wire bodies...wire bodies won't be collected.
    protected virtual bool _CollectSurfaces() { return ToolOperation.OP_SewSheets == m_operation; } // Tool supports and collects sheet bodies for sew operation...
    protected virtual bool _CollectSolids() { return ToolOperation.OP_SewSheets != m_operation; } // Tool supports and collects solid bodies when not sew operation...

    /*---------------------------------------------------------------------------------**//**
                                                                                          * @bsimethod                                                              Bentley Systems
                                                                                          +---------------+---------------+---------------+---------------+---------------+------*/
    protected virtual BentleyStatus _OnProcessSolidPrimitive(SolidPrimitive geomPtr, DisplayPath path) { return BentleyStatus.Error; } // Promote capped surface to solid body and un-capped surface to sheet body...
    protected virtual BentleyStatus _OnProcessBsplineSurface(BSplineSurfaceElement geomPtr, DisplayPath path) { return BentleyStatus.Error; } // Promote surface to sheet body...
    protected virtual BentleyStatus _OnProcessCurveVector(CurveVector geomPtr, DisplayPath path) { return BentleyStatus.Error; } // Promote region to sheet body...
    protected virtual BentleyStatus _OnProcessPolyface(PolyfaceHeader geomPtr, DisplayPath path) { return BentleyStatus.Success; } // Don't convert a closed mesh to a BRep (and don't collect), can be expensive for large meshes...

    /*---------------------------------------------------------------------------------**//**
                                                                                          * Require a minumum of two elements, if control is down select additional elements...
                                                                                          * @bsimethod                                                              Bentley Systems
                                                                                          +---------------+---------------+---------------+---------------+---------------+------*/
    protected virtual int _GetAdditionalLocateNumRequired() { return 2; }
    protected virtual bool _WantAdditionalLocate(DgnButtonEvent ev) { return WantAdditionalLocateHelper(ev); }
    protected virtual bool _OnModifierKeyTransition(bool wentDown, int key) { return OnModifierKeyTransitionHelper(wentDown, key); }

    /*---------------------------------------------------------------------------------**//**
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
    UInt32 GetPromptMsgIdForCurrentToolState()
    {
        if (SOURCE_Pick != _GetElemSource())
            return PROMPT_ExampleAcceptSelection;

        switch (GetElementAgenda().GetCount())
        {
            case 0:
                return PROMPT_ExampleIdentifyElement;

            case 1:
                return PROMPT_ExampleAcceptRejectIdentifyNext;

            default:
                return PROMPT_ExampleAcceptRejectIdentifyAdditional;
        }
    }

    /*---------------------------------------------------------------------------------**//**
                                                                                          * All changes to auto-locate/accusnap state and user prompts are done here!!!
                                                                                          * @bsimethod                                                              Bentley Systems
                                                                                          +---------------+---------------+---------------+---------------+---------------+------*/
    protected virtual void _SetupAndPromptForNextAction()
    {

        var prompt;

        RmgrResource::LoadWString(prompt, 0, STRINGLISTID_Prompts, GetPromptMsgIdForCurrentToolState());
        NotificationManager::OutputPrompt(prompt.c_str());


        SetupAndPromptForNextActionHelper(); // Enable/Disable Auto-Locate based on _GetAdditionalLocateNumRequired.
    }

    /*---------------------------------------------------------------------------------**//**
                                                                                          * Called for each element in tool's ElementAgenda in order to perform modification.
                                                                                          * @bsimethod                                                              Bentley Systems
                                                                                          +---------------+---------------+---------------+---------------+---------------+------*/
    protected virtual StatusInt _OnElementModify(Element eeh)
    {
        eeh.Invalidate();

        return StatusInt.Success; // Returning SUCCESS with an invalid EditElementHandle means delete the original element from the model...
    }

    /*---------------------------------------------------------------------------------**//**
																						  * Return true if this element has the required geometry and should be accepted.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
    protected virtual bool _IsElementValidForOperation(Element eh, HitPath path, string cantAcceptReason)
    {
        // If already verfied by _OnPostLocate there's no reason to re-check for _FilterAgendaEntries.
        if (null == path && ModifyElementSource.Selected == ElementAgenda.Source)
        { return true; }
        
        // Base class implementation returns true if geometry cache isn't empty (assumes tool collects only supported geometry types, in this case BReps).
        if (!base.IsElementValidForOperation(eh, path, out cantAcceptReason))
            return false;

        // To be valid for modification element should be fully represented by a single solid (or surface for sew command).
        // NOTE: The overridden _Collect and _OnProcess methods have tool only collecting the required geometry, so a simple count check will suffice.
        return (1 == GetElementGraphicsCacheCount(eh) && !IsGeometryMissing(eh));
    }

    /*---------------------------------------------------------------------------------**//**
																						  * Perform the solid operation on target/tool entities.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
    StatusInt DoOperation(SolidKernelEntity target, bvector<SolidKernelEntity> tool)
    {
        switch (m_operation)
        {
            case ToolOperation.OP_BooleanUnion:
                {
                    return SolidUtil.Modify.BooleanUnion(target, tool.front(), tool.size());
                }

            case ToolOperation.OP_BooleanDifference:
                {
                    return SolidUtil.Modify.BooleanSubtract(target, &tool.front(), tool.size());
                }

            case ToolOperation.OP_BooleanIntersection:
                {
                    return SolidUtil.Modify.BooleanIntersect(target, &tool.front(), tool.size());
                }

            case ToolOperation.OP_SewSheets:
                {

                    bvector<SolidKernelEntity> sewn;
                    bvector<SolidKernelEntity> unsewn;

                    tool.push_back(target); // Sew operation works on a single list of tool bodies...

                    if (StatusInt.Success != SolidUtil.Modify.SewBodies(sewn, unsewn, &tool.front(), tool.size(), 1.0e-3) || 1 != sewn.size() || 0 != unsewn.size())
                        return StatusInt.Error; // For the purpose of this example a result that isn't a single body or doesn't include all tools is considered a failure.

                    target = sewn.front();

                    return StatusInt.Success;
                }

            default:
                return StatusInt.Error;
        }
    }

    /*---------------------------------------------------------------------------------**//**
                                                                                          * Perform the solid operation on the collected geometry. Produce a new smart solid/surface
                                                                                          * element from the result. Upon successful create/add of a new smart solid/surface,
                                                                                          * call __super to call _OnElementModify on original elements, which then request they be deleted.
                                                                                          * @bsimethod                                                              Bentley Systems
                                                                                          +---------------+---------------+---------------+---------------+---------------+------*/
    protected virtual StatusInt _ProcessAgenda(DgnButtonEvent ev)
    {
        SolidKernelEntity target;
        bvector<SolidKernelEntity> tool;

        var first = ElementAgenda.GetFirst();
        ElementHandleCP end = first + GetElementAgenda().GetCount();

        for (ElementHandleCP curr = first; curr < end; curr++)
        {
            bvector<IElementGraphicsPtr> geomCache;

            if (StatusInt.Success != GetElementGraphicsCache(*curr, geomCache))
                continue;

            foreach (IElementGraphicsPtr geomPtr in geomCache)
            {
                SolidKernelEntity entityPtr = TryGetAsBRep(geomPtr);

                if (!entityPtr.IsValid())
                    continue;

                if (target.IsValid())
                    tool.push_back(entityPtr);
                else
                    target = entityPtr;
            }
        }

        if (StatusInt.Success != DoOperation(target, tool))
            return StatusInt.Error;

        Element newEeh;

        if (StatusInt.Success != SolidUtil.Convert.BodyToElement(newEeh, *target, first, *first->GetModelRef()))
            return StatusInt.Error;

        if (StatusInt.Success != newEeh.AddToModel())
            return StatusInt.Error;

        return base.ProcessAgenda(ev); // Delete originals...
    }

    /*---------------------------------------------------------------------------------**//**
																						  * Install a new instance of the tool. Will be called in response to external events
																						  * such as undo or by the base class from _OnReinitialize when the tool needs to be
																						  * reset to it's initial state.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
    protected virtual void _OnRestartTool()
    {
        InstallNewInstance(GetToolId(), m_operation);
    }



    /*---------------------------------------------------------------------------------**//**
																						  * Method to create and install a new instance of the tool. If InstallTool returns ERROR,
																						  * the new tool instance will be freed/invalid. Never call delete on RefCounted classes.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
    public static void InstallNewInstance(int toolId, ToolOperation operation)
    {
        TestBooleanTool tool = new TestBooleanTool(toolId, operation);

        tool.InstallTool();
    }

    // TestBooleanTool

    public void startExampleUnionElementTool(string unparsed)
    {
        // NOTE: Call the method to create/install the tool, RefCounted classes don't have public constructors...
        TestBooleanTool.InstallNewInstance(CMDNAME_ExampleUnionTool, ToolOperation.OP_BooleanUnion);
    }

    /*---------------------------------------------------------------------------------**//**
                                                                                          * @bsimethod                                                              Bentley Systems
                                                                                          +---------------+---------------+---------------+---------------+---------------+------*/
    public void startExampleDifferenceElementTool(string unparsed)
    {
        // NOTE: Call the method to create/install the tool, RefCounted classes don't have public constructors...
        TestBooleanTool.InstallNewInstance(CMDNAME_ExampleDifferenceTool, ToolOperation.OP_BooleanDifference);
    }

    /*---------------------------------------------------------------------------------**//**
                                                                                          * @bsimethod                                                              Bentley Systems
                                                                                          +---------------+---------------+---------------+---------------+---------------+------*/
    public void startExampleIntersectionElementTool(string unparsed)
    {
        // NOTE: Call the method to create/install the tool, RefCounted classes don't have public constructors...
        TestBooleanTool.InstallNewInstance(CMDNAME_ExampleIntersectionTool, ToolOperation.OP_BooleanIntersection);
    }

    /*---------------------------------------------------------------------------------**//**
                                                                                          * @bsimethod                                                              Bentley Systems
                                                                                          +---------------+---------------+---------------+---------------+---------------+------*/
    public void startExampleSewElementTool(string unparsed)
    {
        // NOTE: Call the method to create/install the tool, RefCounted classes don't have public constructors...
        TestBooleanTool.InstallNewInstance(CMDNAME_ExampleSewTool, ToolOperation.OP_SewSheets);
    }
}