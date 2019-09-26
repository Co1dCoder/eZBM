using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZstd.Miscellaneous;
using MsdiTransportation.SubgradeComponent;

namespace MsdiTransportation.SubgradeTemplateLib
{

    /// <summary> 某具体的路基断面中的各个组件 </summary>
    internal class SubgradeComp : ICloneable
    {
        public int Index { get; set; }

        // public RoadwayComponentType ComponentType { get; set; }

        ///// <summary> 此组件位于断面的左/中/右侧 </summary>
        //public RoadwayDirection Direction { get; set; }

        /// <summary> 此组件的分层材质等属性信息 </summary>
        public RoadwayComponent RoadwayCompnt { get; set; }

        #region --- 构造函数

        public SubgradeComp(RoadwayComponentType type)
        {
            RoadwayCompnt = RoadwayComponent.CreateRoadwayComponent(type);
        }

        public SubgradeComp(RoadwayComponent componentInfo) : this(componentInfo.Type)
        {
            RoadwayCompnt = componentInfo;
        }

        public override string ToString()
        {
            return $"index:{Index}, type: {RoadwayCompnt.Type} 宽度： {RoadwayCompnt.WidthToString()}";
        }
        
        #endregion

          public object Clone()
        {
            var subgradeComp = new SubgradeComp(RoadwayCompnt.Type);
            subgradeComp.Index = Index;
            subgradeComp.RoadwayCompnt = RoadwayCompnt.Clone() as RoadwayComponent;
            return subgradeComp;
        }
  }

    /// <summary> 组件相对于道路中心线的左右侧方向 </summary>
    internal enum RoadwayDirection
    {
        [Description("左侧")]
        Left,
        [Description("右侧")]
        Middle,
        [Description("中间")]
        Right
    }

    /// <summary> 道路中心线位于此组件的哪一侧 </summary>
    internal enum CenterLineSide
    {
        /// <summary> 道路中心线不在此组件的任意一侧 </summary>
        [Description("远离")]
        None,
        /// <summary> 道路中心线位于此组件的左边缘 </summary>
        [Description("左侧")]
        Left,
        /// <summary> 道路中心线位于此组件的右边缘 </summary>
        [Description("右侧")]
        Right,
        /// <summary> 道路中心线位于此组件的中心 </summary>
        [Description("中间")]
        Center
    }
}
