using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033F RID: 831
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateLobbyOptionsInternal : ISettable<CreateLobbyOptions>, IDisposable
	{
		// Token: 0x1700061C RID: 1564
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x0002089E File Offset: 0x0001EA9E
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x000208AE File Offset: 0x0001EAAE
		public uint MaxLobbyMembers
		{
			set
			{
				this.m_MaxLobbyMembers = value;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (set) Token: 0x060015F8 RID: 5624 RVA: 0x000208B8 File Offset: 0x0001EAB8
		public LobbyPermissionLevel PermissionLevel
		{
			set
			{
				this.m_PermissionLevel = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x000208C2 File Offset: 0x0001EAC2
		public bool PresenceEnabled
		{
			set
			{
				Helper.Set(value, ref this.m_PresenceEnabled);
			}
		}

		// Token: 0x17000620 RID: 1568
		// (set) Token: 0x060015FA RID: 5626 RVA: 0x000208D2 File Offset: 0x0001EAD2
		public bool AllowInvites
		{
			set
			{
				Helper.Set(value, ref this.m_AllowInvites);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x000208E2 File Offset: 0x0001EAE2
		public Utf8String BucketId
		{
			set
			{
				Helper.Set(value, ref this.m_BucketId);
			}
		}

		// Token: 0x17000622 RID: 1570
		// (set) Token: 0x060015FC RID: 5628 RVA: 0x000208F2 File Offset: 0x0001EAF2
		public bool DisableHostMigration
		{
			set
			{
				Helper.Set(value, ref this.m_DisableHostMigration);
			}
		}

		// Token: 0x17000623 RID: 1571
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x00020902 File Offset: 0x0001EB02
		public bool EnableRTCRoom
		{
			set
			{
				Helper.Set(value, ref this.m_EnableRTCRoom);
			}
		}

		// Token: 0x17000624 RID: 1572
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x00020912 File Offset: 0x0001EB12
		public LocalRTCOptions? LocalRTCOptions
		{
			set
			{
				Helper.Set<LocalRTCOptions, LocalRTCOptionsInternal>(ref value, ref this.m_LocalRTCOptions);
			}
		}

		// Token: 0x17000625 RID: 1573
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x00020923 File Offset: 0x0001EB23
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x17000626 RID: 1574
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x00020933 File Offset: 0x0001EB33
		public bool EnableJoinById
		{
			set
			{
				Helper.Set(value, ref this.m_EnableJoinById);
			}
		}

		// Token: 0x17000627 RID: 1575
		// (set) Token: 0x06001601 RID: 5633 RVA: 0x00020943 File Offset: 0x0001EB43
		public bool RejoinAfterKickRequiresInvite
		{
			set
			{
				Helper.Set(value, ref this.m_RejoinAfterKickRequiresInvite);
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00020954 File Offset: 0x0001EB54
		public void Set(ref CreateLobbyOptions other)
		{
			this.m_ApiVersion = 8;
			this.LocalUserId = other.LocalUserId;
			this.MaxLobbyMembers = other.MaxLobbyMembers;
			this.PermissionLevel = other.PermissionLevel;
			this.PresenceEnabled = other.PresenceEnabled;
			this.AllowInvites = other.AllowInvites;
			this.BucketId = other.BucketId;
			this.DisableHostMigration = other.DisableHostMigration;
			this.EnableRTCRoom = other.EnableRTCRoom;
			this.LocalRTCOptions = other.LocalRTCOptions;
			this.LobbyId = other.LobbyId;
			this.EnableJoinById = other.EnableJoinById;
			this.RejoinAfterKickRequiresInvite = other.RejoinAfterKickRequiresInvite;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00020A08 File Offset: 0x0001EC08
		public void Set(ref CreateLobbyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 8;
				this.LocalUserId = other.Value.LocalUserId;
				this.MaxLobbyMembers = other.Value.MaxLobbyMembers;
				this.PermissionLevel = other.Value.PermissionLevel;
				this.PresenceEnabled = other.Value.PresenceEnabled;
				this.AllowInvites = other.Value.AllowInvites;
				this.BucketId = other.Value.BucketId;
				this.DisableHostMigration = other.Value.DisableHostMigration;
				this.EnableRTCRoom = other.Value.EnableRTCRoom;
				this.LocalRTCOptions = other.Value.LocalRTCOptions;
				this.LobbyId = other.Value.LobbyId;
				this.EnableJoinById = other.Value.EnableJoinById;
				this.RejoinAfterKickRequiresInvite = other.Value.RejoinAfterKickRequiresInvite;
			}
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00020B28 File Offset: 0x0001ED28
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_BucketId);
			Helper.Dispose(ref this.m_LocalRTCOptions);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x040009F2 RID: 2546
		private int m_ApiVersion;

		// Token: 0x040009F3 RID: 2547
		private IntPtr m_LocalUserId;

		// Token: 0x040009F4 RID: 2548
		private uint m_MaxLobbyMembers;

		// Token: 0x040009F5 RID: 2549
		private LobbyPermissionLevel m_PermissionLevel;

		// Token: 0x040009F6 RID: 2550
		private int m_PresenceEnabled;

		// Token: 0x040009F7 RID: 2551
		private int m_AllowInvites;

		// Token: 0x040009F8 RID: 2552
		private IntPtr m_BucketId;

		// Token: 0x040009F9 RID: 2553
		private int m_DisableHostMigration;

		// Token: 0x040009FA RID: 2554
		private int m_EnableRTCRoom;

		// Token: 0x040009FB RID: 2555
		private IntPtr m_LocalRTCOptions;

		// Token: 0x040009FC RID: 2556
		private IntPtr m_LobbyId;

		// Token: 0x040009FD RID: 2557
		private int m_EnableJoinById;

		// Token: 0x040009FE RID: 2558
		private int m_RejoinAfterKickRequiresInvite;
	}
}
