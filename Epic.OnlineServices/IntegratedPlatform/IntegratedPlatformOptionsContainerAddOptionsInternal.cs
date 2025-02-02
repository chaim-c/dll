using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.IntegratedPlatform
{
	// Token: 0x02000459 RID: 1113
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IntegratedPlatformOptionsContainerAddOptionsInternal : ISettable<IntegratedPlatformOptionsContainerAddOptions>, IDisposable
	{
		// Token: 0x17000813 RID: 2067
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x0002A16E File Offset: 0x0002836E
		public Options? Options
		{
			set
			{
				Helper.Set<Options, OptionsInternal>(ref value, ref this.m_Options);
			}
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0002A17F File Offset: 0x0002837F
		public void Set(ref IntegratedPlatformOptionsContainerAddOptions other)
		{
			this.m_ApiVersion = 1;
			this.Options = other.Options;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0002A198 File Offset: 0x00028398
		public void Set(ref IntegratedPlatformOptionsContainerAddOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Options = other.Value.Options;
			}
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0002A1CE File Offset: 0x000283CE
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Options);
		}

		// Token: 0x04000C9F RID: 3231
		private int m_ApiVersion;

		// Token: 0x04000CA0 RID: 3232
		private IntPtr m_Options;
	}
}
