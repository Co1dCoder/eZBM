using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsdiTransportation.SubgradeTemplateLib.FeatureDefinition
{
    /// <summary> 路基路面模板中的特征点，每一个特征点都对应一种特征定义 </summary>
    internal enum FeaturePointType
    {
        /// <summary> 道路中心线 </summary>
        [Description("道路中心线")]
        RoadCenterLine,
        /// <summary> 车道边缘线 </summary>
        [Description("车道边缘线")]
        EdgeOfPavement,
        /// <summary> 车道分隔线 </summary>
        [Description("车道分隔线")]
        CutOfPavement,
        /// <summary> 硬路肩外边缘线 </summary>
        [Description("硬路肩外边缘线")]
        EdgeOfHardShoulder,
        /// <summary> 土路肩外边缘线 </summary>
        [Description("土路肩外边缘线")]
        EdgeOfSoftShoulder,
    }
}
