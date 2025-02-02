using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000244 RID: 580
	public static class ItemCollectionElementMissionExtensions
	{
		// Token: 0x06001F5F RID: 8031 RVA: 0x0006F170 File Offset: 0x0006D370
		public static StackArray.StackArray4Int GetItemHolsterIndices(this ItemObject item)
		{
			StackArray.StackArray4Int result = default(StackArray.StackArray4Int);
			for (int i = 0; i < item.ItemHolsters.Length; i++)
			{
				result[i] = ((item.ItemHolsters[i].Length > 0) ? MBItem.GetItemHolsterIndex(item.ItemHolsters[i]) : -1);
			}
			for (int j = item.ItemHolsters.Length; j < 4; j++)
			{
				result[j] = -1;
			}
			return result;
		}
	}
}
