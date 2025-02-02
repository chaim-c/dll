using System;
using System.Collections.Generic;
using System.Linq;
using psai.net;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Sound
{
	// Token: 0x0200005B RID: 91
	public class MusicBattleMissionView : MissionView, IMusicHandler
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000217E8 File Offset: 0x0001F9E8
		bool IMusicHandler.IsPausable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000217EB File Offset: 0x0001F9EB
		private BattleSideEnum PlayerSide
		{
			get
			{
				Team playerTeam = Mission.Current.PlayerTeam;
				if (playerTeam == null)
				{
					return BattleSideEnum.None;
				}
				return playerTeam.Side;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00021802 File Offset: 0x0001FA02
		public MusicBattleMissionView(bool isSiegeBattle)
		{
			this._isSiegeBattle = isSiegeBattle;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00021811 File Offset: 0x0001FA11
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionAgentSpawnLogic = Mission.Current.GetMissionBehavior<MissionAgentSpawnLogic>();
			MBMusicManager.Current.DeactivateCurrentMode();
			MBMusicManager.Current.ActivateBattleMode();
			MBMusicManager.Current.OnBattleMusicHandlerInit(this);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00021848 File Offset: 0x0001FA48
		public override void OnMissionScreenFinalize()
		{
			MBMusicManager.Current.DeactivateBattleMode();
			MBMusicManager.Current.OnBattleMusicHandlerFinalize();
			base.Mission.PlayerTeam.PlayerOrderController.OnOrderIssued -= new OnOrderIssuedDelegate(this.PlayerOrderControllerOnOrderIssued);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0002187F File Offset: 0x0001FA7F
		public override void AfterStart()
		{
			this._nextPossibleTimeToIncreaseIntensityForChargeOrder = MissionTime.Now;
			base.Mission.PlayerTeam.PlayerOrderController.OnOrderIssued += new OnOrderIssuedDelegate(this.PlayerOrderControllerOnOrderIssued);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000218B0 File Offset: 0x0001FAB0
		private void PlayerOrderControllerOnOrderIssued(OrderType orderType, IEnumerable<Formation> appliedFormations, OrderController orderController, object[] parameters)
		{
			if ((orderType == OrderType.Charge || orderType == OrderType.ChargeWithTarget) && this._nextPossibleTimeToIncreaseIntensityForChargeOrder.IsPast)
			{
				float currentIntensity = PsaiCore.Instance.GetCurrentIntensity();
				float deltaIntensity = currentIntensity * MusicParameters.PlayerChargeEffectMultiplierOnIntensity - currentIntensity;
				MBMusicManager.Current.ChangeCurrentThemeIntensity(deltaIntensity);
				this._nextPossibleTimeToIncreaseIntensityForChargeOrder = MissionTime.Now + MissionTime.Seconds(60f);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0002190C File Offset: 0x0001FB0C
		private void CheckIntensityFall()
		{
			PsaiInfo psaiInfo = PsaiCore.Instance.GetPsaiInfo();
			if (psaiInfo.effectiveThemeId >= 0)
			{
				if (float.IsNaN(psaiInfo.currentIntensity))
				{
					MBMusicManager.Current.ChangeCurrentThemeIntensity(MusicParameters.MinIntensity);
					return;
				}
				if (psaiInfo.currentIntensity < MusicParameters.MinIntensity)
				{
					MBMusicManager.Current.ChangeCurrentThemeIntensity(MusicParameters.MinIntensity - psaiInfo.currentIntensity);
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00021974 File Offset: 0x0001FB74
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (this._battleState != MusicBattleMissionView.BattleState.Starting)
			{
				bool flag = affectedAgent.IsMine || (affectedAgent.RiderAgent != null && affectedAgent.RiderAgent.IsMine);
				Team team = affectedAgent.Team;
				BattleSideEnum battleSideEnum = (team != null) ? team.Side : BattleSideEnum.None;
				bool flag2;
				if (!flag)
				{
					if (battleSideEnum != BattleSideEnum.None)
					{
						Team playerTeam = Mission.Current.PlayerTeam;
						flag2 = (((playerTeam != null) ? playerTeam.Side : BattleSideEnum.None) == battleSideEnum);
					}
					else
					{
						flag2 = false;
					}
				}
				else
				{
					flag2 = true;
				}
				bool flag3 = flag2;
				if (!this._isSiegeBattle && affectedAgent.IsHuman && battleSideEnum != BattleSideEnum.None && this._battleState == MusicBattleMissionView.BattleState.Started && this._startingTroopCounts.Sum() >= MusicParameters.SmallBattleTreshold && MissionTime.Now.ToSeconds > (double)MusicParameters.BattleTurnsOneSideCooldown && this._missionAgentSpawnLogic.NumberOfRemainingTroops == 0)
				{
					int[] array = new int[]
					{
						this._missionAgentSpawnLogic.NumberOfActiveDefenderTroops,
						this._missionAgentSpawnLogic.NumberOfActiveAttackerTroops
					};
					array[(int)battleSideEnum]--;
					MusicTheme musicTheme = MusicTheme.None;
					if (array[0] > 0 && array[1] > 0)
					{
						float num = (float)array[0] / (float)array[1];
						if (num < this._startingBattleRatio * MusicParameters.BattleRatioTresholdOnIntensity)
						{
							musicTheme = MBMusicManager.Current.GetBattleTurnsOneSideTheme(base.Mission.MusicCulture.GetCultureCode(), this.PlayerSide > BattleSideEnum.Defender, this._isPaganBattle);
						}
						else if (num > this._startingBattleRatio / MusicParameters.BattleRatioTresholdOnIntensity)
						{
							musicTheme = MBMusicManager.Current.GetBattleTurnsOneSideTheme(base.Mission.MusicCulture.GetCultureCode(), this.PlayerSide == BattleSideEnum.Defender, this._isPaganBattle);
						}
					}
					if (musicTheme != MusicTheme.None)
					{
						MBMusicManager.Current.StartTheme(musicTheme, PsaiCore.Instance.GetCurrentIntensity(), false);
						this._battleState = MusicBattleMissionView.BattleState.TurnedOneSide;
					}
				}
				if ((affectedAgent.IsHuman && affectedAgent.State != AgentState.Routed) || flag)
				{
					float num2 = flag3 ? MusicParameters.FriendlyTroopDeadEffectOnIntensity : MusicParameters.EnemyTroopDeadEffectOnIntensity;
					if (flag)
					{
						num2 *= MusicParameters.PlayerTroopDeadEffectMultiplierOnIntensity;
					}
					MBMusicManager.Current.ChangeCurrentThemeIntensity(num2);
				}
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00021B84 File Offset: 0x0001FD84
		private void CheckForStarting()
		{
			if (this._startingTroopCounts == null)
			{
				this._startingTroopCounts = new int[]
				{
					this._missionAgentSpawnLogic.GetTotalNumberOfTroopsForSide(BattleSideEnum.Defender),
					this._missionAgentSpawnLogic.GetTotalNumberOfTroopsForSide(BattleSideEnum.Attacker)
				};
				this._startingBattleRatio = (float)this._startingTroopCounts[0] / (float)this._startingTroopCounts[1];
			}
			Agent main = Agent.Main;
			Vec2 vec = (main != null) ? main.Position.AsVec2 : Vec2.Invalid;
			Team playerTeam = Mission.Current.PlayerTeam;
			bool flag;
			if (playerTeam == null)
			{
				flag = false;
			}
			else
			{
				flag = playerTeam.FormationsIncludingEmpty.Any((Formation f) => f.CountOfUnits > 0);
			}
			bool flag2 = flag;
			float num = float.MaxValue;
			if (flag2 || vec.IsValid)
			{
				foreach (Formation formation in Mission.Current.PlayerEnemyTeam.FormationsIncludingEmpty)
				{
					if (formation.CountOfUnits > 0)
					{
						float num2 = float.MaxValue;
						if (!flag2 && vec.IsValid)
						{
							num2 = vec.DistanceSquared(formation.CurrentPosition);
						}
						else if (flag2)
						{
							foreach (Formation formation2 in Mission.Current.PlayerTeam.FormationsIncludingEmpty)
							{
								if (formation2.CountOfUnits > 0)
								{
									float num3 = formation2.CurrentPosition.DistanceSquared(formation.CurrentPosition);
									if (num2 > num3)
									{
										num2 = num3;
									}
								}
							}
						}
						if (num > num2)
						{
							num = num2;
						}
					}
				}
			}
			int num4 = this._startingTroopCounts.Sum();
			bool flag3 = false;
			if (num4 < MusicParameters.SmallBattleTreshold)
			{
				if (num < MusicParameters.SmallBattleDistanceTreshold * MusicParameters.SmallBattleDistanceTreshold)
				{
					flag3 = true;
				}
			}
			else if (num4 < MusicParameters.MediumBattleTreshold)
			{
				if (num < MusicParameters.MediumBattleDistanceTreshold * MusicParameters.MediumBattleDistanceTreshold)
				{
					flag3 = true;
				}
			}
			else if (num4 < MusicParameters.LargeBattleTreshold)
			{
				if (num < MusicParameters.LargeBattleDistanceTreshold * MusicParameters.LargeBattleDistanceTreshold)
				{
					flag3 = true;
				}
			}
			else if (num < MusicParameters.MaxBattleDistanceTreshold * MusicParameters.MaxBattleDistanceTreshold)
			{
				flag3 = true;
			}
			if (flag3)
			{
				float num5 = (float)num4 / 1000f;
				float startIntensity = MusicParameters.DefaultStartIntensity + num5 * MusicParameters.BattleSizeEffectOnStartIntensity + (MBRandom.RandomFloat - 0.5f) * (MusicParameters.RandomEffectMultiplierOnStartIntensity * 2f);
				MusicTheme theme = this._isSiegeBattle ? MBMusicManager.Current.GetSiegeTheme(base.Mission.MusicCulture.GetCultureCode()) : MBMusicManager.Current.GetBattleTheme(base.Mission.MusicCulture.GetCultureCode(), num4, out this._isPaganBattle);
				MBMusicManager.Current.StartTheme(theme, startIntensity, false);
				this._battleState = MusicBattleMissionView.BattleState.Started;
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00021E54 File Offset: 0x00020054
		private void CheckForEnding()
		{
			if (Mission.Current.IsMissionEnding)
			{
				if (Mission.Current.MissionResult != null)
				{
					MusicTheme battleEndTheme = MBMusicManager.Current.GetBattleEndTheme(base.Mission.MusicCulture.GetCultureCode(), Mission.Current.MissionResult.PlayerVictory);
					MBMusicManager.Current.StartTheme(battleEndTheme, PsaiCore.Instance.GetPsaiInfo().currentIntensity, true);
					this._battleState = MusicBattleMissionView.BattleState.Ending;
					return;
				}
				MBMusicManager.Current.StartTheme(MusicTheme.BattleDefeat, PsaiCore.Instance.GetPsaiInfo().currentIntensity, true);
				this._battleState = MusicBattleMissionView.BattleState.Ending;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00021EF4 File Offset: 0x000200F4
		void IMusicHandler.OnUpdated(float dt)
		{
			if (this._battleState == MusicBattleMissionView.BattleState.Starting)
			{
				if (base.Mission.MusicCulture == null && Mission.Current.GetMissionBehavior<DeploymentHandler>() == null && this._missionAgentSpawnLogic.IsDeploymentOver)
				{
					KeyValuePair<BasicCultureObject, int> keyValuePair = new KeyValuePair<BasicCultureObject, int>(null, -1);
					Dictionary<BasicCultureObject, int> dictionary = new Dictionary<BasicCultureObject, int>();
					foreach (Team team in base.Mission.Teams)
					{
						foreach (Agent agent in team.ActiveAgents)
						{
							BasicCultureObject culture = agent.Character.Culture;
							if (culture != null && culture.IsMainCulture)
							{
								if (!dictionary.ContainsKey(agent.Character.Culture))
								{
									dictionary.Add(agent.Character.Culture, 0);
								}
								Dictionary<BasicCultureObject, int> dictionary2 = dictionary;
								BasicCultureObject culture2 = agent.Character.Culture;
								int num = dictionary2[culture2];
								dictionary2[culture2] = num + 1;
								if (dictionary[agent.Character.Culture] > keyValuePair.Value)
								{
									keyValuePair = new KeyValuePair<BasicCultureObject, int>(agent.Character.Culture, dictionary[agent.Character.Culture]);
								}
							}
						}
					}
					if (keyValuePair.Key != null)
					{
						base.Mission.MusicCulture = keyValuePair.Key;
					}
					else
					{
						base.Mission.MusicCulture = Game.Current.PlayerTroop.Culture;
					}
				}
				if (base.Mission.MusicCulture != null)
				{
					this.CheckForStarting();
				}
			}
			if (this._battleState == MusicBattleMissionView.BattleState.Started || this._battleState == MusicBattleMissionView.BattleState.TurnedOneSide)
			{
				this.CheckForEnding();
			}
			this.CheckIntensityFall();
		}

		// Token: 0x0400029A RID: 666
		private const float ChargeOrderIntensityIncreaseCooldownInSeconds = 60f;

		// Token: 0x0400029B RID: 667
		private MusicBattleMissionView.BattleState _battleState;

		// Token: 0x0400029C RID: 668
		private MissionAgentSpawnLogic _missionAgentSpawnLogic;

		// Token: 0x0400029D RID: 669
		private int[] _startingTroopCounts;

		// Token: 0x0400029E RID: 670
		private float _startingBattleRatio;

		// Token: 0x0400029F RID: 671
		private bool _isSiegeBattle;

		// Token: 0x040002A0 RID: 672
		private bool _isPaganBattle;

		// Token: 0x040002A1 RID: 673
		private MissionTime _nextPossibleTimeToIncreaseIntensityForChargeOrder;

		// Token: 0x020000B3 RID: 179
		private enum BattleState
		{
			// Token: 0x0400038F RID: 911
			Starting,
			// Token: 0x04000390 RID: 912
			Started,
			// Token: 0x04000391 RID: 913
			TurnedOneSide,
			// Token: 0x04000392 RID: 914
			Ending
		}
	}
}
