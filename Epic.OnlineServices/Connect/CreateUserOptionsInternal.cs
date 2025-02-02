using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000525 RID: 1317
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserOptionsInternal : ISettable<CreateUserOptions>, IDisposable
	{
		// Token: 0x170009DC RID: 2524
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x000328C7 File Offset: 0x00030AC7
		public ContinuanceToken ContinuanceToken
		{
			set
			{
				Helper.Set(value, ref this.m_ContinuanceToken);
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000328D7 File Offset: 0x00030AD7
		public void Set(ref CreateUserOptions other)
		{
			this.m_ApiVersion = 1;
			this.ContinuanceToken = other.ContinuanceToken;
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000328F0 File Offset: 0x00030AF0
		public void Set(ref CreateUserOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ContinuanceToken = other.Value.ContinuanceToken;
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x00032926 File Offset: 0x00030B26
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ContinuanceToken);
		}

		// Token: 0x04000F0C RID: 3852
		private int m_ApiVersion;

		// Token: 0x04000F0D RID: 3853
		private IntPtr m_ContinuanceToken;
	}
}
