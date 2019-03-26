using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.MstnPlatformNET.WinForms.Controls;
using MSAppNET;

namespace eZBMCE.Debug
{
    public partial class Form1 : Form
    {

        private MyElementPreviewControl myElemPreview;
        private ColorTable myColorTable;
        private ColorPickerPopup myColorPicker;
        
        public Form1(ulong id)
        {
            InitializeComponent();
            //
            myElemPreview = new MyElementPreviewControl(id, MSRenderMode.SmoothShade);
            myElemPreview.Dock = DockStyle.Fill;
            panel1.Controls.Add(myElemPreview);
        }
    }
}
