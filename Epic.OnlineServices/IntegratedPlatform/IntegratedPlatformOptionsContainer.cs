using System;

namespace Epic.OnlineServices.IntegratedPlatform
{
	// Token: 0x02000457 RID: 1111
	public sealed class IntegratedPlatformOptionsContainer : Handle
	{
		// Token: 0x06001C74 RID: 7284 RVA: 0x0002A0FE File Offset: 0x000282FE
		public IntegratedPlatformOptionsContainer()
		{
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0002A108 File Offset: 0x00028308
		public IntegratedPlatformOptionsContainer(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0002A114 File Offset: 0x00028314
		public Result Add(ref IntegratedPlatformOptionsContainerAddOptions inOptions)
		{
			IntegratedPlatformOptionsContainerAddOptionsInternal integratedPlatformOptionsContainerAddOptionsInternal = default(IntegratedPlatformOptionsContainerAddOptionsInternal);
			integratedPlatformOptionsContainerAddOptionsInternal.Set(ref inOptions);
			Result result = Bindings.EOS_IntegratedPlatformOptionsContainer_Add(base.InnerHandle, ref integratedPlatformOptionsContainerAddOptionsInternal);
			Helper.Dispose<IntegratedPlatformOptionsContainerAddOptionsInternal>(ref integratedPlatformOptionsContainerAddOptionsInternal);
			return result;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0002A14E File Offset: 0x0002834E
		public void Release()
		{
			Bindings.EOS_IntegratedPlatformOptionsContainer_Release(base.InnerHandle);
		}

		// Token: 0x04000C9D RID: 3229
		public const int IntegratedplatformoptionscontainerAddApiLatest = 1;
	}
}
