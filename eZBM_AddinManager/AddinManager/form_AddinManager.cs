﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Bentley.MstnPlatformNET.WinForms;

namespace eZBMCE.AddinManager
{
    internal partial class form_AddinManager : Form
    {

        #region ---   构造函数

        private static form_AddinManager _uniqueForm;
        public static form_AddinManager GetUniqueForm()
        {
            if (_uniqueForm == null)
            {
                _uniqueForm = new form_AddinManager();
            }
            //
            return _uniqueForm;

        }

        /// <summary> 构造函数 </summary>
        private form_AddinManager()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Disposed += OnDisposed;
            //
            _nodesInfo = new Dictionary<AddinManagerAssembly, List<IBMExCommand>>(new AssemblyComparer());
            //
        }

        #endregion

        #region ---   窗口的关闭

        private void form_AddinManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void form_AddinManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void OnDisposed(object sender, EventArgs eventArgs)
        {
            _uniqueForm = null;
            _nodesInfo.Clear();
            _nodesInfo = null;
        }

        #endregion

        #region ---   TreeView 的刷新 与 _nodesInfo同步

        /// <summary> 与 TreeView 同步的节点数据 </summary>
        private Dictionary<AddinManagerAssembly, List<IBMExCommand>> _nodesInfo;

        /// <summary> 与 TreeView 同步的节点数据 </summary>
        internal Dictionary<AddinManagerAssembly, List<IBMExCommand>> NodesInfo
        {
            get
            {
                return _nodesInfo;
            }
        }

        /// <summary>
        /// 将所有的外部命令刷新到列表控件中
        /// </summary>
        /// <param name="nodesInfo">字典中每一个程序集对应其中的多个外部命令</param>
        internal void RefreshTreeView(Dictionary<AddinManagerAssembly, List<IBMExCommand>> nodesInfo)
        {
            if (nodesInfo != null)
            {
                if (nodesInfo.Comparer.GetType() != typeof(AssemblyComparer))
                {
                    throw new ArgumentException("The dictionary used to synchronize the treeview must have an \"AssemblyComparer\".");
                }
                treeView1.Nodes.Clear();
                //
                foreach (var ndInfo in nodesInfo)
                {
                    AddinManagerAssembly asm = ndInfo.Key;
                    List<IBMExCommand> methods = ndInfo.Value;

                    // 添加新的程序集
                    var module = asm.Assembly.ManifestModule;
                    TreeNode tnAss = new TreeNode(text: module.ScopeName);
                    tnAss.Tag = asm;
                    treeView1.Nodes.Add(tnAss);

                    // 添加此程序集中所有的外部命令
                    methods.Sort(new ExCmdCompare());
                    foreach (IBMExCommand m in methods)
                    {
                        TreeNode tnMethod = new TreeNode(m.GetType().FullName);
                        tnMethod.Tag = m;
                        tnAss.Nodes.Add(tnMethod);
                    }
                }

                // 刷新
                _nodesInfo = nodesInfo;
                //
                treeView1.ExpandAll();
                treeView1.Refresh();
            }
        }

        /// <summary> 外部命令进行比较的方法 </summary>
        private class ExCmdCompare : IComparer<IBMExCommand>
        {
            int IComparer<IBMExCommand>.Compare(IBMExCommand x, IBMExCommand y)
            {
                return String.Compare(x.GetType().FullName, y.GetType().FullName, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary> 将从一个 Assembly 中加载进来的所有有效的外部命令同步到 _nodesInfo 中 </summary>
        /// <param name="methods"></param>
        private void AddMethodsInOneAssembly(string assemblyPath, List<IBMExCommand> methods)
        {
            AddinManagerAssembly asm;
            if (methods.Any())
            {
                asm = new AddinManagerAssembly(assemblyPath, methods.First().GetType().Assembly);
                //
                List<IBMExCommand> mds = new List<IBMExCommand>();
                foreach (var m in methods)
                {
                    mds.Add(m);
                }
                // 
                if (_nodesInfo.ContainsKey(asm))  // 覆盖式刷新
                {
                    _nodesInfo[asm] = mds;
                }
                else  // 直接进行添加就可以了
                {
                    _nodesInfo.Add(asm, mds);
                }
            }
        }

        private void RemoveAssembly(TreeNode ndAss)
        {
            if (ndAss.Level != 0)
            {
                throw new ArgumentException("this is not a node representing an assembly.");
            }
            //
            AddinManagerAssembly asm = ndAss.Tag as AddinManagerAssembly;
            _nodesInfo.Remove(asm);
        }

        private void RemoveMethod(TreeNode ndAss)
        {
            if (ndAss.Level != 1)
            {
                throw new ArgumentException("this is not a node representing an method.");
            }
            //
            AddinManagerAssembly asm = ndAss.Parent.Tag as AddinManagerAssembly;

            IBMExCommand mtd = ndAss.Tag as IBMExCommand;
            //
            _nodesInfo[asm].Remove(mtd);
        }

        #endregion

        #region ---   加载

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            string[] dllPaths = ChooseOpenDll("Choose an Addin file");
            bool hasNewMethodAdded = false;
            //
            if (dllPaths != null)
            {
                foreach (string dllPath in dllPaths)
                {
                    if (string.IsNullOrEmpty(dllPath)) { continue; }
                    //
                    var methods = ExCommandFinder.RetriveExternalCommandsFromAssembly(dllPath);
                    if (methods.Any())
                    {
                        // 更新 Dictionary
                        AddMethodsInOneAssembly(dllPath, methods);
                        hasNewMethodAdded = true;
                    }
                }
            }

            if (hasNewMethodAdded)
            {
                // 刷新界面
                RefreshTreeView(_nodesInfo);
            }
        }

        private void button_Reload_Click(object sender, EventArgs e)
        {
            TreeNode nd = treeView1.SelectedNode;
            TreeNode ndAss = null;
            if (nd == null) return;
            //
            if (nd.Level == 0) // 移除程序集
            {
                ndAss = nd;
            }
            else if (nd.Level == 1)// 移除某个方法所对应的程序集
            {
                ndAss = nd.Parent;
            }
            AddinManagerAssembly mtd = ndAss.Tag as AddinManagerAssembly;
            string assFullPath = mtd.Path;

            // 重新加载此程序集
            if (!string.IsNullOrEmpty(assFullPath))
            {
                bool hasNewMethodAdded = false;
                //
                var methods = ExCommandFinder.RetriveExternalCommandsFromAssembly(assFullPath);
                if (methods.Any())
                {
                    // 更新 Dictionary
                    AddMethodsInOneAssembly(assFullPath, methods);
                    hasNewMethodAdded = true;
                }

                if (hasNewMethodAdded)
                {
                    // 刷新界面
                    RefreshTreeView(_nodesInfo);
                }
            }
        }

        /// <summary> 通过选择文件对话框选择要进行数据提取的CAD文件 </summary>
        /// <returns> 要进行数据提取的CAD文件的绝对路径 </returns>
        public static string[] ChooseOpenDll(string title)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = title,
                CheckFileExists = true,
                AddExtension = true,
                Filter = @"Assembly files(*.dll; *.exe)| *.dll; *.exe",
                FilterIndex = 2,
                Multiselect = true,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileNames;
            }
            return null;
        }


        #endregion

        #region ---   移除

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            TreeNode nd = treeView1.SelectedNode;
            if (nd == null) return;
            //
            if (nd.Level == 0)  // 移除程序集
            {
                RemoveAssembly(nd);
                nd.Remove();
            }
            else if (nd.Level == 1)// 移除某个方法
            {
                RemoveMethod(nd);
                nd.Remove();
            }
        }

        #endregion

        #region ---   运行

        private void RunExternalCommand(TreeNode ndCommand)
        {
            var exCommand = ndCommand.Tag as IBMExCommand;
            AddinManagerAssembly asm = ndCommand.Parent.Tag as AddinManagerAssembly;
            //
            string assemblyPath = asm.Path;

            //  ---------- 将窗口缩小 ----------
            if (checkBox_MinimizeWhileRun.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            // 执行外部命令
            ExCommandExecutor.InvokeExternalCommand(assemblyPath, exCommand);
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            TreeNode nd = treeView1.SelectedNode;
            if (nd != null && nd.Level == 1)  // 选择了某一个方法
            {
                RunExternalCommand(nd);
            }
        }


        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var nd = e.Node;
            if (nd != null && nd.Level == 1)  // 选择了某一个方法
            {
                RunExternalCommand(nd);
            }
        }

        #endregion

        /// <summary> 提取出 TreeView中节点对应的外部命令上的描述字符 </summary>
        private void ShowExCommandDescription(object sender, TreeNodeMouseClickEventArgs e)
        {
            var nd = e.Node;
            string description = "描述：";
            if (nd != null && nd.Level == 1)  // 选择了某一个方法
            {
                // 提取此方法所在的类的对应的描述
                var exCommand = nd.Tag as IBMExCommand;
                var atts = exCommand.GetType().GetCustomAttributes(typeof(EcDescriptionAttribute), false);
                if (atts.Length > 0)
                {
                    var att = atts.First() as EcDescriptionAttribute;
                    description += att.Description;
                }
            }
            label_Description.Text = description;
        }

    }
}
