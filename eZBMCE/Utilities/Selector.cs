using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bentley.DgnPlatformNET.Elements;

namespace eZBMCE.Utilities
{
    /// <summary> 界面中的元素选择器 </summary>
    public class Selector
    {

        /// <summary> 在界面中选择一个单一元素 </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SelectSingleElement<T>() where T : Element
        {
            return null;
        }

    }
}
