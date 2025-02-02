using System;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x0200017D RID: 381
	public class PlayerInfo
	{
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x000119E1 File Offset: 0x0000FBE1
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x000119E9 File Offset: 0x0000FBE9
		public string PlayerId { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000119F2 File Offset: 0x0000FBF2
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x000119FA File Offset: 0x0000FBFA
		public string Username { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00011A03 File Offset: 0x0000FC03
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x00011A0B File Offset: 0x0000FC0B
		public int ForcedIndex { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00011A14 File Offset: 0x0000FC14
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x00011A1C File Offset: 0x0000FC1C
		public int TeamNo { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00011A25 File Offset: 0x0000FC25
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x00011A2D File Offset: 0x0000FC2D
		public int Kill { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00011A36 File Offset: 0x0000FC36
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x00011A3E File Offset: 0x0000FC3E
		public int Death { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00011A47 File Offset: 0x0000FC47
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x00011A4F File Offset: 0x0000FC4F
		public int Assist { get; set; }

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00011A58 File Offset: 0x0000FC58
		public bool HasSameContentWith(PlayerInfo other)
		{
			return this.PlayerId == other.PlayerId && this.Username == other.Username && this.ForcedIndex == other.ForcedIndex && this.TeamNo == other.TeamNo && this.Kill == other.Kill && this.Death == other.Death && this.Assist == other.Assist;
		}
	}
}
