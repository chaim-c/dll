using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000572 RID: 1394
	public sealed class AuthInterface : Handle
	{
		// Token: 0x060023B0 RID: 9136 RVA: 0x00034DE0 File Offset: 0x00032FE0
		public AuthInterface()
		{
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00034DEA File Offset: 0x00032FEA
		public AuthInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00034DF8 File Offset: 0x00032FF8
		public ulong AddNotifyLoginStatusChanged(ref AddNotifyLoginStatusChangedOptions options, object clientData, OnLoginStatusChangedCallback notification)
		{
			AddNotifyLoginStatusChangedOptionsInternal addNotifyLoginStatusChangedOptionsInternal = default(AddNotifyLoginStatusChangedOptionsInternal);
			addNotifyLoginStatusChangedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLoginStatusChangedCallbackInternal onLoginStatusChangedCallbackInternal = new OnLoginStatusChangedCallbackInternal(AuthInterface.OnLoginStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notification, onLoginStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Auth_AddNotifyLoginStatusChanged(base.InnerHandle, ref addNotifyLoginStatusChangedOptionsInternal, zero, onLoginStatusChangedCallbackInternal);
			Helper.Dispose<AddNotifyLoginStatusChangedOptionsInternal>(ref addNotifyLoginStatusChangedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00034E64 File Offset: 0x00033064
		public Result CopyIdToken(ref CopyIdTokenOptions options, out IdToken? outIdToken)
		{
			CopyIdTokenOptionsInternal copyIdTokenOptionsInternal = default(CopyIdTokenOptionsInternal);
			copyIdTokenOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Auth_CopyIdToken(base.InnerHandle, ref copyIdTokenOptionsInternal, ref zero);
			Helper.Dispose<CopyIdTokenOptionsInternal>(ref copyIdTokenOptionsInternal);
			Helper.Get<IdTokenInternal, IdToken>(zero, out outIdToken);
			bool flag = outIdToken != null;
			if (flag)
			{
				Bindings.EOS_Auth_IdToken_Release(zero);
			}
			return result;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x00034EC4 File Offset: 0x000330C4
		public Result CopyUserAuthToken(ref CopyUserAuthTokenOptions options, EpicAccountId localUserId, out Token? outUserAuthToken)
		{
			CopyUserAuthTokenOptionsInternal copyUserAuthTokenOptionsInternal = default(CopyUserAuthTokenOptionsInternal);
			copyUserAuthTokenOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Helper.Set(localUserId, ref zero);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Auth_CopyUserAuthToken(base.InnerHandle, ref copyUserAuthTokenOptionsInternal, zero, ref zero2);
			Helper.Dispose<CopyUserAuthTokenOptionsInternal>(ref copyUserAuthTokenOptionsInternal);
			Helper.Get<TokenInternal, Token>(zero2, out outUserAuthToken);
			bool flag = outUserAuthToken != null;
			if (flag)
			{
				Bindings.EOS_Auth_Token_Release(zero2);
			}
			return result;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00034F38 File Offset: 0x00033138
		public void DeletePersistentAuth(ref DeletePersistentAuthOptions options, object clientData, OnDeletePersistentAuthCallback completionDelegate)
		{
			DeletePersistentAuthOptionsInternal deletePersistentAuthOptionsInternal = default(DeletePersistentAuthOptionsInternal);
			deletePersistentAuthOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnDeletePersistentAuthCallbackInternal onDeletePersistentAuthCallbackInternal = new OnDeletePersistentAuthCallbackInternal(AuthInterface.OnDeletePersistentAuthCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onDeletePersistentAuthCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_DeletePersistentAuth(base.InnerHandle, ref deletePersistentAuthOptionsInternal, zero, onDeletePersistentAuthCallbackInternal);
			Helper.Dispose<DeletePersistentAuthOptionsInternal>(ref deletePersistentAuthOptionsInternal);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00034F94 File Offset: 0x00033194
		public EpicAccountId GetLoggedInAccountByIndex(int index)
		{
			IntPtr from = Bindings.EOS_Auth_GetLoggedInAccountByIndex(base.InnerHandle, index);
			EpicAccountId result;
			Helper.Get<EpicAccountId>(from, out result);
			return result;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x00034FC0 File Offset: 0x000331C0
		public int GetLoggedInAccountsCount()
		{
			return Bindings.EOS_Auth_GetLoggedInAccountsCount(base.InnerHandle);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x00034FE0 File Offset: 0x000331E0
		public LoginStatus GetLoginStatus(EpicAccountId localUserId)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(localUserId, ref zero);
			return Bindings.EOS_Auth_GetLoginStatus(base.InnerHandle, zero);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x00035010 File Offset: 0x00033210
		public EpicAccountId GetMergedAccountByIndex(EpicAccountId localUserId, uint index)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(localUserId, ref zero);
			IntPtr from = Bindings.EOS_Auth_GetMergedAccountByIndex(base.InnerHandle, zero, index);
			EpicAccountId result;
			Helper.Get<EpicAccountId>(from, out result);
			return result;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x0003504C File Offset: 0x0003324C
		public uint GetMergedAccountsCount(EpicAccountId localUserId)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(localUserId, ref zero);
			return Bindings.EOS_Auth_GetMergedAccountsCount(base.InnerHandle, zero);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x0003507C File Offset: 0x0003327C
		public Result GetSelectedAccountId(EpicAccountId localUserId, out EpicAccountId outSelectedAccountId)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(localUserId, ref zero);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Auth_GetSelectedAccountId(base.InnerHandle, zero, ref zero2);
			Helper.Get<EpicAccountId>(zero2, out outSelectedAccountId);
			return result;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000350BC File Offset: 0x000332BC
		public void LinkAccount(ref LinkAccountOptions options, object clientData, OnLinkAccountCallback completionDelegate)
		{
			LinkAccountOptionsInternal linkAccountOptionsInternal = default(LinkAccountOptionsInternal);
			linkAccountOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLinkAccountCallbackInternal onLinkAccountCallbackInternal = new OnLinkAccountCallbackInternal(AuthInterface.OnLinkAccountCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onLinkAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_LinkAccount(base.InnerHandle, ref linkAccountOptionsInternal, zero, onLinkAccountCallbackInternal);
			Helper.Dispose<LinkAccountOptionsInternal>(ref linkAccountOptionsInternal);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x00035118 File Offset: 0x00033318
		public void Login(ref LoginOptions options, object clientData, OnLoginCallback completionDelegate)
		{
			LoginOptionsInternal loginOptionsInternal = default(LoginOptionsInternal);
			loginOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLoginCallbackInternal onLoginCallbackInternal = new OnLoginCallbackInternal(AuthInterface.OnLoginCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onLoginCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_Login(base.InnerHandle, ref loginOptionsInternal, zero, onLoginCallbackInternal);
			Helper.Dispose<LoginOptionsInternal>(ref loginOptionsInternal);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x00035174 File Offset: 0x00033374
		public void Logout(ref LogoutOptions options, object clientData, OnLogoutCallback completionDelegate)
		{
			LogoutOptionsInternal logoutOptionsInternal = default(LogoutOptionsInternal);
			logoutOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLogoutCallbackInternal onLogoutCallbackInternal = new OnLogoutCallbackInternal(AuthInterface.OnLogoutCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onLogoutCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_Logout(base.InnerHandle, ref logoutOptionsInternal, zero, onLogoutCallbackInternal);
			Helper.Dispose<LogoutOptionsInternal>(ref logoutOptionsInternal);
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000351D0 File Offset: 0x000333D0
		public void QueryIdToken(ref QueryIdTokenOptions options, object clientData, OnQueryIdTokenCallback completionDelegate)
		{
			QueryIdTokenOptionsInternal queryIdTokenOptionsInternal = default(QueryIdTokenOptionsInternal);
			queryIdTokenOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryIdTokenCallbackInternal onQueryIdTokenCallbackInternal = new OnQueryIdTokenCallbackInternal(AuthInterface.OnQueryIdTokenCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryIdTokenCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_QueryIdToken(base.InnerHandle, ref queryIdTokenOptionsInternal, zero, onQueryIdTokenCallbackInternal);
			Helper.Dispose<QueryIdTokenOptionsInternal>(ref queryIdTokenOptionsInternal);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x0003522A File Offset: 0x0003342A
		public void RemoveNotifyLoginStatusChanged(ulong inId)
		{
			Bindings.EOS_Auth_RemoveNotifyLoginStatusChanged(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x00035244 File Offset: 0x00033444
		public void VerifyIdToken(ref VerifyIdTokenOptions options, object clientData, OnVerifyIdTokenCallback completionDelegate)
		{
			VerifyIdTokenOptionsInternal verifyIdTokenOptionsInternal = default(VerifyIdTokenOptionsInternal);
			verifyIdTokenOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnVerifyIdTokenCallbackInternal onVerifyIdTokenCallbackInternal = new OnVerifyIdTokenCallbackInternal(AuthInterface.OnVerifyIdTokenCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onVerifyIdTokenCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_VerifyIdToken(base.InnerHandle, ref verifyIdTokenOptionsInternal, zero, onVerifyIdTokenCallbackInternal);
			Helper.Dispose<VerifyIdTokenOptionsInternal>(ref verifyIdTokenOptionsInternal);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000352A0 File Offset: 0x000334A0
		public void VerifyUserAuth(ref VerifyUserAuthOptions options, object clientData, OnVerifyUserAuthCallback completionDelegate)
		{
			VerifyUserAuthOptionsInternal verifyUserAuthOptionsInternal = default(VerifyUserAuthOptionsInternal);
			verifyUserAuthOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnVerifyUserAuthCallbackInternal onVerifyUserAuthCallbackInternal = new OnVerifyUserAuthCallbackInternal(AuthInterface.OnVerifyUserAuthCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onVerifyUserAuthCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_VerifyUserAuth(base.InnerHandle, ref verifyUserAuthOptionsInternal, zero, onVerifyUserAuthCallbackInternal);
			Helper.Dispose<VerifyUserAuthOptionsInternal>(ref verifyUserAuthOptionsInternal);
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000352FC File Offset: 0x000334FC
		[MonoPInvokeCallback(typeof(OnDeletePersistentAuthCallbackInternal))]
		internal static void OnDeletePersistentAuthCallbackInternalImplementation(ref DeletePersistentAuthCallbackInfoInternal data)
		{
			OnDeletePersistentAuthCallback onDeletePersistentAuthCallback;
			DeletePersistentAuthCallbackInfo deletePersistentAuthCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<DeletePersistentAuthCallbackInfoInternal, OnDeletePersistentAuthCallback, DeletePersistentAuthCallbackInfo>(ref data, out onDeletePersistentAuthCallback, out deletePersistentAuthCallbackInfo);
			if (flag)
			{
				onDeletePersistentAuthCallback(ref deletePersistentAuthCallbackInfo);
			}
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x00035324 File Offset: 0x00033524
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

		// Token: 0x060023C5 RID: 9157 RVA: 0x0003534C File Offset: 0x0003354C
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

		// Token: 0x060023C6 RID: 9158 RVA: 0x00035374 File Offset: 0x00033574
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

		// Token: 0x060023C7 RID: 9159 RVA: 0x0003539C File Offset: 0x0003359C
		[MonoPInvokeCallback(typeof(OnLogoutCallbackInternal))]
		internal static void OnLogoutCallbackInternalImplementation(ref LogoutCallbackInfoInternal data)
		{
			OnLogoutCallback onLogoutCallback;
			LogoutCallbackInfo logoutCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LogoutCallbackInfoInternal, OnLogoutCallback, LogoutCallbackInfo>(ref data, out onLogoutCallback, out logoutCallbackInfo);
			if (flag)
			{
				onLogoutCallback(ref logoutCallbackInfo);
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000353C4 File Offset: 0x000335C4
		[MonoPInvokeCallback(typeof(OnQueryIdTokenCallbackInternal))]
		internal static void OnQueryIdTokenCallbackInternalImplementation(ref QueryIdTokenCallbackInfoInternal data)
		{
			OnQueryIdTokenCallback onQueryIdTokenCallback;
			QueryIdTokenCallbackInfo queryIdTokenCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryIdTokenCallbackInfoInternal, OnQueryIdTokenCallback, QueryIdTokenCallbackInfo>(ref data, out onQueryIdTokenCallback, out queryIdTokenCallbackInfo);
			if (flag)
			{
				onQueryIdTokenCallback(ref queryIdTokenCallbackInfo);
			}
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000353EC File Offset: 0x000335EC
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

		// Token: 0x060023CA RID: 9162 RVA: 0x00035414 File Offset: 0x00033614
		[MonoPInvokeCallback(typeof(OnVerifyUserAuthCallbackInternal))]
		internal static void OnVerifyUserAuthCallbackInternalImplementation(ref VerifyUserAuthCallbackInfoInternal data)
		{
			OnVerifyUserAuthCallback onVerifyUserAuthCallback;
			VerifyUserAuthCallbackInfo verifyUserAuthCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<VerifyUserAuthCallbackInfoInternal, OnVerifyUserAuthCallback, VerifyUserAuthCallbackInfo>(ref data, out onVerifyUserAuthCallback, out verifyUserAuthCallbackInfo);
			if (flag)
			{
				onVerifyUserAuthCallback(ref verifyUserAuthCallbackInfo);
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x0003543C File Offset: 0x0003363C
		public void Login(ref IOSLoginOptions options, object clientData, OnLoginCallback completionDelegate)
		{
			IOSLoginOptionsInternal iosloginOptionsInternal = default(IOSLoginOptionsInternal);
			iosloginOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLoginCallbackInternal onLoginCallbackInternal = new OnLoginCallbackInternal(AuthInterface.OnLoginCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onLoginCallbackInternal, Array.Empty<Delegate>());
			IOSBindings.EOS_Auth_Login(base.InnerHandle, ref iosloginOptionsInternal, zero, onLoginCallbackInternal);
			Helper.Dispose<IOSLoginOptionsInternal>(ref iosloginOptionsInternal);
		}

		// Token: 0x04000FAD RID: 4013
		public const int AccountfeaturerestrictedinfoApiLatest = 1;

		// Token: 0x04000FAE RID: 4014
		public const int AddnotifyloginstatuschangedApiLatest = 1;

		// Token: 0x04000FAF RID: 4015
		public const int CopyidtokenApiLatest = 1;

		// Token: 0x04000FB0 RID: 4016
		public const int CopyuserauthtokenApiLatest = 1;

		// Token: 0x04000FB1 RID: 4017
		public const int CredentialsApiLatest = 3;

		// Token: 0x04000FB2 RID: 4018
		public const int DeletepersistentauthApiLatest = 2;

		// Token: 0x04000FB3 RID: 4019
		public const int IdtokenApiLatest = 1;

		// Token: 0x04000FB4 RID: 4020
		public const int LinkaccountApiLatest = 1;

		// Token: 0x04000FB5 RID: 4021
		public const int LoginApiLatest = 2;

		// Token: 0x04000FB6 RID: 4022
		public const int LogoutApiLatest = 1;

		// Token: 0x04000FB7 RID: 4023
		public const int PingrantinfoApiLatest = 2;

		// Token: 0x04000FB8 RID: 4024
		public const int QueryidtokenApiLatest = 1;

		// Token: 0x04000FB9 RID: 4025
		public const int TokenApiLatest = 2;

		// Token: 0x04000FBA RID: 4026
		public const int VerifyidtokenApiLatest = 1;

		// Token: 0x04000FBB RID: 4027
		public const int VerifyuserauthApiLatest = 1;

		// Token: 0x04000FBC RID: 4028
		public const int IosCredentialssystemauthcredentialsoptionsApiLatest = 1;
	}
}
