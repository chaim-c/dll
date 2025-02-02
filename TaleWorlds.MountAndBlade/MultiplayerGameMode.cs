using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D0 RID: 720
	public abstract class MultiplayerGameMode
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x0009948B File Offset: 0x0009768B
		// (set) Token: 0x060027C0 RID: 10176 RVA: 0x00099493 File Offset: 0x00097693
		public string Name { get; private set; }

		// Token: 0x060027C1 RID: 10177 RVA: 0x0009949C File Offset: 0x0009769C
		protected MultiplayerGameMode(string name)
		{
			this.Name = name;
		}

		// Token: 0x060027C2 RID: 10178
		public abstract void JoinCustomGame(JoinGameData joinGameData);

		// Token: 0x060027C3 RID: 10179
		public abstract void StartMultiplayerGame(string scene);
	}
}
