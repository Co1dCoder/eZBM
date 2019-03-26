using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZBMCE.AddinManager;

namespace AddinManagerTest2
{
    /// <summary> 用于 AddinManager 中调试 dll 时将引用的程序集加载到进程中 </summary>
    public class DllActivator_Test2 : IDllActivator
    {
        /// <summary> 激活本DLL所引用的那些DLLs </summary>
        public void ActivateReferences()
        {
            var dllAct = new DllActivator_AddinManager();
            dllAct.ActivateReferences();
        }
    }
}
