/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/Examples/SpiralCreator.cs $
|
|  $Copyright: (c) 2018 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.CifNET.LinearGeometry;
using Bentley.GeometryNET;
using Bentley.CifNET.SDK.Edit;
using Bentley.CifNET.GeometryModel.SDK.Edit;

namespace ManagedSDKExample
{
    class SpiralCreator : DgnElementSetTool
    {
        private double UOR_TO_MASTER = GeometryHelper.ConvertUORToMeter(1.0);

        /*----------------------------------------------------------------------------------------------**/
        /* Write Function | The user is prompted to place a point, where a spiral is then created from code.
        /*--------------+---------------+---------------+---------------+---------------+----------------*/
        internal void CreateSpiralFromPoints(DPoint3d startPoint)
        {
            //adjusts x and y values
            startPoint.X *= UOR_TO_MASTER;
            startPoint.Y *= UOR_TO_MASTER;
            startPoint.Z *= UOR_TO_MASTER;

            //creates new spiral
            Spiral spiral = new Spiral(startPoint, 0.0, 1000.0, 30000.0, 0.0, SpiralType.Clothoid, Hand.Clockwise);

            ConsensusConnectionEdit con = ConsensusConnectionEdit.GetActive();
            con.StartTransientMode();
            AlignmentEdit al = AlignmentEdit.CreateByLinearElement(con, spiral, true);
            con.PersistTransients();
        }

        protected override void OnPostInstall()
        {
            NotificationManager.OutputPrompt("Select first data point.");
            BeginDynamics();
        }

        //gets point
        protected override bool OnDataButton(DgnButtonEvent ev)
        {
            CreateSpiralFromPoints(ev.Point);
            NotificationManager.OutputPrompt("Select first data point or pick element selection tool to exit command.");
            return false;
        }

        protected override void OnRestartTool()
        {
            InstallNewInstance();
        }

        public override StatusInt OnElementModify(Element element)
        {
            return StatusInt.Error;
        }

        public static void InstallNewInstance()
        {
            SpiralCreator tool = new SpiralCreator();
            tool.InstallTool();
        }
    }
}
