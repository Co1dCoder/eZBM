using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsdiTransportation.SubgradeTemplateLib
{
    /// <summary> 路基横断面类型 </summary>
    internal enum SubgradeSectionType
    {
        /// <summary> 整体式断面（二、三、四级公路） </summary>
        整体式 = 0,

        /// <summary> 整体式断面（高速公路，一级公路一般整体式断面，带中间带） </summary>
        整体式_中间带 = 1,

        /// <summary> 高速公路整体复合式断面（一侧） </summary>
        整体复合式 = 2,

        /// <summary> 高速公路，一级公路一般分离式断面（一侧） </summary>
        一般分离式 = 3,

        /// <summary> 高速公路分离复合式断面（一侧） </summary>
        分离复合式 = 4,
    }

    /// <summary> 路基横断面类型 </summary>
    internal enum SubgradeSectionType1
    {
        /// <summary> 整体式断面（二、三、四级公路） </summary>
        SingleRoadbed = 0,

        /// <summary> 整体式断面（高速公路，一级公路一般整体式断面，带中间带） </summary>
        SingleRoadbed_Midian = 1,

        /// <summary> 高速公路整体复合式断面（一侧） </summary>
        SingleRoadbed_Midian_Combined = 2,

        /// <summary> 高速公路，一级公路一般分离式断面（一侧） </summary>
        WidelySeparatedRoadBed = 3,

        /// <summary> 高速公路分离复合式断面（一侧） </summary>
        SingleRoadbed_TrailingLane = 4,
    }
}
