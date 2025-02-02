using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000431 RID: 1073
	public sealed class KWSInterface : Handle
	{
		// Token: 0x06001B99 RID: 7065 RVA: 0x00028E29 File Offset: 0x00027029
		public KWSInterface()
		{
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00028E33 File Offset: 0x00027033
		public KWSInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00028E40 File Offset: 0x00027040
		public ulong AddNotifyPermissionsUpdateReceived(ref AddNotifyPermissionsUpdateReceivedOptions options, object clientData, OnPermissionsUpdateReceivedCallback notificationFn)
		{
			AddNotifyPermissionsUpdateReceivedOptionsInternal addNotifyPermissionsUpdateReceivedOptionsInternal = default(AddNotifyPermissionsUpdateReceivedOptionsInternal);
			addNotifyPermissionsUpdateReceivedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnPermissionsUpdateReceivedCallbackInternal onPermissionsUpdateReceivedCallbackInternal = new OnPermissionsUpdateReceivedCallbackInternal(KWSInterface.OnPermissionsUpdateReceivedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onPermissionsUpdateReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_KWS_AddNotifyPermissionsUpdateReceived(base.InnerHandle, ref addNotifyPermissionsUpdateReceivedOptionsInternal, zero, onPermissionsUpdateReceivedCallbackInternal);
			Helper.Dispose<AddNotifyPermissionsUpdateReceivedOptionsInternal>(ref addNotifyPermissionsUpdateReceivedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00028EAC File Offset: 0x000270AC
		public Result CopyPermissionByIndex(ref CopyPermissionByIndexOptions options, out PermissionStatus? outPermission)
		{
			CopyPermissionByIndexOptionsInternal copyPermissionByIndexOptionsInternal = default(CopyPermissionByIndexOptionsInternal);
			copyPermissionByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_KWS_CopyPermissionByIndex(base.InnerHandle, ref copyPermissionByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyPermissionByIndexOptionsInternal>(ref copyPermissionByIndexOptionsInternal);
			Helper.Get<PermissionStatusInternal, PermissionStatus>(zero, out outPermission);
			bool flag = outPermission != null;
			if (flag)
			{
				Bindings.EOS_KWS_PermissionStatus_Release(zero);
			}
			return result;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00028F0C File Offset: 0x0002710C
		public void CreateUser(ref CreateUserOptions options, object clientData, OnCreateUserCallback completionDelegate)
		{
			CreateUserOptionsInternal createUserOptionsInternal = default(CreateUserOptionsInternal);
			createUserOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnCreateUserCallbackInternal onCreateUserCallbackInternal = new OnCreateUserCallbackInternal(KWSInterface.OnCreateUserCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onCreateUserCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_CreateUser(base.InnerHandle, ref createUserOptionsInternal, zero, onCreateUserCallbackInternal);
			Helper.Dispose<CreateUserOptionsInternal>(ref createUserOptionsInternal);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00028F68 File Offset: 0x00027168
		public Result GetPermissionByKey(ref GetPermissionByKeyOptions options, out KWSPermissionStatus outPermission)
		{
			GetPermissionByKeyOptionsInternal getPermissionByKeyOptionsInternal = default(GetPermissionByKeyOptionsInternal);
			getPermissionByKeyOptionsInternal.Set(ref options);
			outPermission = Helper.GetDefault<KWSPermissionStatus>();
			Result result = Bindings.EOS_KWS_GetPermissionByKey(base.InnerHandle, ref getPermissionByKeyOptionsInternal, ref outPermission);
			Helper.Dispose<GetPermissionByKeyOptionsInternal>(ref getPermissionByKeyOptionsInternal);
			return result;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00028FAC File Offset: 0x000271AC
		public int GetPermissionsCount(ref GetPermissionsCountOptions options)
		{
			GetPermissionsCountOptionsInternal getPermissionsCountOptionsInternal = default(GetPermissionsCountOptionsInternal);
			getPermissionsCountOptionsInternal.Set(ref options);
			int result = Bindings.EOS_KWS_GetPermissionsCount(base.InnerHandle, ref getPermissionsCountOptionsInternal);
			Helper.Dispose<GetPermissionsCountOptionsInternal>(ref getPermissionsCountOptionsInternal);
			return result;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00028FE8 File Offset: 0x000271E8
		public void QueryAgeGate(ref QueryAgeGateOptions options, object clientData, OnQueryAgeGateCallback completionDelegate)
		{
			QueryAgeGateOptionsInternal queryAgeGateOptionsInternal = default(QueryAgeGateOptionsInternal);
			queryAgeGateOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryAgeGateCallbackInternal onQueryAgeGateCallbackInternal = new OnQueryAgeGateCallbackInternal(KWSInterface.OnQueryAgeGateCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryAgeGateCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_QueryAgeGate(base.InnerHandle, ref queryAgeGateOptionsInternal, zero, onQueryAgeGateCallbackInternal);
			Helper.Dispose<QueryAgeGateOptionsInternal>(ref queryAgeGateOptionsInternal);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00029044 File Offset: 0x00027244
		public void QueryPermissions(ref QueryPermissionsOptions options, object clientData, OnQueryPermissionsCallback completionDelegate)
		{
			QueryPermissionsOptionsInternal queryPermissionsOptionsInternal = default(QueryPermissionsOptionsInternal);
			queryPermissionsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryPermissionsCallbackInternal onQueryPermissionsCallbackInternal = new OnQueryPermissionsCallbackInternal(KWSInterface.OnQueryPermissionsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryPermissionsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_QueryPermissions(base.InnerHandle, ref queryPermissionsOptionsInternal, zero, onQueryPermissionsCallbackInternal);
			Helper.Dispose<QueryPermissionsOptionsInternal>(ref queryPermissionsOptionsInternal);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0002909E File Offset: 0x0002729E
		public void RemoveNotifyPermissionsUpdateReceived(ulong inId)
		{
			Bindings.EOS_KWS_RemoveNotifyPermissionsUpdateReceived(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000290B8 File Offset: 0x000272B8
		public void RequestPermissions(ref RequestPermissionsOptions options, object clientData, OnRequestPermissionsCallback completionDelegate)
		{
			RequestPermissionsOptionsInternal requestPermissionsOptionsInternal = default(RequestPermissionsOptionsInternal);
			requestPermissionsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnRequestPermissionsCallbackInternal onRequestPermissionsCallbackInternal = new OnRequestPermissionsCallbackInternal(KWSInterface.OnRequestPermissionsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onRequestPermissionsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_RequestPermissions(base.InnerHandle, ref requestPermissionsOptionsInternal, zero, onRequestPermissionsCallbackInternal);
			Helper.Dispose<RequestPermissionsOptionsInternal>(ref requestPermissionsOptionsInternal);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00029114 File Offset: 0x00027314
		public void UpdateParentEmail(ref UpdateParentEmailOptions options, object clientData, OnUpdateParentEmailCallback completionDelegate)
		{
			UpdateParentEmailOptionsInternal updateParentEmailOptionsInternal = default(UpdateParentEmailOptionsInternal);
			updateParentEmailOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUpdateParentEmailCallbackInternal onUpdateParentEmailCallbackInternal = new OnUpdateParentEmailCallbackInternal(KWSInterface.OnUpdateParentEmailCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUpdateParentEmailCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_UpdateParentEmail(base.InnerHandle, ref updateParentEmailOptionsInternal, zero, onUpdateParentEmailCallbackInternal);
			Helper.Dispose<UpdateParentEmailOptionsInternal>(ref updateParentEmailOptionsInternal);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00029170 File Offset: 0x00027370
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

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00029198 File Offset: 0x00027398
		[MonoPInvokeCallback(typeof(OnPermissionsUpdateReceivedCallbackInternal))]
		internal static void OnPermissionsUpdateReceivedCallbackInternalImplementation(ref PermissionsUpdateReceivedCallbackInfoInternal data)
		{
			OnPermissionsUpdateReceivedCallback onPermissionsUpdateReceivedCallback;
			PermissionsUpdateReceivedCallbackInfo permissionsUpdateReceivedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<PermissionsUpdateReceivedCallbackInfoInternal, OnPermissionsUpdateReceivedCallback, PermissionsUpdateReceivedCallbackInfo>(ref data, out onPermissionsUpdateReceivedCallback, out permissionsUpdateReceivedCallbackInfo);
			if (flag)
			{
				onPermissionsUpdateReceivedCallback(ref permissionsUpdateReceivedCallbackInfo);
			}
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000291C0 File Offset: 0x000273C0
		[MonoPInvokeCallback(typeof(OnQueryAgeGateCallbackInternal))]
		internal static void OnQueryAgeGateCallbackInternalImplementation(ref QueryAgeGateCallbackInfoInternal data)
		{
			OnQueryAgeGateCallback onQueryAgeGateCallback;
			QueryAgeGateCallbackInfo queryAgeGateCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryAgeGateCallbackInfoInternal, OnQueryAgeGateCallback, QueryAgeGateCallbackInfo>(ref data, out onQueryAgeGateCallback, out queryAgeGateCallbackInfo);
			if (flag)
			{
				onQueryAgeGateCallback(ref queryAgeGateCallbackInfo);
			}
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000291E8 File Offset: 0x000273E8
		[MonoPInvokeCallback(typeof(OnQueryPermissionsCallbackInternal))]
		internal static void OnQueryPermissionsCallbackInternalImplementation(ref QueryPermissionsCallbackInfoInternal data)
		{
			OnQueryPermissionsCallback onQueryPermissionsCallback;
			QueryPermissionsCallbackInfo queryPermissionsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryPermissionsCallbackInfoInternal, OnQueryPermissionsCallback, QueryPermissionsCallbackInfo>(ref data, out onQueryPermissionsCallback, out queryPermissionsCallbackInfo);
			if (flag)
			{
				onQueryPermissionsCallback(ref queryPermissionsCallbackInfo);
			}
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00029210 File Offset: 0x00027410
		[MonoPInvokeCallback(typeof(OnRequestPermissionsCallbackInternal))]
		internal static void OnRequestPermissionsCallbackInternalImplementation(ref RequestPermissionsCallbackInfoInternal data)
		{
			OnRequestPermissionsCallback onRequestPermissionsCallback;
			RequestPermissionsCallbackInfo requestPermissionsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<RequestPermissionsCallbackInfoInternal, OnRequestPermissionsCallback, RequestPermissionsCallbackInfo>(ref data, out onRequestPermissionsCallback, out requestPermissionsCallbackInfo);
			if (flag)
			{
				onRequestPermissionsCallback(ref requestPermissionsCallbackInfo);
			}
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00029238 File Offset: 0x00027438
		[MonoPInvokeCallback(typeof(OnUpdateParentEmailCallbackInternal))]
		internal static void OnUpdateParentEmailCallbackInternalImplementation(ref UpdateParentEmailCallbackInfoInternal data)
		{
			OnUpdateParentEmailCallback onUpdateParentEmailCallback;
			UpdateParentEmailCallbackInfo updateParentEmailCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UpdateParentEmailCallbackInfoInternal, OnUpdateParentEmailCallback, UpdateParentEmailCallbackInfo>(ref data, out onUpdateParentEmailCallback, out updateParentEmailCallbackInfo);
			if (flag)
			{
				onUpdateParentEmailCallback(ref updateParentEmailCallbackInfo);
			}
		}

		// Token: 0x04000C48 RID: 3144
		public const int AddnotifypermissionsupdatereceivedApiLatest = 1;

		// Token: 0x04000C49 RID: 3145
		public const int CopypermissionbyindexApiLatest = 1;

		// Token: 0x04000C4A RID: 3146
		public const int CreateuserApiLatest = 1;

		// Token: 0x04000C4B RID: 3147
		public const int GetpermissionbykeyApiLatest = 1;

		// Token: 0x04000C4C RID: 3148
		public const int GetpermissionscountApiLatest = 1;

		// Token: 0x04000C4D RID: 3149
		public const int MaxPermissionLength = 32;

		// Token: 0x04000C4E RID: 3150
		public const int MaxPermissions = 16;

		// Token: 0x04000C4F RID: 3151
		public const int PermissionstatusApiLatest = 1;

		// Token: 0x04000C50 RID: 3152
		public const int QueryagegateApiLatest = 1;

		// Token: 0x04000C51 RID: 3153
		public const int QuerypermissionsApiLatest = 1;

		// Token: 0x04000C52 RID: 3154
		public const int RequestpermissionsApiLatest = 1;

		// Token: 0x04000C53 RID: 3155
		public const int UpdateparentemailApiLatest = 1;
	}
}
