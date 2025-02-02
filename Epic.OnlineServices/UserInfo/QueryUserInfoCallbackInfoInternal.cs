using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003F RID: 63
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryUserInfoCallbackInfo>, ISettable<QueryUserInfoCallbackInfo>, IDisposable
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000598C File Offset: 0x00003B8C
		// (set) Token: 0x060003BB RID: 955 RVA: 0x000059A4 File Offset: 0x00003BA4
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000059B0 File Offset: 0x00003BB0
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000059D1 File Offset: 0x00003BD1
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000059E4 File Offset: 0x00003BE4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000059FC File Offset: 0x00003BFC
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00005A1D File Offset: 0x00003C1D
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00005A30 File Offset: 0x00003C30
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x00005A51 File Offset: 0x00003C51
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

		// Token: 0x060003C3 RID: 963 RVA: 0x00005A61 File Offset: 0x00003C61
		public void Set(ref QueryUserInfoCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00005A98 File Offset: 0x00003C98
		public void Set(ref QueryUserInfoCallbackInfo? other)
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

		// Token: 0x060003C5 RID: 965 RVA: 0x00005B06 File Offset: 0x00003D06
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00005B2D File Offset: 0x00003D2D
		public void Get(out QueryUserInfoCallbackInfo output)
		{
			output = default(QueryUserInfoCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400018B RID: 395
		private Result m_ResultCode;

		// Token: 0x0400018C RID: 396
		private IntPtr m_ClientData;

		// Token: 0x0400018D RID: 397
		private IntPtr m_LocalUserId;

		// Token: 0x0400018E RID: 398
		private IntPtr m_TargetUserId;
	}
}
