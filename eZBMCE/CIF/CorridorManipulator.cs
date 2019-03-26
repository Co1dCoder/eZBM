using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.GeometryModel.SDK.Edit;
using Bentley.CifNET.SDK.Edit;
using eZBMCE.AddinManager;

namespace TrainingManagedEditSDKExamples
{
    internal class CorridorManipulator
    {
        public static void CorrdorManipulation(DocumentModifier docMdf)
        {

            TemplateDefinition tempDef = GetTemplateDefinitionFromItl();

            //ConsensusConnectionEdit allows the persistence of civil objects to the DGN file
            Bentley.CifNET.SDK.Edit.ConsensusConnectionEdit con = ConsensusConnectionEdit.GetActive();

            //begins mode to persist objects
            con.StartTransientMode();

            string featureName = "Corridor Test";
            //create SDK.Edit alignment objects from native objects
            // Bentley.CifNET.GeometryModel.SDK.Edit.AlignmentEdit al = AlignmentEdit.CreateByLinearElement(con, complexAlign, true);
            GeometricModel geoModel = con.GetActiveGeometricModel();
            Alignment ali = geoModel.Alignments.FirstOrDefault(r => !string.IsNullOrEmpty(r.Name));
            CorridorEdit corEd = CorridorEdit.CreateByAlignment(con, "CorridorName", ali);
            corEd.FeatureName = featureName;

            TemplateDropParameters tdp = new TemplateDropParameters(tempDef, startDistance: 0, endDistance: 30);
            tdp.Interval = 20;
            TemplateDropEdit tempDrop1 = corEd.AddTemplateDrop(tdp);

            //persists objects created in persist mode
            con.PersistTransients();

            // 对既有Corridor进行修改
            CorridorEdit corrEdit = geoModel.Corridors.FirstOrDefault(r => r.Name == featureName) as CorridorEdit;

            // 提取既有 Corridor 中指定桩号位置的横断面信息
            XSCutPoint[] pts = corrEdit.GetXSCutPoints(20, 50, 50, 0);
            if (pts != null || pts.Length > 0)
            {
                foreach (XSCutPoint pt in pts)
                {
                    docMdf.WriteLineIntoDebuger(
                        $"pts.PointName= {pt.PointName}, pts.PointFeatureName= {pt.PointFeatureName}");
                }
            }
            else
            {
                MessageBox.Show($"XSCutPoints not extracted.");
            }

            // 对既有Corridor进行修改（不用 StartTransientMode）
            foreach (TemplateDrop tempDrop in corrEdit.TemplateDrops)
            {
                TemplateDropEdit tempDropEdit = tempDrop as TemplateDropEdit;
                docMdf.WriteMessageLineNow(tempDropEdit.StartDistanceDefinitionMethod, tempDropEdit.StartDistanceDefinitionMethod);
                tempDropEdit.SetDistanceDefinition(10, 40); // 修改既有 TemplateDropEdit 的起止桩号
            }
        }

        /// <summary> 从 itl 文件中提取某指定的横断面模板定义 </summary>
        /// <returns> 以 XmlDocument 的操作为主 </returns>
        private static TemplateDefinition GetTemplateDefinitionFromItl()
        {
            // 从 itl 的横断面模板文件中提取出某个横断面模板来
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(@"C:\Users\Administrator\Desktop\111.itl");
            System.Xml.XmlNode TemplateNode = null;  // 某一个横断面模板定义

            // 提取 Template 方式1：GetElementsByTagName
            System.Xml.XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Template");
            TemplateNode = nodeList[nodeList.Count - 1];

            // 提取 Template 方式2：SelectNodes
            System.Xml.XmlNodeList attributeList = xmlDoc.SelectNodes("/InRoads/TemplateLibrary/Category/Category/Category/Template/@name");
            foreach (XmlAttribute attr in attributeList)
            {
                // 按横断面名称来索引
                if (attr.InnerText == "Single Barrell Tunnel")
                {
                    TemplateNode = (XmlElement)attr.OwnerElement;  // 从 XmlAttribute 索引其所在的 XmlElement 
                    break;
                }
            }
            string txmlFragement = TemplateNode.OuterXml;
            TemplateDefinition tempDef = TemplateDefinition.LoadXml(txmlFragement);
            return tempDef;
        }
    }
}
