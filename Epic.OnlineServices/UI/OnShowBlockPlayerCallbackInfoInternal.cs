using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000060 RID: 96
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnShowBlockPlayerCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnShowBlockPlayerCallbackInfo>, ISettable<OnShowBlockPlayerCallbackInfo>, IDisposable
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000069C4 File Offset: 0x00004BC4
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x000069DC File Offset: 0x00004BDC
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x000069E8 File Offset: 0x00004BE8
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00006A09 File Offset: 0x00004C09
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00006A1C File Offset: 0x00004C1C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00006A34 File Offset: 0x00004C34
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00006A55 File Offset: 0x00004C55
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00006A68 File Offset: 0x00004C68
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00006A89 File Offset: 0x00004C89
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

		// Token: 0x0600046F RID: 1135 RVA: 0x00006A99 File Offset: 0x00004C99
		public void Set(ref OnShowBlockPlayerCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public void Set(ref OnShowBlockPlayerCallbackInfo? other)
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

		// Token: 0x06000471 RID: 1137 RVA: 0x00006B3E File Offset: 0x00004D3E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00006B65 File Offset: 0x00004D65
		public void Get(out OnShowBlockPlayerCallbackInfo output)
		{
			output = default(OnShowBlockPlayerCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000242 RID: 578
		private Result m_ResultCode;

		// Token: 0x04000243 RID: 579
		private IntPtr m_ClientData;

		// Token: 0x04000244 RID: 580
		private IntPtr m_LocalUserId;

		// Token: 0x04000245 RID: 581
		private IntPtr m_TargetUserId;
	}
}
