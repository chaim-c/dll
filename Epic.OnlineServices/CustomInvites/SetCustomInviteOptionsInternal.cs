using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x0200050C RID: 1292
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetCustomInviteOptionsInternal : ISettable<SetCustomInviteOptions>, IDisposable
	{
		// Token: 0x170009B6 RID: 2486
		// (set) Token: 0x0600213C RID: 8508 RVA: 0x00031598 File Offset: 0x0002F798
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (set) Token: 0x0600213D RID: 8509 RVA: 0x000315A8 File Offset: 0x0002F7A8
		public Utf8String Payload
		{
			set
			{
				Helper.Set(value, ref this.m_Payload);
			}
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000315B8 File Offset: 0x0002F7B8
		public void Set(ref SetCustomInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Payload = other.Payload;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000315DC File Offset: 0x0002F7DC
		public void Set(ref SetCustomInviteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Payload = other.Value.Payload;
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00031627 File Offset: 0x0002F827
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Payload);
		}

		// Token: 0x04000EC1 RID: 3777
		private int m_ApiVersion;

		// Token: 0x04000EC2 RID: 3778
		private IntPtr m_LocalUserId;

		// Token: 0x04000EC3 RID: 3779
		private IntPtr m_Payload;
	}
}
