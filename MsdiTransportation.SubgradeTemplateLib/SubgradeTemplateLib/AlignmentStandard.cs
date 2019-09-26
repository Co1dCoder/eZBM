
namespace MsdiTransportation.SubgradeTemplateLib
{
    /// <summary> 道路等级 </summary>
    internal enum RoadClassType
    {
        // D:\GithubProjects\eZBM\eZBMCE\CIF\SubgradeTemplateLib
        /// <summary> 公路（Highway）中的高速公路（Motorway） </summary>
        Expressway,

        /// <summary> 一级公路 Class 1 highway </summary>
        Class1,

        /// <summary> 二级公路 Class 2 highway </summary>
        Class2,

        /// <summary> 三级公路 Class 3 highway </summary>
        Class3,

        /// <summary> 四级公路 Class 4 highway </summary>
        Class4,

        /// <summary> 场内交通道路 </summary>
        OnSiteAccessRoad,

        /// <summary> 对外交通专用道路 </summary>
        ExternalTrafficAccommondateHighway,
    }

    /// <summary> 与路线相关的设计标准 </summary>
    internal class AlignmentStandard
    {
        /// <summary> 公路等级 </summary>
        public RoadClassType RoadClass { get; set; }

        /// <summary> 公路等级的中文描述 </summary>
        public string Description { get; set; }

        /// <summary> 可能的横断面类型 </summary>
        public SubgradeSectionType[] PossibleSectionType { get; set; }

        /// <summary> 可能的设计速度 </summary>
        public double[] PossibleSpeed { get; set; }

        /// <summary> 可能的车道数量 </summary>
        public short[] LaneNum { get; set; }


        public AlignmentStandard(
            RoadClassType roadClass, string description, double[] possibleSpeed, short[] laneNum, SubgradeSectionType[] possibleSectionType)
        {
            RoadClass = roadClass;
            Description = description;
            PossibleSpeed = possibleSpeed;
            LaneNum = laneNum;
            PossibleSectionType = possibleSectionType;
        }
    }
}