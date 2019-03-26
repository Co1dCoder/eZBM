/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/ManagedToolsExample/TestModifyEdgeToolForm.Designer.cs $
|
|  $Copyright: (c) 2017 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
 
namespace ManagedToolsExample
    {
    partial class TestModifyEdgeToolForm
        {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
            {
            if ( disposing && (components != null) )
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
        private void InitializeComponent ()
            {
            this.txtBlendRadius = new System.Windows.Forms.TextBox();
            this.lblRadius = new System.Windows.Forms.Label();
            this.chkBoxAllowTangentEdge = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtBlendRadius
            // 
            this.txtBlendRadius.Location = new System.Drawing.Point(119, 59);
            this.txtBlendRadius.Name = "txtBlendRadius";
            this.txtBlendRadius.Size = new System.Drawing.Size(99, 22);
            this.txtBlendRadius.TabIndex = 0;
            this.txtBlendRadius.TextChanged += new System.EventHandler(this.txtBlendRadius_TextChanged);
            this.txtBlendRadius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBlendRadius_KeyPress);
            // 
            // lblRadius
            // 
            this.lblRadius.AutoSize = true;
            this.lblRadius.Location = new System.Drawing.Point(13, 63);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(92, 17);
            this.lblRadius.TabIndex = 1;
            this.lblRadius.Text = "Blend Radius";
            // 
            // chkBoxAllowTangentEdge
            // 
            this.chkBoxAllowTangentEdge.AutoSize = true;
            this.chkBoxAllowTangentEdge.Location = new System.Drawing.Point(16, 98);
            this.chkBoxAllowTangentEdge.Name = "chkBoxAllowTangentEdge";
            this.chkBoxAllowTangentEdge.Size = new System.Drawing.Size(156, 21);
            this.chkBoxAllowTangentEdge.TabIndex = 2;
            this.chkBoxAllowTangentEdge.Text = "Allow Tangent Edge";
            this.chkBoxAllowTangentEdge.UseVisualStyleBackColor = true;
            this.chkBoxAllowTangentEdge.CheckedChanged += new System.EventHandler(this.chkBoxAllowTangentEdge_CheckedChanged);
            // 
            // TestModifyEdgeToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 196);
            this.Controls.Add(this.chkBoxAllowTangentEdge);
            this.Controls.Add(this.lblRadius);
            this.Controls.Add(this.txtBlendRadius);
            this.Name = "TestModifyEdgeToolForm";
            this.Text = "Blend Tool";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.TextBox txtBlendRadius;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.CheckBox chkBoxAllowTangentEdge;
        }
    }