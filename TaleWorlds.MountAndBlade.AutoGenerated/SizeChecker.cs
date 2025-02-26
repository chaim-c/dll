﻿using System;
using System.Diagnostics;

namespace TaleWorlds.MountAndBlade.AutoGenerated
{
	// Token: 0x02000004 RID: 4
	internal class SizeChecker
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		[Conditional("_RGL_KEEP_ASSERTS")]
		private static void CheckTypeSizeAux(int managedSize, string name)
		{
			MBAPI.IMBBannerlordChecker.GetEngineStructSize(name);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002066 File Offset: 0x00000266
		[Conditional("_RGL_KEEP_ASSERTS")]
		private static void CheckTypeOffsetAux(IntPtr managedOffset, string className, string memberName)
		{
			MBAPI.IMBBannerlordChecker.GetEngineStructMemberOffset(className, memberName);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002075 File Offset: 0x00000275
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void CheckSharedStructureSizes()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002077 File Offset: 0x00000277
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void CheckSharedStructureOffsets()
		{
		}
	}
}
