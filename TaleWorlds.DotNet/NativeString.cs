using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200002C RID: 44
	[EngineClass("ftdnNative_string")]
	public sealed class NativeString : NativeObject
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000052D4 File Offset: 0x000034D4
		internal NativeString(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000052E3 File Offset: 0x000034E3
		public static NativeString Create()
		{
			return LibraryApplicationInterface.INativeString.Create();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000052EF File Offset: 0x000034EF
		public string GetString()
		{
			return LibraryApplicationInterface.INativeString.GetString(this);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000052FC File Offset: 0x000034FC
		public void SetString(string newString)
		{
			LibraryApplicationInterface.INativeString.SetString(this, newString);
		}
	}
}
