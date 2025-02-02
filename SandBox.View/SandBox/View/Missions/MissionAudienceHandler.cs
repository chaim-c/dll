using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions
{
	// Token: 0x02000012 RID: 18
	public class MissionAudienceHandler : MissionView
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00005179 File Offset: 0x00003379
		public MissionAudienceHandler(float density)
		{
			this._density = density;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005198 File Offset: 0x00003398
		public override void EarlyStart()
		{
			this._allOneShotSoundEventsAreDisabled = true;
			this._audienceMidPoints = base.Mission.Scene.FindEntitiesWithTag("audience_mid_point").ToList<GameEntity>();
			this._arenaSoundEntity = base.Mission.Scene.FindEntityWithTag("arena_sound");
			this._audienceList = new List<KeyValuePair<GameEntity, float>>();
			if (this._audienceMidPoints.Count > 0)
			{
				this.OnInit();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005208 File Offset: 0x00003408
		public void OnInit()
		{
			this._minChance = MathF.Max(this._density - 0.5f, 0f);
			this._maxChance = this._density;
			this.GetAudienceEntities();
			this.SpawnAudienceAgents();
			this._lastOneShotSoundEventStarted = MissionTime.Zero;
			this._allOneShotSoundEventsAreDisabled = false;
			this._ambientSoundEvent = SoundManager.CreateEvent("event:/mission/ambient/detail/arena/arena", base.Mission.Scene);
			this._ambientSoundEvent.Play();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005282 File Offset: 0x00003482
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectorAgent != null && affectorAgent.IsHuman && affectedAgent.IsHuman)
			{
				this.Cheer(false);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000052A0 File Offset: 0x000034A0
		private void Cheer(bool onEnd = false)
		{
			if (!this._allOneShotSoundEventsAreDisabled)
			{
				string text = null;
				if (onEnd)
				{
					text = "event:/mission/ambient/detail/arena/cheer_big";
					this._allOneShotSoundEventsAreDisabled = true;
				}
				else if (this._lastOneShotSoundEventStarted.ElapsedSeconds > 4f && this._lastOneShotSoundEventStarted.ElapsedSeconds < 10f)
				{
					text = "event:/mission/ambient/detail/arena/cheer_medium";
				}
				else if (this._lastOneShotSoundEventStarted.ElapsedSeconds > 10f)
				{
					text = "event:/mission/ambient/detail/arena/cheer_small";
				}
				if (text != null)
				{
					Vec3 vec = (this._arenaSoundEntity != null) ? this._arenaSoundEntity.GlobalPosition : (this._audienceMidPoints.Any<GameEntity>() ? this._audienceMidPoints.GetRandomElement<GameEntity>().GlobalPosition : Vec3.Zero);
					SoundManager.StartOneShotEvent(text, vec);
					this._lastOneShotSoundEventStarted = MissionTime.Now;
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005368 File Offset: 0x00003568
		private void GetAudienceEntities()
		{
			this._maxDist = 0f;
			this._minDist = float.MaxValue;
			this._maxHeight = 0f;
			this._minHeight = float.MaxValue;
			foreach (GameEntity gameEntity in base.Mission.Scene.FindEntitiesWithTag("audience"))
			{
				float distanceSquareToArena = this.GetDistanceSquareToArena(gameEntity);
				this._maxDist = ((distanceSquareToArena > this._maxDist) ? distanceSquareToArena : this._maxDist);
				this._minDist = ((distanceSquareToArena < this._minDist) ? distanceSquareToArena : this._minDist);
				float z = gameEntity.GetFrame().origin.z;
				this._maxHeight = ((z > this._maxHeight) ? z : this._maxHeight);
				this._minHeight = ((z < this._minHeight) ? z : this._minHeight);
				this._audienceList.Add(new KeyValuePair<GameEntity, float>(gameEntity, distanceSquareToArena));
				gameEntity.SetVisibilityExcludeParents(false);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005484 File Offset: 0x00003684
		private float GetDistanceSquareToArena(GameEntity audienceEntity)
		{
			float num = float.MaxValue;
			foreach (GameEntity gameEntity in this._audienceMidPoints)
			{
				float num2 = gameEntity.GlobalPosition.DistanceSquared(audienceEntity.GlobalPosition);
				if (num2 < num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000054F0 File Offset: 0x000036F0
		private CharacterObject GetRandomAudienceCharacterToSpawn()
		{
			Settlement currentSettlement = Settlement.CurrentSettlement;
			CharacterObject characterObject = MBRandom.ChooseWeighted<CharacterObject>(new List<ValueTuple<CharacterObject, float>>
			{
				new ValueTuple<CharacterObject, float>(currentSettlement.Culture.Townswoman, 0.2f),
				new ValueTuple<CharacterObject, float>(currentSettlement.Culture.Townsman, 0.2f),
				new ValueTuple<CharacterObject, float>(currentSettlement.Culture.Armorer, 0.1f),
				new ValueTuple<CharacterObject, float>(currentSettlement.Culture.Merchant, 0.1f),
				new ValueTuple<CharacterObject, float>(currentSettlement.Culture.Musician, 0.1f),
				new ValueTuple<CharacterObject, float>(currentSettlement.Culture.Weaponsmith, 0.1f),
				new ValueTuple<CharacterObject, float>(currentSettlement.Culture.RansomBroker, 0.1f),
				new ValueTuple<CharacterObject, float>(currentSettlement.Culture.Barber, 0.05f),
				new ValueTuple<CharacterObject, float>(currentSettlement.Culture.FemaleDancer, 0.05f)
			});
			if (characterObject == null)
			{
				characterObject = ((MBRandom.RandomFloat < 0.65f) ? currentSettlement.Culture.Townsman : currentSettlement.Culture.Townswoman);
			}
			return characterObject;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000562C File Offset: 0x0000382C
		private void SpawnAudienceAgents()
		{
			for (int i = this._audienceList.Count - 1; i >= 0; i--)
			{
				KeyValuePair<GameEntity, float> keyValuePair = this._audienceList[i];
				float num = this._minChance + (1f - (keyValuePair.Value - this._minDist) / (this._maxDist - this._minDist)) * (this._maxChance - this._minChance);
				float num2 = this._minChance + (1f - MathF.Pow((keyValuePair.Key.GetFrame().origin.z - this._minHeight) / (this._maxHeight - this._minHeight), 2f)) * (this._maxChance - this._minChance);
				float num3 = num * 0.4f + num2 * 0.6f;
				if (MBRandom.RandomFloat < num3)
				{
					MatrixFrame globalFrame = keyValuePair.Key.GetGlobalFrame();
					CharacterObject randomAudienceCharacterToSpawn = this.GetRandomAudienceCharacterToSpawn();
					AgentBuildData agentBuildData = new AgentBuildData(randomAudienceCharacterToSpawn).InitialPosition(globalFrame.origin);
					Vec2 vec = new Vec2(-globalFrame.rotation.f.AsVec2.x, -globalFrame.rotation.f.AsVec2.y);
					AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).TroopOrigin(new SimpleAgentOrigin(randomAudienceCharacterToSpawn, -1, null, default(UniqueTroopDescriptor))).Team(Team.Invalid).CanSpawnOutsideOfMissionBoundary(true);
					Agent agent = Mission.Current.SpawnAgent(agentBuildData2, false);
					MBAnimation.PrefetchAnimationClip(agent.ActionSet, this._spectatorAction);
					agent.SetActionChannel(0, this._spectatorAction, true, 0UL, 0f, MBRandom.RandomFloatRanged(0.75f, 1f), -0.2f, 0.4f, MBRandom.RandomFloatRanged(0.01f, 1f), false, -0.2f, 0, true);
					agent.Controller = Agent.ControllerType.None;
					agent.ToggleInvulnerable();
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005805 File Offset: 0x00003A05
		public override void OnMissionTick(float dt)
		{
			if (this._audienceMidPoints == null)
			{
				return;
			}
			if (base.Mission.MissionEnded)
			{
				this.Cheer(true);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005824 File Offset: 0x00003A24
		public override void OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
		{
			if (oldMissionMode == MissionMode.Battle && Mission.Current.Mode == MissionMode.StartUp && Agent.Main != null && Agent.Main.IsActive())
			{
				this.Cheer(true);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005850 File Offset: 0x00003A50
		public override void OnMissionScreenFinalize()
		{
			SoundEvent ambientSoundEvent = this._ambientSoundEvent;
			if (ambientSoundEvent == null)
			{
				return;
			}
			ambientSoundEvent.Release();
		}

		// Token: 0x04000029 RID: 41
		private const int GapBetweenCheerSmallInSeconds = 10;

		// Token: 0x0400002A RID: 42
		private const int GapBetweenCheerMedium = 4;

		// Token: 0x0400002B RID: 43
		private float _minChance;

		// Token: 0x0400002C RID: 44
		private float _maxChance;

		// Token: 0x0400002D RID: 45
		private float _minDist;

		// Token: 0x0400002E RID: 46
		private float _maxDist;

		// Token: 0x0400002F RID: 47
		private float _minHeight;

		// Token: 0x04000030 RID: 48
		private float _maxHeight;

		// Token: 0x04000031 RID: 49
		private List<GameEntity> _audienceMidPoints;

		// Token: 0x04000032 RID: 50
		private List<KeyValuePair<GameEntity, float>> _audienceList;

		// Token: 0x04000033 RID: 51
		private readonly float _density;

		// Token: 0x04000034 RID: 52
		private GameEntity _arenaSoundEntity;

		// Token: 0x04000035 RID: 53
		private SoundEvent _ambientSoundEvent;

		// Token: 0x04000036 RID: 54
		private MissionTime _lastOneShotSoundEventStarted;

		// Token: 0x04000037 RID: 55
		private bool _allOneShotSoundEventsAreDisabled;

		// Token: 0x04000038 RID: 56
		private ActionIndexCache _spectatorAction = ActionIndexCache.Create("act_arena_spectator");
	}
}
