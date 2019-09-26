/*--------------------------------------------------------------------------------------+
|
|  $Source: PowerProduct/OpenRoadsDesignerSDK/ORDExamples/ManagedSDKExample/Examples/PickAlignmentTool.cs $
|
|  $Copyright: (c) 2018 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bentley.GeometryNET;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.MstnPlatformNET;
using System.Windows.Forms;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.SDK;

namespace ManagedSDKExample
    {
    class PickAlignmentTool : DgnElementSetTool
        {
        ConsensusConnection m_con;

        public PickAlignmentTool() : base()
            {
            }

        protected override void OnRestartTool()
            {
            InstallNewInstance();
            }

        protected override void ExitTool()
            {
            if (m_con != null)
                m_con.Dispose();
            m_con = null;
            base.ExitTool();
            }


        protected override void OnPostInstall()
            {
            base.BeginPickElements();
            Bentley.DgnPlatformNET.AccuSnap.LocateEnabled = true;
            Bentley.DgnPlatformNET.AccuSnap.SnapEnabled = true;
            m_con = new ConsensusConnection(Bentley.MstnPlatformNET.Session.Instance.GetActiveDgnModelRef());
            NotificationManager.OutputPrompt("Select an alignment to report.");
            base.OnPostInstall();
            }


        protected override bool OnDataButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
            {
            Bentley.DgnPlatformNET.HitPath hitPath = DoLocate(ev, true, 0);
            if (hitPath == null)
                return false;

            Element el = hitPath.GetCursorElement();
            if (el == null)
                return false;

            Alignment al = (el.ParentElement == null) ? Alignment.CreateFromElement(m_con, el) : Alignment.CreateFromElement(m_con, el.ParentElement); 

            if (al == null)
                return false;

            HorizontalAlignmentReporter hRep = new HorizontalAlignmentReporter();
            hRep.ReportSingleAlignment(al);

            return true;
            }

        public override Bentley.DgnPlatformNET.StatusInt OnElementModify(Bentley.DgnPlatformNET.Elements.Element element)
            {
            return Bentley.DgnPlatformNET.StatusInt.Error;
            }

        protected override bool OnResetButton(Bentley.DgnPlatformNET.DgnButtonEvent ev)
            {
            ExitTool();
            return true;
            }


        public static void InstallNewInstance()
            {
            PickAlignmentTool tool = new PickAlignmentTool();
            tool.InstallTool();
            }
        }
    }
