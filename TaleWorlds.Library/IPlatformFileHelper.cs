using System;
using System.Threading.Tasks;

namespace TaleWorlds.Library
{
	// Token: 0x0200003F RID: 63
	public interface IPlatformFileHelper
	{
		// Token: 0x06000201 RID: 513
		SaveResult SaveFile(PlatformFilePath path, byte[] data);

		// Token: 0x06000202 RID: 514
		SaveResult SaveFileString(PlatformFilePath path, string data);

		// Token: 0x06000203 RID: 515
		SaveResult AppendLineToFileString(PlatformFilePath path, string data);

		// Token: 0x06000204 RID: 516
		Task<SaveResult> SaveFileAsync(PlatformFilePath path, byte[] data);

		// Token: 0x06000205 RID: 517
		Task<SaveResult> SaveFileStringAsync(PlatformFilePath path, string data);

		// Token: 0x06000206 RID: 518
		bool FileExists(PlatformFilePath path);

		// Token: 0x06000207 RID: 519
		Task<string> GetFileContentStringAsync(PlatformFilePath path);

		// Token: 0x06000208 RID: 520
		string GetFileContentString(PlatformFilePath path);

		// Token: 0x06000209 RID: 521
		byte[] GetFileContent(PlatformFilePath filePath);

		// Token: 0x0600020A RID: 522
		bool DeleteFile(PlatformFilePath path);

		// Token: 0x0600020B RID: 523
		PlatformFilePath[] GetFiles(PlatformDirectoryPath path, string searchPattern);

		// Token: 0x0600020C RID: 524
		string GetFileFullPath(PlatformFilePath filePath);

		// Token: 0x0600020D RID: 525
		string GetError();
	}
}
