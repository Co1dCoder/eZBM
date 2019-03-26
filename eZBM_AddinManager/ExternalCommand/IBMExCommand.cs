using System;
using System.Collections.Generic;
using Bentley.DgnPlatformNET;

namespace eZBMCE.AddinManager
{
    /// <summary> 用来作为实现 <seealso cref="IBMExCommand"/> 的类的描述 </summary>
    public class EcDescriptionAttribute : Attribute
    {
        /// <summary> 具体的描述 </summary>
        public readonly string Description;

        public EcDescriptionAttribute(string description)
        {
            Description = description;
        }
    }

    /// <summary> AddinManager 中， <seealso cref="IBMExCommand"/> 外部命令的返回结果 </summary>
    public enum ExternalCommandResult
    {
        /// <summary> 外部命令执行被手动取消，事务取消 </summary>
        Cancelled = 0,

        /// <summary> 外部命令执行成功，提交事务 </summary>
        Succeeded = 1,

        /// <summary> 外部命令执行过程中出错 </summary>
        Failed = 2,
    }

    /// <summary> 用来进行AddinManager快速调试的接口。实现此接口的类必须有一个无参数的构造函数。
    /// 另外，推荐实现此接口的类所在程序集中也设计一个实现 IDllActivator_std 接口的类。 </summary>
    public interface IBMExCommand
    {
        /// <summary> Bentley AddinManger 快速调试插件 </summary>
        /// <param name="errorMessage">当返回值为<see cref="ExternalCommandResult.Failed"/> 或者在外部命令代码执行过程中出错时，这个属性代表给出的报错信息。</param>
        /// <param name="elementSet">当返回值为<see cref="ExternalCommandResult.Failed"/> 或者在外部命令代码执行过程中出错时，这个属性代表与出错内容相关的任何对象。</param>
        /// <returns></returns>
        ExternalCommandResult Execute(ref string errorMessage, ref IList<ElementId> elementSet);
    }
}