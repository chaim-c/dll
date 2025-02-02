using System;
using System.Linq;
using System.Runtime.CompilerServices;
using MCM.Abstractions.GameFeatures;
using MCM.Internal.Extensions;
using TaleWorlds.Library;

namespace MCM.Internal.GameFeatures
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class FileSystemProvider : IFileSystemProvider
	{
		// Token: 0x06000043 RID: 67 RVA: 0x0000303A File Offset: 0x0000123A
		private static string EnsureDirectoryEndsInSeparator(string directory)
		{
			return directory.EndsWith("\\") ? directory : (directory + "\\");
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003057 File Offset: 0x00001257
		private IPlatformFileHelper PlatformFileHelper
		{
			get
			{
				return Common.PlatformFileHelper;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003060 File Offset: 0x00001260
		private static GameDirectory GetConfigsDirectory()
		{
			PlatformDirectoryPath directory = new PlatformDirectoryPath(PlatformFileType.User, FileSystemProvider.EnsureDirectoryEndsInSeparator("Configs"));
			return new GameDirectory((PlatformDirectoryType)directory.Type, directory.Path);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003098 File Offset: 0x00001298
		public GameDirectory GetModSettingsDirectory()
		{
			return this.GetOrCreateDirectory(FileSystemProvider.GetConfigsDirectory(), FileSystemProvider.EnsureDirectoryEndsInSeparator("ModSettings"));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000030C0 File Offset: 0x000012C0
		[return: Nullable(2)]
		public GameDirectory GetDirectory(GameDirectory directory, string name)
		{
			GameDirectory gameDirectory = directory.<Clone>$();
			gameDirectory.Path = directory.Path + FileSystemProvider.EnsureDirectoryEndsInSeparator(name);
			return gameDirectory;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000030F0 File Offset: 0x000012F0
		public GameDirectory GetOrCreateDirectory(GameDirectory directory, string name)
		{
			GameDirectory gameDirectory = directory.<Clone>$();
			gameDirectory.Path = directory.Path + FileSystemProvider.EnsureDirectoryEndsInSeparator(name);
			return gameDirectory;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003120 File Offset: 0x00001320
		public GameFile[] GetFiles(GameDirectory directory, string searchPattern)
		{
			return (from x in this.PlatformFileHelper.GetFiles(new PlatformDirectoryPath((PlatformFileType)directory.Type, directory.Path), searchPattern)
			select new GameFile(directory, x.FileName)).ToArray<GameFile>();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000317C File Offset: 0x0000137C
		[return: Nullable(2)]
		public GameFile GetFile(GameDirectory directory, string fileName)
		{
			PlatformFilePath file = new PlatformFilePath(new PlatformDirectoryPath((PlatformFileType)directory.Type, directory.Path), fileName);
			return (!this.PlatformFileHelper.FileExists(file)) ? null : new GameFile(directory, fileName);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031C0 File Offset: 0x000013C0
		public GameFile GetOrCreateFile(GameDirectory directory, string fileName)
		{
			PlatformFilePath file = new PlatformFilePath(new PlatformDirectoryPath((PlatformFileType)directory.Type, directory.Path), fileName);
			bool flag = !this.PlatformFileHelper.FileExists(file);
			if (flag)
			{
				this.PlatformFileHelper.SaveFile(file, Array.Empty<byte>());
			}
			return new GameFile(directory, fileName);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000321C File Offset: 0x0000141C
		public bool WriteData(GameFile file, [Nullable(2)] byte[] data)
		{
			PlatformFilePath baseFile = new PlatformFilePath(new PlatformDirectoryPath((PlatformFileType)file.Owner.Type, file.Owner.Path), file.Name);
			bool flag = data == null;
			bool result;
			if (flag)
			{
				result = this.PlatformFileHelper.DeleteFile(baseFile);
			}
			else
			{
				result = (this.PlatformFileHelper.SaveFile(baseFile, data) == SaveResult.Success);
			}
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003280 File Offset: 0x00001480
		[return: Nullable(2)]
		public byte[] ReadData(GameFile file)
		{
			PlatformFilePath baseFile = new PlatformFilePath(new PlatformDirectoryPath((PlatformFileType)file.Owner.Type, file.Owner.Path), file.Name);
			return (!this.PlatformFileHelper.FileExists(baseFile)) ? null : this.PlatformFileHelper.GetFileContent(baseFile);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000032D8 File Offset: 0x000014D8
		[return: Nullable(2)]
		public string GetSystemPath(GameFile file)
		{
			PlatformFilePath baseFile = new PlatformFilePath(new PlatformDirectoryPath((PlatformFileType)file.Owner.Type, file.Owner.Path), file.Name);
			return this.PlatformFileHelper.GetFileFullPath(baseFile);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003320 File Offset: 0x00001520
		[return: Nullable(2)]
		public string GetSystemPath(GameDirectory directory)
		{
			PlatformDirectoryPath baseDirectory = new PlatformDirectoryPath((PlatformFileType)directory.Type, directory.Path);
			return PlatformFileHelperPCExtended.GetDirectoryFullPath(baseDirectory);
		}
	}
}
