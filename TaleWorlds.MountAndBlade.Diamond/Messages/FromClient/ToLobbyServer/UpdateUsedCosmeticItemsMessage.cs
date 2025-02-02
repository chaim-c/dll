using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000CF RID: 207
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class UpdateUsedCosmeticItemsMessage : Message
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00004902 File Offset: 0x00002B02
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000490A File Offset: 0x00002B0A
		[JsonProperty]
		public List<CosmeticItemInfo> UsedCosmetics { get; private set; }

		// Token: 0x060003C4 RID: 964 RVA: 0x00004913 File Offset: 0x00002B13
		public UpdateUsedCosmeticItemsMessage()
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000491B File Offset: 0x00002B1B
		public UpdateUsedCosmeticItemsMessage(List<CosmeticItemInfo> usedCosmetics)
		{
			this.UsedCosmetics = usedCosmetics;
		}
	}
}
