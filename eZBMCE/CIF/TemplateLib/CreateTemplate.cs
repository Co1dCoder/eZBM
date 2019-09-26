using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bentley.CifNET.LinearGeometry;
using Bentley.GeometryNET;
using Bentley.CifNET.Objects;
using Bentley.CifNET.Model;
using Bentley.DgnPlatformNET;
using Bentley.CifNET.GeometryModel.SDK.Edit;

namespace eZBMCE.Cif.TemplateLib
{
    internal class CreateTemplate
    {
        public static void Test_TemplatePointConstraint()
        {
            string Parent_Point = "Parent_point";
            double constraint_value = 0.0;
            TemplatePointConstraint constraint1 = new TemplatePointConstraint(ConstraintType.Horizontal, constraint_value, Parent_Point);

            constraint1.Type = ConstraintType.Vertical;
        }


        public static void Test_TemplateXml()
        {

            //             TemplatePointConstraint constraint1 = new TemplatePointConstraint(ConstraintType.Horizontal, 2.0, "ParentPointName1");
            //             TemplatePointConstraint constraint2 = new TemplatePointConstraint(ConstraintType.Vertical, 4.0, "ParentPointName2");
            // 
            //             string Point_Name1 = "point_Name1";
            //             string featureName1 = "point1_featurename1";
            //             string styleName1 = "point1_styleName1";
            //             double x = 2.0;
            //             double y = 2.0;
            //             string styleConstraintName = "point1_styleConstraintName";
            //             string styleConstraintMode = "point1_styleConstraintMode";
            //             double styleConstraintRange = 2.5;
            //             TemplatePoint point1 = new TemplatePoint(Point_Name1,
            //                              featureName1,
            //                              styleName1,
            //                              false,
            //                              x,
            //                              y,
            //                              0,
            //                              styleConstraintName,
            //                              styleConstraintMode,
            //                              styleConstraintRange,
            //                              constraint1,
            //                              constraint2);
            // 
            //             TemplateDefinition templateDef = new TemplateDefinition("TestPointTemplate");
            //             templateDef.AddPoint(point1);
            //             string xmlFragment = templateDef.GetXml();

            string tmpLib = "D:\\1.itl";

            if (!System.IO.File.Exists(tmpLib))
            {
                return;
            }
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

            xmlDoc.Load(tmpLib);
            System.Xml.XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Template");

            System.Xml.XmlNode TemplateNode = nodeList[nodeList.Count - 1];
            System.Xml.XmlNode ParentNode = TemplateNode.ParentNode;
            string txmlFragement = TemplateNode.OuterXml;
            TemplateDefinition template = TemplateDefinition.LoadXml(txmlFragement);
            // template.Name = "temp1";

            //Create a new node.
            System.Xml.XmlNode TemplateNodeNew = TemplateNode.Clone();
            TemplateNodeNew.RemoveAll();
            string xmlFragment = template.GetXml();
            TemplateNodeNew.InnerXml = xmlFragment;


            //Add the node to the document.
            ParentNode.AppendChild(TemplateNodeNew);

            xmlDoc.Save("D:\\1.itl");

        }

        public static void Test_TemplatePoint()
        {

            //point_Name1
            TemplatePointConstraint constraintA0_1 = new TemplatePointConstraint(ConstraintType.None, 2.0, "");
            TemplatePointConstraint constraintB0_1 = new TemplatePointConstraint(ConstraintType.None, 4.0, "");

            string Point_Name = "point_Name1";
            string featureName = "point1_featurename1";
            string styleName = "point1_styleName1";
            double x = -1;
            double y = -1;
            string styleConstraintName = "point1_styleConstraintName";
            string styleConstraintMode = "point1_styleConstraintMode";
            double styleConstraintRange = 2.5;
            TemplatePoint point1 = new TemplatePoint(Point_Name,
                             featureName,
                             styleName,
                             false,
                             x,
                             y,
                             0,
                             styleConstraintName,
                             styleConstraintMode,
                             styleConstraintRange,
                             constraintA0_1,
                             constraintB0_1);

            TemplateDefinition templateDef = new TemplateDefinition("TestPointTemplate");
            templateDef.AddPoint(point1);

            //point_Name2
            TemplatePointConstraint constraintA1_2 = new TemplatePointConstraint(ConstraintType.Horizontal, 2.0, "point_Name1");
            TemplatePointConstraint constraintB1_2 = new TemplatePointConstraint(ConstraintType.Vertical, 4.0, "point_Name1");

            Point_Name = "point_Name2";
            featureName = "point1_featurename2";
            styleName = "point1_styleName2";
            x = 2.0;
            y = 2.0;
            styleConstraintName = "point2_styleConstraintName";
            styleConstraintMode = "point2_styleConstraintMode";
            styleConstraintRange = 2.5;
            TemplatePoint point2 = new TemplatePoint(Point_Name,
                             featureName,
                             styleName,
                             false,
                             x,
                             y,
                             0,
                             styleConstraintName,
                             styleConstraintMode,
                             styleConstraintRange,
                             constraintA1_2,
                             constraintB1_2);

            templateDef.AddPoint(point2);
            // 
            //point_Name3
            TemplatePointConstraint constraintA2_3 = new TemplatePointConstraint(ConstraintType.Horizontal, 2.0, "point_Name2");
            TemplatePointConstraint constraintB2_3 = new TemplatePointConstraint(ConstraintType.Slope, 0.2, "point_Name2");

            Point_Name = "point_Name3";
            featureName = "point1_featurename3";
            styleName = "point1_styleName3";
            x = 2.0;
            y = 2.0;
            styleConstraintName = "point2_styleConstraintName";
            styleConstraintMode = "point2_styleConstraintMode";
            styleConstraintRange = 2.5;
            TemplatePoint point3 = new TemplatePoint(Point_Name,
                             featureName,
                             styleName,
                             false,
                             x,
                             y,
                             0,
                             styleConstraintName,
                             styleConstraintMode,
                             styleConstraintRange,
                             constraintA2_3,
                             constraintB2_3);

            templateDef.AddPoint(point3);


            //point_Name4
            TemplatePointConstraint constraintA2_4 = new TemplatePointConstraint(ConstraintType.Horizontal, 0, "point_Name2");
            TemplatePointConstraint constraintB2_4 = new TemplatePointConstraint(ConstraintType.Vertical, -4, "point_Name2");

            Point_Name = "point_Name4";
            featureName = "point1_featurename4";
            styleName = "point1_styleName4";
            x = 2.0;
            y = 2.0;
            styleConstraintName = "point2_styleConstraintName";
            styleConstraintMode = "point2_styleConstraintMode";
            styleConstraintRange = 2.5;
            TemplatePoint point4 = new TemplatePoint(Point_Name,
                             featureName,
                             styleName,
                             false,
                             x,
                             y,
                             0,
                             styleConstraintName,
                             styleConstraintMode,
                             styleConstraintRange,
                             constraintA2_4,
                             constraintB2_4);

            templateDef.AddPoint(point4);


            string componentName = "Component1";
            string materialName = "materialName";
            TemplateComponent tmpCom = new TemplateComponent(componentName, materialName, TemplateComponentType.Normal, true, "", "");

            TemplateVertex vertex1 = new TemplateVertex(point1, 0.1);
            TemplateVertex vertex2 = new TemplateVertex(point2, 0.2);
            TemplateVertex vertex3 = new TemplateVertex(point3, 0.3);
            TemplateVertex vertex4 = new TemplateVertex(point4, 0.4);

            tmpCom.AddVertex(vertex1);
            tmpCom.AddVertex(vertex2);
            tmpCom.AddVertex(vertex3);
            tmpCom.AddVertex(vertex4);

            templateDef.AddComponent(tmpCom);



            string xmlFragment = templateDef.GetXml();

            string tmpLib = "D:\\1.itl";

            if (!System.IO.File.Exists(tmpLib))
            {
                return;
            }
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

            xmlDoc.Load(tmpLib);
            System.Xml.XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Template");


            System.Xml.XmlNode TemplateNode = nodeList[nodeList.Count - 1];
            System.Xml.XmlNode ParentNode = TemplateNode.ParentNode;
            string txmlFragement = TemplateNode.OuterXml;
            TemplateDefinition template = TemplateDefinition.LoadXml(txmlFragement);
            // template.Name = "temp1";

            //Create a new node.
            System.Xml.XmlNode TemplateNodeNew = TemplateNode.Clone();
            TemplateNodeNew.RemoveAll();
            //string xmlFragment = template.GetXml();
            TemplateNodeNew.InnerXml = xmlFragment;


            //Add the node to the document.
            ParentNode.AppendChild(TemplateNodeNew);

            xmlDoc.Save("D:\\1.itl");

        }

        public static void Test_TemplateComponent()
        {

            TemplatePointConstraint constraint1_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 2.0, "ParentPointName1");
            TemplatePointConstraint constraint1_2 = new TemplatePointConstraint(ConstraintType.Slope, 0.02, "ParentPointName2");

            TemplatePointConstraint constraint2_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 2.0, "ParentPointName1");
            TemplatePointConstraint constraint2_2 = new TemplatePointConstraint(ConstraintType.Slope, -0.02, "ParentPointName2");

            TemplatePointConstraint constraint3_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 0.0, "ParentPointName1");
            TemplatePointConstraint constraint3_2 = new TemplatePointConstraint(ConstraintType.Vertical, 0.02, "ParentPointName2");

            TemplatePointConstraint constraint4_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 2.0, "ParentPointName1");
            TemplatePointConstraint constraint4_2 = new TemplatePointConstraint(ConstraintType.Vertical, 0.02, "ParentPointName2");

            string Point_Name1 = "point_Name1";
            string featureName1 = "featurename1";
            string styleName1 = "point1_styleName1";
            double x = 2.0;
            double y = 2.0;
            string styleConstraintName = "point1_styleConstraintName";
            string styleConstraintMode = "point1_styleConstraintMode";
            double styleConstraintRange = 2.5;
            TemplatePoint point1 = new TemplatePoint(Point_Name1,
                             featureName1,
                             styleName1,
                             false,
                             x,
                             y,
                             0,
                             styleConstraintName,
                             styleConstraintMode,
                             styleConstraintRange,
                             constraint1_1,
                             constraint1_2);

            TemplatePoint point2 = new TemplatePoint("point_Name2",
                             featureName1,
                             "point2_styleName1",
                             false,
                             4.0,
                             2.0,
                             0,
                             "point2_styleConstraintName",
                             "point2_styleConstraintMode",
                             2.5,
                             constraint2_1,
                             constraint2_2);

            string featureName2 = "featurename2";
            TemplatePoint point3 = new TemplatePoint("point_Name3",
                             featureName2,
                             "point3_styleName1",
                             false,
                             1.5,
                             0.0,
                             0,
                             "point3_styleConstraintName",
                             "point3_styleConstraintMode",
                             2.5,
                             constraint3_1,
                             constraint3_2);

            TemplatePoint point4 = new TemplatePoint("point_Name4",
                             featureName2,
                             "point4_styleName1",
                             false,
                             2.5,
                             0.0,
                             0,
                             "point4_styleConstraintName",
                             "point4_styleConstraintMode",
                             2.5,
                             constraint4_1,
                             constraint4_2);

            string componentName = "Component1";
            string materialName = "materialName";
            TemplateComponent tmpCom = new TemplateComponent(componentName, materialName, TemplateComponentType.Normal, true, "", "");

            TemplateVertex vertex1 = new TemplateVertex(point1, 0.0);
            TemplateVertex vertex2 = new TemplateVertex(point2, 0.0);
            TemplateVertex vertex3 = new TemplateVertex(point3, 0.0);
            TemplateVertex vertex4 = new TemplateVertex(point4, 0.0);

            tmpCom.AddVertex(vertex1);
            tmpCom.AddVertex(vertex2);
            tmpCom.AddVertex(vertex4);
            tmpCom.AddVertex(vertex3);

            System.Collections.Generic.List<TemplateVertex> pointsOfFeature1 = tmpCom.GetVerticesByFeatureName(featureName1);

            System.Collections.Generic.List<TemplateVertex> pointsOfFeature2 = tmpCom.GetVerticesByFeatureName(featureName2);


            System.Collections.Generic.List<TemplateVertex> pointsNoFeature = tmpCom.GetVerticesByFeatureName("HasNoFeatureName");


            for (int i = 1; i <= tmpCom.VertexCount; i++)
            {
                string name = "point_Name";
                name = name + i.ToString();
                //Assert.IsTrue(tmpCom.IsVertexNameIn(name));
            }

            //Assert.IsFalse(tmpCom.IsVertexNameIn("point_Name5"));

        }

        public static void test_loadTemplate()
        {
            //GeometricModel geometricModel = GeometricModel.GetGeometricModelAndCreateIfDontExist(ObjectSpace);
            string tmpLib = "D:\\1.itl";

            if (!System.IO.File.Exists(tmpLib))
            {
                return;
            }
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

            xmlDoc.Load(tmpLib);
            System.Xml.XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Template");
            string xmlFragement = nodeList[nodeList.Count - 1].OuterXml;

            TemplateDefinition template = TemplateDefinition.LoadXml(xmlFragement);

            string strXml = template.GetXml(false);
            TemplateDefinition template2 = TemplateDefinition.LoadXml(strXml);
            string strXml2 = template2.GetXml(false);

            //Assert.AreEqual(strXml, strXml2);

        }
        public static void Test_TemplateDefinition()
        {
            //test_loadTemplate();

            TemplatePointConstraint constraint1_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 2.0, "ParentPointName1");
            TemplatePointConstraint constraint1_2 = new TemplatePointConstraint(ConstraintType.Slope, 0.02, "ParentPointName2");

            TemplatePointConstraint constraint2_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 4.0, "ParentPointName1");
            TemplatePointConstraint constraint2_2 = new TemplatePointConstraint(ConstraintType.Slope, -0.02, "ParentPointName2");

            TemplatePointConstraint constraint3_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 0.0, "ParentPointName1");
            TemplatePointConstraint constraint3_2 = new TemplatePointConstraint(ConstraintType.Vertical, 0.02, "ParentPointName2");

            TemplatePointConstraint constraint4_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 4.0, "ParentPointName1");
            TemplatePointConstraint constraint4_2 = new TemplatePointConstraint(ConstraintType.Vertical, 0.02, "ParentPointName2");

            string Point_Name1 = "point_Name1";
            string featureName1 = "featurename1";
            string styleName1 = "point1_styleName1";
            double x = 2.0;
            double y = 2.0;
            string styleConstraintName = "point1_styleConstraintName";
            string styleConstraintMode = "point1_styleConstraintMode";
            double styleConstraintRange = 2.5;
            TemplatePoint point1 = new TemplatePoint(Point_Name1,
                             featureName1,
                             styleName1,
                             false,
                             x,
                             y,
                             0,
                             styleConstraintName,
                             styleConstraintMode,
                             styleConstraintRange,
                             constraint1_1,
                             constraint1_2);

            TemplatePoint point2 = new TemplatePoint("point_Name2",
                             featureName1,
                             "point2_styleName1",
                             false,
                             4.0,
                             2.0,
                             0,
                             "point2_styleConstraintName",
                             "point2_styleConstraintMode",
                             2.5,
                             constraint2_1,
                             constraint2_2);


            TemplatePoint point3 = new TemplatePoint("point_Name3",
                             "point3_featurename1",
                             "point3_styleName1",
                             false,
                             1.5,
                             0.0,
                             0,
                             "point3_styleConstraintName",
                             "point3_styleConstraintMode",
                             2.5,
                             constraint3_1,
                             constraint3_2);

            TemplatePoint point4 = new TemplatePoint("point_Name4",
                             "point4_featurename1",
                             "point4_styleName1",
                             false,
                             2.5,
                             0.0,
                             0,
                             "point4_styleConstraintName",
                             "point4_styleConstraintMode",
                             2.5,
                             constraint4_1,
                             constraint4_2);


            TemplatePointConstraint constraint5_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 4.0, "ParentPointName1");
            TemplatePointConstraint constraint5_2 = new TemplatePointConstraint(ConstraintType.Slope, 0.02, "ParentPointName2");

            TemplatePointConstraint constraint6_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 6.0, "ParentPointName1");
            TemplatePointConstraint constraint6_2 = new TemplatePointConstraint(ConstraintType.Slope, -0.02, "ParentPointName2");

            TemplatePointConstraint constraint7_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 2.0, "ParentPointName1");
            TemplatePointConstraint constraint7_2 = new TemplatePointConstraint(ConstraintType.Vertical, 0.02, "ParentPointName2");

            TemplatePointConstraint constraint8_1 = new TemplatePointConstraint(ConstraintType.Horizontal, 4.0, "ParentPointName1");
            TemplatePointConstraint constraint8_2 = new TemplatePointConstraint(ConstraintType.Vertical, 0.04, "ParentPointName2");

            string Point_Name5 = "point_Name5";
            string featureName2 = "featurename2";
            string styleName5 = "point5_styleName";
            x = 2.0;
            y = 2.0;
            styleConstraintName = "point5_styleConstraintName";
            styleConstraintMode = "point5_styleConstraintMode";
            styleConstraintRange = 2.5;
            TemplatePoint point5 = new TemplatePoint(Point_Name5,
                             featureName2,
                             styleName5,
                             false,
                             x,
                             y,
                             0,
                             styleConstraintName,
                             styleConstraintMode,
                             styleConstraintRange,
                             constraint5_1,
                             constraint5_2);

            TemplatePoint point6 = new TemplatePoint("point_Name6",
                             featureName2,
                             "point6_styleName1",
                             false,
                             4.0,
                             2.0,
                             0,
                             "point2_styleConstraintName",
                             "point2_styleConstraintMode",
                             2.5,
                             constraint6_1,
                             constraint6_2);


            TemplatePoint point7 = new TemplatePoint("point_Name7",
                             "point7_featurename1",
                             "point7_styleName1",
                             false,
                             1.5,
                             0.0,
                             0,
                             "point7_styleConstraintName",
                             "point7_styleConstraintMode",
                             2.5,
                             constraint7_1,
                             constraint7_2);

            TemplatePoint point8 = new TemplatePoint("point_Name8",
                             "point8_featurename1",
                             "point8_styleName1",
                             false,
                             2.5,
                             0.0,
                             0,
                             "point8_styleConstraintName",
                             "point8_styleConstraintMode",
                             2.5,
                             constraint8_1,
                             constraint8_2);

            TemplateDefinition templateDef = new TemplateDefinition("TestTemplate");
            templateDef.AddPoint(point1);
            templateDef.AddPoint(point2);
            templateDef.AddPoint(point3);
            templateDef.AddPoint(point4);

            templateDef.AddPoint(point5);
            templateDef.AddPoint(point6);
            templateDef.AddPoint(point7);
            templateDef.AddPoint(point8);

            string componentName1 = "Component_name1";
            string materialName = "materialName";
            TemplateComponent tmpCom1 = new TemplateComponent(componentName1, materialName, TemplateComponentType.Normal, true, "", "");

            TemplateVertex vertex1 = new TemplateVertex(point1, 0.0);
            TemplateVertex vertex2 = new TemplateVertex(point2, 0.0);
            TemplateVertex vertex3 = new TemplateVertex(point3, 0.0);
            TemplateVertex vertex4 = new TemplateVertex(point4, 0.0);

            tmpCom1.AddVertex(vertex1);
            tmpCom1.AddVertex(vertex2);
            tmpCom1.AddVertex(vertex4);
            tmpCom1.AddVertex(vertex3);

            string componentName2 = "Component_name2";
            TemplateComponent tmpCom2 = new TemplateComponent(componentName2, materialName, TemplateComponentType.Normal, true, "", "");

            TemplateVertex vertex5 = new TemplateVertex(point5, 0.0);
            TemplateVertex vertex6 = new TemplateVertex(point6, 0.0);
            TemplateVertex vertex7 = new TemplateVertex(point7, 0.0);
            TemplateVertex vertex8 = new TemplateVertex(point8, 0.0);

            tmpCom2.AddVertex(vertex5);
            tmpCom2.AddVertex(vertex6);
            tmpCom2.AddVertex(vertex8);
            tmpCom2.AddVertex(vertex7);

            templateDef.AddComponent(tmpCom1);
            templateDef.AddComponent(tmpCom2);



            System.Collections.Generic.List<TemplatePoint> pointsOfFeature1 = templateDef.GetPointsByFeatureName(featureName1);

            System.Collections.Generic.List<TemplatePoint> pointsOfFeature2 = templateDef.GetPointsByFeatureName(featureName2);

            System.Collections.Generic.List<TemplatePoint> pointsNoFeature = templateDef.GetPointsByFeatureName("HasNoFeatureName");


            System.Collections.Generic.List<TemplateComponent> comsOfFeature1 = templateDef.GetComponentsByFeatureName(featureName1);

            System.Collections.Generic.List<TemplateComponent> comsOfFeature2 = templateDef.GetComponentsByFeatureName(featureName2);

            System.Collections.Generic.List<TemplateComponent> comsNoFeature = templateDef.GetComponentsByFeatureName("HasNoFeatureName");


            double max = -100000000.0;
            for (int i = 0; i < templateDef.PointCount; i++)
            {
                TemplatePoint pnt = templateDef.GetPointAt(i);
                if (pnt.X > max)
                    max = pnt.X;
            }


            double min = 100000000.0;
            for (int i = 0; i < templateDef.PointCount; i++)
            {
                TemplatePoint pnt = templateDef.GetPointAt(i);
                if (pnt.X < min)
                    min = pnt.X;
            }

            {
                var com = templateDef.GetComponentByName(componentName2);
            }

            {
                var com = templateDef.GetComponentByName("Component_notfind");
            }

            {
                var com = templateDef.GetPointByName(point5.Name);
            }

            {
                var com = templateDef.GetPointByName("point_notfind");
            }

            string xmlFragment = templateDef.GetXml(false);

            //             TemplateDefinition templateDef_Clone = TemplateDefinition.LoadXml(xmlFragment, false);
            //             string xmlFragment2 = templateDef.GetXml(false);


            string tmpLib = "D:\\1.itl";

            if (!System.IO.File.Exists(tmpLib))
            {
                return;
            }
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

            xmlDoc.Load(tmpLib);
            System.Xml.XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Template");


            System.Xml.XmlNode TemplateNode = nodeList[nodeList.Count - 1];
            System.Xml.XmlNode ParentNode = TemplateNode.ParentNode;
            string txmlFragement = TemplateNode.OuterXml;
            TemplateDefinition template = TemplateDefinition.LoadXml(txmlFragement);
            // template.Name = "temp1";

            //Create a new node.
            System.Xml.XmlNode TemplateNodeNew = TemplateNode.Clone();
            TemplateNodeNew.RemoveAll();

            TemplateNodeNew.InnerXml = xmlFragment;


            //Add the node to the document.
            ParentNode.AppendChild(TemplateNodeNew);

            xmlDoc.Save("D:\\1.itl");

            //Assert.AreEqual(xmlFragment, xmlFragment2);
        }
    }
}
