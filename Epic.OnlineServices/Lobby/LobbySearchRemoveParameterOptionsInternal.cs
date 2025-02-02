using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A6 RID: 934
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchRemoveParameterOptionsInternal : ISettable<LobbySearchRemoveParameterOptions>, IDisposable
	{
		// Token: 0x17000705 RID: 1797
		// (set) Token: 0x060018A1 RID: 6305 RVA: 0x00025652 File Offset: 0x00023852
		public Utf8String Key
		{
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x00025662 File Offset: 0x00023862
		public ComparisonOp ComparisonOp
		{
			set
			{
				this.m_ComparisonOp = value;
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0002566C File Offset: 0x0002386C
		public void Set(ref LobbySearchRemoveParameterOptions other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
			this.ComparisonOp = other.ComparisonOp;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00025690 File Offset: 0x00023890
		public void Set(ref LobbySearchRemoveParameterOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Value.Key;
				this.ComparisonOp = other.Value.ComparisonOp;
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000256DB File Offset: 0x000238DB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
		}

		// Token: 0x04000B45 RID: 2885
		private int m_ApiVersion;

		// Token: 0x04000B46 RID: 2886
		private IntPtr m_Key;

		// Token: 0x04000B47 RID: 2887
		private ComparisonOp m_ComparisonOp;
	}
}
