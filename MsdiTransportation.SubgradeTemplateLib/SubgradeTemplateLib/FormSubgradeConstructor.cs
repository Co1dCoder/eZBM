using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MsdiTransportation.SubgradeTemplateLib;
using MsdiTransportation.SubgradeTemplateLib.Control;
using MsdiTransportation.SubgradeTemplateLib.FeatureDefinition;

namespace MsdiTransportation.SubgradeComponent
{
    public partial class FormSubgradeConstructor : Form
    {
        #region --- Properties
        private HighwayConfigurations m_HighwayConfigurations;
        #endregion
        public FormSubgradeConstructor()
        {
            InitializeComponent();

            m_HighwayConfigurations = HighwayConfigurations.ReadConfigurationsFromJson(HighwayConfigurations.JsonConfigFileName);
            ConstructHighwayClass();
        }

        #region --- 根据道路标准智能初始化路基横断面

        private void ConstructHighwayClass()
        {
            var stan1 = m_HighwayConfigurations.AlignmentStandards;
            comboBox_highwayClass.DisplayMember = "Description";
            comboBox_highwayClass.DataSource = stan1;
        }

        private void comboBox_designSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_highwayClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            var v = comboBox_highwayClass.SelectedItem as AlignmentStandard;
            // 设置可选的设计速度
            var standard = m_HighwayConfigurations.AlignmentStandards.First(r => r == v);
            comboBox_designSpeed.DataSource = standard.PossibleSpeed;

            //// 设置可选的车道宽度
            comboBox_LaneNum.DataSource = standard.LaneNum;

            comboBox_SectionType.DataSource = standard.PossibleSectionType;
        }

        private void btn_ConstructSubgrade_Click(object sender, EventArgs e)
        {
            var subgrade = GenerateTestSubgrade();
            //
            subgradeConstructor1.InitialiateSubgrade(subgrade);
        }

        private Subgrade GenerateTestSubgrade()
        {
            var subgrade = new Subgrade();
            RoadwayComponentFeature feature;
            //
            RoadwayComponent sc = new Lane(0.2, new double[] { 3.5, 3.5 });
            var sgc = new SubgradeComp(sc);
            sgc.Index = 2;

            sgc.RoadwayCompnt.Features.Add(new RoadwayComponentFeature(FeatureDef_Roadway.CommonMesh,
                thickness: 0.05));
            sgc.RoadwayCompnt.Features.Add(new RoadwayComponentFeature(FeatureDef_Roadway.CommonMesh,
                thickness: 0.25));

            subgrade.Components.Add(sgc);
            //
            sc = new Lane(0.2, new double[] { 0.75 });
            sgc = new SubgradeComp(sc);
            sgc.Index = 2;
            sgc.RoadwayCompnt.Features.Add(new RoadwayComponentFeature(FeatureDef_Roadway.CommonMesh, thickness: 0.3));
            subgrade.Components.Add(sgc);
            //
            sc = new HardShoulder(0.2, new double[] { 0.75 });
            sgc = new SubgradeComp(sc);
            sgc.Index = 1;
            sgc.RoadwayCompnt.Features.Add(new RoadwayComponentFeature(FeatureDef_Roadway.CommonMesh, thickness: 0.3));
            subgrade.Components.Add(sgc);
            //
            sc = new SoftShoulder(0.2, new double[] { 0.5 });
            sgc = new SubgradeComp(sc);
            sgc.Index = 0;
            sgc.RoadwayCompnt.Features.Add(new RoadwayComponentFeature(FeatureDef_Roadway.CommonMesh, thickness: 0.3));
            subgrade.Components.Add(sgc);

            //
            sc = new HardShoulder(0.2, new double[] { 0.75 });
            sgc = new SubgradeComp(sc);
            sgc.Index = 3;
            sgc.RoadwayCompnt.Features.Add(new RoadwayComponentFeature(FeatureDef_Roadway.CommonMesh, thickness: 0.3));
            subgrade.Components.Add(sgc);
            //
            sc = new SoftShoulder(0.2, new double[] { 0.5 });
            sgc = new SubgradeComp(sc);
            sgc.Index = 4;
            sgc.RoadwayCompnt.Features.Add(new RoadwayComponentFeature(FeatureDef_Roadway.CommonMesh, thickness: 0.3));
            subgrade.Components.Add(sgc);

            return subgrade;
        }

        #endregion

        #region --- 从界面中提取横断面方案

        private void btn_apply_Click(object sender, EventArgs e)
        {
            var subgrade = GetSubgradeFromUI(subgradeConstructor1);
            subgrade.GenerateTemplate();
        }

        private Subgrade GetSubgradeFromUI(C_Subgrade subgradeControl)
        {
            Subgrade subgrade;
            var succ = subgradeControl.GetSubgradeDataFromUi(out subgrade);
            if (!succ)
            {
                return null;
            }
            return subgrade;
        }

        #endregion
    }
}