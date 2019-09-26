using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsdiTransportation.SubgradeComponent;

namespace MsdiTransportation.SubgradeTemplateLib
{
    /// <summary> 具体的路基对象 </summary>
    internal class Subgrade
    {
        public List<SubgradeComp> Components { get; }

        /// <summary> 道路中心线位于某组件的哪一侧（用来判断其他组件是位于整个路基的哪一侧） </summary>
        public CenterLineSide CenterSide { get; private set; }

        /// <summary> 与道路中心线相关联的组件，集合中最多只有两个组件，而且下标值较小的为偏左侧的组件 </summary>
        public SubgradeComp[] CenterLineComps { get; private set; }

        public Subgrade()
        {
            Components = new List<SubgradeComp>();
        }

        /// <summary> 根据路基信息生成对应的 ORD 横断面数据 </summary>
        public void GenerateTemplate()
        {
            SortComponents();
            // 先生成道路中心线附近的相同组件
            if (CenterSide == CenterLineSide.Center)
            {
                CenterLineComps[0].RoadwayCompnt.ToOrdTemplate();
            }
        }

        /// <summary> 设置道路中心线信息 </summary>
        /// <param name="centerSide">道路中心线位于组件的哪一侧</param>
        /// <param name="centerLineComp1">集合中最多只有两个组件，此参数不能为 null </param>
        /// <param name="centerLineComp2">集合中最多只有两个组件，此参数可以为 null </param>
        public void SetCenterLineSide(CenterLineSide centerSide, SubgradeComp centerLineComp1, SubgradeComp centerLineComp2)
        {
            CenterSide = centerSide;
            //
            if (centerLineComp2 == null)
            {
                CenterLineComps = new SubgradeComp[] { centerLineComp1 };
            }
            else
            {
                // 确保 Index 小的放在集合前面
                CenterLineComps = centerLineComp1.Index < centerLineComp2.Index
                    ? new SubgradeComp[] { centerLineComp1, centerLineComp2 }
                    : new SubgradeComp[] { centerLineComp2, centerLineComp1 };
            }
        }

        /// <summary> 将组件集合中的元素按 Index 从小到大排列 </summary>
        private void SortComponents()
        {
            // 匿名函数是一个“内联”语句或表达式
            Comparison<SubgradeComp> componentIndexComparer = (s1, s2) =>
          {
              if (s1.Index < s2.Index)
              {
                  return -1;
              }
              else if (s1.Index < s2.Index)
              {
                  return 1;
              }
              else
              {
                  return 0;
              }
          };

            Components.Sort(componentIndexComparer);

            /*
             
        private int ComponentIndexComparer(SubgradeComp s1, SubgradeComp s2)
        {
            if (s1.Index < s2.Index)
            {
                return -1;
            }
            else if (s1.Index < s2.Index)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
             */
        }

    }
}