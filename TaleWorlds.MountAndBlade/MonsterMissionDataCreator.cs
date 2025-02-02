using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002CD RID: 717
	public class MonsterMissionDataCreator : IMonsterMissionDataCreator
	{
		// Token: 0x060027B3 RID: 10163 RVA: 0x0009927A File Offset: 0x0009747A
		IMonsterMissionData IMonsterMissionDataCreator.CreateMonsterMissionData(Monster monster)
		{
			return new MonsterMissionData(monster);
		}
	}
}
