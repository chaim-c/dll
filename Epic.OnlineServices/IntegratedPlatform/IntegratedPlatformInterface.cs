using System;

namespace Epic.OnlineServices.IntegratedPlatform
{
	// Token: 0x02000455 RID: 1109
	public sealed class IntegratedPlatformInterface
	{
		// Token: 0x06001C71 RID: 7281 RVA: 0x0002A0A0 File Offset: 0x000282A0
		public static Result CreateIntegratedPlatformOptionsContainer(ref CreateIntegratedPlatformOptionsContainerOptions options, out IntegratedPlatformOptionsContainer outIntegratedPlatformOptionsContainerHandle)
		{
			CreateIntegratedPlatformOptionsContainerOptionsInternal createIntegratedPlatformOptionsContainerOptionsInternal = default(CreateIntegratedPlatformOptionsContainerOptionsInternal);
			createIntegratedPlatformOptionsContainerOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_IntegratedPlatform_CreateIntegratedPlatformOptionsContainer(ref createIntegratedPlatformOptionsContainerOptionsInternal, ref zero);
			Helper.Dispose<CreateIntegratedPlatformOptionsContainerOptionsInternal>(ref createIntegratedPlatformOptionsContainerOptionsInternal);
			Helper.Get<IntegratedPlatformOptionsContainer>(zero, out outIntegratedPlatformOptionsContainerHandle);
			return result;
		}

		// Token: 0x04000C91 RID: 3217
		public const int CreateintegratedplatformoptionscontainerApiLatest = 1;

		// Token: 0x04000C92 RID: 3218
		public static readonly Utf8String IptSteam = "STEAM";

		// Token: 0x04000C93 RID: 3219
		public const int OptionsApiLatest = 1;

		// Token: 0x04000C94 RID: 3220
		public const int SteamOptionsApiLatest = 2;
	}
}
