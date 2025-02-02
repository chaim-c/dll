using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000094 RID: 148
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class FriendRequestResponseMessage : Message
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00003EAC File Offset: 0x000020AC
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00003EB4 File Offset: 0x000020B4
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00003EBD File Offset: 0x000020BD
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00003EC5 File Offset: 0x000020C5
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00003ECE File Offset: 0x000020CE
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00003ED6 File Offset: 0x000020D6
		[JsonProperty]
		public bool IsAccepted { get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00003EDF File Offset: 0x000020DF
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00003EE7 File Offset: 0x000020E7
		[JsonProperty]
		public bool IsBlocked { get; private set; }

		// Token: 0x060002CE RID: 718 RVA: 0x00003EF0 File Offset: 0x000020F0
		public FriendRequestResponseMessage()
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00003EF8 File Offset: 0x000020F8
		public FriendRequestResponseMessage(PlayerId playerId, bool dontUseNameForUnknownPlayer, bool isAccepted, bool isBlocked)
		{
			this.PlayerId = playerId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
			this.IsAccepted = isAccepted;
			this.IsBlocked = isBlocked;
		}
	}
}
