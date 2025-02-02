using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200015B RID: 347
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StartSessionOptionsInternal : ISettable<StartSessionOptions>, IDisposable
	{
		// Token: 0x17000230 RID: 560
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0000EEFE File Offset: 0x0000D0FE
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0000EF0E File Offset: 0x0000D10E
		public void Set(ref StartSessionOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionName = other.SessionName;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0000EF28 File Offset: 0x0000D128
		public void Set(ref StartSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.Value.SessionName;
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000EF5E File Offset: 0x0000D15E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
		}

		// Token: 0x040004A2 RID: 1186
		private int m_ApiVersion;

		// Token: 0x040004A3 RID: 1187
		private IntPtr m_SessionName;
	}
}
