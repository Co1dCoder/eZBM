using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsdiTransportation.SubgradeTemplateLib.TemplateLib
{
    /// <summary> ORD横断面模板配置数据 </summary>
    internal class Settings
    {
        /// <summary> ORD横断面模板中，当两个点的距离小于等于此值时，则认为几何上可以合并为同一个点（但并不是一定要进行合并）。 </summary>
        public const double PointMergeTolerance = 0.0001;
    }
}
