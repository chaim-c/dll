using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x0200016E RID: 366
	public class BadgeCondition
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0001008A File Offset: 0x0000E28A
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x00010092 File Offset: 0x0000E292
		public ConditionType Type { get; private set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0001009B File Offset: 0x0000E29B
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x000100A3 File Offset: 0x0000E2A3
		public ConditionGroupType GroupType { get; private set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x000100AC File Offset: 0x0000E2AC
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x000100B4 File Offset: 0x0000E2B4
		public TextObject Description { get; private set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000100BD File Offset: 0x0000E2BD
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x000100C5 File Offset: 0x0000E2C5
		public string StringId { get; private set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x000100CE File Offset: 0x0000E2CE
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x000100D6 File Offset: 0x0000E2D6
		public IReadOnlyDictionary<string, string> Parameters { get; private set; }

		// Token: 0x06000A26 RID: 2598 RVA: 0x000100E0 File Offset: 0x0000E2E0
		public BadgeCondition(int index, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			ConditionType type;
			if (!Enum.TryParse<ConditionType>((attributes != null) ? attributes["type"].Value : null, true, out type))
			{
				type = ConditionType.Custom;
				Debug.FailedAssert("No 'type' was provided for a condition", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", ".ctor", 47);
			}
			this.Type = type;
			ConditionGroupType groupType = ConditionGroupType.Any;
			XmlAttributeCollection attributes2 = node.Attributes;
			bool flag;
			if (attributes2 == null)
			{
				flag = (null != null);
			}
			else
			{
				XmlAttribute xmlAttribute = attributes2["group_type"];
				flag = (((xmlAttribute != null) ? xmlAttribute.Value : null) != null);
			}
			if (flag && !Enum.TryParse<ConditionGroupType>(node.Attributes["group_type"].Value, true, out groupType))
			{
				Debug.FailedAssert("Provided 'group_type' was wrong for a condition", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", ".ctor", 54);
			}
			this.GroupType = groupType;
			XmlAttributeCollection attributes3 = node.Attributes;
			this.Description = new TextObject((attributes3 != null) ? attributes3["description"].Value : null, null);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Parameter")
				{
					string key = xmlNode.Attributes["name"].Value.Trim();
					string value = xmlNode.Attributes["value"].Value.Trim();
					dictionary[key] = value;
				}
			}
			this.Parameters = dictionary;
			XmlAttributeCollection attributes4 = node.Attributes;
			string stringId;
			if (attributes4 == null)
			{
				stringId = null;
			}
			else
			{
				XmlAttribute xmlAttribute2 = attributes4["id"];
				stringId = ((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
			}
			this.StringId = stringId;
			string str;
			if (this.StringId == null && this.Parameters.TryGetValue("property", out str))
			{
				this.StringId = str + ((this.GroupType == ConditionGroupType.Party) ? ".Party" : ((this.GroupType == ConditionGroupType.Solo) ? ".Solo" : ""));
			}
			if (this.StringId == null)
			{
				this.StringId = "condition." + index;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00010308 File Offset: 0x0000E508
		public bool Check(string value)
		{
			ConditionType type = this.Type;
			if (type != ConditionType.PlayerData)
			{
				return false;
			}
			string b;
			if (!this.Parameters.TryGetValue("value", out b))
			{
				Debug.FailedAssert("Given condition doesn't have a value parameter", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", "Check", 94);
				return false;
			}
			return value == b;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00010358 File Offset: 0x0000E558
		public bool Check(int value)
		{
			ConditionType type = this.Type;
			if (type - ConditionType.PlayerDataNumeric > 2)
			{
				return false;
			}
			string s;
			if (this.Parameters.TryGetValue("value", out s))
			{
				int num;
				if (!int.TryParse(s, out num))
				{
					Debug.FailedAssert("Given condition value parameter is not valid number", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", "Check", 115);
					return false;
				}
				return value == num;
			}
			else
			{
				string s2;
				bool flag = this.Parameters.TryGetValue("min_value", out s2);
				string s3;
				bool flag2 = this.Parameters.TryGetValue("max_value", out s3);
				int minValue = int.MinValue;
				int maxValue = int.MaxValue;
				if (flag && !int.TryParse(s2, out minValue))
				{
					Debug.FailedAssert("Given condition min_value parameter is not valid number", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", "Check", 129);
					return false;
				}
				if (flag2 && !int.TryParse(s3, out maxValue))
				{
					Debug.FailedAssert("Given condition max_value parameter is not valid number", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", "Check", 134);
					return false;
				}
				return (flag || flag2) && value >= minValue && value <= maxValue;
			}
		}
	}
}
