using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000152 RID: 338
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetParameterOptionsInternal : ISettable<SessionSearchSetParameterOptions>, IDisposable
	{
		// Token: 0x17000224 RID: 548
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0000E15F File Offset: 0x0000C35F
		public AttributeData? Parameter
		{
			set
			{
				Helper.Set<AttributeData, AttributeDataInternal>(ref value, ref this.m_Parameter);
			}
		}

		// Token: 0x17000225 RID: 549
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x0000E170 File Offset: 0x0000C370
		public ComparisonOp ComparisonOp
		{
			set
			{
				this.m_ComparisonOp = value;
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000E17A File Offset: 0x0000C37A
		public void Set(ref SessionSearchSetParameterOptions other)
		{
			this.m_ApiVersion = 1;
			this.Parameter = other.Parameter;
			this.ComparisonOp = other.ComparisonOp;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public void Set(ref SessionSearchSetParameterOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Parameter = other.Value.Parameter;
				this.ComparisonOp = other.Value.ComparisonOp;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0000E1EB File Offset: 0x0000C3EB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Parameter);
		}

		// Token: 0x04000472 RID: 1138
		private int m_ApiVersion;

		// Token: 0x04000473 RID: 1139
		private IntPtr m_Parameter;

		// Token: 0x04000474 RID: 1140
		private ComparisonOp m_ComparisonOp;
	}
}
