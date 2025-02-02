using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard;

namespace SandBox.ViewModelCollection
{
	// Token: 0x02000007 RID: 7
	public class SPScoreboardVM : ScoreboardBaseVM, IBattleObserver
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000425D File Offset: 0x0000245D
		private bool _isPlayerDefendingSiege
		{
			get
			{
				Mission mission = Mission.Current;
				return mission != null && mission.IsSiegeBattle && Mission.Current.PlayerTeam.IsDefender;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004283 File Offset: 0x00002483
		public SPScoreboardVM(BattleSimulation simulation)
		{
			this._battleSimulation = simulation;
			this.BattleResults = new MBBindingList<BattleResultVM>();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000429D File Offset: 0x0000249D
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._isPlayerDefendingSiege)
			{
				base.QuitText = GameTexts.FindText("str_surrender", null).ToString();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000042C4 File Offset: 0x000024C4
		public override void Initialize(IMissionScreen missionScreen, Mission mission, Action releaseSimulationSources, Action<bool> onToggle)
		{
			base.Initialize(missionScreen, mission, releaseSimulationSources, onToggle);
			if (this._battleSimulation != null)
			{
				this._battleSimulation.BattleObserver = this;
				this.PlayerSide = (PlayerEncounter.PlayerIsAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
				base.Defenders = new SPScoreboardSideVM(GameTexts.FindText("str_battle_result_army", "defender"), MobileParty.MainParty.MapEvent.DefenderSide.LeaderParty.Banner);
				base.Attackers = new SPScoreboardSideVM(GameTexts.FindText("str_battle_result_army", "attacker"), MobileParty.MainParty.MapEvent.AttackerSide.LeaderParty.Banner);
				base.IsSimulation = true;
				base.IsMainCharacterDead = true;
				base.ShowScoreboard = true;
				this._battleSimulation.ResetSimulation();
				base.PowerComparer.Update((double)base.Defenders.CurrentPower, (double)base.Attackers.CurrentPower, (double)base.Defenders.CurrentPower, (double)base.Attackers.CurrentPower);
			}
			else
			{
				base.IsSimulation = false;
				BattleObserverMissionLogic missionBehavior = this._mission.GetMissionBehavior<BattleObserverMissionLogic>();
				if (missionBehavior != null)
				{
					missionBehavior.SetObserver(this);
				}
				else
				{
					Debug.FailedAssert("SPScoreboard on CustomBattle", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\SPScoreboardVM.cs", "Initialize", 79);
				}
				if (Campaign.Current != null)
				{
					if (PlayerEncounter.Battle != null)
					{
						base.Defenders = new SPScoreboardSideVM(GameTexts.FindText("str_battle_result_army", "defender"), MobileParty.MainParty.MapEvent.DefenderSide.LeaderParty.Banner);
						base.Attackers = new SPScoreboardSideVM(GameTexts.FindText("str_battle_result_army", "attacker"), MobileParty.MainParty.MapEvent.AttackerSide.LeaderParty.Banner);
						this.PlayerSide = (PlayerEncounter.PlayerIsAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
					}
					else
					{
						base.Defenders = new SPScoreboardSideVM(GameTexts.FindText("str_battle_result_army", "defender"), Mission.Current.Teams.Defender.Banner);
						base.Attackers = new SPScoreboardSideVM(GameTexts.FindText("str_battle_result_army", "attacker"), Mission.Current.Teams.Attacker.Banner);
						this.PlayerSide = BattleSideEnum.Defender;
					}
				}
				else
				{
					Debug.FailedAssert("SPScoreboard on CustomBattle", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\SPScoreboardVM.cs", "Initialize", 105);
				}
			}
			string defenderColor;
			string attackerColor;
			if (MobileParty.MainParty.MapEvent != null)
			{
				PartyBase leaderParty = MobileParty.MainParty.MapEvent.DefenderSide.LeaderParty;
				if (((leaderParty != null) ? leaderParty.MapFaction : null) is Kingdom)
				{
					defenderColor = Color.FromUint(((Kingdom)MobileParty.MainParty.MapEvent.DefenderSide.LeaderParty.MapFaction).PrimaryBannerColor).ToString();
				}
				else
				{
					IFaction mapFaction = MobileParty.MainParty.MapEvent.DefenderSide.LeaderParty.MapFaction;
					defenderColor = Color.FromUint((mapFaction != null) ? mapFaction.Banner.GetPrimaryColor() : 0U).ToString();
				}
				PartyBase leaderParty2 = MobileParty.MainParty.MapEvent.AttackerSide.LeaderParty;
				if (((leaderParty2 != null) ? leaderParty2.MapFaction : null) is Kingdom)
				{
					attackerColor = Color.FromUint(((Kingdom)MobileParty.MainParty.MapEvent.AttackerSide.LeaderParty.MapFaction).PrimaryBannerColor).ToString();
				}
				else
				{
					IFaction mapFaction2 = MobileParty.MainParty.MapEvent.AttackerSide.LeaderParty.MapFaction;
					attackerColor = Color.FromUint((mapFaction2 != null) ? mapFaction2.Banner.GetPrimaryColor() : 0U).ToString();
				}
			}
			else
			{
				attackerColor = Color.FromUint(Mission.Current.Teams.Attacker.Color).ToString();
				defenderColor = Color.FromUint(Mission.Current.Teams.Defender.Color).ToString();
			}
			base.PowerComparer.SetColors(defenderColor, attackerColor);
			base.MissionTimeInSeconds = -1;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000046B8 File Offset: 0x000028B8
		public override void Tick(float dt)
		{
			SallyOutEndLogic missionBehavior = Mission.Current.GetMissionBehavior<SallyOutEndLogic>();
			if (!base.IsOver)
			{
				if (!this._mission.IsMissionEnding)
				{
					BattleEndLogic battleEndLogic = this._battleEndLogic;
					if ((battleEndLogic == null || !battleEndLogic.IsEnemySideRetreating) && (missionBehavior == null || !missionBehavior.IsSallyOutOver))
					{
						goto IL_62;
					}
				}
				if (this._missionEndScoreboardDelayTimer < 1.5f)
				{
					this._missionEndScoreboardDelayTimer += dt;
				}
				else
				{
					this.OnBattleOver();
				}
			}
			IL_62:
			base.PowerComparer.IsEnabled = (Mission.Current != null && Mission.Current.Mode != MissionMode.Deployment);
			base.IsPowerComparerEnabled = (base.PowerComparer.IsEnabled && !BannerlordConfig.HideBattleUI && !MBCommon.IsPaused);
			if (!base.IsSimulation && !base.IsOver)
			{
				base.MissionTimeInSeconds = (int)Mission.Current.CurrentTime;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004792 File Offset: 0x00002992
		public override void ExecutePlayAction()
		{
			if (base.IsSimulation)
			{
				this._battleSimulation.Play();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000047A7 File Offset: 0x000029A7
		public override void ExecuteFastForwardAction()
		{
			if (!base.IsSimulation)
			{
				Mission.Current.SetFastForwardingFromUI(base.IsFastForwarding);
				return;
			}
			if (!base.IsFastForwarding)
			{
				this._battleSimulation.Play();
				return;
			}
			this._battleSimulation.FastForward();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000047E1 File Offset: 0x000029E1
		public override void ExecuteEndSimulationAction()
		{
			if (base.IsSimulation)
			{
				this._battleSimulation.Skip();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000047F6 File Offset: 0x000029F6
		public override void ExecuteQuitAction()
		{
			this.OnExitBattle();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004800 File Offset: 0x00002A00
		private void GetBattleRewards(bool playerVictory)
		{
			this.BattleResults.Clear();
			if (playerVictory)
			{
				ExplainedNumber renownExplained = new ExplainedNumber(0f, true, null);
				ExplainedNumber influencExplained = new ExplainedNumber(0f, true, null);
				ExplainedNumber moraleExplained = new ExplainedNumber(0f, true, null);
				float num;
				float num2;
				float num3;
				float num4;
				float playerEarnedLootPercentage;
				PlayerEncounter.GetBattleRewards(out num, out num2, out num3, out num4, out playerEarnedLootPercentage, ref renownExplained, ref influencExplained, ref moraleExplained);
				if (num > 0.1f)
				{
					this.BattleResults.Add(new BattleResultVM(SPScoreboardVM._renownStr.Format(num), () => SandBoxUIHelper.GetExplainedNumberTooltip(ref renownExplained), null));
				}
				if (num2 > 0.1f)
				{
					this.BattleResults.Add(new BattleResultVM(SPScoreboardVM._influenceStr.Format(num2), () => SandBoxUIHelper.GetExplainedNumberTooltip(ref influencExplained), null));
				}
				if (num3 > 0.1f || num3 < -0.1f)
				{
					this.BattleResults.Add(new BattleResultVM(SPScoreboardVM._moraleStr.Format(num3), () => SandBoxUIHelper.GetExplainedNumberTooltip(ref moraleExplained), null));
				}
				int num5 = (this.PlayerSide == BattleSideEnum.Attacker) ? base.Attackers.Parties.Count : base.Defenders.Parties.Count;
				if (playerEarnedLootPercentage > 0.1f && num5 > 1)
				{
					this.BattleResults.Add(new BattleResultVM(SPScoreboardVM._lootStr.Format(playerEarnedLootPercentage), () => SandBoxUIHelper.GetBattleLootAwardTooltip(playerEarnedLootPercentage), null));
				}
			}
			foreach (SPScoreboardPartyVM spscoreboardPartyVM in base.Defenders.Parties)
			{
				foreach (SPScoreboardUnitVM spscoreboardUnitVM in from member in spscoreboardPartyVM.Members
				where member.IsHero && member.Score.Dead > 0
				select member)
				{
					this.BattleResults.Add(new BattleResultVM(SPScoreboardVM._deadLordStr.SetTextVariable("A0", spscoreboardUnitVM.Character.Name).ToString(), () => new List<TooltipProperty>(), SandBoxUIHelper.GetCharacterCode(spscoreboardUnitVM.Character as CharacterObject, false)));
				}
			}
			foreach (SPScoreboardPartyVM spscoreboardPartyVM2 in base.Attackers.Parties)
			{
				foreach (SPScoreboardUnitVM spscoreboardUnitVM2 in from member in spscoreboardPartyVM2.Members
				where member.IsHero && member.Score.Dead > 0
				select member)
				{
					this.BattleResults.Add(new BattleResultVM(SPScoreboardVM._deadLordStr.SetTextVariable("A0", spscoreboardUnitVM2.Character.Name).ToString(), () => new List<TooltipProperty>(), SandBoxUIHelper.GetCharacterCode(spscoreboardUnitVM2.Character as CharacterObject, false)));
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004B90 File Offset: 0x00002D90
		private void UpdateSimulationResult(bool playerVictory)
		{
			if (!base.IsSimulation)
			{
				this.SimulationResult = "NotSimulation";
				return;
			}
			if (!playerVictory)
			{
				this.SimulationResult = "SimulationDefeat";
				return;
			}
			if (PlayerEncounter.Battle.PartiesOnSide(this.PlayerSide).Sum((MapEventParty x) => x.Party.NumberOfHealthyMembers) < 70)
			{
				this.SimulationResult = "SimulationVictorySmall";
				return;
			}
			this.SimulationResult = "SimulationVictoryLarge";
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004C10 File Offset: 0x00002E10
		public void OnBattleOver()
		{
			ScoreboardBaseVM.BattleResultType battleResultIndex = ScoreboardBaseVM.BattleResultType.NotOver;
			if (PlayerEncounter.IsActive && PlayerEncounter.Battle != null)
			{
				base.IsOver = true;
				if (PlayerEncounter.WinningSide == this.PlayerSide)
				{
					battleResultIndex = ScoreboardBaseVM.BattleResultType.Victory;
				}
				else if (PlayerEncounter.CampaignBattleResult != null && PlayerEncounter.CampaignBattleResult.EnemyPulledBack)
				{
					battleResultIndex = ScoreboardBaseVM.BattleResultType.Retreat;
				}
				else
				{
					battleResultIndex = ScoreboardBaseVM.BattleResultType.Defeat;
				}
				bool playerVictory = PlayerEncounter.WinningSide == this.PlayerSide;
				this.GetBattleRewards(playerVictory);
				this.UpdateSimulationResult(playerVictory);
			}
			else
			{
				Mission mission = Mission.Current;
				if (mission != null && mission.MissionEnded)
				{
					base.IsOver = true;
					if ((Mission.Current.HasMissionBehavior<SallyOutEndLogic>() && !Mission.Current.MissionResult.BattleResolved) || Mission.Current.MissionResult.PlayerVictory)
					{
						battleResultIndex = ScoreboardBaseVM.BattleResultType.Victory;
					}
					else if (Mission.Current.MissionResult.BattleState == BattleState.DefenderPullBack)
					{
						battleResultIndex = ScoreboardBaseVM.BattleResultType.Retreat;
					}
					else
					{
						battleResultIndex = ScoreboardBaseVM.BattleResultType.Defeat;
					}
				}
				else
				{
					BattleEndLogic battleEndLogic = this._battleEndLogic;
					if (battleEndLogic != null && battleEndLogic.IsEnemySideRetreating)
					{
						base.IsOver = true;
					}
				}
			}
			switch (battleResultIndex)
			{
			case ScoreboardBaseVM.BattleResultType.NotOver:
				break;
			case ScoreboardBaseVM.BattleResultType.Defeat:
				base.BattleResult = GameTexts.FindText("str_defeat", null).ToString();
				base.BattleResultIndex = (int)battleResultIndex;
				return;
			case ScoreboardBaseVM.BattleResultType.Victory:
				base.BattleResult = GameTexts.FindText("str_victory", null).ToString();
				base.BattleResultIndex = (int)battleResultIndex;
				return;
			case ScoreboardBaseVM.BattleResultType.Retreat:
				base.BattleResult = GameTexts.FindText("str_battle_result_retreat", null).ToString();
				base.BattleResultIndex = (int)battleResultIndex;
				break;
			default:
				return;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004D78 File Offset: 0x00002F78
		public void OnExitBattle()
		{
			if (base.IsSimulation)
			{
				if (this._battleSimulation.IsSimulationFinished)
				{
					this._releaseSimulationSources();
					this._battleSimulation.OnReturn();
					return;
				}
				Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
				InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_order_Retreat", null).ToString(), GameTexts.FindText("str_retreat_question", null).ToString(), true, true, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), delegate()
				{
					Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
					this._releaseSimulationSources();
					this._battleSimulation.OnReturn();
				}, delegate()
				{
					Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
				}, "", 0f, null, null, null), false, false);
				return;
			}
			else
			{
				BattleEndLogic missionBehavior = this._mission.GetMissionBehavior<BattleEndLogic>();
				BasicMissionHandler missionBehavior2 = this._mission.GetMissionBehavior<BasicMissionHandler>();
				BattleEndLogic.ExitResult exitResult = (missionBehavior != null) ? missionBehavior.TryExit() : (this._mission.MissionEnded ? BattleEndLogic.ExitResult.True : BattleEndLogic.ExitResult.NeedsPlayerConfirmation);
				if (exitResult == BattleEndLogic.ExitResult.NeedsPlayerConfirmation || exitResult == BattleEndLogic.ExitResult.SurrenderSiege)
				{
					this.OnToggle(false);
					missionBehavior2.CreateWarningWidgetForResult(exitResult);
					return;
				}
				if (exitResult == BattleEndLogic.ExitResult.False)
				{
					InformationManager.ShowInquiry(this._retreatInquiryData, false, false);
					return;
				}
				if (missionBehavior == null && exitResult == BattleEndLogic.ExitResult.True)
				{
					this._mission.EndMission();
				}
				return;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004EAC File Offset: 0x000030AC
		public void TroopNumberChanged(BattleSideEnum side, IBattleCombatant battleCombatant, BasicCharacterObject character, int number = 0, int numberDead = 0, int numberWounded = 0, int numberRouted = 0, int numberKilled = 0, int numberReadyToUpgrade = 0)
		{
			PartyBase partyBase = battleCombatant as PartyBase;
			bool isPlayerParty = ((partyBase != null) ? partyBase.Owner : null) == Hero.MainHero;
			base.GetSide(side).UpdateScores(battleCombatant, isPlayerParty, character, number, numberDead, numberWounded, numberRouted, numberKilled, numberReadyToUpgrade);
			base.PowerComparer.Update((double)base.Defenders.CurrentPower, (double)base.Attackers.CurrentPower, (double)base.Defenders.InitialPower, (double)base.Attackers.InitialPower);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004F2C File Offset: 0x0000312C
		public void HeroSkillIncreased(BattleSideEnum side, IBattleCombatant battleCombatant, BasicCharacterObject heroCharacter, SkillObject upgradedSkill)
		{
			PartyBase partyBase = battleCombatant as PartyBase;
			bool isPlayerParty = ((partyBase != null) ? partyBase.Owner : null) == Hero.MainHero;
			base.GetSide(side).UpdateHeroSkills(battleCombatant, isPlayerParty, heroCharacter, upgradedSkill);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004F64 File Offset: 0x00003164
		public void BattleResultsReady()
		{
			if (!base.IsOver)
			{
				this.OnBattleOver();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004F74 File Offset: 0x00003174
		public void TroopSideChanged(BattleSideEnum prevSide, BattleSideEnum newSide, IBattleCombatant battleCombatant, BasicCharacterObject character)
		{
			SPScoreboardStatsVM scoreToBringOver = base.GetSide(prevSide).RemoveTroop(battleCombatant, character);
			SPScoreboardSideVM side = base.GetSide(newSide);
			PartyBase partyBase = battleCombatant as PartyBase;
			side.GetPartyAddIfNotExists(battleCombatant, ((partyBase != null) ? partyBase.Owner : null) == Hero.MainHero);
			base.GetSide(newSide).AddTroop(battleCombatant, character, scoreToBringOver);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00004FC8 File Offset: 0x000031C8
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00004FD0 File Offset: 0x000031D0
		[DataSourceProperty]
		public override MBBindingList<BattleResultVM> BattleResults
		{
			get
			{
				return this._battleResults;
			}
			set
			{
				if (value != this._battleResults)
				{
					this._battleResults = value;
					base.OnPropertyChangedWithValue<MBBindingList<BattleResultVM>>(value, "BattleResults");
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00004FEE File Offset: 0x000031EE
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00004FF6 File Offset: 0x000031F6
		[DataSourceProperty]
		public string SimulationResult
		{
			get
			{
				return this._simulationResult;
			}
			set
			{
				if (value != this._simulationResult)
				{
					this._simulationResult = value;
					base.OnPropertyChangedWithValue<string>(value, "SimulationResult");
				}
			}
		}

		// Token: 0x04000010 RID: 16
		private readonly BattleSimulation _battleSimulation;

		// Token: 0x04000011 RID: 17
		private static readonly TextObject _renownStr = new TextObject("{=eiWQoW9j}You gained {A0} renown.", null);

		// Token: 0x04000012 RID: 18
		private static readonly TextObject _influenceStr = new TextObject("{=5zeL8sa9}You gained {A0} influence.", null);

		// Token: 0x04000013 RID: 19
		private static readonly TextObject _moraleStr = new TextObject("{=WAKz9xX8}You gained {A0} morale.", null);

		// Token: 0x04000014 RID: 20
		private static readonly TextObject _lootStr = new TextObject("{=xu5NA6AW}You earned {A0}% of the loot.", null);

		// Token: 0x04000015 RID: 21
		private static readonly TextObject _deadLordStr = new TextObject("{=gDKhs4lD}{A0} has died on the battlefield.", null);

		// Token: 0x04000016 RID: 22
		private float _missionEndScoreboardDelayTimer;

		// Token: 0x04000017 RID: 23
		private MBBindingList<BattleResultVM> _battleResults;

		// Token: 0x04000018 RID: 24
		private string _simulationResult;
	}
}
