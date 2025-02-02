using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x02000061 RID: 97
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class CustomBattleServerReadyMessage : LoginMessage
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000033EC File Offset: 0x000015EC
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x000033F4 File Offset: 0x000015F4
		[JsonProperty]
		public ApplicationVersion ApplicationVersion { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000033FD File Offset: 0x000015FD
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00003405 File Offset: 0x00001605
		[JsonProperty]
		public string AuthToken { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000340E File Offset: 0x0000160E
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00003416 File Offset: 0x00001616
		[JsonProperty]
		public ModuleInfoModel[] LoadedModules { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000341F File Offset: 0x0000161F
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00003427 File Offset: 0x00001627
		[JsonProperty]
		public bool AllowsOptionalModules { get; private set; }

		// Token: 0x060001DC RID: 476 RVA: 0x00003430 File Offset: 0x00001630
		public CustomBattleServerReadyMessage()
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00003438 File Offset: 0x00001638
		public CustomBattleServerReadyMessage(PeerId peerId, ApplicationVersion applicationVersion, string authToken, ModuleInfoModel[] loadedModules, bool allowsOptionalModules) : base(peerId)
		{
			this.ApplicationVersion = applicationVersion;
			this.AuthToken = authToken;
			this.LoadedModules = loadedModules;
			this.AllowsOptionalModules = allowsOptionalModules;
		}
	}
}
