using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x0200016F RID: 367
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryActivePlayerSanctionsCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryActivePlayerSanctionsCallbackInfo>, ISettable<QueryActivePlayerSanctionsCallbackInfo>, IDisposable
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0000F944 File Offset: 0x0000DB44
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0000F95C File Offset: 0x0000DB5C
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

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0000F968 File Offset: 0x0000DB68
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0000F989 File Offset: 0x0000DB89
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

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0000F99C File Offset: 0x0000DB9C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0000F9D5 File Offset: 0x0000DBD5
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x0000FA09 File Offset: 0x0000DC09
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0000FA19 File Offset: 0x0000DC19
		public void Set(ref QueryActivePlayerSanctionsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0000FA50 File Offset: 0x0000DC50
		public void Set(ref QueryActivePlayerSanctionsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.TargetUserId = other.Value.TargetUserId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0000FABE File Offset: 0x0000DCBE
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000FAE5 File Offset: 0x0000DCE5
		public void Get(out QueryActivePlayerSanctionsCallbackInfo output)
		{
			output = default(QueryActivePlayerSanctionsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040004D4 RID: 1236
		private Result m_ResultCode;

		// Token: 0x040004D5 RID: 1237
		private IntPtr m_ClientData;

		// Token: 0x040004D6 RID: 1238
		private IntPtr m_TargetUserId;

		// Token: 0x040004D7 RID: 1239
		private IntPtr m_LocalUserId;
	}
}
