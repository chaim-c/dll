using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003AC RID: 940
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetParameterOptionsInternal : ISettable<LobbySearchSetParameterOptions>, IDisposable
	{
		// Token: 0x1700070D RID: 1805
		// (set) Token: 0x060018B6 RID: 6326 RVA: 0x000257F7 File Offset: 0x000239F7
		public AttributeData? Parameter
		{
			set
			{
				Helper.Set<AttributeData, AttributeDataInternal>(ref value, ref this.m_Parameter);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x00025808 File Offset: 0x00023A08
		public ComparisonOp ComparisonOp
		{
			set
			{
				this.m_ComparisonOp = value;
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00025812 File Offset: 0x00023A12
		public void Set(ref LobbySearchSetParameterOptions other)
		{
			this.m_ApiVersion = 1;
			this.Parameter = other.Parameter;
			this.ComparisonOp = other.ComparisonOp;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00025838 File Offset: 0x00023A38
		public void Set(ref LobbySearchSetParameterOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Parameter = other.Value.Parameter;
				this.ComparisonOp = other.Value.ComparisonOp;
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00025883 File Offset: 0x00023A83
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Parameter);
		}

		// Token: 0x04000B50 RID: 2896
		private int m_ApiVersion;

		// Token: 0x04000B51 RID: 2897
		private IntPtr m_Parameter;

		// Token: 0x04000B52 RID: 2898
		private ComparisonOp m_ComparisonOp;
	}
}
