using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000126 RID: 294
	public class InventoryData
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x00008787 File Offset: 0x00006987
		public InventoryData()
		{
			this.Items = new List<ItemData>();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000879C File Offset: 0x0000699C
		public ItemData GetItemWithIndex(int itemIndex)
		{
			return this.Items.SingleOrDefault(delegate(ItemData q)
			{
				int? index = q.Index;
				int itemIndex2 = itemIndex;
				return index.GetValueOrDefault() == itemIndex2 & index != null;
			});
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x000087CD File Offset: 0x000069CD
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x000087D5 File Offset: 0x000069D5
		public List<ItemData> Items { get; private set; }

		// Token: 0x0600068E RID: 1678 RVA: 0x000087E0 File Offset: 0x000069E0
		public void DebugPrint()
		{
			string text = "";
			foreach (ItemData itemData in this.Items)
			{
				text = string.Concat(new object[]
				{
					text,
					itemData.Index,
					" ",
					itemData.TypeId,
					"\n"
				});
			}
			Debug.Print(text, 0, Debug.DebugColor.White, 17592186044416UL);
		}
	}
}
