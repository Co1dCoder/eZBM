/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Annotations/ManagedItemTypesExample/MainForm.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;

using Bentley.DgnPlatformNET;
using Bentley.MstnPlatformNET;
using Bentley.GeometryNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.DgnPlatformNET.DgnEC;

namespace ManagedItemTypesExample
{

    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    public partial class MainForm : Form
    {

        private LineElement m_lineElement;

        private CustomItemHost m_itemHost;

        /*---------------------------------------------------------------------------------**//**
        * Constructor.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public MainForm()
        {
            InitializeComponent();

            LstItemTypes.DisplayMember = "ItemTypeName";
            LstItemTypes.ValueMember = "ItemTypeId";

            LstCustomTypes.DisplayMember = "ItemTypeName";
            LstCustomTypes.ValueMember = "ItemTypeId";

            m_itemHost = null;

            CmbPropertyType.SelectedIndex = 0;

            LoadItemTypes();
        }

        /*---------------------------------------------------------------------------------**//**
        * Loads all ItemType Libararies from file.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void LoadItemTypes()
        {
            IList<ItemTypeLibrary> itemTypes = ItemTypeLibrary.PopulateListFromFile(ManagedItemTypesExample.GetActiveDgnFile());

            for (int i = 0; i < itemTypes.Count; i++)
            {
                using (ItemTypeLibrary library = itemTypes[i])
                {
                    LstItemTypesLibraries.Items.Add(library.Name);
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Reads all Item Types from the selected ItemTypeLibarary and lists them in the Item Types List.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void LstItemTypesLibraries_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (null != LstItemTypesLibraries.SelectedItem)
            {
                LstItemTypes.Items.Clear();
                LstCustomTypes.Items.Clear();
                LstItemTypesProperties.Items.Clear();

                string libraryName = LstItemTypesLibraries.SelectedItem.ToString();

                using (ItemTypeLibrary library = ItemTypesHelper.GetItemTypeLibarayByName(libraryName))
                {
                    TxtSelectedLibrary.Text = libraryName;

                    if (null != library)
                    {
                        foreach (ItemType type in library.ItemTypes)
                        {
                           
                            ListBoxItemType item = new ListBoxItemType()
                            {
                                ItemTypeName = type.Name,
                                ItemTypeId = type.Id
                            };

                            LstItemTypes.Items.Add(item);
                        }

                        foreach (CustomPropertyType type in library.CustomTypes)
                        {
                            ListBoxItemType item = new ListBoxItemType()
                            {
                                ItemTypeName = type.Name,
                                ItemTypeId = type.Id
                            };

                            LstCustomTypes.Items.Add(item);
                        }
                    }
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Reads all Property Definitions from the selected ItemType and lists them in the Property Definitions List.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void LstItemTypes_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (null != LstItemTypesLibraries.SelectedItem && null != LstItemTypes.SelectedItem)
            {
                string libraryName = LstItemTypesLibraries.SelectedItem.ToString();

                ListBoxItemType listBoxItem = (ListBoxItemType)LstItemTypes.SelectedItem;
                uint itemTypeId = listBoxItem.ItemTypeId;

                using (ItemTypeLibrary library = ItemTypesHelper.GetItemTypeLibarayByName(libraryName))
                {
                    ItemType itemType = library.GetItemTypeById(itemTypeId);
                    ListPropertyDefinitionsForType(itemType);
                }

                ChkIsCustom.Checked = false;
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Reads all Property Definitions from the selected CustomPropertyType and lists them in the Property Definitions List.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void LstCustomTypes_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (null != LstItemTypesLibraries.SelectedItem && null != LstCustomTypes.SelectedItem)
            {
                string libraryName = LstItemTypesLibraries.SelectedItem.ToString();

                ListBoxItemType listBoxItem = (ListBoxItemType)LstCustomTypes.SelectedItem;
                uint customTypeId = listBoxItem.ItemTypeId;

                using (ItemTypeLibrary library = ItemTypesHelper.GetItemTypeLibarayByName(libraryName))
                {
                    CustomPropertyType customType = library.GetCustomTypeById(customTypeId);
                    ListPropertyDefinitionsForType(customType);
                }

                ChkIsCustom.Checked = true;
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Reads all Property Definitions from the selected CustomPropertyType/ItemType and lists them in the Property Definitions List.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void ListPropertyDefinitionsForType(CustomPropertyContainer propertyContainer)
        {

            if (null == propertyContainer)
                return;

            TxtSelectedType.Tag = propertyContainer.Id;
            TxtSelectedType.Text = propertyContainer.Name;

            LstItemTypesProperties.Items.Clear();

            foreach (CustomProperty property in propertyContainer)
            {
                ListViewItem item = new ListViewItem(property.Name);
                item.SubItems.Add(property.Type.ToString());
                item.SubItems.Add(property.IsArray.ToString());
                item.SubItems.Add((null != property.DefaultValue) ? property.DefaultValue.ToString() : "");
                LstItemTypesProperties.Items.Add(item);
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Creates a new ItemTypeLibrary and writes it to file.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnCreateLibrary_Click(object sender, System.EventArgs e)
        {
            string libraryName = TxtLibraryName.Text;

            string createdName = null;
            if (ItemTypesHelper.CreateNewLibrary(libraryName, out createdName))
                LstItemTypesLibraries.Items.Add(createdName);
        }

        /*---------------------------------------------------------------------------------**//**
        * Deletes the selected ItemTypeLibrary.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnDeleteLibrary_Click(object sender, System.EventArgs e)
        {
            if (0 != LstItemTypesLibraries.SelectedItems.Count)
            {
                if (ItemTypesHelper.DeleteLibrary(LstItemTypesLibraries.SelectedItem.ToString()))
                    LstItemTypesLibraries.Items.RemoveAt(LstItemTypesLibraries.SelectedIndex);
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Creates a new ItemType in the selected ItemTypeLibrary.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnCreateItemType_Click(object sender, System.EventArgs e)
        {
            if (0 != LstItemTypesLibraries.SelectedItems.Count)
            {
                string libraryName = LstItemTypesLibraries.SelectedItem.ToString();
                string itemTypeName = TxtItemTypeName.Text;

                string createdName;
                uint createdId;
                if (ItemTypesHelper.CreateNewItemType(libraryName, itemTypeName, out createdName, out createdId))
                {
                    ListBoxItemType item = new ListBoxItemType()
                    {
                        ItemTypeName = createdName,
                        ItemTypeId = createdId
                    };

                    LstItemTypes.Items.Add(item);
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Deletes the selected ItemType from the selected ItemTypeLibrary.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnDeleteItemType_Click(object sender, System.EventArgs e)
        {
            if (0 != LstItemTypesLibraries.SelectedItems.Count && 0 != LstItemTypes.SelectedItems.Count)
            {
                string libraryName = LstItemTypesLibraries.SelectedItem.ToString();

                ListBoxItemType listBoxItem = (ListBoxItemType)LstItemTypes.SelectedItem;
                uint itemTypeId = listBoxItem.ItemTypeId;

                if (ItemTypesHelper.DeleteItemType(libraryName, itemTypeId))
                    LstItemTypes.Items.RemoveAt(LstItemTypes.SelectedIndex);
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Creates a new CustomType in the selected ItemTypeLibrary.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnCreateCustomType_Click(object sender, System.EventArgs e)
        {
            if (0 != LstItemTypesLibraries.SelectedItems.Count)
            {
                string libraryName = LstItemTypesLibraries.SelectedItem.ToString();
                string customTypeName = TxtCustomTypeName.Text;

                string createdName;
                uint createdId;
                if (ItemTypesHelper.CreateNewCustomPropertyType(libraryName, customTypeName, out createdName, out createdId))
                {
                    ListBoxItemType item = new ListBoxItemType()
                    {
                        ItemTypeName = createdName,
                        ItemTypeId = createdId
                    };

                    LstCustomTypes.Items.Add(item);
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Deletes the selected CusotmType from the selected ItemTypeLibrary.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnDeleteCustomType_Click(object sender, System.EventArgs e)
        {
            if (0 != LstItemTypesLibraries.SelectedItems.Count && 0 != LstCustomTypes.SelectedItems.Count)
            {
                string libraryName = LstItemTypesLibraries.SelectedItem.ToString();

                ListBoxItemType listBoxItem = (ListBoxItemType)LstCustomTypes.SelectedItem;
                uint customTypeId = listBoxItem.ItemTypeId;

                if (ItemTypesHelper.DeleteCustomPropertyType(libraryName, customTypeId))
                    LstCustomTypes.Items.RemoveAt(LstCustomTypes.SelectedIndex);
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Places a line element for attaching/detaching ItemTypes.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnPlaceLine_Click(object sender, System.EventArgs e)
        {
            try
            {
                double x1 = double.Parse(TxtX1.Text);
                double y1 = double.Parse(TxtY1.Text);
                double z1 = double.Parse(TxtZ1.Text);
                double x2 = double.Parse(TxtX2.Text);
                double y2 = double.Parse(TxtY2.Text);
                double z2 = double.Parse(TxtZ2.Text);

                DSegment3d segment = new DSegment3d(x1, y1, z1, x2, y2, z2);

                m_lineElement = new LineElement(ManagedItemTypesExample.GetActiveDgnModel(), null, segment);
                m_lineElement.AddToModel();

                m_itemHost = null;
                LstAttachedItemTypes.Items.Clear();

                Bentley.MstnPlatformNET.Session.Instance.Keyin("FIT VIEW EXTENDED");
            }
            catch
            {

            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Attaches the selected item type from the line element.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnAttachItemType_Click(object sender, System.EventArgs e)
        {
            if (null == m_lineElement)
            {
                MessageBox.Show("Line element is not placed. Please place line element.", "MicroStation SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (0 != LstItemTypesLibraries.SelectedItems.Count && 0 != LstItemTypes.SelectedItems.Count)
            {
                string libraryName = LstItemTypesLibraries.SelectedItem.ToString();

                ListBoxItemType listBoxItem = (ListBoxItemType)LstItemTypes.SelectedItem;
                uint itemTypeId = listBoxItem.ItemTypeId;

                using (ItemTypeLibrary library = ItemTypesHelper.GetItemTypeLibarayByName(libraryName))
                {
                    ItemType itemType = library.GetItemTypeById(itemTypeId);

                    if (null != itemType)
                    {
                        if (null == m_itemHost)
                            m_itemHost = new CustomItemHost(m_lineElement, false);

                        IDgnECInstance appliedItem = m_itemHost.ApplyCustomItem(itemType);

                        if (null != appliedItem)
                            AddAttachedItemTypeToList(libraryName, itemType.Name);
                    }
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Adds attached ItemType to the list of attached ItemTypes.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void AddAttachedItemTypeToList(string libraryName, string itemTypeName)
        {
            ListViewItem item = new ListViewItem((LstAttachedItemTypes.Items.Count + 1).ToString());
            item.SubItems.Add(libraryName);
            item.SubItems.Add(itemTypeName);
            LstAttachedItemTypes.Items.Add(item);
        }

        /*---------------------------------------------------------------------------------**//**
        * Detaches selected item type from the line element.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnDetachItemType_Click(object sender, System.EventArgs e)
        {
            if (0 != LstAttachedItemTypes.Items.Count)
            {
                ListViewItem item = LstAttachedItemTypes.SelectedItems[0];
                string libraryName = item.SubItems[1].Text;
                string itemTypeName = item.SubItems[2].Text;
                if (null != m_itemHost)
                {
                    IDgnECInstance instance = m_itemHost.GetCustomItem(libraryName, itemTypeName);
                    if (DeleteDgnECInstanceStatus.Success == instance.Delete())
                        LstAttachedItemTypes.Items.RemoveAt(item.Index);
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Detaches all item types from the line element.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnDetachAllItemTypes_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in LstAttachedItemTypes.Items)
            {
                string libraryName = item.SubItems[1].Text;
                string itemTypeName = item.SubItems[2].Text;
                if (null != m_itemHost)
                {
                    IDgnECInstance instance = m_itemHost.GetCustomItem(libraryName, itemTypeName);
                    if (DeleteDgnECInstanceStatus.Success == instance.Delete())
                        LstAttachedItemTypes.Items.RemoveAt(item.Index);
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Creates a new property definition and adds it to the selected Type.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void CreatePropertyDefinition(uint typeId, bool isCustom)
        {
            CustomProperty customProperty = null;
            if (0 != LstItemTypesLibraries.SelectedItems.Count)
            {
                string libraryName = TxtSelectedLibrary.Text;
                string propertyName = TxtPropertyName.Text;
                CustomProperty.TypeKind type = GetTypeKind();
                bool isArray = ChkIsArray.Checked;
                object value = TxtDefaultValue.Text;

                using (ItemTypeLibrary library = ItemTypeLibrary.FindByName(libraryName, ManagedItemTypesExample.GetActiveDgnFile()))
                {
                    if (isCustom)
                    {
                        customProperty = ItemTypesHelper.CreateNewCustomTypePropertyDefinition(library, typeId, propertyName, type, isArray, value);
                    }
                    else
                    {
                        customProperty = ItemTypesHelper.CreateNewItemTypePropertyDefinition(library, typeId, propertyName, type, isArray, value);
                    }
                    AddCustomPropertyDefinitionToList(customProperty);
                }
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Adds custom property to the list view.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void AddCustomPropertyDefinitionToList(CustomProperty customProperty)
        {
            if (null != customProperty)
            {
                ListViewItem item = new ListViewItem(customProperty.Name);
                item.SubItems.Add(customProperty.Type.ToString());
                item.SubItems.Add(customProperty.IsArray.ToString());
                item.SubItems.Add((null != customProperty.DefaultValue) ? customProperty.DefaultValue.ToString() : "");
                LstItemTypesProperties.Items.Add(item);
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Returns TypeKind based on ComboBox selection.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private CustomProperty.TypeKind GetTypeKind()
        {
            switch (CmbPropertyType.SelectedIndex)
            {
                case 0:
                    return CustomProperty.TypeKind.String;
                case 1:
                    return CustomProperty.TypeKind.Integer;
                case 2:
                    return CustomProperty.TypeKind.Double;
                case 3:
                    return CustomProperty.TypeKind.Boolean;
                case 4:
                    return CustomProperty.TypeKind.DateTime;
            }

            return CustomProperty.TypeKind.String;
        }

        /*---------------------------------------------------------------------------------**//**
        * Creates a new property definition and adds it to the selected Type.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnCreatePropertyDefinition_Click(object sender, System.EventArgs e)
        {
            uint typeId = (uint)TxtSelectedType.Tag;
            CreatePropertyDefinition(typeId, ChkIsCustom.Checked);
        }

        /*---------------------------------------------------------------------------------**//**
        * Deletes the selected property definition from the selected Type.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void DeletePropertyDefinition(uint typeId, bool isCustom)
        {
            if (0 != LstItemTypesProperties.SelectedItems.Count)
            {
                string libraryName = TxtSelectedLibrary.Text;
                string propertyName = LstItemTypesProperties.SelectedItems[0].SubItems[0].Text;

                bool isProopertyDeleted = false;

                if (isCustom)
                    isProopertyDeleted = ItemTypesHelper.DeleteCustomTypePropertyDefinition(libraryName, typeId, propertyName);
                else
                    isProopertyDeleted = ItemTypesHelper.DeleteItemTypePropertyDefinition(libraryName, typeId, propertyName);

                if (isProopertyDeleted)
                    LstItemTypesProperties.Items.RemoveAt(LstItemTypesProperties.SelectedIndices[0]);
            }
        }

        /*---------------------------------------------------------------------------------**//**
        * Deletes the selected property definition from the selected Type.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        private void BtnDeletePropertyDefinition_Click(object sender, System.EventArgs e)
        {
            uint typeId = (uint)TxtSelectedType.Tag;
            DeletePropertyDefinition(typeId, ChkIsCustom.Checked);
        }
    }

    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    public class ListBoxItemType
    {
        public string ItemTypeName { get; set; }
        public uint ItemTypeId { get; set; }
    }

}

