using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000044 RID: 68
	public sealed class UserInfoInterface : Handle
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x00005F78 File Offset: 0x00004178
		public UserInfoInterface()
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00005F82 File Offset: 0x00004182
		public UserInfoInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00005F90 File Offset: 0x00004190
		public Result CopyExternalUserInfoByAccountId(ref CopyExternalUserInfoByAccountIdOptions options, out ExternalUserInfo? outExternalUserInfo)
		{
			CopyExternalUserInfoByAccountIdOptionsInternal copyExternalUserInfoByAccountIdOptionsInternal = default(CopyExternalUserInfoByAccountIdOptionsInternal);
			copyExternalUserInfoByAccountIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_UserInfo_CopyExternalUserInfoByAccountId(base.InnerHandle, ref copyExternalUserInfoByAccountIdOptionsInternal, ref zero);
			Helper.Dispose<CopyExternalUserInfoByAccountIdOptionsInternal>(ref copyExternalUserInfoByAccountIdOptionsInternal);
			Helper.Get<ExternalUserInfoInternal, ExternalUserInfo>(zero, out outExternalUserInfo);
			bool flag = outExternalUserInfo != null;
			if (flag)
			{
				Bindings.EOS_UserInfo_ExternalUserInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00005FF0 File Offset: 0x000041F0
		public Result CopyExternalUserInfoByAccountType(ref CopyExternalUserInfoByAccountTypeOptions options, out ExternalUserInfo? outExternalUserInfo)
		{
			CopyExternalUserInfoByAccountTypeOptionsInternal copyExternalUserInfoByAccountTypeOptionsInternal = default(CopyExternalUserInfoByAccountTypeOptionsInternal);
			copyExternalUserInfoByAccountTypeOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_UserInfo_CopyExternalUserInfoByAccountType(base.InnerHandle, ref copyExternalUserInfoByAccountTypeOptionsInternal, ref zero);
			Helper.Dispose<CopyExternalUserInfoByAccountTypeOptionsInternal>(ref copyExternalUserInfoByAccountTypeOptionsInternal);
			Helper.Get<ExternalUserInfoInternal, ExternalUserInfo>(zero, out outExternalUserInfo);
			bool flag = outExternalUserInfo != null;
			if (flag)
			{
				Bindings.EOS_UserInfo_ExternalUserInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00006050 File Offset: 0x00004250
		public Result CopyExternalUserInfoByIndex(ref CopyExternalUserInfoByIndexOptions options, out ExternalUserInfo? outExternalUserInfo)
		{
			CopyExternalUserInfoByIndexOptionsInternal copyExternalUserInfoByIndexOptionsInternal = default(CopyExternalUserInfoByIndexOptionsInternal);
			copyExternalUserInfoByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_UserInfo_CopyExternalUserInfoByIndex(base.InnerHandle, ref copyExternalUserInfoByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyExternalUserInfoByIndexOptionsInternal>(ref copyExternalUserInfoByIndexOptionsInternal);
			Helper.Get<ExternalUserInfoInternal, ExternalUserInfo>(zero, out outExternalUserInfo);
			bool flag = outExternalUserInfo != null;
			if (flag)
			{
				Bindings.EOS_UserInfo_ExternalUserInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000060B0 File Offset: 0x000042B0
		public Result CopyUserInfo(ref CopyUserInfoOptions options, out UserInfoData? outUserInfo)
		{
			CopyUserInfoOptionsInternal copyUserInfoOptionsInternal = default(CopyUserInfoOptionsInternal);
			copyUserInfoOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_UserInfo_CopyUserInfo(base.InnerHandle, ref copyUserInfoOptionsInternal, ref zero);
			Helper.Dispose<CopyUserInfoOptionsInternal>(ref copyUserInfoOptionsInternal);
			Helper.Get<UserInfoDataInternal, UserInfoData>(zero, out outUserInfo);
			bool flag = outUserInfo != null;
			if (flag)
			{
				Bindings.EOS_UserInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00006110 File Offset: 0x00004310
		public uint GetExternalUserInfoCount(ref GetExternalUserInfoCountOptions options)
		{
			GetExternalUserInfoCountOptionsInternal getExternalUserInfoCountOptionsInternal = default(GetExternalUserInfoCountOptionsInternal);
			getExternalUserInfoCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_UserInfo_GetExternalUserInfoCount(base.InnerHandle, ref getExternalUserInfoCountOptionsInternal);
			Helper.Dispose<GetExternalUserInfoCountOptionsInternal>(ref getExternalUserInfoCountOptionsInternal);
			return result;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000614C File Offset: 0x0000434C
		public void QueryUserInfo(ref QueryUserInfoOptions options, object clientData, OnQueryUserInfoCallback completionDelegate)
		{
			QueryUserInfoOptionsInternal queryUserInfoOptionsInternal = default(QueryUserInfoOptionsInternal);
			queryUserInfoOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryUserInfoCallbackInternal onQueryUserInfoCallbackInternal = new OnQueryUserInfoCallbackInternal(UserInfoInterface.OnQueryUserInfoCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryUserInfoCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UserInfo_QueryUserInfo(base.InnerHandle, ref queryUserInfoOptionsInternal, zero, onQueryUserInfoCallbackInternal);
			Helper.Dispose<QueryUserInfoOptionsInternal>(ref queryUserInfoOptionsInternal);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000061A8 File Offset: 0x000043A8
		public void QueryUserInfoByDisplayName(ref QueryUserInfoByDisplayNameOptions options, object clientData, OnQueryUserInfoByDisplayNameCallback completionDelegate)
		{
			QueryUserInfoByDisplayNameOptionsInternal queryUserInfoByDisplayNameOptionsInternal = default(QueryUserInfoByDisplayNameOptionsInternal);
			queryUserInfoByDisplayNameOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryUserInfoByDisplayNameCallbackInternal onQueryUserInfoByDisplayNameCallbackInternal = new OnQueryUserInfoByDisplayNameCallbackInternal(UserInfoInterface.OnQueryUserInfoByDisplayNameCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryUserInfoByDisplayNameCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UserInfo_QueryUserInfoByDisplayName(base.InnerHandle, ref queryUserInfoByDisplayNameOptionsInternal, zero, onQueryUserInfoByDisplayNameCallbackInternal);
			Helper.Dispose<QueryUserInfoByDisplayNameOptionsInternal>(ref queryUserInfoByDisplayNameOptionsInternal);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00006204 File Offset: 0x00004404
		public void QueryUserInfoByExternalAccount(ref QueryUserInfoByExternalAccountOptions options, object clientData, OnQueryUserInfoByExternalAccountCallback completionDelegate)
		{
			QueryUserInfoByExternalAccountOptionsInternal queryUserInfoByExternalAccountOptionsInternal = default(QueryUserInfoByExternalAccountOptionsInternal);
			queryUserInfoByExternalAccountOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryUserInfoByExternalAccountCallbackInternal onQueryUserInfoByExternalAccountCallbackInternal = new OnQueryUserInfoByExternalAccountCallbackInternal(UserInfoInterface.OnQueryUserInfoByExternalAccountCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryUserInfoByExternalAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UserInfo_QueryUserInfoByExternalAccount(base.InnerHandle, ref queryUserInfoByExternalAccountOptionsInternal, zero, onQueryUserInfoByExternalAccountCallbackInternal);
			Helper.Dispose<QueryUserInfoByExternalAccountOptionsInternal>(ref queryUserInfoByExternalAccountOptionsInternal);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00006260 File Offset: 0x00004460
		[MonoPInvokeCallback(typeof(OnQueryUserInfoByDisplayNameCallbackInternal))]
		internal static void OnQueryUserInfoByDisplayNameCallbackInternalImplementation(ref QueryUserInfoByDisplayNameCallbackInfoInternal data)
		{
			OnQueryUserInfoByDisplayNameCallback onQueryUserInfoByDisplayNameCallback;
			QueryUserInfoByDisplayNameCallbackInfo queryUserInfoByDisplayNameCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryUserInfoByDisplayNameCallbackInfoInternal, OnQueryUserInfoByDisplayNameCallback, QueryUserInfoByDisplayNameCallbackInfo>(ref data, out onQueryUserInfoByDisplayNameCallback, out queryUserInfoByDisplayNameCallbackInfo);
			if (flag)
			{
				onQueryUserInfoByDisplayNameCallback(ref queryUserInfoByDisplayNameCallbackInfo);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00006288 File Offset: 0x00004488
		[MonoPInvokeCallback(typeof(OnQueryUserInfoByExternalAccountCallbackInternal))]
		internal static void OnQueryUserInfoByExternalAccountCallbackInternalImplementation(ref QueryUserInfoByExternalAccountCallbackInfoInternal data)
		{
			OnQueryUserInfoByExternalAccountCallback onQueryUserInfoByExternalAccountCallback;
			QueryUserInfoByExternalAccountCallbackInfo queryUserInfoByExternalAccountCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryUserInfoByExternalAccountCallbackInfoInternal, OnQueryUserInfoByExternalAccountCallback, QueryUserInfoByExternalAccountCallbackInfo>(ref data, out onQueryUserInfoByExternalAccountCallback, out queryUserInfoByExternalAccountCallbackInfo);
			if (flag)
			{
				onQueryUserInfoByExternalAccountCallback(ref queryUserInfoByExternalAccountCallbackInfo);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000062B0 File Offset: 0x000044B0
		[MonoPInvokeCallback(typeof(OnQueryUserInfoCallbackInternal))]
		internal static void OnQueryUserInfoCallbackInternalImplementation(ref QueryUserInfoCallbackInfoInternal data)
		{
			OnQueryUserInfoCallback onQueryUserInfoCallback;
			QueryUserInfoCallbackInfo queryUserInfoCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryUserInfoCallbackInfoInternal, OnQueryUserInfoCallback, QueryUserInfoCallbackInfo>(ref data, out onQueryUserInfoCallback, out queryUserInfoCallbackInfo);
			if (flag)
			{
				onQueryUserInfoCallback(ref queryUserInfoCallbackInfo);
			}
		}

		// Token: 0x040001A1 RID: 417
		public const int CopyexternaluserinfobyaccountidApiLatest = 1;

		// Token: 0x040001A2 RID: 418
		public const int CopyexternaluserinfobyaccounttypeApiLatest = 1;

		// Token: 0x040001A3 RID: 419
		public const int CopyexternaluserinfobyindexApiLatest = 1;

		// Token: 0x040001A4 RID: 420
		public const int CopyuserinfoApiLatest = 3;

		// Token: 0x040001A5 RID: 421
		public const int ExternaluserinfoApiLatest = 2;

		// Token: 0x040001A6 RID: 422
		public const int GetexternaluserinfocountApiLatest = 1;

		// Token: 0x040001A7 RID: 423
		public const int MaxDisplaynameCharacters = 16;

		// Token: 0x040001A8 RID: 424
		public const int MaxDisplaynameUtf8Length = 64;

		// Token: 0x040001A9 RID: 425
		public const int QueryuserinfoApiLatest = 1;

		// Token: 0x040001AA RID: 426
		public const int QueryuserinfobydisplaynameApiLatest = 1;

		// Token: 0x040001AB RID: 427
		public const int QueryuserinfobyexternalaccountApiLatest = 1;
	}
}
