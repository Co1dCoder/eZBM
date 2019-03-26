namespace eZBMCE.AddinManager
{
    /// <summary>
    /// 用于在IExternalCommand.Execute方法中，将整个项目的所有dll加载到进程中。
    /// 以避免出现在后面出现无法加载文件或者程序集的问题。
    /// 此接口是专门为AddinManager在调试时设计的，在最终软件发布之前，此接口以及所有与之相关的类以及调用方法都可以删除。
    /// </summary>
    /// <remarks>在每一次调用Execute方法的开关，都可以用如下代码来将对应项目的所有引用激活。
    /// DllActivator.DllActivator_Projects dat = new DllActivator.DllActivator_Projects();
    /// dat.ActivateReferences();
    /// </remarks>
    public interface IDllActivator
    {
        /// <summary> 激活本DLL所引用的那些DLLs </summary>
        void ActivateReferences();
    }

    /// <summary> 用于 AddinManager 中调试 dll 时将引用的程序集加载到进程中。
    /// 用于在IExternalCommand.Execute方法中，将整个项目的所有dll加载到进程中。
    /// 以避免出现在后面出现无法加载文件或者程序集的问题。
    /// 此接口是专门为AddinManager在调试时设计的，在最终软件发布之前，此接口以及所有与之相关的类以及调用方法都可以删除。
    /// </summary>
    /// <remarks>在每一次调用Execute方法的开关，都可以用如下代码来将对应项目的所有引用激活。
    /// DllActivator.DllActivator_Projects dat = new DllActivator.DllActivator_Projects();
    /// dat.ActivateReferences();
    /// </remarks>
    public class DllActivator_AddinManager: IDllActivator
    {
        /// <summary>
        /// 激活本DLL所引用的那些DLLs
        /// </summary>
        public void ActivateReferences()
        {

            // 因为此程序集没有引用任何需要边修改边测试的外部 dll，则不需要任何的实际代码，

            //IDllActivator_std dat1 = new DllActivator_std();
            //dat1.ActivateReferences();

            //
            //dat1 = new DllActivator_eZx_API();
            //dat1.ActivateReferences();
        }
    }
}