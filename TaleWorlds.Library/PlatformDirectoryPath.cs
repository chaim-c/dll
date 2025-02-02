using System;
using System.Runtime.InteropServices;

namespace TaleWorlds.Library
{
	// Token: 0x02000076 RID: 118
	public struct PlatformDirectoryPath
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000D414 File Offset: 0x0000B614
		public PlatformDirectoryPath(PlatformFileType type, string path)
		{
			this.Type = type;
			this.Path = path;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000D424 File Offset: 0x0000B624
		public static PlatformDirectoryPath operator +(PlatformDirectoryPath path, string str)
		{
			return new PlatformDirectoryPath(path.Type, path.Path + str);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000D43D File Offset: 0x0000B63D
		public override string ToString()
		{
			return this.Type + " " + this.Path;
		}

		// Token: 0x04000134 RID: 308
		public PlatformFileType Type;

		// Token: 0x04000135 RID: 309
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string Path;
	}
}
