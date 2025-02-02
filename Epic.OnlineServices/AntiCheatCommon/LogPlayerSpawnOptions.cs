using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F2 RID: 1522
	public struct LogPlayerSpawnOptions
	{
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x00039C86 File Offset: 0x00037E86
		// (set) Token: 0x060026C0 RID: 9920 RVA: 0x00039C8E File Offset: 0x00037E8E
		public IntPtr SpawnedPlayerHandle { get; set; }

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x00039C97 File Offset: 0x00037E97
		// (set) Token: 0x060026C2 RID: 9922 RVA: 0x00039C9F File Offset: 0x00037E9F
		public uint TeamId { get; set; }

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x00039CA8 File Offset: 0x00037EA8
		// (set) Token: 0x060026C4 RID: 9924 RVA: 0x00039CB0 File Offset: 0x00037EB0
		public uint CharacterId { get; set; }
	}
}
