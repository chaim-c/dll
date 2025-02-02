using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace SandBox.GameComponents
{
	// Token: 0x02000099 RID: 153
	public class SandboxBattleSpawnModel : BattleSpawnModel
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x0002A69C File Offset: 0x0002889C
		public override void OnMissionStart()
		{
			MissionReinforcementsHelper.OnMissionStart();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002A6A3 File Offset: 0x000288A3
		public override void OnMissionEnd()
		{
			MissionReinforcementsHelper.OnMissionEnd();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002A6AC File Offset: 0x000288AC
		[return: TupleElementNames(new string[]
		{
			"origin",
			"formationIndex"
		})]
		public override List<ValueTuple<IAgentOriginBase, int>> GetInitialSpawnAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins)
		{
			List<ValueTuple<IAgentOriginBase, int>> list = new List<ValueTuple<IAgentOriginBase, int>>();
			SandboxBattleSpawnModel.FormationOrderOfBattleConfiguration[] array;
			if (SandboxBattleSpawnModel.GetOrderOfBattleConfigurationsForFormations(battleSide, troopOrigins, out array))
			{
				using (List<IAgentOriginBase>.Enumerator enumerator = troopOrigins.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IAgentOriginBase agentOriginBase = enumerator.Current;
						SandboxBattleSpawnModel.OrderOfBattleInnerClassType orderOfBattleInnerClassType;
						FormationClass formationClass = SandboxBattleSpawnModel.FindBestOrderOfBattleFormationClassAssignmentForTroop(battleSide, agentOriginBase, array, out orderOfBattleInnerClassType);
						ValueTuple<IAgentOriginBase, int> item = new ValueTuple<IAgentOriginBase, int>(agentOriginBase, (int)formationClass);
						list.Add(item);
						if (orderOfBattleInnerClassType == SandboxBattleSpawnModel.OrderOfBattleInnerClassType.PrimaryClass)
						{
							SandboxBattleSpawnModel.FormationOrderOfBattleConfiguration[] array2 = array;
							FormationClass formationClass2 = formationClass;
							array2[(int)formationClass2].PrimaryClassTroopCount = array2[(int)formationClass2].PrimaryClassTroopCount + 1;
						}
						else if (orderOfBattleInnerClassType == SandboxBattleSpawnModel.OrderOfBattleInnerClassType.SecondaryClass)
						{
							SandboxBattleSpawnModel.FormationOrderOfBattleConfiguration[] array3 = array;
							FormationClass formationClass3 = formationClass;
							array3[(int)formationClass3].SecondaryClassTroopCount = array3[(int)formationClass3].SecondaryClassTroopCount + 1;
						}
					}
					return list;
				}
			}
			foreach (IAgentOriginBase agentOriginBase2 in troopOrigins)
			{
				ValueTuple<IAgentOriginBase, int> item2 = new ValueTuple<IAgentOriginBase, int>(agentOriginBase2, (int)Mission.Current.GetAgentTroopClass(battleSide, agentOriginBase2.Troop));
				list.Add(item2);
			}
			return list;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0002A7B0 File Offset: 0x000289B0
		[return: TupleElementNames(new string[]
		{
			"origin",
			"formationIndex"
		})]
		public override List<ValueTuple<IAgentOriginBase, int>> GetReinforcementAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins)
		{
			return MissionReinforcementsHelper.GetReinforcementAssignments(battleSide, troopOrigins);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0002A7BC File Offset: 0x000289BC
		private static bool GetOrderOfBattleConfigurationsForFormations(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins, out SandboxBattleSpawnModel.FormationOrderOfBattleConfiguration[] formationOrderOfBattleConfigurations)
		{
			formationOrderOfBattleConfigurations = new SandboxBattleSpawnModel.FormationOrderOfBattleConfiguration[8];
			Campaign campaign = Campaign.Current;
			OrderOfBattleCampaignBehavior orderOfBattleCampaignBehavior = (campaign != null) ? campaign.GetCampaignBehavior<OrderOfBattleCampaignBehavior>() : null;
			if (orderOfBattleCampaignBehavior == null)
			{
				return false;
			}
			for (int i = 0; i < 8; i++)
			{
				if (orderOfBattleCampaignBehavior.GetFormationDataAtIndex(i, Mission.Current.IsSiegeBattle) == null)
				{
					return false;
				}
			}
			int[] array = SandboxBattleSpawnModel.CalculateTroopCountsPerDefaultFormation(battleSide, troopOrigins);
			for (int j = 0; j < 8; j++)
			{
				OrderOfBattleCampaignBehavior.OrderOfBattleFormationData formationDataAtIndex = orderOfBattleCampaignBehavior.GetFormationDataAtIndex(j, Mission.Current.IsSiegeBattle);
				formationOrderOfBattleConfigurations[j].OOBFormationClass = formationDataAtIndex.FormationClass;
				formationOrderOfBattleConfigurations[j].Commander = formationDataAtIndex.Commander;
				FormationClass formationClass = FormationClass.NumberOfAllFormations;
				FormationClass formationClass2 = FormationClass.NumberOfAllFormations;
				switch (formationDataAtIndex.FormationClass)
				{
				case DeploymentFormationClass.Infantry:
					formationClass = FormationClass.Infantry;
					break;
				case DeploymentFormationClass.Ranged:
					formationClass = FormationClass.Ranged;
					break;
				case DeploymentFormationClass.Cavalry:
					formationClass = FormationClass.Cavalry;
					break;
				case DeploymentFormationClass.HorseArcher:
					formationClass = FormationClass.HorseArcher;
					break;
				case DeploymentFormationClass.InfantryAndRanged:
					formationClass = FormationClass.Infantry;
					formationClass2 = FormationClass.Ranged;
					break;
				case DeploymentFormationClass.CavalryAndHorseArcher:
					formationClass = FormationClass.Cavalry;
					formationClass2 = FormationClass.HorseArcher;
					break;
				}
				formationOrderOfBattleConfigurations[j].PrimaryFormationClass = formationClass;
				if (formationClass != FormationClass.NumberOfAllFormations)
				{
					formationOrderOfBattleConfigurations[j].PrimaryClassDesiredTroopCount = (int)Math.Ceiling((double)((float)array[(int)formationClass] * ((float)formationDataAtIndex.PrimaryClassWeight / 100f)));
				}
				formationOrderOfBattleConfigurations[j].SecondaryFormationClass = formationClass2;
				if (formationClass2 != FormationClass.NumberOfAllFormations)
				{
					formationOrderOfBattleConfigurations[j].SecondaryClassDesiredTroopCount = (int)Math.Ceiling((double)((float)array[(int)formationClass2] * ((float)formationDataAtIndex.SecondaryClassWeight / 100f)));
				}
			}
			return true;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0002A930 File Offset: 0x00028B30
		private static int[] CalculateTroopCountsPerDefaultFormation(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins)
		{
			int[] array = new int[4];
			foreach (IAgentOriginBase agentOriginBase in troopOrigins)
			{
				FormationClass formationClass = Mission.Current.GetAgentTroopClass(battleSide, agentOriginBase.Troop).DefaultClass();
				array[(int)formationClass]++;
			}
			return array;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0002A9A4 File Offset: 0x00028BA4
		private static FormationClass FindBestOrderOfBattleFormationClassAssignmentForTroop(BattleSideEnum battleSide, IAgentOriginBase origin, SandboxBattleSpawnModel.FormationOrderOfBattleConfiguration[] formationOrderOfBattleConfigurations, out SandboxBattleSpawnModel.OrderOfBattleInnerClassType bestClassInnerClassType)
		{
			FormationClass formationClass = Mission.Current.GetAgentTroopClass(battleSide, origin.Troop).DefaultClass();
			FormationClass result = formationClass;
			float num = float.MinValue;
			bestClassInnerClassType = SandboxBattleSpawnModel.OrderOfBattleInnerClassType.None;
			for (int i = 0; i < 8; i++)
			{
				CharacterObject characterObject;
				if (origin.Troop.IsHero && (characterObject = (origin.Troop as CharacterObject)) != null && characterObject.HeroObject == formationOrderOfBattleConfigurations[i].Commander)
				{
					result = (FormationClass)i;
					bestClassInnerClassType = SandboxBattleSpawnModel.OrderOfBattleInnerClassType.None;
					break;
				}
				if (formationClass == formationOrderOfBattleConfigurations[i].PrimaryFormationClass)
				{
					float num2 = (float)formationOrderOfBattleConfigurations[i].PrimaryClassDesiredTroopCount;
					float num3 = (float)formationOrderOfBattleConfigurations[i].PrimaryClassTroopCount;
					float num4 = 1f - num3 / (num2 + 1f);
					if (num4 > num)
					{
						result = (FormationClass)i;
						bestClassInnerClassType = SandboxBattleSpawnModel.OrderOfBattleInnerClassType.PrimaryClass;
						num = num4;
					}
				}
				else if (formationClass == formationOrderOfBattleConfigurations[i].SecondaryFormationClass)
				{
					float num5 = (float)formationOrderOfBattleConfigurations[i].SecondaryClassDesiredTroopCount;
					float num6 = (float)formationOrderOfBattleConfigurations[i].SecondaryClassTroopCount;
					float num7 = 1f - num6 / (num5 + 1f);
					if (num7 > num)
					{
						result = (FormationClass)i;
						bestClassInnerClassType = SandboxBattleSpawnModel.OrderOfBattleInnerClassType.SecondaryClass;
						num = num7;
					}
				}
			}
			return result;
		}

		// Token: 0x02000177 RID: 375
		private enum OrderOfBattleInnerClassType
		{
			// Token: 0x0400069D RID: 1693
			None,
			// Token: 0x0400069E RID: 1694
			PrimaryClass,
			// Token: 0x0400069F RID: 1695
			SecondaryClass
		}

		// Token: 0x02000178 RID: 376
		private struct FormationOrderOfBattleConfiguration
		{
			// Token: 0x040006A0 RID: 1696
			public DeploymentFormationClass OOBFormationClass;

			// Token: 0x040006A1 RID: 1697
			public FormationClass PrimaryFormationClass;

			// Token: 0x040006A2 RID: 1698
			public int PrimaryClassTroopCount;

			// Token: 0x040006A3 RID: 1699
			public int PrimaryClassDesiredTroopCount;

			// Token: 0x040006A4 RID: 1700
			public FormationClass SecondaryFormationClass;

			// Token: 0x040006A5 RID: 1701
			public int SecondaryClassTroopCount;

			// Token: 0x040006A6 RID: 1702
			public int SecondaryClassDesiredTroopCount;

			// Token: 0x040006A7 RID: 1703
			public Hero Commander;
		}
	}
}
