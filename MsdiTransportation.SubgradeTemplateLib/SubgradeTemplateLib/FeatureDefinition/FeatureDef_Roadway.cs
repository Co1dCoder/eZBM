using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsdiTransportation.SubgradeTemplateLib.FeatureDefinition
{
    /// <summary>  ORD 的中路基路面相关的特征定义 </summary>
    class FeatureDef_Roadway : ICloneable
    {
        #region --- 通用Feature

        /// <summary> 无特征定义 </summary>
        /// <remarks> NOFEATUERDEFINITION </remarks>
        public static readonly FeatureDef_Roadway NoFeature
            = new FeatureDef_Roadway(FeatureType.None, "无", "");

        /// <summary> 任意一种 </summary>
        /// <remarks> NOFEATUERDEFINITION </remarks>
        public static readonly FeatureDef_Roadway CommonMesh
            = new FeatureDef_Roadway(FeatureType.Mesh, "任意", "");


        #endregion

        #region --- Properties

        public FeatureType Type { get; set; }
        public string FeatureName { get; set; }
        public string FeaturePath { get; set; }
        public FeatureDef_Roadway Self { get { return this; } }

        private static List<FeatureDef_Roadway> FeaturesInLib;
        #endregion

        #region --- 构造函数
        public FeatureDef_Roadway()
        {
            Type = FeatureType.None;
            FeaturePath = NoFeature.FeaturePath;
            FeatureName = NoFeature.FeatureName;
        }

        public FeatureDef_Roadway(FeatureType type, string featureName, string featurePath)
        {
            Type = type;
            FeaturePath = featurePath;
            FeatureName = featureName;
        }

        public static FeatureDef_Roadway FindFeatureDefFromLib(FeatureType type, string featureName, string featurePath)
        {
            FeatureDef_Roadway feature = null;
            var newFeature = new FeatureDef_Roadway(type, featureName, featurePath);
            feature = FeaturesInLib.FirstOrDefault(r => r.HasSameContent(newFeature));
            return feature;
        }

        private bool HasSameContent(object obj)
        {
            var f = obj as FeatureDef_Roadway;
            if ((f.Type == Type) && (f.FeatureName == FeatureName) && (f.FeaturePath == FeaturePath))
            {
                return true;
            }
            return false;
        }

        #endregion


        public static List<FeatureDef_Roadway> GenerateRoadwayFeaturesFromLib()
        {
            var features = new List<FeatureDef_Roadway>();
            features.Add(NoFeature);
            features.Add(CommonMesh);
            features.Add(new FeatureDef_Roadway(FeatureType.Mesh, "面层", @"Roadway\C30混凝土面层"));
            features.Add(new FeatureDef_Roadway(FeatureType.Mesh, "基层", @"Roadway\C30混凝土面层"));
            features.Add(new FeatureDef_Roadway(FeatureType.Mesh, "底基层", @"Roadway\底基层"));
            features.Add(new FeatureDef_Roadway(FeatureType.Mesh, "水稳层", @"Roadway\水稳层"));
            features.Add(new FeatureDef_Roadway(FeatureType.Mesh, "垫层", @"Roadway\垫层"));

            //
            FeaturesInLib = features;
            return features;
        }

        public static FeatureDef_Roadway GetRoadWayPointFeature(FeaturePointType pointType)
        {
            FeatureDef_Roadway pointFeature = NoFeature;

            switch (pointType)
            {
                case FeaturePointType.RoadCenterLine:
                    pointFeature = new FeatureDef_Roadway(FeatureType.Point, "RoadCenterLine", "");
                    break;
                case FeaturePointType.EdgeOfPavement:
                    pointFeature = new FeatureDef_Roadway(FeatureType.Point, "EdgeOfPavement", "");
                    break;
            }

            return pointFeature;
        }

        public override string ToString()
        {
            return $"{Type},{FeatureName}";
        }

        public object Clone()
        {
            FeatureDef_Roadway feature = this;// FindFeatureDefFromLib(Type, FeatureName, FeaturePath);
            return feature;
        }
    }
}