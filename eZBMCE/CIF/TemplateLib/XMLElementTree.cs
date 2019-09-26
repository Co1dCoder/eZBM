using System.Xml;

namespace eZBMCE.Cif.TemplateLib
{
    /// <summary>
    /// 通过修改横断面模板的左右方向，以及点或组件名称后缀来切换横断面模板的左右方向
    /// </summary>
    public class XmlElementTree
    {
        /// <summary> 针对某个具体的节点，检查是否需要继续进行下一级节点的检索 </summary>
        public delegate bool CheckNeedKeepDigging(XmlElement ele);

        /// <summary> 针对节点的具体信息作出响应操作 </summary>
        public delegate void XmlElementManipulator(XmlElement ele);

        private XmlElement _parentElement;

        /// <summary> 通过递归的方式将XML中的节点加载到 TreeView 控件中 </summary>
        /// <param name="category"></param>
        /// <param name="checkKeepDigging">针对某个具体的节点，检查是否需要继续进行下一级节点的检索</param>
        /// <param name="elementManipulator">针对节点的具体信息作出响应操作</param>
        /// <returns></returns>
        public void ExpandElement(XmlElement category, CheckNeedKeepDigging checkKeepDigging,
            XmlElementManipulator elementManipulator)
        {
            foreach (var nd in category.ChildNodes)
            {
                if (nd is XmlElement)
                {
                    _parentElement = nd as XmlElement;

                    // 针对此节点的具体信息作出响应操作
                    elementManipulator(_parentElement);
                    // 继续向下检索
                    if (checkKeepDigging(_parentElement))
                    {
                        // 递归运算
                        ExpandElement(_parentElement, checkKeepDigging, elementManipulator);
                    }
                }
            }
        }
    }
}