using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x0200020C RID: 524
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetParticipantHardMuteCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<SetParticipantHardMuteCompleteCallbackInfo>, ISettable<SetParticipantHardMuteCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00015EC8 File Offset: 0x000140C8
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00015EE0 File Offset: 0x000140E0
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

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00015EEC File Offset: 0x000140EC
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x00015F0D File Offset: 0x0001410D
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

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00015F20 File Offset: 0x00014120
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00015F38 File Offset: 0x00014138
		public void Set(ref SetParticipantHardMuteCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00015F58 File Offset: 0x00014158
		public void Set(ref SetParticipantHardMuteCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00015F9C File Offset: 0x0001419C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00015FAB File Offset: 0x000141AB
		public void Get(out SetParticipantHardMuteCompleteCallbackInfo output)
		{
			output = default(SetParticipantHardMuteCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400069C RID: 1692
		private Result m_ResultCode;

		// Token: 0x0400069D RID: 1693
		private IntPtr m_ClientData;
	}
}
