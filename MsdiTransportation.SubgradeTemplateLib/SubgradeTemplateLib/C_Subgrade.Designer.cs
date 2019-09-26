namespace MsdiTransportation.SubgradeTemplateLib.Control
{
    partial class C_Subgrade
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_InsertRight = new System.Windows.Forms.Button();
            this.button_DeleteComp = new System.Windows.Forms.Button();
            this.button_InsertLeft = new System.Windows.Forms.Button();
            this.panel_subgradesHolder = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_InsertRight);
            this.panel1.Controls.Add(this.button_DeleteComp);
            this.panel1.Controls.Add(this.button_InsertLeft);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 31);
            this.panel1.TabIndex = 6;
            // 
            // button_InsertRight
            // 
            this.button_InsertRight.Location = new System.Drawing.Point(167, 3);
            this.button_InsertRight.Name = "button_InsertRight";
            this.button_InsertRight.Size = new System.Drawing.Size(75, 23);
            this.button_InsertRight.TabIndex = 3;
            this.button_InsertRight.Text = "右侧插入 >";
            this.button_InsertRight.UseVisualStyleBackColor = true;
            this.button_InsertRight.Click += new System.EventHandler(this.button_InsertRight_Click);
            // 
            // button_DeleteComp
            // 
            this.button_DeleteComp.Location = new System.Drawing.Point(86, 3);
            this.button_DeleteComp.Name = "button_DeleteComp";
            this.button_DeleteComp.Size = new System.Drawing.Size(75, 23);
            this.button_DeleteComp.TabIndex = 3;
            this.button_DeleteComp.Text = "删除组件";
            this.button_DeleteComp.UseVisualStyleBackColor = true;
            this.button_DeleteComp.Click += new System.EventHandler(this.button_DeleteComp_Click);
            // 
            // button_InsertLeft
            // 
            this.button_InsertLeft.Location = new System.Drawing.Point(6, 3);
            this.button_InsertLeft.Name = "button_InsertLeft";
            this.button_InsertLeft.Size = new System.Drawing.Size(75, 23);
            this.button_InsertLeft.TabIndex = 3;
            this.button_InsertLeft.Text = "< 左侧插入";
            this.button_InsertLeft.UseVisualStyleBackColor = true;
            this.button_InsertLeft.Click += new System.EventHandler(this.button_InsertLeft_Click);
            // 
            // panel_subgradesHolder
            // 
            this.panel_subgradesHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_subgradesHolder.AutoScroll = true;
            this.panel_subgradesHolder.Location = new System.Drawing.Point(4, 41);
            this.panel_subgradesHolder.Name = "panel_subgradesHolder";
            this.panel_subgradesHolder.Size = new System.Drawing.Size(493, 304);
            this.panel_subgradesHolder.TabIndex = 7;
            // 
            // C_RoadwayComponentConstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_subgradesHolder);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(500, 150);
            this.Name = "C_RoadwayComponentConstructor";
            this.Size = new System.Drawing.Size(500, 348);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_InsertRight;
        private System.Windows.Forms.Button button_DeleteComp;
        private System.Windows.Forms.Button button_InsertLeft;
        private System.Windows.Forms.Panel panel_subgradesHolder;
    }
}
