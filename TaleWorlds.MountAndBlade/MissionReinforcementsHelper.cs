using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200028D RID: 653
	public static class MissionReinforcementsHelper
	{
		// Token: 0x06002236 RID: 8758 RVA: 0x0007CC58 File Offset: 0x0007AE58
		public static void OnMissionStart()
		{
			Mission mission = Mission.Current;
			MissionReinforcementsHelper._reinforcementFormationsData = new MissionReinforcementsHelper.ReinforcementFormationData[mission.Teams.Count, 8];
			foreach (Team team in mission.Teams)
			{
				for (int i = 0; i < 8; i++)
				{
					MissionReinforcementsHelper._reinforcementFormationsData[team.TeamIndex, i] = new MissionReinforcementsHelper.ReinforcementFormationData();
				}
			}
			MissionReinforcementsHelper._localInitTime = 0U;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0007CCE8 File Offset: 0x0007AEE8
		[return: TupleElementNames(new string[]
		{
			"origin",
			"formationIndex"
		})]
		public unsafe static List<ValueTuple<IAgentOriginBase, int>> GetReinforcementAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins)
		{
			Mission mission = Mission.Current;
			MissionReinforcementsHelper._localInitTime += 1U;
			List<ValueTuple<IAgentOriginBase, int>> list = new List<ValueTuple<IAgentOriginBase, int>>();
			PriorityQueue<MissionReinforcementsHelper.ReinforcementFormationPriority, Formation> priorityQueue = new PriorityQueue<MissionReinforcementsHelper.ReinforcementFormationPriority, Formation>(new MissionReinforcementsHelper.ReinforcementFormationPreferenceComparer());
			foreach (IAgentOriginBase agentOriginBase in troopOrigins)
			{
				priorityQueue.Clear();
				FormationClass agentTroopClass = Mission.Current.GetAgentTroopClass(battleSide, agentOriginBase.Troop);
				bool isPlayerSide = Mission.Current.PlayerTeam.Side == battleSide;
				Team agentTeam = Mission.GetAgentTeam(agentOriginBase, isPlayerSide);
				foreach (Formation formation in agentTeam.FormationsIncludingEmpty)
				{
					int formationIndex = (int)formation.FormationIndex;
					if (formation.GetReadonlyMovementOrderReference()->OrderEnum != MovementOrder.MovementOrderEnum.Retreat)
					{
						MissionReinforcementsHelper.ReinforcementFormationData reinforcementFormationData = MissionReinforcementsHelper._reinforcementFormationsData[agentTeam.TeamIndex, formationIndex];
						if (!reinforcementFormationData.IsInitialized(MissionReinforcementsHelper._localInitTime))
						{
							reinforcementFormationData.Initialize(formation, MissionReinforcementsHelper._localInitTime);
						}
						MissionReinforcementsHelper.ReinforcementFormationPriority priority = reinforcementFormationData.GetPriority(agentTroopClass);
						if (priorityQueue.IsEmpty<KeyValuePair<MissionReinforcementsHelper.ReinforcementFormationPriority, Formation>>() || priority >= priorityQueue.Peek().Key)
						{
							priorityQueue.Enqueue(priority, formation);
						}
					}
				}
				Formation formation2 = MissionReinforcementsHelper.FindBestFormationAmong(priorityQueue);
				if (formation2 == null)
				{
					formation2 = agentTeam.GetFormation(agentTroopClass);
				}
				int formationIndex2 = (int)formation2.FormationIndex;
				MissionReinforcementsHelper._reinforcementFormationsData[formation2.Team.TeamIndex, formationIndex2].AddProspectiveTroop(agentTroopClass);
				ValueTuple<IAgentOriginBase, int> item = new ValueTuple<IAgentOriginBase, int>(agentOriginBase, formationIndex2);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0007CEBC File Offset: 0x0007B0BC
		public static void OnMissionEnd()
		{
			MissionReinforcementsHelper._reinforcementFormationsData = null;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0007CEC4 File Offset: 0x0007B0C4
		private static Formation FindBestFormationAmong(PriorityQueue<MissionReinforcementsHelper.ReinforcementFormationPriority, Formation> matchingFormations)
		{
			Formation formation = null;
			float num = float.MinValue;
			if (!matchingFormations.IsEmpty<KeyValuePair<MissionReinforcementsHelper.ReinforcementFormationPriority, Formation>>())
			{
				int key = (int)matchingFormations.Peek().Key;
				foreach (KeyValuePair<MissionReinforcementsHelper.ReinforcementFormationPriority, Formation> keyValuePair in matchingFormations)
				{
					int key2 = (int)keyValuePair.Key;
					if (key2 < key)
					{
						break;
					}
					Formation value = keyValuePair.Value;
					if (key2 == 3 || key2 == 4)
					{
						if (formation == null || value.FormationIndex < formation.FormationIndex)
						{
							formation = value;
						}
					}
					else
					{
						float formationReinforcementScore = MissionReinforcementsHelper.GetFormationReinforcementScore(value);
						if (formationReinforcementScore > num)
						{
							num = formationReinforcementScore;
							formation = value;
						}
					}
				}
			}
			return formation;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0007CF7C File Offset: 0x0007B17C
		private static float GetFormationReinforcementScore(Formation formation)
		{
			Mission mission = Mission.Current;
			float num = (float)formation.CountOfUnits / (float)Math.Max(1, formation.Team.ActiveAgents.Count);
			float num2 = MathF.Max(0f, 1f - num);
			float num3 = 0f;
			BattleSideEnum side = formation.Team.Side;
			if (formation.HasBeenPositioned && mission.DeploymentPlan.IsPlanMadeForBattleSide(side, DeploymentPlanType.Reinforcement))
			{
				Vec2 asVec = mission.DeploymentPlan.GetMeanPositionOfPlan(side, DeploymentPlanType.Reinforcement).AsVec2;
				float num4 = formation.CurrentPosition.DistanceSquared(asVec);
				float num5 = MathF.Min(1f, num4 / 62500f);
				num3 = MathF.Max(0f, 1f - num5);
			}
			return 0.6f * num2 + 0.4f * num3;
		}

		// Token: 0x04000CC7 RID: 3271
		private const float DominantClassThreshold = 0.5f;

		// Token: 0x04000CC8 RID: 3272
		private const float CommonClassThreshold = 0.25f;

		// Token: 0x04000CC9 RID: 3273
		private static uint _localInitTime;

		// Token: 0x04000CCA RID: 3274
		private static MissionReinforcementsHelper.ReinforcementFormationData[,] _reinforcementFormationsData;

		// Token: 0x0200053F RID: 1343
		public enum ReinforcementFormationPriority
		{
			// Token: 0x04001CAE RID: 7342
			Dominant = 6,
			// Token: 0x04001CAF RID: 7343
			Common = 5,
			// Token: 0x04001CB0 RID: 7344
			EmptyRepresentativeMatch = 4,
			// Token: 0x04001CB1 RID: 7345
			EmptyNoMatch = 3,
			// Token: 0x04001CB2 RID: 7346
			AlternativeDominant = 2,
			// Token: 0x04001CB3 RID: 7347
			AlternativeCommon = 1,
			// Token: 0x04001CB4 RID: 7348
			Default = 0
		}

		// Token: 0x02000540 RID: 1344
		public class ReinforcementFormationPreferenceComparer : IComparer<MissionReinforcementsHelper.ReinforcementFormationPriority>
		{
			// Token: 0x06003919 RID: 14617 RVA: 0x000E42EC File Offset: 0x000E24EC
			public int Compare(MissionReinforcementsHelper.ReinforcementFormationPriority left, MissionReinforcementsHelper.ReinforcementFormationPriority right)
			{
				if (right < left)
				{
					return 1;
				}
				if (right > left)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x02000541 RID: 1345
		public class ReinforcementFormationData
		{
			// Token: 0x0600391B RID: 14619 RVA: 0x000E4312 File Offset: 0x000E2512
			public ReinforcementFormationData()
			{
				this._initTime = 0U;
				this._expectedTroopCountPerClass = new int[4];
				this._expectedTotalTroopCount = 0;
				this._isClassified = false;
				this._representativeClass = FormationClass.NumberOfAllFormations;
				this._troopClasses = new bool[4];
			}

			// Token: 0x0600391C RID: 14620 RVA: 0x000E4350 File Offset: 0x000E2550
			public void Initialize(Formation formation, uint initTime)
			{
				int countOfUnits = formation.CountOfUnits;
				this._expectedTroopCountPerClass[0] = (int)(formation.QuerySystem.InfantryUnitRatio * (float)countOfUnits);
				this._expectedTroopCountPerClass[1] = (int)(formation.QuerySystem.RangedUnitRatio * (float)countOfUnits);
				this._expectedTroopCountPerClass[2] = (int)(formation.QuerySystem.CavalryUnitRatio * (float)countOfUnits);
				this._expectedTroopCountPerClass[3] = (int)(formation.QuerySystem.RangedCavalryUnitRatio * (float)countOfUnits);
				this._expectedTotalTroopCount = countOfUnits;
				this._isClassified = false;
				this._representativeClass = formation.RepresentativeClass;
				this._initTime = initTime;
			}

			// Token: 0x0600391D RID: 14621 RVA: 0x000E43E4 File Offset: 0x000E25E4
			public void AddProspectiveTroop(FormationClass troopClass)
			{
				this._expectedTroopCountPerClass[(int)troopClass]++;
				this._expectedTotalTroopCount++;
				this._isClassified = false;
			}

			// Token: 0x0600391E RID: 14622 RVA: 0x000E4419 File Offset: 0x000E2619
			public bool IsInitialized(uint initTime)
			{
				return initTime == this._initTime;
			}

			// Token: 0x0600391F RID: 14623 RVA: 0x000E4424 File Offset: 0x000E2624
			public MissionReinforcementsHelper.ReinforcementFormationPriority GetPriority(FormationClass troopClass)
			{
				if (this._expectedTotalTroopCount == 0)
				{
					if (this._representativeClass == troopClass)
					{
						return MissionReinforcementsHelper.ReinforcementFormationPriority.EmptyRepresentativeMatch;
					}
					return MissionReinforcementsHelper.ReinforcementFormationPriority.EmptyNoMatch;
				}
				else
				{
					if (!this._isClassified)
					{
						this.Classify();
					}
					bool flag;
					if (this.HasTroopClass(troopClass, out flag))
					{
						if (!flag)
						{
							return MissionReinforcementsHelper.ReinforcementFormationPriority.Common;
						}
						return MissionReinforcementsHelper.ReinforcementFormationPriority.Dominant;
					}
					else
					{
						FormationClass troopClass2 = troopClass.AlternativeClass();
						if (!this.HasTroopClass(troopClass2, out flag))
						{
							return MissionReinforcementsHelper.ReinforcementFormationPriority.Default;
						}
						if (!flag)
						{
							return MissionReinforcementsHelper.ReinforcementFormationPriority.AlternativeCommon;
						}
						return MissionReinforcementsHelper.ReinforcementFormationPriority.AlternativeDominant;
					}
				}
			}

			// Token: 0x06003920 RID: 14624 RVA: 0x000E4484 File Offset: 0x000E2684
			private void Classify()
			{
				if (this._expectedTotalTroopCount > 0)
				{
					int num = -1;
					int num2 = 4;
					for (int i = 0; i < num2; i++)
					{
						float num3 = (float)this._expectedTroopCountPerClass[i] / (float)this._expectedTotalTroopCount;
						this._troopClasses[i] = (num3 >= 0.25f);
						if (num3 > 0.5f)
						{
							num = i;
							break;
						}
					}
					if (num >= 0)
					{
						this.ResetClassAssignments();
						this._troopClasses[num] = true;
					}
				}
				else
				{
					this.ResetClassAssignments();
				}
				this._isClassified = true;
			}

			// Token: 0x06003921 RID: 14625 RVA: 0x000E4500 File Offset: 0x000E2700
			private bool HasTroopClass(FormationClass troopClass, out bool isDominant)
			{
				int num = 0;
				for (int i = 0; i < 4; i++)
				{
					if (i == (int)troopClass && this._troopClasses[i])
					{
						num++;
					}
				}
				isDominant = (num == 1);
				return num >= 1;
			}

			// Token: 0x06003922 RID: 14626 RVA: 0x000E453C File Offset: 0x000E273C
			private void ResetClassAssignments()
			{
				int num = 4;
				for (int i = 0; i < num; i++)
				{
					this._troopClasses[i] = false;
				}
			}

			// Token: 0x04001CB5 RID: 7349
			private uint _initTime;

			// Token: 0x04001CB6 RID: 7350
			private bool _isClassified;

			// Token: 0x04001CB7 RID: 7351
			private int[] _expectedTroopCountPerClass;

			// Token: 0x04001CB8 RID: 7352
			private int _expectedTotalTroopCount;

			// Token: 0x04001CB9 RID: 7353
			private bool[] _troopClasses;

			// Token: 0x04001CBA RID: 7354
			private FormationClass _representativeClass;
		}
	}
}
