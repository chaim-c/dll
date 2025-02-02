using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200030C RID: 780
	public class MultiplayerGameTypeInfo
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x000A4CE8 File Offset: 0x000A2EE8
		// (set) Token: 0x06002A57 RID: 10839 RVA: 0x000A4CF0 File Offset: 0x000A2EF0
		public string GameModule { get; private set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x000A4CF9 File Offset: 0x000A2EF9
		// (set) Token: 0x06002A59 RID: 10841 RVA: 0x000A4D01 File Offset: 0x000A2F01
		public string GameType { get; private set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x000A4D0A File Offset: 0x000A2F0A
		// (set) Token: 0x06002A5B RID: 10843 RVA: 0x000A4D12 File Offset: 0x000A2F12
		public List<string> Scenes { get; private set; }

		// Token: 0x06002A5C RID: 10844 RVA: 0x000A4D1B File Offset: 0x000A2F1B
		public MultiplayerGameTypeInfo(string gameModule, string gameType)
		{
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.Scenes = new List<string>();
		}
	}
}
