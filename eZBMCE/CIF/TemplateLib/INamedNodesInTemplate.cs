using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using eZBMCE.Cif.TemplateLib;

namespace eZBMCE.CIF.TemplateLib
{

    internal interface INamedNodesInTemplate
    {
        /// <summary> 在itl文件中原本存储的节点名称 </summary>
        string OriginalName { get; set; }
        /// <summary> 在itl文件中原本存储的节点名称，移除其左右侧方向的后缀后的基准名称 </summary>
        string BaseName { get; set; }
        /// <summary> 在基准名称的基础上，修改后的新名称 </summary>
        string TargetName { get; set; }
    }

    internal class TemplatePoint : INamedNodesInTemplate
    {
        public XmlElement PointNode;
        /// <summary> 在itl文件中原本存储的节点名称 </summary>
        public string OriginalName { get; set; }
        /// <summary> 在itl文件中原本存储的节点名称，移除其左右侧方向的后缀后的基准名称 </summary>
        public string BaseName { get; set; }
        /// <summary> 在基准名称的基础上，修改后的新名称 </summary>
        public string TargetName { get; set; }

        public TemplatePoint(string name)
        {
            OriginalName = name;
            BaseName = TemplateDirectionSwitch.GetBaseName(name);
            TargetName = name;
        }
    }

    internal class TemplateComponent : INamedNodesInTemplate
    {
        public XmlElement ComponentNode;
        /// <summary> 在itl文件中原本存储的节点名称 </summary>
        public string OriginalName { get; set; }
        /// <summary> 在itl文件中原本存储的节点名称，移除其左右侧方向的后缀后的基准名称 </summary>
        public string BaseName { get; set; }
        /// <summary> 在基准名称的基础上，修改后的新名称 </summary>
        public string TargetName { get; set; }
        public TemplateComponent(string name)
        {
            OriginalName = name;
            BaseName = TemplateDirectionSwitch. GetBaseName(name);
            TargetName = name;
        }
    }

    internal class TemplateDisplayRule : INamedNodesInTemplate
    {
        public XmlElement DisplayRuleNode;
        /// <summary> 在itl文件中原本存储的节点名称 </summary>
        public string OriginalName { get; set; }
        /// <summary> 在itl文件中原本存储的节点名称，移除其左右侧方向的后缀后的基准名称 </summary>
        public string BaseName { get; set; }
        /// <summary> 在基准名称的基础上，修改后的新名称 </summary>
        public string TargetName { get; set; }
        public TemplateDisplayRule(string name)
        {
            OriginalName = name;
            BaseName = TemplateDirectionSwitch.GetBaseName(name);
            TargetName = name;
        }
    }
}
