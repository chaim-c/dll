using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002F4 RID: 756
	public struct InstallModOptions
	{
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0001E3D3 File Offset: 0x0001C5D3
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x0001E3DB File Offset: 0x0001C5DB
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x0001E3EC File Offset: 0x0001C5EC
		public ModIdentifier? Mod { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0001E3F5 File Offset: 0x0001C5F5
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x0001E3FD File Offset: 0x0001C5FD
		public bool RemoveAfterExit { get; set; }
	}
}
