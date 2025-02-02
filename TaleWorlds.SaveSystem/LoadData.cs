using System;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000012 RID: 18
	public class LoadData
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002C51 File Offset: 0x00000E51
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002C59 File Offset: 0x00000E59
		public MetaData MetaData { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002C62 File Offset: 0x00000E62
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002C6A File Offset: 0x00000E6A
		public GameData GameData { get; private set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002C73 File Offset: 0x00000E73
		public LoadData(MetaData metaData, GameData gameData)
		{
			this.MetaData = metaData;
			this.GameData = gameData;
		}
	}
}
