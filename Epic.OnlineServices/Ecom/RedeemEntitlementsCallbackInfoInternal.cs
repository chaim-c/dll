using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E8 RID: 1256
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RedeemEntitlementsCallbackInfoInternal : ICallbackInfoInternal, IGettable<RedeemEntitlementsCallbackInfo>, ISettable<RedeemEntitlementsCallbackInfo>, IDisposable
	{
		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x0003003C File Offset: 0x0002E23C
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x00030054 File Offset: 0x0002E254
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

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00030060 File Offset: 0x0002E260
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x00030081 File Offset: 0x0002E281
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

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00030094 File Offset: 0x0002E294
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x000300AC File Offset: 0x0002E2AC
		// (set) Token: 0x06002061 RID: 8289 RVA: 0x000300CD File Offset: 0x0002E2CD
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

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x000300E0 File Offset: 0x0002E2E0
		// (set) Token: 0x06002063 RID: 8291 RVA: 0x000300F8 File Offset: 0x0002E2F8
		public uint RedeemedEntitlementIdsCount
		{
			get
			{
				return this.m_RedeemedEntitlementIdsCount;
			}
			set
			{
				this.m_RedeemedEntitlementIdsCount = value;
			}
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00030102 File Offset: 0x0002E302
		public void Set(ref RedeemEntitlementsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RedeemedEntitlementIdsCount = other.RedeemedEntitlementIdsCount;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0003013C File Offset: 0x0002E33C
		public void Set(ref RedeemEntitlementsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RedeemedEntitlementIdsCount = other.Value.RedeemedEntitlementIdsCount;
			}
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000301AA File Offset: 0x0002E3AA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000301C5 File Offset: 0x0002E3C5
		public void Get(out RedeemEntitlementsCallbackInfo output)
		{
			output = default(RedeemEntitlementsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000E6F RID: 3695
		private Result m_ResultCode;

		// Token: 0x04000E70 RID: 3696
		private IntPtr m_ClientData;

		// Token: 0x04000E71 RID: 3697
		private IntPtr m_LocalUserId;

		// Token: 0x04000E72 RID: 3698
		private uint m_RedeemedEntitlementIdsCount;
	}
}
