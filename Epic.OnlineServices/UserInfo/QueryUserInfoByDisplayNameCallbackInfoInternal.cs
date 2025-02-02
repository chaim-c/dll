using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000037 RID: 55
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByDisplayNameCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryUserInfoByDisplayNameCallbackInfo>, ISettable<QueryUserInfoByDisplayNameCallbackInfo>, IDisposable
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000051A8 File Offset: 0x000033A8
		// (set) Token: 0x0600036E RID: 878 RVA: 0x000051C0 File Offset: 0x000033C0
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000051CC File Offset: 0x000033CC
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000051ED File Offset: 0x000033ED
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00005200 File Offset: 0x00003400
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00005218 File Offset: 0x00003418
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00005239 File Offset: 0x00003439
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000524C File Offset: 0x0000344C
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000526D File Offset: 0x0000346D
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00005280 File Offset: 0x00003480
		// (set) Token: 0x06000377 RID: 887 RVA: 0x000052A1 File Offset: 0x000034A1
		public Utf8String DisplayName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DisplayName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DisplayName);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000052B4 File Offset: 0x000034B4
		public void Set(ref QueryUserInfoByDisplayNameCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.DisplayName = other.DisplayName;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00005304 File Offset: 0x00003504
		public void Set(ref QueryUserInfoByDisplayNameCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.DisplayName = other.Value.DisplayName;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00005387 File Offset: 0x00003587
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_DisplayName);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000053BA File Offset: 0x000035BA
		public void Get(out QueryUserInfoByDisplayNameCallbackInfo output)
		{
			output = default(QueryUserInfoByDisplayNameCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400016A RID: 362
		private Result m_ResultCode;

		// Token: 0x0400016B RID: 363
		private IntPtr m_ClientData;

		// Token: 0x0400016C RID: 364
		private IntPtr m_LocalUserId;

		// Token: 0x0400016D RID: 365
		private IntPtr m_TargetUserId;

		// Token: 0x0400016E RID: 366
		private IntPtr m_DisplayName;
	}
}
