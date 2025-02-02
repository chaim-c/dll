using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000AD RID: 173
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class InitializeSession : LoginMessage
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00004185 File Offset: 0x00002385
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000418D File Offset: 0x0000238D
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00004196 File Offset: 0x00002396
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000419E File Offset: 0x0000239E
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000041A7 File Offset: 0x000023A7
		// (set) Token: 0x06000314 RID: 788 RVA: 0x000041AF File Offset: 0x000023AF
		[JsonProperty]
		public AccessObject AccessObject { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000041B8 File Offset: 0x000023B8
		// (set) Token: 0x06000316 RID: 790 RVA: 0x000041C0 File Offset: 0x000023C0
		[JsonProperty]
		public ApplicationVersion ApplicationVersion { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000317 RID: 791 RVA: 0x000041C9 File Offset: 0x000023C9
		// (set) Token: 0x06000318 RID: 792 RVA: 0x000041D1 File Offset: 0x000023D1
		[JsonProperty]
		public string ConnectionPassword { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000319 RID: 793 RVA: 0x000041DA File Offset: 0x000023DA
		// (set) Token: 0x0600031A RID: 794 RVA: 0x000041E2 File Offset: 0x000023E2
		[JsonProperty]
		public ModuleInfoModel[] LoadedModules { get; private set; }

		// Token: 0x0600031B RID: 795 RVA: 0x000041EB File Offset: 0x000023EB
		public InitializeSession()
		{
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000041F3 File Offset: 0x000023F3
		public InitializeSession(PlayerId playerId, string playerName, AccessObject accessObject, ApplicationVersion applicationVersion, string connectionPassword, ModuleInfoModel[] loadedModules) : base(playerId.ConvertToPeerId())
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.AccessObject = accessObject;
			this.ApplicationVersion = applicationVersion;
			this.ConnectionPassword = connectionPassword;
			this.LoadedModules = loadedModules;
		}
	}
}
