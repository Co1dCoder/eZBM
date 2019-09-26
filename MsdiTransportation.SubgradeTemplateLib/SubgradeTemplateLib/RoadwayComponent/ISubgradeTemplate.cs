using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsdiTransportation.SubgradeComponent
{

    /// <summary> 用于转换为ORD的横断面模板数据 </summary>
    interface ISubgradeTemplate
    {
        /// <summary> 转换成ORD的路基横断面模板 </summary>
        string ToOrdTemplate();
    }

}
