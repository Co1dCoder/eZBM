/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Annotations/ManagedItemTypesExample/ManagedItemTypesExample.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET;
using Bentley.MstnPlatformNET;

namespace ManagedItemTypesExample
{

    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    [AddInAttribute(MdlTaskID = "ManagedItemTypesExample")]
    public sealed class ManagedItemTypesExample : AddIn
    {

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public ManagedItemTypesExample(System.IntPtr mdlDesc)
            : base(mdlDesc)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        /*---------------------------------------------------------------------------------**//**
        * Returns active DgnFile.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static DgnFile GetActiveDgnFile()
        {
            return Session.Instance.GetActiveDgnFile();
        }

        /*---------------------------------------------------------------------------------**//**
        * Returns active DgnModel.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static DgnModel GetActiveDgnModel()
        {
            return Session.Instance.GetActiveDgnModel();
        }

        /*---------------------------------------------------------------------------------**//**
        * Required Run method of AddIn class.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        protected override int Run(string[] commandLine)
        {
            return 0;
        }

    }
}
