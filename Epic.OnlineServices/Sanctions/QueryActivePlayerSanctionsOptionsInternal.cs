using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000171 RID: 369
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryActivePlayerSanctionsOptionsInternal : ISettable<QueryActivePlayerSanctionsOptions>, IDisposable
	{
		// Token: 0x17000262 RID: 610
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x0000FB19 File Offset: 0x0000DD19
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000263 RID: 611
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0000FB29 File Offset: 0x0000DD29
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000FB39 File Offset: 0x0000DD39
		public void Set(ref QueryActivePlayerSanctionsOptions other)
		{
			this.m_ApiVersion = 2;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0000FB60 File Offset: 0x0000DD60
		public void Set(ref QueryActivePlayerSanctionsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.Value.TargetUserId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0000FBAB File Offset: 0x0000DDAB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040004DA RID: 1242
		private int m_ApiVersion;

		// Token: 0x040004DB RID: 1243
		private IntPtr m_TargetUserId;

		// Token: 0x040004DC RID: 1244
		private IntPtr m_LocalUserId;
	}
}
