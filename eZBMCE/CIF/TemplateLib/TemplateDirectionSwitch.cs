using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using TempLib = eZBMCE.CIF.TemplateLib;
using eZstd.Data;
using eZstd.Miscellaneous;
using TemplateComponent = Bentley.CifNET.GeometryModel.SDK.Edit.TemplateComponent;
using TemplatePoint = Bentley.CifNET.GeometryModel.SDK.Edit.TemplatePoint;

namespace eZBMCE.Cif.TemplateLib
{
    /// <summary> 通过修改横断面模板的左右方向，以及点或组件名称后缀来切换横断面模板的左右方向 </summary>
    internal class TemplateDirectionSwitch
    {
        /// <summary> 方向转换操作的具体方式 </summary>
        public enum SwitchType
        {
            /// <summary> 未进行转换 </summary>
            UnSwitched,
            /// <summary> 目标模板与选中模板同名，此时修改了自己的内容 </summary>
            ReplacedMySelf,
            /// <summary> 目标模板在文件夹中已经存在，此时将那个同名的模板内容进行修改 </summary>
            ReplacedDuplicated,
            /// <summary> 目标模板在文件夹中不存在，新增一个模板 </summary>
            Appended,
        }


        /// <summary> 基准模板是在左侧还是在右侧 </summary>
        private readonly bool _isLeft;
        private bool _hasBeenSwitched;

        /// <summary> 目标模板是在左侧还是在右侧 </summary>
        private readonly bool _toLeft;
        private readonly string _directionSuff;

        /// <summary> 基准模板的名称（不含左右侧信息） </summary>
        private readonly string _templateBaseName;

        /// <summary> 基准模板节点 </summary>
        private readonly XmlElement _baseTemplateElement;

        /// <summary> 指定的横断面模板节点所对应的父节点，Category </summary>
        private readonly XmlElement _categoryNode;

        private XmlElement _templateElementToModify;
        private const string TemplateName_LeftSideSuffix = "_ML";
        private const string TemplateName_RightSideSuffix = "_MR";
        private const string Pattern_leftRightSuff = @"_L|_R";
        private const string AscendingSuffix = @"1";

        /// <summary>
        /// 设置ORD中横断面模板的左右方向
        /// </summary>
        /// <param name="toLeft"> false表示设置为右侧模板 </param>
        public TemplateDirectionSwitch(XmlElement templateElement, string templateBaseName, bool isLeft, bool toLeft)
        {
            _baseTemplateElement = templateElement;
            _categoryNode = templateElement.ParentNode as XmlElement;
            _isLeft = isLeft;
            _toLeft = toLeft;
            _templateBaseName = templateBaseName;
            _directionSuff = toLeft ? "_L" : "_R";
            _hasBeenSwitched = false;
            //
        }

        /// <summary> 判断此Template节点是否对应一个三维线性法中的侧边模板 </summary>
        /// <param name="templateElement"></param>
        /// <returns></returns>
        public static bool? IsGradeSideTemplate(XmlElement templateElement, out string templateBaseName)
        {
            templateBaseName = null;
            var name = XmlHandler.GetAttributeValue(templateElement, AttributeNames_Template.name);
            if (name.EndsWith(TemplateName_LeftSideSuffix, comparisonType: StringComparison.OrdinalIgnoreCase))
            {
                templateBaseName = name.Substring(0, name.Length - 3);
                return true;
            }
            else if (name.EndsWith(TemplateName_RightSideSuffix, comparisonType: StringComparison.OrdinalIgnoreCase))
            {
                templateBaseName = name.Substring(0, name.Length - 3);
                return false;
            }
            else
            {
                return null;
            }
        }

        /// <summary> 某个文件夹中已经处理过的模板的名称集合 </summary>
        private static List<string> _handledTemplateNames;

        /// <summary> 在开始转换前后，都需要将已经处理过的模板集合进行清空 </summary>
        public static void RefreshSwitchPool()
        {
            _handledTemplateNames = new List<string>();
        }

        #region --- TemplateDefinition 的获取与复制

        /// <summary> 对指定侧的模板进行修改，或者新建一个对向侧的模板再进行修改 </summary>
        public SwitchType GetTemplateNodeToModify(out XmlElement templateToHandle)
        {
            var switchType = SwitchType.UnSwitched;
            if (_isLeft == _toLeft)
            {
                // 直接对原模块进行修改
                _templateElementToModify = _baseTemplateElement;
                switchType = SwitchType.ReplacedMySelf;
            }
            else
            {
                // 在基准模板所在文件夹中创建一个新的模板文件，并对其进行修改
                var newTemplateName = _templateBaseName + (_toLeft ? "_ML" : "_MR");
                if (_handledTemplateNames.Contains(newTemplateName))
                {
                    switchType = SwitchType.UnSwitched;
                    templateToHandle = _baseTemplateElement; // 设置任何值都不会生效
                    return switchType;
                }
                _handledTemplateNames.Add(newTemplateName);

                // 新名称的模板还没有修改过
                _templateElementToModify = _baseTemplateElement.Clone() as XmlElement;
                XmlHandler.SetAttributeValue(_templateElementToModify, AttributeNames_Template.name, newTemplateName);
                XmlHandler.SetAttributeValue(_templateElementToModify, AttributeNames_Template.oid, @"{" + Guid.NewGuid() + @"}");

                // 1. 检查原文件夹中是否有同名模板
                bool hasDuplicatedTemplate = false;
                var templates = _categoryNode.SelectNodes("Template");
                foreach (XmlElement template in templates)
                {
                    if (
                        String.Compare(XmlHandler.GetAttributeValue(template, AttributeNames_Template.name), newTemplateName,
                            StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        // 替换同名模板
                        switchType = SwitchType.ReplacedDuplicated;
                        _categoryNode.ReplaceChild(_templateElementToModify, template);
                        hasDuplicatedTemplate = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (!hasDuplicatedTemplate)
                {
                    // 创建一个新的模板
                    switchType = SwitchType.Appended;
                    _categoryNode.AppendChild(_templateElementToModify);
                }
            }

            templateToHandle = _templateElementToModify;
            return switchType;
        }

        #endregion


        #region --- 转换具体某个模板的所有内容的方向

        /// <summary> 将模板中的所有点(不包括组件、显示规则）按其位置是在原点的左侧还是右侧，来对应地修改点的名称 </summary>
        /// <param name="templateNode"></param>
        public void SwitchTemplatePointsDirectionByX(XmlNode templateNode, double tolerance)
        {
            tolerance = 0.0001; // 即在原点左右0.1mm内，将认为其与原点对齐

        }


        /// <summary> 将模板中的所有点、组件、显示规则都转换到某一个特定的方向，而不管其在模板中的几何定位 </summary>
        /// <param name="templateNode"></param>
        public void SwitchTemplateDirection(XmlNode templateNode)
        {
            if (_hasBeenSwitched) return;
            _hasBeenSwitched = true;

            //
            string directionSuff = _directionSuff;
            //TemplateDefinition template = TemplateDefinition.LoadXml(templateNode.OuterXml);
            // 设置 TemplatePoint 的方向
            XmlNodeList points = templateNode.SelectNodes(@"Points/Point");
            var allPointsInTemplate = ConstructPointsInTemplate(points);
            ReConstructNamesInTemplate(allPointsInTemplate, directionSuff);

            foreach (var pointNode in allPointsInTemplate)
            {
                SwitchTemplatePointDirection(pointNode, directionSuff, allPointsInTemplate);
            }


            // 设置 TemplateDisplayRule 的方向
            XmlNodeList displayRules = templateNode.SelectNodes(@"DisplayRules/DisplayRule");
            var allDisplayRulesInTemplate = ConstructDisplayRulesInTemplate(displayRules);
            ReConstructNamesInTemplate(allDisplayRulesInTemplate, directionSuff);

            foreach (var displayRule in allDisplayRulesInTemplate)
            {
                SwitchTemplateDisplayRuleDirection(displayRule, directionSuff);
            }

            // 设置 TemplateComponent 的方向
            XmlNodeList components = templateNode.SelectNodes(@"Components/Component");
            var allComponentsInTemplate = ConstructComponentsInTemplate(components);
            ReConstructNamesInTemplate(allComponentsInTemplate, directionSuff);
            foreach (var componentNode in allComponentsInTemplate)
            {
                SwitchTemplateComponentDirection(componentNode, directionSuff, allPointsInTemplate, allDisplayRulesInTemplate);
            }
            // 
        }

        private static void SwitchTemplatePointDirection(TempLib.TemplatePoint templatePoint, string directionSuff,
            TempLib.TemplatePoint[] allPointsInTemplate)
        {
            var pointNode = templatePoint.PointNode;
            string baseName;
            // 设置点名称的方向
            var value = templatePoint.TargetName;
            XmlHandler.SetAttributeValue(pointNode, AttributeNames_TemplatePoint.name, value);

            // 设置点的特征名称覆盖名称
            value = XmlHandler.GetAttributeValue(pointNode, AttributeNames_TemplatePoint.featureName);
            if (!string.IsNullOrEmpty(value))
            {
                baseName = GetBaseName(value);
                value = AppendDirectionSuffix(baseName, directionSuff);
                XmlHandler.SetAttributeValue(pointNode, AttributeNames_TemplatePoint.featureName, value);
            }

            string parentPoint;
            string parentPointNewName;
            // 修改约束标签的左右值
            XmlNodeList constraints = pointNode.SelectNodes(@"Constraint");
            foreach (XmlElement constraint in constraints)
            {
                // 修改约束标签的左右值
                value = XmlHandler.GetAttributeValue(constraint, AttributeNames_Constraint.parametricLabel);
                if (!string.IsNullOrEmpty(value))
                {
                    baseName = GetBaseName(value);
                    value = AppendDirectionSuffix(baseName, directionSuff);
                    XmlHandler.SetAttributeValue(constraint, AttributeNames_Constraint.parametricLabel, value);
                }

                // 修改约束的 parent 的名称
                parentPoint = XmlHandler.GetAttributeValue(constraint, AttributeNames_Constraint.parent);
                if (!string.IsNullOrEmpty(parentPoint))
                {
                    parentPointNewName = allPointsInTemplate.First(r => r.OriginalName == parentPoint).TargetName;
                    XmlHandler.SetAttributeValue(constraint, AttributeNames_Constraint.parent, parentPointNewName);
                }

                parentPoint = XmlHandler.GetAttributeValue(constraint, AttributeNames_Constraint.parent2);
                if (!string.IsNullOrEmpty(parentPoint))
                {
                    parentPointNewName = allPointsInTemplate.First(r => r.OriginalName == parentPoint).TargetName;
                    XmlHandler.SetAttributeValue(constraint, AttributeNames_Constraint.parent2, parentPointNewName);
                }
            }
        }

        private static void SwitchTemplateDisplayRuleDirection(TempLib.TemplateDisplayRule templateDisplayRule, string directionSuff)
        {

            var displayRuleNode = templateDisplayRule.DisplayRuleNode;
            string baseName;
            // 设置显示规则名称的方向
            var value = templateDisplayRule.TargetName;
            XmlHandler.SetAttributeValue(displayRuleNode, AttributeNames_DisplayRule.name, value);


            // 设置显示规则基准对象名称
            value = XmlHandler.GetAttributeValue(displayRuleNode, AttributeNames_DisplayRule.point1);
            baseName = GetBaseName(value);
            value = AppendDirectionSuffix(baseName, directionSuff);
            XmlHandler.SetAttributeValue(displayRuleNode, AttributeNames_DisplayRule.point1, value);
        }

        private static void SwitchTemplateComponentDirection(TempLib.TemplateComponent templateComponent, string directionSuff,
            TempLib.TemplatePoint[] allPointsInTemplate, TempLib.TemplateDisplayRule[] allDisplayRulesInTemplate)
        {

            var componentNode = templateComponent.ComponentNode;
            string baseName;
            // 设置组件名称的方向
            var value = templateComponent.TargetName;
            XmlHandler.SetAttributeValue(componentNode, AttributeNames_TemplateComponent.name, value);

            // 设置组件名称替代的方向
            value = XmlHandler.GetAttributeValue(componentNode, AttributeNames_TemplateComponent.nameOverride);
            if (!string.IsNullOrEmpty(value))
            {
                baseName = GetBaseName(value);
                value = AppendDirectionSuffix(baseName, directionSuff);
                XmlHandler.SetAttributeValue(componentNode, AttributeNames_TemplateComponent.nameOverride, value);
            }

            // 设置组件的显示规则
            value = XmlHandler.GetAttributeValue(componentNode, AttributeNames_TemplateComponent.displayExpression);
            if (string.IsNullOrEmpty(value))
            {
                var displayExpression = XmlHandler.GetAttributeValue(componentNode, AttributeNames_TemplateComponent.displayExpression);
                if (!string.IsNullOrEmpty(displayExpression))
                {
                    value = RematchDisplayExpression(displayExpression, allDisplayRulesInTemplate);
                    XmlHandler.SetAttributeValue(componentNode, AttributeNames_TemplateComponent.displayExpression, value);
                }
            }

            // 设置组件顶点的名称
            string originalVertexName;
            string newVertexName;
            XmlNodeList vertexs = componentNode.SelectNodes(AttributeNames_TemplateComponent.node_Vertex);
            foreach (XmlElement vertex in vertexs)
            {
                originalVertexName = XmlHandler.GetAttributeValue(vertex, AttributeNames_TemplateVertex.name);
                newVertexName = allPointsInTemplate.First(r => r.OriginalName == originalVertexName).TargetName;
                XmlHandler.SetAttributeValue(vertex, AttributeNames_TemplateVertex.name, newVertexName);
            }
        }

        private static TempLib.TemplatePoint[] ConstructPointsInTemplate(XmlNodeList points)
        {
            var allTemplatePoints = new TempLib.TemplatePoint[points.Count];
            string name;
            int index = 0;
            foreach (XmlElement pointNode in points)
            {
                name = XmlHandler.GetAttributeValue(pointNode, AttributeNames_TemplatePoint.name);
                var tp = new TempLib.TemplatePoint(name);
                tp.PointNode = pointNode;
                allTemplatePoints[index] = (tp);
                index += 1;
            }
            Array.Sort(allTemplatePoints as TempLib.INamedNodesInTemplate[], new NamedNodesComparer());
            return allTemplatePoints;
        }

        private static TempLib.TemplateComponent[] ConstructComponentsInTemplate(XmlNodeList components)
        {
            var allTemplateComponents = new TempLib.TemplateComponent[components.Count];
            string name;
            int index = 0;
            foreach (XmlElement componentNode in components)
            {
                name = XmlHandler.GetAttributeValue(componentNode, AttributeNames_TemplateComponent.name);
                var tp = new TempLib.TemplateComponent(name);
                tp.ComponentNode = componentNode;
                allTemplateComponents[index] = (tp);
                index += 1;
            }
            Array.Sort(allTemplateComponents as TempLib.INamedNodesInTemplate[], new NamedNodesComparer());
            return allTemplateComponents;
        }

        private static TempLib.TemplateDisplayRule[] ConstructDisplayRulesInTemplate(XmlNodeList displayRules)
        {
            var allTemplateDisplayRules = new TempLib.TemplateDisplayRule[displayRules.Count];
            string name;
            int index = 0;
            foreach (XmlElement displayRuleNode in displayRules)
            {
                name = XmlHandler.GetAttributeValue(displayRuleNode, AttributeNames_DisplayRule.name);
                var tp = new TempLib.TemplateDisplayRule(name);
                tp.DisplayRuleNode = displayRuleNode;
                allTemplateDisplayRules[index] = (tp);
                index += 1;
            }
            Array.Sort(allTemplateDisplayRules as TempLib.INamedNodesInTemplate[], new NamedNodesComparer());
            return allTemplateDisplayRules;
        }

        public class NamedNodesComparer : IComparer<TempLib.INamedNodesInTemplate>
        {
            public int Compare(TempLib.INamedNodesInTemplate x, TempLib.INamedNodesInTemplate y)
            {
                return x.OriginalName.CompareTo(y.OriginalName);
            }
        }

        private static void ReConstructNamesInTemplate(TempLib.INamedNodesInTemplate[] namedNodesInTemplate, string directionSuff)
        {
            {
                // baseNames 从小往大排，可能出现同名
                var baseNames = namedNodesInTemplate.Select(r => r.BaseName).ToArray();
                var count = namedNodesInTemplate.Length;
                if (count == 0)
                {
                    return;
                }
                else
                {
                    // 第一个元素的名称不会出现同名，因为已经按字符大小排序过了
                    namedNodesInTemplate[0].TargetName = namedNodesInTemplate[0].BaseName + directionSuff;

                    // 从第二个元素开始批量修改
                    int searchStart = 0;
                    int searchEnd = 0;

                    string nameToModify;
                    for (int modifyingIndex = 1; modifyingIndex < count; modifyingIndex++)
                    {
                        nameToModify = baseNames[modifyingIndex];
                        var lastSearchedind = Array.IndexOf(baseNames, baseNames[modifyingIndex], searchStart, modifyingIndex - searchStart);
                        bool findDuplicated = false;
                        while (lastSearchedind >= 0)
                        {
                            findDuplicated = true;
                            nameToModify = nameToModify + AscendingSuffix;
                            lastSearchedind = Array.IndexOf(baseNames, nameToModify, lastSearchedind, modifyingIndex - lastSearchedind);
                        }
                        baseNames[modifyingIndex] = nameToModify;
                        namedNodesInTemplate[modifyingIndex].BaseName = nameToModify;
                        namedNodesInTemplate[modifyingIndex].TargetName = nameToModify + directionSuff;
                        if (!findDuplicated)
                        {
                            searchStart = modifyingIndex;
                        }
                    }
                }

            }
        }

        #endregion

        /// <summary> 根据基准名与左右侧方向，设置最终的左右后缀 </summary>
        /// <param name="baseName"></param>
        /// <param name="directionSuff"></param>
        /// <returns>输入字符类似 "_Liter_Right_EOP"，返回值类似 "_Liter_Right_EOP_L"</returns>
        private static string AppendDirectionSuffix(string baseName, string directionSuff)
        {
            var ret = baseName.Insert(baseName.Length, directionSuff);
            return ret;
        }

        /// <summary>从带有左右方向后缀的名称中，提取出基准名称</summary>
        /// <param name="name"></param>
        /// <returns>输入字符类似 "_Liter_Right_EOP_L_R_R_L_r"，返回值类似 "_Liter_Right_EOP"</returns>
        public static string GetBaseName(string name)
        {
            int totalLength = name.Length;
            int lastIndex = totalLength;
            int valueLength;
            var m = Regex.Match(name, Pattern_leftRightSuff, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            while (m.Success)
            {
                var g = m.Groups[0];
                // 判断搜索到的后缀字符是否位于字符的最后
                valueLength = g.Value.Length;
                if (g.Index == lastIndex - valueLength)
                {
                    lastIndex = g.Index;
                }
                m = m.NextMatch();
            }
            var baseName = name.Substring(0, lastIndex);
            // 测试 
            // name = "_Liter_Right_EOP_L_R_R_L_r";
            // baseName = "_Liter_Right_EOP";

            return baseName;
        }

        /// <summary> 显示规则表达式中的关键字 </summary>
        private static readonly string[] DisplayExpressionManipulators = { "AND", "OR", "NOT", "(", ")" };

        /// <summary> 转换组件的显示规则表达式，比如 "NOT 边坡填方判断_L " </summary>
        /// <param name="displayExpression"></param>
        /// <param name="allDisplayRulesInTemplate"></param>
        /// <returns>输入字符类似  "NOT 边坡填方判断_L "，返回值类似  "NOT 边坡填方判断_R " </returns>
        private static string RematchDisplayExpression(string displayExpression, TempLib.TemplateDisplayRule[] allDisplayRulesInTemplate)
        {
            // 比如 "NOT 边坡填方判断_L "
            const char seperator = ' ';
            StringBuilder sb = new StringBuilder();
            var items = displayExpression.Split(seperator);
            string item;
            for (int i = 0; i < items.Length; i++)
            {
                item = items[i];
                item = item.Trim();
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                if (DisplayExpressionManipulators.Contains(item))
                {
                    sb.Append(item);
                }
                else
                {
                    // 说明这段字符为显示规则名称
                    //MessageBox.Show("|" + item + "|");
                    var newDisplayExp = allDisplayRulesInTemplate.First(r => r.OriginalName == item).TargetName;
                    sb.Append(newDisplayExp);
                }
                sb.Append(seperator);
            }
            return sb.ToString();
        }
    }
}

