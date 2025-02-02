using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000531 RID: 1329
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProductUserExternalAccountCountOptionsInternal : ISettable<GetProductUserExternalAccountCountOptions>, IDisposable
	{
		// Token: 0x170009F7 RID: 2551
		// (set) Token: 0x06002218 RID: 8728 RVA: 0x00032FB4 File Offset: 0x000311B4
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00032FC4 File Offset: 0x000311C4
		public void Set(ref GetProductUserExternalAccountCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00032FDC File Offset: 0x000311DC
		public void Set(ref GetProductUserExternalAccountCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00033012 File Offset: 0x00031212
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000F2B RID: 3883
		private int m_ApiVersion;

		// Token: 0x04000F2C RID: 3884
		private IntPtr m_TargetUserId;
	}
}
