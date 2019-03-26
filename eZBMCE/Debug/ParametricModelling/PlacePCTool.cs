using System.Windows.Forms;
using System.Collections.Generic;

using Bentley.GeometryNET;
using Bentley.ECObjects.Instance;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.DgnPlatformNET.DgnEC;
using Bentley.MstnPlatformNET;

namespace Hello
{
    class PlacePCTool : DgnPrimitiveTool
    {
        private PCForm m_pcForm = null;

        public PlacePCTool(int toolId, int prompt) : base(toolId, prompt)
        {

        }
        protected override void OnRestartTool()
        {
            PlacePCTool tool = new PlacePCTool(0, 0);
            tool.InstallTool();
        }
        protected override void ExitTool()
        {
            base.ExitTool();
        }
        protected override void OnPostInstall()
        {
            if (null == m_pcForm)
            {
                m_pcForm = new PCForm();
                m_pcForm.AttachToToolSettings(null);
                m_pcForm.Show();
            }
            AccuSnap.SnapEnabled = true;
            BeginDynamics();

            base.OnPostInstall();
        }
        protected override void OnCleanup()
        {
            if (null != m_pcForm)
            {
                m_pcForm.DetachFromMicroStation();
                m_pcForm = null;
            }
        }
        protected override bool OnDataButton(DgnButtonEvent ev)
        {
            double width = double.Parse(m_pcForm.cabinetWidth.Text);
            Element pc = CreateParametricCell(ev.Point, width);
            if (null == pc)
                return false;

            pc.AddToModel();
            return true;
        }
        protected override void OnDynamicFrame(DgnButtonEvent ev)
        {
            double width = double.Parse(m_pcForm.cabinetWidth.Text);
            Element pc = CreateParametricCell(ev.Point, width);
            if (null == pc)
                return;

            RedrawElems redrawElems = new RedrawElems();
            redrawElems.SetDynamicsViewsFromActiveViewSet(Bentley.MstnPlatformNET.Session.GetActiveViewport());
            redrawElems.DrawMode = DgnDrawMode.TempDraw;
            redrawElems.DrawPurpose = DrawPurpose.Dynamics;

            redrawElems.DoRedraw(pc);
        }
        protected override bool OnResetButton(DgnButtonEvent ev)
        {
            ExitTool();
            return true;
        }
        public static void InstallNewInstance ()
        {
            PlacePCTool tool = new PlacePCTool(0, 0);
            tool.InstallTool();
        }

        private static DgnModel LocateCellModel(string name)
        {
            var opts = CellLibraryOptions.Include3d | CellLibraryOptions.IncludeAllLibraries | CellLibraryOptions.IncludeParametric;
            var libs = new CellLibraryCollection(opts);
            DgnModel cellModel = null;
            foreach (var lib in libs)
            {
                MessageCenter.Instance.ShowInfoMessage(lib.Name, lib.Name, false);
                if (name.Equals(lib.Name))
                {
                    StatusInt status;
                    cellModel = lib.File.LoadRootModelById(out status, lib.File.FindModelIdByName(lib.Name), true, false, true);
                    break;
                }
            }
            return cellModel;
        }
        private ParametricCellElement CreateParametricCell(DPoint3d origin, double width)
        {
            const string pcName = "Double Door Cabinet";
            const string setName = "Standard";
            var dgnFile = Session.Instance.GetActiveDgnFile();
            var dgnModel = Session.Instance.GetActiveDgnModel();
            var pcDef = ParametricCellDefinitionElement.FindByName(pcName, dgnFile);
            if (null == pcDef)  //Not find cell def in active design file
            {
                var cellModel = LocateCellModel(pcName);
                if (null == cellModel)
                {
                    MessageCenter.Instance.ShowErrorMessage("Not found cell", null, true);
                    return null;
                }
                var hdlr = DgnComponentDefinitionHandler.GetForModel(cellModel);
                var status = hdlr.DefinitionModelHandler.CreateCellDefinition(dgnFile);
                if (ParameterStatus.Success == status)
                    pcDef = ParametricCellDefinitionElement.FindByName(pcName, dgnFile);
                else
                {
                    MessageCenter.Instance.ShowErrorMessage("Error Creating cellDef", null, true);
                    return null;
                }
            }
            //IECStructArrayValue, IECArrayValue, IECStructValue, IECPropertyValue

            //IECStructType structType = new ECClass("PipingMode");

            //IECClass testClass = new ECClass("TestClass");
            //testClass.Add(new ECProperty("pipingMode", structType));
            //testClass.Add(new ECProperty("pointList", new ECArrayType(ECObjects.PointType)));

            var pc = ParametricCellElement.Create(pcDef, setName, dgnModel);
            IDgnECInstance inst = pc.Parameters as IDgnECInstance;

            //System.IO.StreamWriter output = new System.IO.StreamWriter("D:\\dump.txt");
            //inst.Dump(output, null);
            //output.Close();

            IECArrayValue arr = inst.GetPropertyValue("ParameterValuesContainer") as IECArrayValue;
            IECStructValue structVal = arr[1] as IECStructValue;  // .Adhoc_Name = _LOCAL_Cabinet_W
            structVal.SetValue("Adhoc_Value", width);

            inst.ScheduleChanges(pc);

            DTransform3d trans = DTransform3d.Identity;
            trans.Translation = new DVector3d(origin);
            TransformInfo transInfo = new TransformInfo(trans);
            pc.ApplyTransform(transInfo);

            return pc;
        }
    }
}
