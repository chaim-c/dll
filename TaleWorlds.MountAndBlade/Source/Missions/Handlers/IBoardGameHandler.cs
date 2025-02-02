using System;

namespace TaleWorlds.MountAndBlade.Source.Missions.Handlers
{
	// Token: 0x020003B8 RID: 952
	public interface IBoardGameHandler
	{
		// Token: 0x060032E4 RID: 13028
		void SwitchTurns();

		// Token: 0x060032E5 RID: 13029
		void DiceRoll(int roll);

		// Token: 0x060032E6 RID: 13030
		void Install();

		// Token: 0x060032E7 RID: 13031
		void Uninstall();

		// Token: 0x060032E8 RID: 13032
		void Activate();
	}
}
