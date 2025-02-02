using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000246 RID: 582
	public sealed class PresenceInterface : Handle
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x00017C32 File Offset: 0x00015E32
		public PresenceInterface()
		{
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00017C3C File Offset: 0x00015E3C
		public PresenceInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00017C48 File Offset: 0x00015E48
		public ulong AddNotifyJoinGameAccepted(ref AddNotifyJoinGameAcceptedOptions options, object clientData, OnJoinGameAcceptedCallback notificationFn)
		{
			AddNotifyJoinGameAcceptedOptionsInternal addNotifyJoinGameAcceptedOptionsInternal = default(AddNotifyJoinGameAcceptedOptionsInternal);
			addNotifyJoinGameAcceptedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnJoinGameAcceptedCallbackInternal onJoinGameAcceptedCallbackInternal = new OnJoinGameAcceptedCallbackInternal(PresenceInterface.OnJoinGameAcceptedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onJoinGameAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Presence_AddNotifyJoinGameAccepted(base.InnerHandle, ref addNotifyJoinGameAcceptedOptionsInternal, zero, onJoinGameAcceptedCallbackInternal);
			Helper.Dispose<AddNotifyJoinGameAcceptedOptionsInternal>(ref addNotifyJoinGameAcceptedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00017CB4 File Offset: 0x00015EB4
		public ulong AddNotifyOnPresenceChanged(ref AddNotifyOnPresenceChangedOptions options, object clientData, OnPresenceChangedCallback notificationHandler)
		{
			AddNotifyOnPresenceChangedOptionsInternal addNotifyOnPresenceChangedOptionsInternal = default(AddNotifyOnPresenceChangedOptionsInternal);
			addNotifyOnPresenceChangedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnPresenceChangedCallbackInternal onPresenceChangedCallbackInternal = new OnPresenceChangedCallbackInternal(PresenceInterface.OnPresenceChangedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationHandler, onPresenceChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Presence_AddNotifyOnPresenceChanged(base.InnerHandle, ref addNotifyOnPresenceChangedOptionsInternal, zero, onPresenceChangedCallbackInternal);
			Helper.Dispose<AddNotifyOnPresenceChangedOptionsInternal>(ref addNotifyOnPresenceChangedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00017D20 File Offset: 0x00015F20
		public Result CopyPresence(ref CopyPresenceOptions options, out Info? outPresence)
		{
			CopyPresenceOptionsInternal copyPresenceOptionsInternal = default(CopyPresenceOptionsInternal);
			copyPresenceOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Presence_CopyPresence(base.InnerHandle, ref copyPresenceOptionsInternal, ref zero);
			Helper.Dispose<CopyPresenceOptionsInternal>(ref copyPresenceOptionsInternal);
			Helper.Get<InfoInternal, Info>(zero, out outPresence);
			bool flag = outPresence != null;
			if (flag)
			{
				Bindings.EOS_Presence_Info_Release(zero);
			}
			return result;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00017D80 File Offset: 0x00015F80
		public Result CreatePresenceModification(ref CreatePresenceModificationOptions options, out PresenceModification outPresenceModificationHandle)
		{
			CreatePresenceModificationOptionsInternal createPresenceModificationOptionsInternal = default(CreatePresenceModificationOptionsInternal);
			createPresenceModificationOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Presence_CreatePresenceModification(base.InnerHandle, ref createPresenceModificationOptionsInternal, ref zero);
			Helper.Dispose<CreatePresenceModificationOptionsInternal>(ref createPresenceModificationOptionsInternal);
			Helper.Get<PresenceModification>(zero, out outPresenceModificationHandle);
			return result;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00017DCC File Offset: 0x00015FCC
		public Result GetJoinInfo(ref GetJoinInfoOptions options, out Utf8String outBuffer)
		{
			GetJoinInfoOptionsInternal getJoinInfoOptionsInternal = default(GetJoinInfoOptionsInternal);
			getJoinInfoOptionsInternal.Set(ref options);
			int size = 256;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Presence_GetJoinInfo(base.InnerHandle, ref getJoinInfoOptionsInternal, intPtr, ref size);
			Helper.Dispose<GetJoinInfoOptionsInternal>(ref getJoinInfoOptionsInternal);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00017E28 File Offset: 0x00016028
		public bool HasPresence(ref HasPresenceOptions options)
		{
			HasPresenceOptionsInternal hasPresenceOptionsInternal = default(HasPresenceOptionsInternal);
			hasPresenceOptionsInternal.Set(ref options);
			int from = Bindings.EOS_Presence_HasPresence(base.InnerHandle, ref hasPresenceOptionsInternal);
			Helper.Dispose<HasPresenceOptionsInternal>(ref hasPresenceOptionsInternal);
			bool result;
			Helper.Get(from, out result);
			return result;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00017E6C File Offset: 0x0001606C
		public void QueryPresence(ref QueryPresenceOptions options, object clientData, OnQueryPresenceCompleteCallback completionDelegate)
		{
			QueryPresenceOptionsInternal queryPresenceOptionsInternal = default(QueryPresenceOptionsInternal);
			queryPresenceOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryPresenceCompleteCallbackInternal onQueryPresenceCompleteCallbackInternal = new OnQueryPresenceCompleteCallbackInternal(PresenceInterface.OnQueryPresenceCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryPresenceCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Presence_QueryPresence(base.InnerHandle, ref queryPresenceOptionsInternal, zero, onQueryPresenceCompleteCallbackInternal);
			Helper.Dispose<QueryPresenceOptionsInternal>(ref queryPresenceOptionsInternal);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00017EC6 File Offset: 0x000160C6
		public void RemoveNotifyJoinGameAccepted(ulong inId)
		{
			Bindings.EOS_Presence_RemoveNotifyJoinGameAccepted(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00017EDD File Offset: 0x000160DD
		public void RemoveNotifyOnPresenceChanged(ulong notificationId)
		{
			Bindings.EOS_Presence_RemoveNotifyOnPresenceChanged(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00017EF4 File Offset: 0x000160F4
		public void SetPresence(ref SetPresenceOptions options, object clientData, SetPresenceCompleteCallback completionDelegate)
		{
			SetPresenceOptionsInternal setPresenceOptionsInternal = default(SetPresenceOptionsInternal);
			setPresenceOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			SetPresenceCompleteCallbackInternal setPresenceCompleteCallbackInternal = new SetPresenceCompleteCallbackInternal(PresenceInterface.SetPresenceCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, setPresenceCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Presence_SetPresence(base.InnerHandle, ref setPresenceOptionsInternal, zero, setPresenceCompleteCallbackInternal);
			Helper.Dispose<SetPresenceOptionsInternal>(ref setPresenceOptionsInternal);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00017F50 File Offset: 0x00016150
		[MonoPInvokeCallback(typeof(OnJoinGameAcceptedCallbackInternal))]
		internal static void OnJoinGameAcceptedCallbackInternalImplementation(ref JoinGameAcceptedCallbackInfoInternal data)
		{
			OnJoinGameAcceptedCallback onJoinGameAcceptedCallback;
			JoinGameAcceptedCallbackInfo joinGameAcceptedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<JoinGameAcceptedCallbackInfoInternal, OnJoinGameAcceptedCallback, JoinGameAcceptedCallbackInfo>(ref data, out onJoinGameAcceptedCallback, out joinGameAcceptedCallbackInfo);
			if (flag)
			{
				onJoinGameAcceptedCallback(ref joinGameAcceptedCallbackInfo);
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00017F78 File Offset: 0x00016178
		[MonoPInvokeCallback(typeof(OnPresenceChangedCallbackInternal))]
		internal static void OnPresenceChangedCallbackInternalImplementation(ref PresenceChangedCallbackInfoInternal data)
		{
			OnPresenceChangedCallback onPresenceChangedCallback;
			PresenceChangedCallbackInfo presenceChangedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<PresenceChangedCallbackInfoInternal, OnPresenceChangedCallback, PresenceChangedCallbackInfo>(ref data, out onPresenceChangedCallback, out presenceChangedCallbackInfo);
			if (flag)
			{
				onPresenceChangedCallback(ref presenceChangedCallbackInfo);
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00017FA0 File Offset: 0x000161A0
		[MonoPInvokeCallback(typeof(OnQueryPresenceCompleteCallbackInternal))]
		internal static void OnQueryPresenceCompleteCallbackInternalImplementation(ref QueryPresenceCallbackInfoInternal data)
		{
			OnQueryPresenceCompleteCallback onQueryPresenceCompleteCallback;
			QueryPresenceCallbackInfo queryPresenceCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryPresenceCallbackInfoInternal, OnQueryPresenceCompleteCallback, QueryPresenceCallbackInfo>(ref data, out onQueryPresenceCompleteCallback, out queryPresenceCallbackInfo);
			if (flag)
			{
				onQueryPresenceCompleteCallback(ref queryPresenceCallbackInfo);
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00017FC8 File Offset: 0x000161C8
		[MonoPInvokeCallback(typeof(SetPresenceCompleteCallbackInternal))]
		internal static void SetPresenceCompleteCallbackInternalImplementation(ref SetPresenceCallbackInfoInternal data)
		{
			SetPresenceCompleteCallback setPresenceCompleteCallback;
			SetPresenceCallbackInfo setPresenceCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SetPresenceCallbackInfoInternal, SetPresenceCompleteCallback, SetPresenceCallbackInfo>(ref data, out setPresenceCompleteCallback, out setPresenceCallbackInfo);
			if (flag)
			{
				setPresenceCompleteCallback(ref setPresenceCallbackInfo);
			}
		}

		// Token: 0x04000727 RID: 1831
		public const int AddnotifyjoingameacceptedApiLatest = 2;

		// Token: 0x04000728 RID: 1832
		public const int AddnotifyonpresencechangedApiLatest = 1;

		// Token: 0x04000729 RID: 1833
		public const int CopypresenceApiLatest = 3;

		// Token: 0x0400072A RID: 1834
		public const int CreatepresencemodificationApiLatest = 1;

		// Token: 0x0400072B RID: 1835
		public const int DataMaxKeyLength = 64;

		// Token: 0x0400072C RID: 1836
		public const int DataMaxKeys = 32;

		// Token: 0x0400072D RID: 1837
		public const int DataMaxValueLength = 255;

		// Token: 0x0400072E RID: 1838
		public const int DatarecordApiLatest = 1;

		// Token: 0x0400072F RID: 1839
		public const int DeletedataApiLatest = 1;

		// Token: 0x04000730 RID: 1840
		public const int GetjoininfoApiLatest = 1;

		// Token: 0x04000731 RID: 1841
		public const int HaspresenceApiLatest = 1;

		// Token: 0x04000732 RID: 1842
		public const int InfoApiLatest = 3;

		// Token: 0x04000733 RID: 1843
		public static readonly Utf8String KeyPlatformPresence = "EOS_PlatformPresence";

		// Token: 0x04000734 RID: 1844
		public const int QuerypresenceApiLatest = 1;

		// Token: 0x04000735 RID: 1845
		public const int RichTextMaxValueLength = 255;

		// Token: 0x04000736 RID: 1846
		public const int SetdataApiLatest = 1;

		// Token: 0x04000737 RID: 1847
		public const int SetpresenceApiLatest = 1;

		// Token: 0x04000738 RID: 1848
		public const int SetrawrichtextApiLatest = 1;

		// Token: 0x04000739 RID: 1849
		public const int SetstatusApiLatest = 1;
	}
}
