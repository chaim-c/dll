using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B0 RID: 432
	[ScriptingInterfaceBase]
	internal interface IMBTeam
	{
		// Token: 0x06001792 RID: 6034
		[EngineMethod("is_enemy", false)]
		bool IsEnemy(UIntPtr missionPointer, int teamIndex, int otherTeamIndex);

		// Token: 0x06001793 RID: 6035
		[EngineMethod("set_is_enemy", false)]
		void SetIsEnemy(UIntPtr missionPointer, int teamIndex, int otherTeamIndex, bool isEnemy);
	}
}
