using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200042C RID: 1068
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserOptionsInternal : ISettable<CreateUserOptions>, IDisposable
	{
		// Token: 0x170007D2 RID: 2002
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x00028BF3 File Offset: 0x00026DF3
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x00028C03 File Offset: 0x00026E03
		public Utf8String DateOfBirth
		{
			set
			{
				Helper.Set(value, ref this.m_DateOfBirth);
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x00028C13 File Offset: 0x00026E13
		public Utf8String ParentEmail
		{
			set
			{
				Helper.Set(value, ref this.m_ParentEmail);
			}
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00028C23 File Offset: 0x00026E23
		public void Set(ref CreateUserOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.DateOfBirth = other.DateOfBirth;
			this.ParentEmail = other.ParentEmail;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00028C54 File Offset: 0x00026E54
		public void Set(ref CreateUserOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.DateOfBirth = other.Value.DateOfBirth;
				this.ParentEmail = other.Value.ParentEmail;
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00028CB4 File Offset: 0x00026EB4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_DateOfBirth);
			Helper.Dispose(ref this.m_ParentEmail);
		}

		// Token: 0x04000C3C RID: 3132
		private int m_ApiVersion;

		// Token: 0x04000C3D RID: 3133
		private IntPtr m_LocalUserId;

		// Token: 0x04000C3E RID: 3134
		private IntPtr m_DateOfBirth;

		// Token: 0x04000C3F RID: 3135
		private IntPtr m_ParentEmail;
	}
}
