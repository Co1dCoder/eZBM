using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using Bentley.CifNET.GeometryModel.SDK.Edit;
using Bentley.DgnPlatformNET;
using eZBMCE.AddinManager;
using eZBMCE.CIF.TemplateLib;
using BM = Bentley.MstnPlatformNET;
using BMI = Bentley.MstnPlatformNET.InteropServices;
using BIM = Bentley.Interop.MicroStationDGN;

namespace eZBMCE.Cif.TemplateLib
{
    /// <summary> 计算选择的所有曲线的面积与长度之和 </summary>
    [EcDescription(CommandDescription)]
    [BM.AddInAttribute(MdlTaskID = "eZBMCEAddinManager")]
    //[BM.AddInAttribute(KeyinTree = "csAddins.commands.xml",MdlTaskID = "csAddins_xml")]
    public class Ec_SwitchTemplateDirection : IBMExCommand
    {
        #region --- 命令设计

        /// <summary> 命令行命令名称，同时亦作为命令语句所对应的C#代码中的函数的名称 </summary>
        public const string CommandName = @"SwitchTemplateDirection";

        private const string CommandDescription = @"根据横断面模板的文件名后缀（_LM或_RM）来转换横断面模板的左右方向";

        /// <summary> 注意此函数的名称要与<seealso cref="CommandName"/>相同</summary>
        public void SwitchTemplateDirection()
        {
            AddinManagerDebuger.ExecuteCommand(SwitchTemplateDirection);
        }

        /// <summary> 在 AddinMananger 中以外部命令的形式调用和调试 </summary>
        public ExternalCommandResult Execute(ref string errorMessage,
            ref IList<ElementId> elementSet)
        {
            var s = new Ec_SwitchTemplateDirection();
            return AddinManagerDebuger.DebugInAddinManager(s.SwitchTemplateDirection, ref errorMessage, ref elementSet);
        }

        #endregion

        private DocumentModifier _docMdf;

        /// <summary> 遍历廊道 </summary>
        public AddinManagerDebuger.ExternalCmdResult SwitchTemplateDirection(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            var templateLibPath = TemplateLibrary.GetDefaultTemplateLibraryPath();
            TemplateLibOrganizer form_tempLibList = new TemplateLibOrganizer(templateLibPath);
            var res = form_tempLibList.ShowDialog();
            if (res == DialogResult.OK)
            {

            }

            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }


        public AddinManagerDebuger.ExternalCmdResult SwitchTemplateDirectionFromFile(DocumentModifier docMdf)
        {
            _docMdf = docMdf;

            var templateLibPath = TemplateLibrary.GetDefaultTemplateLibraryPath();
            var templateLibDoc = new System.Xml.XmlDocument();
            templateLibDoc.Load(templateLibPath);
            // 搜索多个 Template 节点
            System.Xml.XmlNodeList nodeList = templateLibDoc.GetElementsByTagName("Template");

            bool toLeft = true;
            TemplateDirectionSwitch dirSwitcher;
            string templateBaseName = null;
            int templatesCount = nodeList.Count;

            TemplateDirectionSwitch.RefreshSwitchPool();
            // 对每一个搜索到的 Template 进行判断与修改
            for (int i = 0; i < templatesCount; i++)
            {
                XmlElement templateNode = nodeList[i] as XmlElement;

                var isSideTemplate = TemplateDirectionSwitch.IsGradeSideTemplate(templateNode, out templateBaseName);
                if (isSideTemplate == null) { continue; }
                //
                dirSwitcher = new TemplateDirectionSwitch(templateNode, templateBaseName, isLeft: isSideTemplate.Value, toLeft: toLeft);
                XmlElement templateNodeToHandle;
                TemplateDirectionSwitch.SwitchType switchType = dirSwitcher.GetTemplateNodeToModify(out templateNodeToHandle);
                //
                if (switchType != TemplateDirectionSwitch.SwitchType.UnSwitched)
                {
                    dirSwitcher.SwitchTemplateDirection(templateNodeToHandle);
                }
            }

            // 保存数据
            templateLibDoc.Save(templateLibPath);
            TemplateDirectionSwitch.RefreshSwitchPool();

            return AddinManagerDebuger.ExternalCmdResult.Commit;
        }


    }
}