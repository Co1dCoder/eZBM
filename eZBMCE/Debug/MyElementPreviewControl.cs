using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Bentley.GeometryNET;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.MstnPlatformNET;
using Bentley.MstnPlatformNET.WinForms.Controls;
using System.Windows.Forms;
using eZBMCE.Debug.Geometry;

namespace MSAppNET
{
    public class MyElementPreviewControl : ElementPreview
    {
        private ulong m_elemId;
        private MSRenderMode m_renderMode;
        public MyElementPreviewControl(ulong elemId, MSRenderMode renderMode)
        {
            m_elemId = elemId;
            m_renderMode = renderMode;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            EcLevelManager._docMdf.WriteMessageLineNow("OnPaint");
            base.OnPaint(e);

            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            DisplayableElement myElem = dgnModel.FindElementById((ElementId)m_elemId) as DisplayableElement;
            if (null == myElem)
            {
                MessageCenter.Instance.ShowErrorMessage("myElem is invalid", null, true);
                return;
            }
            ViewFlags myViewFlags = ViewInformation.GetDefaultFlags();
            myViewFlags.Camera = false;
            myViewFlags.RenderMode = (int)m_renderMode;
            myViewFlags.Grid = false;
            DRange3d elemRng;
            myElem.CalcElementRange(out elemRng);
            DPoint3d myOrg = elemRng.Low;
            DPoint3d myRng = DPoint3d.Subtract(elemRng.High, elemRng.Low);
            Rectangle myRect = new Rectangle(Location, Size);
            DisplayElemHandle(BytesToIntptr(myElem.ElementHandle), myViewFlags, myRect, null, myOrg, myRng);
        }
        public static IntPtr BytesToIntptr(byte[] bytes)
        {
            int size = bytes.Length;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, buffer, size);
            return buffer;
        }
    }
}
