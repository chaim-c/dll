using System;
using Newtonsoft.Json;
using TaleWorlds.Core;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000CB RID: 203
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class UpdateCharacterMessage : Message
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000481A File Offset: 0x00002A1A
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00004822 File Offset: 0x00002A22
		[JsonProperty]
		public BodyProperties BodyProperties { get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000482B File Offset: 0x00002A2B
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00004833 File Offset: 0x00002A33
		[JsonProperty]
		public bool IsFemale { get; private set; }

		// Token: 0x060003B0 RID: 944 RVA: 0x0000483C File Offset: 0x00002A3C
		public UpdateCharacterMessage()
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00004844 File Offset: 0x00002A44
		public UpdateCharacterMessage(BodyProperties bodyProperties, bool isFemale)
		{
			this.BodyProperties = bodyProperties;
			this.IsFemale = isFemale;
		}
	}
}
