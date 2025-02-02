using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x02000066 RID: 102
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class UpdateGamePropertiesMessage : Message
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00003730 File Offset: 0x00001930
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00003738 File Offset: 0x00001938
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00003741 File Offset: 0x00001941
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00003749 File Offset: 0x00001949
		[JsonProperty]
		public string Scene { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00003752 File Offset: 0x00001952
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000375A File Offset: 0x0000195A
		[JsonProperty]
		public string UniqueSceneId { get; private set; }

		// Token: 0x0600021A RID: 538 RVA: 0x00003763 File Offset: 0x00001963
		public UpdateGamePropertiesMessage()
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000376B File Offset: 0x0000196B
		public UpdateGamePropertiesMessage(string gameType, string scene, string uniqueSceneId)
		{
			this.GameType = gameType;
			this.Scene = scene;
			this.UniqueSceneId = uniqueSceneId;
		}
	}
}
