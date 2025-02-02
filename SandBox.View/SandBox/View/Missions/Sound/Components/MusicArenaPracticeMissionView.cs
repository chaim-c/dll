using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions.Sound.Components
{
	// Token: 0x02000027 RID: 39
	public class MusicArenaPracticeMissionView : MissionView, IMusicHandler
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00010FEA File Offset: 0x0000F1EA
		bool IMusicHandler.IsPausable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00010FED File Offset: 0x0000F1ED
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			MBMusicManager.Current.DeactivateCurrentMode();
			MBMusicManager.Current.ActivateBattleMode();
			MBMusicManager.Current.OnBattleMusicHandlerInit(this);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00011014 File Offset: 0x0000F214
		public override void EarlyStart()
		{
			this._allOneShotSoundEventsAreDisabled = false;
			this._arenaSoundEntity = base.Mission.Scene.FindEntityWithTag("arena_sound");
			SoundManager.SetGlobalParameter("ArenaIntensity", 0f);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00011047 File Offset: 0x0000F247
		public override void OnMissionScreenFinalize()
		{
			SoundManager.SetGlobalParameter("ArenaIntensity", 0f);
			MBMusicManager.Current.DeactivateBattleMode();
			MBMusicManager.Current.OnBattleMusicHandlerFinalize();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0001106C File Offset: 0x0000F26C
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectedAgent != null && affectorAgent != null && affectorAgent.IsMainAgent && (agentState == AgentState.Killed || agentState == AgentState.Unconscious))
			{
				if (affectedAgent.Team != affectorAgent.Team)
				{
					if (affectedAgent.IsHuman)
					{
						this._currentTournamentIntensity++;
						if (affectedAgent.HasMount)
						{
							this._currentTournamentIntensity++;
						}
						if (killingBlow.OverrideKillInfo == Agent.KillInfo.Headshot)
						{
							this._currentTournamentIntensity += 3;
						}
						if (killingBlow.IsMissile)
						{
							this._currentTournamentIntensity++;
						}
						else
						{
							this._currentTournamentIntensity += 2;
						}
					}
					else if (affectedAgent.RiderAgent != null)
					{
						this._currentTournamentIntensity += 3;
					}
				}
				this.UpdateAudienceIntensity();
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00011134 File Offset: 0x0000F334
		public override void OnMissionTick(float dt)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00011138 File Offset: 0x0000F338
		public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, WeaponComponentData attackerWeapon, bool isBlocked, bool isSiegeEngineHit, in Blow blow, in AttackCollisionData collisionData, float damagedHp, float hitDistance, float shotDifficulty)
		{
			if (affectorAgent != null && affectedAgent != null && affectorAgent.IsMainAgent && affectedAgent.IsHuman && affectedAgent.Position.Distance(affectorAgent.Position) >= 15f && (blow.VictimBodyPart == BoneBodyPartType.Head || blow.VictimBodyPart == BoneBodyPartType.Neck))
			{
				this._currentTournamentIntensity += 3;
				this.UpdateAudienceIntensity();
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0001119D File Offset: 0x0000F39D
		public override void OnMissileHit(Agent attacker, Agent victim, bool isCanceled, AttackCollisionData collisionData)
		{
			if (!isCanceled && attacker != null && victim != null && attacker.IsMainAgent && victim.IsHuman && collisionData.IsShieldBroken)
			{
				this._currentTournamentIntensity += 2;
				this.UpdateAudienceIntensity();
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000111D5 File Offset: 0x0000F3D5
		public override void OnMeleeHit(Agent attacker, Agent victim, bool isCanceled, AttackCollisionData collisionData)
		{
			if (!isCanceled && attacker != null && victim != null && attacker.IsMainAgent && victim.IsHuman && collisionData.IsShieldBroken)
			{
				this._currentTournamentIntensity += 2;
				this.UpdateAudienceIntensity();
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00011210 File Offset: 0x0000F410
		private void UpdateAudienceIntensity()
		{
			bool flag = false;
			if (this._currentTournamentIntensity > 60)
			{
				flag = (this._currentArenaIntensityLevel != MusicArenaPracticeMissionView.ArenaIntensityLevel.High);
				this._currentArenaIntensityLevel = MusicArenaPracticeMissionView.ArenaIntensityLevel.High;
			}
			else if (this._currentTournamentIntensity > 30)
			{
				flag = (this._currentArenaIntensityLevel != MusicArenaPracticeMissionView.ArenaIntensityLevel.Mid);
				this._currentArenaIntensityLevel = MusicArenaPracticeMissionView.ArenaIntensityLevel.Mid;
			}
			else if (this._currentTournamentIntensity <= 30)
			{
				flag = (this._currentArenaIntensityLevel != MusicArenaPracticeMissionView.ArenaIntensityLevel.Low);
				this._currentArenaIntensityLevel = MusicArenaPracticeMissionView.ArenaIntensityLevel.Low;
			}
			if (flag)
			{
				SoundManager.SetGlobalParameter("ArenaIntensity", (float)this._currentArenaIntensityLevel);
			}
			if (!this._allOneShotSoundEventsAreDisabled)
			{
				this.Cheer();
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000112A0 File Offset: 0x0000F4A0
		private void Cheer()
		{
			string eventFullName = "event:/mission/ambient/arena/reaction";
			Vec3 globalPosition = this._arenaSoundEntity.GlobalPosition;
			SoundManager.StartOneShotEvent(eventFullName, globalPosition);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000112C6 File Offset: 0x0000F4C6
		public void OnUpdated(float dt)
		{
		}

		// Token: 0x040000B9 RID: 185
		private const string ArenaSoundTag = "arena_sound";

		// Token: 0x040000BA RID: 186
		private const string ArenaIntensityParameterId = "ArenaIntensity";

		// Token: 0x040000BB RID: 187
		private const string ArenaPositiveReactionsSoundId = "event:/mission/ambient/arena/reaction";

		// Token: 0x040000BC RID: 188
		private const int MainAgentKnocksDownAnOpponentBaseIntensityChange = 1;

		// Token: 0x040000BD RID: 189
		private const int MainAgentKnocksDownAnOpponentHeadShotIntensityChange = 3;

		// Token: 0x040000BE RID: 190
		private const int MainAgentKnocksDownAnOpponentMountedTargetIntensityChange = 1;

		// Token: 0x040000BF RID: 191
		private const int MainAgentKnocksDownAnOpponentRangedHitIntensityChange = 1;

		// Token: 0x040000C0 RID: 192
		private const int MainAgentKnocksDownAnOpponentMeleeHitIntensityChange = 2;

		// Token: 0x040000C1 RID: 193
		private const int MainAgentHeadShotFrom15MetersRangeIntensityChange = 3;

		// Token: 0x040000C2 RID: 194
		private const int MainAgentDismountsAnOpponentIntensityChange = 3;

		// Token: 0x040000C3 RID: 195
		private const int MainAgentBreaksAShieldIntensityChange = 2;

		// Token: 0x040000C4 RID: 196
		private int _currentTournamentIntensity;

		// Token: 0x040000C5 RID: 197
		private MusicArenaPracticeMissionView.ArenaIntensityLevel _currentArenaIntensityLevel;

		// Token: 0x040000C6 RID: 198
		private bool _allOneShotSoundEventsAreDisabled;

		// Token: 0x040000C7 RID: 199
		private GameEntity _arenaSoundEntity;

		// Token: 0x02000074 RID: 116
		private enum ArenaIntensityLevel
		{
			// Token: 0x040002B2 RID: 690
			None,
			// Token: 0x040002B3 RID: 691
			Low,
			// Token: 0x040002B4 RID: 692
			Mid,
			// Token: 0x040002B5 RID: 693
			High
		}
	}
}
