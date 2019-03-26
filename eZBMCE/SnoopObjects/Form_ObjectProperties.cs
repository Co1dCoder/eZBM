using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.MstnPlatformNET.GUI;
using Bentley.MstnPlatformNET.WinForms;
using eZstd.UserControls;

namespace eZBMCE.SnoopObjects
{
    public partial class Form_ObjectProperties :
    System.Windows.Forms.Form
    //    Bentley.MstnPlatformNET.WinForms.Adapter
    {
        public readonly object[] Items;

        #region ---   窗口的打开与关闭

        public Form_ObjectProperties(IEnumerable<object> items)
        {
            InitializeComponent();
            //
            Items = items.ToArray();
            ConstructListBox(Items);
        }

        private void ConstructListBox(object[] items)
        {
            var listItems =
                items.Select(r => new ListControlValue<object>(DisplayedText: GetDisplayName(r), Value: r)).ToArray();
            listBox1.DisplayMember = ListControlValue<object>.DisplayMember;
            listBox1.ValueMember = ListControlValue<object>.ValueMember;
            listBox1.DataSource = listItems;
            //
        }

        public static string GetDisplayName(object obj)
        {
            // 按继承关系从子类到基类排列
            var msg = obj.GetType().Name;
            return msg;
            if (obj is Element)
            {
                return ((Element)obj).TypeName;
            }
            else if (obj is DgnModel)
            {
                return ((DgnModel)obj).ModelName;
            }
            else
            {
                return obj.GetType().FullName;
            }
        }

        private void Form_ObjectProperties_Shown(object sender, EventArgs e)
        {
            propertyGrid1.SelectedGridItemChanged +=
                new SelectedGridItemChangedEventHandler(propertyGrid1_SelectedGridItemChanged);
            //
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            // 手动选择，以触发对应事件
            listBox1.SetSelected(0, true);
            //
        }

        private void Form_ObjectProperties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        #endregion

        #region --- 界面事件

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var obj = listBox1.SelectedItem as ListControlValue<object>;
            if (obj != null)
            {
                propertyGrid1.SelectedGridItemChanged -=
                    new SelectedGridItemChangedEventHandler(propertyGrid1_SelectedGridItemChanged);

                //
                object v = obj.Value;
                v = ConvertIEnumerableToArray(v);
                //
                RefreshObject(v);
                //
                propertyGrid1.SelectedGridItemChanged +=
                    new SelectedGridItemChangedEventHandler(propertyGrid1_SelectedGridItemChanged);
            }
        }

        /// <summary>
        /// 将所有实现 IEnumerable泛型接口 的对象转换为数组对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns>如果输入对象没有实现 IEnumerable泛型接口，则返回原对象</returns>
        private object ConvertIEnumerableToArray(object value)
        {
            bool isEnumerableGener = false;
            var ifs = value.GetType().GetInterfaces();

            foreach (var interf in ifs)
            {
                isEnumerableGener = interf.Name == "IEnumerable`1" &&
                                        interf.Namespace == "System.Collections.Generic";
                if (isEnumerableGener)
                {
                    break;
                }
            }
            if (isEnumerableGener)
            {
                var arr = (value as IEnumerable<object>);
                if (arr != null)
                {
                    return arr.ToArray();
                }
            }
            return value;
        }

        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            var selectedPropertyItem = e.NewSelection;
            var selectedPropertyValue = selectedPropertyItem.Value;
            // 对选择的不同属性进行不同的操作
            if (!CanBeExpanded(selectedPropertyItem))
            {
                return;
            }
            if (selectedPropertyValue is DgnModel)
            {
                var f = new Form_ObjectProperties(new object[] { selectedPropertyValue });
                f.ShowDialog(null);
            }
            else
            {
                var f = new Form_ObjectProperties(new object[] { selectedPropertyValue });
                f.ShowDialog(null);
            }

        }

        #endregion

        #region ---   对选择的属性进行判断与分类

        /// <summary> 系统自带的简单类型，这些类型不用进一步地提取属性 </summary>
        public static Type[] SystemTypes = new Type[]
        {
            typeof (bool), typeof (string), typeof (char),
            typeof (byte), typeof (sbyte), typeof (short), typeof (ushort),
            typeof (int), typeof (uint), typeof (long), typeof (ulong),
            typeof (float), typeof (double), typeof (decimal),
        };

        /// <summary> Bentley 中的一些简单的数据类型，这些类型不用进一步地提取属性 </summary>
        public static Type[] BentleySimpleTypes = new Type[]
        {
            typeof (ElementId), typeof (MSElementType), typeof (LevelId),
        };

        /// <summary> 某项属性是否需要扩展出一个新窗口进行数据展示 </summary>
        public static bool CanBeExpanded(GridItem item)
        {
            var tp = item.Value.GetType();
            if (SystemTypes.Contains(tp) || BentleySimpleTypes.Contains(tp))
            {
                return false;
            }
            if (item.Expandable)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region ---   选择一个新的对象

        private const string FormTitle = "对象属性";

        private void RefreshObject(object obj)
        {
            var tp = obj.GetType();
            Text = $"{FormTitle} - {tp.FullName} - {tp.Module.Name}";
            propertyGrid1.SelectedObject = obj;
            //
        }

        #endregion
    }
}