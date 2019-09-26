using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsdiTransportation.SubgradeComponent
{
    /// <summary> 道路某一侧的行车道，包括一侧多个车道 </summary>
    class Lane : RoadwayComponent
    {
        /// <summary> 构造函数 </summary>
        public Lane() : base()
        {
            Type = RoadwayComponentType.Lane;
        }
        /// <summary> 构造函数 </summary>
        public Lane(double crossSlope, double[] laneWidth) : base(crossSlope, laneWidth)
        {
            Type = RoadwayComponentType.Lane;

        }

    }

    /// <summary> 路缘带 </summary>
    class MarginalStrip : RoadwayComponent
    {        /// <summary> 构造函数 </summary>
        public MarginalStrip() : base()
        {
            Type = RoadwayComponentType.MarginalStrip;
        }
        /// <summary> 构造函数 </summary>
        public MarginalStrip(double crossSlope, double[] laneWidth) : base(crossSlope, laneWidth)
        {
            Type = RoadwayComponentType.MarginalStrip;

        }
    }

    /// <summary> 中央分隔带 </summary>
    class DividingStrip : RoadwayComponent
    {
        /// <summary> 构造函数 </summary>
        public DividingStrip() : base()
        {
            Type = RoadwayComponentType.DividingStrip;
        }
        /// <summary> 构造函数 </summary>
        public DividingStrip(double crossSlope, double[] laneWidth) : base(crossSlope, laneWidth)
        {
            Type = RoadwayComponentType.DividingStrip;

        }
    }
    /// <summary> 硬路肩 </summary>
    class HardShoulder : RoadwayComponent
    {        /// <summary> 构造函数 </summary>
        public HardShoulder() : base()
        {
            Type = RoadwayComponentType.HardShoulder;
        }
        /// <summary> 构造函数 </summary>
        public HardShoulder(double crossSlope, double[] laneWidth) : base(crossSlope, laneWidth)
        {
            Type = RoadwayComponentType.HardShoulder;

        }
    }

    /// <summary> 土路肩 </summary>
    class SoftShoulder : RoadwayComponent
    {
        /// <summary> 构造函数 </summary>
        public SoftShoulder() : base()
        {
            Type = RoadwayComponentType.SoftShoulder;
        }
        /// <summary> 构造函数 </summary>
        public SoftShoulder(double crossSlope, double[] laneWidth) : base(crossSlope, laneWidth)
        {
            Type = RoadwayComponentType.SoftShoulder;

        }
    }


    /// <summary> 培路肩 </summary>
    class EarthingShoulder : RoadwayComponent
    {
        /// <summary> 构造函数 </summary>
        public EarthingShoulder() : base()
        {
            Type = RoadwayComponentType.EarthingShoulder;
        }

        /// <summary> 构造函数 </summary>
        public EarthingShoulder(double crossSlope, double[] laneWidth) : base(crossSlope, laneWidth)
        {
            Type = RoadwayComponentType.EarthingShoulder;
        }
    }
}
