using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class GetOfficialServerProviderNameResult : FunctionResult
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000272A File Offset: 0x0000092A
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00002732 File Offset: 0x00000932
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x060000AD RID: 173 RVA: 0x0000273B File Offset: 0x0000093B
		public GetOfficialServerProviderNameResult()
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002743 File Offset: 0x00000943
		public GetOfficialServerProviderNameResult(string name)
		{
			this.Name = name;
		}
	}
}
