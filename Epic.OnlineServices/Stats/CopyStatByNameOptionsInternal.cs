using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000A8 RID: 168
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyStatByNameOptionsInternal : ISettable<CopyStatByNameOptions>, IDisposable
	{
		// Token: 0x17000118 RID: 280
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00009598 File Offset: 0x00007798
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000119 RID: 281
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x000095A8 File Offset: 0x000077A8
		public Utf8String Name
		{
			set
			{
				Helper.Set(value, ref this.m_Name);
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000095B8 File Offset: 0x000077B8
		public void Set(ref CopyStatByNameOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
			this.Name = other.Name;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000095DC File Offset: 0x000077DC
		public void Set(ref CopyStatByNameOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
				this.Name = other.Value.Name;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00009627 File Offset: 0x00007827
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_Name);
		}

		// Token: 0x04000300 RID: 768
		private int m_ApiVersion;

		// Token: 0x04000301 RID: 769
		private IntPtr m_TargetUserId;

		// Token: 0x04000302 RID: 770
		private IntPtr m_Name;
	}
}
