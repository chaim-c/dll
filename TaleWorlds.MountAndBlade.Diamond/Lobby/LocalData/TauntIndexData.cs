using System;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x02000180 RID: 384
	public struct TauntIndexData
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00011EDB File Offset: 0x000100DB
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x00011EE3 File Offset: 0x000100E3
		public string TauntId { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00011EEC File Offset: 0x000100EC
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x00011EF4 File Offset: 0x000100F4
		public int TauntIndex { get; set; }

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00011EFD File Offset: 0x000100FD
		public TauntIndexData(string tauntId, int tauntIndex)
		{
			this.TauntId = tauntId;
			this.TauntIndex = tauntIndex;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00011F10 File Offset: 0x00010110
		public override bool Equals(object obj)
		{
			if (obj is TauntIndexData)
			{
				TauntIndexData tauntIndexData = (TauntIndexData)obj;
				return this.TauntId == tauntIndexData.TauntId && this.TauntIndex == tauntIndexData.TauntIndex;
			}
			return false;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00011F58 File Offset: 0x00010158
		public override int GetHashCode()
		{
			return this.TauntId.GetHashCode() * 397 ^ this.TauntIndex.GetHashCode();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00011F85 File Offset: 0x00010185
		public static bool operator ==(TauntIndexData first, TauntIndexData second)
		{
			return first.TauntId == second.TauntId && first.TauntIndex == second.TauntIndex;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00011FAE File Offset: 0x000101AE
		public static bool operator !=(TauntIndexData first, TauntIndexData second)
		{
			return !(first == second);
		}
	}
}
