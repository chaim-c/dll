using System;
using System.Collections.Generic;
using System.Xml;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000171 RID: 369
	public class ConditionalBadge : Badge
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00010C6E File Offset: 0x0000EE6E
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x00010C76 File Offset: 0x0000EE76
		public IReadOnlyList<BadgeCondition> BadgeConditions { get; private set; }

		// Token: 0x06000A3A RID: 2618 RVA: 0x00010C7F File Offset: 0x0000EE7F
		public ConditionalBadge(int index, BadgeType badgeType) : base(index, badgeType)
		{
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00010C8C File Offset: 0x0000EE8C
		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			List<BadgeCondition> list = new List<BadgeCondition>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Condition")
				{
					BadgeCondition item = new BadgeCondition(list.Count, xmlNode);
					list.Add(item);
				}
			}
			this.BadgeConditions = list;
		}
	}
}
