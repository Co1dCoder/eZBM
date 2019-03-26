/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Annotations/ManagedItemTypesExample/ItemTypesHelper.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using Bentley.DgnPlatformNET;

namespace ManagedItemTypesExample
{
    /*=================================================================================**/ /**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/

    class ItemTypesHelper
    {
        /*---------------------------------------------------------------------------------**/ /**
        * Get ItemTypeLibrary by name.
/// ItemTypeLibrary implements IDisposable. The caller of this method is responsible for disposing this object.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static ItemTypeLibrary GetItemTypeLibarayByName(string libraryName)
        {
            ItemTypeLibrary library = ItemTypeLibrary.FindByName(libraryName, ManagedItemTypesExample.GetActiveDgnFile());

            return library;
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Creates New ItemTypeLibrary.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static bool CreateNewLibrary(string name, out string createdName)
        {
            createdName = name;

            using (ItemTypeLibrary library = ItemTypeLibrary.Create(name, ManagedItemTypesExample.GetActiveDgnFile()))
            {
                if (null == library)
                    return false;

                createdName = library.Name;

                return library.Write();
            }
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Deletes ItemTypeLibrary.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static bool DeleteLibrary(string name)
        {
            using (
                ItemTypeLibrary library = ItemTypeLibrary.FindByName(name, ManagedItemTypesExample.GetActiveDgnFile()))
            {
                return null != library && SchemaDeleteStatus.Success == library.Delete() && library.Write();
            }
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Creates New ItemType.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static bool CreateNewItemType(string libraryName, string itemTypeName, out string createdName,
            out uint createdId)
        {
            createdName = "";
            createdId = 0;
            using (
                ItemTypeLibrary library = ItemTypeLibrary.FindByName(libraryName,
                    ManagedItemTypesExample.GetActiveDgnFile()))
            {
                ItemType itemType = (null != library) ? library.AddItemType(itemTypeName) : null;

                if (null != itemType)
                {
                    createdName = itemType.Name;
                    createdId = itemType.Id;
                }

                return library.Write();
            }
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Deletes ItemType.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static bool DeleteItemType(string libraryName, uint itemTypeId)
        {
            using (
                ItemTypeLibrary library = ItemTypeLibrary.FindByName(libraryName,
                    ManagedItemTypesExample.GetActiveDgnFile()))
            {
                ItemType itemType = (null != library) ? library.GetItemTypeById(itemTypeId) : null;
                return null != itemType && library.RemoveItemType(itemType) && library.Write();
            }
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Creates New CustomPropertyType.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static bool CreateNewCustomPropertyType(string libraryName, string customTypeName, out string createdName,
            out uint createdId)
        {
            createdName = "";
            createdId = 0;
            using (
                ItemTypeLibrary library = ItemTypeLibrary.FindByName(libraryName,
                    ManagedItemTypesExample.GetActiveDgnFile()))
            {
                CustomPropertyType customType = (null != library) ? library.AddCustomType(customTypeName) : null;

                if (null != customType)
                {
                    createdName = customType.Name;
                    createdId = customType.Id;
                }

                return library.Write();
            }
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Deletes CustomPropertyType.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static bool DeleteCustomPropertyType(string libraryName, uint customTypeId)
        {
            using (
                ItemTypeLibrary library = ItemTypeLibrary.FindByName(libraryName,
                    ManagedItemTypesExample.GetActiveDgnFile()))
            {
                CustomPropertyType customType = (null != library) ? library.GetCustomTypeById(customTypeId) : null;

                return null != customType && library.RemoveCustomType(customType) && library.Write();
            }
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Creates a new property definition for an ItemType or CustomType.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        private static CustomProperty CreateNewPropertyDefinition(ItemTypeLibrary library,
            CustomPropertyContainer propertyContainer, string propertyName, CustomProperty.TypeKind type, bool isArray,
            object value)
        {
            CustomProperty customProperty = propertyContainer.AddProperty(propertyName);

            if (null != customProperty)
            {
                customProperty.Type = type;
                customProperty.DefaultValue = value;
                customProperty.IsArray = isArray;
            }

            return (library.Write()) ? customProperty : null;
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Creates a new property definition for an ItemType.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static CustomProperty CreateNewItemTypePropertyDefinition(ItemTypeLibrary library, uint itemTypeId,
            string propertyName, CustomProperty.TypeKind type, bool isArray, object value)
        {
            ItemType itemType = (null != library) ? library.GetItemTypeById(itemTypeId) : null;
            return (null != itemType)
                ? CreateNewPropertyDefinition(library, itemType, propertyName, type, isArray, value)
                : null;
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Creates a new property definition for a Custom Type.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static CustomProperty CreateNewCustomTypePropertyDefinition(ItemTypeLibrary library, uint customTypeId,
            string propertyName, CustomProperty.TypeKind type, bool isArray, object value)
        {
            CustomPropertyType customType = (null != library) ? library.GetCustomTypeById(customTypeId) : null;
            return (null != customType)
                ? CreateNewPropertyDefinition(library, customType, propertyName, type, isArray, value)
                : null;
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Deletes the selected property definition from an ItemType or a CustomType.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        private static bool DeletePropertyDefinition(ItemTypeLibrary library, CustomPropertyContainer propertyContainer,
            string propertyName)
        {
            CustomProperty customProperty = propertyContainer.GetPropertyByName(propertyName);
            return null != customProperty && propertyContainer.RemoveProperty(customProperty) && library.Write();
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Deletes the selected property definition from an ItemType.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static bool DeleteItemTypePropertyDefinition(string libraryName, uint itemTypeId, string propertyName)
        {
            using (ItemTypeLibrary library = ItemTypeLibrary.FindByName(libraryName,ManagedItemTypesExample.GetActiveDgnFile()))
            {
                ItemType itemType = (null != library) ? library.GetItemTypeById(itemTypeId) : null;
                return DeletePropertyDefinition(library, itemType, propertyName);
            }
        }

        /*---------------------------------------------------------------------------------**/ /**
        * Deletes the selected property definition from a Custom Type.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/

        public static bool DeleteCustomTypePropertyDefinition(string libraryName, uint customTypeId, string propertyName)
        {
            using (
                ItemTypeLibrary library = ItemTypeLibrary.FindByName(libraryName,
                    ManagedItemTypesExample.GetActiveDgnFile()))
            {
                CustomPropertyType customType = (null != library) ? library.GetCustomTypeById(customTypeId) : null;
                return DeletePropertyDefinition(library, customType, propertyName);
            }
        }
    }
}