using System;
using TaleWorlds.Localization;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000013 RID: 19
	public class AccessObjectResult
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002915 File Offset: 0x00000B15
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000291D File Offset: 0x00000B1D
		public AccessObject AccessObject { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002926 File Offset: 0x00000B26
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000292E File Offset: 0x00000B2E
		public bool Success { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002937 File Offset: 0x00000B37
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000293F File Offset: 0x00000B3F
		public TextObject FailReason { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002950 File Offset: 0x00000B50
		public static AccessObjectResult CreateSuccess(AccessObject accessObject)
		{
			return new AccessObjectResult
			{
				Success = true,
				AccessObject = accessObject
			};
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002965 File Offset: 0x00000B65
		public static AccessObjectResult CreateFailed(TextObject failReason)
		{
			return new AccessObjectResult
			{
				Success = false,
				FailReason = failReason
			};
		}
	}
}
