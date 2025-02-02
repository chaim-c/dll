using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001BB RID: 443
	[EngineStruct("Deform_Key_Data", false)]
	public struct DeformKeyData
	{
		// Token: 0x04000774 RID: 1908
		public int GroupId;

		// Token: 0x04000775 RID: 1909
		public int KeyTimePoint;

		// Token: 0x04000776 RID: 1910
		public float KeyMin;

		// Token: 0x04000777 RID: 1911
		public float KeyMax;

		// Token: 0x04000778 RID: 1912
		public float Value;

		// Token: 0x04000779 RID: 1913
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string Id;
	}
}
