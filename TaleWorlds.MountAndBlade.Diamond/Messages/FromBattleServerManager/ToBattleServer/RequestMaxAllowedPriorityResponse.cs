using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E9 RID: 233
	[MessageDescription("BattleServerManager", "BattleServer")]
	[Serializable]
	public class RequestMaxAllowedPriorityResponse : FunctionResult
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00004F03 File Offset: 0x00003103
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00004F0B File Offset: 0x0000310B
		[JsonProperty]
		public sbyte Priority { get; private set; }

		// Token: 0x06000449 RID: 1097 RVA: 0x00004F14 File Offset: 0x00003114
		public RequestMaxAllowedPriorityResponse()
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00004F1C File Offset: 0x0000311C
		public RequestMaxAllowedPriorityResponse(sbyte priority)
		{
			this.Priority = priority;
		}
	}
}
