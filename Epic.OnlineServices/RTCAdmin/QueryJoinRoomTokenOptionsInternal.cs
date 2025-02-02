using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x02000209 RID: 521
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryJoinRoomTokenOptionsInternal : ISettable<QueryJoinRoomTokenOptions>, IDisposable
	{
		// Token: 0x170003C8 RID: 968
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x00015AD8 File Offset: 0x00013CD8
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00015AE8 File Offset: 0x00013CE8
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x170003CA RID: 970
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x00015AF8 File Offset: 0x00013CF8
		public ProductUserId[] TargetUserIds
		{
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_TargetUserIds, out this.m_TargetUserIdsCount);
			}
		}

		// Token: 0x170003CB RID: 971
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x00015B0E File Offset: 0x00013D0E
		public Utf8String TargetUserIpAddresses
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserIpAddresses);
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00015B1E File Offset: 0x00013D1E
		public void Set(ref QueryJoinRoomTokenOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.TargetUserIds = other.TargetUserIds;
			this.TargetUserIpAddresses = other.TargetUserIpAddresses;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00015B5C File Offset: 0x00013D5C
		public void Set(ref QueryJoinRoomTokenOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.TargetUserIds = other.Value.TargetUserIds;
				this.TargetUserIpAddresses = other.Value.TargetUserIpAddresses;
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00015BD1 File Offset: 0x00013DD1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_TargetUserIds);
			Helper.Dispose(ref this.m_TargetUserIpAddresses);
		}

		// Token: 0x0400068E RID: 1678
		private int m_ApiVersion;

		// Token: 0x0400068F RID: 1679
		private IntPtr m_LocalUserId;

		// Token: 0x04000690 RID: 1680
		private IntPtr m_RoomName;

		// Token: 0x04000691 RID: 1681
		private IntPtr m_TargetUserIds;

		// Token: 0x04000692 RID: 1682
		private uint m_TargetUserIdsCount;

		// Token: 0x04000693 RID: 1683
		private IntPtr m_TargetUserIpAddresses;
	}
}
