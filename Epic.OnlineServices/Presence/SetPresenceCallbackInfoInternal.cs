using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000259 RID: 601
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPresenceCallbackInfoInternal : ICallbackInfoInternal, IGettable<SetPresenceCallbackInfo>, ISettable<SetPresenceCallbackInfo>, IDisposable
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00018824 File Offset: 0x00016A24
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x0001883C File Offset: 0x00016A3C
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

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x00018848 File Offset: 0x00016A48
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x00018869 File Offset: 0x00016A69
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

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0001887C File Offset: 0x00016A7C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00018894 File Offset: 0x00016A94
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x000188B5 File Offset: 0x00016AB5
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

		// Token: 0x06001087 RID: 4231 RVA: 0x000188C5 File Offset: 0x00016AC5
		public void Set(ref SetPresenceCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x000188F0 File Offset: 0x00016AF0
		public void Set(ref SetPresenceCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00018949 File Offset: 0x00016B49
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00018964 File Offset: 0x00016B64
		public void Get(out SetPresenceCallbackInfo output)
		{
			output = default(SetPresenceCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000765 RID: 1893
		private Result m_ResultCode;

		// Token: 0x04000766 RID: 1894
		private IntPtr m_ClientData;

		// Token: 0x04000767 RID: 1895
		private IntPtr m_LocalUserId;
	}
}
