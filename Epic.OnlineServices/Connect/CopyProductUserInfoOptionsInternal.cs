using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200051D RID: 1309
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserInfoOptionsInternal : ISettable<CopyProductUserInfoOptions>, IDisposable
	{
		// Token: 0x170009CC RID: 2508
		// (set) Token: 0x060021A0 RID: 8608 RVA: 0x000324A7 File Offset: 0x000306A7
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000324B7 File Offset: 0x000306B7
		public void Set(ref CopyProductUserInfoOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000324D0 File Offset: 0x000306D0
		public void Set(ref CopyProductUserInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00032506 File Offset: 0x00030706
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000EFC RID: 3836
		private int m_ApiVersion;

		// Token: 0x04000EFD RID: 3837
		private IntPtr m_TargetUserId;
	}
}
