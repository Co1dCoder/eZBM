using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Bentley.GeometryNET;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.MstnPlatformNET;
using System.Windows.Forms;
using Bentley.MstnPlatformNET.WinForms.Controls;
using eZBMCE.Debug.Geometry;

namespace MSDIAddin
{

    public partial class MstnControlForm : Form
    {
        private ColorTable myColorTable;
        private ColorPickerPopup myColorPicker;
        private MyElementPreviewControl myElemPreview;
        public MstnControlForm()
        {
            InitializeComponent();
            AddControls();
        }

        private void AddControls()
        {
            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            ElementColor elemColor = new ElementColor(0, dgnModel);
            myColorTable = new ColorTable(dgnModel, elemColor);
            myColorTable.Location = new Point(18, 34);
            myColorTable.Size = new Size(400, 400);
            myColorTable.TabIndex = 3;
            myColorTable.ForeColorChanged += MyCT_ForeColorChanged;
            this.Controls.Add(myColorTable);

            ColorPickerPopup myColorPicker = new ColorPickerPopup();
            myColorPicker.ModelRef = dgnModel;
            myColorPicker.SelectedValue = elemColor;
            myColorPicker.Location = new Point(420, 34);
            myColorPicker.Size = new Size(40, 40);
            myColorTable.TabIndex = 4;
            myColorPicker.SelectedValueChanged += MyColorPicker_SelectedValueChanged;
            this.Controls.Add(myColorPicker);

            myElemPreview = new MyElementPreviewControl(1151L, MSRenderMode.Wireframe);
            myElemPreview.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(myElemPreview);
        }

        private void MyColorPicker_SelectedValueChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = this.myColorPicker.SelectedValue.ToString();
        }

        private void MyCT_ForeColorChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = this.myColorTable.SelectedColorIndex.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myElemPreview.PaintElement();
        }
    }

    public class colorTableHost : ColorTable
    {
        public colorTableHost(DgnModelRef modelRef, ElementColor elementColor) : base(modelRef, elementColor)
        {
        }
    }

}
