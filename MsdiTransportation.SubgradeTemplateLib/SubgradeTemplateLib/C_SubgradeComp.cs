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
using MsdiTransportation.SubgradeTemplateLib.FeatureDefinition;

namespace MsdiTransportation.SubgradeTemplateLib.Control
{

    internal partial class C_SubgradeComp : UserControl
    {
        #region ---     Configurations

        /// <summary> 此控件的宽度 </summary>
        public const int WIDTH = 158;

        private readonly Color DefaultCenterSideBackColor;

        /// <summary> 表示道路中心线的位置的颜色 </summary>
        private readonly Color CenterSideBackColor = Color.Red;

        #endregion

        #region ---  道路中心线位于此组件的哪一侧
        /// <summary> 道路中心线位于此组件的哪一侧 </summary>
        public CenterLineSide CenterSide { get; private set; }
        #endregion

        #region ---  Properties


        /// <summary> 此构件所维护的具体的路基组件 </summary>
        public SubgradeComp SubgradeCompent { get; private set; }

        private List<FeatureDef_Roadway> m_roadwayFeatureSources;


        #endregion

        public C_SubgradeComp()
        {
            InitializeComponent();

            //
            DefaultCenterSideBackColor = pictureBox_Center.BackColor;
            pictureBox_Left.BackColor = DefaultCenterSideBackColor;
            pictureBox_Right.BackColor = DefaultCenterSideBackColor;

            m_roadwayFeatureSources = FeatureDef_Roadway.GenerateRoadwayFeaturesFromLib();

            //
            comboBox_type.DataSource =
                    Utils.GetEnumDescriptionAttributes(typeof(RoadwayComponentType));
            // 提取选择的枚举值  var dire = (RoadwayDirection)comboBox_Direction.SelectedIndex;

            ConstructDgv();
        }

        #region ---  界面初始化

        private void ConstructDgv()
        {
            // 设置表属性
            dataGridView_Features.DataError += DataGridViewFeaturesOnDataError;
            dataGridView_Features.AutoGenerateColumns = false;
            dataGridView_Features.AllowUserToAddRows = true;

            // 设置列属性
            FeatureCol.DataSource = m_roadwayFeatureSources;
            FeatureCol.DisplayMember = "FeatureName";
            FeatureCol.ValueMember = "Self";
            FeatureCol.DataPropertyName = "Feature";

            // 厚度列对应的属性
            ThicknessCol.DataPropertyName = "Thickness";
        }

        private void DataGridViewFeaturesOnDataError(object sender,
            DataGridViewDataErrorEventArgs e)
        {
            var c = dataGridView_Features.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var v = c.Value;
            var msg = e.Exception.Message;
        }

        public void InitialiateSubgrade(SubgradeComp subgrade)
        {
            ConstructUIFromSubgrade(subgrade);
        }
        
        /// <summary> 根据 Subgrade 数据构造 Datagridview  </summary>
        private void ConstructUIFromSubgrade(SubgradeComp subgrade)
        {
            SubgradeCompent = subgrade;
            if (subgrade != null)
            {
                if (subgrade.RoadwayCompnt != null)
                {
                    var componentInfo = subgrade.RoadwayCompnt;

                    // 提取选择的枚举值  var dire = (RoadwayDirection)comboBox_Direction.SelectedIndex;
                    // 设置要选中的枚举值;
                    comboBox_type.SelectedIndex = (int)subgrade.RoadwayCompnt.Type;
                    // comboBox_Direction.SelectedIndex = (int)subgrade.Direction;

                    textBox_Width.Text = componentInfo.WidthToString();
                    textBox_CrossSlope.Text = componentInfo.CrossSlope.ToString();
                    // 分层信息
                    var ds = new BindingList<RoadwayComponentFeature>(componentInfo.Features);
                    ds.AllowNew = true;
                    ds.AddingNew += DsOnAddingNew;
                    dataGridView_Features.DataSource = ds;
                }
            }
        }

        private void DsOnAddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new RoadwayComponentFeature();
            // throw new NotImplementedException();
        }
        
        #endregion

        #region ---  从界面中提取数据

        /// <summary> 从界面中提取数据 </summary>
        /// <param name="subgradeComp"></param>
        /// <returns></returns>
        public bool GetSubgradeCompDataFromUi(out SubgradeComp subgradeComp)
        {
            bool validated = true; // 是否通过最终校验

            // 提取组件类型
            var compType = (RoadwayComponentType)comboBox_type.SelectedIndex;
            subgradeComp = new SubgradeComp(compType);

            // 提取 Index的信息
            subgradeComp.Index = SubgradeCompent.Index;
            
            // 提取分层信息
            subgradeComp.RoadwayCompnt.Features = (dataGridView_Features.DataSource as BindingList<RoadwayComponentFeature>).ToList();

            // 提取横坡
            double crossSlope;
            validated = double.TryParse(textBox_CrossSlope.Text, out crossSlope);
            if (validated)
            {
                subgradeComp.RoadwayCompnt.CrossSlope = crossSlope;
            }

            // 提取宽度
            double[] widths;
            validated = RoadwayComponent.GetWidthFromString(textBox_Width.Text, out widths);
            if (validated)
            {
                subgradeComp.RoadwayCompnt.LaneWidth = widths;
            }

            //
            SubgradeCompent = subgradeComp;
            return validated;
        }

        #endregion

        #region ---  组件绑定的数据发生改变

        /// <summary> 组件绑定的数据发生改变 </summary>
        public event Action<C_SubgradeComp> ComponentDataChanged;

        #endregion

        #region ---  控件的激活与取消激活

        /// <summary> 组件被激活 </summary>
        public event Action<C_SubgradeComp> ComponentActivated;

        private void Components_Activated_Click(object sender, EventArgs e)
        {
            InvokeActiveEvent(sender);
        }

        private void InvokeActiveEvent(object sender)
        {
            if (ComponentActivated != null)
            {
                // 触发事件
                ComponentActivated(this);
            }
            label_Features.Text = SubgradeCompent.Index.ToString() + CenterSide;

        }

        public void SwitchActiveState(bool active)
        {
            if (active)
            {
                pictureBox_Activated.BackColor = Color.Green;
            }
            else
            {
                pictureBox_Activated.BackColor = Color.Transparent;
            }
        }

        #endregion

        #region ---  控件被选中为道路中心 - 道路中心线位于此组件的哪一侧

        /// <summary> 控件被选中为道路中心 </summary>
        public event Action<C_SubgradeComp, CenterLineSide> CenterProposed;

        /// <summary> 组件被激活 </summary>
        private void pictureBox_centerline_CheckedChanged(object sender, EventArgs e)
        {
            CenterSide = CenterLineSide.None;
            if (sender == pictureBox_Center)
            {
                CenterSide = CenterLineSide.Center;
            }
            else if (sender == pictureBox_Left)
            {
                CenterSide = CenterLineSide.Left;
            }
            else if (sender == pictureBox_Right)
            {
                CenterSide = CenterLineSide.Right;
            }
            if (CenterProposed != null)
            {
                // 触发事件
                CenterProposed.Invoke(this, CenterSide);
            }
        }


        public void SwitchCenterLineState(CenterLineSide centerSide)
        {
            CenterSide = centerSide;
            //
            pictureBox_Left.BackColor = centerSide == CenterLineSide.Left ? CenterSideBackColor : DefaultCenterSideBackColor;
            pictureBox_Center.BackColor = centerSide == CenterLineSide.Center ? CenterSideBackColor : DefaultCenterSideBackColor;
            pictureBox_Right.BackColor = centerSide == CenterLineSide.Right ? CenterSideBackColor : DefaultCenterSideBackColor;
        }

        #endregion

        public override string ToString()
        {
            return SubgradeCompent.ToString();
        }
    }
}
