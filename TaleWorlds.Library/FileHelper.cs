using System;
using System.IO;
using System.Threading.Tasks;

namespace TaleWorlds.Library
{
	// Token: 0x0200002F RID: 47
	public static class FileHelper
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0000694F File Offset: 0x00004B4F
		public static SaveResult SaveFile(PlatformFilePath path, byte[] data)
		{
			return Common.PlatformFileHelper.SaveFile(path, data);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000695D File Offset: 0x00004B5D
		public static SaveResult SaveFileString(PlatformFilePath path, string data)
		{
			return Common.PlatformFileHelper.SaveFileString(path, data);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000696B File Offset: 0x00004B6B
		public static string GetFileFullPath(PlatformFilePath path)
		{
			return Common.PlatformFileHelper.GetFileFullPath(path);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006978 File Offset: 0x00004B78
		public static SaveResult AppendLineToFileString(PlatformFilePath path, string data)
		{
			return Common.PlatformFileHelper.AppendLineToFileString(path, data);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006986 File Offset: 0x00004B86
		public static Task<SaveResult> SaveFileAsync(PlatformFilePath path, byte[] data)
		{
			return Common.PlatformFileHelper.SaveFileAsync(path, data);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006994 File Offset: 0x00004B94
		public static Task<SaveResult> SaveFileStringAsync(PlatformFilePath path, string data)
		{
			return Common.PlatformFileHelper.SaveFileStringAsync(path, data);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000069A2 File Offset: 0x00004BA2
		public static string GetError()
		{
			return Common.PlatformFileHelper.GetError();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000069AE File Offset: 0x00004BAE
		public static bool FileExists(PlatformFilePath path)
		{
			return Common.PlatformFileHelper.FileExists(path);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000069BB File Offset: 0x00004BBB
		public static Task<string> GetFileContentStringAsync(PlatformFilePath path)
		{
			return Common.PlatformFileHelper.GetFileContentStringAsync(path);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000069C8 File Offset: 0x00004BC8
		public static string GetFileContentString(PlatformFilePath path)
		{
			return Common.PlatformFileHelper.GetFileContentString(path);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000069D5 File Offset: 0x00004BD5
		public static void DeleteFile(PlatformFilePath path)
		{
			Common.PlatformFileHelper.DeleteFile(path);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000069E3 File Offset: 0x00004BE3
		public static PlatformFilePath[] GetFiles(PlatformDirectoryPath path, string searchPattern)
		{
			return Common.PlatformFileHelper.GetFiles(path, searchPattern);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000069F1 File Offset: 0x00004BF1
		public static byte[] GetFileContent(PlatformFilePath filePath)
		{
			return Common.PlatformFileHelper.GetFileContent(filePath);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006A00 File Offset: 0x00004C00
		public static void CopyFile(PlatformFilePath source, PlatformFilePath target)
		{
			byte[] fileContent = FileHelper.GetFileContent(source);
			FileHelper.SaveFile(target, fileContent);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006A1C File Offset: 0x00004C1C
		public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourceDir);
			if (!directoryInfo.Exists)
			{
				return;
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			Directory.CreateDirectory(destinationDir);
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				string destFileName = Path.Combine(destinationDir, fileInfo.Name);
				fileInfo.CopyTo(destFileName);
			}
			if (recursive)
			{
				foreach (DirectoryInfo directoryInfo2 in directories)
				{
					string destinationDir2 = Path.Combine(destinationDir, directoryInfo2.Name);
					FileHelper.CopyDirectory(directoryInfo2.FullName, destinationDir2, true);
				}
			}
		}
	}
}
