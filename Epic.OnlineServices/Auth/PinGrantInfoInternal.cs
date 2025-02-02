using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A2 RID: 1442
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PinGrantInfoInternal : IGettable<PinGrantInfo>, ISettable<PinGrantInfo>, IDisposable
	{
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x000368A4 File Offset: 0x00034AA4
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x000368C5 File Offset: 0x00034AC5
		public Utf8String UserCode
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_UserCode, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UserCode);
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x000368D8 File Offset: 0x00034AD8
		// (set) Token: 0x060024DF RID: 9439 RVA: 0x000368F9 File Offset: 0x00034AF9
		public Utf8String VerificationURI
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_VerificationURI, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_VerificationURI);
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x0003690C File Offset: 0x00034B0C
		// (set) Token: 0x060024E1 RID: 9441 RVA: 0x00036924 File Offset: 0x00034B24
		public int ExpiresIn
		{
			get
			{
				return this.m_ExpiresIn;
			}
			set
			{
				this.m_ExpiresIn = value;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x00036930 File Offset: 0x00034B30
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x00036951 File Offset: 0x00034B51
		public Utf8String VerificationURIComplete
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_VerificationURIComplete, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_VerificationURIComplete);
			}
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x00036961 File Offset: 0x00034B61
		public void Set(ref PinGrantInfo other)
		{
			this.m_ApiVersion = 2;
			this.UserCode = other.UserCode;
			this.VerificationURI = other.VerificationURI;
			this.ExpiresIn = other.ExpiresIn;
			this.VerificationURIComplete = other.VerificationURIComplete;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000369A0 File Offset: 0x00034BA0
		public void Set(ref PinGrantInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.UserCode = other.Value.UserCode;
				this.VerificationURI = other.Value.VerificationURI;
				this.ExpiresIn = other.Value.ExpiresIn;
				this.VerificationURIComplete = other.Value.VerificationURIComplete;
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00036A15 File Offset: 0x00034C15
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserCode);
			Helper.Dispose(ref this.m_VerificationURI);
			Helper.Dispose(ref this.m_VerificationURIComplete);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00036A3C File Offset: 0x00034C3C
		public void Get(out PinGrantInfo output)
		{
			output = default(PinGrantInfo);
			output.Set(ref this);
		}

		// Token: 0x04001028 RID: 4136
		private int m_ApiVersion;

		// Token: 0x04001029 RID: 4137
		private IntPtr m_UserCode;

		// Token: 0x0400102A RID: 4138
		private IntPtr m_VerificationURI;

		// Token: 0x0400102B RID: 4139
		private int m_ExpiresIn;

		// Token: 0x0400102C RID: 4140
		private IntPtr m_VerificationURIComplete;
	}
}
