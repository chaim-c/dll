using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class RequestJoinPlayerPartyMessageResult : FunctionResult
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000310C File Offset: 0x0000130C
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00003114 File Offset: 0x00001314
		[JsonProperty]
		public bool Success { get; private set; }

		// Token: 0x06000198 RID: 408 RVA: 0x0000311D File Offset: 0x0000131D
		public RequestJoinPlayerPartyMessageResult()
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003125 File Offset: 0x00001325
		public RequestJoinPlayerPartyMessageResult(bool success)
		{
			this.Success = success;
		}
	}
}
