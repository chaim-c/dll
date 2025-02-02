using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000154 RID: 340
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetSessionIdOptionsInternal : ISettable<SessionSearchSetSessionIdOptions>, IDisposable
	{
		// Token: 0x17000227 RID: 551
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x0000E20B File Offset: 0x0000C40B
		public Utf8String SessionId
		{
			set
			{
				Helper.Set(value, ref this.m_SessionId);
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0000E21B File Offset: 0x0000C41B
		public void Set(ref SessionSearchSetSessionIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionId = other.SessionId;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0000E234 File Offset: 0x0000C434
		public void Set(ref SessionSearchSetSessionIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionId = other.Value.SessionId;
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0000E26A File Offset: 0x0000C46A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionId);
		}

		// Token: 0x04000476 RID: 1142
		private int m_ApiVersion;

		// Token: 0x04000477 RID: 1143
		private IntPtr m_SessionId;
	}
}
