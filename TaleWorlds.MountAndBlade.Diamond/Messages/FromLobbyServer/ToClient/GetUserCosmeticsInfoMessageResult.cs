using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class GetUserCosmeticsInfoMessageResult : FunctionResult
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000290A File Offset: 0x00000B0A
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002912 File Offset: 0x00000B12
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000291B File Offset: 0x00000B1B
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00002923 File Offset: 0x00000B23
		[JsonProperty]
		public List<string> OwnedCosmetics { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000292C File Offset: 0x00000B2C
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00002934 File Offset: 0x00000B34
		[JsonProperty]
		public Dictionary<string, List<string>> UsedCosmetics { get; private set; }

		// Token: 0x060000E1 RID: 225 RVA: 0x0000293D File Offset: 0x00000B3D
		public GetUserCosmeticsInfoMessageResult()
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002945 File Offset: 0x00000B45
		public GetUserCosmeticsInfoMessageResult(bool successful, List<string> ownedCosmetics, Dictionary<string, List<string>> usedCosmetics)
		{
			this.Successful = successful;
			this.OwnedCosmetics = ownedCosmetics;
			this.UsedCosmetics = usedCosmetics;
		}
	}
}
