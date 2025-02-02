using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200009B RID: 155
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileOptionsInternal : ISettable<QueryFileOptions>, IDisposable
	{
		// Token: 0x170000F0 RID: 240
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00008794 File Offset: 0x00006994
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000087A4 File Offset: 0x000069A4
		public Utf8String Filename
		{
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000087B4 File Offset: 0x000069B4
		public void Set(ref QueryFileOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000087D8 File Offset: 0x000069D8
		public void Set(ref QueryFileOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00008823 File Offset: 0x00006A23
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x040002C0 RID: 704
		private int m_ApiVersion;

		// Token: 0x040002C1 RID: 705
		private IntPtr m_LocalUserId;

		// Token: 0x040002C2 RID: 706
		private IntPtr m_Filename;
	}
}
