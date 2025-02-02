using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000649 RID: 1609
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ClientCredentialsInternal : IGettable<ClientCredentials>, ISettable<ClientCredentials>, IDisposable
	{
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x0003D574 File Offset: 0x0003B774
		// (set) Token: 0x06002917 RID: 10519 RVA: 0x0003D595 File Offset: 0x0003B795
		public Utf8String ClientId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ClientId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientId);
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x0003D5A8 File Offset: 0x0003B7A8
		// (set) Token: 0x06002919 RID: 10521 RVA: 0x0003D5C9 File Offset: 0x0003B7C9
		public Utf8String ClientSecret
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ClientSecret, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientSecret);
			}
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x0003D5D9 File Offset: 0x0003B7D9
		public void Set(ref ClientCredentials other)
		{
			this.ClientId = other.ClientId;
			this.ClientSecret = other.ClientSecret;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x0003D5F8 File Offset: 0x0003B7F8
		public void Set(ref ClientCredentials? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientId = other.Value.ClientId;
				this.ClientSecret = other.Value.ClientSecret;
			}
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x0003D63C File Offset: 0x0003B83C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientId);
			Helper.Dispose(ref this.m_ClientSecret);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0003D657 File Offset: 0x0003B857
		public void Get(out ClientCredentials output)
		{
			output = default(ClientCredentials);
			output.Set(ref this);
		}

		// Token: 0x04001285 RID: 4741
		private IntPtr m_ClientId;

		// Token: 0x04001286 RID: 4742
		private IntPtr m_ClientSecret;
	}
}
