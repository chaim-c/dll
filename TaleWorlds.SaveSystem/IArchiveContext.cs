using System;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000017 RID: 23
	internal interface IArchiveContext
	{
		// Token: 0x0600007E RID: 126
		SaveEntryFolder CreateFolder(SaveEntryFolder parentFolder, FolderId folderId, int entryCount);
	}
}
