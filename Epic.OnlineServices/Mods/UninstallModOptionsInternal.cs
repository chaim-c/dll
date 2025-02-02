using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x02000307 RID: 775
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UninstallModOptionsInternal : ISettable<UninstallModOptions>, IDisposable
	{
		// Token: 0x170005C1 RID: 1473
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x0001EDED File Offset: 0x0001CFED
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (set) Token: 0x060014DE RID: 5342 RVA: 0x0001EDFD File Offset: 0x0001CFFD
		public ModIdentifier? Mod
		{
			set
			{
				Helper.Set<ModIdentifier, ModIdentifierInternal>(ref value, ref this.m_Mod);
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0001EE0E File Offset: 0x0001D00E
		public void Set(ref UninstallModOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Mod = other.Mod;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0001EE34 File Offset: 0x0001D034
		public void Set(ref UninstallModOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Mod = other.Value.Mod;
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0001EE7F File Offset: 0x0001D07F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Mod);
		}

		// Token: 0x04000950 RID: 2384
		private int m_ApiVersion;

		// Token: 0x04000951 RID: 2385
		private IntPtr m_LocalUserId;

		// Token: 0x04000952 RID: 2386
		private IntPtr m_Mod;
	}
}
