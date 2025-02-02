using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	public class BadgeDataEntry
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000052D4 File Offset: 0x000034D4
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x000052DC File Offset: 0x000034DC
		[JsonProperty]
		public PlayerId PlayerId { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000052E5 File Offset: 0x000034E5
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x000052ED File Offset: 0x000034ED
		[JsonProperty]
		public string BadgeId { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000052F6 File Offset: 0x000034F6
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x000052FE File Offset: 0x000034FE
		[JsonProperty]
		public string ConditionId { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00005307 File Offset: 0x00003507
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0000530F File Offset: 0x0000350F
		[JsonProperty]
		public int Count { get; set; }

		// Token: 0x0600049A RID: 1178 RVA: 0x00005320 File Offset: 0x00003520
		public static Dictionary<ValueTuple<PlayerId, string, string>, int> ToDictionary(List<BadgeDataEntry> entries)
		{
			Dictionary<ValueTuple<PlayerId, string, string>, int> dictionary = new Dictionary<ValueTuple<PlayerId, string, string>, int>();
			if (entries != null)
			{
				foreach (BadgeDataEntry badgeDataEntry in entries)
				{
					dictionary.Add(new ValueTuple<PlayerId, string, string>(badgeDataEntry.PlayerId, badgeDataEntry.BadgeId, badgeDataEntry.ConditionId), badgeDataEntry.Count);
				}
			}
			return dictionary;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00005394 File Offset: 0x00003594
		public static List<BadgeDataEntry> ToList(Dictionary<ValueTuple<PlayerId, string, string>, int> dictionary)
		{
			List<BadgeDataEntry> list = new List<BadgeDataEntry>();
			if (dictionary != null)
			{
				foreach (KeyValuePair<ValueTuple<PlayerId, string, string>, int> keyValuePair in dictionary)
				{
					list.Add(new BadgeDataEntry
					{
						PlayerId = keyValuePair.Key.Item1,
						BadgeId = keyValuePair.Key.Item2,
						ConditionId = keyValuePair.Key.Item3,
						Count = keyValuePair.Value
					});
				}
			}
			return list;
		}
	}
}
