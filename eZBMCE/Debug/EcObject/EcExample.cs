using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.DgnPlatformNET.Elements;
using Bentley.EC.Persistence.Query;
using Bentley.ECObjects.Instance;
using Bentley.ECObjects.Schema;
using eZBMCE.AddinManager;

namespace eZBMCE.Debug.EcObject
{
    class EcExample
    {
        /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
        public static AddinManagerDebuger.ExternalCmdResult DebugFast(DocumentModifier docMdf)
        {
            // Start Test here
            Bentley.DgnPlatformNET.DgnEC.DgnECManager manager = DgnECManager.Manager;
            var model = docMdf.DgnModel;
            var dgnFile = model.GetDgnFile();

            const string namespacePrefix = "DgnECExample";
            const string schemaName = "ZengSchema1";
            const string className = "ZengClass";
            const string propertyName = "ZengProperty";
            const string propertyValue = "ZengPropertyValue";

            // 1、 Create Schemas
            Bentley.ECObjects.Schema.ECSchema eCSchema = new Bentley.ECObjects.Schema.ECSchema(schemaName,
                versionMajor: 1, versionMinor: 0, namespacePrefix: namespacePrefix);
            ECClass eCClass = new ECClass(className);
            ECProperty eCProp = new ECProperty(propertyName, Bentley.ECObjects.ECObjects.StringType);
            eCClass.Add(eCProp);
            eCSchema.AddClass(eCClass);

            // 2、 define the schema into a dgnFile
            var importSchemaOptions = new ImportSchemaOptions();
            // In default: isExternal: false, importReferencedSchemas: true
            SchemaImportStatus res = DgnECManager.Manager.ImportSchema(eCSchema, dgnFile, importSchemaOptions);
            // query schemas in a model
            List<string> schemasInModel =
                (List<string>)manager.DiscoverSchemasForModel(model, ReferencedModelScopeOption.All, false);

            // 3、 Attach an instance to an element.
            var scopeOption1 = new FindInstancesScopeOption();
            Bentley.DgnPlatformNET.DgnEC.FindInstancesScope scope1 = FindInstancesScope.CreateScope(model, scopeOption1);

            Bentley.ECObjects.Schema.IECSchema schema = DgnECManager.Manager.LocateSchemaInScope(scope1, schemaName,
                majorVersion: 1, minorVersion: 0, schemaMatchType: SchemaMatchType.Exact);
            schema = eCSchema as IECSchema;

            Bentley.DgnPlatformNET.Elements.Element[] sels = Utilities.Utils.GetSelection();
            if (sels.Length == 0)
            {
                MessageBox.Show("请先选择一个对象以进行属性添加");
                return AddinManagerDebuger.ExternalCmdResult.Cancel;
            }
            Element ele = sels[0];
            ECClass calss1 = schema.GetClass(className) as ECClass;
            Bentley.DgnPlatformNET.DgnEC.DgnECInstanceEnabler instanceEnabler =
                DgnECManager.Manager.ObtainInstanceEnabler(dgnFile, calss1);
            StandaloneECDInstance wipInstance = instanceEnabler.SharedWipInstance;
            wipInstance.MemoryBuffer.SetStringValue(propertyName, arrayIndex: -1, value: propertyValue);
            //
            IDgnECInstance dgnInst = instanceEnabler.CreateInstanceOnElement(ele, wipInstance, instanceOwnsElement: false);

            // 4、Rretrive ECSchema and IDgnECInstance from an element
            var scopeOption2 = new FindInstancesScopeOption(DgnECHostType.Element, includeAttachments: true);
            FindInstancesScope scope2 = FindInstancesScope.CreateScope(ele, scopeOption2);
            IECClass[] ecClassesToQuery = schema.GetClasses();
            Bentley.EC.Persistence.Query.ECQuery query = new ECQuery(ecClassesToQuery);
            query.SelectClause.SelectAllProperties = true;
            DgnECInstanceCollection ecInstances = manager.FindInstances(scope2, query);

            // 5、Extrack propertyValue from IDgnECInstance
            var sb = PopulateInstances(ecInstances);
            docMdf.WriteLineIntoDebuger(sb.ToString());
            //
            return AddinManagerDebuger.ExternalCmdResult.Cancel;
        }


        private static StringBuilder PopulateInstances(DgnECInstanceCollection ecInstances)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Bentley.DgnPlatformNET.DgnEC.IDgnECInstance instance in ecInstances)
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
    }
}
