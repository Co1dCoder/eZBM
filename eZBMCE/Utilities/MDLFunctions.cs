using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using Bentley.MstnPlatformNET;

namespace eZBMCE.Utilities
{
    public static class MDLFunctions
    {

        #region  ---   Window 操作

        /*
   先用mdlWindow_viewWindowGet取得视图对应的MSWindow，再用mdlNativeWindow_getWindowHandle取得MSWindow对应的HWND，
   此时就可以调用MFC的函数来操作这个窗口了。

   在MicroStation的编程(MDL)中，视图(View)靠一个视图索引(viewIndex)来代表，
   但有时对View进行窗口操作时（如用程序修改View的位置或大小）又需要一个MSWindow的指针，
   mdlWindow_viewWindowGet能从一个viewIndex获取一个对应的MSWindow,
   反之，mdlView_indexFromWindow能从一个View的MSWindow获取其viewIndex。
   注意：viewIndex在编程时是以0为基准的，也就是说，viewIndex = 0代表的是“视图1”（MicroStation中最多有8个视图）。
   */

        [DllImport("stdmdlbltin.dll", SetLastError = true)]
        public static extern long mdlWindow_nativeWindowHandleGet(ref long nativeHandleP, long windowP, long type_);

        /// <summary>
        /// 通过视图取得对应的MSWindow句柄
        /// </summary>
        /// <param name="viewNum">视图编号，0对应视图1，7对应视图8</param>
        /// <returns></returns>
        [DllImport("stdmdlbltin.dll", SetLastError = true)]
        public static extern long mdlWindow_viewWindowGet(long viewNum);

        [DllImport("stdmdlbltin.dll", SetLastError = true)]
        public static extern long mdlWindow_resize(long windowP, long cornerNum, ref LPoint2d newPositionP);

        public struct LPoint2d
        {
            long X;
            long Y;
        }

        #endregion

        #region  ---   View 操作

        /// <summary>
        /// Fit the contents of the specified range in the given view. 
        /// </summary>
        /// <param name="minP">the minimum X Y and Z values of the range </param>
        /// <param name="maxP">the maximum X Y and Z values of the range </param>
        /// <param name="optionP">the FitViewOptions to apply to the view when it is redrawn </param>
        /// <param name="viewIndex">the index of the view to fit the specified range block in.
        /// viewIndex在编程时是以0为基准的，也就是说，viewIndex = 0代表的是“视图1”（MicroStation中最多有8个视图） </param>
        /// <returns>StatusInt:SUCCESS if the view is redrawn without error.  </returns>
        /// <remarks>Range points need to be in the view coordinate system, see mdlRMatrix_multiplyRange. 
        /// Unlike many other view functions, this function expects points in design file coordinates, so apply the currtrans via mdlCurrTrans_transformPointArray.
        /// </remarks>
        [DllImport("stdmdlbltin.dll", SetLastError = true)]
        public static extern long mdlView_fitViewToRange(ref DPoint3d minP, ref DPoint3d maxP, FitViewOptions optionP, long viewIndex);

        /// <summary>
        /// Options control structure for fitting views. 
        /// </summary>
        public enum FitViewOptions : ushort
        {
            /// <summary> make ActiveZ go to center of view volume </summary>
            forceActiveZToCenter = 1,
            /// <summary> move front and back planes out as needed </summary>
            expandClippingPlanes = 1,
            /// <summary> disableCenterCamera whether fit view re-centers the camera </summary>
            disableCenterCamera = 1,
            /// <summary> ignoreTransients whether transient elements should be included in fit range </summary>
            ignoreTransients = 1,
            /// <summary> dontIncludeParentsOfNestedRefs  if true, do only modelRefs in fitList, and not the parent references </summary>
            dontIncludeParentsOfNestedRefs = 1,
            /// <summary> ignoreCallouts  if true, callouts will be ignored </summary>
            ignoreCallouts = 1,
            /// <summary> optionPadding  reserved </summary>
            optionPadding = 10,
            /// <summary> optionPadding2 reserved </summary>
            optionPadding2 = 16,
        }

        /// <summary>
        /// Extract your element's range (two points: low left bottom point + high right top point). 
        /// Used to extract the range from an element. The range is transformed by the current transform if one exists. 
        /// </summary>
        /// <param name="rangeP">out: rangeP is the element range of type DVector3d*.  </param>
        /// <param name="pElement">is the input element</param>
        /// <returns>SUCCESS if the element range is extracted successfully - an appropriate error code otherwise. </returns>
        [DllImport("stdmdlbltin.dll", SetLastError = true)]
        public static extern long mdlElement_extractRange(ref long rangeP, long pElement);

        #endregion
    }
}
