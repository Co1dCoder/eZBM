using System.Windows.Forms;
using MsdiTransportation.SubgradeTemplateLib;
using MsdiTransportation.SubgradeTemplateLib.Control;

namespace MsdiTransportation.SubgradeComponent
{
    partial class FormSubgradeConstructor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_LaneNum = new System.Windows.Forms.ComboBox();
            this.btn_ConstructSubgrade = new System.Windows.Forms.Button();
            this.comboBox_SectionType = new System.Windows.Forms.ComboBox();
            this.comboBox_designSpeed = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_highwayClass = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_apply = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_ChooseItlLib = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox_itlToSave = new System.Windows.Forms.TextBox();
            this.subgradeConstructor1 = new MsdiTransportation.SubgradeTemplateLib.Control.C_Subgrade();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_LaneNum);
            this.groupBox1.Controls.Add(this.btn_ConstructSubgrade);
            this.groupBox1.Controls.Add(this.comboBox_SectionType);
            this.groupBox1.Controls.Add(this.comboBox_designSpeed);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBox_highwayClass);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 163);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "道路标准";
            // 
            // comboBox_LaneNum
            // 
            this.comboBox_LaneNum.DisplayMember = "Key";
            this.comboBox_LaneNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_LaneNum.FormattingEnabled = true;
            this.comboBox_LaneNum.Location = new System.Drawing.Point(68, 102);
            this.comboBox_LaneNum.Name = "comboBox_LaneNum";
            this.comboBox_LaneNum.Size = new System.Drawing.Size(121, 20);
            this.comboBox_LaneNum.TabIndex = 1;
            this.comboBox_LaneNum.ValueMember = "Value";
            this.comboBox_LaneNum.SelectedIndexChanged += new System.EventHandler(this.comboBox_designSpeed_SelectedIndexChanged);
            // 
            // btn_ConstructSubgrade
            // 
            this.btn_ConstructSubgrade.Location = new System.Drawing.Point(6, 134);
            this.btn_ConstructSubgrade.Name = "btn_ConstructSubgrade";
            this.btn_ConstructSubgrade.Size = new System.Drawing.Size(75, 23);
            this.btn_ConstructSubgrade.TabIndex = 6;
            this.btn_ConstructSubgrade.Text = "构造";
            this.toolTip1.SetToolTip(this.btn_ConstructSubgrade, "根据选择的道路标准构造一个初始的横断面");
            this.btn_ConstructSubgrade.UseVisualStyleBackColor = true;
            this.btn_ConstructSubgrade.Click += new System.EventHandler(this.btn_ConstructSubgrade_Click);
            // 
            // comboBox_SectionType
            // 
            this.comboBox_SectionType.DisplayMember = "Key";
            this.comboBox_SectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SectionType.FormattingEnabled = true;
            this.comboBox_SectionType.Location = new System.Drawing.Point(68, 50);
            this.comboBox_SectionType.Name = "comboBox_SectionType";
            this.comboBox_SectionType.Size = new System.Drawing.Size(121, 20);
            this.comboBox_SectionType.TabIndex = 1;
            this.comboBox_SectionType.ValueMember = "Value";
            this.comboBox_SectionType.SelectedIndexChanged += new System.EventHandler(this.comboBox_designSpeed_SelectedIndexChanged);
            // 
            // comboBox_designSpeed
            // 
            this.comboBox_designSpeed.DisplayMember = "Key";
            this.comboBox_designSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_designSpeed.FormattingEnabled = true;
            this.comboBox_designSpeed.Location = new System.Drawing.Point(68, 76);
            this.comboBox_designSpeed.Name = "comboBox_designSpeed";
            this.comboBox_designSpeed.Size = new System.Drawing.Size(121, 20);
            this.comboBox_designSpeed.TabIndex = 1;
            this.comboBox_designSpeed.ValueMember = "Value";
            this.comboBox_designSpeed.SelectedIndexChanged += new System.EventHandler(this.comboBox_designSpeed_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "车道数量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "断面形式";
            // 
            // comboBox_highwayClass
            // 
            this.comboBox_highwayClass.DisplayMember = "Key";
            this.comboBox_highwayClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_highwayClass.FormattingEnabled = true;
            this.comboBox_highwayClass.Location = new System.Drawing.Point(68, 24);
            this.comboBox_highwayClass.Name = "comboBox_highwayClass";
            this.comboBox_highwayClass.Size = new System.Drawing.Size(121, 20);
            this.comboBox_highwayClass.TabIndex = 1;
            this.comboBox_highwayClass.ValueMember = "Value";
            this.comboBox_highwayClass.SelectedIndexChanged += new System.EventHandler(this.comboBox_highwayClass_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "设计车速";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "公路等级";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(217, 71);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(355, 113);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btn_apply
            // 
            this.btn_apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_apply.Location = new System.Drawing.Point(497, 479);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(75, 23);
            this.btn_apply.TabIndex = 4;
            this.btn_apply.Text = "应用";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(298, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "某一个记录了横断面数据的库文件";
            // 
            // btn_ChooseItlLib
            // 
            this.btn_ChooseItlLib.Location = new System.Drawing.Point(217, 27);
            this.btn_ChooseItlLib.Name = "btn_ChooseItlLib";
            this.btn_ChooseItlLib.Size = new System.Drawing.Size(75, 23);
            this.btn_ChooseItlLib.TabIndex = 4;
            this.btn_ChooseItlLib.Text = "选择库";
            this.toolTip1.SetToolTip(this.btn_ChooseItlLib, "从库文件中提取一个断面");
            this.btn_ChooseItlLib.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(416, 479);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // textBox_itlToSave
            // 
            this.textBox_itlToSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_itlToSave.Location = new System.Drawing.Point(12, 479);
            this.textBox_itlToSave.Name = "textBox_itlToSave";
            this.textBox_itlToSave.Size = new System.Drawing.Size(398, 21);
            this.textBox_itlToSave.TabIndex = 7;
            // 
            // subgradeConstructor1
            // 
            this.subgradeConstructor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subgradeConstructor1.Location = new System.Drawing.Point(12, 190);
            this.subgradeConstructor1.MinimumSize = new System.Drawing.Size(500, 150);
            this.subgradeConstructor1.Name = "subgradeConstructor1";
            this.subgradeConstructor1.Size = new System.Drawing.Size(560, 283);
            this.subgradeConstructor1.TabIndex = 3;
            // 
            // FormSubgradeConstructor
            // 
            this.AcceptButton = this.btn_apply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(584, 514);
            this.Controls.Add(this.textBox_itlToSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_ChooseItlLib);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_apply);
            this.Controls.Add(this.subgradeConstructor1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "FormSubgradeConstructor";
            this.Text = "路基断面设计器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_designSpeed;
        private System.Windows.Forms.ComboBox comboBox_highwayClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_LaneNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_SectionType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private C_Subgrade subgradeConstructor1;
        private Button btn_apply;
        private Label label5;
        private Button btn_ChooseItlLib;
        private Button btn_cancel;
        private Button btn_ConstructSubgrade;
        private ToolTip toolTip1;
        private TextBox textBox_itlToSave;
    }
}