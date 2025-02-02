using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E0 RID: 1248
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryOwnershipCallbackInfo>, ISettable<QueryOwnershipCallbackInfo>, IDisposable
	{
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x0002F94C File Offset: 0x0002DB4C
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x0002F964 File Offset: 0x0002DB64
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

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x0002F970 File Offset: 0x0002DB70
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x0002F991 File Offset: 0x0002DB91
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

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x0002F9A4 File Offset: 0x0002DBA4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0002F9BC File Offset: 0x0002DBBC
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x0002F9DD File Offset: 0x0002DBDD
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

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0002F9F0 File Offset: 0x0002DBF0
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x0002FA17 File Offset: 0x0002DC17
		public ItemOwnership[] ItemOwnership
		{
			get
			{
				ItemOwnership[] result;
				Helper.Get<ItemOwnershipInternal, ItemOwnership>(this.m_ItemOwnership, out result, this.m_ItemOwnershipCount);
				return result;
			}
			set
			{
				Helper.Set<ItemOwnership, ItemOwnershipInternal>(ref value, ref this.m_ItemOwnership, out this.m_ItemOwnershipCount);
			}
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0002FA2E File Offset: 0x0002DC2E
		public void Set(ref QueryOwnershipCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.ItemOwnership = other.ItemOwnership;
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0002FA68 File Offset: 0x0002DC68
		public void Set(ref QueryOwnershipCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.ItemOwnership = other.Value.ItemOwnership;
			}
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0002FAD6 File Offset: 0x0002DCD6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ItemOwnership);
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0002FAFD File Offset: 0x0002DCFD
		public void Get(out QueryOwnershipCallbackInfo output)
		{
			output = default(QueryOwnershipCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000E4E RID: 3662
		private Result m_ResultCode;

		// Token: 0x04000E4F RID: 3663
		private IntPtr m_ClientData;

		// Token: 0x04000E50 RID: 3664
		private IntPtr m_LocalUserId;

		// Token: 0x04000E51 RID: 3665
		private IntPtr m_ItemOwnership;

		// Token: 0x04000E52 RID: 3666
		private uint m_ItemOwnershipCount;
	}
}
