using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C2 RID: 194
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class RequestJoinCustomGameMessage : Message
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00004652 File Offset: 0x00002852
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000465A File Offset: 0x0000285A
		[JsonProperty]
		public CustomBattleId CustomBattleId { get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00004663 File Offset: 0x00002863
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000466B File Offset: 0x0000286B
		[JsonProperty]
		public string Password { get; private set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00004674 File Offset: 0x00002874
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000467C File Offset: 0x0000287C
		[JsonProperty]
		public bool IsJoinAsAdminOnly { get; private set; }

		// Token: 0x06000386 RID: 902 RVA: 0x00004685 File Offset: 0x00002885
		public RequestJoinCustomGameMessage()
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000468D File Offset: 0x0000288D
		public RequestJoinCustomGameMessage(CustomBattleId customBattleId, string password = "", bool isJoinAsAdminOnly = false)
		{
			this.CustomBattleId = customBattleId;
			this.Password = password;
			this.IsJoinAsAdminOnly = isJoinAsAdminOnly;
		}
	}
}
