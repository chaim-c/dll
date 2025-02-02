using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DD RID: 477
	public class MBUnusedResourceManager
	{
		// Token: 0x06001AD7 RID: 6871 RVA: 0x0005D2F7 File Offset: 0x0005B4F7
		public static void SetMeshUsed(string meshName)
		{
			MBAPI.IMBWorld.SetMeshUsed(meshName);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0005D304 File Offset: 0x0005B504
		public static void SetMaterialUsed(string meshName)
		{
			MBAPI.IMBWorld.SetMaterialUsed(meshName);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0005D311 File Offset: 0x0005B511
		public static void SetBodyUsed(string bodyName)
		{
			MBAPI.IMBWorld.SetBodyUsed(bodyName);
		}
	}
}
