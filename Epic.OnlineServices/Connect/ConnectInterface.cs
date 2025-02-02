using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000513 RID: 1299
	public sealed class ConnectInterface : Handle
	{
		// Token: 0x06002156 RID: 8534 RVA: 0x00031809 File Offset: 0x0002FA09
		public ConnectInterface()
		{
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00031813 File Offset: 0x0002FA13
		public ConnectInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00031820 File Offset: 0x0002FA20
		public ulong AddNotifyAuthExpiration(ref AddNotifyAuthExpirationOptions options, object clientData, OnAuthExpirationCallback notification)
		{
			AddNotifyAuthExpirationOptionsInternal addNotifyAuthExpirationOptionsInternal = default(AddNotifyAuthExpirationOptionsInternal);
			addNotifyAuthExpirationOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnAuthExpirationCallbackInternal onAuthExpirationCallbackInternal = new OnAuthExpirationCallbackInternal(ConnectInterface.OnAuthExpirationCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notification, onAuthExpirationCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Connect_AddNotifyAuthExpiration(base.InnerHandle, ref addNotifyAuthExpirationOptionsInternal, zero, onAuthExpirationCallbackInternal);
			Helper.Dispose<AddNotifyAuthExpirationOptionsInternal>(ref addNotifyAuthExpirationOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0003188C File Offset: 0x0002FA8C
		public ulong AddNotifyLoginStatusChanged(ref AddNotifyLoginStatusChangedOptions options, object clientData, OnLoginStatusChangedCallback notification)
		{
			AddNotifyLoginStatusChangedOptionsInternal addNotifyLoginStatusChangedOptionsInternal = default(AddNotifyLoginStatusChangedOptionsInternal);
			addNotifyLoginStatusChangedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLoginStatusChangedCallbackInternal onLoginStatusChangedCallbackInternal = new OnLoginStatusChangedCallbackInternal(ConnectInterface.OnLoginStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notification, onLoginStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Connect_AddNotifyLoginStatusChanged(base.InnerHandle, ref addNotifyLoginStatusChangedOptionsInternal, zero, onLoginStatusChangedCallbackInternal);
			Helper.Dispose<AddNotifyLoginStatusChangedOptionsInternal>(ref addNotifyLoginStatusChangedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000318F8 File Offset: 0x0002FAF8
		public Result CopyIdToken(ref CopyIdTokenOptions options, out IdToken? outIdToken)
		{
			CopyIdTokenOptionsInternal copyIdTokenOptionsInternal = default(CopyIdTokenOptionsInternal);
			copyIdTokenOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyIdToken(base.InnerHandle, ref copyIdTokenOptionsInternal, ref zero);
			Helper.Dispose<CopyIdTokenOptionsInternal>(ref copyIdTokenOptionsInternal);
			Helper.Get<IdTokenInternal, IdToken>(zero, out outIdToken);
			bool flag = outIdToken != null;
			if (flag)
			{
				Bindings.EOS_Connect_IdToken_Release(zero);
			}
			return result;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00031958 File Offset: 0x0002FB58
		public Result CopyProductUserExternalAccountByAccountId(ref CopyProductUserExternalAccountByAccountIdOptions options, out ExternalAccountInfo? outExternalAccountInfo)
		{
			CopyProductUserExternalAccountByAccountIdOptionsInternal copyProductUserExternalAccountByAccountIdOptionsInternal = default(CopyProductUserExternalAccountByAccountIdOptionsInternal);
			copyProductUserExternalAccountByAccountIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyProductUserExternalAccountByAccountId(base.InnerHandle, ref copyProductUserExternalAccountByAccountIdOptionsInternal, ref zero);
			Helper.Dispose<CopyProductUserExternalAccountByAccountIdOptionsInternal>(ref copyProductUserExternalAccountByAccountIdOptionsInternal);
			Helper.Get<ExternalAccountInfoInternal, ExternalAccountInfo>(zero, out outExternalAccountInfo);
			bool flag = outExternalAccountInfo != null;
			if (flag)
			{
				Bindings.EOS_Connect_ExternalAccountInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000319B8 File Offset: 0x0002FBB8
		public Result CopyProductUserExternalAccountByAccountType(ref CopyProductUserExternalAccountByAccountTypeOptions options, out ExternalAccountInfo? outExternalAccountInfo)
		{
			CopyProductUserExternalAccountByAccountTypeOptionsInternal copyProductUserExternalAccountByAccountTypeOptionsInternal = default(CopyProductUserExternalAccountByAccountTypeOptionsInternal);
			copyProductUserExternalAccountByAccountTypeOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyProductUserExternalAccountByAccountType(base.InnerHandle, ref copyProductUserExternalAccountByAccountTypeOptionsInternal, ref zero);
			Helper.Dispose<CopyProductUserExternalAccountByAccountTypeOptionsInternal>(ref copyProductUserExternalAccountByAccountTypeOptionsInternal);
			Helper.Get<ExternalAccountInfoInternal, ExternalAccountInfo>(zero, out outExternalAccountInfo);
			bool flag = outExternalAccountInfo != null;
			if (flag)
			{
				Bindings.EOS_Connect_ExternalAccountInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00031A18 File Offset: 0x0002FC18
		public Result CopyProductUserExternalAccountByIndex(ref CopyProductUserExternalAccountByIndexOptions options, out ExternalAccountInfo? outExternalAccountInfo)
		{
			CopyProductUserExternalAccountByIndexOptionsInternal copyProductUserExternalAccountByIndexOptionsInternal = default(CopyProductUserExternalAccountByIndexOptionsInternal);
			copyProductUserExternalAccountByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyProductUserExternalAccountByIndex(base.InnerHandle, ref copyProductUserExternalAccountByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyProductUserExternalAccountByIndexOptionsInternal>(ref copyProductUserExternalAccountByIndexOptionsInternal);
			Helper.Get<ExternalAccountInfoInternal, ExternalAccountInfo>(zero, out outExternalAccountInfo);
			bool flag = outExternalAccountInfo != null;
			if (flag)
			{
				Bindings.EOS_Connect_ExternalAccountInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00031A78 File Offset: 0x0002FC78
		public Result CopyProductUserInfo(ref CopyProductUserInfoOptions options, out ExternalAccountInfo? outExternalAccountInfo)
		{
			CopyProductUserInfoOptionsInternal copyProductUserInfoOptionsInternal = default(CopyProductUserInfoOptionsInternal);
			copyProductUserInfoOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyProductUserInfo(base.InnerHandle, ref copyProductUserInfoOptionsInternal, ref zero);
			Helper.Dispose<CopyProductUserInfoOptionsInternal>(ref copyProductUserInfoOptionsInternal);
			Helper.Get<ExternalAccountInfoInternal, ExternalAccountInfo>(zero, out outExternalAccountInfo);
			bool flag = outExternalAccountInfo != null;
			if (flag)
			{
				Bindings.EOS_Connect_ExternalAccountInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x00031AD8 File Offset: 0x0002FCD8
		public void CreateDeviceId(ref CreateDeviceIdOptions options, object clientData, OnCreateDeviceIdCallback completionDelegate)
		{
			CreateDeviceIdOptionsInternal createDeviceIdOptionsInternal = default(CreateDeviceIdOptionsInternal);
			createDeviceIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnCreateDeviceIdCallbackInternal onCreateDeviceIdCallbackInternal = new OnCreateDeviceIdCallbackInternal(ConnectInterface.OnCreateDeviceIdCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onCreateDeviceIdCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_CreateDeviceId(base.InnerHandle, ref createDeviceIdOptionsInternal, zero, onCreateDeviceIdCallbackInternal);
			Helper.Dispose<CreateDeviceIdOptionsInternal>(ref createDeviceIdOptionsInternal);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00031B34 File Offset: 0x0002FD34
		public void CreateUser(ref CreateUserOptions options, object clientData, OnCreateUserCallback completionDelegate)
		{
			CreateUserOptionsInternal createUserOptionsInternal = default(CreateUserOptionsInternal);
			createUserOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnCreateUserCallbackInternal onCreateUserCallbackInternal = new OnCreateUserCallbackInternal(ConnectInterface.OnCreateUserCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onCreateUserCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_CreateUser(base.InnerHandle, ref createUserOptionsInternal, zero, onCreateUserCallbackInternal);
			Helper.Dispose<CreateUserOptionsInternal>(ref createUserOptionsInternal);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x00031B90 File Offset: 0x0002FD90
		public void DeleteDeviceId(ref DeleteDeviceIdOptions options, object clientData, OnDeleteDeviceIdCallback completionDelegate)
		{
			DeleteDeviceIdOptionsInternal deleteDeviceIdOptionsInternal = default(DeleteDeviceIdOptionsInternal);
			deleteDeviceIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnDeleteDeviceIdCallbackInternal onDeleteDeviceIdCallbackInternal = new OnDeleteDeviceIdCallbackInternal(ConnectInterface.OnDeleteDeviceIdCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onDeleteDeviceIdCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_DeleteDeviceId(base.InnerHandle, ref deleteDeviceIdOptionsInternal, zero, onDeleteDeviceIdCallbackInternal);
			Helper.Dispose<DeleteDeviceIdOptionsInternal>(ref deleteDeviceIdOptionsInternal);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x00031BEC File Offset: 0x0002FDEC
		public ProductUserId GetExternalAccountMapping(ref GetExternalAccountMappingsOptions options)
		{
			GetExternalAccountMappingsOptionsInternal getExternalAccountMappingsOptionsInternal = default(GetExternalAccountMappingsOptionsInternal);
			getExternalAccountMappingsOptionsInternal.Set(ref options);
			IntPtr from = Bindings.EOS_Connect_GetExternalAccountMapping(base.InnerHandle, ref getExternalAccountMappingsOptionsInternal);
			Helper.Dispose<GetExternalAccountMappingsOptionsInternal>(ref getExternalAccountMappingsOptionsInternal);
			ProductUserId result;
			Helper.Get<ProductUserId>(from, out result);
			return result;
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00031C30 File Offset: 0x0002FE30
		public ProductUserId GetLoggedInUserByIndex(int index)
		{
			IntPtr from = Bindings.EOS_Connect_GetLoggedInUserByIndex(base.InnerHandle, index);
			ProductUserId result;
			Helper.Get<ProductUserId>(from, out result);
			return result;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00031C5C File Offset: 0x0002FE5C
		public int GetLoggedInUsersCount()
		{
			return Bindings.EOS_Connect_GetLoggedInUsersCount(base.InnerHandle);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00031C7C File Offset: 0x0002FE7C
		public LoginStatus GetLoginStatus(ProductUserId localUserId)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(localUserId, ref zero);
			return Bindings.EOS_Connect_GetLoginStatus(base.InnerHandle, zero);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00031CAC File Offset: 0x0002FEAC
		public uint GetProductUserExternalAccountCount(ref GetProductUserExternalAccountCountOptions options)
		{
			GetProductUserExternalAccountCountOptionsInternal getProductUserExternalAccountCountOptionsInternal = default(GetProductUserExternalAccountCountOptionsInternal);
			getProductUserExternalAccountCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Connect_GetProductUserExternalAccountCount(base.InnerHandle, ref getProductUserExternalAccountCountOptionsInternal);
			Helper.Dispose<GetProductUserExternalAccountCountOptionsInternal>(ref getProductUserExternalAccountCountOptionsInternal);
			return result;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00031CE8 File Offset: 0x0002FEE8
		public Result GetProductUserIdMapping(ref GetProductUserIdMappingOptions options, out Utf8String outBuffer)
		{
			GetProductUserIdMappingOptionsInternal getProductUserIdMappingOptionsInternal = default(GetProductUserIdMappingOptionsInternal);
			getProductUserIdMappingOptionsInternal.Set(ref options);
			int size = 257;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Connect_GetProductUserIdMapping(base.InnerHandle, ref getProductUserIdMappingOptionsInternal, intPtr, ref size);
			Helper.Dispose<GetProductUserIdMappingOptionsInternal>(ref getProductUserIdMappingOptionsInternal);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x00031D44 File Offset: 0x0002FF44
		public void LinkAccount(ref LinkAccountOptions options, object clientData, OnLinkAccountCallback completionDelegate)
		{
			LinkAccountOptionsInternal linkAccountOptionsInternal = default(LinkAccountOptionsInternal);
			linkAccountOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLinkAccountCallbackInternal onLinkAccountCallbackInternal = new OnLinkAccountCallbackInternal(ConnectInterface.OnLinkAccountCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onLinkAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_LinkAccount(base.InnerHandle, ref linkAccountOptionsInternal, zero, onLinkAccountCallbackInternal);
			Helper.Dispose<LinkAccountOptionsInternal>(ref linkAccountOptionsInternal);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x00031DA0 File Offset: 0x0002FFA0
		public void Login(ref LoginOptions options, object clientData, OnLoginCallback completionDelegate)
		{
			LoginOptionsInternal loginOptionsInternal = default(LoginOptionsInternal);
			loginOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLoginCallbackInternal onLoginCallbackInternal = new OnLoginCallbackInternal(ConnectInterface.OnLoginCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onLoginCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_Login(base.InnerHandle, ref loginOptionsInternal, zero, onLoginCallbackInternal);
			Helper.Dispose<LoginOptionsInternal>(ref loginOptionsInternal);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00031DFC File Offset: 0x0002FFFC
		public void QueryExternalAccountMappings(ref QueryExternalAccountMappingsOptions options, object clientData, OnQueryExternalAccountMappingsCallback completionDelegate)
		{
			QueryExternalAccountMappingsOptionsInternal queryExternalAccountMappingsOptionsInternal = default(QueryExternalAccountMappingsOptionsInternal);
			queryExternalAccountMappingsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryExternalAccountMappingsCallbackInternal onQueryExternalAccountMappingsCallbackInternal = new OnQueryExternalAccountMappingsCallbackInternal(ConnectInterface.OnQueryExternalAccountMappingsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryExternalAccountMappingsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_QueryExternalAccountMappings(base.InnerHandle, ref queryExternalAccountMappingsOptionsInternal, zero, onQueryExternalAccountMappingsCallbackInternal);
			Helper.Dispose<QueryExternalAccountMappingsOptionsInternal>(ref queryExternalAccountMappingsOptionsInternal);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x00031E58 File Offset: 0x00030058
		public void QueryProductUserIdMappings(ref QueryProductUserIdMappingsOptions options, object clientData, OnQueryProductUserIdMappingsCallback completionDelegate)
		{
			QueryProductUserIdMappingsOptionsInternal queryProductUserIdMappingsOptionsInternal = default(QueryProductUserIdMappingsOptionsInternal);
			queryProductUserIdMappingsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryProductUserIdMappingsCallbackInternal onQueryProductUserIdMappingsCallbackInternal = new OnQueryProductUserIdMappingsCallbackInternal(ConnectInterface.OnQueryProductUserIdMappingsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryProductUserIdMappingsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_QueryProductUserIdMappings(base.InnerHandle, ref queryProductUserIdMappingsOptionsInternal, zero, onQueryProductUserIdMappingsCallbackInternal);
			Helper.Dispose<QueryProductUserIdMappingsOptionsInternal>(ref queryProductUserIdMappingsOptionsInternal);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x00031EB2 File Offset: 0x000300B2
		public void RemoveNotifyAuthExpiration(ulong inId)
		{
			Bindings.EOS_Connect_RemoveNotifyAuthExpiration(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x00031EC9 File Offset: 0x000300C9
		public void RemoveNotifyLoginStatusChanged(ulong inId)
		{
			Bindings.EOS_Connect_RemoveNotifyLoginStatusChanged(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x00031EE0 File Offset: 0x000300E0
		public void TransferDeviceIdAccount(ref TransferDeviceIdAccountOptions options, object clientData, OnTransferDeviceIdAccountCallback completionDelegate)
		{
			TransferDeviceIdAccountOptionsInternal transferDeviceIdAccountOptionsInternal = default(TransferDeviceIdAccountOptionsInternal);
			transferDeviceIdAccountOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnTransferDeviceIdAccountCallbackInternal onTransferDeviceIdAccountCallbackInternal = new OnTransferDeviceIdAccountCallbackInternal(ConnectInterface.OnTransferDeviceIdAccountCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onTransferDeviceIdAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_TransferDeviceIdAccount(base.InnerHandle, ref transferDeviceIdAccountOptionsInternal, zero, onTransferDeviceIdAccountCallbackInternal);
			Helper.Dispose<TransferDeviceIdAccountOptionsInternal>(ref transferDeviceIdAccountOptionsInternal);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x00031F3C File Offset: 0x0003013C
		public void UnlinkAccount(ref UnlinkAccountOptions options, object clientData, OnUnlinkAccountCallback completionDelegate)
		{
			UnlinkAccountOptionsInternal unlinkAccountOptionsInternal = default(UnlinkAccountOptionsInternal);
			unlinkAccountOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUnlinkAccountCallbackInternal onUnlinkAccountCallbackInternal = new OnUnlinkAccountCallbackInternal(ConnectInterface.OnUnlinkAccountCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUnlinkAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_UnlinkAccount(base.InnerHandle, ref unlinkAccountOptionsInternal, zero, onUnlinkAccountCallbackInternal);
			Helper.Dispose<UnlinkAccountOptionsInternal>(ref unlinkAccountOptionsInternal);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x00031F98 File Offset: 0x00030198
		public void VerifyIdToken(ref VerifyIdTokenOptions options, object clientData, OnVerifyIdTokenCallback completionDelegate)
		{
			VerifyIdTokenOptionsInternal verifyIdTokenOptionsInternal = default(VerifyIdTokenOptionsInternal);
			verifyIdTokenOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnVerifyIdTokenCallbackInternal onVerifyIdTokenCallbackInternal = new OnVerifyIdTokenCallbackInternal(ConnectInterface.OnVerifyIdTokenCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onVerifyIdTokenCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_VerifyIdToken(base.InnerHandle, ref verifyIdTokenOptionsInternal, zero, onVerifyIdTokenCallbackInternal);
			Helper.Dispose<VerifyIdTokenOptionsInternal>(ref verifyIdTokenOptionsInternal);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00031FF4 File Offset: 0x000301F4
		[MonoPInvokeCallback(typeof(OnAuthExpirationCallbackInternal))]
		internal static void OnAuthExpirationCallbackInternalImplementation(ref AuthExpirationCallbackInfoInternal data)
		{
			OnAuthExpirationCallback onAuthExpirationCallback;
			AuthExpirationCallbackInfo authExpirationCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<AuthExpirationCallbackInfoInternal, OnAuthExpirationCallback, AuthExpirationCallbackInfo>(ref data, out onAuthExpirationCallback, out authExpirationCallbackInfo);
			if (flag)
			{
				onAuthExpirationCallback(ref authExpirationCallbackInfo);
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0003201C File Offset: 0x0003021C
		[MonoPInvokeCallback(typeof(OnCreateDeviceIdCallbackInternal))]
		internal static void OnCreateDeviceIdCallbackInternalImplementation(ref CreateDeviceIdCallbackInfoInternal data)
		{
			OnCreateDeviceIdCallback onCreateDeviceIdCallback;
			CreateDeviceIdCallbackInfo createDeviceIdCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<CreateDeviceIdCallbackInfoInternal, OnCreateDeviceIdCallback, CreateDeviceIdCallbackInfo>(ref data, out onCreateDeviceIdCallback, out createDeviceIdCallbackInfo);
			if (flag)
			{
				onCreateDeviceIdCallback(ref createDeviceIdCallbackInfo);
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x00032044 File Offset: 0x00030244
		[MonoPInvokeCallback(typeof(OnCreateUserCallbackInternal))]
		internal static void OnCreateUserCallbackInternalImplementation(ref CreateUserCallbackInfoInternal data)
		{
			OnCreateUserCallback onCreateUserCallback;
			CreateUserCallbackInfo createUserCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<CreateUserCallbackInfoInternal, OnCreateUserCallback, CreateUserCallbackInfo>(ref data, out onCreateUserCallback, out createUserCallbackInfo);
			if (flag)
			{
				onCreateUserCallback(ref createUserCallbackInfo);
			}
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0003206C File Offset: 0x0003026C
		[MonoPInvokeCallback(typeof(OnDeleteDeviceIdCallbackInternal))]
		internal static void OnDeleteDeviceIdCallbackInternalImplementation(ref DeleteDeviceIdCallbackInfoInternal data)
		{
			OnDeleteDeviceIdCallback onDeleteDeviceIdCallback;
			DeleteDeviceIdCallbackInfo deleteDeviceIdCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<DeleteDeviceIdCallbackInfoInternal, OnDeleteDeviceIdCallback, DeleteDeviceIdCallbackInfo>(ref data, out onDeleteDeviceIdCallback, out deleteDeviceIdCallbackInfo);
			if (flag)
			{
				onDeleteDeviceIdCallback(ref deleteDeviceIdCallbackInfo);
			}
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00032094 File Offset: 0x00030294
		[MonoPInvokeCallback(typeof(OnLinkAccountCallbackInternal))]
		internal static void OnLinkAccountCallbackInternalImplementation(ref LinkAccountCallbackInfoInternal data)
		{
			OnLinkAccountCallback onLinkAccountCallback;
			LinkAccountCallbackInfo linkAccountCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LinkAccountCallbackInfoInternal, OnLinkAccountCallback, LinkAccountCallbackInfo>(ref data, out onLinkAccountCallback, out linkAccountCallbackInfo);
			if (flag)
			{
				onLinkAccountCallback(ref linkAccountCallbackInfo);
			}
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000320BC File Offset: 0x000302BC
		[MonoPInvokeCallback(typeof(OnLoginCallbackInternal))]
		internal static void OnLoginCallbackInternalImplementation(ref LoginCallbackInfoInternal data)
		{
			OnLoginCallback onLoginCallback;
			LoginCallbackInfo loginCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LoginCallbackInfoInternal, OnLoginCallback, LoginCallbackInfo>(ref data, out onLoginCallback, out loginCallbackInfo);
			if (flag)
			{
				onLoginCallback(ref loginCallbackInfo);
			}
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000320E4 File Offset: 0x000302E4
		[MonoPInvokeCallback(typeof(OnLoginStatusChangedCallbackInternal))]
		internal static void OnLoginStatusChangedCallbackInternalImplementation(ref LoginStatusChangedCallbackInfoInternal data)
		{
			OnLoginStatusChangedCallback onLoginStatusChangedCallback;
			LoginStatusChangedCallbackInfo loginStatusChangedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LoginStatusChangedCallbackInfoInternal, OnLoginStatusChangedCallback, LoginStatusChangedCallbackInfo>(ref data, out onLoginStatusChangedCallback, out loginStatusChangedCallbackInfo);
			if (flag)
			{
				onLoginStatusChangedCallback(ref loginStatusChangedCallbackInfo);
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0003210C File Offset: 0x0003030C
		[MonoPInvokeCallback(typeof(OnQueryExternalAccountMappingsCallbackInternal))]
		internal static void OnQueryExternalAccountMappingsCallbackInternalImplementation(ref QueryExternalAccountMappingsCallbackInfoInternal data)
		{
			OnQueryExternalAccountMappingsCallback onQueryExternalAccountMappingsCallback;
			QueryExternalAccountMappingsCallbackInfo queryExternalAccountMappingsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryExternalAccountMappingsCallbackInfoInternal, OnQueryExternalAccountMappingsCallback, QueryExternalAccountMappingsCallbackInfo>(ref data, out onQueryExternalAccountMappingsCallback, out queryExternalAccountMappingsCallbackInfo);
			if (flag)
			{
				onQueryExternalAccountMappingsCallback(ref queryExternalAccountMappingsCallbackInfo);
			}
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x00032134 File Offset: 0x00030334
		[MonoPInvokeCallback(typeof(OnQueryProductUserIdMappingsCallbackInternal))]
		internal static void OnQueryProductUserIdMappingsCallbackInternalImplementation(ref QueryProductUserIdMappingsCallbackInfoInternal data)
		{
			OnQueryProductUserIdMappingsCallback onQueryProductUserIdMappingsCallback;
			QueryProductUserIdMappingsCallbackInfo queryProductUserIdMappingsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryProductUserIdMappingsCallbackInfoInternal, OnQueryProductUserIdMappingsCallback, QueryProductUserIdMappingsCallbackInfo>(ref data, out onQueryProductUserIdMappingsCallback, out queryProductUserIdMappingsCallbackInfo);
			if (flag)
			{
				onQueryProductUserIdMappingsCallback(ref queryProductUserIdMappingsCallbackInfo);
			}
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0003215C File Offset: 0x0003035C
		[MonoPInvokeCallback(typeof(OnTransferDeviceIdAccountCallbackInternal))]
		internal static void OnTransferDeviceIdAccountCallbackInternalImplementation(ref TransferDeviceIdAccountCallbackInfoInternal data)
		{
			OnTransferDeviceIdAccountCallback onTransferDeviceIdAccountCallback;
			TransferDeviceIdAccountCallbackInfo transferDeviceIdAccountCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<TransferDeviceIdAccountCallbackInfoInternal, OnTransferDeviceIdAccountCallback, TransferDeviceIdAccountCallbackInfo>(ref data, out onTransferDeviceIdAccountCallback, out transferDeviceIdAccountCallbackInfo);
			if (flag)
			{
				onTransferDeviceIdAccountCallback(ref transferDeviceIdAccountCallbackInfo);
			}
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00032184 File Offset: 0x00030384
		[MonoPInvokeCallback(typeof(OnUnlinkAccountCallbackInternal))]
		internal static void OnUnlinkAccountCallbackInternalImplementation(ref UnlinkAccountCallbackInfoInternal data)
		{
			OnUnlinkAccountCallback onUnlinkAccountCallback;
			UnlinkAccountCallbackInfo unlinkAccountCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UnlinkAccountCallbackInfoInternal, OnUnlinkAccountCallback, UnlinkAccountCallbackInfo>(ref data, out onUnlinkAccountCallback, out unlinkAccountCallbackInfo);
			if (flag)
			{
				onUnlinkAccountCallback(ref unlinkAccountCallbackInfo);
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000321AC File Offset: 0x000303AC
		[MonoPInvokeCallback(typeof(OnVerifyIdTokenCallbackInternal))]
		internal static void OnVerifyIdTokenCallbackInternalImplementation(ref VerifyIdTokenCallbackInfoInternal data)
		{
			OnVerifyIdTokenCallback onVerifyIdTokenCallback;
			VerifyIdTokenCallbackInfo verifyIdTokenCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<VerifyIdTokenCallbackInfoInternal, OnVerifyIdTokenCallback, VerifyIdTokenCallbackInfo>(ref data, out onVerifyIdTokenCallback, out verifyIdTokenCallbackInfo);
			if (flag)
			{
				onVerifyIdTokenCallback(ref verifyIdTokenCallbackInfo);
			}
		}

		// Token: 0x04000ECA RID: 3786
		public const int AddnotifyauthexpirationApiLatest = 1;

		// Token: 0x04000ECB RID: 3787
		public const int AddnotifyloginstatuschangedApiLatest = 1;

		// Token: 0x04000ECC RID: 3788
		public const int CopyidtokenApiLatest = 1;

		// Token: 0x04000ECD RID: 3789
		public const int CopyproductuserexternalaccountbyaccountidApiLatest = 1;

		// Token: 0x04000ECE RID: 3790
		public const int CopyproductuserexternalaccountbyaccounttypeApiLatest = 1;

		// Token: 0x04000ECF RID: 3791
		public const int CopyproductuserexternalaccountbyindexApiLatest = 1;

		// Token: 0x04000ED0 RID: 3792
		public const int CopyproductuserinfoApiLatest = 1;

		// Token: 0x04000ED1 RID: 3793
		public const int CreatedeviceidApiLatest = 1;

		// Token: 0x04000ED2 RID: 3794
		public const int CreatedeviceidDevicemodelMaxLength = 64;

		// Token: 0x04000ED3 RID: 3795
		public const int CreateuserApiLatest = 1;

		// Token: 0x04000ED4 RID: 3796
		public const int CredentialsApiLatest = 1;

		// Token: 0x04000ED5 RID: 3797
		public const int DeletedeviceidApiLatest = 1;

		// Token: 0x04000ED6 RID: 3798
		public const int ExternalAccountIdMaxLength = 256;

		// Token: 0x04000ED7 RID: 3799
		public const int ExternalaccountinfoApiLatest = 1;

		// Token: 0x04000ED8 RID: 3800
		public const int GetexternalaccountmappingApiLatest = 1;

		// Token: 0x04000ED9 RID: 3801
		public const int GetexternalaccountmappingsApiLatest = 1;

		// Token: 0x04000EDA RID: 3802
		public const int GetproductuserexternalaccountcountApiLatest = 1;

		// Token: 0x04000EDB RID: 3803
		public const int GetproductuseridmappingApiLatest = 1;

		// Token: 0x04000EDC RID: 3804
		public const int IdtokenApiLatest = 1;

		// Token: 0x04000EDD RID: 3805
		public const int LinkaccountApiLatest = 1;

		// Token: 0x04000EDE RID: 3806
		public const int LoginApiLatest = 2;

		// Token: 0x04000EDF RID: 3807
		public const int OnauthexpirationcallbackApiLatest = 1;

		// Token: 0x04000EE0 RID: 3808
		public const int QueryexternalaccountmappingsApiLatest = 1;

		// Token: 0x04000EE1 RID: 3809
		public const int QueryexternalaccountmappingsMaxAccountIds = 128;

		// Token: 0x04000EE2 RID: 3810
		public const int QueryproductuseridmappingsApiLatest = 2;

		// Token: 0x04000EE3 RID: 3811
		public const int TimeUndefined = -1;

		// Token: 0x04000EE4 RID: 3812
		public const int TransferdeviceidaccountApiLatest = 1;

		// Token: 0x04000EE5 RID: 3813
		public const int UnlinkaccountApiLatest = 1;

		// Token: 0x04000EE6 RID: 3814
		public const int UserlogininfoApiLatest = 1;

		// Token: 0x04000EE7 RID: 3815
		public const int UserlogininfoDisplaynameMaxLength = 32;

		// Token: 0x04000EE8 RID: 3816
		public const int VerifyidtokenApiLatest = 1;
	}
}
