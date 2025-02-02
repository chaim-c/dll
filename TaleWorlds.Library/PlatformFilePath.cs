using System;
using System.Runtime.InteropServices;

namespace TaleWorlds.Library
{
	// Token: 0x02000079 RID: 121
	public struct PlatformFilePath
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		public PlatformFilePath(PlatformDirectoryPath folderPath, string fileName)
		{
			this.FolderPath = folderPath;
			this.FileName = fileName;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public static PlatformFilePath operator +(PlatformFilePath path, string str)
		{
			return new PlatformFilePath(path.FolderPath, path.FileName + str);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000D8FD File Offset: 0x0000BAFD
		public string FileFullPath
		{
			get
			{
				return Common.PlatformFileHelper.GetFileFullPath(this);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000D910 File Offset: 0x0000BB10
		public string GetFileNameWithoutExtension()
		{
			int num = this.FileName.LastIndexOf('.');
			if (num == -1)
			{
				return this.FileName;
			}
			return this.FileName.Substring(0, num);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000D943 File Offset: 0x0000BB43
		public override string ToString()
		{
			return this.FolderPath.ToString() + " - " + this.FileName;
		}

		// Token: 0x0400013C RID: 316
		public PlatformDirectoryPath FolderPath;

		// Token: 0x0400013D RID: 317
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string FileName;
	}
}
