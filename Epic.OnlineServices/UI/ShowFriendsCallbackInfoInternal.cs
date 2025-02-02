using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000074 RID: 116
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ShowFriendsCallbackInfoInternal : ICallbackInfoInternal, IGettable<ShowFriendsCallbackInfo>, ISettable<ShowFriendsCallbackInfo>, IDisposable
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000071C4 File Offset: 0x000053C4
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x000071DC File Offset: 0x000053DC
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x000071E8 File Offset: 0x000053E8
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00007209 File Offset: 0x00005409
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000721C File Offset: 0x0000541C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00007234 File Offset: 0x00005434
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00007255 File Offset: 0x00005455
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

		// Token: 0x060004D6 RID: 1238 RVA: 0x00007265 File Offset: 0x00005465
		public void Set(ref ShowFriendsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00007290 File Offset: 0x00005490
		public void Set(ref ShowFriendsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000072E9 File Offset: 0x000054E9
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00007304 File Offset: 0x00005504
		public void Get(out ShowFriendsCallbackInfo output)
		{
			output = default(ShowFriendsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000265 RID: 613
		private Result m_ResultCode;

		// Token: 0x04000266 RID: 614
		private IntPtr m_ClientData;

		// Token: 0x04000267 RID: 615
		private IntPtr m_LocalUserId;
	}
}
