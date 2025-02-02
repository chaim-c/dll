using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200043F RID: 1087
	public struct PermissionStatus
	{
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0002925F File Offset: 0x0002745F
		// (set) Token: 0x06001BDC RID: 7132 RVA: 0x00029267 File Offset: 0x00027467
		public Utf8String Name { get; set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00029270 File Offset: 0x00027470
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x00029278 File Offset: 0x00027478
		public KWSPermissionStatus Status { get; set; }

		// Token: 0x06001BDF RID: 7135 RVA: 0x00029281 File Offset: 0x00027481
		internal void Set(ref PermissionStatusInternal other)
		{
			this.Name = other.Name;
			this.Status = other.Status;
		}
	}
}
