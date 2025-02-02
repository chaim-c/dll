using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003B3 RID: 947
	public class HideoutPhasedMissionController : MissionLogic
	{
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000D3003 File Offset: 0x000D1203
		public override MissionBehaviorType BehaviorType
		{
			get
			{
				return MissionBehaviorType.Logic;
			}
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000D3008 File Offset: 0x000D1208
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (!this._isNewlyPopulatedFormationGivenOrder)
			{
				foreach (Team team in base.Mission.Teams)
				{
					if (team.Side == BattleSideEnum.Defender)
					{
						foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
						{
							if (formation.CountOfUnits > 0)
							{
								formation.SetMovementOrder(MovementOrder.MovementOrderMove(formation.QuerySystem.MedianPosition));
								this._isNewlyPopulatedFormationGivenOrder = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000D30D4 File Offset: 0x000D12D4
		protected override void OnEndMission()
		{
			base.Mission.AreOrderGesturesEnabled_AdditionalCondition -= this.AreOrderGesturesEnabled_AdditionalCondition;
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000D30ED File Offset: 0x000D12ED
		public override void OnBehaviorInitialize()
		{
			this.ReadySpawnPointLogic();
			base.Mission.AreOrderGesturesEnabled_AdditionalCondition += this.AreOrderGesturesEnabled_AdditionalCondition;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000D310C File Offset: 0x000D130C
		public override void AfterStart()
		{
			base.AfterStart();
			MissionAgentSpawnLogic missionBehavior = base.Mission.GetMissionBehavior<MissionAgentSpawnLogic>();
			if (missionBehavior != null && this.IsPhasingInitialized)
			{
				missionBehavior.AddPhaseChangeAction(BattleSideEnum.Defender, new MissionAgentSpawnLogic.OnPhaseChangedDelegate(this.OnPhaseChanged));
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x000D3149 File Offset: 0x000D1349
		private bool IsPhasingInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000D314C File Offset: 0x000D134C
		private void ReadySpawnPointLogic()
		{
			List<GameEntity> list = Mission.Current.GetActiveEntitiesWithScriptComponentOfType<HideoutSpawnPointGroup>().ToList<GameEntity>();
			if (list.Count == 0)
			{
				return;
			}
			HideoutSpawnPointGroup[] array = new HideoutSpawnPointGroup[list.Count];
			foreach (GameEntity gameEntity in list)
			{
				HideoutSpawnPointGroup firstScriptOfType = gameEntity.GetFirstScriptOfType<HideoutSpawnPointGroup>();
				array[firstScriptOfType.PhaseNumber - 1] = firstScriptOfType;
			}
			List<HideoutSpawnPointGroup> list2 = array.ToList<HideoutSpawnPointGroup>();
			list2.RemoveAt(0);
			for (int i = 0; i < 3; i++)
			{
				list2.RemoveAt(MBRandom.RandomInt(list2.Count));
			}
			this._spawnPointFrames = new Stack<MatrixFrame[]>();
			for (int j = 0; j < array.Length; j++)
			{
				if (!list2.Contains(array[j]))
				{
					this._spawnPointFrames.Push(array[j].GetSpawnPointFrames());
					Debug.Print("Spawn " + array[j].PhaseNumber + " is active.", 0, Debug.DebugColor.Green, 64UL);
				}
				array[j].RemoveWithAllChildren();
			}
			this.CreateSpawnPoints();
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000D3270 File Offset: 0x000D1470
		private void CreateSpawnPoints()
		{
			MatrixFrame[] array = this._spawnPointFrames.Pop();
			this._spawnPoints = new GameEntity[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsIdentity)
				{
					this._spawnPoints[i] = GameEntity.CreateEmpty(base.Mission.Scene, true);
					this._spawnPoints[i].SetGlobalFrame(array[i]);
					this._spawnPoints[i].AddTag("defender_" + ((FormationClass)i).GetName().ToLower());
				}
			}
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000D3304 File Offset: 0x000D1504
		private void OnPhaseChanged()
		{
			if (this._spawnPointFrames.Count == 0)
			{
				Debug.FailedAssert("No position left.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\HideoutPhasedMissionController.cs", "OnPhaseChanged", 142);
				return;
			}
			for (int i = 0; i < this._spawnPoints.Length; i++)
			{
				if (!(this._spawnPoints[i] == null))
				{
					this._spawnPoints[i].Remove(78);
				}
			}
			this.CreateSpawnPoints();
			this._isNewlyPopulatedFormationGivenOrder = false;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000D3377 File Offset: 0x000D1577
		private bool AreOrderGesturesEnabled_AdditionalCondition()
		{
			return false;
		}

		// Token: 0x04001606 RID: 5638
		public const int PhaseCount = 4;

		// Token: 0x04001607 RID: 5639
		private GameEntity[] _spawnPoints;

		// Token: 0x04001608 RID: 5640
		private Stack<MatrixFrame[]> _spawnPointFrames;

		// Token: 0x04001609 RID: 5641
		private bool _isNewlyPopulatedFormationGivenOrder = true;
	}
}
