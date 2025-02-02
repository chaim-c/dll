using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public class GetPublishedLobbyNewsMessageResult : FunctionResult
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002892 File Offset: 0x00000A92
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000289A File Offset: 0x00000A9A
		[JsonProperty]
		public PublishedLobbyNewsArticle[] Content { get; private set; }

		// Token: 0x060000D1 RID: 209 RVA: 0x000028A3 File Offset: 0x00000AA3
		public GetPublishedLobbyNewsMessageResult()
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000028AB File Offset: 0x00000AAB
		public GetPublishedLobbyNewsMessageResult(PublishedLobbyNewsArticle[] content)
		{
			this.Content = content;
		}
	}
}
