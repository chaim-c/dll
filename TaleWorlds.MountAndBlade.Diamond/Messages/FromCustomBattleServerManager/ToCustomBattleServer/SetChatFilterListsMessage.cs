using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x0200006C RID: 108
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer")]
	[DataContract]
	[Serializable]
	public class SetChatFilterListsMessage : Message
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00003830 File Offset: 0x00001A30
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00003838 File Offset: 0x00001A38
		[JsonProperty]
		public string[] ProfanityList { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00003841 File Offset: 0x00001A41
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00003849 File Offset: 0x00001A49
		[JsonProperty]
		public string[] AllowList { get; private set; }

		// Token: 0x06000231 RID: 561 RVA: 0x00003852 File Offset: 0x00001A52
		public SetChatFilterListsMessage()
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000385A File Offset: 0x00001A5A
		public SetChatFilterListsMessage(string[] profanityList, string[] allowList)
		{
			this.ProfanityList = profanityList;
			this.AllowList = allowList;
		}
	}
}
