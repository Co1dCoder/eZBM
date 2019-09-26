using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZBMCE.CIF.SubgradeTemplateLib
{
    /// <summary> 道路等级 </summary>
    enum RoadClass
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
    class AlignmentStandard
    {

        /// <summary> 不同路线等级下的可选设计车速 </summary>
        public static readonly Dictionary<RoadClass, double[]> DesignSpeed =
            new Dictionary<RoadClass, double[]>
            {
                { RoadClass.Expressway, new double[] { 120,100,80 } },
                { RoadClass.Class1, new double[] { 100,80,60 } },
                { RoadClass.Class2, new double[] { 80, 60 } },
                { RoadClass.Class2, new double[] { 80, 60 } },
                { RoadClass.Class3, new double[] { 40,30 } },
                { RoadClass.Class4, new double[] { 20 } },
                { RoadClass.OnSiteAccessRoad, new double[] { 40 } },
                { RoadClass.ExternalTrafficAccommondateHighway, new double[] { 60 } },
            };
    }
    
}