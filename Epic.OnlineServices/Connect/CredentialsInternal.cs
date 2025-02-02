using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000527 RID: 1319
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CredentialsInternal : IGettable<Credentials>, ISettable<Credentials>, IDisposable
	{
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x00032974 File Offset: 0x00030B74
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x00032995 File Offset: 0x00030B95
		public Utf8String Token
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Token, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Token);
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x000329A8 File Offset: 0x00030BA8
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x000329C0 File Offset: 0x00030BC0
		public ExternalCredentialType Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000329CA File Offset: 0x00030BCA
		public void Set(ref Credentials other)
		{
			this.m_ApiVersion = 1;
			this.Token = other.Token;
			this.Type = other.Type;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000329F0 File Offset: 0x00030BF0
		public void Set(ref Credentials? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Token = other.Value.Token;
				this.Type = other.Value.Type;
			}
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00032A3B File Offset: 0x00030C3B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Token);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x00032A4A File Offset: 0x00030C4A
		public void Get(out Credentials output)
		{
			output = default(Credentials);
			output.Set(ref this);
		}

		// Token: 0x04000F10 RID: 3856
		private int m_ApiVersion;

		// Token: 0x04000F11 RID: 3857
		private IntPtr m_Token;

		// Token: 0x04000F12 RID: 3858
		private ExternalCredentialType m_Type;
	}
}
