using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004E RID: 78
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class PremadeGameEligibilityStatusMessage : Message
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00002F9B File Offset: 0x0000119B
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00002FA3 File Offset: 0x000011A3
		[JsonProperty]
		public PremadeGameType[] EligibleGameTypes { get; private set; }

		// Token: 0x06000175 RID: 373 RVA: 0x00002FAC File Offset: 0x000011AC
		public PremadeGameEligibilityStatusMessage()
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00002FB4 File Offset: 0x000011B4
		public PremadeGameEligibilityStatusMessage(PremadeGameType[] eligibleGameTypes)
		{
			this.EligibleGameTypes = eligibleGameTypes;
		}
	}
}
