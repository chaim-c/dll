using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.IntegratedPlatform;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000655 RID: 1621
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OptionsInternal : ISettable<Options>, IDisposable
	{
		// Token: 0x17000C72 RID: 3186
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x0003DDE9 File Offset: 0x0003BFE9
		public IntPtr Reserved
		{
			set
			{
				this.m_Reserved = value;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (set) Token: 0x06002980 RID: 10624 RVA: 0x0003DDF3 File Offset: 0x0003BFF3
		public Utf8String ProductId
		{
			set
			{
				Helper.Set(value, ref this.m_ProductId);
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (set) Token: 0x06002981 RID: 10625 RVA: 0x0003DE03 File Offset: 0x0003C003
		public Utf8String SandboxId
		{
			set
			{
				Helper.Set(value, ref this.m_SandboxId);
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (set) Token: 0x06002982 RID: 10626 RVA: 0x0003DE13 File Offset: 0x0003C013
		public ClientCredentials ClientCredentials
		{
			set
			{
				Helper.Set<ClientCredentials, ClientCredentialsInternal>(ref value, ref this.m_ClientCredentials);
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (set) Token: 0x06002983 RID: 10627 RVA: 0x0003DE24 File Offset: 0x0003C024
		public bool IsServer
		{
			set
			{
				Helper.Set(value, ref this.m_IsServer);
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (set) Token: 0x06002984 RID: 10628 RVA: 0x0003DE34 File Offset: 0x0003C034
		public Utf8String EncryptionKey
		{
			set
			{
				Helper.Set(value, ref this.m_EncryptionKey);
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (set) Token: 0x06002985 RID: 10629 RVA: 0x0003DE44 File Offset: 0x0003C044
		public Utf8String OverrideCountryCode
		{
			set
			{
				Helper.Set(value, ref this.m_OverrideCountryCode);
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (set) Token: 0x06002986 RID: 10630 RVA: 0x0003DE54 File Offset: 0x0003C054
		public Utf8String OverrideLocaleCode
		{
			set
			{
				Helper.Set(value, ref this.m_OverrideLocaleCode);
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (set) Token: 0x06002987 RID: 10631 RVA: 0x0003DE64 File Offset: 0x0003C064
		public Utf8String DeploymentId
		{
			set
			{
				Helper.Set(value, ref this.m_DeploymentId);
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (set) Token: 0x06002988 RID: 10632 RVA: 0x0003DE74 File Offset: 0x0003C074
		public PlatformFlags Flags
		{
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (set) Token: 0x06002989 RID: 10633 RVA: 0x0003DE7E File Offset: 0x0003C07E
		public Utf8String CacheDirectory
		{
			set
			{
				Helper.Set(value, ref this.m_CacheDirectory);
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (set) Token: 0x0600298A RID: 10634 RVA: 0x0003DE8E File Offset: 0x0003C08E
		public uint TickBudgetInMilliseconds
		{
			set
			{
				this.m_TickBudgetInMilliseconds = value;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (set) Token: 0x0600298B RID: 10635 RVA: 0x0003DE98 File Offset: 0x0003C098
		public RTCOptions? RTCOptions
		{
			set
			{
				Helper.Set<RTCOptions, RTCOptionsInternal>(ref value, ref this.m_RTCOptions);
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (set) Token: 0x0600298C RID: 10636 RVA: 0x0003DEA9 File Offset: 0x0003C0A9
		public IntegratedPlatformOptionsContainer IntegratedPlatformOptionsContainerHandle
		{
			set
			{
				Helper.Set(value, ref this.m_IntegratedPlatformOptionsContainerHandle);
			}
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x0003DEBC File Offset: 0x0003C0BC
		public void Set(ref Options other)
		{
			this.m_ApiVersion = 12;
			this.Reserved = other.Reserved;
			this.ProductId = other.ProductId;
			this.SandboxId = other.SandboxId;
			this.ClientCredentials = other.ClientCredentials;
			this.IsServer = other.IsServer;
			this.EncryptionKey = other.EncryptionKey;
			this.OverrideCountryCode = other.OverrideCountryCode;
			this.OverrideLocaleCode = other.OverrideLocaleCode;
			this.DeploymentId = other.DeploymentId;
			this.Flags = other.Flags;
			this.CacheDirectory = other.CacheDirectory;
			this.TickBudgetInMilliseconds = other.TickBudgetInMilliseconds;
			this.RTCOptions = other.RTCOptions;
			this.IntegratedPlatformOptionsContainerHandle = other.IntegratedPlatformOptionsContainerHandle;
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x0003DF88 File Offset: 0x0003C188
		public void Set(ref Options? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 12;
				this.Reserved = other.Value.Reserved;
				this.ProductId = other.Value.ProductId;
				this.SandboxId = other.Value.SandboxId;
				this.ClientCredentials = other.Value.ClientCredentials;
				this.IsServer = other.Value.IsServer;
				this.EncryptionKey = other.Value.EncryptionKey;
				this.OverrideCountryCode = other.Value.OverrideCountryCode;
				this.OverrideLocaleCode = other.Value.OverrideLocaleCode;
				this.DeploymentId = other.Value.DeploymentId;
				this.Flags = other.Value.Flags;
				this.CacheDirectory = other.Value.CacheDirectory;
				this.TickBudgetInMilliseconds = other.Value.TickBudgetInMilliseconds;
				this.RTCOptions = other.Value.RTCOptions;
				this.IntegratedPlatformOptionsContainerHandle = other.Value.IntegratedPlatformOptionsContainerHandle;
			}
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0003E0D4 File Offset: 0x0003C2D4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Reserved);
			Helper.Dispose(ref this.m_ProductId);
			Helper.Dispose(ref this.m_SandboxId);
			Helper.Dispose<ClientCredentialsInternal>(ref this.m_ClientCredentials);
			Helper.Dispose(ref this.m_EncryptionKey);
			Helper.Dispose(ref this.m_OverrideCountryCode);
			Helper.Dispose(ref this.m_OverrideLocaleCode);
			Helper.Dispose(ref this.m_DeploymentId);
			Helper.Dispose(ref this.m_CacheDirectory);
			Helper.Dispose(ref this.m_RTCOptions);
			Helper.Dispose(ref this.m_IntegratedPlatformOptionsContainerHandle);
		}

		// Token: 0x040012C5 RID: 4805
		private int m_ApiVersion;

		// Token: 0x040012C6 RID: 4806
		private IntPtr m_Reserved;

		// Token: 0x040012C7 RID: 4807
		private IntPtr m_ProductId;

		// Token: 0x040012C8 RID: 4808
		private IntPtr m_SandboxId;

		// Token: 0x040012C9 RID: 4809
		private ClientCredentialsInternal m_ClientCredentials;

		// Token: 0x040012CA RID: 4810
		private int m_IsServer;

		// Token: 0x040012CB RID: 4811
		private IntPtr m_EncryptionKey;

		// Token: 0x040012CC RID: 4812
		private IntPtr m_OverrideCountryCode;

		// Token: 0x040012CD RID: 4813
		private IntPtr m_OverrideLocaleCode;

		// Token: 0x040012CE RID: 4814
		private IntPtr m_DeploymentId;

		// Token: 0x040012CF RID: 4815
		private PlatformFlags m_Flags;

		// Token: 0x040012D0 RID: 4816
		private IntPtr m_CacheDirectory;

		// Token: 0x040012D1 RID: 4817
		private uint m_TickBudgetInMilliseconds;

		// Token: 0x040012D2 RID: 4818
		private IntPtr m_RTCOptions;

		// Token: 0x040012D3 RID: 4819
		private IntPtr m_IntegratedPlatformOptionsContainerHandle;
	}
}
