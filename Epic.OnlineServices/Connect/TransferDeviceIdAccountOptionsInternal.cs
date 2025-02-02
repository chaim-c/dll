using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000563 RID: 1379
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransferDeviceIdAccountOptionsInternal : ISettable<TransferDeviceIdAccountOptions>, IDisposable
	{
		// Token: 0x17000A47 RID: 2631
		// (set) Token: 0x0600233F RID: 9023 RVA: 0x0003421D File Offset: 0x0003241D
		public ProductUserId PrimaryLocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_PrimaryLocalUserId);
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (set) Token: 0x06002340 RID: 9024 RVA: 0x0003422D File Offset: 0x0003242D
		public ProductUserId LocalDeviceUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalDeviceUserId);
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x0003423D File Offset: 0x0003243D
		public ProductUserId ProductUserIdToPreserve
		{
			set
			{
				Helper.Set(value, ref this.m_ProductUserIdToPreserve);
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0003424D File Offset: 0x0003244D
		public void Set(ref TransferDeviceIdAccountOptions other)
		{
			this.m_ApiVersion = 1;
			this.PrimaryLocalUserId = other.PrimaryLocalUserId;
			this.LocalDeviceUserId = other.LocalDeviceUserId;
			this.ProductUserIdToPreserve = other.ProductUserIdToPreserve;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00034280 File Offset: 0x00032480
		public void Set(ref TransferDeviceIdAccountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PrimaryLocalUserId = other.Value.PrimaryLocalUserId;
				this.LocalDeviceUserId = other.Value.LocalDeviceUserId;
				this.ProductUserIdToPreserve = other.Value.ProductUserIdToPreserve;
			}
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000342E0 File Offset: 0x000324E0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PrimaryLocalUserId);
			Helper.Dispose(ref this.m_LocalDeviceUserId);
			Helper.Dispose(ref this.m_ProductUserIdToPreserve);
		}

		// Token: 0x04000F7E RID: 3966
		private int m_ApiVersion;

		// Token: 0x04000F7F RID: 3967
		private IntPtr m_PrimaryLocalUserId;

		// Token: 0x04000F80 RID: 3968
		private IntPtr m_LocalDeviceUserId;

		// Token: 0x04000F81 RID: 3969
		private IntPtr m_ProductUserIdToPreserve;
	}
}
