using System;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000214 RID: 532
	public sealed class ReportsInterface : Handle
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x00016214 File Offset: 0x00014414
		public ReportsInterface()
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0001621E File Offset: 0x0001441E
		public ReportsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0001622C File Offset: 0x0001442C
		public void SendPlayerBehaviorReport(ref SendPlayerBehaviorReportOptions options, object clientData, OnSendPlayerBehaviorReportCompleteCallback completionDelegate)
		{
			SendPlayerBehaviorReportOptionsInternal sendPlayerBehaviorReportOptionsInternal = default(SendPlayerBehaviorReportOptionsInternal);
			sendPlayerBehaviorReportOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSendPlayerBehaviorReportCompleteCallbackInternal onSendPlayerBehaviorReportCompleteCallbackInternal = new OnSendPlayerBehaviorReportCompleteCallbackInternal(ReportsInterface.OnSendPlayerBehaviorReportCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onSendPlayerBehaviorReportCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Reports_SendPlayerBehaviorReport(base.InnerHandle, ref sendPlayerBehaviorReportOptionsInternal, zero, onSendPlayerBehaviorReportCompleteCallbackInternal);
			Helper.Dispose<SendPlayerBehaviorReportOptionsInternal>(ref sendPlayerBehaviorReportOptionsInternal);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00016288 File Offset: 0x00014488
		[MonoPInvokeCallback(typeof(OnSendPlayerBehaviorReportCompleteCallbackInternal))]
		internal static void OnSendPlayerBehaviorReportCompleteCallbackInternalImplementation(ref SendPlayerBehaviorReportCompleteCallbackInfoInternal data)
		{
			OnSendPlayerBehaviorReportCompleteCallback onSendPlayerBehaviorReportCompleteCallback;
			SendPlayerBehaviorReportCompleteCallbackInfo sendPlayerBehaviorReportCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SendPlayerBehaviorReportCompleteCallbackInfoInternal, OnSendPlayerBehaviorReportCompleteCallback, SendPlayerBehaviorReportCompleteCallbackInfo>(ref data, out onSendPlayerBehaviorReportCompleteCallback, out sendPlayerBehaviorReportCompleteCallbackInfo);
			if (flag)
			{
				onSendPlayerBehaviorReportCompleteCallback(ref sendPlayerBehaviorReportCompleteCallbackInfo);
			}
		}

		// Token: 0x040006B3 RID: 1715
		public const int ReportcontextMaxLength = 4096;

		// Token: 0x040006B4 RID: 1716
		public const int ReportmessageMaxLength = 512;

		// Token: 0x040006B5 RID: 1717
		public const int SendplayerbehaviorreportApiLatest = 2;
	}
}
