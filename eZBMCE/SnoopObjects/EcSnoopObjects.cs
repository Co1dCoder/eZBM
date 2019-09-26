using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Bentley.CifNET.GeometryModel.SDK;
using Bentley.CifNET.SDK;
using Bentley.DgnPlatformNET;
using eZBMCE.AddinManager;
using eZBMCE.SnoopObjects;
using eZBMCE.Utilities;
using Application = Bentley.Interop.MicroStationDGN.Application;
using BM = Bentley.MstnPlatformNET;
using BMI = Bentley.MstnPlatformNET.InteropServices;
using Element = Bentley.DgnPlatformNET.Elements.Element;


namespace eZBMCE.Addins.SnoopObjects
{
    /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    internal sealed class EcSnoopObjects : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"SnoopObjects";

        private const string CommandText = @"显示选择的对象的属性值";
        private const string CommandDescription = @"显示选择的对象的属性值";

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void SnoopObjects()
        {
            AddinManagerDebuger.ExecuteCommand(SnoopObjects);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new EcSnoopObjects();
            return AddinManagerDebuger.DebugInAddinManager(s.SnoopObjects, ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> <seealso cref="CommandDescription"/> </summary>
        public AddinManagerDebuger.ExternalCmdResult SnoopObjects(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            var prod = CheckProductType();
            switch (prod)
            {
                case ProductType.OpenRoadsDesigner:
                    return SnoopObjectsInOrd(docMdf);
                    break;
                default:
                    return SnoopObjectsInMS(docMdf);
                    break;
            }
            return AddinManagerDebuger.ExternalCmdResult.Cancel;
        }


        private AddinManagerDebuger.ExternalCmdResult SnoopObjectsInOrd(DocumentModifier docMdf)
        {
            ConsensusConnection sdkCon;//= ConsensusConnection.Create(BM.Session.Instance.GetActiveDgnModelRef());
            sdkCon = new ConsensusConnection(BM.Session.Instance.GetActiveDgnModelRef());
            if (sdkCon == null)
            {
                return AddinManagerDebuger.ExternalCmdResult.Cancel;
            }

            //
            var selection = Utils.GetSelection();
            if (selection.Length > 0)
            {
                // 尝试将MS中的元素转换为ORD中的元素
                var objs = TryConvertMSElementToORDElement(sdkCon, selection);
                var frm = new Form_ObjectProperties(objs);
                frm.Show(null);
                return AddinManagerDebuger.ExternalCmdResult.Commit;
            }
            else
            {
                GeometricModel gm = sdkCon.GetActiveGeometricModel();// .GetActive();
                if (gm == null)
                {
                    MessageBox.Show(@"模型中没有任何ORD对象");
                    return SnoopObjectsInMS(docMdf);
                    return AddinManagerDebuger.ExternalCmdResult.Cancel;
                }
                var frm = new Form_ObjectProperties(new object[] { gm });
                frm.Show(null);
            }
            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        private AddinManagerDebuger.ExternalCmdResult SnoopObjectsInMS(DocumentModifier docMdf)
        {
            //
            var selection2 = Utils.GetSelection();
            if (selection2.Length > 0)
            {
                var frm = new Form_ObjectProperties(selection2);
                frm.Show(null);
                return AddinManagerDebuger.ExternalCmdResult.Commit;
            }
            else
            {
                var model = BM.Session.Instance.GetActiveDgnModelRef();
                if (model == null)
                    return AddinManagerDebuger.ExternalCmdResult.Cancel;

                var frm = new Form_ObjectProperties(new object[] { model });
                frm.Show(null);
            }
            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }

        private enum ProductType
        {
            MicroStation,
            OpenRoadsDesigner,
        }


        /// <summary> 当前运行的程序 </summary>
        /// <returns></returns>
        private ProductType CheckProductType()
        {
            string name = Process.GetCurrentProcess().ProcessName;
            switch (name)
            {
                case "OpenRoadsDesigner":
                    return ProductType.OpenRoadsDesigner;
                    break;
            }
            return ProductType.MicroStation;
        }

        /// <summary>
        /// 尝试将MS中的元素转换为ORD中的元素
        /// </summary>
        /// <param name="sdkCon"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        private static object[] TryConvertMSElementToORDElement(ConsensusConnection sdkCon, Element[] objs)
        {
            var count = objs.Length;
            var newObjs = new object[count];
            for (int i = 0; i < count; i++)
            {
                Element elem = objs[i];
                // ConsensusItem me = ConsensusItem.CreateFromDynamicInstanceId(elem.ElementId.ToString());
                bool isOrdEnt = false;
                if (!isOrdEnt)
                {
                    FeaturizedModelEntity ordEnt = FeaturizedModelEntity.CreateFromElement(sdkCon, elem);
                    if (ordEnt != null)
                    {
                        isOrdEnt = true;
                        newObjs[i] = ordEnt;
                    }
                }

                // 处理其他 ORD 中特有的类型
                if (!isOrdEnt)
                {
                    Corridor ordEnt = Corridor.CreateFromElement(sdkCon, elem); // 若转换不了则返回 null;
                    if (ordEnt != null)
                    {
                        // Corridor 与 Linear Template 在代码中都属于Corridor 对象，但是 Linear Template 对象并不含有Corridor对象的所有特性，
                        // 如果按Corridor的接口来读取 Linear Template 对象的信息的话，会导致ORD崩溃，
                        // 所以这里再作一个特殊处理——屏蔽掉 Linear Template 所对应的 Corridor。
                        if (!ordEnt.Element.Description.StartsWith("Linear Template"))
                        {
                            isOrdEnt = true;
                            newObjs[i] = ordEnt;
                        }
                    }
                }
                // 处理其他 ORD 中特有的类型
                if (!isOrdEnt)
                {
                    CurveWidening cw = CurveWidening.CreateFromElement(sdkCon, elem);
                    if (cw != null)
                    {
                        isOrdEnt = true;
                        newObjs[i] = cw;
                    }
                }
                // 处理其他 ORD 中特有的类型
                if (!isOrdEnt)
                {
                    PointControl pc = PointControl.CreateFromElement(sdkCon, elem);
                    if (pc != null)
                    {
                        isOrdEnt = true;
                        newObjs[i] = pc;
                    }
                }
                // 处理其他 ORD 中特有的类型
                if (!isOrdEnt)
                {
                    TemplateDrop td = TemplateDrop.CreateFromElement(sdkCon, elem);
                    if (td != null)
                    {
                        isOrdEnt = true;
                        newObjs[i] = td;
                    }
                }
                // 最终处理
                if (!isOrdEnt)
                {
                    // 保持原来的 Microstation 中的Element类型
                    newObjs[i] = objs[i];
                }
            }
            return newObjs;
        }
    }
}