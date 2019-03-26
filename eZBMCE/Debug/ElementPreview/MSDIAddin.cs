//--------------------------------------------------------------------------------------+
//
//    $Source: MSDIAddin.cs $
// 
//    $Copyright: (c) 2018 Bentley Systems, Incorporated. All rights reserved. $
//
//---------------------------------------------------------------------------------------+

namespace MSDIAddin
{
    [Bentley.MstnPlatformNET.AddIn(MdlTaskID = "MSDIAddin")]
    internal sealed class MSDIAddin : Bentley.MstnPlatformNET.AddIn
    {
        //--------------------------------------------------------------------------------------
        // @description   This function does...
        // @bsimethod                                                    Bentley
        //+---------------+---------------+---------------+---------------+---------------+------
        private MSDIAddin(System.IntPtr mdlDesc) : base(mdlDesc)
        {
        }
        //--------------------------------------------------------------------------------------
        // @description   This function does...
        // @bsimethod                                                    Bentley
        //+---------------+---------------+---------------+---------------+---------------+------
        protected override int Run(string[] commandLine)
        {
            return 0;
        }
    }
}