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
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.DgnPlatformNET.Elements;
using Bentley.EC.Persistence.Query;
using Bentley.ECObjects.Instance;
using Bentley.ECObjects.Schema;
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
        }
        public void CreateECDInstancesOnElement(DocumentModifier docMdf)
        {

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
                return;
            }
            Element ele = sels[0];
            ECClass calss1 = schema.GetClass(className) as ECClass;
            Bentley.DgnPlatformNET.DgnEC.DgnECInstanceEnabler instanceEnabler =
                DgnECManager.Manager.ObtainInstanceEnabler(dgnFile, calss1);
            StandaloneECDInstance wipInstance1 = instanceEnabler.SharedWipInstance;
            wipInstance1.MemoryBuffer.SetStringValue(propertyName, arrayIndex: -1, value: propertyValue);

            wipInstance1[propertyName].StringValue = "StringValue1";
            wipInstance1.ECPropertyValueChanged += new ECI.ECPropertyValueChangedHandler(WipInstanceOnEcPropertyValueChanged);
            wipInstance1[propertyName].StringValue = "StringValue2";
            wipInstance1.ClearInstanceChangedEvents();
            wipInstance1[propertyName].StringValue = "StringValue3";
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

            TextWriter writer = new StringWriter();

            //Console.WriteLine ("Managed DgnECInstance");

            dgnInstance.Dump(writer, null);

            //Console.Write (writer.ToString ());



            //dgnInstance["ContactInfo.Email"].StringValue = "bsteinbk@att.com";
            //dgnInstance["ContactInfo.PhoneNumber.AreaCode"].IntValue = 502;

            //dgnInstance["PhoneNumbers[0].AreaCode"].IntValue = 502;
            //dgnInstance["PhoneNumbers[0].Number"].IntValue = 2416267;

            //dgnInstance["Employees[0].FirstName"].StringValue = "Bill";
            //dgnInstance["Employees[0].LastName"].StringValue = "Steinbock";

            //dgnInstance["Employees[0].ContactInfo.Email"].StringValue = "bsteinbk@att.com";
            //dgnInstance["Employees[0].ContactInfo.PhoneNumber.AreaCode"].IntValue = 502;

            //dgnInstance["Employees[0].ContactInfo.PhoneNumber.Number"].IntValue = 2416267;

            //dgnInstance["Employees[0].ContactInfo.Address.Town"].StringValue = "Crestwood";

            //dgnInstance["Employees[0].ContactInfo.Address.State"].StringValue = "KY";

            //dgnInstance["Employees[0].PhoneNumbers[0].AreaCode"].IntValue = 502;

            //dgnInstance["Employees[0].PhoneNumbers[0].Number"].IntValue = 2416267;

            //dgnInstance["Employees[0].PhoneNumbers[1].AreaCode"].IntValue = 502;

            //dgnInstance["Employees[0].PhoneNumbers[1].Number"].IntValue = 6480175;

            //dgnInstance["IntegerArray[0]"].IntValue = 1;

            //dgnInstance["IntegerArray[1]"].IntValue = 2;



            //dgnInstance["Employees[0].IntegerArray[0]"].IntValue = 50;

            //dgnInstance["Employees[0].IntegerArray[1]"].IntValue = 100;

            //dgnInstance["Employees[0].IntegerArray[2]"].IntValue = 150;



            //dgnInstance["Employees[0].IntegerArray[0]"].IntValue = 123;

            //dgnInstance["Employees[0].IntegerArray[1]"].IntValue = 231;

            //dgnInstance["Employees[0].IntegerArray[2]"].IntValue = 312;



            //// modify the email address in a struct array member with a longer string

            //dgnInstance["Employees[0].ContactInfo.Email"].StringValue = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbsteinbk@att.com";



            //// modify the email address with a longer string

            //dgnInstance["ContactInfo.Email"].StringValue = "bill.steinbock@bentley.com_bill.steinbock@bentley.com_bill.steinbock@bentley.com_bill.steinbock@bentley.com";



            //// add enough primtive array entries to force a reallocation

            //dgnInstance["IntegerArray[2]"].IntValue = 2;

            //dgnInstance["IntegerArray[3]"].IntValue = 3;

            //dgnInstance["IntegerArray[4]"].IntValue = 4;

            //dgnInstance["IntegerArray[5]"].IntValue = 5;

            //dgnInstance["IntegerArray[6]"].IntValue = 6;

            //dgnInstance["IntegerArray[7]"].IntValue = 7;

            //dgnInstance["IntegerArray[8]"].IntValue = 8;

            //dgnInstance["IntegerArray[9]"].IntValue = 9;

            //dgnInstance["IntegerArray[10]"].IntValue = 10;

            //dgnInstance["IntegerArray[11]"].IntValue = 11;

            //dgnInstance["IntegerArray[12]"].IntValue = 12;

            //dgnInstance["IntegerArray[13]"].IntValue = 13;

            //dgnInstance["IntegerArray[14]"].IntValue = 14;

            //dgnInstance["IntegerArray[15]"].IntValue = 15;

            //dgnInstance["IntegerArray[16]"].IntValue = 16;

            //dgnInstance["IntegerArray[17]"].IntValue = 17;

            //dgnInstance["IntegerArray[18]"].IntValue = 18;

            //dgnInstance["IntegerArray[19]"].IntValue = 19;

            //dgnInstance["IntegerArray[20]"].IntValue = 20;



            //dgnInstance["StringArray[0]"].StringValue = "FirstString";

            //dgnInstance["StringArray[1]"].StringValue = "SecondString";

            //dgnInstance["StringArray[2]"].StringValue = "This is a longer string";



            //dgnInstance["Employees[1].FirstName"].StringValue = "Bill";

            //dgnInstance["Employees[1].LastName"].StringValue = "Steinbock";

            //dgnInstance["Employees[1].ContactInfo.Email"].StringValue = "bsteinbk@att.com";

            //dgnInstance["Employees[1].ContactInfo.PhoneNumber.AreaCode"].IntValue = 502;

            //dgnInstance["Employees[1].ContactInfo.PhoneNumber.Number"].IntValue = 2416267;

            //dgnInstance["Employees[1].ContactInfo.Address.Town"].StringValue = "Crestwood";

            //dgnInstance["Employees[1].ContactInfo.Address.State"].StringValue = "KY";

            //dgnInstance["Employees[1].IntegerArray[0]"].IntValue = 50;

            //dgnInstance["Employees[1].IntegerArray[1]"].IntValue = 100;

            //dgnInstance["Employees[1].IntegerArray[2]"].IntValue = 200;

            //dgnInstance["Employees[1].IntegerArray[3]"].IntValue = 300;

            //dgnInstance["Employees[1].IntegerArray[4]"].IntValue = 400;

            //dgnInstance["Employees[1].IntegerArray[5]"].IntValue = 500;

            //dgnInstance["Employees[1].IntegerArray[6]"].IntValue = 600;

            //dgnInstance["Employees[1].IntegerArray[7]"].IntValue = 700;

            //dgnInstance["Employees[1].IntegerArray[8]"].IntValue = 800;

            //dgnInstance["Employees[1].IntegerArray[9]"].IntValue = 900;

            //dgnInstance["Employees[1].IntegerArray[10]"].IntValue = 1000;

            //dgnInstance["Employees[1].IntegerArray[11]"].IntValue = 1100;

            //dgnInstance["Employees[1].IntegerArray[12]"].IntValue = 1200;

            //dgnInstance["Employees[1].IntegerArray[13]"].IntValue = 1300;

            //dgnInstance["Employees[1].IntegerArray[14]"].IntValue = 1400;

            //dgnInstance["Employees[1].IntegerArray[15]"].IntValue = 1500;

            //dgnInstance["Employees[1].IntegerArray[16]"].IntValue = 1600;

            //dgnInstance["Employees[1].IntegerArray[17]"].IntValue = 1700;

            //dgnInstance["Employees[1].IntegerArray[18]"].IntValue = 1800;

            //dgnInstance["Employees[1].IntegerArray[19]"].IntValue = 1900;



            //dgnInstance["Employees[1].PhoneNumbers[0].AreaCode"].IntValue = 502;

            //dgnInstance["Employees[1].PhoneNumbers[0].Number"].IntValue = 2416267;

            //dgnInstance["Employees[1].PhoneNumbers[1].AreaCode"].IntValue = 502;

            //dgnInstance["Employees[1].PhoneNumbers[1].Number"].IntValue = 6480175;



            TextWriter writer2 = new StringWriter();

            // Console.WriteLine ("Editted Managed DgnECInstance");

            dgnInstance.Dump(writer2, null);

            // Console.Write (writer2.ToString ());



            TextWriter writer3 = new StringWriter();

            ECI.ECDInstanceFactory.CopyInstanceData(wipInstance, dgnInstance);

            //Console.WriteLine ("Copied Standalone Instance");

            wipInstance.Dump(writer3, null);

            //Console.Write (writer3.ToString ());

            dgnInstance.WriteChanges();



            IDgnECInstance writtenInstance = FindECInstanceOnElement(lineElement, m_ecClass);


            TextWriter writer4 = new StringWriter();

            //Console.WriteLine ("Found IDgnECInstance Instance");

            writtenInstance.Dump(writer4, null);

            //Console.Write (writer4.ToString ());



            //// Test deleting array entries

            //ECI.IECPropertyValue propVal = dgnInstance["IntegerArray"];

            //ECI.IECArrayValue integerArrayProp = propVal.ContainedValues as ECI.IECArrayValue;

            //integerArrayProp.RemoveAt(2);



            //// Test deleting struct array entries

            //propVal = dgnInstance["Employees"];

            //ECI.IECArrayValue structArrayProp = propVal.ContainedValues as ECI.IECArrayValue;

            //structArrayProp.RemoveAt(0);



            //propVal = dgnInstance["Employees[1].PhoneNumbers"];

            //structArrayProp = propVal.ContainedValues as ECI.IECArrayValue;

            //structArrayProp.RemoveAt(0);



            TextWriter writer5 = new StringWriter();

            //Console.WriteLine ("Deleted Employee Instance");

            dgnInstance.Dump(writer5, null);

            //Console.Write (writer5.ToString ());

        }

        private void WipInstanceOnEcPropertyValueChanged(object sender, ECPropertyValueChangedEventArgs args)
        {
            MessageBox.Show("WipInstanceOnEcPropertyValueChanged 事件: " + args.PropertyValue.StringValue + args.PropertyValue.Property.Name);
            //throw new NotImplementedException();
        }
    }
}
