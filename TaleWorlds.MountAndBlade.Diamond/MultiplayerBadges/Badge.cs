using System;
using System.Globalization;
using System.Xml;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x0200016A RID: 362
	public class Badge
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0000FE55 File Offset: 0x0000E055
		public int Index { get; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0000FE5D File Offset: 0x0000E05D
		public BadgeType Type { get; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0000FE65 File Offset: 0x0000E065
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0000FE6D File Offset: 0x0000E06D
		public string StringId { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0000FE76 File Offset: 0x0000E076
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0000FE7E File Offset: 0x0000E07E
		public string GroupId { get; private set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0000FE87 File Offset: 0x0000E087
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0000FE8F File Offset: 0x0000E08F
		public TextObject Name { get; private set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0000FE98 File Offset: 0x0000E098
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public TextObject Description { get; private set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0000FEA9 File Offset: 0x0000E0A9
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0000FEB1 File Offset: 0x0000E0B1
		public bool IsVisibleOnlyWhenEarned { get; private set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0000FEBA File Offset: 0x0000E0BA
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0000FEC2 File Offset: 0x0000E0C2
		public DateTime PeriodStart { get; private set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0000FECB File Offset: 0x0000E0CB
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0000FED3 File Offset: 0x0000E0D3
		public DateTime PeriodEnd { get; private set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0000FEDC File Offset: 0x0000E0DC
		public bool IsActive
		{
			get
			{
				return DateTime.UtcNow >= this.PeriodStart && DateTime.UtcNow <= this.PeriodEnd;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0000FF02 File Offset: 0x0000E102
		public bool IsTimed
		{
			get
			{
				return this.PeriodStart > DateTime.MinValue || this.PeriodEnd < DateTime.MaxValue;
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0000FF28 File Offset: 0x0000E128
		public Badge(int index, BadgeType badgeType)
		{
			this.Index = index;
			this.Type = badgeType;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0000FF40 File Offset: 0x0000E140
		public virtual void Deserialize(XmlNode node)
		{
			this.StringId = node.Attributes["id"].Value;
			XmlAttributeCollection attributes = node.Attributes;
			string text;
			if (attributes == null)
			{
				text = null;
			}
			else
			{
				XmlAttribute xmlAttribute = attributes["group_id"];
				text = ((xmlAttribute != null) ? xmlAttribute.Value : null);
			}
			string text2 = text;
			this.GroupId = (string.IsNullOrWhiteSpace(text2) ? null : text2);
			string value = node.Attributes["name"].Value;
			string value2 = node.Attributes["description"].Value;
			XmlAttribute xmlAttribute2 = node.Attributes["is_visible_only_when_earned"];
			this.IsVisibleOnlyWhenEarned = Convert.ToBoolean((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
			XmlAttribute xmlAttribute3 = node.Attributes["period_start"];
			DateTime value3;
			this.PeriodStart = (DateTime.TryParse((xmlAttribute3 != null) ? xmlAttribute3.Value : null, CultureInfo.InvariantCulture, DateTimeStyles.None, out value3) ? DateTime.SpecifyKind(value3, DateTimeKind.Utc) : DateTime.MinValue);
			XmlAttribute xmlAttribute4 = node.Attributes["period_end"];
			DateTime value4;
			this.PeriodEnd = (DateTime.TryParse((xmlAttribute4 != null) ? xmlAttribute4.Value : null, CultureInfo.InvariantCulture, DateTimeStyles.None, out value4) ? DateTime.SpecifyKind(value4, DateTimeKind.Utc) : DateTime.MaxValue);
			this.Name = new TextObject(value, null);
			this.Description = new TextObject(value2, null);
		}
	}
}
