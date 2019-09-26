using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bentley.DgnPlatformNET.DgnEC;
using eZBMCE.AddinManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Bentley.CifNET.ContentManagementModel;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.DgnPlatformNET.Elements;
using Bentley.EC.Persistence.Query;
using Bentley.ECObjects;
using Bentley.ECObjects.Instance;
using Bentley.ECObjects.Schema;
using Bentley.ECObjects.UI;
using eZBMCE.AddinManager;
using Application = Bentley.Interop.MicroStationDGN.Application;
using BM = Bentley.MstnPlatformNET;
using ItemTypeLibrary = Bentley.DgnPlatformNET.ItemTypeLibrary;
using ECI = Bentley.ECObjects.Instance;
using Utils = eZBMCE.Utilities.Utils;

namespace eZBMCE.Debug.EcObject
{
    class ECPropertyValueChangedTest
    {
        private DocumentModifier _docMdf;
        ECClass m_ecClass;
        DgnECInstanceEnabler m_ecInstanceEnabler;
        string[] m_integerPropertyNames;

        private void Initialize(DocumentModifier docMdf)
        {

            _docMdf = docMdf;
            const string namespacePrefix = "DgnECExample";
            const string schemaName = "ZengSchema1";
            const string className = "ZengClass";
            const string propertyName = "ZengProperty";
            const string propertyValue = "ZengPropertyValue";
            m_integerPropertyNames = new string[2] { "Age", "Height" };

            // 1、 Create Schemas
            Bentley.ECObjects.Schema.ECSchema eCSchema = new Bentley.ECObjects.Schema.ECSchema(schemaName,
                versionMajor: 1, versionMinor: 0, namespacePrefix: namespacePrefix);
            m_ecClass = new ECClass(className);
            m_ecClass.Add(new ECProperty(m_integerPropertyNames[0], Bentley.ECObjects.ECObjects.IntegerType));
            m_ecClass.Add(new ECProperty(m_integerPropertyNames[1], Bentley.ECObjects.ECObjects.IntegerType));
            eCSchema.AddClass(m_ecClass);

            // 2、 define the schema into a dgnFile
            var importSchemaOptions = new ImportSchemaOptions();
            // In default: isExternal: false, importReferencedSchemas: true
            SchemaImportStatus res = DgnECManager.Manager.ImportSchema(eCSchema, docMdf.DgnFile, importSchemaOptions);
            // query schemas in a model
            List<string> schemasInModel =
                (List<string>)DgnECManager.Manager.DiscoverSchemasForModel(docMdf.DgnModel, ReferencedModelScopeOption.All, false);

            m_ecInstanceEnabler =
                DgnECManager.Manager.ObtainInstanceEnabler(docMdf.DgnFile, m_ecClass);
        }

        private IDgnECInstance FindECInstanceOnElement(Element ele, ECClass ecclass)
        {
            // 4、Rretrive ECSchema and IDgnECInstance from an element
            Bentley.DgnPlatformNET.DgnEC.DgnECManager manager = DgnECManager.Manager;
            var scopeOption2 = new FindInstancesScopeOption(DgnECHostType.Element, includeAttachments: true);
            FindInstancesScope scope2 = FindInstancesScope.CreateScope(ele, scopeOption2);
            Bentley.EC.Persistence.Query.ECQuery query = new ECQuery(m_ecClass);
            query.SelectClause.SelectAllProperties = true;
            DgnECInstanceCollection ecInstances = manager.FindInstances(scope2, query);
            return ecInstances.First();
        }

        private void PopulateMinimumIECInstance(StandaloneECDInstance dgnModel)
        {
            // 静态事件，可以写在任意位置
            Bentley.ECObjects.UI.ECPropertyPane.OnPropertyValueChanging += OnPropertyPaneValueChanging;
        }


        /// <summary> 用户在界面中修改了某属性的值后触发此事件 </summary>
        /// <param name="beforePropertyValue">修改前的值</param>
        /// <param name="afterValue">修改后的值</param>
        /// <param name="rejectionMessage"> 如果返回false，则会弹出警告框，并显示此提示信息 </param>
        /// <returns>true表示接受修改，false则会撤消对属性值的修改</returns>
        private bool OnPropertyPaneValueChanging(IECPropertyValue beforePropertyValue, object afterValue, ref string rejectionMessage)
        {
            bool isValid = true;

            // Check Validation
            isValid = afterValue is System.Int32;
            //
            if (!isValid)
            {
                rejectionMessage = "请输入数值";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 设计 Bentley.ECObjects.Schema.IECValidatorDelegate 委托，并将其直接赋值给 ECProperty对象.Validator 属性。
        /// 就可以当用户在属性面板中修改此属性值时，对新值进行验证。 
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="nativeValue"></param>
        /// <remarks>当输入值非法时，则会通过 throw new Bentley.ECObjects.ECObjectsException.Validation() 弹出警告框，并显示此提示信息</remarks>
        public void TestIECValidatorDelegate(IECPropertyValue propertyValue, Object nativeValue)
        {
            bool isValid = true;
            // Check Validation
            isValid = nativeValue is System.Int32 && (int)nativeValue < 2;
            //
            MessageBox.Show("DFASD");
            if (!isValid)
            {
                throw new Bentley.ECObjects.ECObjectsException.Validation("TestIECValidatorDelegate ECObjectsException");
            }
        }

        public void CreateECDInstancesOnElement(DocumentModifier docMdf)
        {

            //Bentley.Interop.MicroStationDGN.Application app = Bentley.MstnPlatformNET.InteropServices.Utilities.ComApp;
            var session = BM.Session.Instance;

            Bentley.DgnPlatformNET.DgnEC.DgnECManager manager = DgnECManager.Manager;
            var model = docMdf.DgnModel;
            var dgnFile = model.GetDgnFile();

            const string namespacePrefix = "DgnECExample";
            const string schemaName = "ZengSchema02";
            const string className = "ZengClass";
            const string propertyName = "ZengProperty";
            const int propertyValue = 1;

            // Find if there is a corrisponding ECSchema
            FindInstancesScope scope = FindInstancesScope.CreateModelSpecificFileScope(model, dgnFile, new FindInstancesScopeOption(DgnECHostType.Model));
            IECSchema eCSchema = manager.LocateSchemaInScope(scope, schemaName, 1, 0, SchemaMatchType.Exact);

            if (eCSchema == null)
            {
                MessageBox.Show("未找到已存在的Schema");

                // 1、 Create Schemas
                eCSchema = new Bentley.ECObjects.Schema.ECSchema(schemaName,
                    versionMajor: 1, versionMinor: 0, namespacePrefix: namespacePrefix);
                ECClass eCClass = new ECClass(className);
                ECProperty eCProp = new ECProperty(propertyName, Bentley.ECObjects.ECObjects.IntegerType);
                eCClass.Add(eCProp);
                eCSchema.AddClass(eCClass);

                eCProp.Validator = TestIECValidatorDelegate;

                // 2、 define the schema into a dgnFile
                var importSchemaOptions = new ImportSchemaOptions();
                // In default: isExternal: false, importReferencedSchemas: true
                SchemaImportStatus res = DgnECManager.Manager.ImportSchema(eCSchema, dgnFile, importSchemaOptions);

                // query all schemas in a model
                List<string> schemasInModel =
                    (List<string>)manager.DiscoverSchemasForModel(model, ReferencedModelScopeOption.All, false);
            }
            else
            {
                MessageBox.Show("已找到已存在的Schema， 属性数量 " + eCSchema.GetProperties().Count.ToString());
            }

            var scopeOption1 = new FindInstancesScopeOption();
            Bentley.DgnPlatformNET.DgnEC.FindInstancesScope scope1 = FindInstancesScope.CreateScope(model, scopeOption1);

            Bentley.ECObjects.Schema.IECSchema schema = DgnECManager.Manager.LocateSchemaInScope(scope1, schemaName,
                majorVersion: 1, minorVersion: 0, schemaMatchType: SchemaMatchType.Exact);

            // 3、 Attach an instance to an element.
            schema = eCSchema as IECSchema;
            Bentley.DgnPlatformNET.Elements.Element[] sels = Utilities.Utils.GetSelection();
            if (sels.Length == 0)
            {
                MessageBox.Show("请先选择一个对象以进行属性添加");
                return;
            }
            Element ele = sels[0];
            ECClass calss1 = schema.GetClass(className) as ECClass;
            Bentley.DgnPlatformNET.DgnEC.DgnECInstanceEnabler instanceEnabler =
                DgnECManager.Manager.ObtainInstanceEnabler(dgnFile, calss1);

            Bentley.ECObjects.Instance.StandaloneECDInstance wipInstance1 = instanceEnabler.SharedWipInstance;
            wipInstance1.MemoryBuffer.SetIntegerValue(propertyName, arrayIndex: -1, value: propertyValue);

            wipInstance1[propertyName].IntValue = 1;
            //wipInstance1.ECPropertyValueChanged += new ECI.ECPropertyValueChangedHandler(WipInstanceOnEcPropertyValueChanged);

            //wipInstance1[propertyName].IntValue = 3;
            //wipInstance1.ClearInstanceChangedEvents();
            //wipInstance1[propertyName].IntValue = 4;
            //
            IDgnECInstance dgnInst = instanceEnabler.CreateInstanceOnElement(ele, wipInstance1, instanceOwnsElement: false);

            return;
            Initialize(docMdf);

            var sel = Utils.GetSelection();
            if (sel.Length == 0)
            {
                return;
            }
            Element lineElement = sel[0];

            ECI.StandaloneECDInstance wipInstance = new ECI.StandaloneECDInstance(m_ecClass);
            //PopulateMinimumIECInstance(wipInstance);

            wipInstance.ECPropertyValueChanged += new ECI.ECPropertyValueChangedHandler(WipInstanceOnEcPropertyValueChanged);
            wipInstance[m_integerPropertyNames[0]].IntValue = 100;
            wipInstance.ClearInstanceChangedEvents();
            wipInstance[m_integerPropertyNames[0]].IntValue = 0;
            IDgnECInstance dgnInstance = m_ecInstanceEnabler.CreateInstanceOnElement(lineElement, wipInstance, false);

        }


        private void WipInstanceOnEcPropertyValueChanged(object sender, ECPropertyValueChangedEventArgs args)
        {
            MessageBox.Show("WipInstanceOnEcPropertyValueChanged 事件: " + args.PropertyValue.StringValue + args.PropertyValue.Property.Name);
            //throw new NotImplementedException();
        }
    }
}
