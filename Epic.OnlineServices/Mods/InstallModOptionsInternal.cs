using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002F5 RID: 757
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InstallModOptionsInternal : ISettable<InstallModOptions>, IDisposable
	{
		// Token: 0x170005A5 RID: 1445
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x0001E406 File Offset: 0x0001C606
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (set) Token: 0x0600146C RID: 5228 RVA: 0x0001E416 File Offset: 0x0001C616
		public ModIdentifier? Mod
		{
			set
			{
				Helper.Set<ModIdentifier, ModIdentifierInternal>(ref value, ref this.m_Mod);
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x0001E427 File Offset: 0x0001C627
		public bool RemoveAfterExit
		{
			set
			{
				Helper.Set(value, ref this.m_RemoveAfterExit);
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0001E437 File Offset: 0x0001C637
		public void Set(ref InstallModOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Mod = other.Mod;
			this.RemoveAfterExit = other.RemoveAfterExit;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0001E468 File Offset: 0x0001C668
		public void Set(ref InstallModOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Mod = other.Value.Mod;
				this.RemoveAfterExit = other.Value.RemoveAfterExit;
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Mod);
		}

		// Token: 0x04000927 RID: 2343
		private int m_ApiVersion;

		// Token: 0x04000928 RID: 2344
		private IntPtr m_LocalUserId;

		// Token: 0x04000929 RID: 2345
		private IntPtr m_Mod;

		// Token: 0x0400092A RID: 2346
		private int m_RemoveAfterExit;
	}
}
