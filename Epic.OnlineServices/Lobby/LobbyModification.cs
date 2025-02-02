using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000388 RID: 904
	public sealed class LobbyModification : Handle
	{
		// Token: 0x0600182A RID: 6186 RVA: 0x00024B1A File Offset: 0x00022D1A
		public LobbyModification()
		{
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00024B24 File Offset: 0x00022D24
		public LobbyModification(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00024B30 File Offset: 0x00022D30
		public Result AddAttribute(ref LobbyModificationAddAttributeOptions options)
		{
			LobbyModificationAddAttributeOptionsInternal lobbyModificationAddAttributeOptionsInternal = default(LobbyModificationAddAttributeOptionsInternal);
			lobbyModificationAddAttributeOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbyModification_AddAttribute(base.InnerHandle, ref lobbyModificationAddAttributeOptionsInternal);
			Helper.Dispose<LobbyModificationAddAttributeOptionsInternal>(ref lobbyModificationAddAttributeOptionsInternal);
			return result;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00024B6C File Offset: 0x00022D6C
		public Result AddMemberAttribute(ref LobbyModificationAddMemberAttributeOptions options)
		{
			LobbyModificationAddMemberAttributeOptionsInternal lobbyModificationAddMemberAttributeOptionsInternal = default(LobbyModificationAddMemberAttributeOptionsInternal);
			lobbyModificationAddMemberAttributeOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbyModification_AddMemberAttribute(base.InnerHandle, ref lobbyModificationAddMemberAttributeOptionsInternal);
			Helper.Dispose<LobbyModificationAddMemberAttributeOptionsInternal>(ref lobbyModificationAddMemberAttributeOptionsInternal);
			return result;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00024BA6 File Offset: 0x00022DA6
		public void Release()
		{
			Bindings.EOS_LobbyModification_Release(base.InnerHandle);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00024BB8 File Offset: 0x00022DB8
		public Result RemoveAttribute(ref LobbyModificationRemoveAttributeOptions options)
		{
			LobbyModificationRemoveAttributeOptionsInternal lobbyModificationRemoveAttributeOptionsInternal = default(LobbyModificationRemoveAttributeOptionsInternal);
			lobbyModificationRemoveAttributeOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbyModification_RemoveAttribute(base.InnerHandle, ref lobbyModificationRemoveAttributeOptionsInternal);
			Helper.Dispose<LobbyModificationRemoveAttributeOptionsInternal>(ref lobbyModificationRemoveAttributeOptionsInternal);
			return result;
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00024BF4 File Offset: 0x00022DF4
		public Result RemoveMemberAttribute(ref LobbyModificationRemoveMemberAttributeOptions options)
		{
			LobbyModificationRemoveMemberAttributeOptionsInternal lobbyModificationRemoveMemberAttributeOptionsInternal = default(LobbyModificationRemoveMemberAttributeOptionsInternal);
			lobbyModificationRemoveMemberAttributeOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbyModification_RemoveMemberAttribute(base.InnerHandle, ref lobbyModificationRemoveMemberAttributeOptionsInternal);
			Helper.Dispose<LobbyModificationRemoveMemberAttributeOptionsInternal>(ref lobbyModificationRemoveMemberAttributeOptionsInternal);
			return result;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00024C30 File Offset: 0x00022E30
		public Result SetBucketId(ref LobbyModificationSetBucketIdOptions options)
		{
			LobbyModificationSetBucketIdOptionsInternal lobbyModificationSetBucketIdOptionsInternal = default(LobbyModificationSetBucketIdOptionsInternal);
			lobbyModificationSetBucketIdOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbyModification_SetBucketId(base.InnerHandle, ref lobbyModificationSetBucketIdOptionsInternal);
			Helper.Dispose<LobbyModificationSetBucketIdOptionsInternal>(ref lobbyModificationSetBucketIdOptionsInternal);
			return result;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00024C6C File Offset: 0x00022E6C
		public Result SetInvitesAllowed(ref LobbyModificationSetInvitesAllowedOptions options)
		{
			LobbyModificationSetInvitesAllowedOptionsInternal lobbyModificationSetInvitesAllowedOptionsInternal = default(LobbyModificationSetInvitesAllowedOptionsInternal);
			lobbyModificationSetInvitesAllowedOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbyModification_SetInvitesAllowed(base.InnerHandle, ref lobbyModificationSetInvitesAllowedOptionsInternal);
			Helper.Dispose<LobbyModificationSetInvitesAllowedOptionsInternal>(ref lobbyModificationSetInvitesAllowedOptionsInternal);
			return result;
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00024CA8 File Offset: 0x00022EA8
		public Result SetMaxMembers(ref LobbyModificationSetMaxMembersOptions options)
		{
			LobbyModificationSetMaxMembersOptionsInternal lobbyModificationSetMaxMembersOptionsInternal = default(LobbyModificationSetMaxMembersOptionsInternal);
			lobbyModificationSetMaxMembersOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbyModification_SetMaxMembers(base.InnerHandle, ref lobbyModificationSetMaxMembersOptionsInternal);
			Helper.Dispose<LobbyModificationSetMaxMembersOptionsInternal>(ref lobbyModificationSetMaxMembersOptionsInternal);
			return result;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00024CE4 File Offset: 0x00022EE4
		public Result SetPermissionLevel(ref LobbyModificationSetPermissionLevelOptions options)
		{
			LobbyModificationSetPermissionLevelOptionsInternal lobbyModificationSetPermissionLevelOptionsInternal = default(LobbyModificationSetPermissionLevelOptionsInternal);
			lobbyModificationSetPermissionLevelOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbyModification_SetPermissionLevel(base.InnerHandle, ref lobbyModificationSetPermissionLevelOptionsInternal);
			Helper.Dispose<LobbyModificationSetPermissionLevelOptionsInternal>(ref lobbyModificationSetPermissionLevelOptionsInternal);
			return result;
		}

		// Token: 0x04000B06 RID: 2822
		public const int LobbymodificationAddattributeApiLatest = 1;

		// Token: 0x04000B07 RID: 2823
		public const int LobbymodificationAddmemberattributeApiLatest = 1;

		// Token: 0x04000B08 RID: 2824
		public const int LobbymodificationMaxAttributeLength = 64;

		// Token: 0x04000B09 RID: 2825
		public const int LobbymodificationMaxAttributes = 64;

		// Token: 0x04000B0A RID: 2826
		public const int LobbymodificationRemoveattributeApiLatest = 1;

		// Token: 0x04000B0B RID: 2827
		public const int LobbymodificationRemovememberattributeApiLatest = 1;

		// Token: 0x04000B0C RID: 2828
		public const int LobbymodificationSetbucketidApiLatest = 1;

		// Token: 0x04000B0D RID: 2829
		public const int LobbymodificationSetinvitesallowedApiLatest = 1;

		// Token: 0x04000B0E RID: 2830
		public const int LobbymodificationSetmaxmembersApiLatest = 1;

		// Token: 0x04000B0F RID: 2831
		public const int LobbymodificationSetpermissionlevelApiLatest = 1;
	}
}
