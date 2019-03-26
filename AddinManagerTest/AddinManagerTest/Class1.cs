using System.Collections.Generic;
using Bentley.DgnPlatformNET;
using eZBMCE.AddinManager;

namespace AddinManagerTest
{
    [EcDescription("this is a great tool, which can be utilized for adding two numbers!")]
    public class Class1 : IBMExCommand
    {
        /// <summary> 某AddinManager调试命令 </summary>
        public ExternalCommandResult Execute(ref string errorMessage, ref IList<ElementId> elementSet)
        {
            var dllActi = new DllActivator_Test();
            dllActi.ActivateReferences();

            Form1 f = new Form1();
            f.Show(null);
            return ExternalCommandResult.Succeeded;
        }
    }
}