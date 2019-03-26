using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.MstnPlatformNET;

namespace eZBMCE.Utilities
{
    /// <summary> 一些常用的静态方式，用来辅助开发 </summary>
    public static class Utils
    {

        /// <summary> 从当前界面中的选择集中提取指定类型的对象 </summary>
        /// <returns>如果未选择指定类型的对象，则返回null</returns>
        public static T TryGetElement<T>() where T : Bentley.DgnPlatformNET.Elements.Element
        {
            var sel = GetSelection();
            T ele = null;
            foreach (var elem in sel)
            {
                ele = elem as T;
                if (ele != null)
                {
                    return ele;
                }
            }
            return null;
        }


        /// <summary> 获取当前选择集（Net） </summary>
        public static Bentley.DgnPlatformNET.Elements.Element[] GetSelection()
        {
            Bentley.DgnPlatformNET.ElementAgenda eleAgenda = new ElementAgenda();
            Bentley.DgnPlatformNET.Elements.Element ele = null;
            var dgnMdl = Session.Instance.GetActiveDgnModelRef();
            StatusInt status = SelectionSetManager.BuildAgenda(ref eleAgenda);
            var count = eleAgenda.GetCount();
            var selectedElements = new Element[count];
            for (uint i = 0; i < eleAgenda.GetCount(); i++)
            {
                status = SelectionSetManager.GetElement(i, ref ele, ref dgnMdl);
                selectedElements[i] = ele;
            }
            return selectedElements;
        }

        /// <summary> 获取当前选择集（Interop） </summary>
        public static Bentley.Interop.MicroStationDGN.Element[] GetSelection(Bentley.Interop.MicroStationDGN.Application app)
        {
            //throw new InvalidOperationException("选择对象时无法返回非 COM 对象");
            return app.ActiveModelReference.GetSelectedElements().BuildArrayFromContents();
        }

        /// <summary> 显示某Element的属性 </summary>
        public static string ShowElementProperties(Bentley.DgnPlatformNET.Elements.Element element)
        {
            var sb = new StringBuilder();
            sb.Append(element.Description);

            return sb.ToString();
        }

        /// <summary> Get definiton model. </summary>
        /// <param name="modelName">Definition model name. 如果输入null，则返回当前活动的DgnModel </param>
        /// <param name="createIfNotFound">Create a new definition model if the model does not exist.</param>
        /// <returns>Returns definition model.</returns>
        public static DgnModel GetDefinitionModel(string modelName = null, bool createIfNotFound = true)
        {
            if (null == modelName)
                return Session.Instance.GetActiveDgnModel();

            DgnFile activeFile = Session.Instance.GetActiveDgnFile();
            if (null == activeFile)
                return null;

            StatusInt loadStatus;
            DgnModel defModel = activeFile.LoadRootModelById(out loadStatus, activeFile.FindModelIdByName(modelName), true, true, false);
            if (null == defModel && createIfNotFound)
            {
                DgnModelStatus status;
                defModel = activeFile.CreateNewModel(out status, modelName, DgnModelType.Normal, true, null);
            }
            return defModel;
        }


        /// <summary> 刷新视口显示 </summary>
        public static void UpdateView(int viewNum)
        {
            Bentley.MstnPlatformNET.Session.Instance.Keyin("UPDATE VIEW EXTENDED");
        }

        /// <summary> 视口匹配对象 </summary>
        public static void FitView(int viewNum)
        {
            Bentley.MstnPlatformNET.Session.Instance.Keyin("FIT VIEW EXTENDED");
        }

    }
}
