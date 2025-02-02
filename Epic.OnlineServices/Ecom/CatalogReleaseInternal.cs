using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000489 RID: 1161
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CatalogReleaseInternal : IGettable<CatalogRelease>, ISettable<CatalogRelease>, IDisposable
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x0002C83C File Offset: 0x0002AA3C
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x0002C864 File Offset: 0x0002AA64
		public Utf8String[] CompatibleAppIds
		{
			get
			{
				Utf8String[] result;
				Helper.Get<Utf8String>(this.m_CompatibleAppIds, out result, this.m_CompatibleAppIdCount, true);
				return result;
			}
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_CompatibleAppIds, true, out this.m_CompatibleAppIdCount);
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x0002C87C File Offset: 0x0002AA7C
		// (set) Token: 0x06001E17 RID: 7703 RVA: 0x0002C8A4 File Offset: 0x0002AAA4
		public Utf8String[] CompatiblePlatforms
		{
			get
			{
				Utf8String[] result;
				Helper.Get<Utf8String>(this.m_CompatiblePlatforms, out result, this.m_CompatiblePlatformCount, true);
				return result;
			}
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_CompatiblePlatforms, true, out this.m_CompatiblePlatformCount);
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0002C8BC File Offset: 0x0002AABC
		// (set) Token: 0x06001E19 RID: 7705 RVA: 0x0002C8DD File Offset: 0x0002AADD
		public Utf8String ReleaseNote
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ReleaseNote, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ReleaseNote);
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0002C8ED File Offset: 0x0002AAED
		public void Set(ref CatalogRelease other)
		{
			this.m_ApiVersion = 1;
			this.CompatibleAppIds = other.CompatibleAppIds;
			this.CompatiblePlatforms = other.CompatiblePlatforms;
			this.ReleaseNote = other.ReleaseNote;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0002C920 File Offset: 0x0002AB20
		public void Set(ref CatalogRelease? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.CompatibleAppIds = other.Value.CompatibleAppIds;
				this.CompatiblePlatforms = other.Value.CompatiblePlatforms;
				this.ReleaseNote = other.Value.ReleaseNote;
			}
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0002C980 File Offset: 0x0002AB80
		public void Dispose()
		{
			Helper.Dispose(ref this.m_CompatibleAppIds);
			Helper.Dispose(ref this.m_CompatiblePlatforms);
			Helper.Dispose(ref this.m_ReleaseNote);
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x0002C9A7 File Offset: 0x0002ABA7
		public void Get(out CatalogRelease output)
		{
			output = default(CatalogRelease);
			output.Set(ref this);
		}

		// Token: 0x04000D47 RID: 3399
		private int m_ApiVersion;

		// Token: 0x04000D48 RID: 3400
		private uint m_CompatibleAppIdCount;

		// Token: 0x04000D49 RID: 3401
		private IntPtr m_CompatibleAppIds;

		// Token: 0x04000D4A RID: 3402
		private uint m_CompatiblePlatformCount;

		// Token: 0x04000D4B RID: 3403
		private IntPtr m_CompatiblePlatforms;

		// Token: 0x04000D4C RID: 3404
		private IntPtr m_ReleaseNote;
	}
}
