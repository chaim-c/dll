using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200000C RID: 12
	public class CheckClanTagValidResult : FunctionResult
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000226B File Offset: 0x0000046B
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002273 File Offset: 0x00000473
		[JsonProperty]
		public bool TagExists { get; private set; }

		// Token: 0x06000037 RID: 55 RVA: 0x0000227C File Offset: 0x0000047C
		public CheckClanTagValidResult()
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002284 File Offset: 0x00000484
		public CheckClanTagValidResult(bool tagExists)
		{
			this.TagExists = tagExists;
		}
	}
}
