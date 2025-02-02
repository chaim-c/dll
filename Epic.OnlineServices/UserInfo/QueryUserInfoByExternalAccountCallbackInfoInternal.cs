using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003B RID: 59
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByExternalAccountCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryUserInfoByExternalAccountCallbackInfo>, ISettable<QueryUserInfoByExternalAccountCallbackInfo>, IDisposable
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000557C File Offset: 0x0000377C
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00005594 File Offset: 0x00003794
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000055A0 File Offset: 0x000037A0
		// (set) Token: 0x06000396 RID: 918 RVA: 0x000055C1 File Offset: 0x000037C1
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000397 RID: 919 RVA: 0x000055D4 File Offset: 0x000037D4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000055EC File Offset: 0x000037EC
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000560D File Offset: 0x0000380D
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00005620 File Offset: 0x00003820
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00005641 File Offset: 0x00003841
		public Utf8String ExternalAccountId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ExternalAccountId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ExternalAccountId);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00005654 File Offset: 0x00003854
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000566C File Offset: 0x0000386C
		public ExternalAccountType AccountType
		{
			get
			{
				return this.m_AccountType;
			}
			set
			{
				this.m_AccountType = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00005678 File Offset: 0x00003878
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00005699 File Offset: 0x00003899
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

		// Token: 0x060003A0 RID: 928 RVA: 0x000056AC File Offset: 0x000038AC
		public void Set(ref QueryUserInfoByExternalAccountCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.ExternalAccountId = other.ExternalAccountId;
			this.AccountType = other.AccountType;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00005708 File Offset: 0x00003908
		public void Set(ref QueryUserInfoByExternalAccountCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.ExternalAccountId = other.Value.ExternalAccountId;
				this.AccountType = other.Value.AccountType;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000057A3 File Offset: 0x000039A3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ExternalAccountId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000057D6 File Offset: 0x000039D6
		public void Get(out QueryUserInfoByExternalAccountCallbackInfo output)
		{
			output = default(QueryUserInfoByExternalAccountCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400017A RID: 378
		private Result m_ResultCode;

		// Token: 0x0400017B RID: 379
		private IntPtr m_ClientData;

		// Token: 0x0400017C RID: 380
		private IntPtr m_LocalUserId;

		// Token: 0x0400017D RID: 381
		private IntPtr m_ExternalAccountId;

		// Token: 0x0400017E RID: 382
		private ExternalAccountType m_AccountType;

		// Token: 0x0400017F RID: 383
		private IntPtr m_TargetUserId;
	}
}
