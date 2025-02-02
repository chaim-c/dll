using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.Library;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D8 RID: 216
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class BattleServerReadyMessage : LoginMessage
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00004B2D File Offset: 0x00002D2D
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00004B35 File Offset: 0x00002D35
		[JsonProperty]
		public ApplicationVersion ApplicationVersion { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00004B3E File Offset: 0x00002D3E
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00004B46 File Offset: 0x00002D46
		[JsonProperty]
		public string AssignedAddress { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00004B4F File Offset: 0x00002D4F
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00004B57 File Offset: 0x00002D57
		[JsonProperty]
		public ushort AssignedPort { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00004B60 File Offset: 0x00002D60
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00004B68 File Offset: 0x00002D68
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00004B71 File Offset: 0x00002D71
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00004B79 File Offset: 0x00002D79
		[JsonProperty]
		public sbyte Priority { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00004B82 File Offset: 0x00002D82
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00004B8A File Offset: 0x00002D8A
		[JsonProperty]
		public string Password { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00004B93 File Offset: 0x00002D93
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00004B9B File Offset: 0x00002D9B
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x060003FB RID: 1019 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public BattleServerReadyMessage()
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00004BAC File Offset: 0x00002DAC
		public BattleServerReadyMessage(PeerId peerId, ApplicationVersion applicationVersion, string assignedAddress, ushort assignedPort, string region, sbyte priority, string password, string gameType) : base(peerId)
		{
			this.ApplicationVersion = applicationVersion;
			this.AssignedAddress = assignedAddress;
			this.AssignedPort = assignedPort;
			this.Region = region;
			this.Priority = priority;
			this.Password = password;
			this.GameType = gameType;
		}
	}
}
