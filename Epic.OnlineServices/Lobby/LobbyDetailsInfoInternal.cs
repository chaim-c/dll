using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200037B RID: 891
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsInfoInternal : IGettable<LobbyDetailsInfo>, ISettable<LobbyDetailsInfo>, IDisposable
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00022B50 File Offset: 0x00020D50
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x00022B71 File Offset: 0x00020D71
		public Utf8String LobbyId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LobbyId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00022B84 File Offset: 0x00020D84
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00022BA5 File Offset: 0x00020DA5
		public ProductUserId LobbyOwnerUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LobbyOwnerUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LobbyOwnerUserId);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00022BB8 File Offset: 0x00020DB8
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00022BD0 File Offset: 0x00020DD0
		public LobbyPermissionLevel PermissionLevel
		{
			get
			{
				return this.m_PermissionLevel;
			}
			set
			{
				this.m_PermissionLevel = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00022BDC File Offset: 0x00020DDC
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00022BF4 File Offset: 0x00020DF4
		public uint AvailableSlots
		{
			get
			{
				return this.m_AvailableSlots;
			}
			set
			{
				this.m_AvailableSlots = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x00022C00 File Offset: 0x00020E00
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x00022C18 File Offset: 0x00020E18
		public uint MaxMembers
		{
			get
			{
				return this.m_MaxMembers;
			}
			set
			{
				this.m_MaxMembers = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00022C24 File Offset: 0x00020E24
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x00022C45 File Offset: 0x00020E45
		public bool AllowInvites
		{
			get
			{
				bool result;
				Helper.Get(this.m_AllowInvites, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AllowInvites);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x00022C58 File Offset: 0x00020E58
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x00022C79 File Offset: 0x00020E79
		public Utf8String BucketId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_BucketId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_BucketId);
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00022C8C File Offset: 0x00020E8C
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x00022CAD File Offset: 0x00020EAD
		public bool AllowHostMigration
		{
			get
			{
				bool result;
				Helper.Get(this.m_AllowHostMigration, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AllowHostMigration);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00022CC0 File Offset: 0x00020EC0
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x00022CE1 File Offset: 0x00020EE1
		public bool RTCRoomEnabled
		{
			get
			{
				bool result;
				Helper.Get(this.m_RTCRoomEnabled, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_RTCRoomEnabled);
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x00022CF4 File Offset: 0x00020EF4
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x00022D15 File Offset: 0x00020F15
		public bool AllowJoinById
		{
			get
			{
				bool result;
				Helper.Get(this.m_AllowJoinById, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AllowJoinById);
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x00022D28 File Offset: 0x00020F28
		// (set) Token: 0x0600176F RID: 5999 RVA: 0x00022D49 File Offset: 0x00020F49
		public bool RejoinAfterKickRequiresInvite
		{
			get
			{
				bool result;
				Helper.Get(this.m_RejoinAfterKickRequiresInvite, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_RejoinAfterKickRequiresInvite);
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00022D5C File Offset: 0x00020F5C
		public void Set(ref LobbyDetailsInfo other)
		{
			this.m_ApiVersion = 2;
			this.LobbyId = other.LobbyId;
			this.LobbyOwnerUserId = other.LobbyOwnerUserId;
			this.PermissionLevel = other.PermissionLevel;
			this.AvailableSlots = other.AvailableSlots;
			this.MaxMembers = other.MaxMembers;
			this.AllowInvites = other.AllowInvites;
			this.BucketId = other.BucketId;
			this.AllowHostMigration = other.AllowHostMigration;
			this.RTCRoomEnabled = other.RTCRoomEnabled;
			this.AllowJoinById = other.AllowJoinById;
			this.RejoinAfterKickRequiresInvite = other.RejoinAfterKickRequiresInvite;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00022E00 File Offset: 0x00021000
		public void Set(ref LobbyDetailsInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LobbyId = other.Value.LobbyId;
				this.LobbyOwnerUserId = other.Value.LobbyOwnerUserId;
				this.PermissionLevel = other.Value.PermissionLevel;
				this.AvailableSlots = other.Value.AvailableSlots;
				this.MaxMembers = other.Value.MaxMembers;
				this.AllowInvites = other.Value.AllowInvites;
				this.BucketId = other.Value.BucketId;
				this.AllowHostMigration = other.Value.AllowHostMigration;
				this.RTCRoomEnabled = other.Value.RTCRoomEnabled;
				this.AllowJoinById = other.Value.AllowJoinById;
				this.RejoinAfterKickRequiresInvite = other.Value.RejoinAfterKickRequiresInvite;
			}
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00022F0B File Offset: 0x0002110B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LobbyOwnerUserId);
			Helper.Dispose(ref this.m_BucketId);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00022F32 File Offset: 0x00021132
		public void Get(out LobbyDetailsInfo output)
		{
			output = default(LobbyDetailsInfo);
			output.Set(ref this);
		}

		// Token: 0x04000A9F RID: 2719
		private int m_ApiVersion;

		// Token: 0x04000AA0 RID: 2720
		private IntPtr m_LobbyId;

		// Token: 0x04000AA1 RID: 2721
		private IntPtr m_LobbyOwnerUserId;

		// Token: 0x04000AA2 RID: 2722
		private LobbyPermissionLevel m_PermissionLevel;

		// Token: 0x04000AA3 RID: 2723
		private uint m_AvailableSlots;

		// Token: 0x04000AA4 RID: 2724
		private uint m_MaxMembers;

		// Token: 0x04000AA5 RID: 2725
		private int m_AllowInvites;

		// Token: 0x04000AA6 RID: 2726
		private IntPtr m_BucketId;

		// Token: 0x04000AA7 RID: 2727
		private int m_AllowHostMigration;

		// Token: 0x04000AA8 RID: 2728
		private int m_RTCRoomEnabled;

		// Token: 0x04000AA9 RID: 2729
		private int m_AllowJoinById;

		// Token: 0x04000AAA RID: 2730
		private int m_RejoinAfterKickRequiresInvite;
	}
}
