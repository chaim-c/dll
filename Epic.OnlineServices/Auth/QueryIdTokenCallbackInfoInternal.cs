using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A4 RID: 1444
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryIdTokenCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryIdTokenCallbackInfo>, ISettable<QueryIdTokenCallbackInfo>, IDisposable
	{
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x00036AE8 File Offset: 0x00034CE8
		// (set) Token: 0x060024F3 RID: 9459 RVA: 0x00036B00 File Offset: 0x00034D00
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

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x00036B0C File Offset: 0x00034D0C
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x00036B2D File Offset: 0x00034D2D
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

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x00036B40 File Offset: 0x00034D40
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x00036B58 File Offset: 0x00034D58
		// (set) Token: 0x060024F8 RID: 9464 RVA: 0x00036B79 File Offset: 0x00034D79
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

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x00036B8C File Offset: 0x00034D8C
		// (set) Token: 0x060024FA RID: 9466 RVA: 0x00036BAD File Offset: 0x00034DAD
		public EpicAccountId TargetAccountId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_TargetAccountId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetAccountId);
			}
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x00036BBD File Offset: 0x00034DBD
		public void Set(ref QueryIdTokenCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetAccountId = other.TargetAccountId;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00036BF4 File Offset: 0x00034DF4
		public void Set(ref QueryIdTokenCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetAccountId = other.Value.TargetAccountId;
			}
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00036C62 File Offset: 0x00034E62
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetAccountId);
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00036C89 File Offset: 0x00034E89
		public void Get(out QueryIdTokenCallbackInfo output)
		{
			output = default(QueryIdTokenCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04001031 RID: 4145
		private Result m_ResultCode;

		// Token: 0x04001032 RID: 4146
		private IntPtr m_ClientData;

		// Token: 0x04001033 RID: 4147
		private IntPtr m_LocalUserId;

		// Token: 0x04001034 RID: 4148
		private IntPtr m_TargetAccountId;
	}
}
