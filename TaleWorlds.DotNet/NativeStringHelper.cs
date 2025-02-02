using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200002D RID: 45
	internal static class NativeStringHelper
	{
		// Token: 0x0600011F RID: 287 RVA: 0x0000530A File Offset: 0x0000350A
		internal static UIntPtr CreateRglVarString(string text)
		{
			return LibraryApplicationInterface.INativeStringHelper.CreateRglVarString(text);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005317 File Offset: 0x00003517
		internal static UIntPtr GetThreadLocalCachedRglVarString()
		{
			return LibraryApplicationInterface.INativeStringHelper.GetThreadLocalCachedRglVarString();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005323 File Offset: 0x00003523
		internal static void SetRglVarString(UIntPtr pointer, string text)
		{
			LibraryApplicationInterface.INativeStringHelper.SetRglVarString(pointer, text);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005331 File Offset: 0x00003531
		internal static void DeleteRglVarString(UIntPtr pointer)
		{
			LibraryApplicationInterface.INativeStringHelper.DeleteRglVarString(pointer);
		}
	}
}
