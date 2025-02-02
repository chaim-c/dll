using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200056B RID: 1387
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyIdTokenCallbackInfoInternal : ICallbackInfoInternal, IGettable<VerifyIdTokenCallbackInfo>, ISettable<VerifyIdTokenCallbackInfo>, IDisposable
	{
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x000347B0 File Offset: 0x000329B0
		// (set) Token: 0x06002382 RID: 9090 RVA: 0x000347C8 File Offset: 0x000329C8
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

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000347D4 File Offset: 0x000329D4
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x000347F5 File Offset: 0x000329F5
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

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x00034808 File Offset: 0x00032A08
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x00034820 File Offset: 0x00032A20
		// (set) Token: 0x06002387 RID: 9095 RVA: 0x00034841 File Offset: 0x00032A41
		public ProductUserId ProductUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_ProductUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ProductUserId);
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x00034854 File Offset: 0x00032A54
		// (set) Token: 0x06002389 RID: 9097 RVA: 0x00034875 File Offset: 0x00032A75
		public bool IsAccountInfoPresent
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsAccountInfoPresent, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsAccountInfoPresent);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x00034888 File Offset: 0x00032A88
		// (set) Token: 0x0600238B RID: 9099 RVA: 0x000348A0 File Offset: 0x00032AA0
		public ExternalAccountType AccountIdType
		{
			get
			{
				return this.m_AccountIdType;
			}
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000348AC File Offset: 0x00032AAC
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x000348CD File Offset: 0x00032ACD
		public Utf8String AccountId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_AccountId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AccountId);
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x000348E0 File Offset: 0x00032AE0
		// (set) Token: 0x0600238F RID: 9103 RVA: 0x00034901 File Offset: 0x00032B01
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

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x00034914 File Offset: 0x00032B14
		// (set) Token: 0x06002391 RID: 9105 RVA: 0x00034935 File Offset: 0x00032B35
		public Utf8String DeviceType
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DeviceType, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DeviceType);
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x00034948 File Offset: 0x00032B48
		// (set) Token: 0x06002393 RID: 9107 RVA: 0x00034969 File Offset: 0x00032B69
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

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x0003497C File Offset: 0x00032B7C
		// (set) Token: 0x06002395 RID: 9109 RVA: 0x0003499D File Offset: 0x00032B9D
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

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x000349B0 File Offset: 0x00032BB0
		// (set) Token: 0x06002397 RID: 9111 RVA: 0x000349D1 File Offset: 0x00032BD1
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

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x000349E4 File Offset: 0x00032BE4
		// (set) Token: 0x06002399 RID: 9113 RVA: 0x00034A05 File Offset: 0x00032C05
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

		// Token: 0x0600239A RID: 9114 RVA: 0x00034A18 File Offset: 0x00032C18
		public void Set(ref VerifyIdTokenCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.ProductUserId = other.ProductUserId;
			this.IsAccountInfoPresent = other.IsAccountInfoPresent;
			this.AccountIdType = other.AccountIdType;
			this.AccountId = other.AccountId;
			this.Platform = other.Platform;
			this.DeviceType = other.DeviceType;
			this.ClientId = other.ClientId;
			this.ProductId = other.ProductId;
			this.SandboxId = other.SandboxId;
			this.DeploymentId = other.DeploymentId;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00034AC4 File Offset: 0x00032CC4
		public void Set(ref VerifyIdTokenCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.ProductUserId = other.Value.ProductUserId;
				this.IsAccountInfoPresent = other.Value.IsAccountInfoPresent;
				this.AccountIdType = other.Value.AccountIdType;
				this.AccountId = other.Value.AccountId;
				this.Platform = other.Value.Platform;
				this.DeviceType = other.Value.DeviceType;
				this.ClientId = other.Value.ClientId;
				this.ProductId = other.Value.ProductId;
				this.SandboxId = other.Value.SandboxId;
				this.DeploymentId = other.Value.DeploymentId;
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x00034BE0 File Offset: 0x00032DE0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_ProductUserId);
			Helper.Dispose(ref this.m_AccountId);
			Helper.Dispose(ref this.m_Platform);
			Helper.Dispose(ref this.m_DeviceType);
			Helper.Dispose(ref this.m_ClientId);
			Helper.Dispose(ref this.m_ProductId);
			Helper.Dispose(ref this.m_SandboxId);
			Helper.Dispose(ref this.m_DeploymentId);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x00034C5A File Offset: 0x00032E5A
		public void Get(out VerifyIdTokenCallbackInfo output)
		{
			output = default(VerifyIdTokenCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F9A RID: 3994
		private Result m_ResultCode;

		// Token: 0x04000F9B RID: 3995
		private IntPtr m_ClientData;

		// Token: 0x04000F9C RID: 3996
		private IntPtr m_ProductUserId;

		// Token: 0x04000F9D RID: 3997
		private int m_IsAccountInfoPresent;

		// Token: 0x04000F9E RID: 3998
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000F9F RID: 3999
		private IntPtr m_AccountId;

		// Token: 0x04000FA0 RID: 4000
		private IntPtr m_Platform;

		// Token: 0x04000FA1 RID: 4001
		private IntPtr m_DeviceType;

		// Token: 0x04000FA2 RID: 4002
		private IntPtr m_ClientId;

		// Token: 0x04000FA3 RID: 4003
		private IntPtr m_ProductId;

		// Token: 0x04000FA4 RID: 4004
		private IntPtr m_SandboxId;

		// Token: 0x04000FA5 RID: 4005
		private IntPtr m_DeploymentId;
	}
}
