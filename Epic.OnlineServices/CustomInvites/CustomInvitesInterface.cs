using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004F8 RID: 1272
	public sealed class CustomInvitesInterface : Handle
	{
		// Token: 0x060020A4 RID: 8356 RVA: 0x000307D8 File Offset: 0x0002E9D8
		public CustomInvitesInterface()
		{
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000307E2 File Offset: 0x0002E9E2
		public CustomInvitesInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000307F0 File Offset: 0x0002E9F0
		public ulong AddNotifyCustomInviteAccepted(ref AddNotifyCustomInviteAcceptedOptions options, object clientData, OnCustomInviteAcceptedCallback notificationFn)
		{
			AddNotifyCustomInviteAcceptedOptionsInternal addNotifyCustomInviteAcceptedOptionsInternal = default(AddNotifyCustomInviteAcceptedOptionsInternal);
			addNotifyCustomInviteAcceptedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnCustomInviteAcceptedCallbackInternal onCustomInviteAcceptedCallbackInternal = new OnCustomInviteAcceptedCallbackInternal(CustomInvitesInterface.OnCustomInviteAcceptedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onCustomInviteAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_CustomInvites_AddNotifyCustomInviteAccepted(base.InnerHandle, ref addNotifyCustomInviteAcceptedOptionsInternal, zero, onCustomInviteAcceptedCallbackInternal);
			Helper.Dispose<AddNotifyCustomInviteAcceptedOptionsInternal>(ref addNotifyCustomInviteAcceptedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x0003085C File Offset: 0x0002EA5C
		public ulong AddNotifyCustomInviteReceived(ref AddNotifyCustomInviteReceivedOptions options, object clientData, OnCustomInviteReceivedCallback notificationFn)
		{
			AddNotifyCustomInviteReceivedOptionsInternal addNotifyCustomInviteReceivedOptionsInternal = default(AddNotifyCustomInviteReceivedOptionsInternal);
			addNotifyCustomInviteReceivedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnCustomInviteReceivedCallbackInternal onCustomInviteReceivedCallbackInternal = new OnCustomInviteReceivedCallbackInternal(CustomInvitesInterface.OnCustomInviteReceivedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onCustomInviteReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_CustomInvites_AddNotifyCustomInviteReceived(base.InnerHandle, ref addNotifyCustomInviteReceivedOptionsInternal, zero, onCustomInviteReceivedCallbackInternal);
			Helper.Dispose<AddNotifyCustomInviteReceivedOptionsInternal>(ref addNotifyCustomInviteReceivedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000308C8 File Offset: 0x0002EAC8
		public ulong AddNotifyCustomInviteRejected(ref AddNotifyCustomInviteRejectedOptions options, object clientData, OnCustomInviteRejectedCallback notificationFn)
		{
			AddNotifyCustomInviteRejectedOptionsInternal addNotifyCustomInviteRejectedOptionsInternal = default(AddNotifyCustomInviteRejectedOptionsInternal);
			addNotifyCustomInviteRejectedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnCustomInviteRejectedCallbackInternal onCustomInviteRejectedCallbackInternal = new OnCustomInviteRejectedCallbackInternal(CustomInvitesInterface.OnCustomInviteRejectedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onCustomInviteRejectedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_CustomInvites_AddNotifyCustomInviteRejected(base.InnerHandle, ref addNotifyCustomInviteRejectedOptionsInternal, zero, onCustomInviteRejectedCallbackInternal);
			Helper.Dispose<AddNotifyCustomInviteRejectedOptionsInternal>(ref addNotifyCustomInviteRejectedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00030934 File Offset: 0x0002EB34
		public Result FinalizeInvite(ref FinalizeInviteOptions options)
		{
			FinalizeInviteOptionsInternal finalizeInviteOptionsInternal = default(FinalizeInviteOptionsInternal);
			finalizeInviteOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_CustomInvites_FinalizeInvite(base.InnerHandle, ref finalizeInviteOptionsInternal);
			Helper.Dispose<FinalizeInviteOptionsInternal>(ref finalizeInviteOptionsInternal);
			return result;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0003096E File Offset: 0x0002EB6E
		public void RemoveNotifyCustomInviteAccepted(ulong inId)
		{
			Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteAccepted(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00030985 File Offset: 0x0002EB85
		public void RemoveNotifyCustomInviteReceived(ulong inId)
		{
			Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteReceived(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x0003099C File Offset: 0x0002EB9C
		public void RemoveNotifyCustomInviteRejected(ulong inId)
		{
			Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteRejected(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000309B4 File Offset: 0x0002EBB4
		public void SendCustomInvite(ref SendCustomInviteOptions options, object clientData, OnSendCustomInviteCallback completionDelegate)
		{
			SendCustomInviteOptionsInternal sendCustomInviteOptionsInternal = default(SendCustomInviteOptionsInternal);
			sendCustomInviteOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSendCustomInviteCallbackInternal onSendCustomInviteCallbackInternal = new OnSendCustomInviteCallbackInternal(CustomInvitesInterface.OnSendCustomInviteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onSendCustomInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_CustomInvites_SendCustomInvite(base.InnerHandle, ref sendCustomInviteOptionsInternal, zero, onSendCustomInviteCallbackInternal);
			Helper.Dispose<SendCustomInviteOptionsInternal>(ref sendCustomInviteOptionsInternal);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00030A10 File Offset: 0x0002EC10
		public Result SetCustomInvite(ref SetCustomInviteOptions options)
		{
			SetCustomInviteOptionsInternal setCustomInviteOptionsInternal = default(SetCustomInviteOptionsInternal);
			setCustomInviteOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_CustomInvites_SetCustomInvite(base.InnerHandle, ref setCustomInviteOptionsInternal);
			Helper.Dispose<SetCustomInviteOptionsInternal>(ref setCustomInviteOptionsInternal);
			return result;
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00030A4C File Offset: 0x0002EC4C
		[MonoPInvokeCallback(typeof(OnCustomInviteAcceptedCallbackInternal))]
		internal static void OnCustomInviteAcceptedCallbackInternalImplementation(ref OnCustomInviteAcceptedCallbackInfoInternal data)
		{
			OnCustomInviteAcceptedCallback onCustomInviteAcceptedCallback;
			OnCustomInviteAcceptedCallbackInfo onCustomInviteAcceptedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnCustomInviteAcceptedCallbackInfoInternal, OnCustomInviteAcceptedCallback, OnCustomInviteAcceptedCallbackInfo>(ref data, out onCustomInviteAcceptedCallback, out onCustomInviteAcceptedCallbackInfo);
			if (flag)
			{
				onCustomInviteAcceptedCallback(ref onCustomInviteAcceptedCallbackInfo);
			}
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00030A74 File Offset: 0x0002EC74
		[MonoPInvokeCallback(typeof(OnCustomInviteReceivedCallbackInternal))]
		internal static void OnCustomInviteReceivedCallbackInternalImplementation(ref OnCustomInviteReceivedCallbackInfoInternal data)
		{
			OnCustomInviteReceivedCallback onCustomInviteReceivedCallback;
			OnCustomInviteReceivedCallbackInfo onCustomInviteReceivedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnCustomInviteReceivedCallbackInfoInternal, OnCustomInviteReceivedCallback, OnCustomInviteReceivedCallbackInfo>(ref data, out onCustomInviteReceivedCallback, out onCustomInviteReceivedCallbackInfo);
			if (flag)
			{
				onCustomInviteReceivedCallback(ref onCustomInviteReceivedCallbackInfo);
			}
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00030A9C File Offset: 0x0002EC9C
		[MonoPInvokeCallback(typeof(OnCustomInviteRejectedCallbackInternal))]
		internal static void OnCustomInviteRejectedCallbackInternalImplementation(ref CustomInviteRejectedCallbackInfoInternal data)
		{
			OnCustomInviteRejectedCallback onCustomInviteRejectedCallback;
			CustomInviteRejectedCallbackInfo customInviteRejectedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<CustomInviteRejectedCallbackInfoInternal, OnCustomInviteRejectedCallback, CustomInviteRejectedCallbackInfo>(ref data, out onCustomInviteRejectedCallback, out customInviteRejectedCallbackInfo);
			if (flag)
			{
				onCustomInviteRejectedCallback(ref customInviteRejectedCallbackInfo);
			}
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x00030AC4 File Offset: 0x0002ECC4
		[MonoPInvokeCallback(typeof(OnSendCustomInviteCallbackInternal))]
		internal static void OnSendCustomInviteCallbackInternalImplementation(ref SendCustomInviteCallbackInfoInternal data)
		{
			OnSendCustomInviteCallback onSendCustomInviteCallback;
			SendCustomInviteCallbackInfo sendCustomInviteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SendCustomInviteCallbackInfoInternal, OnSendCustomInviteCallback, SendCustomInviteCallbackInfo>(ref data, out onSendCustomInviteCallback, out sendCustomInviteCallbackInfo);
			if (flag)
			{
				onSendCustomInviteCallback(ref sendCustomInviteCallbackInfo);
			}
		}

		// Token: 0x04000E8C RID: 3724
		public const int AddnotifycustominviteacceptedApiLatest = 1;

		// Token: 0x04000E8D RID: 3725
		public const int AddnotifycustominvitereceivedApiLatest = 1;

		// Token: 0x04000E8E RID: 3726
		public const int AddnotifycustominviterejectedApiLatest = 1;

		// Token: 0x04000E8F RID: 3727
		public const int FinalizeinviteApiLatest = 1;

		// Token: 0x04000E90 RID: 3728
		public const int MaxPayloadLength = 500;

		// Token: 0x04000E91 RID: 3729
		public const int SendcustominviteApiLatest = 1;

		// Token: 0x04000E92 RID: 3730
		public const int SetcustominviteApiLatest = 1;
	}
}
