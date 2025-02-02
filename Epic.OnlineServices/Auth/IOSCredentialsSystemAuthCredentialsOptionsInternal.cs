using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005B4 RID: 1460
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IOSCredentialsSystemAuthCredentialsOptionsInternal : IGettable<IOSCredentialsSystemAuthCredentialsOptions>, ISettable<IOSCredentialsSystemAuthCredentialsOptions>, IDisposable
	{
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x00037E70 File Offset: 0x00036070
		// (set) Token: 0x060025A8 RID: 9640 RVA: 0x00037E88 File Offset: 0x00036088
		public IntPtr PresentationContextProviding
		{
			get
			{
				return this.m_PresentationContextProviding;
			}
			set
			{
				this.m_PresentationContextProviding = value;
			}
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00037E92 File Offset: 0x00036092
		public void Set(ref IOSCredentialsSystemAuthCredentialsOptions other)
		{
			this.m_ApiVersion = 1;
			this.PresentationContextProviding = other.PresentationContextProviding;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00037EAC File Offset: 0x000360AC
		public void Set(ref IOSCredentialsSystemAuthCredentialsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PresentationContextProviding = other.Value.PresentationContextProviding;
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x00037EE2 File Offset: 0x000360E2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PresentationContextProviding);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00037EF1 File Offset: 0x000360F1
		public void Get(out IOSCredentialsSystemAuthCredentialsOptions output)
		{
			output = default(IOSCredentialsSystemAuthCredentialsOptions);
			output.Set(ref this);
		}

		// Token: 0x0400107F RID: 4223
		private int m_ApiVersion;

		// Token: 0x04001080 RID: 4224
		private IntPtr m_PresentationContextProviding;
	}
}
