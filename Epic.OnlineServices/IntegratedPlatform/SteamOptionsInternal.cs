using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.IntegratedPlatform
{
	// Token: 0x0200045D RID: 1117
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamOptionsInternal : IGettable<SteamOptions>, ISettable<SteamOptions>, IDisposable
	{
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x0002A3D4 File Offset: 0x000285D4
		// (set) Token: 0x06001C97 RID: 7319 RVA: 0x0002A3F5 File Offset: 0x000285F5
		public Utf8String OverrideLibraryPath
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_OverrideLibraryPath, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_OverrideLibraryPath);
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x0002A408 File Offset: 0x00028608
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x0002A420 File Offset: 0x00028620
		public uint SteamMajorVersion
		{
			get
			{
				return this.m_SteamMajorVersion;
			}
			set
			{
				this.m_SteamMajorVersion = value;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x0002A42C File Offset: 0x0002862C
		// (set) Token: 0x06001C9B RID: 7323 RVA: 0x0002A444 File Offset: 0x00028644
		public uint SteamMinorVersion
		{
			get
			{
				return this.m_SteamMinorVersion;
			}
			set
			{
				this.m_SteamMinorVersion = value;
			}
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0002A44E File Offset: 0x0002864E
		public void Set(ref SteamOptions other)
		{
			this.m_ApiVersion = 2;
			this.OverrideLibraryPath = other.OverrideLibraryPath;
			this.SteamMajorVersion = other.SteamMajorVersion;
			this.SteamMinorVersion = other.SteamMinorVersion;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0002A480 File Offset: 0x00028680
		public void Set(ref SteamOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.OverrideLibraryPath = other.Value.OverrideLibraryPath;
				this.SteamMajorVersion = other.Value.SteamMajorVersion;
				this.SteamMinorVersion = other.Value.SteamMinorVersion;
			}
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0002A4E0 File Offset: 0x000286E0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_OverrideLibraryPath);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0002A4EF File Offset: 0x000286EF
		public void Get(out SteamOptions output)
		{
			output = default(SteamOptions);
			output.Set(ref this);
		}

		// Token: 0x04000CAB RID: 3243
		private int m_ApiVersion;

		// Token: 0x04000CAC RID: 3244
		private IntPtr m_OverrideLibraryPath;

		// Token: 0x04000CAD RID: 3245
		private uint m_SteamMajorVersion;

		// Token: 0x04000CAE RID: 3246
		private uint m_SteamMinorVersion;
	}
}
