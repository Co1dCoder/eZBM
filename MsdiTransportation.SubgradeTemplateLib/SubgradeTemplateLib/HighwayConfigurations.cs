using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MsdiTransportation.SubgradeTemplateLib
{
    class HighwayConfigurations
    {
        private static HighwayConfigurations m_instance;

        #region --- 配置的内容

        public const string JsonConfigFileName = @"D:\GithubProjects\eZBM\MsdiTransportation.SubgradeTemplateLib\bin\Debug\AlignmentStandards.json";
        
        public List<AlignmentStandard> AlignmentStandards { get; }

        #endregion

        #region --- 配置文件的读写

        /// <summary> 构造函数 </summary>
        private HighwayConfigurations()
        {
            AlignmentStandards = new List<AlignmentStandard>();
            m_instance = this;
        }

        public static HighwayConfigurations ReadConfigurationsFromJson(string jsonFileName)
        {
            HighwayConfigurations config = m_instance;
            if ((m_instance == null) && (File.Exists(jsonFileName)))
            {
                using (StreamReader reader = new StreamReader(jsonFileName))
                {
                    var configStr = reader.ReadToEnd();
                    JsonSerializer ser = JsonSerializer.Create();
                    config = Newtonsoft.Json.JsonConvert.
                        DeserializeObject<HighwayConfigurations>(configStr);
                }
            }
            m_instance = config ?? new HighwayConfigurations();
            return m_instance;
        }

        public void WriteToJson(string jsonFileName)
        {
            // 直接序列化为字符
            string config = Newtonsoft.Json.JsonConvert.SerializeObject(value: m_instance, formatting: Formatting.Indented);
            config = Newtonsoft.Json.JsonConvert.SerializeObject(value: m_instance,
                formatting: Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(jsonFileName))
            {
                writer.Write(config);
            }
        }
        #endregion

        /// <summary> 与路线相关的设计标准 </summary>
        public static void TestSetAlignmentStandards()
        {
            var config = new HighwayConfigurations();
            var stands = config.AlignmentStandards;

            stands.Add(new AlignmentStandard(
                roadClass: RoadClassType.Expressway, description: "高速公路",
                possibleSpeed: new double[] { 120, 100, 80 }, laneNum: new short[] { 8, 6, 4 },
                possibleSectionType:new SubgradeSectionType[] {SubgradeSectionType.整体式, SubgradeSectionType.整体式_中间带, }));

            stands.Add(new AlignmentStandard(
                roadClass: RoadClassType.Class1, description: "一级公路",
                possibleSpeed: new double[] { 100, 80, 60 }, laneNum: new short[] { 2 },
                possibleSectionType: new SubgradeSectionType[] { SubgradeSectionType.整体式_中间带, }));
                
            config.WriteToJson(JsonConfigFileName);
        }
    }
}
