using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x0200050A RID: 1290
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendCustomInviteOptionsInternal : ISettable<SendCustomInviteOptions>, IDisposable
	{
		// Token: 0x170009B2 RID: 2482
		// (set) Token: 0x06002133 RID: 8499 RVA: 0x000314C5 File Offset: 0x0002F6C5
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (set) Token: 0x06002134 RID: 8500 RVA: 0x000314D5 File Offset: 0x0002F6D5
		public ProductUserId[] TargetUserIds
		{
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_TargetUserIds, out this.m_TargetUserIdsCount);
			}
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000314EB File Offset: 0x0002F6EB
		public void Set(ref SendCustomInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserIds = other.TargetUserIds;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00031510 File Offset: 0x0002F710
		public void Set(ref SendCustomInviteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserIds = other.Value.TargetUserIds;
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0003155B File Offset: 0x0002F75B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserIds);
		}

		// Token: 0x04000EBB RID: 3771
		private int m_ApiVersion;

		// Token: 0x04000EBC RID: 3772
		private IntPtr m_LocalUserId;

		// Token: 0x04000EBD RID: 3773
		private IntPtr m_TargetUserIds;

		// Token: 0x04000EBE RID: 3774
		private uint m_TargetUserIdsCount;
	}
}
