using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Bentley.CifNET.GeometryModel.SDK.Edit;
using eZBMCE.Cif.TemplateLib;
using eZstd.Data;

namespace eZBMCE.CIF.TemplateLib
{

    public partial class TemplateLibOrganizer : Form
    {

        #region --- Properties

        private string _templateLibPath;

        /// <summary> 横断面模板对应的 xml 文档 </summary>
        public XmlDocument TemplateLibDoc { get; private set; }

        private readonly Dictionary<XmlElement, TreeNode> _categories = new Dictionary<XmlElement, TreeNode>();

        /// <summary> 横断面模板文件中的左/右侧模板 </summary>
        private List<TreeNodeTag> _sideTemplates = new List<TreeNodeTag>();

        /// <summary> 基准模板的名称（不含左右侧信息） </summary>
        private readonly string _templateBaseName;

        /// <summary> 基准模板节点 </summary>
        private readonly XmlElement _baseTemplateElement;

        /// <summary> 指定的横断面模板节点所对应的父节点，Category </summary>
        private readonly XmlElement _categoryNode;

        #endregion

        public TemplateLibOrganizer(string templateLibPath)
        {
            InitializeComponent();
            //
            _templateLibPath = templateLibPath;
            var succ = TryLoadNewTemplateLib(templateLibPath);
        }

        #region --- 加载横断面模板文件到列表

        private void button_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = @"横断面模板文件 (*.itl)|*.itl|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                if (filePath != _templateLibPath)
                {
                    // AlertOpenNewTemplateLib
                    var res = MessageBox.Show(@"在打开新模板文件之前，是否要保存当前模板文件", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.Cancel)
                    {
                        return;
                    }
                    else if (res == DialogResult.Yes)
                    {
                        // 先保存当前文档
                        TemplateLibDoc.Save(_templateLibPath);
                    }

                    // 用户选择Yes或No时，都要再打开新文档
                    var succ = TryLoadNewTemplateLib(filePath);
                    if (succ)
                    {
                        _templateLibPath = filePath;
                    }
                }
            }
        }

        private bool TryLoadNewTemplateLib(string templateLibPath)
        {
            try
            {
                //
                TemplateLibDoc = new System.Xml.XmlDocument();
                TemplateLibDoc.Load(templateLibPath);
                _templateLibPath = templateLibPath;
                //
                treeView_TemplateLib.Nodes.Clear();
                _sideTemplates.Clear();
                _categories.Clear();
                //
                textBox_TemplateLibPath.Text = templateLibPath;
                return ListTemplates(TemplateLibDoc, this.treeView_TemplateLib);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, @"加载横断面模板文件失败", MessageBoxButtons.OK);
                return false;
            }
        }

        /// <summary> 将横断面模板中的内容加载到 TreeView 控件中 </summary>
        /// <param name="templateLibDoc"></param>
        /// <param name="treeView"></param>
        /// <returns></returns>
        private bool ListTemplates(XmlDocument templateLibDoc, TreeView treeView)
        {
            try
            {
                var categories = templateLibDoc.SelectNodes(@"InRoads/TemplateLibrary/Category");
                foreach (XmlElement category in categories)
                {
                    ListCategory(category);
                }
                treeView.Sort();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, @"列举模板失败", MessageBoxButtons.OK);
                return false;
            }
        }

        /// <summary> 将横断面模板中的内容加载到 TreeView 控件中 </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private bool ListCategory(XmlElement category)
        {
            XmlElementTree xmlEleTree = new XmlElementTree();
            XmlElementTree.CheckNeedKeepDigging checkKeepDigging = KeepDigging;
            XmlElementTree.XmlElementManipulator elementDrawer = DrawElementInTreeView;

            xmlEleTree.ExpandElement(category, checkKeepDigging, elementDrawer);
            return true;
        }

        private bool KeepDigging(XmlElement ele)
        {
            return IsCategoryOrTemplate(ele);
        }


        /// <summary> 针对节点的具体信息作出响应操作 </summary>
        private void DrawElementInTreeView(XmlElement ele)
        {
            // 将 XmlElement 转换为 TreeNode
            var treeNodeText = XmlHandler.GetAttributeValue(ele, AttributeNames_Template.name);
            TreeNode tn = new TreeNode(treeNodeText);
            TreeNodeTag tnt = new TreeNodeTag()
            {
                Element = ele,
                Name = treeNodeText,
                IsCategory = IsCategoryOrTemplate(ele),
                TreeNode = tn,
                IsSideTemplate = null,
            };
            tn.Tag = tnt;

            // 针对不同类型数据，个性化定制 TreeNode
            if (tnt.IsCategory && !_categories.ContainsKey(ele))
            {
                _categories.Add(ele, tn);
                //
            }
            else
            {
                tn.ImageKey = @"Template_16.png";
                tn.SelectedImageKey = @"Template_16.png";
                // 
                string templateBaseName;
                var isLeftSide = TemplateDirectionSwitch.IsGradeSideTemplate(ele, out templateBaseName);
                if (isLeftSide != null)
                {
                    tnt.IsSideTemplate = isLeftSide;
                    tn.ForeColor = Color.Blue;

                    _sideTemplates.Add(tnt);

                    //
                    if (isLeftSide.Value)
                    {
                        tn.ImageKey = @"TemplateLeftSide_16.png";
                        tn.SelectedImageKey = @"TemplateLeftSide_16.png";
                    }
                    else
                    {
                        tn.ImageKey = @"TemplateRightSide_16.png";
                        tn.SelectedImageKey = @"TemplateRightSide_16.png";
                    }
                }
            }
            // 放置 TreeNode
            var parentNd = ele.ParentNode as XmlElement;
            if (_categories.ContainsKey(parentNd))
            {
                var parentTreeNd = _categories[parentNd];
                parentTreeNd.Nodes.Add(tn);
            }
            else
            {
                treeView_TemplateLib.Nodes.Add(tn);
            }

            return;
        }

        private static bool IsCategoryOrTemplate(XmlElement ele)
        {
            return ele.Name == "Category";
        }

        #endregion

        #region --- 转换模板方向

        private void button_Apply_Click(object sender, EventArgs e)
        {
            var treeNodeToHandle = treeView_TemplateLib.SelectedNode;
            if (treeNodeToHandle == null)
            {
                SetPromptLabel($"请先选择一个模板");
                return;
            }
            var ndt = treeNodeToHandle.Tag as TreeNodeTag;
            if (ndt.IsSideTemplate != null)
            {
                bool toLeft = ndt.IsSideTemplate.Value;//  radioButton_toLeft.Checked;
                XmlElement templateNodeToHandle;
                var switchType = SwitchTemplateDirection(ndt.Element, toLeft, out templateNodeToHandle);
                if (switchType == TemplateDirectionSwitch.SwitchType.UnSwitched)
                {

                }
                else
                {
                    var name = XmlHandler.GetAttributeValue(templateNodeToHandle, AttributeNames_Template.name);

                    XmlElement parentNd = templateNodeToHandle.ParentNode as XmlElement;
                    RefreshTreeFromChild(parentNd, treeNodeToHandle);
                    switch (switchType)
                    {
                        case TemplateDirectionSwitch.SwitchType.Appended:
                            SetPromptLabel($"添加了新模板\"{name}\"");
                            break;
                        case TemplateDirectionSwitch.SwitchType.ReplacedMySelf:
                            SetPromptLabel($"修改了模板\"{name}\"本身");

                            break;
                        case TemplateDirectionSwitch.SwitchType.ReplacedDuplicated:
                            SetPromptLabel($"修改了同名模板\"{name}\"");

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 刷新 Treeview 与 xml 的绑定关系
        /// </summary>
        /// <param name="parentNd"> 要刷新的文件夹 </param>
        /// <param name="templateToSelect"></param>
        private void RefreshTreeFromChild(XmlElement parentNd, TreeNode templateToSelect)
        {
            if (_categories.ContainsKey(parentNd))
            {
                // 先清空 parent Treenode 节点
                var parentTreeNd = _categories[parentNd];
                parentTreeNd.Nodes.Clear();

                // 重新添加所有的子节点
                foreach (XmlElement template in parentNd.ChildNodes)
                {
                    DrawElementInTreeView(template);
                }
                treeView_TemplateLib.SelectedNode = templateToSelect;
            }
        }

        /// <summary> 转换某个横断面模板的左右方向 </summary>
        /// <param name="templateToSwitch"></param>
        /// <param name="toLeft"></param>
        /// <param name="templateNodeToHandle"></param>
        /// <returns></returns>
        private TemplateDirectionSwitch.SwitchType SwitchTemplateDirection(XmlElement templateToSwitch, bool toLeft, out XmlElement templateNodeToHandle)
        {

            templateNodeToHandle = null;
            string templateBaseName = null;

            TemplateDirectionSwitch.RefreshSwitchPool();
            // 对每一个搜索到的 Template 进行判断与修改

            var isSideTemplate = TemplateDirectionSwitch.IsGradeSideTemplate(templateToSwitch, out templateBaseName);
            if (isSideTemplate == null) { return TemplateDirectionSwitch.SwitchType.UnSwitched; }
            //
            var dirSwitcher = new TemplateDirectionSwitch(templateToSwitch, templateBaseName, isLeft: isSideTemplate.Value, toLeft: toLeft);
            TemplateDirectionSwitch.SwitchType switchType = dirSwitcher.GetTemplateNodeToModify(out templateNodeToHandle);
            //
            if (switchType != TemplateDirectionSwitch.SwitchType.UnSwitched)
            {
                dirSwitcher.SwitchTemplateDirection(templateNodeToHandle);
            }
            return switchType;
        }

        #endregion

        #region --- UI

        private void button_Save_Click(object sender, EventArgs e)
        {
            TemplateLibDoc.Save(_templateLibPath);
            SetPromptLabel("文件成功保存到了" + _templateLibPath);
            DialogResult = DialogResult.OK;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            var treeNodeToRemove = treeView_TemplateLib.SelectedNode;
            if (treeNodeToRemove == null)
            {
                SetPromptLabel($"请先选择一个模板");
                return;
            }
            var ndt = treeNodeToRemove.Tag as TreeNodeTag;
            if (ndt.IsCategory)
            {
                SetPromptLabel($"无法删除文件夹。");
            }
            else if (ndt.IsSideTemplate != null)
            {
                XmlElement parentNd = ndt.Element.ParentNode as XmlElement;
                parentNd.RemoveChild(ndt.Element);
                RefreshTreeFromChild(parentNd, treeNodeToRemove.Parent);
                SetPromptLabel($"删除了模板 {ndt.Name}");
            }
            else
            {
                SetPromptLabel($"无法删除非路基两边坡模板，请确定模板名称以 _LM 或 _RM 结尾。");
            }

        }
        #endregion

        private int _promptIndex;
        private void SetPromptLabel(string prompt)
        {
            _promptIndex += 1;
            label_Prompt.Text = $"提示({_promptIndex}): {prompt}";
        }

        #region ---   上/下列举两侧边坡模板

        private string _lastSelectedSideTemplateFullPath;

        private class SideTemplateTagComparere : IComparer<TreeNodeTag>
        {
            private readonly bool _nodeAscend;

            public SideTemplateTagComparere(bool nodeAscend)
            {
                _nodeAscend = nodeAscend;
            }

            public int Compare(TreeNodeTag x, TreeNodeTag y)
            {
                if (_nodeAscend)
                {
                    return String.Compare(x.TreeNode.FullPath, y.TreeNode.FullPath, StringComparison.CurrentCulture);
                }
                else
                {
                    return String.Compare(y.TreeNode.FullPath, x.TreeNode.FullPath, StringComparison.CurrentCulture);
                }
            }
        }

        private void treeView_TemplateLib_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView_TemplateLib.SelectedNode != null)
            {
                var treeNodeToHandle = treeView_TemplateLib.SelectedNode.Tag as TreeNodeTag;
                if ((treeNodeToHandle != null) && (treeNodeToHandle.IsSideTemplate != null))
                {
                    _lastSelectedSideTemplateFullPath = treeNodeToHandle.TreeNode.FullPath;
                }
            }
        }

        private void button_PreviousSideTemplate_Click(object sender, EventArgs e)
        {
            var previousSideTempateTreenode = GetNextSideTempateTreenode(false);
            if (previousSideTempateTreenode != null)
            {
                treeView_TemplateLib.SelectedNode = previousSideTempateTreenode;
            }
            treeView_TemplateLib.Select();
        }

        private void button_NextSideTemplate_Click(object sender, EventArgs e)
        {
            var nextSideTempateTreenode = GetNextSideTempateTreenode(true);
            if (nextSideTempateTreenode != null)
            {
                treeView_TemplateLib.SelectedNode = nextSideTempateTreenode;
            }
            treeView_TemplateLib.Select();
        }

        private TreeNode GetNextSideTempateTreenode(bool nextOrPrevious)
        {
            TreeNode nextSideTempateTreenode = null;
            // 清理出 Treeview 中有效的 表示某侧模板的 treenode
            string fullPath;
            var validTreenodeInTreeview = new List<TreeNodeTag>();
            foreach (var sideTemplate in _sideTemplates)
            {
                try
                {
                    fullPath = sideTemplate.TreeNode.FullPath;
                    validTreenodeInTreeview.Add(sideTemplate);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            _sideTemplates = validTreenodeInTreeview;
            if (_sideTemplates.Count == 0) { return null; }
            _sideTemplates.Sort(new SideTemplateTagComparere(nextOrPrevious));

            // 
            if (string.IsNullOrEmpty(_lastSelectedSideTemplateFullPath))
            {
                nextSideTempateTreenode = _sideTemplates.First().TreeNode;
                _lastSelectedSideTemplateFullPath = nextSideTempateTreenode.FullPath;
                return nextSideTempateTreenode;
            }
            else
            {
                var matchedNode = _sideTemplates.FirstOrDefault(r => r.TreeNode.FullPath == _lastSelectedSideTemplateFullPath);
                if (matchedNode != null)
                {
                    var ind = _sideTemplates.IndexOf(matchedNode);
                    if (ind == _sideTemplates.Count - 1)
                    {
                        // 还是选择最后一个
                        _lastSelectedSideTemplateFullPath = matchedNode.TreeNode.FullPath;
                        SetPromptLabel(nextOrPrevious ? @"已经搜索到最后一个侧面模板" : @"已经搜索到第一个侧面模板");
                        return matchedNode.TreeNode;
                    }
                    else
                    {
                        // 选择下一个
                        matchedNode = _sideTemplates[ind + 1];
                        _lastSelectedSideTemplateFullPath = matchedNode.TreeNode.FullPath;
                        return matchedNode.TreeNode;
                    }
                }
                else
                {
                    nextSideTempateTreenode = _sideTemplates.First().TreeNode;
                    _lastSelectedSideTemplateFullPath = nextSideTempateTreenode.FullPath;
                    return nextSideTempateTreenode;
                }
            }
            return nextSideTempateTreenode;
        }

        #endregion

        private class TreeNodeTag
        {
            public XmlElement Element;
            public TreeNode TreeNode;

            /// <summary> true表示是文件夹，false 表示是 Template </summary>
            public bool IsCategory;
            /// <summary> true表示是路基左侧，false 表示是 路基右侧 </summary>
            public bool? IsSideTemplate;

            public string Name;
        }

    }
}
