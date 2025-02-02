using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F3 RID: 755
	public static class IntermissionVoteItemListExtensions
	{
		// Token: 0x060028F0 RID: 10480 RVA: 0x0009D7F4 File Offset: 0x0009B9F4
		public static bool ContainsItem(this List<IntermissionVoteItem> intermissionVoteItems, string id)
		{
			return intermissionVoteItems != null && intermissionVoteItems.FirstOrDefault((IntermissionVoteItem item) => item.Id == id) != null;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x0009D828 File Offset: 0x0009BA28
		public static IntermissionVoteItem Add(this List<IntermissionVoteItem> intermissionVoteItems, string id)
		{
			IntermissionVoteItem result = null;
			if (intermissionVoteItems != null)
			{
				int count = intermissionVoteItems.Count;
				IntermissionVoteItem intermissionVoteItem = new IntermissionVoteItem(id, count);
				intermissionVoteItems.Add(intermissionVoteItem);
				result = intermissionVoteItem;
			}
			return result;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x0009D854 File Offset: 0x0009BA54
		public static IntermissionVoteItem GetItem(this List<IntermissionVoteItem> intermissionVoteItems, string id)
		{
			IntermissionVoteItem result = null;
			if (intermissionVoteItems != null)
			{
				result = intermissionVoteItems.FirstOrDefault((IntermissionVoteItem item) => item.Id == id);
			}
			return result;
		}
	}
}
