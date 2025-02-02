using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.GameFeatures
{
	// Token: 0x02000072 RID: 114
	[NullableContext(1)]
	public interface IFileSystemProvider
	{
		// Token: 0x06000294 RID: 660
		GameDirectory GetModSettingsDirectory();

		// Token: 0x06000295 RID: 661
		[return: Nullable(2)]
		GameDirectory GetDirectory(GameDirectory directory, string directoryName);

		// Token: 0x06000296 RID: 662
		GameDirectory GetOrCreateDirectory(GameDirectory rootFolder, string id);

		// Token: 0x06000297 RID: 663
		GameFile[] GetFiles(GameDirectory directory, string searchPattern);

		// Token: 0x06000298 RID: 664
		[return: Nullable(2)]
		GameFile GetFile(GameDirectory directory, string fileName);

		// Token: 0x06000299 RID: 665
		GameFile GetOrCreateFile(GameDirectory directory, string fileName);

		// Token: 0x0600029A RID: 666
		bool WriteData(GameFile file, [Nullable(2)] byte[] data);

		// Token: 0x0600029B RID: 667
		[return: Nullable(2)]
		byte[] ReadData(GameFile file);

		// Token: 0x0600029C RID: 668
		[return: Nullable(2)]
		string GetSystemPath(GameFile file);

		// Token: 0x0600029D RID: 669
		[return: Nullable(2)]
		string GetSystemPath(GameDirectory directory);
	}
}
