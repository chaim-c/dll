using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B8 RID: 184
	[Serializable]
	public class PSPlayerJoinedToPlayerSessionMessageResult : FunctionResult
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000043EE File Offset: 0x000025EE
		// (set) Token: 0x0600034A RID: 842 RVA: 0x000043F6 File Offset: 0x000025F6
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x0600034B RID: 843 RVA: 0x000043FF File Offset: 0x000025FF
		public PSPlayerJoinedToPlayerSessionMessageResult()
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00004407 File Offset: 0x00002607
		public PSPlayerJoinedToPlayerSessionMessageResult(bool successful)
		{
			this.Successful = successful;
		}
	}
}
