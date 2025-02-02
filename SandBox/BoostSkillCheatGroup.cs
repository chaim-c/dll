using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace SandBox
{
	// Token: 0x02000010 RID: 16
	public class BoostSkillCheatGroup : GameplayCheatGroup
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000038A4 File Offset: 0x00001AA4
		public override IEnumerable<GameplayCheatBase> GetCheats()
		{
			foreach (SkillObject skillToBoost in Skills.All)
			{
				yield return new BoostSkillCheatGroup.BoostSkillCheeat(skillToBoost);
			}
			List<SkillObject>.Enumerator enumerator = default(List<SkillObject>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000038AD File Offset: 0x00001AAD
		public override TextObject GetName()
		{
			return new TextObject("{=SFn4UFd4}Boost Skill", null);
		}

		// Token: 0x020000DF RID: 223
		public class BoostSkillCheeat : GameplayCheatItem
		{
			// Token: 0x06000AFD RID: 2813 RVA: 0x0004FB0B File Offset: 0x0004DD0B
			public BoostSkillCheeat(SkillObject skillToBoost)
			{
				this._skillToBoost = skillToBoost;
			}

			// Token: 0x06000AFE RID: 2814 RVA: 0x0004FB1C File Offset: 0x0004DD1C
			public override void ExecuteCheat()
			{
				int num = 50;
				if (Hero.MainHero.GetSkillValue(this._skillToBoost) + num > 330)
				{
					num = 330 - Hero.MainHero.GetSkillValue(this._skillToBoost);
				}
				Hero.MainHero.HeroDeveloper.ChangeSkillLevel(this._skillToBoost, num, false);
			}

			// Token: 0x06000AFF RID: 2815 RVA: 0x0004FB73 File Offset: 0x0004DD73
			public override TextObject GetName()
			{
				return this._skillToBoost.GetName();
			}

			// Token: 0x04000448 RID: 1096
			private readonly SkillObject _skillToBoost;
		}
	}
}
