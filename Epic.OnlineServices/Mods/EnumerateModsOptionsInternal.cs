using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002F1 RID: 753
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EnumerateModsOptionsInternal : ISettable<EnumerateModsOptions>, IDisposable
	{
		// Token: 0x17000597 RID: 1431
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x0001E0E9 File Offset: 0x0001C2E9
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000598 RID: 1432
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x0001E0F9 File Offset: 0x0001C2F9
		public ModEnumerationType Type
		{
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0001E103 File Offset: 0x0001C303
		public void Set(ref EnumerateModsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Type = other.Type;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0001E128 File Offset: 0x0001C328
		public void Set(ref EnumerateModsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Type = other.Value.Type;
			}
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0001E173 File Offset: 0x0001C373
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000919 RID: 2329
		private int m_ApiVersion;

		// Token: 0x0400091A RID: 2330
		private IntPtr m_LocalUserId;

		// Token: 0x0400091B RID: 2331
		private ModEnumerationType m_Type;
	}
}
