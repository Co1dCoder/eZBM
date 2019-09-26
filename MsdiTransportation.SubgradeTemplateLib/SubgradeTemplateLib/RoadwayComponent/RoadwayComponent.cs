using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MsdiTransportation.SubgradeTemplateLib.FeatureDefinition;

namespace MsdiTransportation.SubgradeComponent
{

    /// <summary> 路面及路肩范围内的各种组件信息 </summary>
    class RoadwayComponent : ISubgradeTemplate, ICloneable
    {
        #region --- Properties

        /// <summary> 组件类型 </summary>
        public RoadwayComponentType Type { get; protected set; }

        /// <summary> 横坡（0.2 代表 0.2% 的横坡） </summary>
        public double CrossSlope { get; set; }

        /// <summary> 组件总宽度（或行车道各车道宽度之和） </summary>
        public double Width => LaneWidth.Sum();

        /// <summary> 车道数以及各车道宽度 </summary>
        public double[] LaneWidth { get; set; }

        /// <summary> 组件分层特征 </summary>
        public List<RoadwayComponentFeature> Features { get; set; }

        #endregion

        #region --- 构造函数

        public RoadwayComponent()
        {
            Type = RoadwayComponentType.Any;
            Features = new List<RoadwayComponentFeature>();
            LaneWidth = new double[0];
        }
        public RoadwayComponent(double crossSlope, double[] laneWidth) : this()
        {
            CrossSlope = crossSlope;
            LaneWidth = laneWidth;
        }

        public static RoadwayComponent CreateRoadwayComponent(RoadwayComponentType type)
        {
            RoadwayComponent comp = new RoadwayComponent();
            switch (type)
            {
                case RoadwayComponentType.Lane: comp = new Lane(); break;
                case RoadwayComponentType.DividingStrip: comp = new DividingStrip(); break;
                case RoadwayComponentType.EarthingShoulder: comp = new EarthingShoulder(); break;
                case RoadwayComponentType.HardShoulder: comp = new HardShoulder(); break;
                case RoadwayComponentType.SoftShoulder: comp = new SoftShoulder(); break;
                case RoadwayComponentType.MarginalStrip: comp = new MarginalStrip(); break;
            }
            return comp;
        }
        #endregion

        /// <summary> 用于转换为ORD的横断面模板数据 </summary>
        public string ToOrdTemplate()
        {

            throw new NotImplementedException();
        }

        public RoadwayComponentType GetCompType()
        {
            if (this is Lane)
            {
                return RoadwayComponentType.Lane;
            }
            else if (this is MarginalStrip)
            {
                return RoadwayComponentType.MarginalStrip;
            }
            else if (this is DividingStrip)
            {
                return RoadwayComponentType.DividingStrip;
            }
            else if (this is HardShoulder)
            {
                return RoadwayComponentType.HardShoulder;
            }
            else if (this is SoftShoulder)
            {
                return RoadwayComponentType.SoftShoulder;
            }
            return RoadwayComponentType.Lane;
        }

        #region --- Width & String

        /// <summary> 将车道宽度输出为字符表达式 </summary>
        /// <returns></returns>
        public string WidthToString()
        {
            if (LaneWidth.Length == 0)
            {
                return string.Empty;
            }
            else if (LaneWidth.Length == 1)
            {
                return LaneWidth[0].ToString();
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append(LaneWidth[0].ToString());

                for (int i = 1; i < LaneWidth.Length; i++)
                {
                    sb.Append("+");
                    sb.Append(LaneWidth[i].ToString());
                }
                return sb.ToString();
            }

        }

        /// <summary> 将为字符表达式“1.2+6.+8”解析为车道宽度 </summary>
        public static bool GetWidthFromString(string widthsText, out double[] widths)
        {
            widths = new double[0];
            var widthsStr = widthsText.Split('+');
            var wis = new List<double>();

            foreach (var w in widthsStr)
            {
                double width;
                if (double.TryParse(w, out width))
                {
                    wis.Add(width);
                }
                else
                {
                    return false;
                }
            }
            widths = wis.ToArray();
            return true;
        }
        #endregion

        public override string ToString()
        {
            return $"Type:{Type},  宽度:{Width}";
        }

        /// <summary> 复制 </summary>
        public object Clone()
        {
            RoadwayComponent comp = new RoadwayComponent(CrossSlope, LaneWidth.Clone() as double[]);
            comp.Type = this.Type;
            foreach (var f in Features)
            {
                var roadFeature = f.Clone() as RoadwayComponentFeature;
                comp.Features.Add(roadFeature);
            }
            return comp;
        }
    }

}