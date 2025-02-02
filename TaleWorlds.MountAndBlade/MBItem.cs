using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C8 RID: 456
	public static class MBItem
	{
		// Token: 0x06001A24 RID: 6692 RVA: 0x0005C4CA File Offset: 0x0005A6CA
		public static int GetItemUsageIndex(string itemUsageName)
		{
			return MBAPI.IMBItem.GetItemUsageIndex(itemUsageName);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0005C4D7 File Offset: 0x0005A6D7
		public static int GetItemHolsterIndex(string itemHolsterName)
		{
			return MBAPI.IMBItem.GetItemHolsterIndex(itemHolsterName);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0005C4E4 File Offset: 0x0005A6E4
		public static bool GetItemIsPassiveUsage(string itemUsageName)
		{
			return MBAPI.IMBItem.GetItemIsPassiveUsage(itemUsageName);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0005C4F4 File Offset: 0x0005A6F4
		public static MatrixFrame GetHolsterFrameByIndex(int index)
		{
			MatrixFrame result = default(MatrixFrame);
			MBAPI.IMBItem.GetHolsterFrameByIndex(index, ref result);
			return result;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0005C517 File Offset: 0x0005A717
		public static ItemObject.ItemUsageSetFlags GetItemUsageSetFlags(string ItemUsageName)
		{
			return (ItemObject.ItemUsageSetFlags)MBAPI.IMBItem.GetItemUsageSetFlags(ItemUsageName);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0005C524 File Offset: 0x0005A724
		public static ActionIndexValueCache GetItemUsageReloadActionCode(string itemUsageName, int usageDirection, bool isMounted, int leftHandUsageSetIndex, bool isLeftStance)
		{
			return new ActionIndexValueCache(MBAPI.IMBItem.GetItemUsageReloadActionCode(itemUsageName, usageDirection, isMounted, leftHandUsageSetIndex, isLeftStance));
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0005C53B File Offset: 0x0005A73B
		public static int GetItemUsageStrikeType(string itemUsageName, int usageDirection, bool isMounted, int leftHandUsageSetIndex, bool isLeftStance)
		{
			return MBAPI.IMBItem.GetItemUsageStrikeType(itemUsageName, usageDirection, isMounted, leftHandUsageSetIndex, isLeftStance);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0005C54D File Offset: 0x0005A74D
		public static float GetMissileRange(float shotSpeed, float zDiff)
		{
			return MBAPI.IMBItem.GetMissileRange(shotSpeed, zDiff);
		}
	}
}
