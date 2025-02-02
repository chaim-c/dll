using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004D8 RID: 1240
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryEntitlementTokenCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryEntitlementTokenCallbackInfo>, ISettable<QueryEntitlementTokenCallbackInfo>, IDisposable
	{
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0002F390 File Offset: 0x0002D590
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x0002F3A8 File Offset: 0x0002D5A8
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

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0002F3B4 File Offset: 0x0002D5B4
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x0002F3D5 File Offset: 0x0002D5D5
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

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x0002F3E8 File Offset: 0x0002D5E8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x0002F400 File Offset: 0x0002D600
		// (set) Token: 0x06001FDF RID: 8159 RVA: 0x0002F421 File Offset: 0x0002D621
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

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x0002F434 File Offset: 0x0002D634
		// (set) Token: 0x06001FE1 RID: 8161 RVA: 0x0002F455 File Offset: 0x0002D655
		public Utf8String EntitlementToken
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_EntitlementToken, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_EntitlementToken);
			}
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0002F465 File Offset: 0x0002D665
		public void Set(ref QueryEntitlementTokenCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.EntitlementToken = other.EntitlementToken;
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0002F49C File Offset: 0x0002D69C
		public void Set(ref QueryEntitlementTokenCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.EntitlementToken = other.Value.EntitlementToken;
			}
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0002F50A File Offset: 0x0002D70A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_EntitlementToken);
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0002F531 File Offset: 0x0002D731
		public void Get(out QueryEntitlementTokenCallbackInfo output)
		{
			output = default(QueryEntitlementTokenCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000E35 RID: 3637
		private Result m_ResultCode;

		// Token: 0x04000E36 RID: 3638
		private IntPtr m_ClientData;

		// Token: 0x04000E37 RID: 3639
		private IntPtr m_LocalUserId;

		// Token: 0x04000E38 RID: 3640
		private IntPtr m_EntitlementToken;
	}
}
