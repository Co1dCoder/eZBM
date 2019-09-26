namespace MsdiTransportation.SubgradeTemplateLib.Control
{
    partial class C_SubgradeComp
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Width = new System.Windows.Forms.TextBox();
            this.label_Features = new System.Windows.Forms.Label();
            this.dataGridView_Features = new System.Windows.Forms.DataGridView();
            this.FeatureCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ThicknessCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox_Activated = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.panel_centerSide = new System.Windows.Forms.Panel();
            this.pictureBox_Center = new System.Windows.Forms.PictureBox();
            this.pictureBox_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Left = new System.Windows.Forms.PictureBox();
            this.textBox_CrossSlope = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Features)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Activated)).BeginInit();
            this.panel_centerSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Center)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Left)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型";
            // 
            // comboBox_type
            // 
            this.comboBox_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Location = new System.Drawing.Point(39, 5);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(84, 20);
            this.comboBox_type.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "宽度(m)";
            this.toolTip1.SetToolTip(this.label3, "从道路中心向外的各车道宽度");
            // 
            // textBox_Width
            // 
            this.textBox_Width.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Width.Location = new System.Drawing.Point(57, 31);
            this.textBox_Width.Name = "textBox_Width";
            this.textBox_Width.Size = new System.Drawing.Size(97, 21);
            this.textBox_Width.TabIndex = 2;
            // 
            // label_Features
            // 
            this.label_Features.AutoSize = true;
            this.label_Features.Location = new System.Drawing.Point(4, 105);
            this.label_Features.Name = "label_Features";
            this.label_Features.Size = new System.Drawing.Size(53, 12);
            this.label_Features.TabIndex = 0;
            this.label_Features.Text = "分层信息";
            this.label_Features.Click += new System.EventHandler(this.Components_Activated_Click);
            // 
            // dataGridView_Features
            // 
            this.dataGridView_Features.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Features.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_Features.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Features.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FeatureCol,
            this.ThicknessCol});
            this.dataGridView_Features.Location = new System.Drawing.Point(0, 120);
            this.dataGridView_Features.Name = "dataGridView_Features";
            this.dataGridView_Features.RowHeadersWidth = 10;
            this.dataGridView_Features.RowTemplate.Height = 23;
            this.dataGridView_Features.Size = new System.Drawing.Size(157, 96);
            this.dataGridView_Features.TabIndex = 3;
            this.dataGridView_Features.Click += new System.EventHandler(this.Components_Activated_Click);
            // 
            // FeatureCol
            // 
            this.FeatureCol.HeaderText = "特征";
            this.FeatureCol.Name = "FeatureCol";
            this.FeatureCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FeatureCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.FeatureCol.Width = 75;
            // 
            // ThicknessCol
            // 
            this.ThicknessCol.HeaderText = "厚度(m)";
            this.ThicknessCol.Name = "ThicknessCol";
            this.ThicknessCol.Width = 70;
            // 
            // pictureBox_Activated
            // 
            this.pictureBox_Activated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Activated.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Activated.Location = new System.Drawing.Point(142, 10);
            this.pictureBox_Activated.Name = "pictureBox_Activated";
            this.pictureBox_Activated.Size = new System.Drawing.Size(10, 10);
            this.pictureBox_Activated.TabIndex = 5;
            this.pictureBox_Activated.TabStop = false;
            this.pictureBox_Activated.Click += new System.EventHandler(this.Components_Activated_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "横坡";
            this.toolTip1.SetToolTip(this.label4, "正负值的含义与其所在左右侧相关");
            // 
            // panel_centerSide
            // 
            this.panel_centerSide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_centerSide.BackColor = System.Drawing.Color.Transparent;
            this.panel_centerSide.Controls.Add(this.pictureBox_Center);
            this.panel_centerSide.Controls.Add(this.pictureBox_Right);
            this.panel_centerSide.Controls.Add(this.pictureBox_Left);
            this.panel_centerSide.Location = new System.Drawing.Point(0, 83);
            this.panel_centerSide.Name = "panel_centerSide";
            this.panel_centerSide.Size = new System.Drawing.Size(157, 17);
            this.panel_centerSide.TabIndex = 7;
            this.toolTip1.SetToolTip(this.panel_centerSide, "选择道路中心线的位置");
            this.panel_centerSide.Click += new System.EventHandler(this.Components_Activated_Click);
            // 
            // pictureBox_Center
            // 
            this.pictureBox_Center.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pictureBox_Center.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox_Center.Location = new System.Drawing.Point(65, 0);
            this.pictureBox_Center.Name = "pictureBox_Center";
            this.pictureBox_Center.Size = new System.Drawing.Size(30, 17);
            this.pictureBox_Center.TabIndex = 5;
            this.pictureBox_Center.TabStop = false;
            this.pictureBox_Center.Click += new System.EventHandler(this.pictureBox_centerline_CheckedChanged);
            // 
            // pictureBox_Right
            // 
            this.pictureBox_Right.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Right.Location = new System.Drawing.Point(127, 0);
            this.pictureBox_Right.Name = "pictureBox_Right";
            this.pictureBox_Right.Size = new System.Drawing.Size(30, 17);
            this.pictureBox_Right.TabIndex = 5;
            this.pictureBox_Right.TabStop = false;
            this.pictureBox_Right.Click += new System.EventHandler(this.pictureBox_centerline_CheckedChanged);
            // 
            // pictureBox_Left
            // 
            this.pictureBox_Left.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_Left.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Left.Name = "pictureBox_Left";
            this.pictureBox_Left.Size = new System.Drawing.Size(30, 17);
            this.pictureBox_Left.TabIndex = 5;
            this.pictureBox_Left.TabStop = false;
            this.pictureBox_Left.Click += new System.EventHandler(this.pictureBox_centerline_CheckedChanged);
            // 
            // textBox_CrossSlope
            // 
            this.textBox_CrossSlope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_CrossSlope.Location = new System.Drawing.Point(57, 58);
            this.textBox_CrossSlope.Name = "textBox_CrossSlope";
            this.textBox_CrossSlope.Size = new System.Drawing.Size(97, 21);
            this.textBox_CrossSlope.TabIndex = 2;
            // 
            // C_RoadwayComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_centerSide);
            this.Controls.Add(this.pictureBox_Activated);
            this.Controls.Add(this.dataGridView_Features);
            this.Controls.Add(this.textBox_CrossSlope);
            this.Controls.Add(this.textBox_Width);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_Features);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "C_RoadwayComponent";
            this.Size = new System.Drawing.Size(157, 216);
            this.Click += new System.EventHandler(this.Components_Activated_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Features)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Activated)).EndInit();
            this.panel_centerSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Center)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Left)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Width;
        private System.Windows.Forms.Label label_Features;
        private System.Windows.Forms.DataGridView dataGridView_Features;
        private System.Windows.Forms.PictureBox pictureBox_Activated;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_CrossSlope;
        private System.Windows.Forms.DataGridViewComboBoxColumn FeatureCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThicknessCol;
        private System.Windows.Forms.Panel panel_centerSide;
        private System.Windows.Forms.PictureBox pictureBox_Left;
        private System.Windows.Forms.PictureBox pictureBox_Center;
        private System.Windows.Forms.PictureBox pictureBox_Right;
    }
}
