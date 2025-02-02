using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.IntegratedPlatform;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200065C RID: 1628
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WindowsOptionsInternal : ISettable<WindowsOptions>, IDisposable
	{
		// Token: 0x17000C90 RID: 3216
		// (set) Token: 0x060029BD RID: 10685 RVA: 0x0003E309 File Offset: 0x0003C509
		public IntPtr Reserved
		{
			set
			{
				this.m_Reserved = value;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (set) Token: 0x060029BE RID: 10686 RVA: 0x0003E313 File Offset: 0x0003C513
		public Utf8String ProductId
		{
			set
			{
				Helper.Set(value, ref this.m_ProductId);
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (set) Token: 0x060029BF RID: 10687 RVA: 0x0003E323 File Offset: 0x0003C523
		public Utf8String SandboxId
		{
			set
			{
				Helper.Set(value, ref this.m_SandboxId);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (set) Token: 0x060029C0 RID: 10688 RVA: 0x0003E333 File Offset: 0x0003C533
		public ClientCredentials ClientCredentials
		{
			set
			{
				Helper.Set<ClientCredentials, ClientCredentialsInternal>(ref value, ref this.m_ClientCredentials);
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (set) Token: 0x060029C1 RID: 10689 RVA: 0x0003E344 File Offset: 0x0003C544
		public bool IsServer
		{
			set
			{
				Helper.Set(value, ref this.m_IsServer);
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (set) Token: 0x060029C2 RID: 10690 RVA: 0x0003E354 File Offset: 0x0003C554
		public Utf8String EncryptionKey
		{
			set
			{
				Helper.Set(value, ref this.m_EncryptionKey);
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (set) Token: 0x060029C3 RID: 10691 RVA: 0x0003E364 File Offset: 0x0003C564
		public Utf8String OverrideCountryCode
		{
			set
			{
				Helper.Set(value, ref this.m_OverrideCountryCode);
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (set) Token: 0x060029C4 RID: 10692 RVA: 0x0003E374 File Offset: 0x0003C574
		public Utf8String OverrideLocaleCode
		{
			set
			{
				Helper.Set(value, ref this.m_OverrideLocaleCode);
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (set) Token: 0x060029C5 RID: 10693 RVA: 0x0003E384 File Offset: 0x0003C584
		public Utf8String DeploymentId
		{
			set
			{
				Helper.Set(value, ref this.m_DeploymentId);
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (set) Token: 0x060029C6 RID: 10694 RVA: 0x0003E394 File Offset: 0x0003C594
		public PlatformFlags Flags
		{
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x0003E39E File Offset: 0x0003C59E
		public Utf8String CacheDirectory
		{
			set
			{
				Helper.Set(value, ref this.m_CacheDirectory);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (set) Token: 0x060029C8 RID: 10696 RVA: 0x0003E3AE File Offset: 0x0003C5AE
		public uint TickBudgetInMilliseconds
		{
			set
			{
				this.m_TickBudgetInMilliseconds = value;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x0003E3B8 File Offset: 0x0003C5B8
		public WindowsRTCOptions? RTCOptions
		{
			set
			{
				Helper.Set<WindowsRTCOptions, WindowsRTCOptionsInternal>(ref value, ref this.m_RTCOptions);
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (set) Token: 0x060029CA RID: 10698 RVA: 0x0003E3C9 File Offset: 0x0003C5C9
		public IntegratedPlatformOptionsContainer IntegratedPlatformOptionsContainerHandle
		{
			set
			{
				Helper.Set(value, ref this.m_IntegratedPlatformOptionsContainerHandle);
			}
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x0003E3DC File Offset: 0x0003C5DC
		public void Set(ref WindowsOptions other)
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

		// Token: 0x060029CC RID: 10700 RVA: 0x0003E4A8 File Offset: 0x0003C6A8
		public void Set(ref WindowsOptions? other)
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

		// Token: 0x060029CD RID: 10701 RVA: 0x0003E5F4 File Offset: 0x0003C7F4
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

		// Token: 0x040012EE RID: 4846
		private int m_ApiVersion;

		// Token: 0x040012EF RID: 4847
		private IntPtr m_Reserved;

		// Token: 0x040012F0 RID: 4848
		private IntPtr m_ProductId;

		// Token: 0x040012F1 RID: 4849
		private IntPtr m_SandboxId;

		// Token: 0x040012F2 RID: 4850
		private ClientCredentialsInternal m_ClientCredentials;

		// Token: 0x040012F3 RID: 4851
		private int m_IsServer;

		// Token: 0x040012F4 RID: 4852
		private IntPtr m_EncryptionKey;

		// Token: 0x040012F5 RID: 4853
		private IntPtr m_OverrideCountryCode;

		// Token: 0x040012F6 RID: 4854
		private IntPtr m_OverrideLocaleCode;

		// Token: 0x040012F7 RID: 4855
		private IntPtr m_DeploymentId;

		// Token: 0x040012F8 RID: 4856
		private PlatformFlags m_Flags;

		// Token: 0x040012F9 RID: 4857
		private IntPtr m_CacheDirectory;

		// Token: 0x040012FA RID: 4858
		private uint m_TickBudgetInMilliseconds;

		// Token: 0x040012FB RID: 4859
		private IntPtr m_RTCOptions;

		// Token: 0x040012FC RID: 4860
		private IntPtr m_IntegratedPlatformOptionsContainerHandle;
	}
}
