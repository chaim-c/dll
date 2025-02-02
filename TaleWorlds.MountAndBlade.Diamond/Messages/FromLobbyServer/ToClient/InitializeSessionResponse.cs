using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000035 RID: 53
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class InitializeSessionResponse : LoginResultObject
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002962 File Offset: 0x00000B62
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x0000296A File Offset: 0x00000B6A
		[JsonProperty]
		public PlayerData PlayerData { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002973 File Offset: 0x00000B73
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000297B File Offset: 0x00000B7B
		[JsonProperty]
		public ServerStatus ServerStatus { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002984 File Offset: 0x00000B84
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000298C File Offset: 0x00000B8C
		[JsonProperty]
		public AvailableScenes AvailableScenes { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002995 File Offset: 0x00000B95
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000299D File Offset: 0x00000B9D
		[JsonProperty]
		public SupportedFeatures SupportedFeatures { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000029A6 File Offset: 0x00000BA6
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000029AE File Offset: 0x00000BAE
		[JsonProperty]
		public bool HasPendingRejoin { get; private set; }

		// Token: 0x060000ED RID: 237 RVA: 0x000029B7 File Offset: 0x00000BB7
		public InitializeSessionResponse()
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000029BF File Offset: 0x00000BBF
		public InitializeSessionResponse(PlayerData playerData, ServerStatus serverStatus, AvailableScenes availableScenes, SupportedFeatures supportedFeatures, bool hasPendingRejoin)
		{
			this.PlayerData = playerData;
			this.ServerStatus = serverStatus;
			this.AvailableScenes = availableScenes;
			this.SupportedFeatures = supportedFeatures;
			this.HasPendingRejoin = hasPendingRejoin;
		}
	}
}
