using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000050 RID: 80
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HideFriendsCallbackInfoInternal : ICallbackInfoInternal, IGettable<HideFriendsCallbackInfo>, ISettable<HideFriendsCallbackInfo>, IDisposable
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00006558 File Offset: 0x00004758
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00006570 File Offset: 0x00004770
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

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000657C File Offset: 0x0000477C
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000659D File Offset: 0x0000479D
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000065B0 File Offset: 0x000047B0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x000065C8 File Offset: 0x000047C8
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x000065E9 File Offset: 0x000047E9
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

		// Token: 0x06000424 RID: 1060 RVA: 0x000065F9 File Offset: 0x000047F9
		public void Set(ref HideFriendsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00006624 File Offset: 0x00004824
		public void Set(ref HideFriendsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000667D File Offset: 0x0000487D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00006698 File Offset: 0x00004898
		public void Get(out HideFriendsCallbackInfo output)
		{
			output = default(HideFriendsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040001BC RID: 444
		private Result m_ResultCode;

		// Token: 0x040001BD RID: 445
		private IntPtr m_ClientData;

		// Token: 0x040001BE RID: 446
		private IntPtr m_LocalUserId;
	}
}
