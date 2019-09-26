using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Bentley.CifNET.LinearGeometry;
using Bentley.GeometryNET;
using Bentley.CifNET.Objects;
using Bentley.CifNET.Model;
using Bentley.DgnPlatformNET;
using Bentley.CifNET.GeometryModel.SDK.Edit;
using eZBMCE.AddinManager;

namespace eZBMCE.Cif.TemplateLib
{
    public class AttributeNames_Template
    {
        /// <summary> 组件名称 </summary>
        public const string name = "name";

        /// <summary> id值，比如"{D0DFCC90-42BD-4A02-9D5B-CF5EFF9A9BDA}"，多个模板之间的id值有重复时，仍然可以使用（当然最好不要重复） </summary>
        public const string oid = "oid";

        /// <summary>  </summary>
        public const string dragPoint = "dragPoint";

        /// <summary>  </summary>
        public const string description = "description";

        /// <summary>  </summary>
        public const string translationDX = "translationDX";

        /// <summary>  </summary>
        public const string translationDY = "translationDY";

    }

    public class AttributeNames_TemplateComponent
    {
        /// <summary> 组件名称 </summary>
        public const string name = "name";

        /// <summary> Use Name Override，没有特征名称覆盖的话，则为"" </summary>
        public const string nameOverride = "nameOverride";


        /// <summary> 组件的类型，如""EndCondition"" </summary>
        public const string type = "type";

        /// <summary> 组件的特征定义，如""TC_Draft-DNC"" </summary>
        public const string material = "material";

        /// <summary> 组件的特征定义所对应的路径（文件夹），如"Mesh\DNC\" </summary>
        public const string materialPath = "materialPath";

        /// <summary> 对应的显示规则，比如 "NOT 边坡填方判断_L " </summary>
        public const string displayExpression = "displayExpression";

        /// <summary> </summary>
        public const string description = "description";

        /// <summary> 是否为封闭组件 </summary>
        public const string isClosed = "isClosed";

        /// <summary> true或false，表示是否为空心组件 </summary>
        public const string isVoid = "isVoid";

        /// <summary> 非 void 则为 "None"  </summary>
        public const string voidType = "voidType";

        /// <summary> 组件下的“顶点”节点的名称 </summary>
        public const string node_Vertex = "Vertex";

    }

    /// <summary> 末端组件目标 </summary>
    public class AttributeNames_TemplateEndConditionTarget
    {
        /// <summary> 末端组件目标对象类型，比如"Surface" </summary>
        public const string targetType = "targetType";

        /// <summary> 末端组件目标对象的名称，没有则为"" </summary>
        public const string targetName = "targetName";


    }

    /// <summary> 组件上的顶点 </summary>
    public class AttributeNames_TemplateVertex
    {
        /// <summary> 组件上顶点的名称 </summary>
        public const string name = "name";

        /// <summary> 顶点所对应的圆角,比如"0.5"表示半径为0.5m的圆角，无圆角则为"0.00000000" </summary>
        public const string filletTangentLength = "filletTangentLength";
    }

    public class AttributeNames_TemplatePoint
    {
        /// <summary> 点名称 </summary>
        public const string name = "name";

        /// <summary> Feature Name Override，没有特征名称覆盖的话，则为"" </summary>
        public const string featureName = "featureName";

        /// <summary> 点的特征定义名称，如"TL_End Cond Cut Tie" </summary>
        public const string style = "stylePath";

        /// <summary> 点的特征定义所对应的路径（文件夹），如"Linear\Template Points\Grading\"  </summary>
        public const string stylePath = "stylePath";

        /// <summary> 点在模板中计算得到的x坐标，
        /// zengfy推测：如果点指定的两个约束，则此处记录的x或y值最终是不会生效的，如果只有一个或者没有约束，是点的定位才会与此x或y相关 </summary>
        public const string x = "x";

        /// <summary> 点在模板中计算得到的y坐标 
        /// zengfy推测：如果点指定的两个约束，则此处记录的x或y值最终是不会生效的，如果只有一个或者没有约束，是点的定位才会与此x或y相关 </summary>
        public const string y = "y";

    }

    /// <summary> 点的约束信息  </summary>
    public class AttributeNames_Constraint
    {
        /// <summary> 约束类型，无约束时为"None",其余有Horizontal、Vertical、Vector 等 </summary>
        public const string type = "type";

        /// <summary> 约束的值 </summary>
        public const string value = "value";

        /// <summary> 约束的 parent1 </summary>
        public const string parent = "parent";

        /// <summary> 有些约束类型没有 parent2，此时 XmlElement中根本就没有这一项 Attribute 的定义 </summary>
        public const string parent2 = "parent2";

        /// <summary> 约束类型值所对应的标签，有些约束类型没有 parametricLabel，此时 XmlElement中根本就没有这一项 Attribute 的定义 </summary>
        public const string parametricLabel = "parametricLabel";
    }

    /// <summary> 显示规则的信息  </summary>
    public class AttributeNames_DisplayRule
    {
        /// <summary> 显示规则的名称 </summary>
        public const string name = "name";

        /// <summary>  </summary>
        public const string description = "description";

        /// <summary> 显示规则的类型，如 "Component Is Displayed" </summary>
        public const string type = "parent";

        /// <summary> "Component Is Displayed"时指定的组件名称 </summary>
        public const string point1 = "point1";

    }
}

