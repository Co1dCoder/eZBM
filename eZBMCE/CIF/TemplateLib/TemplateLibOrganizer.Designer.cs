namespace eZBMCE.CIF.TemplateLib
{
    partial class TemplateLibOrganizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateLibOrganizer));
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_OpenFile = new System.Windows.Forms.Button();
            this.button_Apply = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_PreviousSideTemplate = new System.Windows.Forms.Button();
            this.button_NextSideTemplate = new System.Windows.Forms.Button();
            this.textBox_TemplateLibPath = new System.Windows.Forms.TextBox();
            this.treeView_TemplateLib = new System.Windows.Forms.TreeView();
            this.imageList_forTreeview = new System.Windows.Forms.ImageList(this.components);
            this.groupBox_DestiDirection = new System.Windows.Forms.GroupBox();
            this.radioButton_toRight = new System.Windows.Forms.RadioButton();
            this.radioButton_toLeft = new System.Windows.Forms.RadioButton();
            this.label_Prompt = new System.Windows.Forms.Label();
            this.groupBox_DestiDirection.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Save.Location = new System.Drawing.Point(493, 426);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "保存";
            this.toolTip1.SetToolTip(this.button_Save, "将修改信息保存至横断面模板 itl 文件");
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(493, 397);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OpenFile
            // 
            this.button_OpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OpenFile.Location = new System.Drawing.Point(535, 11);
            this.button_OpenFile.Name = "button_OpenFile";
            this.button_OpenFile.Size = new System.Drawing.Size(33, 23);
            this.button_OpenFile.TabIndex = 7;
            this.button_OpenFile.Text = "...";
            this.toolTip1.SetToolTip(this.button_OpenFile, "选择要进行修改的横断面模板文件");
            this.button_OpenFile.UseVisualStyleBackColor = true;
            this.button_OpenFile.Click += new System.EventHandler(this.button_OpenFile_Click);
            // 
            // button_Apply
            // 
            this.button_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Apply.Location = new System.Drawing.Point(446, 93);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(75, 23);
            this.button_Apply.TabIndex = 12;
            this.button_Apply.Text = "转换定义";
            this.toolTip1.SetToolTip(this.button_Apply, "修正横断面模板中点、组件等的定义方向转换到目标侧，但是不修改模板的几何。");
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Delete.Location = new System.Drawing.Point(446, 122);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(75, 23);
            this.button_Delete.TabIndex = 13;
            this.button_Delete.Text = "删除";
            this.toolTip1.SetToolTip(this.button_Delete, "删除指定的横断面模板");
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_PreviousSideTemplate
            // 
            this.button_PreviousSideTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_PreviousSideTemplate.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_PreviousSideTemplate.Location = new System.Drawing.Point(446, 163);
            this.button_PreviousSideTemplate.Name = "button_PreviousSideTemplate";
            this.button_PreviousSideTemplate.Size = new System.Drawing.Size(23, 48);
            this.button_PreviousSideTemplate.TabIndex = 15;
            this.button_PreviousSideTemplate.Text = "∧";
            this.toolTip1.SetToolTip(this.button_PreviousSideTemplate, "上一个左右侧路基模板");
            this.button_PreviousSideTemplate.UseVisualStyleBackColor = true;
            this.button_PreviousSideTemplate.Click += new System.EventHandler(this.button_PreviousSideTemplate_Click);
            // 
            // button_NextSideTemplate
            // 
            this.button_NextSideTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NextSideTemplate.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_NextSideTemplate.Location = new System.Drawing.Point(446, 217);
            this.button_NextSideTemplate.Name = "button_NextSideTemplate";
            this.button_NextSideTemplate.Size = new System.Drawing.Size(23, 48);
            this.button_NextSideTemplate.TabIndex = 15;
            this.button_NextSideTemplate.Text = "∨";
            this.toolTip1.SetToolTip(this.button_NextSideTemplate, "下一个左右侧路基模板");
            this.button_NextSideTemplate.UseVisualStyleBackColor = true;
            this.button_NextSideTemplate.Click += new System.EventHandler(this.button_NextSideTemplate_Click);
            // 
            // textBox_TemplateLibPath
            // 
            this.textBox_TemplateLibPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_TemplateLibPath.Location = new System.Drawing.Point(12, 11);
            this.textBox_TemplateLibPath.Name = "textBox_TemplateLibPath";
            this.textBox_TemplateLibPath.ReadOnly = true;
            this.textBox_TemplateLibPath.Size = new System.Drawing.Size(517, 21);
            this.textBox_TemplateLibPath.TabIndex = 9;
            // 
            // treeView_TemplateLib
            // 
            this.treeView_TemplateLib.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView_TemplateLib.ImageIndex = 0;
            this.treeView_TemplateLib.ImageList = this.imageList_forTreeview;
            this.treeView_TemplateLib.Location = new System.Drawing.Point(12, 38);
            this.treeView_TemplateLib.Name = "treeView_TemplateLib";
            this.treeView_TemplateLib.SelectedImageIndex = 0;
            this.treeView_TemplateLib.Size = new System.Drawing.Size(428, 382);
            this.treeView_TemplateLib.TabIndex = 10;
            this.treeView_TemplateLib.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_TemplateLib_NodeMouseClick);
            // 
            // imageList_forTreeview
            // 
            this.imageList_forTreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_forTreeview.ImageStream")));
            this.imageList_forTreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_forTreeview.Images.SetKeyName(0, "Folder_16.png");
            this.imageList_forTreeview.Images.SetKeyName(1, "Template_16.png");
            this.imageList_forTreeview.Images.SetKeyName(2, "TemplateRightSide_16.png");
            this.imageList_forTreeview.Images.SetKeyName(3, "TemplateLeftSide_16.png");
            // 
            // groupBox_DestiDirection
            // 
            this.groupBox_DestiDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_DestiDirection.Controls.Add(this.radioButton_toRight);
            this.groupBox_DestiDirection.Controls.Add(this.radioButton_toLeft);
            this.groupBox_DestiDirection.Location = new System.Drawing.Point(446, 40);
            this.groupBox_DestiDirection.Name = "groupBox_DestiDirection";
            this.groupBox_DestiDirection.Size = new System.Drawing.Size(122, 47);
            this.groupBox_DestiDirection.TabIndex = 11;
            this.groupBox_DestiDirection.TabStop = false;
            this.groupBox_DestiDirection.Text = "目标侧";
            this.groupBox_DestiDirection.Visible = false;
            // 
            // radioButton_toRight
            // 
            this.radioButton_toRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton_toRight.AutoSize = true;
            this.radioButton_toRight.Checked = true;
            this.radioButton_toRight.Location = new System.Drawing.Point(69, 20);
            this.radioButton_toRight.Name = "radioButton_toRight";
            this.radioButton_toRight.Size = new System.Drawing.Size(47, 16);
            this.radioButton_toRight.TabIndex = 1;
            this.radioButton_toRight.TabStop = true;
            this.radioButton_toRight.Text = "向右";
            this.radioButton_toRight.UseVisualStyleBackColor = true;
            // 
            // radioButton_toLeft
            // 
            this.radioButton_toLeft.AutoSize = true;
            this.radioButton_toLeft.Location = new System.Drawing.Point(6, 20);
            this.radioButton_toLeft.Name = "radioButton_toLeft";
            this.radioButton_toLeft.Size = new System.Drawing.Size(47, 16);
            this.radioButton_toLeft.TabIndex = 1;
            this.radioButton_toLeft.Text = "向左";
            this.radioButton_toLeft.UseVisualStyleBackColor = true;
            // 
            // label_Prompt
            // 
            this.label_Prompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Prompt.AutoSize = true;
            this.label_Prompt.Location = new System.Drawing.Point(13, 436);
            this.label_Prompt.Name = "label_Prompt";
            this.label_Prompt.Size = new System.Drawing.Size(41, 12);
            this.label_Prompt.TabIndex = 14;
            this.label_Prompt.Text = "提示：";
            // 
            // TemplateLibOrganizer
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(580, 461);
            this.Controls.Add(this.button_NextSideTemplate);
            this.Controls.Add(this.button_PreviousSideTemplate);
            this.Controls.Add(this.label_Prompt);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.groupBox_DestiDirection);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.treeView_TemplateLib);
            this.Controls.Add(this.textBox_TemplateLibPath);
            this.Controls.Add(this.button_OpenFile);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "TemplateLibOrganizer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "横断面模板转换";
            this.groupBox_DestiDirection.ResumeLayout(false);
            this.groupBox_DestiDirection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OpenFile;
        private System.Windows.Forms.TextBox textBox_TemplateLibPath;
        private System.Windows.Forms.TreeView treeView_TemplateLib;
        private System.Windows.Forms.GroupBox groupBox_DestiDirection;
        private System.Windows.Forms.RadioButton radioButton_toRight;
        private System.Windows.Forms.RadioButton radioButton_toLeft;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.ImageList imageList_forTreeview;
        private System.Windows.Forms.Label label_Prompt;
        private System.Windows.Forms.Button button_PreviousSideTemplate;
        private System.Windows.Forms.Button button_NextSideTemplate;
    }
}