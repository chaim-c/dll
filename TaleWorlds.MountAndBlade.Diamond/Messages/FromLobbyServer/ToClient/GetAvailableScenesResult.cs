using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public class GetAvailableScenesResult : FunctionResult
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000263A File Offset: 0x0000083A
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002642 File Offset: 0x00000842
		[JsonProperty]
		public AvailableScenes AvailableScenes { get; private set; }

		// Token: 0x06000095 RID: 149 RVA: 0x0000264B File Offset: 0x0000084B
		public GetAvailableScenesResult()
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002653 File Offset: 0x00000853
		public GetAvailableScenesResult(AvailableScenes scenes)
		{
			this.AvailableScenes = scenes;
		}
	}
}
