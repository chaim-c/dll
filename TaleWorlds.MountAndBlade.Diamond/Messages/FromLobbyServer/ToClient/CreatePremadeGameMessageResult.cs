using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class CreatePremadeGameMessageResult : FunctionResult
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002464 File Offset: 0x00000664
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000246C File Offset: 0x0000066C
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x06000068 RID: 104 RVA: 0x00002475 File Offset: 0x00000675
		public CreatePremadeGameMessageResult()
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000247D File Offset: 0x0000067D
		public CreatePremadeGameMessageResult(bool successful)
		{
			this.Successful = successful;
		}
	}
}
