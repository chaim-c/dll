using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public class CosmeticItemInfo
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x000078CA File Offset: 0x00005ACA
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x000078D2 File Offset: 0x00005AD2
		public string TroopId { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x000078DB File Offset: 0x00005ADB
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x000078E3 File Offset: 0x00005AE3
		public string CosmeticIndex { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x000078EC File Offset: 0x00005AEC
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x000078F4 File Offset: 0x00005AF4
		public bool IsEquipped { get; set; }

		// Token: 0x060005EF RID: 1519 RVA: 0x000078FD File Offset: 0x00005AFD
		public CosmeticItemInfo()
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00007905 File Offset: 0x00005B05
		public CosmeticItemInfo(string troopId, string cosmeticIndex, bool isEquipped)
		{
			this.TroopId = troopId;
			this.CosmeticIndex = cosmeticIndex;
			this.IsEquipped = isEquipped;
		}
	}
}
