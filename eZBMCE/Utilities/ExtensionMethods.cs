using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Bentley.DgnPlatformNET;
using Bentley.Interop.MicroStationDGN;

namespace eZBMCE.Utilities
{
    /// <summary> 扩展函数 </summary>
    public static class ExtensionMethods
    {
        #region ---   ElementEnumerator

        /// <summary>  </summary>
        /// <param name="elements"><seealso cref="Element"/>集合</param>
        public static Element[] ToArray(this ElementEnumerator elements)
        {
            return elements.BuildArrayFromContents();
            var elems = new List<Element>();
            Element elem;
            elem = elements.Current;
            while (elem != null)
            {
                elems.Add(elem);
                elements.MoveNext();
                elem = elements.Current;
            };
            return elems.ToArray();
        }
        #endregion

        #region ---   Element

        /// <summary> C# Element  转换为 ElemHandle 指针 </summary>
        public static IntPtr ToElementHandle(Bentley.DgnPlatformNET.Elements.Element element)
        {
            // Element scanElem = ElementTreeApplicationAddin.GetActiveDgnModel().FindElementById(ei.ElementID);
            GCHandle hObject = GCHandle.Alloc(element.ElementHandle, GCHandleType.Pinned);
            IntPtr pObject = hObject.AddrOfPinnedObject();
            if (hObject.IsAllocated)
            {
                // 这里操作pObject
                hObject.Free();
            }
            return pObject;
        }

        /// <summary> COM Element 与 Native Element 的相互转换 </summary>
        public static Bentley.Interop.MicroStationDGN.Element AsInteropElement(this
            Bentley.DgnPlatformNET.Elements.Element element, Bentley.Interop.MicroStationDGN.ModelReference modelRef)
        {
            var id = element.ElementId;
            return modelRef.GetElementByID(id);
        }

        /// <summary> COM Element 与 Native Element 的相互转换 </summary>
        public static Bentley.DgnPlatformNET.Elements.Element AsNativeElement(this
            Bentley.Interop.MicroStationDGN.Element element, Bentley.DgnPlatformNET.DgnModel model)
        {
            var id = element.ID;
            return model.FindElementById(new ElementId(ref id));
        }

        #endregion
    }
}
