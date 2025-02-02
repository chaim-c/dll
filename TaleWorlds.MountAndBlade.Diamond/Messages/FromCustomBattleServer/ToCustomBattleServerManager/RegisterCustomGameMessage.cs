using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x02000064 RID: 100
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class RegisterCustomGameMessage : Message
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00003547 File Offset: 0x00001747
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000354F File Offset: 0x0000174F
		[JsonProperty]
		public int GameDefinitionId { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00003558 File Offset: 0x00001758
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00003560 File Offset: 0x00001760
		[JsonProperty]
		public string GameModule { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00003569 File Offset: 0x00001769
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00003571 File Offset: 0x00001771
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000357A File Offset: 0x0000177A
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00003582 File Offset: 0x00001782
		[JsonProperty]
		public string ServerName { get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000358B File Offset: 0x0000178B
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00003593 File Offset: 0x00001793
		[JsonProperty]
		public string ServerAddress { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000359C File Offset: 0x0000179C
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x000035A4 File Offset: 0x000017A4
		[JsonProperty]
		public int MaxPlayerCount { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000035AD File Offset: 0x000017AD
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x000035B5 File Offset: 0x000017B5
		[JsonProperty]
		public string Map { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000035BE File Offset: 0x000017BE
		// (set) Token: 0x060001FB RID: 507 RVA: 0x000035C6 File Offset: 0x000017C6
		[JsonProperty]
		public string UniqueMapId { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000035CF File Offset: 0x000017CF
		// (set) Token: 0x060001FD RID: 509 RVA: 0x000035D7 File Offset: 0x000017D7
		[JsonProperty]
		public string GamePassword { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000035E0 File Offset: 0x000017E0
		// (set) Token: 0x060001FF RID: 511 RVA: 0x000035E8 File Offset: 0x000017E8
		[JsonProperty]
		public string AdminPassword { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000035F1 File Offset: 0x000017F1
		// (set) Token: 0x06000201 RID: 513 RVA: 0x000035F9 File Offset: 0x000017F9
		[JsonProperty]
		public int Port { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00003602 File Offset: 0x00001802
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000360A File Offset: 0x0000180A
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00003613 File Offset: 0x00001813
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000361B File Offset: 0x0000181B
		[JsonProperty]
		public int Permission { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00003624 File Offset: 0x00001824
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000362C File Offset: 0x0000182C
		[JsonProperty]
		public bool IsOverridingIP { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00003635 File Offset: 0x00001835
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000363D File Offset: 0x0000183D
		[JsonProperty]
		public bool CrossplayEnabled { get; private set; }

		// Token: 0x0600020A RID: 522 RVA: 0x00003646 File Offset: 0x00001846
		public RegisterCustomGameMessage()
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00003650 File Offset: 0x00001850
		public RegisterCustomGameMessage(int gameDefinitionId, string gameModule, string gameType, string serverName, string serverAddress, int maxPlayerCount, string map, string uniqueMapId, string gamePassword, string adminPassword, int port, string region, int permission, bool crossplayEnabled, bool isOverridingIP)
		{
			this.GameDefinitionId = gameDefinitionId;
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.ServerName = serverName;
			this.ServerAddress = serverAddress;
			this.MaxPlayerCount = maxPlayerCount;
			this.Map = map;
			this.UniqueMapId = uniqueMapId;
			this.GamePassword = gamePassword;
			this.AdminPassword = adminPassword;
			this.Port = port;
			this.Region = region;
			this.Permission = permission;
			this.CrossplayEnabled = crossplayEnabled;
			this.IsOverridingIP = isOverridingIP;
		}
	}
}
