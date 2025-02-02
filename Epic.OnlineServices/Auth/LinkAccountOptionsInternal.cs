using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000585 RID: 1413
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountOptionsInternal : ISettable<LinkAccountOptions>, IDisposable
	{
		// Token: 0x17000A97 RID: 2711
		// (set) Token: 0x06002431 RID: 9265 RVA: 0x00035E13 File Offset: 0x00034013
		public LinkAccountFlags LinkAccountFlags
		{
			set
			{
				this.m_LinkAccountFlags = value;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (set) Token: 0x06002432 RID: 9266 RVA: 0x00035E1D File Offset: 0x0003401D
		public ContinuanceToken ContinuanceToken
		{
			set
			{
				Helper.Set(value, ref this.m_ContinuanceToken);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (set) Token: 0x06002433 RID: 9267 RVA: 0x00035E2D File Offset: 0x0003402D
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x00035E3D File Offset: 0x0003403D
		public void Set(ref LinkAccountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LinkAccountFlags = other.LinkAccountFlags;
			this.ContinuanceToken = other.ContinuanceToken;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x00035E70 File Offset: 0x00034070
		public void Set(ref LinkAccountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LinkAccountFlags = other.Value.LinkAccountFlags;
				this.ContinuanceToken = other.Value.ContinuanceToken;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00035ED0 File Offset: 0x000340D0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ContinuanceToken);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000FF3 RID: 4083
		private int m_ApiVersion;

		// Token: 0x04000FF4 RID: 4084
		private LinkAccountFlags m_LinkAccountFlags;

		// Token: 0x04000FF5 RID: 4085
		private IntPtr m_ContinuanceToken;

		// Token: 0x04000FF6 RID: 4086
		private IntPtr m_LocalUserId;
	}
}
