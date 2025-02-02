using System;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000227 RID: 551
	public sealed class ProgressionSnapshotInterface : Handle
	{
		// Token: 0x06000F53 RID: 3923 RVA: 0x000169FD File Offset: 0x00014BFD
		public ProgressionSnapshotInterface()
		{
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00016A07 File Offset: 0x00014C07
		public ProgressionSnapshotInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00016A14 File Offset: 0x00014C14
		public Result AddProgression(ref AddProgressionOptions options)
		{
			AddProgressionOptionsInternal addProgressionOptionsInternal = default(AddProgressionOptionsInternal);
			addProgressionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_ProgressionSnapshot_AddProgression(base.InnerHandle, ref addProgressionOptionsInternal);
			Helper.Dispose<AddProgressionOptionsInternal>(ref addProgressionOptionsInternal);
			return result;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00016A50 File Offset: 0x00014C50
		public Result BeginSnapshot(ref BeginSnapshotOptions options, out uint outSnapshotId)
		{
			BeginSnapshotOptionsInternal beginSnapshotOptionsInternal = default(BeginSnapshotOptionsInternal);
			beginSnapshotOptionsInternal.Set(ref options);
			outSnapshotId = Helper.GetDefault<uint>();
			Result result = Bindings.EOS_ProgressionSnapshot_BeginSnapshot(base.InnerHandle, ref beginSnapshotOptionsInternal, ref outSnapshotId);
			Helper.Dispose<BeginSnapshotOptionsInternal>(ref beginSnapshotOptionsInternal);
			return result;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00016A94 File Offset: 0x00014C94
		public void DeleteSnapshot(ref DeleteSnapshotOptions options, object clientData, OnDeleteSnapshotCallback completionDelegate)
		{
			DeleteSnapshotOptionsInternal deleteSnapshotOptionsInternal = default(DeleteSnapshotOptionsInternal);
			deleteSnapshotOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnDeleteSnapshotCallbackInternal onDeleteSnapshotCallbackInternal = new OnDeleteSnapshotCallbackInternal(ProgressionSnapshotInterface.OnDeleteSnapshotCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onDeleteSnapshotCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_ProgressionSnapshot_DeleteSnapshot(base.InnerHandle, ref deleteSnapshotOptionsInternal, zero, onDeleteSnapshotCallbackInternal);
			Helper.Dispose<DeleteSnapshotOptionsInternal>(ref deleteSnapshotOptionsInternal);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00016AF0 File Offset: 0x00014CF0
		public Result EndSnapshot(ref EndSnapshotOptions options)
		{
			EndSnapshotOptionsInternal endSnapshotOptionsInternal = default(EndSnapshotOptionsInternal);
			endSnapshotOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_ProgressionSnapshot_EndSnapshot(base.InnerHandle, ref endSnapshotOptionsInternal);
			Helper.Dispose<EndSnapshotOptionsInternal>(ref endSnapshotOptionsInternal);
			return result;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00016B2C File Offset: 0x00014D2C
		public void SubmitSnapshot(ref SubmitSnapshotOptions options, object clientData, OnSubmitSnapshotCallback completionDelegate)
		{
			SubmitSnapshotOptionsInternal submitSnapshotOptionsInternal = default(SubmitSnapshotOptionsInternal);
			submitSnapshotOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSubmitSnapshotCallbackInternal onSubmitSnapshotCallbackInternal = new OnSubmitSnapshotCallbackInternal(ProgressionSnapshotInterface.OnSubmitSnapshotCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onSubmitSnapshotCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_ProgressionSnapshot_SubmitSnapshot(base.InnerHandle, ref submitSnapshotOptionsInternal, zero, onSubmitSnapshotCallbackInternal);
			Helper.Dispose<SubmitSnapshotOptionsInternal>(ref submitSnapshotOptionsInternal);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00016B88 File Offset: 0x00014D88
		[MonoPInvokeCallback(typeof(OnDeleteSnapshotCallbackInternal))]
		internal static void OnDeleteSnapshotCallbackInternalImplementation(ref DeleteSnapshotCallbackInfoInternal data)
		{
			OnDeleteSnapshotCallback onDeleteSnapshotCallback;
			DeleteSnapshotCallbackInfo deleteSnapshotCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<DeleteSnapshotCallbackInfoInternal, OnDeleteSnapshotCallback, DeleteSnapshotCallbackInfo>(ref data, out onDeleteSnapshotCallback, out deleteSnapshotCallbackInfo);
			if (flag)
			{
				onDeleteSnapshotCallback(ref deleteSnapshotCallbackInfo);
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00016BB0 File Offset: 0x00014DB0
		[MonoPInvokeCallback(typeof(OnSubmitSnapshotCallbackInternal))]
		internal static void OnSubmitSnapshotCallbackInternalImplementation(ref SubmitSnapshotCallbackInfoInternal data)
		{
			OnSubmitSnapshotCallback onSubmitSnapshotCallback;
			SubmitSnapshotCallbackInfo submitSnapshotCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SubmitSnapshotCallbackInfoInternal, OnSubmitSnapshotCallback, SubmitSnapshotCallbackInfo>(ref data, out onSubmitSnapshotCallback, out submitSnapshotCallbackInfo);
			if (flag)
			{
				onSubmitSnapshotCallback(ref submitSnapshotCallbackInfo);
			}
		}

		// Token: 0x040006DB RID: 1755
		public const int AddprogressionApiLatest = 1;

		// Token: 0x040006DC RID: 1756
		public const int BeginsnapshotApiLatest = 1;

		// Token: 0x040006DD RID: 1757
		public const int DeletesnapshotApiLatest = 1;

		// Token: 0x040006DE RID: 1758
		public const int EndsnapshotApiLatest = 1;

		// Token: 0x040006DF RID: 1759
		public const int InvalidProgressionsnapshotid = 0;

		// Token: 0x040006E0 RID: 1760
		public const int SubmitsnapshotApiLatest = 1;
	}
}
