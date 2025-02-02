using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E4 RID: 1252
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipTokenCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryOwnershipTokenCallbackInfo>, ISettable<QueryOwnershipTokenCallbackInfo>, IDisposable
	{
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0002FCCC File Offset: 0x0002DECC
		// (set) Token: 0x06002039 RID: 8249 RVA: 0x0002FCE4 File Offset: 0x0002DEE4
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

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0002FCF0 File Offset: 0x0002DEF0
		// (set) Token: 0x0600203B RID: 8251 RVA: 0x0002FD11 File Offset: 0x0002DF11
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

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x0002FD24 File Offset: 0x0002DF24
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x0002FD3C File Offset: 0x0002DF3C
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x0002FD5D File Offset: 0x0002DF5D
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

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x0002FD70 File Offset: 0x0002DF70
		// (set) Token: 0x06002040 RID: 8256 RVA: 0x0002FD91 File Offset: 0x0002DF91
		public Utf8String OwnershipToken
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_OwnershipToken, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_OwnershipToken);
			}
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0002FDA1 File Offset: 0x0002DFA1
		public void Set(ref QueryOwnershipTokenCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.OwnershipToken = other.OwnershipToken;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0002FDD8 File Offset: 0x0002DFD8
		public void Set(ref QueryOwnershipTokenCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.OwnershipToken = other.Value.OwnershipToken;
			}
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0002FE46 File Offset: 0x0002E046
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_OwnershipToken);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0002FE6D File Offset: 0x0002E06D
		public void Get(out QueryOwnershipTokenCallbackInfo output)
		{
			output = default(QueryOwnershipTokenCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000E5F RID: 3679
		private Result m_ResultCode;

		// Token: 0x04000E60 RID: 3680
		private IntPtr m_ClientData;

		// Token: 0x04000E61 RID: 3681
		private IntPtr m_LocalUserId;

		// Token: 0x04000E62 RID: 3682
		private IntPtr m_OwnershipToken;
	}
}
