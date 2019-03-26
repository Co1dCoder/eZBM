using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.EC.Persistence.Query;
using Bentley.ECObjects.Instance;
using Bentley.ECObjects.Schema;
using eZBMCE.AddinManager;
using Application = Bentley.Interop.MicroStationDGN.Application;
using BM = Bentley.MstnPlatformNET;
using ItemTypeLibrary = Bentley.DgnPlatformNET.ItemTypeLibrary;

namespace eZBMCE.Debug.EcObject
{
    /// <summary> 几何模型的信息提取与几何元素的创建 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    internal sealed class EcItemTypeTest : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"TestItemType";

        private const string CommandDescription = @"ItemType测试";
        private const string CommandText = CommandDescription;

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void TestItemType()
        {
            AddinManagerDebuger.ExecuteCommand(TestItemType);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new EcItemTypeTest();
            return AddinManagerDebuger.DebugInAddinManager(s.TestItemType, ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> <seealso cref="CommandDescription"/> </summary>
        public AddinManagerDebuger.ExternalCmdResult TestItemType(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            ECPropertyValueChangedTest tt = new ECPropertyValueChangedTest();
            tt.CreateECDInstancesOnElement(docMdf);
            return AddinManagerDebuger.ExternalCmdResult.Commit;

            EcExample.DebugFast(docMdf);
            return AddinManagerDebuger.ExternalCmdResult.Commit;

            ManipulateInstance(docMdf);
            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        public AddinManagerDebuger.ExternalCmdResult ManipulateInstance(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            docMdf.WriteMessageLineNow($"ManipulateInstance ItemType 命令开始");
            //
            // return EcExample.DebugFast(docMdf);

            ItemTypeLibrary itemtypeLib = ItemTypeLibrary.FindByName("MyItemTypeLibrary", docMdf.DgnFile);
            var myItemType = itemtypeLib.GetItemTypeByName("MyItemType");
            var customPropType = itemtypeLib.GetCustomTypeByName("MyPropertyType");

            //
            long circleIdD = 1566;
            var circleId = new ElementId(ref circleIdD);
            var circle = docMdf.DgnModel.FindElementById(circleId);
            CustomItemHost host = new CustomItemHost(circle, scheduleItemsOnElement: false);

            // Attatch the item type to the specified element.
            IDgnECInstance instance = host.GetCustomItem("MyItemTypeLibrary", "MyItemType");
            Bentley.ECObjects.Schema.IECClass ecClass = instance.ClassDefinition;
            Bentley.ECObjects.Schema.ECClass ss;
            const string propertyName = "MyPropertyDefinition__x0020__11"; // 这是一个CustomPropertyType类型的字段
            var prop = ecClass.FindLocalProperty(propertyName);
            var propV = instance.GetPropertyValue(propertyName);
            IECType tp = prop.Type;  // 类型为 Bentley.ECObjects.Schema.ECClass
            var value = propV.NativeValue; // 类型为  // Bentley.ECObjects.Instance.ECDStructValue
            //
            docMdf.WriteMessageLineNow(prop.Type.GetType().FullName, propV.Type.Name, propV.NativeValue, propV.NativeValue.GetType().FullName);
            instance.WriteChanges();

            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        private void StandaloneinstanceOnEcPropertyValueChanged(object sender, ECPropertyValueChangedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private static StringBuilder PopulateInstances(DgnECInstanceCollection ecInstances)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IDgnECInstance instance in ecInstances)
            {
                IECClass eCClass = instance.ClassDefinition;
                sb.AppendLine($"IDgnECInstance 的Element：{instance.Element.ElementId}, 名称：{eCClass.Name}");
                foreach (IECProperty prop in instance.ClassDefinition.Properties(false))
                {
                    sb.AppendLine($"IECProperty 的名称：{prop.Name}");
                    IECPropertyValue propValue = instance.GetPropertyValue(prop.Name);

                    var isArray = propValue.IsArray;
                    string type = prop.Type.Name.ToLower();
                    switch (type)
                    {
                        case "string":
                            sb.AppendLine($"IECProperty 的字符值：{propValue.StringValue}");
                            break;
                        case "boolean":
                            sb.AppendLine($"IECProperty 的布尔值：{propValue.StringValue}");
                            break;
                        case "int":
                            sb.AppendLine($"IECProperty 的整数值：{propValue.IntValue}");
                            break;
                        case "double":
                            sb.AppendLine($"IECProperty 的浮点值：{propValue.DoubleValue}");
                            break;
                    }
                }
            }
            return sb;
        }

        public object GetPropertyValue(IECPropertyValue propValue)
        {
            object value = null;
            var isArray = propValue.IsArray;
            string type = propValue.Property.Type.Name.ToLower();
            switch (type)
            {
                case "string":
                    propValue.StringValue = "abc";
                    propValue.NativeValue = "bcd";
                    value = propValue.StringValue;
                    break;
                case "boolean":
                    value = propValue.StringValue;
                    break;
                case "int":
                    value = propValue.IntValue;
                    break;
                case "double":
                    value = propValue.DoubleValue;
                    break;
                case "datetime":
                    value = (DateTime)propValue.NativeValue;
                    break;
            }
            return value;
        }

        public AddinManagerDebuger.ExternalCmdResult AttatchItemTypes(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            docMdf.WriteMessageLineNow($"测试 ItemType 命令开始");
            //

            ItemTypeLibrary itemtypeLib = ItemTypeLibrary.FindByName("MyItemTypeLibrary", docMdf.DgnFile);
            var myItemType = itemtypeLib.GetItemTypeByName("MyItemType");
            var customPropType = itemtypeLib.GetCustomTypeByName("MyPropertyType");
            //
            long circleIdD = 1558;
            var circleId = new ElementId(ref circleIdD);
            var circle = docMdf.DgnModel.FindElementById(circleId);
            CustomItemHost host = new CustomItemHost(circle, scheduleItemsOnElement: false);

            // Attatch the item type to the specified element.
            IDgnECInstance instance = host.ApplyCustomItem(myItemType);
            // Bentley.ECObjects.Schema.IECClass ecClass = instance.ClassDefinition;
            instance.ECPropertyValueChanged += InstanceOnEcPropertyValueChanged;

            // docMdf.WriteMessageLineNow(itemType.IsCustomType, itemType.ECClass, itemType.Guid, "名称", ecClass.Name);
            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        private void InstanceOnEcPropertyValueChanged(object sender, ECPropertyValueChangedEventArgs args)
        {
            MessageBox.Show("InstanceOnEcPropertyValueChanged Event Invoked.");
        }
    }
}