using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000479 RID: 1145
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFriendsCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryFriendsCallbackInfo>, ISettable<QueryFriendsCallbackInfo>, IDisposable
	{
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0002B128 File Offset: 0x00029328
		// (set) Token: 0x06001D37 RID: 7479 RVA: 0x0002B140 File Offset: 0x00029340
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

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0002B14C File Offset: 0x0002934C
		// (set) Token: 0x06001D39 RID: 7481 RVA: 0x0002B16D File Offset: 0x0002936D
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

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0002B180 File Offset: 0x00029380
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0002B198 File Offset: 0x00029398
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x0002B1B9 File Offset: 0x000293B9
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

		// Token: 0x06001D3D RID: 7485 RVA: 0x0002B1C9 File Offset: 0x000293C9
		public void Set(ref QueryFriendsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0002B1F4 File Offset: 0x000293F4
		public void Set(ref QueryFriendsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0002B24D File Offset: 0x0002944D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0002B268 File Offset: 0x00029468
		public void Get(out QueryFriendsCallbackInfo output)
		{
			output = default(QueryFriendsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000CE4 RID: 3300
		private Result m_ResultCode;

		// Token: 0x04000CE5 RID: 3301
		private IntPtr m_ClientData;

		// Token: 0x04000CE6 RID: 3302
		private IntPtr m_LocalUserId;
	}
}
