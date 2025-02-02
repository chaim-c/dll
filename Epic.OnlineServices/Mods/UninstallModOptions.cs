using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x02000306 RID: 774
	public struct UninstallModOptions
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0001EDCB File Offset: 0x0001CFCB
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x0001EDD3 File Offset: 0x0001CFD3
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x0001EDDC File Offset: 0x0001CFDC
		// (set) Token: 0x060014DC RID: 5340 RVA: 0x0001EDE4 File Offset: 0x0001CFE4
		public ModIdentifier? Mod { get; set; }
	}
}
