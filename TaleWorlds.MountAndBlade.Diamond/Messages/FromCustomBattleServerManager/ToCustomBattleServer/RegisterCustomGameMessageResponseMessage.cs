using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x0200006B RID: 107
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer")]
	[Serializable]
	public class RegisterCustomGameMessageResponseMessage : FunctionResult
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00003808 File Offset: 0x00001A08
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00003810 File Offset: 0x00001A10
		[JsonProperty]
		public bool ShouldReportActivities { get; private set; }

		// Token: 0x0600022B RID: 555 RVA: 0x00003819 File Offset: 0x00001A19
		public RegisterCustomGameMessageResponseMessage()
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00003821 File Offset: 0x00001A21
		public RegisterCustomGameMessageResponseMessage(bool shouldReportActivities)
		{
			this.ShouldReportActivities = shouldReportActivities;
		}
	}
}
