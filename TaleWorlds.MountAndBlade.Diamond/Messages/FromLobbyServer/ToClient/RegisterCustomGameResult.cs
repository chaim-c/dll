using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public class RegisterCustomGameResult : FunctionResult
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000302B File Offset: 0x0000122B
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00003033 File Offset: 0x00001233
		[JsonProperty]
		public bool Success { get; private set; }

		// Token: 0x06000183 RID: 387 RVA: 0x0000303C File Offset: 0x0000123C
		public RegisterCustomGameResult()
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003044 File Offset: 0x00001244
		public RegisterCustomGameResult(bool success)
		{
			this.Success = success;
		}
	}
}
