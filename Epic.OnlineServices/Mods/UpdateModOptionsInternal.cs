using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x0200030B RID: 779
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateModOptionsInternal : ISettable<UpdateModOptions>, IDisposable
	{
		// Token: 0x170005CE RID: 1486
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x0001F10D File Offset: 0x0001D30D
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x0001F11D File Offset: 0x0001D31D
		public ModIdentifier? Mod
		{
			set
			{
				Helper.Set<ModIdentifier, ModIdentifierInternal>(ref value, ref this.m_Mod);
			}
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0001F12E File Offset: 0x0001D32E
		public void Set(ref UpdateModOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Mod = other.Mod;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0001F154 File Offset: 0x0001D354
		public void Set(ref UpdateModOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Mod = other.Value.Mod;
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0001F19F File Offset: 0x0001D39F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Mod);
		}

		// Token: 0x0400095D RID: 2397
		private int m_ApiVersion;

		// Token: 0x0400095E RID: 2398
		private IntPtr m_LocalUserId;

		// Token: 0x0400095F RID: 2399
		private IntPtr m_Mod;
	}
}
