using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000066 RID: 102
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnShowReportPlayerCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnShowReportPlayerCallbackInfo>, ISettable<OnShowReportPlayerCallbackInfo>, IDisposable
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00006C10 File Offset: 0x00004E10
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00006C28 File Offset: 0x00004E28
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00006C34 File Offset: 0x00004E34
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00006C55 File Offset: 0x00004E55
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00006C68 File Offset: 0x00004E68
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00006C80 File Offset: 0x00004E80
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x00006CA1 File Offset: 0x00004EA1
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00006CB4 File Offset: 0x00004EB4
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00006CD5 File Offset: 0x00004ED5
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00006CE5 File Offset: 0x00004EE5
		public void Set(ref OnShowReportPlayerCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00006D1C File Offset: 0x00004F1C
		public void Set(ref OnShowReportPlayerCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00006D8A File Offset: 0x00004F8A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00006DB1 File Offset: 0x00004FB1
		public void Get(out OnShowReportPlayerCallbackInfo output)
		{
			output = default(OnShowReportPlayerCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400024A RID: 586
		private Result m_ResultCode;

		// Token: 0x0400024B RID: 587
		private IntPtr m_ClientData;

		// Token: 0x0400024C RID: 588
		private IntPtr m_LocalUserId;

		// Token: 0x0400024D RID: 589
		private IntPtr m_TargetUserId;
	}
}
