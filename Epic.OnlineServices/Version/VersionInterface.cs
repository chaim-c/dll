using System;

namespace Epic.OnlineServices.Version
{
	// Token: 0x02000023 RID: 35
	public sealed class VersionInterface
	{
		// Token: 0x060002FB RID: 763 RVA: 0x00004980 File Offset: 0x00002B80
		public static Utf8String GetVersion()
		{
			IntPtr from = Bindings.EOS_GetVersion();
			Utf8String result;
			Helper.Get(from, out result);
			return result;
		}

		// Token: 0x04000136 RID: 310
		public static readonly Utf8String CompanyName = "Epic Games, Inc.";

		// Token: 0x04000137 RID: 311
		public static readonly Utf8String CopyrightString = "Copyright Epic Games, Inc. All Rights Reserved.";

		// Token: 0x04000138 RID: 312
		public const int MajorVersion = 1;

		// Token: 0x04000139 RID: 313
		public const int MinorVersion = 15;

		// Token: 0x0400013A RID: 314
		public const int PatchVersion = 4;

		// Token: 0x0400013B RID: 315
		public static readonly Utf8String ProductIdentifier = "Epic Online Services SDK";

		// Token: 0x0400013C RID: 316
		public static readonly Utf8String ProductName = "Epic Online Services SDK";
	}
}
