using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000131 RID: 305
	public sealed class SessionModification : Handle
	{
		// Token: 0x0600092F RID: 2351 RVA: 0x0000D54B File Offset: 0x0000B74B
		public SessionModification()
		{
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000D555 File Offset: 0x0000B755
		public SessionModification(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0000D560 File Offset: 0x0000B760
		public Result AddAttribute(ref SessionModificationAddAttributeOptions options)
		{
			SessionModificationAddAttributeOptionsInternal sessionModificationAddAttributeOptionsInternal = default(SessionModificationAddAttributeOptionsInternal);
			sessionModificationAddAttributeOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionModification_AddAttribute(base.InnerHandle, ref sessionModificationAddAttributeOptionsInternal);
			Helper.Dispose<SessionModificationAddAttributeOptionsInternal>(ref sessionModificationAddAttributeOptionsInternal);
			return result;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0000D59A File Offset: 0x0000B79A
		public void Release()
		{
			Bindings.EOS_SessionModification_Release(base.InnerHandle);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public Result RemoveAttribute(ref SessionModificationRemoveAttributeOptions options)
		{
			SessionModificationRemoveAttributeOptionsInternal sessionModificationRemoveAttributeOptionsInternal = default(SessionModificationRemoveAttributeOptionsInternal);
			sessionModificationRemoveAttributeOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionModification_RemoveAttribute(base.InnerHandle, ref sessionModificationRemoveAttributeOptionsInternal);
			Helper.Dispose<SessionModificationRemoveAttributeOptionsInternal>(ref sessionModificationRemoveAttributeOptionsInternal);
			return result;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		public Result SetBucketId(ref SessionModificationSetBucketIdOptions options)
		{
			SessionModificationSetBucketIdOptionsInternal sessionModificationSetBucketIdOptionsInternal = default(SessionModificationSetBucketIdOptionsInternal);
			sessionModificationSetBucketIdOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionModification_SetBucketId(base.InnerHandle, ref sessionModificationSetBucketIdOptionsInternal);
			Helper.Dispose<SessionModificationSetBucketIdOptionsInternal>(ref sessionModificationSetBucketIdOptionsInternal);
			return result;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0000D624 File Offset: 0x0000B824
		public Result SetHostAddress(ref SessionModificationSetHostAddressOptions options)
		{
			SessionModificationSetHostAddressOptionsInternal sessionModificationSetHostAddressOptionsInternal = default(SessionModificationSetHostAddressOptionsInternal);
			sessionModificationSetHostAddressOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionModification_SetHostAddress(base.InnerHandle, ref sessionModificationSetHostAddressOptionsInternal);
			Helper.Dispose<SessionModificationSetHostAddressOptionsInternal>(ref sessionModificationSetHostAddressOptionsInternal);
			return result;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0000D660 File Offset: 0x0000B860
		public Result SetInvitesAllowed(ref SessionModificationSetInvitesAllowedOptions options)
		{
			SessionModificationSetInvitesAllowedOptionsInternal sessionModificationSetInvitesAllowedOptionsInternal = default(SessionModificationSetInvitesAllowedOptionsInternal);
			sessionModificationSetInvitesAllowedOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionModification_SetInvitesAllowed(base.InnerHandle, ref sessionModificationSetInvitesAllowedOptionsInternal);
			Helper.Dispose<SessionModificationSetInvitesAllowedOptionsInternal>(ref sessionModificationSetInvitesAllowedOptionsInternal);
			return result;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0000D69C File Offset: 0x0000B89C
		public Result SetJoinInProgressAllowed(ref SessionModificationSetJoinInProgressAllowedOptions options)
		{
			SessionModificationSetJoinInProgressAllowedOptionsInternal sessionModificationSetJoinInProgressAllowedOptionsInternal = default(SessionModificationSetJoinInProgressAllowedOptionsInternal);
			sessionModificationSetJoinInProgressAllowedOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionModification_SetJoinInProgressAllowed(base.InnerHandle, ref sessionModificationSetJoinInProgressAllowedOptionsInternal);
			Helper.Dispose<SessionModificationSetJoinInProgressAllowedOptionsInternal>(ref sessionModificationSetJoinInProgressAllowedOptionsInternal);
			return result;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		public Result SetMaxPlayers(ref SessionModificationSetMaxPlayersOptions options)
		{
			SessionModificationSetMaxPlayersOptionsInternal sessionModificationSetMaxPlayersOptionsInternal = default(SessionModificationSetMaxPlayersOptionsInternal);
			sessionModificationSetMaxPlayersOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionModification_SetMaxPlayers(base.InnerHandle, ref sessionModificationSetMaxPlayersOptionsInternal);
			Helper.Dispose<SessionModificationSetMaxPlayersOptionsInternal>(ref sessionModificationSetMaxPlayersOptionsInternal);
			return result;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0000D714 File Offset: 0x0000B914
		public Result SetPermissionLevel(ref SessionModificationSetPermissionLevelOptions options)
		{
			SessionModificationSetPermissionLevelOptionsInternal sessionModificationSetPermissionLevelOptionsInternal = default(SessionModificationSetPermissionLevelOptionsInternal);
			sessionModificationSetPermissionLevelOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionModification_SetPermissionLevel(base.InnerHandle, ref sessionModificationSetPermissionLevelOptionsInternal);
			Helper.Dispose<SessionModificationSetPermissionLevelOptionsInternal>(ref sessionModificationSetPermissionLevelOptionsInternal);
			return result;
		}

		// Token: 0x0400042F RID: 1071
		public const int SessionmodificationAddattributeApiLatest = 1;

		// Token: 0x04000430 RID: 1072
		public const int SessionmodificationMaxSessionAttributeLength = 64;

		// Token: 0x04000431 RID: 1073
		public const int SessionmodificationMaxSessionAttributes = 64;

		// Token: 0x04000432 RID: 1074
		public const int SessionmodificationMaxSessionidoverrideLength = 64;

		// Token: 0x04000433 RID: 1075
		public const int SessionmodificationMinSessionidoverrideLength = 16;

		// Token: 0x04000434 RID: 1076
		public const int SessionmodificationRemoveattributeApiLatest = 1;

		// Token: 0x04000435 RID: 1077
		public const int SessionmodificationSetbucketidApiLatest = 1;

		// Token: 0x04000436 RID: 1078
		public const int SessionmodificationSethostaddressApiLatest = 1;

		// Token: 0x04000437 RID: 1079
		public const int SessionmodificationSetinvitesallowedApiLatest = 1;

		// Token: 0x04000438 RID: 1080
		public const int SessionmodificationSetjoininprogressallowedApiLatest = 1;

		// Token: 0x04000439 RID: 1081
		public const int SessionmodificationSetmaxplayersApiLatest = 1;

		// Token: 0x0400043A RID: 1082
		public const int SessionmodificationSetpermissionlevelApiLatest = 1;
	}
}
