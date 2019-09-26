using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsdiTransportation.SubgradeTemplateLib.FeatureDefinition
{
    /// <summary> ORD中特征定义的类型 </summary>
    internal enum FeatureType
    {
        None,
        /// <summary> 横断面中的点，对应模型中放样后的线 </summary>
        Point,
        /// <summary> 横断面中的面，对应模型中放样后的面（比如边坡面） </summary>
        Surface,
        /// <summary> 横断面中的封闭体，对应模型中放样后的实体（比如路面实体） </summary>
        Mesh,
    }
}
