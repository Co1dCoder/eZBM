using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using Bentley.MstnPlatformNET;

namespace eZBMCE.Examples
{
    public class GeometryCreator
    {
        #region ---   线条

        /// <summary> 创建线串对象 </summary>
        /// <returns>比如：Line String（由 line 组成）</returns>
        public static LineStringElement CreateLineString(DgnModel model, Element templElement, DPoint3d[] points)
        {
            return new LineStringElement(model, templElement, points);
        }

        /// <summary> 创建圆弧或椭圆弧 </summary>
        /// <returns> </returns>
        public static ArcElement CreateArc(DgnModel model)
        {
            DPoint3d center = new DPoint3d(0, 0, 0);
            double axis1 = 10; // 椭圆上x轴的长度
            double axis2 = 10; // 椭圆上y轴的长度
            return new ArcElement(model, null, center, axis1, axis2, 0, Math.PI, Math.PI);
        }

        /// <summary> 创建复杂链对象 </summary>
        /// <returns>比如：Complex Chain（由 line、arc组成）</returns>
        public static ComplexStringElement CreateComplexString(DgnModel model, Element templElement, DPoint3d[] points)
        {
            //creaet header
            ComplexStringElement cse = new ComplexStringElement(model, null);
            return null;
        }


        #endregion

        #region ---   多边形

        private Bentley.DgnPlatformNET.Elements.ComplexShapeElement CreateComplexShape(DgnModel model,
            DPoint3d[] points, DPoint3d center, bool isHole, string modelName = null)
        {
            //creaet header
            ComplexShapeElement shapeEl = new ComplexShapeElement(model, null);

            LineStringElement lineStringEl = new LineStringElement(model, null, points);
            if (Bentley.DgnPlatformNET.BentleyStatus.Success != shapeEl.AddComponentElement(lineStringEl))
                return null;

            double radius = Math.Abs(points[2].X - points[1].X) / 2.0;
            ArcElement ehArc = CreateArc(model);
            if (BentleyStatus.Success != shapeEl.AddComponentElement(ehArc))
                return null;

            if (BentleyStatus.Success != shapeEl.AddComponentComplete())
                return null;

            return shapeEl;
        }
        #endregion

    }
}
