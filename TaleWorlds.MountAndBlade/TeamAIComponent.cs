using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200017F RID: 383
	public abstract class TeamAIComponent
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0004A950 File Offset: 0x00048B50
		public MBReadOnlyList<StrategicArea> StrategicAreas
		{
			get
			{
				return this._strategicAreas;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0004A958 File Offset: 0x00048B58
		public bool HasStrategicAreas
		{
			get
			{
				return !this._strategicAreas.IsEmpty<StrategicArea>();
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0004A968 File Offset: 0x00048B68
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x0004A970 File Offset: 0x00048B70
		public bool IsDefenseApplicable { get; private set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0004A979 File Offset: 0x00048B79
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x0004A981 File Offset: 0x00048B81
		public bool GetIsFirstTacticChosen { get; private set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0004A98A File Offset: 0x00048B8A
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x0004A992 File Offset: 0x00048B92
		private protected TacticComponent CurrentTactic
		{
			protected get
			{
				return this._currentTactic;
			}
			private set
			{
				TacticComponent currentTactic = this._currentTactic;
				if (currentTactic != null)
				{
					currentTactic.OnCancel();
				}
				this._currentTactic = value;
				if (this._currentTactic != null)
				{
					this._currentTactic.OnApply();
					this._currentTactic.TickOccasionally();
				}
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004A9CC File Offset: 0x00048BCC
		protected TeamAIComponent(Mission currentMission, Team currentTeam, float thinkTimerTime, float applyTimerTime)
		{
			this.Mission = currentMission;
			this.Team = currentTeam;
			this._thinkTimer = new Timer(this.Mission.CurrentTime, thinkTimerTime, true);
			this._applyTimer = new Timer(this.Mission.CurrentTime, applyTimerTime, true);
			this._occasionalTickTime = applyTimerTime;
			this._availableTactics = new List<TacticComponent>();
			this.TacticalPositions = currentMission.ActiveMissionObjects.FindAllWithType<TacticalPosition>().ToList<TacticalPosition>();
			this.TacticalRegions = currentMission.ActiveMissionObjects.FindAllWithType<TacticalRegion>().ToList<TacticalRegion>();
			this._strategicAreas = (from amo in currentMission.ActiveMissionObjects.Where(delegate(MissionObject amo)
			{
				StrategicArea strategicArea;
				return (strategicArea = (amo as StrategicArea)) != null && strategicArea.IsActive && strategicArea.IsUsableBy(this.Team.Side);
			})
			select amo as StrategicArea).ToMBList<StrategicArea>();
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004AAA3 File Offset: 0x00048CA3
		public void AddStrategicArea(StrategicArea strategicArea)
		{
			this._strategicAreas.Add(strategicArea);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004AAB1 File Offset: 0x00048CB1
		public void RemoveStrategicArea(StrategicArea strategicArea)
		{
			if (this.Team.DetachmentManager.ContainsDetachment(strategicArea))
			{
				this.Team.DetachmentManager.DestroyDetachment(strategicArea);
			}
			this._strategicAreas.Remove(strategicArea);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0004AAE4 File Offset: 0x00048CE4
		public void RemoveAllStrategicAreas()
		{
			foreach (StrategicArea strategicArea in this._strategicAreas)
			{
				if (this.Team.DetachmentManager.ContainsDetachment(strategicArea))
				{
					this.Team.DetachmentManager.DestroyDetachment(strategicArea);
				}
			}
			this._strategicAreas.Clear();
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004AB60 File Offset: 0x00048D60
		public void AddTacticOption(TacticComponent tacticOption)
		{
			this._availableTactics.Add(tacticOption);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004AB70 File Offset: 0x00048D70
		public void RemoveTacticOption(Type tacticType)
		{
			this._availableTactics.RemoveAll((TacticComponent at) => tacticType == at.GetType());
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0004ABA2 File Offset: 0x00048DA2
		public void ClearTacticOptions()
		{
			this._availableTactics.Clear();
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0004ABAF File Offset: 0x00048DAF
		[Conditional("DEBUG")]
		public void AssertTeam(Team team)
		{
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0004ABB1 File Offset: 0x00048DB1
		public void NotifyTacticalDecision(in TacticalDecision decision)
		{
			TeamAIComponent.TacticalDecisionDelegate onNotifyTacticalDecision = this.OnNotifyTacticalDecision;
			if (onNotifyTacticalDecision == null)
			{
				return;
			}
			onNotifyTacticalDecision(decision);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0004ABC4 File Offset: 0x00048DC4
		public virtual void OnDeploymentFinished()
		{
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0004ABC8 File Offset: 0x00048DC8
		public void OnMissionEnded()
		{
			MBDebug.Print("Mission end received by teamAI", 0, Debug.DebugColor.White, 17592186044416UL);
			foreach (Formation formation in this.Team.FormationsIncludingSpecialAndEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					foreach (UsableMachine usable in formation.GetUsedMachines().ToList<UsableMachine>())
					{
						formation.StopUsingMachine(usable, false);
					}
				}
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0004AC80 File Offset: 0x00048E80
		public void ResetTactic(bool keepCurrentTactic = true)
		{
			if (!keepCurrentTactic)
			{
				this.CurrentTactic = null;
			}
			this._thinkTimer.Reset(this.Mission.CurrentTime);
			this._applyTimer.Reset(this.Mission.CurrentTime);
			this.MakeDecision();
			this.TickOccasionally();
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0004ACD0 File Offset: 0x00048ED0
		protected internal virtual void Tick(float dt)
		{
			if (this.Team.BodyGuardFormation != null && this.Team.BodyGuardFormation.CountOfUnits > 0 && (this.Team.GeneralsFormation == null || this.Team.GeneralsFormation.CountOfUnits == 0))
			{
				this.Team.BodyGuardFormation.AI.ResetBehaviorWeights();
				this.Team.BodyGuardFormation.AI.SetBehaviorWeight<BehaviorCharge>(1f);
			}
			if (this._nextTacticChooseTime.IsPast)
			{
				this.MakeDecision();
				this._nextTacticChooseTime = MissionTime.SecondsFromNow(5f);
			}
			if (this._nextOccasionalTickTime.IsPast)
			{
				this.TickOccasionally();
				this._nextOccasionalTickTime = MissionTime.SecondsFromNow(this._occasionalTickTime);
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004AD94 File Offset: 0x00048F94
		public void CheckIsDefenseApplicable()
		{
			if (this.Team.Side != BattleSideEnum.Defender)
			{
				this.IsDefenseApplicable = false;
				return;
			}
			int memberCount = this.Team.QuerySystem.MemberCount;
			float maxUnderRangedAttackRatio = this.Team.QuerySystem.MaxUnderRangedAttackRatio;
			float num = (float)memberCount * maxUnderRangedAttackRatio;
			int deathByRangedCount = this.Team.QuerySystem.DeathByRangedCount;
			int deathCount = this.Team.QuerySystem.DeathCount;
			float num2 = MBMath.ClampFloat((num + (float)deathByRangedCount) / (float)(memberCount + deathCount), 0.05f, 1f);
			int enemyUnitCount = this.Team.QuerySystem.EnemyUnitCount;
			float num3 = 0f;
			int num4 = 0;
			int num5 = 0;
			foreach (Team team in this.Mission.Teams)
			{
				if (this.Team.IsEnemyOf(team))
				{
					TeamQuerySystem querySystem = team.QuerySystem;
					num4 += querySystem.DeathByRangedCount;
					num5 += querySystem.DeathCount;
					num3 += ((enemyUnitCount == 0) ? 0f : (querySystem.MaxUnderRangedAttackRatio * ((float)querySystem.MemberCount / (float)((enemyUnitCount > 0) ? enemyUnitCount : 1))));
				}
			}
			float num6 = (float)enemyUnitCount * num3;
			int num7 = enemyUnitCount + num5;
			float num8 = MBMath.ClampFloat((num6 + (float)num4) / (float)((num7 > 0) ? num7 : 1), 0.05f, 1f);
			float num9 = MathF.Pow(num2 / num8, 3f * (this.Team.QuerySystem.EnemyRangedRatio + this.Team.QuerySystem.EnemyRangedCavalryRatio));
			this.IsDefenseApplicable = (num9 <= 1.5f);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0004AF4C File Offset: 0x0004914C
		public void OnTacticAppliedForFirstTime()
		{
			this.GetIsFirstTacticChosen = false;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0004AF58 File Offset: 0x00049158
		private void MakeDecision()
		{
			List<TacticComponent> availableTactics = this._availableTactics;
			if ((this.Mission.CurrentState != Mission.State.Continuing && availableTactics.Count == 0) || !this.Team.HasAnyFormationsIncludingSpecialThatIsNotEmpty())
			{
				return;
			}
			bool flag = true;
			foreach (Team team in this.Mission.Teams)
			{
				if (team.IsEnemyOf(this.Team) && team.HasAnyFormationsIncludingSpecialThatIsNotEmpty())
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				if (this.Mission.MissionEnded)
				{
					return;
				}
				if (!(this.CurrentTactic is TacticCharge))
				{
					foreach (TacticComponent tacticComponent in availableTactics)
					{
						if (tacticComponent is TacticCharge)
						{
							if (this.CurrentTactic == null)
							{
								this.GetIsFirstTacticChosen = true;
							}
							this.CurrentTactic = tacticComponent;
							break;
						}
					}
					if (!(this.CurrentTactic is TacticCharge))
					{
						if (this.CurrentTactic == null)
						{
							this.GetIsFirstTacticChosen = true;
						}
						this.CurrentTactic = availableTactics.FirstOrDefault<TacticComponent>();
					}
				}
			}
			this.CheckIsDefenseApplicable();
			TacticComponent tacticComponent2 = availableTactics.MaxBy((TacticComponent to) => to.GetTacticWeight() * ((to == this._currentTactic) ? 1.5f : 1f));
			bool flag2 = false;
			if (this.CurrentTactic == null)
			{
				flag2 = true;
			}
			else if (this.CurrentTactic != tacticComponent2)
			{
				if (!this.CurrentTactic.ResetTacticalPositions())
				{
					flag2 = true;
				}
				else
				{
					float tacticWeight = tacticComponent2.GetTacticWeight();
					float num = this.CurrentTactic.GetTacticWeight() * 1.5f;
					if (tacticWeight > num)
					{
						flag2 = true;
					}
				}
			}
			if (flag2)
			{
				if (this.CurrentTactic == null)
				{
					this.GetIsFirstTacticChosen = true;
				}
				this.CurrentTactic = tacticComponent2;
				if (Mission.Current.MainAgent != null && this.Team.GeneralAgent != null && this.Team.IsPlayerTeam && this.Team.IsPlayerSergeant)
				{
					string name = tacticComponent2.GetType().Name;
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_team_ai_tactic_text", name), 4000, this.Team.GeneralAgent.Character, "");
				}
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0004B180 File Offset: 0x00049380
		public void TickOccasionally()
		{
			if (Mission.Current.AllowAiTicking && this.Team.HasBots)
			{
				this.CurrentTactic.TickOccasionally();
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0004B1A6 File Offset: 0x000493A6
		public bool IsCurrentTactic(TacticComponent tactic)
		{
			return tactic == this.CurrentTactic;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0004B1B4 File Offset: 0x000493B4
		[Conditional("DEBUG")]
		protected virtual void DebugTick(float dt)
		{
			if (!MBDebug.IsDisplayingHighLevelAI)
			{
				return;
			}
			TacticComponent currentTactic = this.CurrentTactic;
			if (Input.DebugInput.IsHotKeyPressed("UsableMachineAiBaseHotkeyRetreatScriptActive"))
			{
				TeamAIComponent._retreatScriptActive = true;
			}
			else if (Input.DebugInput.IsHotKeyPressed("UsableMachineAiBaseHotkeyRetreatScriptPassive"))
			{
				TeamAIComponent._retreatScriptActive = false;
			}
			bool retreatScriptActive = TeamAIComponent._retreatScriptActive;
		}

		// Token: 0x060013D4 RID: 5076
		public abstract void OnUnitAddedToFormationForTheFirstTime(Formation formation);

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004B206 File Offset: 0x00049406
		protected internal virtual void CreateMissionSpecificBehaviors()
		{
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0004B208 File Offset: 0x00049408
		protected internal virtual void InitializeDetachments(Mission mission)
		{
			DeploymentHandler missionBehavior = this.Mission.GetMissionBehavior<DeploymentHandler>();
			if (missionBehavior == null)
			{
				return;
			}
			missionBehavior.InitializeDeploymentPoints();
		}

		// Token: 0x04000585 RID: 1413
		public TeamAIComponent.TacticalDecisionDelegate OnNotifyTacticalDecision;

		// Token: 0x04000586 RID: 1414
		public const int BattleTokenForceSize = 10;

		// Token: 0x04000587 RID: 1415
		private readonly List<TacticComponent> _availableTactics;

		// Token: 0x04000588 RID: 1416
		private static bool _retreatScriptActive;

		// Token: 0x04000589 RID: 1417
		protected readonly Mission Mission;

		// Token: 0x0400058A RID: 1418
		protected readonly Team Team;

		// Token: 0x0400058B RID: 1419
		private readonly Timer _thinkTimer;

		// Token: 0x0400058C RID: 1420
		private readonly Timer _applyTimer;

		// Token: 0x0400058D RID: 1421
		private TacticComponent _currentTactic;

		// Token: 0x0400058E RID: 1422
		public List<TacticalPosition> TacticalPositions;

		// Token: 0x0400058F RID: 1423
		public List<TacticalRegion> TacticalRegions;

		// Token: 0x04000590 RID: 1424
		private readonly MBList<StrategicArea> _strategicAreas;

		// Token: 0x04000591 RID: 1425
		private readonly float _occasionalTickTime;

		// Token: 0x04000592 RID: 1426
		private MissionTime _nextTacticChooseTime;

		// Token: 0x04000593 RID: 1427
		private MissionTime _nextOccasionalTickTime;

		// Token: 0x020004AC RID: 1196
		protected class TacticOption
		{
			// Token: 0x17000951 RID: 2385
			// (get) Token: 0x060036CF RID: 14031 RVA: 0x000DE056 File Offset: 0x000DC256
			// (set) Token: 0x060036D0 RID: 14032 RVA: 0x000DE05E File Offset: 0x000DC25E
			public string Id { get; private set; }

			// Token: 0x17000952 RID: 2386
			// (get) Token: 0x060036D1 RID: 14033 RVA: 0x000DE067 File Offset: 0x000DC267
			// (set) Token: 0x060036D2 RID: 14034 RVA: 0x000DE06F File Offset: 0x000DC26F
			public Lazy<TacticComponent> Tactic { get; private set; }

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x060036D3 RID: 14035 RVA: 0x000DE078 File Offset: 0x000DC278
			// (set) Token: 0x060036D4 RID: 14036 RVA: 0x000DE080 File Offset: 0x000DC280
			public float Weight { get; set; }

			// Token: 0x060036D5 RID: 14037 RVA: 0x000DE089 File Offset: 0x000DC289
			public TacticOption(string id, Lazy<TacticComponent> tactic, float weight)
			{
				this.Id = id;
				this.Tactic = tactic;
				this.Weight = weight;
			}
		}

		// Token: 0x020004AD RID: 1197
		// (Invoke) Token: 0x060036D7 RID: 14039
		public delegate void TacticalDecisionDelegate(in TacticalDecision decision);
	}
}
