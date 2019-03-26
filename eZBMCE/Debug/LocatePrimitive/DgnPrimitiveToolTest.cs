using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bentley.DgnPlatformNET;

namespace eZBMCE.LocatePrimitive
{
    class DgnPrimitiveToolTest : DgnPrimitiveTool
    {
        public DgnPrimitiveToolTest(int toolName, int toolPrompt) : base(toolName, toolPrompt)
        {
        }

        protected override bool OnResetButton(DgnButtonEvent ev)
        {
            return false;
        }

        protected override bool OnDataButton(DgnButtonEvent ev)
        {
            return false;
        }

        protected override void OnRestartTool()
        {
        }
    }
}
