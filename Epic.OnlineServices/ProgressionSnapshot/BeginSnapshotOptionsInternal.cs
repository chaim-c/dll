using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x0200021C RID: 540
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BeginSnapshotOptionsInternal : ISettable<BeginSnapshotOptions>, IDisposable
	{
		// Token: 0x170003F1 RID: 1009
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x000166D4 File Offset: 0x000148D4
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000166E4 File Offset: 0x000148E4
		public void Set(ref BeginSnapshotOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000166FC File Offset: 0x000148FC
		public void Set(ref BeginSnapshotOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00016732 File Offset: 0x00014932
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040006CD RID: 1741
		private int m_ApiVersion;

		// Token: 0x040006CE RID: 1742
		private IntPtr m_LocalUserId;
	}
}
