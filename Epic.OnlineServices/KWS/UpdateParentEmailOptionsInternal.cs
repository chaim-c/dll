using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000452 RID: 1106
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateParentEmailOptionsInternal : ISettable<UpdateParentEmailOptions>, IDisposable
	{
		// Token: 0x17000810 RID: 2064
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x00029FC8 File Offset: 0x000281C8
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (set) Token: 0x06001C6A RID: 7274 RVA: 0x00029FD8 File Offset: 0x000281D8
		public Utf8String ParentEmail
		{
			set
			{
				Helper.Set(value, ref this.m_ParentEmail);
			}
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00029FE8 File Offset: 0x000281E8
		public void Set(ref UpdateParentEmailOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.ParentEmail = other.ParentEmail;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0002A00C File Offset: 0x0002820C
		public void Set(ref UpdateParentEmailOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.ParentEmail = other.Value.ParentEmail;
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0002A057 File Offset: 0x00028257
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ParentEmail);
		}

		// Token: 0x04000C8D RID: 3213
		private int m_ApiVersion;

		// Token: 0x04000C8E RID: 3214
		private IntPtr m_LocalUserId;

		// Token: 0x04000C8F RID: 3215
		private IntPtr m_ParentEmail;
	}
}
