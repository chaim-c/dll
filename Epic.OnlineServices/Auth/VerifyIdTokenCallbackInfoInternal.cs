using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005AA RID: 1450
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyIdTokenCallbackInfoInternal : ICallbackInfoInternal, IGettable<VerifyIdTokenCallbackInfo>, ISettable<VerifyIdTokenCallbackInfo>, IDisposable
	{
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x00037438 File Offset: 0x00035638
		// (set) Token: 0x06002552 RID: 9554 RVA: 0x00037450 File Offset: 0x00035650
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

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x0003745C File Offset: 0x0003565C
		// (set) Token: 0x06002554 RID: 9556 RVA: 0x0003747D File Offset: 0x0003567D
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

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x00037490 File Offset: 0x00035690
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000374A8 File Offset: 0x000356A8
		// (set) Token: 0x06002557 RID: 9559 RVA: 0x000374C9 File Offset: 0x000356C9
		public Utf8String ApplicationId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ApplicationId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ApplicationId);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000374DC File Offset: 0x000356DC
		// (set) Token: 0x06002559 RID: 9561 RVA: 0x000374FD File Offset: 0x000356FD
		public Utf8String ClientId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ClientId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientId);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x00037510 File Offset: 0x00035710
		// (set) Token: 0x0600255B RID: 9563 RVA: 0x00037531 File Offset: 0x00035731
		public Utf8String ProductId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ProductId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ProductId);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x00037544 File Offset: 0x00035744
		// (set) Token: 0x0600255D RID: 9565 RVA: 0x00037565 File Offset: 0x00035765
		public Utf8String SandboxId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_SandboxId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SandboxId);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x00037578 File Offset: 0x00035778
		// (set) Token: 0x0600255F RID: 9567 RVA: 0x00037599 File Offset: 0x00035799
		public Utf8String DeploymentId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DeploymentId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DeploymentId);
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000375AC File Offset: 0x000357AC
		// (set) Token: 0x06002561 RID: 9569 RVA: 0x000375CD File Offset: 0x000357CD
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

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000375E0 File Offset: 0x000357E0
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x00037601 File Offset: 0x00035801
		public bool IsExternalAccountInfoPresent
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsExternalAccountInfoPresent, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsExternalAccountInfoPresent);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x00037614 File Offset: 0x00035814
		// (set) Token: 0x06002565 RID: 9573 RVA: 0x0003762C File Offset: 0x0003582C
		public ExternalAccountType ExternalAccountIdType
		{
			get
			{
				return this.m_ExternalAccountIdType;
			}
			set
			{
				this.m_ExternalAccountIdType = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00037638 File Offset: 0x00035838
		// (set) Token: 0x06002567 RID: 9575 RVA: 0x00037659 File Offset: 0x00035859
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

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x0003766C File Offset: 0x0003586C
		// (set) Token: 0x06002569 RID: 9577 RVA: 0x0003768D File Offset: 0x0003588D
		public Utf8String ExternalAccountDisplayName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ExternalAccountDisplayName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ExternalAccountDisplayName);
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x000376A0 File Offset: 0x000358A0
		// (set) Token: 0x0600256B RID: 9579 RVA: 0x000376C1 File Offset: 0x000358C1
		public Utf8String Platform
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Platform, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Platform);
			}
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000376D4 File Offset: 0x000358D4
		public void Set(ref VerifyIdTokenCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.ApplicationId = other.ApplicationId;
			this.ClientId = other.ClientId;
			this.ProductId = other.ProductId;
			this.SandboxId = other.SandboxId;
			this.DeploymentId = other.DeploymentId;
			this.DisplayName = other.DisplayName;
			this.IsExternalAccountInfoPresent = other.IsExternalAccountInfoPresent;
			this.ExternalAccountIdType = other.ExternalAccountIdType;
			this.ExternalAccountId = other.ExternalAccountId;
			this.ExternalAccountDisplayName = other.ExternalAccountDisplayName;
			this.Platform = other.Platform;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x0003778C File Offset: 0x0003598C
		public void Set(ref VerifyIdTokenCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.ApplicationId = other.Value.ApplicationId;
				this.ClientId = other.Value.ClientId;
				this.ProductId = other.Value.ProductId;
				this.SandboxId = other.Value.SandboxId;
				this.DeploymentId = other.Value.DeploymentId;
				this.DisplayName = other.Value.DisplayName;
				this.IsExternalAccountInfoPresent = other.Value.IsExternalAccountInfoPresent;
				this.ExternalAccountIdType = other.Value.ExternalAccountIdType;
				this.ExternalAccountId = other.Value.ExternalAccountId;
				this.ExternalAccountDisplayName = other.Value.ExternalAccountDisplayName;
				this.Platform = other.Value.Platform;
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000378BC File Offset: 0x00035ABC
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_ApplicationId);
			Helper.Dispose(ref this.m_ClientId);
			Helper.Dispose(ref this.m_ProductId);
			Helper.Dispose(ref this.m_SandboxId);
			Helper.Dispose(ref this.m_DeploymentId);
			Helper.Dispose(ref this.m_DisplayName);
			Helper.Dispose(ref this.m_ExternalAccountId);
			Helper.Dispose(ref this.m_ExternalAccountDisplayName);
			Helper.Dispose(ref this.m_Platform);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x00037942 File Offset: 0x00035B42
		public void Get(out VerifyIdTokenCallbackInfo output)
		{
			output = default(VerifyIdTokenCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400105C RID: 4188
		private Result m_ResultCode;

		// Token: 0x0400105D RID: 4189
		private IntPtr m_ClientData;

		// Token: 0x0400105E RID: 4190
		private IntPtr m_ApplicationId;

		// Token: 0x0400105F RID: 4191
		private IntPtr m_ClientId;

		// Token: 0x04001060 RID: 4192
		private IntPtr m_ProductId;

		// Token: 0x04001061 RID: 4193
		private IntPtr m_SandboxId;

		// Token: 0x04001062 RID: 4194
		private IntPtr m_DeploymentId;

		// Token: 0x04001063 RID: 4195
		private IntPtr m_DisplayName;

		// Token: 0x04001064 RID: 4196
		private int m_IsExternalAccountInfoPresent;

		// Token: 0x04001065 RID: 4197
		private ExternalAccountType m_ExternalAccountIdType;

		// Token: 0x04001066 RID: 4198
		private IntPtr m_ExternalAccountId;

		// Token: 0x04001067 RID: 4199
		private IntPtr m_ExternalAccountDisplayName;

		// Token: 0x04001068 RID: 4200
		private IntPtr m_Platform;
	}
}
