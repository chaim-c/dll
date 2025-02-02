using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000587 RID: 1415
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginCallbackInfoInternal : ICallbackInfoInternal, IGettable<LoginCallbackInfo>, ISettable<LoginCallbackInfo>, IDisposable
	{
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x00035FF0 File Offset: 0x000341F0
		// (set) Token: 0x06002448 RID: 9288 RVA: 0x00036008 File Offset: 0x00034208
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

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x00036014 File Offset: 0x00034214
		// (set) Token: 0x0600244A RID: 9290 RVA: 0x00036035 File Offset: 0x00034235
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

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x00036048 File Offset: 0x00034248
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x00036060 File Offset: 0x00034260
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x00036081 File Offset: 0x00034281
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

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x00036094 File Offset: 0x00034294
		// (set) Token: 0x0600244F RID: 9295 RVA: 0x000360B5 File Offset: 0x000342B5
		public PinGrantInfo? PinGrantInfo
		{
			get
			{
				PinGrantInfo? result;
				Helper.Get<PinGrantInfoInternal, PinGrantInfo>(this.m_PinGrantInfo, out result);
				return result;
			}
			set
			{
				Helper.Set<PinGrantInfo, PinGrantInfoInternal>(ref value, ref this.m_PinGrantInfo);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x000360C8 File Offset: 0x000342C8
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x000360E9 File Offset: 0x000342E9
		public ContinuanceToken ContinuanceToken
		{
			get
			{
				ContinuanceToken result;
				Helper.Get<ContinuanceToken>(this.m_ContinuanceToken, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ContinuanceToken);
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x000360FC File Offset: 0x000342FC
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x0003611D File Offset: 0x0003431D
		public AccountFeatureRestrictedInfo? AccountFeatureRestrictedInfo
		{
			get
			{
				AccountFeatureRestrictedInfo? result;
				Helper.Get<AccountFeatureRestrictedInfoInternal, AccountFeatureRestrictedInfo>(this.m_AccountFeatureRestrictedInfo, out result);
				return result;
			}
			set
			{
				Helper.Set<AccountFeatureRestrictedInfo, AccountFeatureRestrictedInfoInternal>(ref value, ref this.m_AccountFeatureRestrictedInfo);
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x00036130 File Offset: 0x00034330
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x00036151 File Offset: 0x00034351
		public EpicAccountId SelectedAccountId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_SelectedAccountId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SelectedAccountId);
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x00036164 File Offset: 0x00034364
		public void Set(ref LoginCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PinGrantInfo = other.PinGrantInfo;
			this.ContinuanceToken = other.ContinuanceToken;
			this.AccountFeatureRestrictedInfo = other.AccountFeatureRestrictedInfo;
			this.SelectedAccountId = other.SelectedAccountId;
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000361D0 File Offset: 0x000343D0
		public void Set(ref LoginCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PinGrantInfo = other.Value.PinGrantInfo;
				this.ContinuanceToken = other.Value.ContinuanceToken;
				this.AccountFeatureRestrictedInfo = other.Value.AccountFeatureRestrictedInfo;
				this.SelectedAccountId = other.Value.SelectedAccountId;
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x00036280 File Offset: 0x00034480
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_PinGrantInfo);
			Helper.Dispose(ref this.m_ContinuanceToken);
			Helper.Dispose(ref this.m_AccountFeatureRestrictedInfo);
			Helper.Dispose(ref this.m_SelectedAccountId);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000362D6 File Offset: 0x000344D6
		public void Get(out LoginCallbackInfo output)
		{
			output = default(LoginCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000FFE RID: 4094
		private Result m_ResultCode;

		// Token: 0x04000FFF RID: 4095
		private IntPtr m_ClientData;

		// Token: 0x04001000 RID: 4096
		private IntPtr m_LocalUserId;

		// Token: 0x04001001 RID: 4097
		private IntPtr m_PinGrantInfo;

		// Token: 0x04001002 RID: 4098
		private IntPtr m_ContinuanceToken;

		// Token: 0x04001003 RID: 4099
		private IntPtr m_AccountFeatureRestrictedInfo;

		// Token: 0x04001004 RID: 4100
		private IntPtr m_SelectedAccountId;
	}
}
