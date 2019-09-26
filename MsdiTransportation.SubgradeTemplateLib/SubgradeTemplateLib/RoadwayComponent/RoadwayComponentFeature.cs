using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsdiTransportation.SubgradeTemplateLib.FeatureDefinition;

namespace MsdiTransportation.SubgradeComponent
{
    /// <summary> 路基分层的属性特征 </summary>
    class RoadwayComponentFeature : ICloneable
    {
        #region --- Properties
        public FeatureDef_Roadway Feature { get; set; }
        public double Thickness { get; set; }
        #endregion

        public RoadwayComponentFeature()
        {
            Feature = FeatureDef_Roadway.NoFeature;
            Thickness = 0.0;
        }
        public RoadwayComponentFeature(FeatureDef_Roadway feature, double thickness)
        {
            Feature = feature;
            Thickness = thickness;
        }

        public override string ToString()
        {
            return $"{Feature},{Thickness}";
        }

        public object Clone()
        {
            return new RoadwayComponentFeature(Feature.Clone() as FeatureDef_Roadway, Thickness);
        }
    }

}
