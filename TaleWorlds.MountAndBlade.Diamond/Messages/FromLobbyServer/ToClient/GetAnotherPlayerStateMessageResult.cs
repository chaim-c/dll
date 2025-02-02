using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class GetAnotherPlayerStateMessageResult : FunctionResult
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000260C File Offset: 0x0000080C
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002614 File Offset: 0x00000814
		[JsonProperty]
		public AnotherPlayerData AnotherPlayerData { get; private set; }

		// Token: 0x06000091 RID: 145 RVA: 0x0000261D File Offset: 0x0000081D
		public GetAnotherPlayerStateMessageResult()
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002625 File Offset: 0x00000825
		public GetAnotherPlayerStateMessageResult(AnotherPlayerState anotherPlayerState, int anotherPlayerExperience)
		{
			this.AnotherPlayerData = new AnotherPlayerData(anotherPlayerState, anotherPlayerExperience);
		}
	}
}
