using System;
using psai.net;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions.Sound.Components
{
	// Token: 0x02000028 RID: 40
	public class MusicTournamentMissionView : MissionView, IMusicHandler
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000112D0 File Offset: 0x0000F4D0
		bool IMusicHandler.IsPausable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000112D4 File Offset: 0x0000F4D4
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			MBMusicManager.Current.DeactivateCurrentMode();
			MBMusicManager.Current.ActivateBattleMode();
			MBMusicManager.Current.OnBattleMusicHandlerInit(this);
			this._startTimer = new Timer(Mission.Current.CurrentTime, 3f, true);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00011324 File Offset: 0x0000F524
		public override void EarlyStart()
		{
			this._allOneShotSoundEventsAreDisabled = false;
			this._tournamentBehavior = Mission.Current.GetMissionBehavior<TournamentBehavior>();
			this._currentMatch = null;
			this._lastMatch = null;
			this._arenaSoundEntity = base.Mission.Scene.FindEntityWithTag("arena_sound");
			SoundManager.SetGlobalParameter("ArenaIntensity", 0f);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00011380 File Offset: 0x0000F580
		public override void OnMissionScreenFinalize()
		{
			SoundManager.SetGlobalParameter("ArenaIntensity", 0f);
			MBMusicManager.Current.DeactivateBattleMode();
			MBMusicManager.Current.OnBattleMusicHandlerFinalize();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000113A8 File Offset: 0x0000F5A8
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

		// Token: 0x06000160 RID: 352 RVA: 0x00011410 File Offset: 0x0000F610
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (this._fightStarted)
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
				if ((affectedAgent.IsHuman && affectedAgent.State != AgentState.Routed) || flag)
				{
					float num = flag3 ? MusicParameters.FriendlyTroopDeadEffectOnIntensity : MusicParameters.EnemyTroopDeadEffectOnIntensity;
					if (flag)
					{
						num *= MusicParameters.PlayerTroopDeadEffectMultiplierOnIntensity;
					}
					MBMusicManager.Current.ChangeCurrentThemeIntensity(num);
				}
			}
			if (affectedAgent != null && affectorAgent != null && affectorAgent.IsMainAgent && (agentState == AgentState.Killed || agentState == AgentState.Unconscious))
			{
				int num2 = 0;
				if (affectedAgent.Team == affectorAgent.Team)
				{
					if (affectedAgent.IsHuman)
					{
						num2 += -30;
					}
					else
					{
						num2 += -20;
					}
				}
				else if (affectedAgent.IsHuman)
				{
					num2++;
					if (affectedAgent.HasMount)
					{
						num2++;
					}
					if (killingBlow.OverrideKillInfo == Agent.KillInfo.Headshot)
					{
						num2 += 3;
					}
					if (killingBlow.IsMissile)
					{
						num2++;
					}
					else
					{
						num2 += 2;
					}
				}
				else if (affectedAgent.RiderAgent != null)
				{
					num2 += 3;
				}
				this.UpdateAudienceIntensity(num2, false);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00011570 File Offset: 0x0000F770
		void IMusicHandler.OnUpdated(float dt)
		{
			if (!this._fightStarted && Agent.Main != null && Agent.Main.IsActive() && this._startTimer.Check(Mission.Current.CurrentTime))
			{
				this._fightStarted = true;
				MBMusicManager.Current.StartTheme(MusicTheme.BattleSmall, 0.5f, false);
			}
			if (this._fightStarted)
			{
				this.CheckIntensityFall();
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000115D8 File Offset: 0x0000F7D8
		public override void OnMissionTick(float dt)
		{
			if (this._tournamentBehavior != null)
			{
				if (this._currentMatch != this._tournamentBehavior.CurrentMatch)
				{
					TournamentMatch currentMatch = this._tournamentBehavior.CurrentMatch;
					if (currentMatch != null && currentMatch.IsPlayerParticipating())
					{
						Agent main = Agent.Main;
						if (main != null && main.IsActive())
						{
							this._currentMatch = this._tournamentBehavior.CurrentMatch;
							this.OnTournamentRoundBegin(this._tournamentBehavior.NextRound == null);
						}
					}
				}
				if (this._lastMatch != this._tournamentBehavior.LastMatch)
				{
					this._lastMatch = this._tournamentBehavior.LastMatch;
					if (this._tournamentBehavior.NextRound == null || this._tournamentBehavior.LastMatch.IsPlayerParticipating())
					{
						this.OnTournamentRoundEnd();
					}
				}
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000116A0 File Offset: 0x0000F8A0
		public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, WeaponComponentData attackerWeapon, bool isBlocked, bool isSiegeEngineHit, in Blow blow, in AttackCollisionData collisionData, float damagedHp, float hitDistance, float shotDifficulty)
		{
			if (affectorAgent != null && affectedAgent != null && affectorAgent.IsMainAgent && affectedAgent.IsHuman && affectedAgent.Position.Distance(affectorAgent.Position) >= 15f && (blow.VictimBodyPart == BoneBodyPartType.Head || blow.VictimBodyPart == BoneBodyPartType.Neck))
			{
				this.UpdateAudienceIntensity(3, false);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000116F9 File Offset: 0x0000F8F9
		public override void OnMissileHit(Agent attacker, Agent victim, bool isCanceled, AttackCollisionData collisionData)
		{
			if (!isCanceled && attacker != null && victim != null && attacker.IsMainAgent && victim.IsHuman && collisionData.IsShieldBroken)
			{
				this.UpdateAudienceIntensity(2, false);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00011725 File Offset: 0x0000F925
		public override void OnMeleeHit(Agent attacker, Agent victim, bool isCanceled, AttackCollisionData collisionData)
		{
			if (!isCanceled && attacker != null && victim != null && attacker.IsMainAgent && victim.IsHuman && collisionData.IsShieldBroken)
			{
				this.UpdateAudienceIntensity(2, false);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00011754 File Offset: 0x0000F954
		private void UpdateAudienceIntensity(int intensityChangeAmount, bool isEnd = false)
		{
			MusicTournamentMissionView.ReactionType reactionType;
			if (!isEnd)
			{
				reactionType = ((intensityChangeAmount >= 0) ? MusicTournamentMissionView.ReactionType.Positive : MusicTournamentMissionView.ReactionType.Negative);
			}
			else
			{
				reactionType = MusicTournamentMissionView.ReactionType.End;
			}
			this._currentTournamentIntensity += intensityChangeAmount;
			bool flag = false;
			if (this._currentTournamentIntensity > 60)
			{
				flag = (this._arenaIntensityLevel != MusicTournamentMissionView.ArenaIntensityLevel.High);
				this._arenaIntensityLevel = MusicTournamentMissionView.ArenaIntensityLevel.High;
			}
			else if (this._currentTournamentIntensity > 30)
			{
				flag = (this._arenaIntensityLevel != MusicTournamentMissionView.ArenaIntensityLevel.Mid);
				this._arenaIntensityLevel = MusicTournamentMissionView.ArenaIntensityLevel.Mid;
			}
			else if (this._currentTournamentIntensity <= 30)
			{
				flag = (this._arenaIntensityLevel != MusicTournamentMissionView.ArenaIntensityLevel.Low);
				this._arenaIntensityLevel = MusicTournamentMissionView.ArenaIntensityLevel.Low;
			}
			if (flag)
			{
				SoundManager.SetGlobalParameter("ArenaIntensity", (float)this._arenaIntensityLevel);
			}
			if (!this._allOneShotSoundEventsAreDisabled)
			{
				this.Cheer(reactionType);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00011804 File Offset: 0x0000FA04
		private void Cheer(MusicTournamentMissionView.ReactionType reactionType)
		{
			string text = null;
			switch (reactionType)
			{
			case MusicTournamentMissionView.ReactionType.Positive:
				text = "event:/mission/ambient/arena/reaction";
				break;
			case MusicTournamentMissionView.ReactionType.Negative:
				text = "event:/mission/ambient/arena/negative_reaction";
				break;
			case MusicTournamentMissionView.ReactionType.End:
				text = "event:/mission/ambient/arena/reaction";
				break;
			}
			if (text != null)
			{
				string eventFullName = text;
				Vec3 globalPosition = this._arenaSoundEntity.GlobalPosition;
				SoundManager.StartOneShotEvent(eventFullName, globalPosition);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00011855 File Offset: 0x0000FA55
		public void OnTournamentRoundBegin(bool isFinalRound)
		{
			this._isFinalRound = isFinalRound;
			this.UpdateAudienceIntensity(0, false);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00011868 File Offset: 0x0000FA68
		public void OnTournamentRoundEnd()
		{
			int num = 10;
			if (this._lastMatch.IsPlayerWinner())
			{
				num += 10;
			}
			this.UpdateAudienceIntensity(num, this._isFinalRound);
		}

		// Token: 0x040000C8 RID: 200
		private const string ArenaSoundTag = "arena_sound";

		// Token: 0x040000C9 RID: 201
		private const string ArenaIntensityParameterId = "ArenaIntensity";

		// Token: 0x040000CA RID: 202
		private const string ArenaPositiveReactionsSoundId = "event:/mission/ambient/arena/reaction";

		// Token: 0x040000CB RID: 203
		private const string ArenaNegativeReactionsSoundId = "event:/mission/ambient/arena/negative_reaction";

		// Token: 0x040000CC RID: 204
		private const string ArenaTournamentEndSoundId = "event:/mission/ambient/arena/reaction";

		// Token: 0x040000CD RID: 205
		private const int MainAgentKnocksDownAnOpponentBaseIntensityChange = 1;

		// Token: 0x040000CE RID: 206
		private const int MainAgentKnocksDownAnOpponentHeadShotIntensityChange = 3;

		// Token: 0x040000CF RID: 207
		private const int MainAgentKnocksDownAnOpponentMountedTargetIntensityChange = 1;

		// Token: 0x040000D0 RID: 208
		private const int MainAgentKnocksDownAnOpponentRangedHitIntensityChange = 1;

		// Token: 0x040000D1 RID: 209
		private const int MainAgentKnocksDownAnOpponentMeleeHitIntensityChange = 2;

		// Token: 0x040000D2 RID: 210
		private const int MainAgentHeadShotFrom15MetersRangeIntensityChange = 3;

		// Token: 0x040000D3 RID: 211
		private const int MainAgentDismountsAnOpponentIntensityChange = 3;

		// Token: 0x040000D4 RID: 212
		private const int MainAgentBreaksAShieldIntensityChange = 2;

		// Token: 0x040000D5 RID: 213
		private const int MainAgentWinsTournamentRoundIntensityChange = 10;

		// Token: 0x040000D6 RID: 214
		private const int RoundEndIntensityChange = 10;

		// Token: 0x040000D7 RID: 215
		private const int MainAgentKnocksDownATeamMateIntensityChange = -30;

		// Token: 0x040000D8 RID: 216
		private const int MainAgentKnocksDownAFriendlyHorseIntensityChange = -20;

		// Token: 0x040000D9 RID: 217
		private int _currentTournamentIntensity;

		// Token: 0x040000DA RID: 218
		private MusicTournamentMissionView.ArenaIntensityLevel _arenaIntensityLevel;

		// Token: 0x040000DB RID: 219
		private bool _allOneShotSoundEventsAreDisabled;

		// Token: 0x040000DC RID: 220
		private TournamentBehavior _tournamentBehavior;

		// Token: 0x040000DD RID: 221
		private TournamentMatch _currentMatch;

		// Token: 0x040000DE RID: 222
		private TournamentMatch _lastMatch;

		// Token: 0x040000DF RID: 223
		private GameEntity _arenaSoundEntity;

		// Token: 0x040000E0 RID: 224
		private bool _isFinalRound;

		// Token: 0x040000E1 RID: 225
		private bool _fightStarted;

		// Token: 0x040000E2 RID: 226
		private Timer _startTimer;

		// Token: 0x02000075 RID: 117
		private enum ArenaIntensityLevel
		{
			// Token: 0x040002B7 RID: 695
			None,
			// Token: 0x040002B8 RID: 696
			Low,
			// Token: 0x040002B9 RID: 697
			Mid,
			// Token: 0x040002BA RID: 698
			High
		}

		// Token: 0x02000076 RID: 118
		private enum ReactionType
		{
			// Token: 0x040002BC RID: 700
			Positive,
			// Token: 0x040002BD RID: 701
			Negative,
			// Token: 0x040002BE RID: 702
			End
		}
	}
}
