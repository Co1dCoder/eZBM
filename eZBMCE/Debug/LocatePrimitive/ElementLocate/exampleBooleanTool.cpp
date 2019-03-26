/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/exampleSolids/exampleBooleanTool.cpp $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
#include "exampleSolids.h"

USING_NAMESPACE_BENTLEY_DGNPLATFORM;
USING_NAMESPACE_BENTLEY_MSTNPLATFORM;
USING_NAMESPACE_BENTLEY_MSTNPLATFORM_ELEMENT;

/*=================================================================================**//**
																					  * Example showing how to use ElementGraphicsTool to write a tool for doing boolean
																					  * operations between elements using the solids kernel api.
																					  *
																					  * The base class provides a mechanism for caching geometry collected from elements.
																					  * Extracting or converting element geometry is potentially an expensive operation; this
																					  * is particularily true for breps. The geometry cache allows for efficient query operations
																					  * to be done in the tool's _OnPostLocate method.
																					  *
																					  * This tool populates an ElementAgenda of elements that can be represented as surface or
																					  * solid breps from a pick or selection set. When picking, the tool completes when a
																					  * minimum of 2 elements have been selected, control can be held to pick additional elements.
																					  * The tool also supports post-selection using drag select.
																					  *
																					  * The _OnElementModify method is used to delete the original elements after a successful
																					  * boolean result and creation of a new smart solid or surface element.
																					  *
																					  * @bsiclass                                                               Bentley Systems
																					  +===============+===============+===============+===============+===============+======*/
struct          TestBooleanTool : ElementGraphicsTool
{
	enum ToolOperation
	{
		OP_BooleanUnion = 0,
		OP_BooleanDifference = 1,
		OP_BooleanIntersection = 2,
		OP_SewSheets = 3,
	};

protected:

	ToolOperation   m_operation;

	/*---------------------------------------------------------------------------------**//**
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
	TestBooleanTool(int cmdName, ToolOperation operation)
	{
		SetCmdName(cmdName, 0);
		m_operation = operation;
	}

	/*---------------------------------------------------------------------------------**//**
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
	virtual bool _IsModifyOriginal() override { return true; } // This is a modify tool, the original elements are consumed/deleted...
	virtual bool _DoGroups() override { return false; } // Don't automatically include members of named and graphic groups...
	virtual bool _WantDynamics() override { return false; }
	virtual bool _NeedAcceptPoint() override { return SOURCE_Pick == _GetElemSource(); }
	virtual bool _WantAccuSnap() override { return false; }
	virtual UsesDragSelect _AllowDragSelect() override { return USES_DRAGSELECT_Box; }

	/*---------------------------------------------------------------------------------**//**
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
	virtual bool _CollectCurves() override { return false; } // Tool does not support wire bodies...wire bodies won't be collected.
	virtual bool _CollectSurfaces() override { return OP_SewSheets == m_operation; } // Tool supports and collects sheet bodies for sew operation...
	virtual bool _CollectSolids() override { return OP_SewSheets != m_operation; } // Tool supports and collects solid bodies when not sew operation...

																				   /*---------------------------------------------------------------------------------**//**
																																										 * @bsimethod                                                              Bentley Systems
																																										 +---------------+---------------+---------------+---------------+---------------+------*/
	virtual BentleyStatus _OnProcessSolidPrimitive(ISolidPrimitivePtr& geomPtr, DisplayPathCR path) override { return ERROR; } // Promote capped surface to solid body and un-capped surface to sheet body...
	virtual BentleyStatus _OnProcessBsplineSurface(MSBsplineSurfacePtr& geomPtr, DisplayPathCR path) override { return ERROR; } // Promote surface to sheet body...
	virtual BentleyStatus _OnProcessCurveVector(CurveVectorPtr& geomPtr, DisplayPathCR path) override { return ERROR; } // Promote region to sheet body...
	virtual BentleyStatus _OnProcessPolyface(PolyfaceHeaderPtr& geomPtr, DisplayPathCR path) override { return SUCCESS; } // Don't convert a closed mesh to a BRep (and don't collect), can be expensive for large meshes...

																														  /*---------------------------------------------------------------------------------**//**
																																																				* Require a minumum of two elements, if control is down select additional elements...
																																																				* @bsimethod                                                              Bentley Systems
																																																				+---------------+---------------+---------------+---------------+---------------+------*/
	virtual size_t _GetAdditionalLocateNumRequired() override { return 2; }
	virtual bool _WantAdditionalLocate(DgnButtonEventCP ev) override { return WantAdditionalLocateHelper(ev); }
	virtual bool _OnModifierKeyTransition(bool wentDown, int key) override { return OnModifierKeyTransitionHelper(wentDown, key); }

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
	virtual void _SetupAndPromptForNextAction() override
	{
		WString     prompt;

		RmgrResource::LoadWString(prompt, 0, STRINGLISTID_Prompts, GetPromptMsgIdForCurrentToolState());
		NotificationManager::OutputPrompt(prompt.c_str());

		SetupAndPromptForNextActionHelper(); // Enable/Disable Auto-Locate based on _GetAdditionalLocateNumRequired.
	}

	/*---------------------------------------------------------------------------------**//**
																						  * Called for each element in tool's ElementAgenda in order to perform modification.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
	virtual StatusInt _OnElementModify(EditElementHandleR eeh) override
	{
		eeh.Invalidate();

		return SUCCESS; // Returning SUCCESS with an invalid EditElementHandle means delete the original element from the model...
	}

	/*---------------------------------------------------------------------------------**//**
																						  * Return true if this element has the required geometry and should be accepted.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
	virtual bool _IsElementValidForOperation(ElementHandleCR eh, HitPathCP path, WStringR cantAcceptReason) override
	{
		// If already verfied by _OnPostLocate there's no reason to re-check for _FilterAgendaEntries.
		if (NULL == path && ModifyElementSource::Selected == GetElementAgenda().GetSource())
			return true;

		// Base class implementation returns true if geometry cache isn't empty (assumes tool collects only supported geometry types, in this case BReps).
		if (!__super::_IsElementValidForOperation(eh, path, cantAcceptReason))
			return false;

		// To be valid for modification element should be fully represented by a single solid (or surface for sew command).
		// NOTE: The overridden _Collect and _OnProcess methods have tool only collecting the required geometry, so a simple count check will suffice.
		return (1 == GetElementGraphicsCacheCount(eh) && !IsGeometryMissing(eh));
	}

	/*---------------------------------------------------------------------------------**//**
																						  * Perform the solid operation on target/tool entities.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
	StatusInt DoOperation(ISolidKernelEntityPtr& target, bvector<ISolidKernelEntityPtr>& tool)
	{
		switch (m_operation)
		{
		case OP_BooleanUnion:
		{
			return SolidUtil::Modify::BooleanUnion(target, &tool.front(), tool.size());
		}

		case OP_BooleanDifference:
		{
			return SolidUtil::Modify::BooleanSubtract(target, &tool.front(), tool.size());
		}

		case OP_BooleanIntersection:
		{
			return SolidUtil::Modify::BooleanIntersect(target, &tool.front(), tool.size());
		}

		case OP_SewSheets:
		{
			bvector<ISolidKernelEntityPtr> sewn;
			bvector<ISolidKernelEntityPtr> unsewn;

			tool.push_back(target); // Sew operation works on a single list of tool bodies...

			if (SUCCESS != SolidUtil::Modify::SewBodies(sewn, unsewn, &tool.front(), tool.size(), 1.0e-3) || 1 != sewn.size() || 0 != unsewn.size())
				return ERROR; // For the purpose of this example a result that isn't a single body or doesn't include all tools is considered a failure.

			target = sewn.front();

			return SUCCESS;
		}

		default:
			return ERROR;
		}
	}

	/*---------------------------------------------------------------------------------**//**
																						  * Perform the solid operation on the collected geometry. Produce a new smart solid/surface
																						  * element from the result. Upon successful create/add of a new smart solid/surface,
																						  * call __super to call _OnElementModify on original elements, which then request they be deleted.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
	virtual StatusInt _ProcessAgenda(DgnButtonEventCR ev) override
	{
		ISolidKernelEntityPtr           target;
		bvector<ISolidKernelEntityPtr>  tool;

		ElementHandleCP  first = GetElementAgenda().GetFirstP();
		ElementHandleCP  end = first + GetElementAgenda().GetCount();

		for (ElementHandleCP curr = first; curr < end; curr++)
		{
			bvector<IElementGraphicsPtr> geomCache;

			if (SUCCESS != GetElementGraphicsCache(*curr, geomCache))
				continue;

			for each (IElementGraphicsPtr geomPtr in geomCache)
			{
				ISolidKernelEntityPtr entityPtr = TryGetAsBRep(geomPtr);

				if (!entityPtr.IsValid())
					continue;

				if (target.IsValid())
					tool.push_back(entityPtr);
				else
					target = entityPtr;
			}
		}

		if (SUCCESS != DoOperation(target, tool))
			return ERROR;

		EditElementHandle  newEeh;

		if (SUCCESS != SolidUtil::Convert::BodyToElement(newEeh, *target, first, *first->GetModelRef()))
			return ERROR;

		if (SUCCESS != newEeh.AddToModel())
			return ERROR;

		return __super::_ProcessAgenda(ev); // Delete originals...
	}

	/*---------------------------------------------------------------------------------**//**
																						  * Install a new instance of the tool. Will be called in response to external events
																						  * such as undo or by the base class from _OnReinitialize when the tool needs to be
																						  * reset to it's initial state.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
	virtual void _OnRestartTool() override
	{
		InstallNewInstance(GetToolId(), m_operation);
	}

public:

	/*---------------------------------------------------------------------------------**//**
																						  * Method to create and install a new instance of the tool. If InstallTool returns ERROR,
																						  * the new tool instance will be freed/invalid. Never call delete on RefCounted classes.
																						  * @bsimethod                                                              Bentley Systems
																						  +---------------+---------------+---------------+---------------+---------------+------*/
	static void InstallNewInstance(int toolId, ToolOperation operation)
	{
		TestBooleanTool* tool = new TestBooleanTool(toolId, operation);

		tool->InstallTool();
	}

}; // TestBooleanTool

   /*=================================================================================**//**
																						 * Functions associated with command numbers for starting tools.
																						 * @param[in] unparsed Additional input supplied after command string.
																						 +===============+===============+===============+===============+===============+======*/
																						 /*---------------------------------------------------------------------------------**//**
																																											   * @bsimethod                                                              Bentley Systems
																																											   +---------------+---------------+---------------+---------------+---------------+------*/
Public void startExampleUnionElementTool(WCharCP unparsed)
{
	// NOTE: Call the method to create/install the tool, RefCounted classes don't have public constructors...
	TestBooleanTool::InstallNewInstance(CMDNAME_ExampleUnionTool, TestBooleanTool::OP_BooleanUnion);
}

/*---------------------------------------------------------------------------------**//**
																					  * @bsimethod                                                              Bentley Systems
																					  +---------------+---------------+---------------+---------------+---------------+------*/
Public void startExampleDifferenceElementTool(WCharCP unparsed)
{
	// NOTE: Call the method to create/install the tool, RefCounted classes don't have public constructors...
	TestBooleanTool::InstallNewInstance(CMDNAME_ExampleDifferenceTool, TestBooleanTool::OP_BooleanDifference);
}

/*---------------------------------------------------------------------------------**//**
																					  * @bsimethod                                                              Bentley Systems
																					  +---------------+---------------+---------------+---------------+---------------+------*/
Public void startExampleIntersectionElementTool(WCharCP unparsed)
{
	// NOTE: Call the method to create/install the tool, RefCounted classes don't have public constructors...
	TestBooleanTool::InstallNewInstance(CMDNAME_ExampleIntersectionTool, TestBooleanTool::OP_BooleanIntersection);
}

/*---------------------------------------------------------------------------------**//**
																					  * @bsimethod                                                              Bentley Systems
																					  +---------------+---------------+---------------+---------------+---------------+------*/
Public void startExampleSewElementTool(WCharCP unparsed)
{
	// NOTE: Call the method to create/install the tool, RefCounted classes don't have public constructors...
	TestBooleanTool::InstallNewInstance(CMDNAME_ExampleSewTool, TestBooleanTool::OP_SewSheets);
}
