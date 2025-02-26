﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MBHelpers;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F2 RID: 498
	public class CustomBattleMoraleModel : BattleMoraleModel
	{
		// Token: 0x06001BF6 RID: 7158 RVA: 0x00060C64 File Offset: 0x0005EE64
		[return: TupleElementNames(new string[]
		{
			"affectedSideMaxMoraleLoss",
			"affectorSideMaxMoraleGain"
		})]
		public override ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentIncapacitated(Agent affectedAgent, AgentState affectedAgentState, Agent affectorAgent, in KillingBlow killingBlow)
		{
			float battleImportance = affectedAgent.GetBattleImportance();
			Team team = affectedAgent.Team;
			BattleSideEnum battleSide = (team != null) ? team.Side : BattleSideEnum.None;
			float num = this.CalculateCasualtiesFactor(battleSide);
			SkillObject relevantSkillFromWeaponClass = WeaponComponentData.GetRelevantSkillFromWeaponClass((WeaponClass)killingBlow.WeaponClass);
			bool flag = relevantSkillFromWeaponClass == DefaultSkills.Bow || relevantSkillFromWeaponClass == DefaultSkills.Crossbow || relevantSkillFromWeaponClass == DefaultSkills.Throwing;
			bool flag2 = killingBlow.WeaponRecordWeaponFlags.HasAnyFlag(WeaponFlags.AffectsArea | WeaponFlags.AffectsAreaBig | WeaponFlags.MultiplePenetration);
			float num2 = 0.75f;
			if (flag2)
			{
				num2 = 0.25f;
				if (killingBlow.WeaponRecordWeaponFlags.HasAllFlags(WeaponFlags.Burning | WeaponFlags.MultiplePenetration))
				{
					num2 += num2 * 0.25f;
				}
			}
			else if (flag)
			{
				num2 = 0.5f;
			}
			num2 = Math.Max(0f, num2);
			FactoredNumber factoredNumber = new FactoredNumber(battleImportance * 3f * num2);
			FactoredNumber factoredNumber2 = new FactoredNumber(battleImportance * 4f * num2 * num);
			Formation formation = affectedAgent.Formation;
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(formation);
			if (activeBanner != null)
			{
				BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedMoraleShock, activeBanner, ref factoredNumber2);
			}
			return new ValueTuple<float, float>(MathF.Max(factoredNumber2.ResultNumber, 0f), MathF.Max(factoredNumber.ResultNumber, 0f));
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00060D94 File Offset: 0x0005EF94
		[return: TupleElementNames(new string[]
		{
			"affectedSideMaxMoraleLoss",
			"affectorSideMaxMoraleGain"
		})]
		public override ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentPanicked(Agent agent)
		{
			float battleImportance = agent.GetBattleImportance();
			Team team = agent.Team;
			BattleSideEnum battleSide = (team != null) ? team.Side : BattleSideEnum.None;
			float num = this.CalculateCasualtiesFactor(battleSide);
			float a = battleImportance * 2f;
			float num2 = battleImportance * num * 1.1f;
			if (agent.Character != null)
			{
				FactoredNumber factoredNumber = new FactoredNumber(num2);
				Formation formation = agent.Formation;
				BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(formation);
				if (activeBanner != null)
				{
					BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedMoraleShock, activeBanner, ref factoredNumber);
				}
				num2 = factoredNumber.ResultNumber;
			}
			return new ValueTuple<float, float>(MathF.Max(num2, 0f), MathF.Max(a, 0f));
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00060E33 File Offset: 0x0005F033
		public override float CalculateMoraleChangeToCharacter(Agent agent, float maxMoraleChange)
		{
			return maxMoraleChange / MathF.Max(1f, agent.Character.GetMoraleResistance());
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00060E4C File Offset: 0x0005F04C
		public override float GetEffectiveInitialMorale(Agent agent, float baseMorale)
		{
			return baseMorale;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00060E4F File Offset: 0x0005F04F
		public override bool CanPanicDueToMorale(Agent agent)
		{
			return true;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00060E54 File Offset: 0x0005F054
		public override float CalculateCasualtiesFactor(BattleSideEnum battleSide)
		{
			float num = 1f;
			if (Mission.Current != null && battleSide != BattleSideEnum.None)
			{
				float removedAgentRatioForSide = Mission.Current.GetRemovedAgentRatioForSide(battleSide);
				num += removedAgentRatioForSide * 2f;
				num = MathF.Max(0f, num);
			}
			return num;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00060E98 File Offset: 0x0005F098
		public override float GetAverageMorale(Formation formation)
		{
			float num = 0f;
			int num2 = 0;
			if (formation != null)
			{
				using (List<IFormationUnit>.Enumerator enumerator = formation.Arrangement.GetAllUnits().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent;
						if ((agent = (enumerator.Current as Agent)) != null && agent.IsActive() && agent.IsHuman && agent.IsAIControlled)
						{
							num2++;
							num += agent.GetMorale();
						}
					}
				}
			}
			if (num2 > 0)
			{
				return MBMath.ClampFloat(num / (float)num2, 0f, 100f);
			}
			return 0f;
		}
	}
}
