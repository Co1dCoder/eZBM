using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eZstd.Miscellaneous;
using MsdiTransportation.SubgradeComponent;
using MsdiTransportation.SubgradeTemplateLib;
using Newtonsoft.Json.Linq;

namespace MsdiTransportation.SubgradeTemplateLib.Control
{
    /// <summary> 处理横断面中多个组件的构造 </summary>
    internal partial class C_Subgrade : UserControl
    {
        #region --- Fields

        /// <summary> 此构件所维护的具体的路基横断面数据 </summary>
        // private Subgrade m_subgrade;

        /// <summary> 用户当前交互激活的组件对应的下标 </summary>
        private C_SubgradeComp m_activeComp;

        #region --- 道路中心线

        private CenterLineSide m_centerLineSide;
        private C_SubgradeComp m_centerLineComp1;
        private C_SubgradeComp m_centerLineComp2;

        #endregion

        #endregion

        #region --- 界面初始化

        public C_Subgrade()
        {
            InitializeComponent();
        }

        public void InitialiateSubgrade(Subgrade subgrade)
        {
            ConstructDgvFromSubgrade(subgrade);
        }

        /// <summary> 根据 Subgrade 数据构造 Datagridview  </summary>
        private void ConstructDgvFromSubgrade(Subgrade subgrade)
        {
            // m_subgrade = subgrade;
            panel_subgradesHolder.Controls.Clear();

            // 将集合中的所有组件添加到路基中，但不改变每个组件的 Index 序号 </summary>
            foreach (var cmpnt in subgrade.Components)
            {
                // 将新组件添加到Panel中
                var rc = NewComponentC(cmpnt);
                panel_subgradesHolder.Controls.Add(rc);
            }

            // 对所有的组件进行重新定位
            SetAllComponentLocation(panel_subgradesHolder);

            // 设置一个默认激活的组件
            var activeComp = panel_subgradesHolder.Controls.Cast<C_SubgradeComp>()
              .FirstOrDefault(c => c.SubgradeCompent.Index == 0);
            OnComponentActivated(activeComp);
            OnCenterSideProposed(activeComp, CenterLineSide.Left);
        }

        #endregion

        #region ---  从界面中提取数据

        /// <summary> 从界面中提取数据 </summary>
        /// <param name="subgradeComp"></param>
        /// <returns></returns>
        public bool GetSubgradeDataFromUi(out Subgrade subgrade)
        {
            bool validated = true; // 是否通过最终校验
            // subgrade = m_subgrade;
            subgrade = new Subgrade();
            // 提取组件集合
            foreach (C_SubgradeComp c in panel_subgradesHolder.Controls)
            {
                SubgradeComp subComp;
                var succ = c.GetSubgradeCompDataFromUi(out subComp);
                if (succ)
                {
                    subgrade.Components.Add(subComp);
                }
                else
                {
                    validated = false;
                }
            }

            // 设置道路中心线
            subgrade.SetCenterLineSide(m_centerLineSide,
                m_centerLineComp1.SubgradeCompent, m_centerLineComp2?.SubgradeCompent);
            //
            return validated;
        }

        #endregion

        #region --- 组件的增删

        private void button_InsertLeft_Click(object sender, EventArgs e)
        {
            InsertComponent(sender as C_SubgradeComp, true);
        }

        private void button_InsertRight_Click(object sender, EventArgs e)
        {
            InsertComponent(sender as C_SubgradeComp, false);
        }

        private void InsertComponent(C_SubgradeComp compControl, bool intoLR)
        {
            int baseIndex = -1;
            SubgradeComp baseSubgradeComp;
            SubgradeComp newSubgradeComp;
            if (m_activeComp != null)
            {
                baseSubgradeComp = m_activeComp.SubgradeCompent;
                baseIndex = baseSubgradeComp.Index;
                // 根据基准组件复制一个新组件，因为相邻两组件的分层材质很可能是相同的
                newSubgradeComp = baseSubgradeComp.Clone() as SubgradeComp;
            }
            else
            {
                baseIndex = -1;
                newSubgradeComp = new SubgradeComp(RoadwayComponentType.Any);
            }

            // 将新组件添加到Panel中
            var newComp = InsertComponent(newSubgradeComp, baseIndex, intoLR: intoLR);
            OnComponentActivated(newComp);

            // 对所有的组件进行重新定位
            SetAllComponentLocation(panel_subgradesHolder);
            // 调整道路中心线
            OnCenterSideProposed(m_centerLineComp1, m_centerLineSide);
        }

        /// <summary> 添加组件 </summary>
        /// <param name="cmpnt"> 要添加的组件 </param>
        /// <param name="baseIndex"> 基准组件的下标，第一个组件下标为0。如果输入值为-1，则添加一个新的组件 </param>
        /// <param name="intoLR">true表示在当前选中的组件左侧添加，false表示在右侧添加</param>
        private C_SubgradeComp InsertComponent(SubgradeComp cmpnt, int baseIndex, bool intoLR)
        {
            // 提取已有的组件
            var count = panel_subgradesHolder.Controls.Count;
            int destiIndex;
            if (intoLR)
            {
                // 插入指标位置的左侧
                for (int i = 0; i < count; i++)
                {
                    var contr = (panel_subgradesHolder.Controls[i] as C_SubgradeComp);
                    if (contr.SubgradeCompent.Index >= baseIndex)
                    {
                        contr.SubgradeCompent.Index += 1;
                    }
                }
                destiIndex = baseIndex;
            }
            else
            {
                // 插入指标位置的右侧
                for (int i = 0; i < count; i++)
                {
                    var contr = (panel_subgradesHolder.Controls[i] as C_SubgradeComp);
                    if (contr.SubgradeCompent.Index > baseIndex)
                    {
                        contr.SubgradeCompent.Index += 1;
                    }
                }
                destiIndex = baseIndex + 1;
            }
            // 将新组件添加到Panel中
            var rc = NewComponentC(cmpnt);
            cmpnt.Index = destiIndex;
            panel_subgradesHolder.Controls.Add(rc);
            return rc;
        }

        private C_SubgradeComp NewComponentC(SubgradeComp subgradeComp)
        {
            var rc = new C_SubgradeComp();
            rc.InitialiateSubgrade(subgradeComp);
            rc.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            rc.Height = panel_subgradesHolder.Height;
            rc.ComponentActivated += OnComponentActivated;
            rc.CenterProposed += OnCenterSideProposed;
            return rc;
        }
        #endregion

        /// <summary> 删除组件 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_DeleteComp_Click(object sender, EventArgs e)
        {
            if (m_activeComp == null || panel_subgradesHolder.Controls.Count == 0)
            {
                return;
            }
            int activeIndex = m_activeComp.SubgradeCompent.Index;
            int compCountBeforDelete = panel_subgradesHolder.Controls.Count;
            panel_subgradesHolder.Controls.Remove(m_activeComp);
            m_activeComp = null;

            int newActiveIndex = 0;
            // 对所有的组件进行重新定位
            SetAllComponentLocation(panel_subgradesHolder);

            // 设置一个默认激活的组件
            if (compCountBeforDelete == 1)
            {
                // 删除后就没有组件了
                OnComponentActivated(null);
            }
            else if (activeIndex == compCountBeforDelete - 1)
            {
                // 原来删除的组件在最后一个
                newActiveIndex = activeIndex - 1;
            }
            else
            {
                // 原来删除的组件不位于最后一个
                newActiveIndex = activeIndex;
            }

            // 将计算出来要激活的组件进行激活
            var activeComp = panel_subgradesHolder.Controls.Cast<C_SubgradeComp>()
                .FirstOrDefault(c => c.SubgradeCompent.Index == newActiveIndex);
            OnComponentActivated(activeComp);

            // 调整道路中心线
            if (panel_subgradesHolder.Controls.Contains(m_centerLineComp1))
            {
                OnCenterSideProposed(m_centerLineComp1, m_centerLineSide);
            }
            else
            {
                // 说明这个控件被删除了，则用它前后的组件作为道路中心线
                foreach (C_SubgradeComp c in panel_subgradesHolder.Controls)
                {
                    if (c.CenterSide != CenterLineSide.None)
                    {
                        OnCenterSideProposed(c, c.CenterSide);
                        break;
                    }
                }
            }
        }

        #region --- 组件的增删
        /// <summary> 控件被选中为道路中心 - 道路中心线位于此组件的哪一侧  </summary>
        private void OnCenterSideProposed(C_SubgradeComp sender, CenterLineSide centerLineSide)
        {
            var index = sender.SubgradeCompent.Index;
            C_SubgradeComp compNearby = null;
            foreach (C_SubgradeComp c in panel_subgradesHolder.Controls)
            {
                var id = c.SubgradeCompent.Index;
                if (id == index)
                {
                    // 自己就是目标组件
                    c.SwitchCenterLineState(centerLineSide);
                }
                else if (id == index - 1)
                {
                    // 在目标组件左侧
                    if (centerLineSide == CenterLineSide.Left)
                    {
                        c.SwitchCenterLineState(CenterLineSide.Right);
                        compNearby = c;
                    }
                    else
                    {
                        c.SwitchCenterLineState(CenterLineSide.None);
                    }
                }

                else if (id == index + 1)
                {
                    // 在目标组件右侧
                    if (centerLineSide == CenterLineSide.Right)
                    {
                        c.SwitchCenterLineState(CenterLineSide.Left);
                        compNearby = c;
                    }
                    else
                    {
                        c.SwitchCenterLineState(CenterLineSide.None);
                    }
                }
                else
                {
                    // 远离目标组件
                    c.SwitchCenterLineState(CenterLineSide.None);
                }
            }
            //  sender.SwitchCenterLineState(centerLineSide);
            // 
            m_centerLineSide = centerLineSide;
            m_centerLineComp1 = sender;
            m_centerLineComp2 = compNearby;
            // m_subgrade.SetCenterLineSide(centerLineSide, m_centerLineComp1, compNearby);
        }

        /// <summary> Panel中某个组件被激活后的操作 </summary>
        /// <param name="cRoadwayComponent">输入 null，则表示没有激活的组件</param>
        private void OnComponentActivated(C_SubgradeComp cRoadwayComponent)
        {
            m_activeComp = null;
            foreach (C_SubgradeComp c in panel_subgradesHolder.Controls)
            {
                if (c.Equals(cRoadwayComponent))
                {
                    c.SwitchActiveState(true);
                    m_activeComp = c;
                }
                else
                {
                    c.SwitchActiveState(false);
                }
            }
        }

        /// <summary> 对Panel中所有的组件进行重新定位 </summary>
        /// <param name="control"></param>
        private void SetAllComponentLocation(Panel panel)
        {
            var count = panel_subgradesHolder.Controls.Count;

            // 重新梳理每一个组件的编号，使编号较小的位于 SortedDictionary 的前面。
            var id_comp = new SortedDictionary<double, C_SubgradeComp>();
            for (int i = 0; i < count; i++)
            {
                var control = panel_subgradesHolder.Controls[i] as C_SubgradeComp;
                var id = control.SubgradeCompent.Index;

                // 如果出现相同的id，则取两个值的平均值
                double sortedId = id;
                var ids = id_comp.Keys.ToArray(); // 集合中的id值
                if (ids.Contains(id))
                {
                    var matchingId = Array.IndexOf(ids, id); // 相同的id在集合中的下标
                    var idsCount = ids.Length;
                    if (matchingId == idsCount - 1)
                    {
                        // 与集合中最后一个id相同，则向后移一位
                        sortedId = (double)id + 0.5d;
                    }
                    else
                    {
                        // 与集合中非最后一个id相同
                        // 从下一个值开始判断，看是否
                        double xiaoshu = id;
                        for (int j = matchingId + 1; j < idsCount; j++)
                        {
                            if (Math.Abs(Math.Floor(ids[j]) - id) < 10e-30)
                            {
                                // id = 5, ids[j] = 5.75
                                xiaoshu = ids[j];
                            }
                            else
                            {
                                // id = 5, ids[j] = 6
                                break;
                            }
                        }
                        sortedId = (xiaoshu + ids[matchingId] + 1) / 2;
                    }
                }
                id_comp.Add(sortedId, control);
            }

            // 重新梳理每一个组件的编号，第一个组件的编号为0，往右依次增加1。
            int sortedId2 = 0;
            foreach (var c in id_comp)
            {
                c.Value.SubgradeCompent.Index = sortedId2;
                sortedId2 += 1;
            }

            // 按重新排列后的组件进行定位
            for (int i = 0; i < count; i++)
            {
                var control = panel_subgradesHolder.Controls[i] as C_SubgradeComp;
                var id = control.SubgradeCompent.Index;
                control.Width = C_SubgradeComp.WIDTH;
                control.Left = C_SubgradeComp.WIDTH * id;
                // 设置奇偶位的背景色
                var yuShu = id % 2;
                if (yuShu == 0)
                {
                    control.BackColor = Color.LightGray;
                }
                else
                {
                    control.BackColor = System.Drawing.SystemColors.Control;
                }
            }

        }

        #endregion
    }
}
