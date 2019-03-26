/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Annotations/ManagedItemTypesExample/MainForm.Designer.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
namespace ManagedItemTypesExample
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.BtnDetachAllItemTypes = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.LstAttachedItemTypes = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BtnDetachItemType = new System.Windows.Forms.Button();
            this.BtnAttachItemType = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.BtnPlaceLine = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtZ2 = new System.Windows.Forms.TextBox();
            this.TxtX2 = new System.Windows.Forms.TextBox();
            this.TxtY2 = new System.Windows.Forms.TextBox();
            this.LblCenterStart = new System.Windows.Forms.Label();
            this.TxtZ1 = new System.Windows.Forms.TextBox();
            this.TxtX1 = new System.Windows.Forms.TextBox();
            this.TxtY1 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BtnDeleteCustomType = new System.Windows.Forms.Button();
            this.BtnCreateCustomType = new System.Windows.Forms.Button();
            this.TxtCustomTypeName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LstCustomTypes = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BtnDeletePropertyDefinition = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.ChkIsCustom = new System.Windows.Forms.CheckBox();
            this.TxtSelectedType = new System.Windows.Forms.TextBox();
            this.ChkIsArray = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CmbPropertyType = new System.Windows.Forms.ComboBox();
            this.TxtSelectedLibrary = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtDefaultValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtPropertyName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnCreatePropertyDefinition = new System.Windows.Forms.Button();
            this.LstItemTypesProperties = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnDeleteItemType = new System.Windows.Forms.Button();
            this.BtnCreateItemType = new System.Windows.Forms.Button();
            this.TxtItemTypeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LstItemTypes = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnDeleteLibrary = new System.Windows.Forms.Button();
            this.BtnCreateLibrary = new System.Windows.Forms.Button();
            this.TxtLibraryName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LstItemTypesLibraries = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(899, 611);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item Types";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.BtnDetachAllItemTypes);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.LstAttachedItemTypes);
            this.groupBox6.Controls.Add(this.BtnDetachItemType);
            this.groupBox6.Controls.Add(this.BtnAttachItemType);
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Location = new System.Drawing.Point(599, 268);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(283, 328);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Attach/Detach Item Types";
            // 
            // BtnDetachAllItemTypes
            // 
            this.BtnDetachAllItemTypes.Location = new System.Drawing.Point(147, 165);
            this.BtnDetachAllItemTypes.Name = "BtnDetachAllItemTypes";
            this.BtnDetachAllItemTypes.Size = new System.Drawing.Size(124, 23);
            this.BtnDetachAllItemTypes.TabIndex = 35;
            this.BtnDetachAllItemTypes.Text = "Detach All Item Types";
            this.BtnDetachAllItemTypes.UseVisualStyleBackColor = true;
            this.BtnDetachAllItemTypes.Click += new System.EventHandler(this.BtnDetachAllItemTypes_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Attached Item Types:";
            // 
            // LstAttachedItemTypes
            // 
            this.LstAttachedItemTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.LstAttachedItemTypes.FullRowSelect = true;
            this.LstAttachedItemTypes.GridLines = true;
            this.LstAttachedItemTypes.Location = new System.Drawing.Point(15, 194);
            this.LstAttachedItemTypes.MultiSelect = false;
            this.LstAttachedItemTypes.Name = "LstAttachedItemTypes";
            this.LstAttachedItemTypes.Size = new System.Drawing.Size(256, 119);
            this.LstAttachedItemTypes.TabIndex = 33;
            this.LstAttachedItemTypes.UseCompatibleStateImageBehavior = false;
            this.LstAttachedItemTypes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "#";
            this.columnHeader5.Width = 30;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Item Type Library";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Item Type";
            this.columnHeader7.Width = 120;
            // 
            // BtnDetachItemType
            // 
            this.BtnDetachItemType.Location = new System.Drawing.Point(147, 138);
            this.BtnDetachItemType.Name = "BtnDetachItemType";
            this.BtnDetachItemType.Size = new System.Drawing.Size(124, 23);
            this.BtnDetachItemType.TabIndex = 32;
            this.BtnDetachItemType.Text = "Detach Item Type";
            this.BtnDetachItemType.UseVisualStyleBackColor = true;
            this.BtnDetachItemType.Click += new System.EventHandler(this.BtnDetachItemType_Click);
            // 
            // BtnAttachItemType
            // 
            this.BtnAttachItemType.Location = new System.Drawing.Point(15, 138);
            this.BtnAttachItemType.Name = "BtnAttachItemType";
            this.BtnAttachItemType.Size = new System.Drawing.Size(124, 23);
            this.BtnAttachItemType.TabIndex = 31;
            this.BtnAttachItemType.Text = "Attach Item Type";
            this.BtnAttachItemType.UseVisualStyleBackColor = true;
            this.BtnAttachItemType.Click += new System.EventHandler(this.BtnAttachItemType_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.BtnPlaceLine);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.TxtZ2);
            this.groupBox7.Controls.Add(this.TxtX2);
            this.groupBox7.Controls.Add(this.TxtY2);
            this.groupBox7.Controls.Add(this.LblCenterStart);
            this.groupBox7.Controls.Add(this.TxtZ1);
            this.groupBox7.Controls.Add(this.TxtX1);
            this.groupBox7.Controls.Add(this.TxtY1);
            this.groupBox7.Location = new System.Drawing.Point(15, 26);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(255, 106);
            this.groupBox7.TabIndex = 26;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Line Element";
            // 
            // BtnPlaceLine
            // 
            this.BtnPlaceLine.Location = new System.Drawing.Point(171, 74);
            this.BtnPlaceLine.Name = "BtnPlaceLine";
            this.BtnPlaceLine.Size = new System.Drawing.Size(75, 23);
            this.BtnPlaceLine.TabIndex = 30;
            this.BtnPlaceLine.Text = "Place";
            this.BtnPlaceLine.UseVisualStyleBackColor = true;
            this.BtnPlaceLine.Click += new System.EventHandler(this.BtnPlaceLine_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "End Point:";
            // 
            // TxtZ2
            // 
            this.TxtZ2.Location = new System.Drawing.Point(192, 46);
            this.TxtZ2.Name = "TxtZ2";
            this.TxtZ2.Size = new System.Drawing.Size(54, 20);
            this.TxtZ2.TabIndex = 29;
            this.TxtZ2.Tag = "";
            this.TxtZ2.Text = "0";
            // 
            // TxtX2
            // 
            this.TxtX2.Location = new System.Drawing.Point(77, 46);
            this.TxtX2.Name = "TxtX2";
            this.TxtX2.Size = new System.Drawing.Size(49, 20);
            this.TxtX2.TabIndex = 27;
            this.TxtX2.Text = "1000";
            // 
            // TxtY2
            // 
            this.TxtY2.Location = new System.Drawing.Point(132, 46);
            this.TxtY2.Name = "TxtY2";
            this.TxtY2.Size = new System.Drawing.Size(54, 20);
            this.TxtY2.TabIndex = 28;
            this.TxtY2.Tag = "";
            this.TxtY2.Text = "0";
            // 
            // LblCenterStart
            // 
            this.LblCenterStart.AutoSize = true;
            this.LblCenterStart.Location = new System.Drawing.Point(12, 23);
            this.LblCenterStart.Name = "LblCenterStart";
            this.LblCenterStart.Size = new System.Drawing.Size(59, 13);
            this.LblCenterStart.TabIndex = 22;
            this.LblCenterStart.Text = "Start Point:";
            // 
            // TxtZ1
            // 
            this.TxtZ1.Location = new System.Drawing.Point(192, 20);
            this.TxtZ1.Name = "TxtZ1";
            this.TxtZ1.Size = new System.Drawing.Size(54, 20);
            this.TxtZ1.TabIndex = 25;
            this.TxtZ1.Tag = "";
            this.TxtZ1.Text = "0";
            // 
            // TxtX1
            // 
            this.TxtX1.Location = new System.Drawing.Point(77, 20);
            this.TxtX1.Name = "TxtX1";
            this.TxtX1.Size = new System.Drawing.Size(49, 20);
            this.TxtX1.TabIndex = 23;
            this.TxtX1.Text = "0";
            // 
            // TxtY1
            // 
            this.TxtY1.Location = new System.Drawing.Point(132, 20);
            this.TxtY1.Name = "TxtY1";
            this.TxtY1.Size = new System.Drawing.Size(54, 20);
            this.TxtY1.TabIndex = 24;
            this.TxtY1.Tag = "";
            this.TxtY1.Text = "0";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BtnDeleteCustomType);
            this.groupBox5.Controls.Add(this.BtnCreateCustomType);
            this.groupBox5.Controls.Add(this.TxtCustomTypeName);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.LstCustomTypes);
            this.groupBox5.Location = new System.Drawing.Point(599, 23);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(283, 239);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Custom Types";
            // 
            // BtnDeleteCustomType
            // 
            this.BtnDeleteCustomType.Location = new System.Drawing.Point(195, 23);
            this.BtnDeleteCustomType.Name = "BtnDeleteCustomType";
            this.BtnDeleteCustomType.Size = new System.Drawing.Size(75, 23);
            this.BtnDeleteCustomType.TabIndex = 8;
            this.BtnDeleteCustomType.Text = "Delete";
            this.BtnDeleteCustomType.UseVisualStyleBackColor = true;
            this.BtnDeleteCustomType.Click += new System.EventHandler(this.BtnDeleteCustomType_Click);
            // 
            // BtnCreateCustomType
            // 
            this.BtnCreateCustomType.Location = new System.Drawing.Point(195, 173);
            this.BtnCreateCustomType.Name = "BtnCreateCustomType";
            this.BtnCreateCustomType.Size = new System.Drawing.Size(75, 23);
            this.BtnCreateCustomType.TabIndex = 7;
            this.BtnCreateCustomType.Text = "Create New";
            this.BtnCreateCustomType.UseVisualStyleBackColor = true;
            this.BtnCreateCustomType.Click += new System.EventHandler(this.BtnCreateCustomType_Click);
            // 
            // TxtCustomTypeName
            // 
            this.TxtCustomTypeName.Location = new System.Drawing.Point(59, 206);
            this.TxtCustomTypeName.Name = "TxtCustomTypeName";
            this.TxtCustomTypeName.Size = new System.Drawing.Size(211, 20);
            this.TxtCustomTypeName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Name: ";
            // 
            // LstCustomTypes
            // 
            this.LstCustomTypes.FormattingEnabled = true;
            this.LstCustomTypes.Location = new System.Drawing.Point(15, 23);
            this.LstCustomTypes.Name = "LstCustomTypes";
            this.LstCustomTypes.Size = new System.Drawing.Size(174, 173);
            this.LstCustomTypes.TabIndex = 2;
            this.LstCustomTypes.SelectedIndexChanged += new System.EventHandler(this.LstCustomTypes_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BtnDeletePropertyDefinition);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.LstItemTypesProperties);
            this.groupBox4.Location = new System.Drawing.Point(15, 268);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(575, 328);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Property Definitions";
            // 
            // BtnDeletePropertyDefinition
            // 
            this.BtnDeletePropertyDefinition.Location = new System.Drawing.Point(325, 290);
            this.BtnDeletePropertyDefinition.Name = "BtnDeletePropertyDefinition";
            this.BtnDeletePropertyDefinition.Size = new System.Drawing.Size(75, 23);
            this.BtnDeletePropertyDefinition.TabIndex = 28;
            this.BtnDeletePropertyDefinition.Text = "Delete";
            this.BtnDeletePropertyDefinition.UseVisualStyleBackColor = true;
            this.BtnDeletePropertyDefinition.Click += new System.EventHandler(this.BtnDeletePropertyDefinition_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.ChkIsCustom);
            this.groupBox8.Controls.Add(this.TxtSelectedType);
            this.groupBox8.Controls.Add(this.ChkIsArray);
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Controls.Add(this.CmbPropertyType);
            this.groupBox8.Controls.Add(this.TxtSelectedLibrary);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.TxtDefaultValue);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.TxtPropertyName);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.BtnCreatePropertyDefinition);
            this.groupBox8.Location = new System.Drawing.Point(325, 21);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(237, 240);
            this.groupBox8.TabIndex = 27;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "New Property Definition";
            // 
            // ChkIsCustom
            // 
            this.ChkIsCustom.AutoSize = true;
            this.ChkIsCustom.Enabled = false;
            this.ChkIsCustom.Location = new System.Drawing.Point(57, 77);
            this.ChkIsCustom.Name = "ChkIsCustom";
            this.ChkIsCustom.Size = new System.Drawing.Size(69, 17);
            this.ChkIsCustom.TabIndex = 39;
            this.ChkIsCustom.Text = "IsCustom";
            this.ChkIsCustom.UseVisualStyleBackColor = true;
            // 
            // TxtSelectedType
            // 
            this.TxtSelectedType.Location = new System.Drawing.Point(57, 51);
            this.TxtSelectedType.Name = "TxtSelectedType";
            this.TxtSelectedType.ReadOnly = true;
            this.TxtSelectedType.Size = new System.Drawing.Size(168, 20);
            this.TxtSelectedType.TabIndex = 38;
            // 
            // ChkIsArray
            // 
            this.ChkIsArray.AutoSize = true;
            this.ChkIsArray.Location = new System.Drawing.Point(57, 157);
            this.ChkIsArray.Name = "ChkIsArray";
            this.ChkIsArray.Size = new System.Drawing.Size(61, 17);
            this.ChkIsArray.TabIndex = 37;
            this.ChkIsArray.Text = "Is Array";
            this.ChkIsArray.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Type: ";
            // 
            // CmbPropertyType
            // 
            this.CmbPropertyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPropertyType.FormattingEnabled = true;
            this.CmbPropertyType.Items.AddRange(new object[] {
            "Text",
            "Integer",
            "Double",
            "Boolean",
            "DateTime"});
            this.CmbPropertyType.Location = new System.Drawing.Point(57, 130);
            this.CmbPropertyType.Name = "CmbPropertyType";
            this.CmbPropertyType.Size = new System.Drawing.Size(168, 21);
            this.CmbPropertyType.TabIndex = 36;
            // 
            // TxtSelectedLibrary
            // 
            this.TxtSelectedLibrary.Location = new System.Drawing.Point(57, 25);
            this.TxtSelectedLibrary.Name = "TxtSelectedLibrary";
            this.TxtSelectedLibrary.ReadOnly = true;
            this.TxtSelectedLibrary.Size = new System.Drawing.Size(168, 20);
            this.TxtSelectedLibrary.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Library: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Type: ";
            // 
            // TxtDefaultValue
            // 
            this.TxtDefaultValue.Location = new System.Drawing.Point(57, 180);
            this.TxtDefaultValue.Name = "TxtDefaultValue";
            this.TxtDefaultValue.Size = new System.Drawing.Size(168, 20);
            this.TxtDefaultValue.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Value: ";
            // 
            // TxtPropertyName
            // 
            this.TxtPropertyName.Location = new System.Drawing.Point(57, 104);
            this.TxtPropertyName.Name = "TxtPropertyName";
            this.TxtPropertyName.Size = new System.Drawing.Size(168, 20);
            this.TxtPropertyName.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Name: ";
            // 
            // BtnCreatePropertyDefinition
            // 
            this.BtnCreatePropertyDefinition.Location = new System.Drawing.Point(105, 206);
            this.BtnCreatePropertyDefinition.Name = "BtnCreatePropertyDefinition";
            this.BtnCreatePropertyDefinition.Size = new System.Drawing.Size(120, 23);
            this.BtnCreatePropertyDefinition.TabIndex = 30;
            this.BtnCreatePropertyDefinition.Text = "Create New Property";
            this.BtnCreatePropertyDefinition.UseVisualStyleBackColor = true;
            this.BtnCreatePropertyDefinition.Click += new System.EventHandler(this.BtnCreatePropertyDefinition_Click);
            // 
            // LstItemTypesProperties
            // 
            this.LstItemTypesProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.LstItemTypesProperties.FullRowSelect = true;
            this.LstItemTypesProperties.GridLines = true;
            this.LstItemTypesProperties.Location = new System.Drawing.Point(16, 26);
            this.LstItemTypesProperties.MultiSelect = false;
            this.LstItemTypesProperties.Name = "LstItemTypesProperties";
            this.LstItemTypesProperties.Size = new System.Drawing.Size(303, 287);
            this.LstItemTypesProperties.TabIndex = 4;
            this.LstItemTypesProperties.UseCompatibleStateImageBehavior = false;
            this.LstItemTypesProperties.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Is Array";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Default Value";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnDeleteItemType);
            this.groupBox3.Controls.Add(this.BtnCreateItemType);
            this.groupBox3.Controls.Add(this.TxtItemTypeName);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.LstItemTypes);
            this.groupBox3.Location = new System.Drawing.Point(307, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 239);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Item Types";
            // 
            // BtnDeleteItemType
            // 
            this.BtnDeleteItemType.Location = new System.Drawing.Point(195, 23);
            this.BtnDeleteItemType.Name = "BtnDeleteItemType";
            this.BtnDeleteItemType.Size = new System.Drawing.Size(75, 23);
            this.BtnDeleteItemType.TabIndex = 8;
            this.BtnDeleteItemType.Text = "Delete";
            this.BtnDeleteItemType.UseVisualStyleBackColor = true;
            this.BtnDeleteItemType.Click += new System.EventHandler(this.BtnDeleteItemType_Click);
            // 
            // BtnCreateItemType
            // 
            this.BtnCreateItemType.Location = new System.Drawing.Point(195, 173);
            this.BtnCreateItemType.Name = "BtnCreateItemType";
            this.BtnCreateItemType.Size = new System.Drawing.Size(75, 23);
            this.BtnCreateItemType.TabIndex = 7;
            this.BtnCreateItemType.Text = "Create New";
            this.BtnCreateItemType.UseVisualStyleBackColor = true;
            this.BtnCreateItemType.Click += new System.EventHandler(this.BtnCreateItemType_Click);
            // 
            // TxtItemTypeName
            // 
            this.TxtItemTypeName.Location = new System.Drawing.Point(59, 206);
            this.TxtItemTypeName.Name = "TxtItemTypeName";
            this.TxtItemTypeName.Size = new System.Drawing.Size(211, 20);
            this.TxtItemTypeName.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name: ";
            // 
            // LstItemTypes
            // 
            this.LstItemTypes.FormattingEnabled = true;
            this.LstItemTypes.Location = new System.Drawing.Point(15, 23);
            this.LstItemTypes.Name = "LstItemTypes";
            this.LstItemTypes.Size = new System.Drawing.Size(174, 173);
            this.LstItemTypes.TabIndex = 2;
            this.LstItemTypes.SelectedIndexChanged += new System.EventHandler(this.LstItemTypes_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnDeleteLibrary);
            this.groupBox2.Controls.Add(this.BtnCreateLibrary);
            this.groupBox2.Controls.Add(this.TxtLibraryName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.LstItemTypesLibraries);
            this.groupBox2.Location = new System.Drawing.Point(15, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 239);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Item Types Libraries";
            // 
            // BtnDeleteLibrary
            // 
            this.BtnDeleteLibrary.Location = new System.Drawing.Point(196, 23);
            this.BtnDeleteLibrary.Name = "BtnDeleteLibrary";
            this.BtnDeleteLibrary.Size = new System.Drawing.Size(75, 23);
            this.BtnDeleteLibrary.TabIndex = 4;
            this.BtnDeleteLibrary.Text = "Delete";
            this.BtnDeleteLibrary.UseVisualStyleBackColor = true;
            this.BtnDeleteLibrary.Click += new System.EventHandler(this.BtnDeleteLibrary_Click);
            // 
            // BtnCreateLibrary
            // 
            this.BtnCreateLibrary.Location = new System.Drawing.Point(196, 173);
            this.BtnCreateLibrary.Name = "BtnCreateLibrary";
            this.BtnCreateLibrary.Size = new System.Drawing.Size(75, 23);
            this.BtnCreateLibrary.TabIndex = 3;
            this.BtnCreateLibrary.Text = "Create New";
            this.BtnCreateLibrary.UseVisualStyleBackColor = true;
            this.BtnCreateLibrary.Click += new System.EventHandler(this.BtnCreateLibrary_Click);
            // 
            // TxtLibraryName
            // 
            this.TxtLibraryName.Location = new System.Drawing.Point(60, 206);
            this.TxtLibraryName.Name = "TxtLibraryName";
            this.TxtLibraryName.Size = new System.Drawing.Size(211, 20);
            this.TxtLibraryName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name: ";
            // 
            // LstItemTypesLibraries
            // 
            this.LstItemTypesLibraries.FormattingEnabled = true;
            this.LstItemTypesLibraries.Location = new System.Drawing.Point(16, 23);
            this.LstItemTypesLibraries.Name = "LstItemTypesLibraries";
            this.LstItemTypesLibraries.Size = new System.Drawing.Size(174, 173);
            this.LstItemTypesLibraries.TabIndex = 0;
            this.LstItemTypesLibraries.SelectedIndexChanged += new System.EventHandler(this.LstItemTypesLibraries_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 635);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Item Types";
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox LstItemTypesLibraries;
        private System.Windows.Forms.ListBox LstItemTypes;
        private System.Windows.Forms.ListView LstItemTypesProperties;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnCreateLibrary;
        private System.Windows.Forms.TextBox TxtLibraryName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnDeleteLibrary;
        private System.Windows.Forms.Button BtnDeleteItemType;
        private System.Windows.Forms.Button BtnCreateItemType;
        private System.Windows.Forms.TextBox TxtItemTypeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button BtnDeleteCustomType;
        private System.Windows.Forms.Button BtnCreateCustomType;
        private System.Windows.Forms.TextBox TxtCustomTypeName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox LstCustomTypes;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox TxtY1;
        private System.Windows.Forms.TextBox TxtX1;
        private System.Windows.Forms.Label LblCenterStart;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtZ2;
        private System.Windows.Forms.TextBox TxtX2;
        private System.Windows.Forms.TextBox TxtY2;
        private System.Windows.Forms.TextBox TxtZ1;
        private System.Windows.Forms.Button BtnPlaceLine;
        private System.Windows.Forms.Button BtnDetachItemType;
        private System.Windows.Forms.Button BtnAttachItemType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView LstAttachedItemTypes;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button BtnDetachAllItemTypes;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button BtnCreatePropertyDefinition;
        private System.Windows.Forms.TextBox TxtPropertyName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ChkIsArray;
        private System.Windows.Forms.ComboBox CmbPropertyType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtDefaultValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnDeletePropertyDefinition;
        private System.Windows.Forms.TextBox TxtSelectedType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TxtSelectedLibrary;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ChkIsCustom;
    }
}