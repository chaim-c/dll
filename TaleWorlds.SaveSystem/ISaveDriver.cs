using System;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000010 RID: 16
	public interface ISaveDriver
	{
		// Token: 0x06000046 RID: 70
		Task<SaveResultWithMessage> Save(string saveName, int version, MetaData metaData, GameData gameData);

		// Token: 0x06000047 RID: 71
		SaveGameFileInfo[] GetSaveGameFileInfos();

		// Token: 0x06000048 RID: 72
		string[] GetSaveGameFileNames();

		// Token: 0x06000049 RID: 73
		MetaData LoadMetaData(string saveName);

		// Token: 0x0600004A RID: 74
		LoadData Load(string saveName);

		// Token: 0x0600004B RID: 75
		bool Delete(string saveName);

		// Token: 0x0600004C RID: 76
		bool IsSaveGameFileExists(string saveName);

		// Token: 0x0600004D RID: 77
		bool IsWorkingAsync();
	}
}
