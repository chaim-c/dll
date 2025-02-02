using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200006A RID: 106
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PrePresentOptionsInternal : IGettable<PrePresentOptions>, ISettable<PrePresentOptions>, IDisposable
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00006E58 File Offset: 0x00005058
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00006E70 File Offset: 0x00005070
		public IntPtr PlatformSpecificData
		{
			get
			{
				return this.m_PlatformSpecificData;
			}
			set
			{
				this.m_PlatformSpecificData = value;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00006E7A File Offset: 0x0000507A
		public void Set(ref PrePresentOptions other)
		{
			this.m_ApiVersion = 1;
			this.PlatformSpecificData = other.PlatformSpecificData;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00006E94 File Offset: 0x00005094
		public void Set(ref PrePresentOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PlatformSpecificData = other.Value.PlatformSpecificData;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00006ECA File Offset: 0x000050CA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PlatformSpecificData);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00006ED9 File Offset: 0x000050D9
		public void Get(out PrePresentOptions output)
		{
			output = default(PrePresentOptions);
			output.Set(ref this);
		}

		// Token: 0x04000252 RID: 594
		private int m_ApiVersion;

		// Token: 0x04000253 RID: 595
		private IntPtr m_PlatformSpecificData;
	}
}
