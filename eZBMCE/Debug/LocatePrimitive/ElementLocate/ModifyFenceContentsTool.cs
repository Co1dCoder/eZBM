/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/ManagedFenceExample/ModifyFenceContentsTool.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.MstnPlatformNET;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using System.Windows.Forms;

namespace ManagedFenceExample
{

/*=================================================================================**//**
* @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
public class ModifyFenceContentsTool : DgnElementSetTool
{

//Options available through this tool.
public enum ModifyFenceContentsFlags
    {
    ModifyFenceMove,
    ModifyFenceClip,
    ModifyFenceStretch
    }

private ModifyFenceContentsFlags m_modifyFenceOption;

private static ModifyFenceContentsOptionsForm s_modifyFenceOptionsForm = null;

/*---------------------------------------------------------------------------------**//**
* Constructor.
* @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
private ModifyFenceContentsTool(ModifyFenceContentsFlags modifyFenceOption) : base()
    {
    m_modifyFenceOption = modifyFenceOption;

    string toolName = "";

    switch(m_modifyFenceOption)
        {
        case ModifyFenceContentsFlags.ModifyFenceMove:
            toolName = "Move";
            break;
        case ModifyFenceContentsFlags.ModifyFenceClip:
            toolName = "Clip";
            break;
        case ModifyFenceContentsFlags.ModifyFenceStretch:
            toolName = "Stretch";
        break;
        }

    //Load options form.
    if(null == s_modifyFenceOptionsForm)
        {
        s_modifyFenceOptionsForm = new ModifyFenceContentsOptionsForm(toolName);
        s_modifyFenceOptionsForm.Show();
        }
    }

/*---------------------------------------------------------------------------------**//**
* Close previous form if a new command is issued.
* @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
public static void CloseForm()
    {
    if(null != s_modifyFenceOptionsForm)
        {
        s_modifyFenceOptionsForm.Close();
        s_modifyFenceOptionsForm = null;
        }
    }

/*---------------------------------------------------------------------------------**//**
* When the tool is installed, show an appropriate message to the user what to do.
* @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
protected override void OnPostInstall()
    {
    switch(m_modifyFenceOption)
        {
        case ModifyFenceContentsFlags.ModifyFenceMove:
            NotificationManager.OutputPrompt("Enter a data point to move fence contents");
            break;
        case ModifyFenceContentsFlags.ModifyFenceClip:
            NotificationManager.OutputPrompt("Enter a data point to clip and move fence contents");
            break;
        case ModifyFenceContentsFlags.ModifyFenceStretch:
            NotificationManager.OutputPrompt("Enter a data point to stretch fence contents");
        break;
        }
    }

/*---------------------------------------------------------------------------------**//**
* Move, Clip and Stretch operations are done on data point.
* @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
protected override bool OnDataButton(DgnButtonEvent ev)
    {

    DgnModelRef modelRef = Session.Instance.GetActiveDgnModelRef();
    ModifyFenceContentsOptions modifyOptions = ModifyFenceContentsOptionsForm.GetModifyFenceContentsOptions();

    FenceParameters fenceParams = new FenceParameters(modelRef, DTransform3d.Identity);
    FenceManager.InitFromActiveFence(fenceParams, modifyOptions.m_overlap, modifyOptions.m_doClip, modifyOptions.m_fenceClipMode);

    ElementAgenda eAgenda = new ElementAgenda();
    DgnModelRef[] modelRefList = new DgnModelRef[1];
    modelRefList[0] = modelRef;

    FenceManager.BuildAgenda(fenceParams, eAgenda, modelRefList, false, false, false);

    MessageBox.Show(eAgenda.GetCount().ToString());

    if(m_modifyFenceOption == ModifyFenceContentsFlags.ModifyFenceMove)
        {
            for(uint i=0; i<eAgenda.GetCount(); i++)
            {
            Element element = eAgenda.GetEntry(i);
            
            DTransform3d transform = DTransform3d.FromTranslation(ev.Point);
            TransformInfo tInfo = new TransformInfo(transform);
            element.ApplyTransform(tInfo);
            element.ReplaceInModel(element);
            }
        }
    else if(m_modifyFenceOption == ModifyFenceContentsFlags.ModifyFenceStretch)
        {
        for(uint i=0; i<eAgenda.GetCount(); i++)
            {
            Element element = eAgenda.GetEntry(i);
            DTransform3d transform = DTransform3d.FromTranslation(ev.Point);
            FenceManager.StretchElement(fenceParams, element, transform, FenceStretchFlags.None);

            element.ReplaceInModel(element);
            }
        }
    else if(m_modifyFenceOption == ModifyFenceContentsFlags.ModifyFenceClip)
        {
            for(uint i=0; i<eAgenda.GetCount(); i++)
            {
            ElementAgenda insideElems  = new ElementAgenda();
            ElementAgenda outsideElems = new ElementAgenda();
            Element element = eAgenda.GetEntry(i);
            
            //Clip element.
            FenceManager.ClipElement(fenceParams, insideElems, outsideElems, element, FenceClipFlags.None);

            DTransform3d transform = DTransform3d.FromTranslation(ev.Point);
            for(uint j=0; j<insideElems.GetCount(); j++)
                {
                Element elemToCopy = insideElems.GetEntry(j);
                TransformInfo tInfo = new TransformInfo(transform);
                elemToCopy.ApplyTransform(tInfo);
                using (ElementCopyContext copyContext = new ElementCopyContext(modelRef))
                    {
                    copyContext.DoCopy(elemToCopy);
                    }
                }
            }
        }

    return true;
    }

/*---------------------------------------------------------------------------------**//**
* Everything is done in OnDataButton event, just return Error from this method.
* @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
public override StatusInt OnElementModify(Element element)
    {
    return StatusInt.Error;
    }

/*---------------------------------------------------------------------------------**//**
* Start instance with the same option.
* @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
protected override void OnRestartTool()
    {
    InstallNewInstance(m_modifyFenceOption);
    }

/*---------------------------------------------------------------------------------**//**
* Static method to be called from AddIn class.
* @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
public static void InstallNewInstance (ModifyFenceContentsFlags modifyFenceOption)
    {
    ModifyFenceContentsTool.CloseForm();
    ModifyFenceContentsTool modifyFenceContentsTool = new ModifyFenceContentsTool(modifyFenceOption);
    modifyFenceContentsTool.InstallTool();
    }
}
}
