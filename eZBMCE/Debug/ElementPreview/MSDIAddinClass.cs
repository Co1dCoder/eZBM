//--------------------------------------------------------------------------------------+
//
//    $Source: MSDIAddinClass.cs $
// 
//    $Copyright: (c) 2018 Bentley Systems, Incorporated. All rights reserved. $
//
//---------------------------------------------------------------------------------------+

//---------------------------------------------------------------------------------------+
//	Using Directives
//---------------------------------------------------------------------------------------+
using Bentley.GeometryNET;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.MstnPlatformNET;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MSDIAddin
{
    class MSDIAddinClass
    {
        //--------------------------------------------------------------------------------------
        // @description   This function does...
        // @bsimethod                                                    Bentley
        //+---------------+---------------+---------------+---------------+---------------+------
        public static void HelloWorld(string unparsed)
        {
            MessageCenter.Instance.ShowInfoMessage("Hello MSDI", "Hello MSDI", true);
        }
        public static void MoveElement(string unparsed)
        {
            DgnFile dgnFile = Session.Instance.GetActiveDgnFile();
            MessageBox.Show(dgnFile.GetFileName());

            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            Element myElem = dgnModel.FindElementById((ElementId)913L);

            DTransform3d trans = DTransform3d.Identity;
            trans.Translation = new DPoint3d(10000, 20000, 0);
            TransformInfo transInfo = new TransformInfo(trans);
            myElem.ApplyTransform(transInfo);

            myElem.ReplaceInModel(myElem);
        }
        public static void FileAnalyze(string unparsed)
        {
            DgnFile dgnFile = Session.Instance.GetActiveDgnFile();
            DgnModel dicModel = dgnFile.GetDictionaryModel();
            uint nCntAll = dicModel.GetElementCount(DgnModelSections.All);
            uint nCntDic = dicModel.GetElementCount(DgnModelSections.Dictionary);

            string msg = "nCntAll = " + nCntAll.ToString() + ", nCntDic = " + nCntDic.ToString();
            MessageCenter.Instance.ShowMessage(MessageType.Info, msg, msg, MessageAlert.None);

            ModelElementsCollection elemCol = dicModel.GetControlElements();
            foreach (Element el in elemCol)
            {
                msg = "ElemType = " + el.TypeName + ", ElemId = " + el.ElementId.ToString();
                MessageCenter.Instance.ShowMessage(MessageType.Info, msg, msg, MessageAlert.None);
            }
        }
        public static void CreateLine(string unparsed)
        {
            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            DSegment3d seg = new DSegment3d(0, 0, 10000, 20000);
            LineElement myLine = new LineElement(dgnModel, null, seg);
            myLine.AddToModel();
        }
        public static void CreateCone(string unparsed)
        {
            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            ConeElement myCone = new ConeElement(dgnModel, null, 0, 5000, DPoint3d.Zero, 
                                                 new DPoint3d(0, 0, -10000), DMatrix3d.Identity, true);
            myCone.AddToModel();
        }
        public static void CreateColorIndexedMesh(string unparsed)
        {
            PolyfaceHeader polyHeader = new PolyfaceHeader();
            polyHeader.NumberPerFace = 0;

            var pts = new List<DPoint3d>();
            pts.Add(new DPoint3d(-57735, 0, -81649));
            pts.Add(new DPoint3d(-57735, 0, 81649));
            pts.Add(new DPoint3d(57735, -81649, 0));
            pts.Add(new DPoint3d(57735, 81649, 0));
            polyHeader.Point = pts;

            var clrs = new List<uint>();
            clrs.Add(0x000000ff);             // RED
            clrs.Add(0x0000ff00);             // GREEN
            clrs.Add(0x00ff0000);             // BLUE
            clrs.Add(0x0000ffff);
            polyHeader.IntColor = clrs;

            polyHeader.ActivateVectorsForIndexing(polyHeader);

            var indices = new List<int>();
            indices.Add(1); indices.Add(2); indices.Add(4);  //one-based
            polyHeader.AddIndexedFacet(indices, null, null, indices);
            indices.Clear();
            indices.Add(2); indices.Add(3); indices.Add(4);
            polyHeader.AddIndexedFacet(indices, null, null, indices);
            indices.Clear();
            indices.Add(3); indices.Add(2); indices.Add(1);
            polyHeader.AddIndexedFacet(indices, null, null, indices);
            indices.Clear();
            indices.Add(1); indices.Add(4); indices.Add(3);
            polyHeader.AddIndexedFacet(indices, null, null, indices);

            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            MeshHeaderElement meshElem = new MeshHeaderElement(dgnModel, null, polyHeader);
            meshElem.AddToModel();
        }
        public static void CreateComplexChain(string unparsed)
        {
            const double degreeToRadians = 3.1415926 / 180;

            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            ModelInfo modelInfo = dgnModel.GetModelInfo();
            double uor = modelInfo.UorPerMaster;

            ComplexStringElement csElem = new ComplexStringElement(dgnModel, null);

            DSegment3d seg = new DSegment3d(-1.45 * uor, 0.55 * uor, 0.78 * uor, 2.56 * uor);
            LineElement line1 = new LineElement(dgnModel, null, seg);
            csElem.AddComponentElement(line1);

            DPoint3d center = new DPoint3d(1.45 * uor, 1.82 * uor);
            ArcElement arc = new ArcElement(dgnModel, null, center, uor, uor,
                                     131.88 * degreeToRadians, 0, -103.2 * degreeToRadians);
            csElem.AddComponentElement(arc);

            seg = new DSegment3d(2.33 * uor, 2.3 * uor, 3.63 * uor, -0.09 * uor);
            LineElement line2 = new LineElement(dgnModel, null, seg);
            csElem.AddComponentElement(line2);

            csElem.AddToModel();
        }
        public static void CreateText(string unparsed)
        {
            DgnFile dgnFile = Session.Instance.GetActiveDgnFile();
            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            DgnTextStyle dts = new DgnTextStyle("test", dgnFile);
            dts.SetProperty(TextStyleProperty.Width, 2000);
            dts.SetProperty(TextStyleProperty.Height, 4000);

            TextBlockProperties tbProps = new TextBlockProperties(dts, dgnModel);
            tbProps.IsViewIndependent = true;
            ParagraphProperties pProps = new ParagraphProperties(dts, dgnModel);
            RunProperties runProps = new RunProperties(dts, dgnModel);
            TextBlock TxtBlock = new TextBlock(tbProps, pProps, runProps, dgnModel);
            TxtBlock.SetUserOrigin(DPoint3d.Zero);
            TxtBlock.AppendText("My Text String");

            TextElement txtElem = TextHandlerBase.CreateElement(null, TxtBlock) as TextElement;
            txtElem.AddToModel();
        }
        public static void ShowMessage (string msg)
        {
            MessageCenter.Instance.ShowInfoMessage(msg, msg, true);
        }
        public static void GetLength(string unparsed)  // Need to consider 5 cases
        {
            ulong id;
            try
            {
                id = ulong.Parse(unparsed);
            }
            catch
            {
                ShowMessage("未提供ElemId作为命令参数或提供的参数不合法");
                return;
            }
            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            Element myElem = dgnModel.FindElementById((ElementId)id);
            if (null == myElem)
            {
                ShowMessage("所提供ElemId无效");
                return;
            }
            CurvePathQuery myQuery = CurvePathQuery.GetAsCurvePathQuery(myElem);
            if (null == myQuery)
            {
                ShowMessage("本工具不支持该类型元素");
                return;
            }
            CurveVector cv = myQuery.GetCurveVector();

            double length;
            DPoint3d centroid;
            cv.WireCentroid(out length, out centroid);
            double uor = dgnModel.GetModelInfo().UorPerMeter;
            length /= uor;
            ShowMessage("长度=" + length.ToString() + "米");
        }
        public static void MstnControlDemo(string unparsed)
        {
            MstnControlForm myForm = new MstnControlForm();
            myForm.Show();
        }
    }
}