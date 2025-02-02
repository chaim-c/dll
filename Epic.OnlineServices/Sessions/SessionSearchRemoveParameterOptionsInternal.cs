using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200014E RID: 334
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchRemoveParameterOptionsInternal : ISettable<SessionSearchRemoveParameterOptions>, IDisposable
	{
		// Token: 0x1700021E RID: 542
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x0000E03A File Offset: 0x0000C23A
		public Utf8String Key
		{
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x1700021F RID: 543
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0000E04A File Offset: 0x0000C24A
		public ComparisonOp ComparisonOp
		{
			set
			{
				this.m_ComparisonOp = value;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0000E054 File Offset: 0x0000C254
		public void Set(ref SessionSearchRemoveParameterOptions other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
			this.ComparisonOp = other.ComparisonOp;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0000E078 File Offset: 0x0000C278
		public void Set(ref SessionSearchRemoveParameterOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Value.Key;
				this.ComparisonOp = other.Value.ComparisonOp;
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0000E0C3 File Offset: 0x0000C2C3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
		}

		// Token: 0x0400046A RID: 1130
		private int m_ApiVersion;

		// Token: 0x0400046B RID: 1131
		private IntPtr m_Key;

		// Token: 0x0400046C RID: 1132
		private ComparisonOp m_ComparisonOp;
	}
}
