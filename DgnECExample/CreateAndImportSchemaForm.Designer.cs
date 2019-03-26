/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/DgnEC/DgnECExample/CreateAndImportSchemaForm.Designer.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
namespace DgnECExample
{
    partial class CreateAndImportSchemaForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TSchema = new System.Windows.Forms.TreeView();
            this.BtnCreateAndImportSchema = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.GrpProperties = new System.Windows.Forms.GroupBox();
            this.CmbType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnAddProperty = new System.Windows.Forms.Button();
            this.TxtPropertyName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GrpClasses = new System.Windows.Forms.GroupBox();
            this.BtnAddClass = new System.Windows.Forms.Button();
            this.TxtClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GrpSchema = new System.Windows.Forms.GroupBox();
            this.BtnCreateSchema = new System.Windows.Forms.Button();
            this.TxtSchemaName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.GrpProperties.SuspendLayout();
            this.GrpClasses.SuspendLayout();
            this.GrpSchema.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TSchema);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.BtnCreateAndImportSchema);
            this.splitContainer1.Panel2.Controls.Add(this.BtnClear);
            this.splitContainer1.Panel2.Controls.Add(this.BtnClose);
            this.splitContainer1.Panel2.Controls.Add(this.GrpProperties);
            this.splitContainer1.Panel2.Controls.Add(this.GrpClasses);
            this.splitContainer1.Panel2.Controls.Add(this.GrpSchema);
            this.splitContainer1.Size = new System.Drawing.Size(754, 559);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 1;
            // 
            // TSchema
            // 
            this.TSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TSchema.Location = new System.Drawing.Point(0, 0);
            this.TSchema.Name = "TSchema";
            this.TSchema.Size = new System.Drawing.Size(250, 559);
            this.TSchema.TabIndex = 1;
            // 
            // BtnCreateAndImportSchema
            // 
            this.BtnCreateAndImportSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCreateAndImportSchema.Location = new System.Drawing.Point(123, 518);
            this.BtnCreateAndImportSchema.Name = "BtnCreateAndImportSchema";
            this.BtnCreateAndImportSchema.Size = new System.Drawing.Size(203, 29);
            this.BtnCreateAndImportSchema.TabIndex = 5;
            this.BtnCreateAndImportSchema.Text = "Create And Import Schema";
            this.BtnCreateAndImportSchema.UseVisualStyleBackColor = true;
            this.BtnCreateAndImportSchema.Click += new System.EventHandler(this.BtnCreateAndImportSchema_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClear.Location = new System.Drawing.Point(332, 518);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 29);
            this.BtnClear.TabIndex = 4;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.Location = new System.Drawing.Point(413, 518);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 29);
            this.BtnClose.TabIndex = 3;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // GrpProperties
            // 
            this.GrpProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpProperties.Controls.Add(this.CmbType);
            this.GrpProperties.Controls.Add(this.label4);
            this.GrpProperties.Controls.Add(this.BtnAddProperty);
            this.GrpProperties.Controls.Add(this.TxtPropertyName);
            this.GrpProperties.Controls.Add(this.label3);
            this.GrpProperties.Enabled = false;
            this.GrpProperties.Location = new System.Drawing.Point(17, 188);
            this.GrpProperties.Name = "GrpProperties";
            this.GrpProperties.Size = new System.Drawing.Size(471, 109);
            this.GrpProperties.TabIndex = 2;
            this.GrpProperties.TabStop = false;
            this.GrpProperties.Text = "Properties";
            // 
            // CmbType
            // 
            this.CmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbType.FormattingEnabled = true;
            this.CmbType.Items.AddRange(new object[] {
            "String",
            "Boolean",
            "Integer",
            "Double"});
            this.CmbType.Location = new System.Drawing.Point(73, 64);
            this.CmbType.Name = "CmbType";
            this.CmbType.Size = new System.Drawing.Size(303, 24);
            this.CmbType.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Type:";
            // 
            // BtnAddProperty
            // 
            this.BtnAddProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddProperty.Location = new System.Drawing.Point(382, 29);
            this.BtnAddProperty.Name = "BtnAddProperty";
            this.BtnAddProperty.Size = new System.Drawing.Size(75, 29);
            this.BtnAddProperty.TabIndex = 2;
            this.BtnAddProperty.Text = "Create";
            this.BtnAddProperty.UseVisualStyleBackColor = true;
            this.BtnAddProperty.Click += new System.EventHandler(this.BtnAddProperty_Click);
            // 
            // TxtPropertyName
            // 
            this.TxtPropertyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPropertyName.Location = new System.Drawing.Point(73, 32);
            this.TxtPropertyName.Name = "TxtPropertyName";
            this.TxtPropertyName.Size = new System.Drawing.Size(303, 22);
            this.TxtPropertyName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            // 
            // GrpClasses
            // 
            this.GrpClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpClasses.Controls.Add(this.BtnAddClass);
            this.GrpClasses.Controls.Add(this.TxtClassName);
            this.GrpClasses.Controls.Add(this.label2);
            this.GrpClasses.Enabled = false;
            this.GrpClasses.Location = new System.Drawing.Point(17, 100);
            this.GrpClasses.Name = "GrpClasses";
            this.GrpClasses.Size = new System.Drawing.Size(471, 82);
            this.GrpClasses.TabIndex = 1;
            this.GrpClasses.TabStop = false;
            this.GrpClasses.Text = "Classes";
            // 
            // BtnAddClass
            // 
            this.BtnAddClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddClass.Location = new System.Drawing.Point(382, 29);
            this.BtnAddClass.Name = "BtnAddClass";
            this.BtnAddClass.Size = new System.Drawing.Size(75, 29);
            this.BtnAddClass.TabIndex = 2;
            this.BtnAddClass.Text = "Create";
            this.BtnAddClass.UseVisualStyleBackColor = true;
            this.BtnAddClass.Click += new System.EventHandler(this.BtnAddClass_Click);
            // 
            // TxtClassName
            // 
            this.TxtClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtClassName.Location = new System.Drawing.Point(73, 32);
            this.TxtClassName.Name = "TxtClassName";
            this.TxtClassName.Size = new System.Drawing.Size(303, 22);
            this.TxtClassName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // GrpSchema
            // 
            this.GrpSchema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpSchema.Controls.Add(this.BtnCreateSchema);
            this.GrpSchema.Controls.Add(this.TxtSchemaName);
            this.GrpSchema.Controls.Add(this.label1);
            this.GrpSchema.Location = new System.Drawing.Point(17, 12);
            this.GrpSchema.Name = "GrpSchema";
            this.GrpSchema.Size = new System.Drawing.Size(471, 82);
            this.GrpSchema.TabIndex = 0;
            this.GrpSchema.TabStop = false;
            this.GrpSchema.Text = "Schema";
            // 
            // BtnCreateSchema
            // 
            this.BtnCreateSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCreateSchema.Location = new System.Drawing.Point(382, 29);
            this.BtnCreateSchema.Name = "BtnCreateSchema";
            this.BtnCreateSchema.Size = new System.Drawing.Size(75, 29);
            this.BtnCreateSchema.TabIndex = 2;
            this.BtnCreateSchema.Text = "Create";
            this.BtnCreateSchema.UseVisualStyleBackColor = true;
            this.BtnCreateSchema.Click += new System.EventHandler(this.BtnCreateSchema_Click);
            // 
            // TxtSchemaName
            // 
            this.TxtSchemaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSchemaName.Location = new System.Drawing.Point(73, 32);
            this.TxtSchemaName.Name = "TxtSchemaName";
            this.TxtSchemaName.Size = new System.Drawing.Size(303, 22);
            this.TxtSchemaName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // CreateAndImportSchemaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 559);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CreateAndImportSchemaForm";
            this.Text = "Create And Import Schema";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.GrpProperties.ResumeLayout(false);
            this.GrpProperties.PerformLayout();
            this.GrpClasses.ResumeLayout(false);
            this.GrpClasses.PerformLayout();
            this.GrpSchema.ResumeLayout(false);
            this.GrpSchema.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView TSchema;
        private System.Windows.Forms.GroupBox GrpProperties;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnAddProperty;
        private System.Windows.Forms.TextBox TxtPropertyName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox GrpClasses;
        private System.Windows.Forms.Button BtnAddClass;
        private System.Windows.Forms.TextBox TxtClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox GrpSchema;
        private System.Windows.Forms.Button BtnCreateSchema;
        private System.Windows.Forms.TextBox TxtSchemaName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCreateAndImportSchema;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.ComboBox CmbType;
    }
}