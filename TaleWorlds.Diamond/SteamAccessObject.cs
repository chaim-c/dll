using System;
using Newtonsoft.Json;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class SteamAccessObject : AccessObject
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000033D2 File Offset: 0x000015D2
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000033DA File Offset: 0x000015DA
		[JsonProperty]
		public string UserName { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000033E3 File Offset: 0x000015E3
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000033EB File Offset: 0x000015EB
		[JsonProperty]
		public string ExternalAccessToken { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000033F4 File Offset: 0x000015F4
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000033FC File Offset: 0x000015FC
		[JsonProperty]
		public int AppId { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003405 File Offset: 0x00001605
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000340D File Offset: 0x0000160D
		[JsonProperty]
		public string AppTicket { get; private set; }

		// Token: 0x060000C2 RID: 194 RVA: 0x00003416 File Offset: 0x00001616
		public SteamAccessObject()
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000341E File Offset: 0x0000161E
		public SteamAccessObject(string userName, string externalAccessToken, int appId, string appTicket)
		{
			base.Type = "Steam";
			this.UserName = userName;
			this.ExternalAccessToken = externalAccessToken;
			this.AppId = appId;
			this.AppTicket = appTicket;
		}
	}
}
