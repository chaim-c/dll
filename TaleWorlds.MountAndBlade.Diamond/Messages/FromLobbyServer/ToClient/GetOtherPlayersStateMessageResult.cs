using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class GetOtherPlayersStateMessageResult : FunctionResult
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002752 File Offset: 0x00000952
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000275A File Offset: 0x0000095A
		[JsonProperty]
		public List<ValueTuple<PlayerId, AnotherPlayerData>> States { get; private set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00002763 File Offset: 0x00000963
		public GetOtherPlayersStateMessageResult()
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000276B File Offset: 0x0000096B
		public GetOtherPlayersStateMessageResult(List<ValueTuple<PlayerId, AnotherPlayerData>> states)
		{
			this.States = states;
		}
	}
}
