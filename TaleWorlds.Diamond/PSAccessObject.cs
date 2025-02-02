using System;
using Newtonsoft.Json;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public class PSAccessObject : AccessObject
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000031C3 File Offset: 0x000013C3
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000031CB File Offset: 0x000013CB
		[JsonProperty]
		public int IssuerId { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000031D4 File Offset: 0x000013D4
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000031DC File Offset: 0x000013DC
		[JsonProperty]
		public string AuthCode { get; private set; }

		// Token: 0x060000A3 RID: 163 RVA: 0x000031E5 File Offset: 0x000013E5
		public PSAccessObject()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000031ED File Offset: 0x000013ED
		public PSAccessObject(int issuerId, string authCode)
		{
			base.Type = "PS";
			this.IssuerId = issuerId;
			this.AuthCode = authCode;
		}
	}
}
