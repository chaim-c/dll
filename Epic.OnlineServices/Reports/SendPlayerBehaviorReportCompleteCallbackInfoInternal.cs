using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000216 RID: 534
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendPlayerBehaviorReportCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<SendPlayerBehaviorReportCompleteCallbackInfo>, ISettable<SendPlayerBehaviorReportCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00016310 File Offset: 0x00014510
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x00016328 File Offset: 0x00014528
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
			set
			{
				this.m_ResultCode = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00016334 File Offset: 0x00014534
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x00016355 File Offset: 0x00014555
		public object ClientData
		{
			get
			{
				object result;
				Helper.Get(this.m_ClientData, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientData);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00016368 File Offset: 0x00014568
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00016380 File Offset: 0x00014580
		public void Set(ref SendPlayerBehaviorReportCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x000163A0 File Offset: 0x000145A0
		public void Set(ref SendPlayerBehaviorReportCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000163E4 File Offset: 0x000145E4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x000163F3 File Offset: 0x000145F3
		public void Get(out SendPlayerBehaviorReportCompleteCallbackInfo output)
		{
			output = default(SendPlayerBehaviorReportCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040006B8 RID: 1720
		private Result m_ResultCode;

		// Token: 0x040006B9 RID: 1721
		private IntPtr m_ClientData;
	}
}
