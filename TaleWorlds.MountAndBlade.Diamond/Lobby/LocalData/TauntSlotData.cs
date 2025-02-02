using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x0200017F RID: 383
	public class TauntSlotData : MultiplayerLocalData
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00011E5E File Offset: 0x0001005E
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x00011E66 File Offset: 0x00010066
		public string PlayerId { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00011E6F File Offset: 0x0001006F
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x00011E77 File Offset: 0x00010077
		public List<TauntIndexData> TauntIndices { get; set; }

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00011E80 File Offset: 0x00010080
		public TauntSlotData(string playerId)
		{
			this.PlayerId = playerId;
			this.TauntIndices = new List<TauntIndexData>();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00011E9C File Offset: 0x0001009C
		public override bool HasSameContentWith(MultiplayerLocalData other)
		{
			TauntSlotData tauntSlotData;
			return (tauntSlotData = (other as TauntSlotData)) != null && this.PlayerId == tauntSlotData.PlayerId && this.TauntIndices.SequenceEqual(tauntSlotData.TauntIndices);
		}
	}
}
