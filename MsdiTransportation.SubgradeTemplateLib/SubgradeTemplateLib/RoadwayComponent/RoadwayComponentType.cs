using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsdiTransportation.SubgradeComponent
{

    enum RoadwayComponentType
    {
        /// <summary> 任意类型 </summary>
        [Description("任意")]
        Any,
        /// <summary> 车道 Lane</summary>
        [Description("车道")]
        Lane,
        /// <summary> 中央分隔带 DividingStrip</summary>
        [Description("中央分隔带")]
        DividingStrip,
        /// <summary> 硬路肩 HardShoulder</summary>
        [Description("硬路肩")]
        HardShoulder,
        /// <summary> 土路肩 SoftShoulder</summary>
        [Description("土路肩")]
        SoftShoulder,
        /// <summary> 路缘带 MarginalStrip</summary>
        [Description("路缘带")]
        MarginalStrip,
        /// <summary> 培路肩 Earthing Shoulder </summary>
        [Description("培路肩")]
        EarthingShoulder,
    }

    //enum RoadwayComponentType
    //{
    //    /// <summary> 车道 </summary>
    //    Lane,
    //    /// <summary> 中央分隔带 </summary>
    //    DividingStrip,
    //    /// <summary> 硬路肩 </summary>
    //    HardShoulder,
    //    /// <summary> 土路肩 </summary>
    //    SoftShoulder,
    //    /// <summary> 路缘带 </summary>
    //    MarginalStrip,
    //}
}
