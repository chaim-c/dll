using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005B3 RID: 1459
	public struct IOSCredentialsSystemAuthCredentialsOptions
	{
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x00037E4F File Offset: 0x0003604F
		// (set) Token: 0x060025A5 RID: 9637 RVA: 0x00037E57 File Offset: 0x00036057
		public IntPtr PresentationContextProviding { get; set; }

		// Token: 0x060025A6 RID: 9638 RVA: 0x00037E60 File Offset: 0x00036060
		internal void Set(ref IOSCredentialsSystemAuthCredentialsOptionsInternal other)
		{
			this.PresentationContextProviding = other.PresentationContextProviding;
		}
	}
}
