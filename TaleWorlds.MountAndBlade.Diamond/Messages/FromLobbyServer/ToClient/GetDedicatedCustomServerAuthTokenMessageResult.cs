using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class GetDedicatedCustomServerAuthTokenMessageResult : FunctionResult
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002702 File Offset: 0x00000902
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000270A File Offset: 0x0000090A
		[JsonProperty]
		public string AuthToken { get; private set; }

		// Token: 0x060000A9 RID: 169 RVA: 0x00002713 File Offset: 0x00000913
		public GetDedicatedCustomServerAuthTokenMessageResult()
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000271B File Offset: 0x0000091B
		public GetDedicatedCustomServerAuthTokenMessageResult(string authToken)
		{
			this.AuthToken = authToken;
		}
	}
}
