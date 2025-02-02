using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.ComponentInterfaces;
using TaleWorlds.MountAndBlade.Network;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001BD RID: 445
	public sealed class Mission : DotNetObject, IMission
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00050617 File Offset: 0x0004E817
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x0005061F File Offset: 0x0004E81F
		internal UIntPtr Pointer { get; private set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x00050628 File Offset: 0x0004E828
		public bool IsFinalized
		{
			get
			{
				return this.Pointer == UIntPtr.Zero;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0005063A File Offset: 0x0004E83A
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x00050647 File Offset: 0x0004E847
		public static Mission Current
		{
			get
			{
				Mission current = Mission._current;
				return Mission._current;
			}
			private set
			{
				if (value == null)
				{
					Mission current = Mission._current;
				}
				Mission._current = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00050658 File Offset: 0x0004E858
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x00050660 File Offset: 0x0004E860
		private MissionInitializerRecord InitializerRecord { get; set; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00050669 File Offset: 0x0004E869
		public string SceneName
		{
			get
			{
				return this.InitializerRecord.SceneName;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00050676 File Offset: 0x0004E876
		public string SceneLevels
		{
			get
			{
				return this.InitializerRecord.SceneLevels;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00050683 File Offset: 0x0004E883
		public float DamageToPlayerMultiplier
		{
			get
			{
				return this.InitializerRecord.DamageToPlayerMultiplier;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00050690 File Offset: 0x0004E890
		public float DamageToFriendsMultiplier
		{
			get
			{
				return this.InitializerRecord.DamageToFriendsMultiplier;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0005069D File Offset: 0x0004E89D
		public float DamageFromPlayerToFriendsMultiplier
		{
			get
			{
				return this.InitializerRecord.DamageFromPlayerToFriendsMultiplier;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x000506AA File Offset: 0x0004E8AA
		public bool HasValidTerrainType
		{
			get
			{
				return this.InitializerRecord.TerrainType >= 0;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x000506BD File Offset: 0x0004E8BD
		public TerrainType TerrainType
		{
			get
			{
				if (!this.HasValidTerrainType)
				{
					return TerrainType.Water;
				}
				return (TerrainType)this.InitializerRecord.TerrainType;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x000506D4 File Offset: 0x0004E8D4
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x000506DC File Offset: 0x0004E8DC
		public Scene Scene { get; private set; }

		// Token: 0x06001809 RID: 6153 RVA: 0x000506E8 File Offset: 0x0004E8E8
		public IEnumerable<GameEntity> GetActiveEntitiesWithScriptComponentOfType<T>()
		{
			return from amo in this._activeMissionObjects
			where amo is T
			select amo.GameEntity;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00050743 File Offset: 0x0004E943
		public void AddActiveMissionObject(MissionObject missionObject)
		{
			this._missionObjects.Add(missionObject);
			this._activeMissionObjects.Add(missionObject);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0005075D File Offset: 0x0004E95D
		public void ActivateMissionObject(MissionObject missionObject)
		{
			this._activeMissionObjects.Add(missionObject);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0005076B File Offset: 0x0004E96B
		public void DeactivateMissionObject(MissionObject missionObject)
		{
			this._activeMissionObjects.Remove(missionObject);
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0005077A File Offset: 0x0004E97A
		public MBReadOnlyList<MissionObject> ActiveMissionObjects
		{
			get
			{
				return this._activeMissionObjects;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00050782 File Offset: 0x0004E982
		public MBReadOnlyList<MissionObject> MissionObjects
		{
			get
			{
				return this._missionObjects;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0005078A File Offset: 0x0004E98A
		public MBReadOnlyList<Mission.DynamicallyCreatedEntity> AddedEntitiesInfo
		{
			get
			{
				return this._addedEntitiesInfo;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00050792 File Offset: 0x0004E992
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x0005079A File Offset: 0x0004E99A
		public Mission.MBBoundaryCollection Boundaries { get; private set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x000507A4 File Offset: 0x0004E9A4
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x000507F8 File Offset: 0x0004E9F8
		public bool IsMainAgentObjectInteractionEnabled
		{
			get
			{
				switch (this._missionMode)
				{
				case MissionMode.Conversation:
				case MissionMode.Barter:
				case MissionMode.Deployment:
				case MissionMode.Replay:
				case MissionMode.CutScene:
					return false;
				}
				return !this.MissionEnded && this._isMainAgentObjectInteractionEnabled;
			}
			set
			{
				this._isMainAgentObjectInteractionEnabled = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00050804 File Offset: 0x0004EA04
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x0005084E File Offset: 0x0004EA4E
		public bool IsMainAgentItemInteractionEnabled
		{
			get
			{
				switch (this._missionMode)
				{
				case MissionMode.Conversation:
				case MissionMode.Barter:
				case MissionMode.Deployment:
				case MissionMode.Replay:
				case MissionMode.CutScene:
					return false;
				}
				return this._isMainAgentItemInteractionEnabled;
			}
			set
			{
				this._isMainAgentItemInteractionEnabled = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00050857 File Offset: 0x0004EA57
		// (set) Token: 0x06001817 RID: 6167 RVA: 0x0005085F File Offset: 0x0004EA5F
		public bool IsTeleportingAgents { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x00050868 File Offset: 0x0004EA68
		// (set) Token: 0x06001819 RID: 6169 RVA: 0x00050870 File Offset: 0x0004EA70
		public bool ForceTickOccasionally { get; set; }

		// Token: 0x0600181A RID: 6170 RVA: 0x00050879 File Offset: 0x0004EA79
		private void FinalizeMission()
		{
			TeamAISiegeComponent.OnMissionFinalize();
			MBAPI.IMBMission.FinalizeMission(this.Pointer);
			this.Pointer = UIntPtr.Zero;
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0005089B File Offset: 0x0004EA9B
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x000508AD File Offset: 0x0004EAAD
		public Mission.MissionCombatType CombatType
		{
			get
			{
				return (Mission.MissionCombatType)MBAPI.IMBMission.GetCombatType(this.Pointer);
			}
			set
			{
				MBAPI.IMBMission.SetCombatType(this.Pointer, (int)value);
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x000508C0 File Offset: 0x0004EAC0
		public void SetMissionCombatType(Mission.MissionCombatType missionCombatType)
		{
			MBAPI.IMBMission.SetCombatType(this.Pointer, (int)missionCombatType);
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x000508D3 File Offset: 0x0004EAD3
		public MissionMode Mode
		{
			get
			{
				return this._missionMode;
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000508DC File Offset: 0x0004EADC
		public void ConversationCharacterChanged()
		{
			foreach (IMissionListener missionListener in this._listeners)
			{
				missionListener.OnConversationCharacterChanged();
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0005092C File Offset: 0x0004EB2C
		public void SetMissionMode(MissionMode newMode, bool atStart)
		{
			if (this._missionMode != newMode)
			{
				MissionMode missionMode = this._missionMode;
				this._missionMode = newMode;
				if (this.CurrentState != Mission.State.Over)
				{
					for (int i = 0; i < this.MissionBehaviors.Count; i++)
					{
						this.MissionBehaviors[i].OnMissionModeChange(missionMode, atStart);
					}
					foreach (IMissionListener missionListener in this._listeners)
					{
						missionListener.OnMissionModeChange(missionMode, atStart);
					}
				}
			}
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000509C8 File Offset: 0x0004EBC8
		private Mission.AgentCreationResult CreateAgentInternal(AgentFlag agentFlags, int forcedAgentIndex, bool isFemale, ref AgentSpawnData spawnData, ref AgentCapsuleData capsuleData, ref AnimationSystemData animationSystemData, int instanceNo)
		{
			return MBAPI.IMBMission.CreateAgent(this.Pointer, (ulong)agentFlags, forcedAgentIndex, isFemale, ref spawnData, ref capsuleData.BodyCap, ref capsuleData.CrouchedBodyCap, ref animationSystemData, instanceNo);
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x000509FD File Offset: 0x0004EBFD
		public float CurrentTime
		{
			get
			{
				return this._cachedMissionTime;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x00050A05 File Offset: 0x0004EC05
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x00050A17 File Offset: 0x0004EC17
		public bool PauseAITick
		{
			get
			{
				return MBAPI.IMBMission.GetPauseAITick(this.Pointer);
			}
			set
			{
				MBAPI.IMBMission.SetPauseAITick(this.Pointer, value);
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00050A2A File Offset: 0x0004EC2A
		[UsedImplicitly]
		[MBCallback]
		internal void UpdateMissionTimeCache(float curTime)
		{
			this._cachedMissionTime = curTime;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00050A33 File Offset: 0x0004EC33
		public float GetAverageFps()
		{
			return MBAPI.IMBMission.GetAverageFps(this.Pointer);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00050A45 File Offset: 0x0004EC45
		public static bool ToggleDisableFallAvoid()
		{
			return MBAPI.IMBMission.ToggleDisableFallAvoid();
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00050A51 File Offset: 0x0004EC51
		public bool IsPositionInsideBoundaries(Vec2 position)
		{
			return MBAPI.IMBMission.IsPositionInsideBoundaries(this.Pointer, position);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00050A64 File Offset: 0x0004EC64
		public bool IsPositionInsideAnyBlockerNavMeshFace2D(Vec2 position)
		{
			return MBAPI.IMBMission.IsPositionInsideAnyBlockerNavMeshFace2D(this.Pointer, position);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00050A77 File Offset: 0x0004EC77
		private bool IsFormationUnitPositionAvailableAux(ref WorldPosition formationPosition, ref WorldPosition unitPosition, ref WorldPosition nearestAvailableUnitPosition, float manhattanDistance)
		{
			return MBAPI.IMBMission.IsFormationUnitPositionAvailable(this.Pointer, ref formationPosition, ref unitPosition, ref nearestAvailableUnitPosition, manhattanDistance);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00050A8E File Offset: 0x0004EC8E
		public Agent RayCastForClosestAgent(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, int excludedAgentIndex = -1, float rayThickness = 0.01f)
		{
			collisionDistance = float.NaN;
			return MBAPI.IMBMission.RayCastForClosestAgent(this.Pointer, sourcePoint, targetPoint, excludedAgentIndex, ref collisionDistance, rayThickness);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00050AAE File Offset: 0x0004ECAE
		public bool RayCastForClosestAgentsLimbs(Vec3 sourcePoint, Vec3 targetPoint, int excludeAgentIndex, out float collisionDistance, ref int agentIndex, ref sbyte boneIndex)
		{
			collisionDistance = float.NaN;
			return MBAPI.IMBMission.RayCastForClosestAgentsLimbs(this.Pointer, sourcePoint, targetPoint, excludeAgentIndex, ref collisionDistance, ref agentIndex, ref boneIndex);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00050AD1 File Offset: 0x0004ECD1
		public bool RayCastForClosestAgentsLimbs(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, ref int agentIndex, ref sbyte boneIndex)
		{
			collisionDistance = float.NaN;
			return MBAPI.IMBMission.RayCastForClosestAgentsLimbs(this.Pointer, sourcePoint, targetPoint, -1, ref collisionDistance, ref agentIndex, ref boneIndex);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00050AF2 File Offset: 0x0004ECF2
		public bool RayCastForGivenAgentsLimbs(Vec3 sourcePoint, Vec3 rayFinishPoint, int givenAgentIndex, ref float collisionDistance, ref sbyte boneIndex)
		{
			return MBAPI.IMBMission.RayCastForGivenAgentsLimbs(this.Pointer, sourcePoint, rayFinishPoint, givenAgentIndex, ref collisionDistance, ref boneIndex);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00050B0B File Offset: 0x0004ED0B
		internal AgentProximityMap.ProximityMapSearchStructInternal ProximityMapBeginSearch(Vec2 searchPos, float searchRadius)
		{
			return MBAPI.IMBMission.ProximityMapBeginSearch(this.Pointer, searchPos, searchRadius);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00050B1F File Offset: 0x0004ED1F
		internal float ProximityMapMaxSearchRadius()
		{
			return MBAPI.IMBMission.ProximityMapMaxSearchRadius(this.Pointer);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00050B31 File Offset: 0x0004ED31
		public float GetBiggestAgentCollisionPadding()
		{
			return MBAPI.IMBMission.GetBiggestAgentCollisionPadding(this.Pointer);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00050B43 File Offset: 0x0004ED43
		public void SetMissionCorpseFadeOutTimeInSeconds(float corpseFadeOutTimeInSeconds)
		{
			MBAPI.IMBMission.SetMissionCorpseFadeOutTimeInSeconds(this.Pointer, corpseFadeOutTimeInSeconds);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00050B56 File Offset: 0x0004ED56
		public void SetReportStuckAgentsMode(bool value)
		{
			MBAPI.IMBMission.SetReportStuckAgentsMode(this.Pointer, value);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00050B6C File Offset: 0x0004ED6C
		internal void BatchFormationUnitPositions(MBArrayList<Vec2i> orderedPositionIndices, MBArrayList<Vec2> orderedLocalPositions, MBList2D<int> availabilityTable, MBList2D<WorldPosition> globalPositionTable, WorldPosition orderPosition, Vec2 direction, int fileCount, int rankCount)
		{
			MBAPI.IMBMission.BatchFormationUnitPositions(this.Pointer, orderedPositionIndices.RawArray, orderedLocalPositions.RawArray, availabilityTable.RawArray, globalPositionTable.RawArray, orderPosition, direction, fileCount, rankCount);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00050BAA File Offset: 0x0004EDAA
		internal void ProximityMapFindNext(ref AgentProximityMap.ProximityMapSearchStructInternal searchStruct)
		{
			MBAPI.IMBMission.ProximityMapFindNext(this.Pointer, ref searchStruct);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00050BC0 File Offset: 0x0004EDC0
		[UsedImplicitly]
		[MBCallback]
		public void ResetMission()
		{
			IMissionListener[] array = this._listeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnResetMission();
			}
			foreach (Agent agent in this._activeAgents)
			{
				agent.OnRemove();
			}
			foreach (Agent agent2 in this._allAgents)
			{
				agent2.OnDelete();
			}
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnClearScene();
			}
			this.NumOfFormationsSpawnedTeamOne = 0;
			this.NumOfFormationsSpawnedTeamTwo = 0;
			foreach (Team team in this.Teams)
			{
				team.Reset();
			}
			MBAPI.IMBMission.ClearScene(this.Pointer);
			this._activeAgents.Clear();
			this._allAgents.Clear();
			this._mountsWithoutRiders.Clear();
			this.MainAgent = null;
			this.ClearMissiles();
			this._missiles.Clear();
			this._agentCount = 0;
			for (int j = 0; j < 2; j++)
			{
				this._initialAgentCountPerSide[j] = 0;
				this._removedAgentCountPerSide[j] = 0;
			}
			this.ResetMissionObjects();
			this.RemoveSpawnedMissionObjects();
			this._activeMissionObjects.Clear();
			this._activeMissionObjects.AddRange(this.MissionObjects);
			this.Scene.ClearDecals();
			PropertyChangedEventHandler onMissionReset = this.OnMissionReset;
			if (onMissionReset == null)
			{
				return;
			}
			onMissionReset(this, null);
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001837 RID: 6199 RVA: 0x00050DB8 File Offset: 0x0004EFB8
		// (remove) Token: 0x06001838 RID: 6200 RVA: 0x00050DF0 File Offset: 0x0004EFF0
		public event PropertyChangedEventHandler OnMissionReset;

		// Token: 0x06001839 RID: 6201 RVA: 0x00050E28 File Offset: 0x0004F028
		public void Initialize()
		{
			Mission.Current = this;
			this.CurrentState = Mission.State.Initializing;
			MissionInitializerRecord initializerRecord = this.InitializerRecord;
			MBAPI.IMBMission.InitializeMission(this.Pointer, ref initializerRecord);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00050E5B File Offset: 0x0004F05B
		[UsedImplicitly]
		[MBCallback]
		internal void OnSceneCreated(Scene scene)
		{
			this.Scene = scene;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00050E64 File Offset: 0x0004F064
		[UsedImplicitly]
		[MBCallback]
		internal void TickAgentsAndTeams(float dt)
		{
			this.TickAgentsAndTeamsImp(dt);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00050E6D File Offset: 0x0004F06D
		public void TickAgentsAndTeamsAsync(float dt)
		{
			MBAPI.IMBMission.tickAgentsAndTeamsAsync(this.Pointer, dt);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00050E80 File Offset: 0x0004F080
		internal void Tick(float dt)
		{
			MBAPI.IMBMission.Tick(this.Pointer, dt);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00050E93 File Offset: 0x0004F093
		internal void IdleTick(float dt)
		{
			MBAPI.IMBMission.IdleTick(this.Pointer, dt);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00050EA6 File Offset: 0x0004F0A6
		public void MakeSound(int soundIndex, Vec3 position, bool soundCanBePredicted, bool isReliable, int relatedAgent1, int relatedAgent2)
		{
			MBAPI.IMBMission.MakeSound(this.Pointer, soundIndex, position, soundCanBePredicted, isReliable, relatedAgent1, relatedAgent2);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00050EC4 File Offset: 0x0004F0C4
		public void MakeSound(int soundIndex, Vec3 position, bool soundCanBePredicted, bool isReliable, int relatedAgent1, int relatedAgent2, ref SoundEventParameter parameter)
		{
			MBAPI.IMBMission.MakeSoundWithParameter(this.Pointer, soundIndex, position, soundCanBePredicted, isReliable, relatedAgent1, relatedAgent2, parameter);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00050EF1 File Offset: 0x0004F0F1
		public void MakeSoundOnlyOnRelatedPeer(int soundIndex, Vec3 position, int relatedAgent)
		{
			MBAPI.IMBMission.MakeSoundOnlyOnRelatedPeer(this.Pointer, soundIndex, position, relatedAgent);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00050F06 File Offset: 0x0004F106
		public void AddSoundAlarmFactorToAgents(int ownerId, Vec3 position, float alarmFactor)
		{
			MBAPI.IMBMission.AddSoundAlarmFactorToAgents(this.Pointer, ownerId, position, alarmFactor);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00050F1B File Offset: 0x0004F11B
		public void AddDynamicallySpawnedMissionObjectInfo(Mission.DynamicallyCreatedEntity entityInfo)
		{
			this._addedEntitiesInfo.Add(entityInfo);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00050F2C File Offset: 0x0004F12C
		private void RemoveDynamicallySpawnedMissionObjectInfo(MissionObjectId id)
		{
			Mission.DynamicallyCreatedEntity dynamicallyCreatedEntity = this._addedEntitiesInfo.FirstOrDefault((Mission.DynamicallyCreatedEntity x) => x.ObjectId == id);
			if (dynamicallyCreatedEntity != null)
			{
				this._addedEntitiesInfo.Remove(dynamicallyCreatedEntity);
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00050F70 File Offset: 0x0004F170
		private int AddMissileAux(int forcedMissileIndex, bool isPrediction, Agent shooterAgent, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, float damageBonus, ref Vec3 position, ref Vec3 direction, ref Mat3 orientation, float baseSpeed, float speed, bool addRigidBody, GameEntity gameEntityToIgnore, bool isPrimaryWeaponShot, out GameEntity missileEntity)
		{
			UIntPtr pointer;
			int result = MBAPI.IMBMission.AddMissile(this.Pointer, isPrediction, shooterAgent.Index, weaponData, weaponStatsData, weaponStatsData.Length, damageBonus, ref position, ref direction, ref orientation, baseSpeed, speed, addRigidBody, (gameEntityToIgnore != null) ? gameEntityToIgnore.Pointer : UIntPtr.Zero, forcedMissileIndex, isPrimaryWeaponShot, out pointer);
			missileEntity = (isPrediction ? null : new GameEntity(pointer));
			return result;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00050FD0 File Offset: 0x0004F1D0
		private int AddMissileSingleUsageAux(int forcedMissileIndex, bool isPrediction, Agent shooterAgent, in WeaponData weaponData, in WeaponStatsData weaponStatsData, float damageBonus, ref Vec3 position, ref Vec3 direction, ref Mat3 orientation, float baseSpeed, float speed, bool addRigidBody, GameEntity gameEntityToIgnore, bool isPrimaryWeaponShot, out GameEntity missileEntity)
		{
			UIntPtr pointer;
			int result = MBAPI.IMBMission.AddMissileSingleUsage(this.Pointer, isPrediction, shooterAgent.Index, weaponData, weaponStatsData, damageBonus, ref position, ref direction, ref orientation, baseSpeed, speed, addRigidBody, (gameEntityToIgnore != null) ? gameEntityToIgnore.Pointer : UIntPtr.Zero, forcedMissileIndex, isPrimaryWeaponShot, out pointer);
			missileEntity = (isPrediction ? null : new GameEntity(pointer));
			return result;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0005102C File Offset: 0x0004F22C
		public Vec3 GetMissileCollisionPoint(Vec3 missileStartingPosition, Vec3 missileDirection, float missileSpeed, in WeaponData weaponData)
		{
			return MBAPI.IMBMission.GetMissileCollisionPoint(this.Pointer, missileStartingPosition, missileDirection, missileSpeed, weaponData);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00051043 File Offset: 0x0004F243
		public void RemoveMissileAsClient(int missileIndex)
		{
			MBAPI.IMBMission.RemoveMissile(this.Pointer, missileIndex);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00051056 File Offset: 0x0004F256
		public static float GetMissileVerticalAimCorrection(Vec3 vecToTarget, float missileStartingSpeed, ref WeaponStatsData weaponStatsData, float airFrictionConstant)
		{
			return MBAPI.IMBMission.GetMissileVerticalAimCorrection(vecToTarget, missileStartingSpeed, ref weaponStatsData, airFrictionConstant);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00051066 File Offset: 0x0004F266
		public static float GetMissileRange(float missileStartingSpeed, float heightDifference)
		{
			return MBAPI.IMBMission.GetMissileRange(missileStartingSpeed, heightDifference);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00051074 File Offset: 0x0004F274
		public void PrepareMissileWeaponForDrop(int missileIndex)
		{
			MBAPI.IMBMission.PrepareMissileWeaponForDrop(this.Pointer, missileIndex);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00051087 File Offset: 0x0004F287
		public void AddParticleSystemBurstByName(string particleSystem, MatrixFrame frame, bool synchThroughNetwork)
		{
			MBAPI.IMBMission.AddParticleSystemBurstByName(this.Pointer, particleSystem, ref frame, synchThroughNetwork);
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0005109D File Offset: 0x0004F29D
		public int EnemyAlarmStateIndicator
		{
			get
			{
				return MBAPI.IMBMission.GetEnemyAlarmStateIndicator(this.Pointer);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x000510AF File Offset: 0x0004F2AF
		public float PlayerAlarmIndicator
		{
			get
			{
				return MBAPI.IMBMission.GetPlayerAlarmIndicator(this.Pointer);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000510C1 File Offset: 0x0004F2C1
		public bool IsLoadingFinished
		{
			get
			{
				return MBAPI.IMBMission.GetIsLoadingFinished(this.Pointer);
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000510D3 File Offset: 0x0004F2D3
		public Vec2 GetClosestBoundaryPosition(Vec2 position)
		{
			return MBAPI.IMBMission.GetClosestBoundaryPosition(this.Pointer, position);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000510E8 File Offset: 0x0004F2E8
		private void ResetMissionObjects()
		{
			for (int i = this._dynamicEntities.Count - 1; i >= 0; i--)
			{
				Mission.DynamicEntityInfo dynamicEntityInfo = this._dynamicEntities[i];
				dynamicEntityInfo.Entity.RemoveEnginePhysics();
				dynamicEntityInfo.Entity.Remove(74);
				this._dynamicEntities.RemoveAt(i);
			}
			foreach (MissionObject missionObject in this.MissionObjects)
			{
				if (missionObject.CreatedAtRuntime)
				{
					break;
				}
				missionObject.OnMissionReset();
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0005118C File Offset: 0x0004F38C
		private void RemoveSpawnedMissionObjects()
		{
			MissionObject[] array = this._missionObjects.ToArray();
			for (int i = array.Length - 1; i >= 0; i--)
			{
				MissionObject missionObject = array[i];
				if (!missionObject.CreatedAtRuntime)
				{
					break;
				}
				if (missionObject.GameEntity != null)
				{
					missionObject.GameEntity.RemoveAllChildren();
					missionObject.GameEntity.Remove(75);
				}
			}
			this._spawnedItemEntitiesCreatedAtRuntime.Clear();
			this._lastRuntimeMissionObjectIdCount = 0;
			this._emptyRuntimeMissionObjectIds.Clear();
			this._addedEntitiesInfo.Clear();
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00051210 File Offset: 0x0004F410
		public int GetFreeRuntimeMissionObjectId()
		{
			float totalMissionTime = MBCommon.GetTotalMissionTime();
			int result = -1;
			if (this._emptyRuntimeMissionObjectIds.Count > 0)
			{
				if (totalMissionTime - this._emptyRuntimeMissionObjectIds.Peek().Item2 > 30f || this._lastRuntimeMissionObjectIdCount >= 4095)
				{
					result = this._emptyRuntimeMissionObjectIds.Pop().Item1;
				}
				else
				{
					result = this._lastRuntimeMissionObjectIdCount;
					this._lastRuntimeMissionObjectIdCount++;
				}
			}
			else if (this._lastRuntimeMissionObjectIdCount < 4095)
			{
				result = this._lastRuntimeMissionObjectIdCount;
				this._lastRuntimeMissionObjectIdCount++;
			}
			return result;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x000512A6 File Offset: 0x0004F4A6
		private void ReturnRuntimeMissionObjectId(int id)
		{
			this._emptyRuntimeMissionObjectIds.Push(new ValueTuple<int, float>(id, MBCommon.GetTotalMissionTime()));
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000512BE File Offset: 0x0004F4BE
		public int GetFreeSceneMissionObjectId()
		{
			int lastSceneMissionObjectIdCount = this._lastSceneMissionObjectIdCount;
			this._lastSceneMissionObjectIdCount++;
			return lastSceneMissionObjectIdCount;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000512D4 File Offset: 0x0004F4D4
		public void SetCameraFrame(ref MatrixFrame cameraFrame, float zoomFactor)
		{
			this.SetCameraFrame(ref cameraFrame, zoomFactor, ref cameraFrame.origin);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000512E4 File Offset: 0x0004F4E4
		public void SetCameraFrame(ref MatrixFrame cameraFrame, float zoomFactor, ref Vec3 attenuationPosition)
		{
			cameraFrame.Fill();
			MBAPI.IMBMission.SetCameraFrame(this.Pointer, ref cameraFrame, zoomFactor, ref attenuationPosition);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000512FF File Offset: 0x0004F4FF
		public MatrixFrame GetCameraFrame()
		{
			return MBAPI.IMBMission.GetCameraFrame(this.Pointer);
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x00051311 File Offset: 0x0004F511
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x00051318 File Offset: 0x0004F518
		public bool CameraIsFirstPerson
		{
			get
			{
				return Mission._isCameraFirstPerson;
			}
			set
			{
				if (Mission._isCameraFirstPerson != value)
				{
					Mission._isCameraFirstPerson = value;
					MBAPI.IMBMission.SetCameraIsFirstPerson(value);
					this.ResetFirstThirdPersonView();
				}
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00051339 File Offset: 0x0004F539
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x00051340 File Offset: 0x0004F540
		public static float CameraAddedDistance
		{
			get
			{
				return BannerlordConfig.CombatCameraDistance;
			}
			set
			{
				if (value != BannerlordConfig.CombatCameraDistance)
				{
					BannerlordConfig.CombatCameraDistance = value;
				}
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x00051350 File Offset: 0x0004F550
		public float ClearSceneTimerElapsedTime
		{
			get
			{
				return MBAPI.IMBMission.GetClearSceneTimerElapsedTime(this.Pointer);
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00051362 File Offset: 0x0004F562
		public void ResetFirstThirdPersonView()
		{
			MBAPI.IMBMission.ResetFirstThirdPersonView(this.Pointer);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00051374 File Offset: 0x0004F574
		internal void UpdateSceneTimeSpeed()
		{
			if (this.Scene != null)
			{
				float num = 1f;
				int num2 = -1;
				for (int i = 0; i < this._timeSpeedRequests.Count; i++)
				{
					if (this._timeSpeedRequests[i].RequestedTimeSpeed < num)
					{
						num = this._timeSpeedRequests[i].RequestedTimeSpeed;
						num2 = this._timeSpeedRequests[i].RequestID;
					}
				}
				if (!this.Scene.TimeSpeed.ApproximatelyEqualsTo(num, 1E-05f))
				{
					if (num2 != -1)
					{
						Debug.Print(string.Format("Updated mission time speed with request ID:{0}, time speed{1}", num2, num), 0, Debug.DebugColor.White, 17592186044416UL);
					}
					else
					{
						Debug.Print(string.Format("Reverted time speed back to default({0})", num), 0, Debug.DebugColor.White, 17592186044416UL);
					}
					this.Scene.TimeSpeed = num;
				}
			}
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00051463 File Offset: 0x0004F663
		public void AddTimeSpeedRequest(Mission.TimeSpeedRequest request)
		{
			this._timeSpeedRequests.Add(request);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00051474 File Offset: 0x0004F674
		[Conditional("_RGL_KEEP_ASSERTS")]
		private void AssertTimeSpeedRequestDoesntExist(Mission.TimeSpeedRequest request)
		{
			for (int i = 0; i < this._timeSpeedRequests.Count; i++)
			{
				int requestID = this._timeSpeedRequests[i].RequestID;
				int requestID2 = request.RequestID;
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000514B4 File Offset: 0x0004F6B4
		public void RemoveTimeSpeedRequest(int timeSpeedRequestID)
		{
			int index = -1;
			for (int i = 0; i < this._timeSpeedRequests.Count; i++)
			{
				if (this._timeSpeedRequests[i].RequestID == timeSpeedRequestID)
				{
					index = i;
				}
			}
			this._timeSpeedRequests.RemoveAt(index);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00051500 File Offset: 0x0004F700
		public bool GetRequestedTimeSpeed(int timeSpeedRequestID, out float requestedTime)
		{
			for (int i = 0; i < this._timeSpeedRequests.Count; i++)
			{
				if (this._timeSpeedRequests[i].RequestID == timeSpeedRequestID)
				{
					requestedTime = this._timeSpeedRequests[i].RequestedTimeSpeed;
					return true;
				}
			}
			requestedTime = 0f;
			return false;
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0005155A File Offset: 0x0004F75A
		public void ClearAgentActions()
		{
			MBAPI.IMBMission.ClearAgentActions(this.Pointer);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0005156C File Offset: 0x0004F76C
		public void ClearMissiles()
		{
			MBAPI.IMBMission.ClearMissiles(this.Pointer);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0005157E File Offset: 0x0004F77E
		public void ClearCorpses(bool isMissionReset)
		{
			MBAPI.IMBMission.ClearCorpses(this.Pointer, isMissionReset);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00051591 File Offset: 0x0004F791
		private Agent FindAgentWithIndexAux(int index)
		{
			if (index >= 0)
			{
				return MBAPI.IMBMission.FindAgentWithIndex(this.Pointer, index);
			}
			return null;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000515AA File Offset: 0x0004F7AA
		private Agent GetClosestEnemyAgent(MBTeam team, Vec3 position, float radius)
		{
			return MBAPI.IMBMission.GetClosestEnemy(this.Pointer, team.Index, position, radius);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x000515C4 File Offset: 0x0004F7C4
		private Agent GetClosestAllyAgent(MBTeam team, Vec3 position, float radius)
		{
			return MBAPI.IMBMission.GetClosestAlly(this.Pointer, team.Index, position, radius);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000515E0 File Offset: 0x0004F7E0
		private int GetNearbyEnemyAgentCount(MBTeam team, Vec2 position, float radius)
		{
			int num = 0;
			int result = 0;
			MBAPI.IMBMission.GetAgentCountAroundPosition(this.Pointer, team.Index, position, radius, ref num, ref result);
			return result;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0005160E File Offset: 0x0004F80E
		public bool IsAgentInProximityMap(Agent agent)
		{
			return MBAPI.IMBMission.IsAgentInProximityMap(this.Pointer, agent.Index);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00051628 File Offset: 0x0004F828
		public void OnMissionStateActivate()
		{
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnMissionStateActivated();
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00051678 File Offset: 0x0004F878
		public void OnMissionStateDeactivate()
		{
			if (this.MissionBehaviors != null)
			{
				foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
				{
					missionBehavior.OnMissionStateDeactivated();
				}
			}
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000516D0 File Offset: 0x0004F8D0
		public void OnMissionStateFinalize(bool forceClearGPUResources)
		{
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnMissionStateFinalized();
			}
			if (GameNetwork.IsSessionActive && this.GetMissionBehavior<MissionNetworkComponent>() != null)
			{
				this.RemoveMissionBehavior(this.GetMissionBehavior<MissionNetworkComponent>());
			}
			for (int i = this.MissionBehaviors.Count - 1; i >= 0; i--)
			{
				this.RemoveMissionBehavior(this.MissionBehaviors[i]);
			}
			this.MissionLogics.Clear();
			this.Scene = null;
			Mission.Current = null;
			this.ClearUnreferencedResources(forceClearGPUResources);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00051784 File Offset: 0x0004F984
		public void ClearUnreferencedResources(bool forceClearGPUResources)
		{
			Common.MemoryCleanupGC(false);
			if (forceClearGPUResources)
			{
				MBAPI.IMBMission.ClearResources(this.Pointer);
			}
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000517A0 File Offset: 0x0004F9A0
		internal void OnEntityHit(GameEntity entity, Agent attackerAgent, int inflictedDamage, DamageTypes damageType, Vec3 impactPosition, Vec3 impactDirection, in MissionWeapon weapon)
		{
			bool flag = false;
			while (entity != null)
			{
				bool flag2 = false;
				using (IEnumerator<MissionObject> enumerator = entity.GetScriptComponents<MissionObject>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						bool flag3;
						if (enumerator.Current.OnHit(attackerAgent, inflictedDamage, impactPosition, impactDirection, weapon, null, out flag3))
						{
							flag2 = true;
						}
						flag = (flag || flag3);
					}
				}
				if (flag2)
				{
					break;
				}
				entity = entity.Parent;
			}
			if (flag && attackerAgent != null && !attackerAgent.IsMount && !attackerAgent.IsAIControlled)
			{
				bool isVictimAgentSameAsAttackerAgent = false;
				bool isHuman = attackerAgent.IsHuman;
				bool isMine = attackerAgent.IsMine;
				bool doesAttackerAgentHaveRiderAgent = attackerAgent.RiderAgent != null;
				Agent riderAgent = attackerAgent.RiderAgent;
				this.AddCombatLogSafe(attackerAgent, null, entity, new CombatLogData(isVictimAgentSameAsAttackerAgent, isHuman, isMine, doesAttackerAgentHaveRiderAgent, riderAgent != null && riderAgent.IsMine, attackerAgent.IsMount, false, false, false, false, false, false, true, false, false, false, 0f)
				{
					DamageType = damageType,
					InflictedDamage = inflictedDamage
				});
			}
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00051890 File Offset: 0x0004FA90
		public float GetMainAgentMaxCameraZoom()
		{
			if (this.MainAgent != null)
			{
				return MissionGameModels.Current.AgentStatCalculateModel.GetMaxCameraZoom(this.MainAgent);
			}
			return 1f;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000518B5 File Offset: 0x0004FAB5
		public WorldPosition GetBestSlopeTowardsDirection(ref WorldPosition centerPosition, float halfSize, ref WorldPosition referencePosition)
		{
			return MBAPI.IMBMission.GetBestSlopeTowardsDirection(this.Pointer, ref centerPosition, halfSize, ref referencePosition);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000518CC File Offset: 0x0004FACC
		public WorldPosition GetBestSlopeAngleHeightPosForDefending(WorldPosition enemyPosition, WorldPosition defendingPosition, int sampleSize, float distanceRatioAllowedFromDefendedPos, float distanceSqrdAllowedFromBoundary, float cosinusOfBestSlope, float cosinusOfMaxAcceptedSlope, float minSlopeScore, float maxSlopeScore, float excessiveSlopePenalty, float nearConeCenterRatio, float nearConeCenterBonus, float heightDifferenceCeiling, float maxDisplacementPenalty)
		{
			return MBAPI.IMBMission.GetBestSlopeAngleHeightPosForDefending(this.Pointer, enemyPosition, defendingPosition, sampleSize, distanceRatioAllowedFromDefendedPos, distanceSqrdAllowedFromBoundary, cosinusOfBestSlope, cosinusOfMaxAcceptedSlope, minSlopeScore, maxSlopeScore, excessiveSlopePenalty, nearConeCenterRatio, nearConeCenterBonus, heightDifferenceCeiling, maxDisplacementPenalty);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00051904 File Offset: 0x0004FB04
		public Vec2 GetAveragePositionOfAgents(List<Agent> agents)
		{
			int num = 0;
			Vec2 vec = Vec2.Zero;
			foreach (Agent agent in agents)
			{
				num++;
				vec += agent.Position.AsVec2;
			}
			if (num == 0)
			{
				return Vec2.Invalid;
			}
			return vec * (1f / (float)num);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00051984 File Offset: 0x0004FB84
		private void GetNearbyAgentsAux(Vec2 center, float radius, MBTeam team, Mission.GetNearbyAgentsAuxType type, MBList<Agent> resultList)
		{
			EngineStackArray.StackArray40Int stackArray40Int = default(EngineStackArray.StackArray40Int);
			object getNearbyAgentsAuxLock = Mission.GetNearbyAgentsAuxLock;
			lock (getNearbyAgentsAuxLock)
			{
				int num = 0;
				for (;;)
				{
					int num2 = -1;
					MBAPI.IMBMission.GetNearbyAgentsAux(this.Pointer, center, radius, team.Index, (int)type, num, ref stackArray40Int, ref num2);
					for (int i = 0; i < num2; i++)
					{
						Agent item = DotNetObject.GetManagedObjectWithId(stackArray40Int[i]) as Agent;
						resultList.Add(item);
					}
					if (num2 < 40)
					{
						break;
					}
					num += 40;
				}
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00051A28 File Offset: 0x0004FC28
		private int GetNearbyAgentsCountAux(Vec2 center, float radius, MBTeam team, Mission.GetNearbyAgentsAuxType type)
		{
			int num = 0;
			EngineStackArray.StackArray40Int stackArray40Int = default(EngineStackArray.StackArray40Int);
			object getNearbyAgentsAuxLock = Mission.GetNearbyAgentsAuxLock;
			lock (getNearbyAgentsAuxLock)
			{
				int num2 = 0;
				for (;;)
				{
					int num3 = -1;
					MBAPI.IMBMission.GetNearbyAgentsAux(this.Pointer, center, radius, team.Index, (int)type, num2, ref stackArray40Int, ref num3);
					num += num3;
					if (num3 < 40)
					{
						break;
					}
					num2 += 40;
				}
			}
			return num;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00051AA8 File Offset: 0x0004FCA8
		public void SetRandomDecideTimeOfAgentsWithIndices(int[] agentIndices, float? minAIReactionTime = null, float? maxAIReactionTime = null)
		{
			if (minAIReactionTime == null || maxAIReactionTime == null)
			{
				maxAIReactionTime = new float?((float)-1);
				minAIReactionTime = maxAIReactionTime;
			}
			MBAPI.IMBMission.SetRandomDecideTimeOfAgents(this.Pointer, agentIndices.Length, agentIndices, minAIReactionTime.Value, maxAIReactionTime.Value);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00051AF5 File Offset: 0x0004FCF5
		public void SetBowMissileSpeedModifier(float modifier)
		{
			MBAPI.IMBMission.SetBowMissileSpeedModifier(this.Pointer, modifier);
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00051B08 File Offset: 0x0004FD08
		public void SetCrossbowMissileSpeedModifier(float modifier)
		{
			MBAPI.IMBMission.SetCrossbowMissileSpeedModifier(this.Pointer, modifier);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00051B1B File Offset: 0x0004FD1B
		public void SetThrowingMissileSpeedModifier(float modifier)
		{
			MBAPI.IMBMission.SetThrowingMissileSpeedModifier(this.Pointer, modifier);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00051B2E File Offset: 0x0004FD2E
		public void SetMissileRangeModifier(float modifier)
		{
			MBAPI.IMBMission.SetMissileRangeModifier(this.Pointer, modifier);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x00051B41 File Offset: 0x0004FD41
		public void SetLastMovementKeyPressed(Agent.MovementControlFlag lastMovementKeyPressed)
		{
			MBAPI.IMBMission.SetLastMovementKeyPressed(this.Pointer, lastMovementKeyPressed);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00051B54 File Offset: 0x0004FD54
		public Vec2 GetWeightedPointOfEnemies(Agent agent, Vec2 basePoint)
		{
			return MBAPI.IMBMission.GetWeightedPointOfEnemies(this.Pointer, agent.Index, basePoint);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00051B6D File Offset: 0x0004FD6D
		public bool GetPathBetweenPositions(ref NavigationData navData)
		{
			return MBAPI.IMBMission.GetNavigationPoints(this.Pointer, ref navData);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00051B80 File Offset: 0x0004FD80
		public void SetNavigationFaceCostWithIdAroundPosition(int navigationFaceId, Vec3 position, float cost)
		{
			MBAPI.IMBMission.SetNavigationFaceCostWithIdAroundPosition(this.Pointer, navigationFaceId, position, cost);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00051B95 File Offset: 0x0004FD95
		public WorldPosition GetStraightPathToTarget(Vec2 targetPosition, WorldPosition startingPosition, float samplingDistance = 1f, bool stopAtObstacle = true)
		{
			return MBAPI.IMBMission.GetStraightPathToTarget(this.Pointer, targetPosition, startingPosition, samplingDistance, stopAtObstacle);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00051BAC File Offset: 0x0004FDAC
		public void FastForwardMission(float startTime, float endTime)
		{
			MBAPI.IMBMission.FastForwardMission(this.Pointer, startTime, endTime);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00051BC0 File Offset: 0x0004FDC0
		public int GetDebugAgent()
		{
			return MBAPI.IMBMission.GetDebugAgent(this.Pointer);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00051BD2 File Offset: 0x0004FDD2
		public void AddAiDebugText(string str)
		{
			MBAPI.IMBMission.AddAiDebugText(this.Pointer, str);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00051BE5 File Offset: 0x0004FDE5
		public void SetDebugAgent(int index)
		{
			MBAPI.IMBMission.SetDebugAgent(this.Pointer, index);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00051BF8 File Offset: 0x0004FDF8
		public static float GetFirstPersonFov()
		{
			return BannerlordConfig.FirstPersonFov;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00051BFF File Offset: 0x0004FDFF
		public float GetWaterLevelAtPosition(Vec2 position)
		{
			return MBAPI.IMBMission.GetWaterLevelAtPosition(this.Pointer, position);
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00051C12 File Offset: 0x0004FE12
		public float GetWaterLevelAtPositionMT(Vec2 position)
		{
			return MBAPI.IMBMission.GetWaterLevelAtPosition(this.Pointer, position);
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001888 RID: 6280 RVA: 0x00051C28 File Offset: 0x0004FE28
		// (remove) Token: 0x06001889 RID: 6281 RVA: 0x00051C60 File Offset: 0x0004FE60
		public event Func<WorldPosition, Team, bool> IsFormationUnitPositionAvailable_AdditionalCondition;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600188A RID: 6282 RVA: 0x00051C98 File Offset: 0x0004FE98
		// (remove) Token: 0x0600188B RID: 6283 RVA: 0x00051CD0 File Offset: 0x0004FED0
		public event Func<Agent, bool> CanAgentRout_AdditionalCondition;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600188C RID: 6284 RVA: 0x00051D08 File Offset: 0x0004FF08
		// (remove) Token: 0x0600188D RID: 6285 RVA: 0x00051D40 File Offset: 0x0004FF40
		public event Func<Agent, WorldPosition?> GetOverriddenFleePositionForAgent;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600188E RID: 6286 RVA: 0x00051D78 File Offset: 0x0004FF78
		// (remove) Token: 0x0600188F RID: 6287 RVA: 0x00051DB0 File Offset: 0x0004FFB0
		public event Func<bool> IsAgentInteractionAllowed_AdditionalCondition;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06001890 RID: 6288 RVA: 0x00051DE8 File Offset: 0x0004FFE8
		// (remove) Token: 0x06001891 RID: 6289 RVA: 0x00051E20 File Offset: 0x00050020
		public event Action<Agent, SpawnedItemEntity> OnItemPickUp;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001892 RID: 6290 RVA: 0x00051E58 File Offset: 0x00050058
		// (remove) Token: 0x06001893 RID: 6291 RVA: 0x00051E90 File Offset: 0x00050090
		public event PropertyChangedEventHandler OnMainAgentChanged;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001894 RID: 6292 RVA: 0x00051EC8 File Offset: 0x000500C8
		// (remove) Token: 0x06001895 RID: 6293 RVA: 0x00051F00 File Offset: 0x00050100
		public event Func<BattleSideEnum, BasicCharacterObject, FormationClass> GetAgentTroopClass_Override;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001896 RID: 6294 RVA: 0x00051F38 File Offset: 0x00050138
		// (remove) Token: 0x06001897 RID: 6295 RVA: 0x00051F70 File Offset: 0x00050170
		public event Action<Agent, SpawnedItemEntity> OnItemDrop;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06001898 RID: 6296 RVA: 0x00051FA8 File Offset: 0x000501A8
		// (remove) Token: 0x06001899 RID: 6297 RVA: 0x00051FE0 File Offset: 0x000501E0
		public event Func<bool> AreOrderGesturesEnabled_AdditionalCondition;

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x00052015 File Offset: 0x00050215
		// (set) Token: 0x0600189B RID: 6299 RVA: 0x00052020 File Offset: 0x00050220
		public bool MissionEnded
		{
			get
			{
				return this._missionEnded;
			}
			private set
			{
				if (!this._missionEnded && value)
				{
					this.MissionIsEnding = true;
					foreach (MissionObject missionObject in this.MissionObjects)
					{
						missionObject.OnMissionEnded();
					}
					this.MissionIsEnding = false;
				}
				this._missionEnded = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x00052094 File Offset: 0x00050294
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x0005209C File Offset: 0x0005029C
		public bool MissionIsEnding { get; private set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x000520A5 File Offset: 0x000502A5
		public MBReadOnlyList<KeyValuePair<Agent, MissionTime>> MountsWithoutRiders
		{
			get
			{
				return this._mountsWithoutRiders;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600189F RID: 6303 RVA: 0x000520B0 File Offset: 0x000502B0
		// (remove) Token: 0x060018A0 RID: 6304 RVA: 0x000520E8 File Offset: 0x000502E8
		public event Func<bool> IsBattleInRetreatEvent;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060018A1 RID: 6305 RVA: 0x00052120 File Offset: 0x00050320
		// (remove) Token: 0x060018A2 RID: 6306 RVA: 0x00052158 File Offset: 0x00050358
		public event Mission.OnBeforeAgentRemovedDelegate OnBeforeAgentRemoved;

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0005218D File Offset: 0x0005038D
		// (set) Token: 0x060018A4 RID: 6308 RVA: 0x00052195 File Offset: 0x00050395
		public BattleSideEnum RetreatSide { get; private set; } = BattleSideEnum.None;

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0005219E File Offset: 0x0005039E
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x000521A6 File Offset: 0x000503A6
		public bool IsFastForward { get; private set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x000521AF File Offset: 0x000503AF
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x000521B7 File Offset: 0x000503B7
		public bool FixedDeltaTimeMode { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x000521C0 File Offset: 0x000503C0
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x000521C8 File Offset: 0x000503C8
		public float FixedDeltaTime { get; set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x000521D1 File Offset: 0x000503D1
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x000521D9 File Offset: 0x000503D9
		public Mission.State CurrentState { get; private set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x000521E2 File Offset: 0x000503E2
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x000521EA File Offset: 0x000503EA
		public Mission.TeamCollection Teams { get; private set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x000521F3 File Offset: 0x000503F3
		public Team AttackerTeam
		{
			get
			{
				return this.Teams.Attacker;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x00052200 File Offset: 0x00050400
		public Team DefenderTeam
		{
			get
			{
				return this.Teams.Defender;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0005220D File Offset: 0x0005040D
		public Team AttackerAllyTeam
		{
			get
			{
				return this.Teams.AttackerAlly;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0005221A File Offset: 0x0005041A
		public Team DefenderAllyTeam
		{
			get
			{
				return this.Teams.DefenderAlly;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00052227 File Offset: 0x00050427
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x00052234 File Offset: 0x00050434
		public Team PlayerTeam
		{
			get
			{
				return this.Teams.Player;
			}
			set
			{
				this.Teams.Player = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x00052242 File Offset: 0x00050442
		public Team PlayerEnemyTeam
		{
			get
			{
				return this.Teams.PlayerEnemy;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0005224F File Offset: 0x0005044F
		public Team PlayerAllyTeam
		{
			get
			{
				return this.Teams.PlayerAlly;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0005225C File Offset: 0x0005045C
		// (set) Token: 0x060018B8 RID: 6328 RVA: 0x00052264 File Offset: 0x00050464
		public Team SpectatorTeam { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0005226D File Offset: 0x0005046D
		IMissionTeam IMission.PlayerTeam
		{
			get
			{
				return this.PlayerTeam;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00052275 File Offset: 0x00050475
		public bool IsMissionEnding
		{
			get
			{
				return this.CurrentState != Mission.State.Over && this.MissionEnded;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x00052288 File Offset: 0x00050488
		public List<MissionLogic> MissionLogics { get; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00052290 File Offset: 0x00050490
		public List<MissionBehavior> MissionBehaviors { get; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x00052298 File Offset: 0x00050498
		public IEnumerable<Mission.Missile> Missiles
		{
			get
			{
				return this._missiles.Values;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x000522A5 File Offset: 0x000504A5
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x000522AD File Offset: 0x000504AD
		public IInputContext InputManager { get; set; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x000522B6 File Offset: 0x000504B6
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x000522BE File Offset: 0x000504BE
		public bool NeedsMemoryCleanup { get; internal set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x000522C7 File Offset: 0x000504C7
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x000522CF File Offset: 0x000504CF
		public Agent MainAgent
		{
			get
			{
				return this._mainAgent;
			}
			set
			{
				this._mainAgent = value;
				if (this.OnMainAgentChanged != null)
				{
					this.OnMainAgentChanged(this, null);
				}
				if (!GameNetwork.IsClient)
				{
					this.MainAgentServer = this._mainAgent;
				}
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00052302 File Offset: 0x00050502
		public IMissionDeploymentPlan DeploymentPlan
		{
			get
			{
				return this._deploymentPlan;
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0005230C File Offset: 0x0005050C
		public float GetRemovedAgentRatioForSide(BattleSideEnum side)
		{
			float result = 0f;
			if (side == BattleSideEnum.NumSides)
			{
				Debug.FailedAssert("Cannot get removed agent count for side. Invalid battle side passed!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "GetRemovedAgentRatioForSide", 637);
			}
			float num = (float)this._initialAgentCountPerSide[(int)side];
			if (num > 0f && this._agentCount > 0)
			{
				result = MathF.Min((float)this._removedAgentCountPerSide[(int)side] / num, 1f);
			}
			return result;
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0005236E File Offset: 0x0005056E
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x00052376 File Offset: 0x00050576
		public Agent MainAgentServer { get; set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0005237F File Offset: 0x0005057F
		public bool HasSpawnPath
		{
			get
			{
				return this._battleSpawnPathSelector.IsInitialized;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0005238C File Offset: 0x0005058C
		public bool IsFieldBattle
		{
			get
			{
				return this.MissionTeamAIType == Mission.MissionTeamAITypeEnum.FieldBattle;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00052397 File Offset: 0x00050597
		public bool IsSiegeBattle
		{
			get
			{
				return this.MissionTeamAIType == Mission.MissionTeamAITypeEnum.Siege;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x000523A2 File Offset: 0x000505A2
		public bool IsSallyOutBattle
		{
			get
			{
				return this.MissionTeamAIType == Mission.MissionTeamAITypeEnum.SallyOut;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x000523AD File Offset: 0x000505AD
		public MBReadOnlyList<Agent> AllAgents
		{
			get
			{
				return this._allAgents;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x000523B5 File Offset: 0x000505B5
		public MBReadOnlyList<Agent> Agents
		{
			get
			{
				return this._activeAgents;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x000523BD File Offset: 0x000505BD
		public bool IsInventoryAccessAllowed
		{
			get
			{
				return (Game.Current.GameType.IsInventoryAccessibleAtMission || this._isScreenAccessAllowed) && this.IsInventoryAccessible;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x000523E0 File Offset: 0x000505E0
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x000523E8 File Offset: 0x000505E8
		public bool IsInventoryAccessible { private get; set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x000523F1 File Offset: 0x000505F1
		// (set) Token: 0x060018D2 RID: 6354 RVA: 0x000523F9 File Offset: 0x000505F9
		public MissionResult MissionResult { get; private set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00052402 File Offset: 0x00050602
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x0005240A File Offset: 0x0005060A
		public bool IsQuestScreenAccessible { private get; set; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x00052413 File Offset: 0x00050613
		private bool _isScreenAccessAllowed
		{
			get
			{
				return this.Mode != MissionMode.Stealth && this.Mode != MissionMode.Battle && this.Mode != MissionMode.Deployment && this.Mode != MissionMode.Duel && this.Mode != MissionMode.CutScene;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00052448 File Offset: 0x00050648
		public bool IsQuestScreenAccessAllowed
		{
			get
			{
				return (Game.Current.GameType.IsQuestScreenAccessibleAtMission || this._isScreenAccessAllowed) && this.IsQuestScreenAccessible;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0005246B File Offset: 0x0005066B
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x00052473 File Offset: 0x00050673
		public bool IsCharacterWindowAccessible { private get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0005247C File Offset: 0x0005067C
		public bool IsCharacterWindowAccessAllowed
		{
			get
			{
				return (Game.Current.GameType.IsCharacterWindowAccessibleAtMission || this._isScreenAccessAllowed) && this.IsCharacterWindowAccessible;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0005249F File Offset: 0x0005069F
		// (set) Token: 0x060018DB RID: 6363 RVA: 0x000524A7 File Offset: 0x000506A7
		public bool IsPartyWindowAccessible { private get; set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x000524B0 File Offset: 0x000506B0
		public bool IsPartyWindowAccessAllowed
		{
			get
			{
				return (Game.Current.GameType.IsPartyWindowAccessibleAtMission || this._isScreenAccessAllowed) && this.IsPartyWindowAccessible;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x000524D3 File Offset: 0x000506D3
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x000524DB File Offset: 0x000506DB
		public bool IsKingdomWindowAccessible { private get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x000524E4 File Offset: 0x000506E4
		public bool IsKingdomWindowAccessAllowed
		{
			get
			{
				return (Game.Current.GameType.IsKingdomWindowAccessibleAtMission || this._isScreenAccessAllowed) && this.IsKingdomWindowAccessible;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x00052507 File Offset: 0x00050707
		// (set) Token: 0x060018E1 RID: 6369 RVA: 0x0005250F File Offset: 0x0005070F
		public bool IsClanWindowAccessible { private get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00052518 File Offset: 0x00050718
		public bool IsClanWindowAccessAllowed
		{
			get
			{
				return Game.Current.GameType.IsClanWindowAccessibleAtMission && this._isScreenAccessAllowed && this.IsClanWindowAccessible;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x0005253B File Offset: 0x0005073B
		// (set) Token: 0x060018E4 RID: 6372 RVA: 0x00052543 File Offset: 0x00050743
		public bool IsEncyclopediaWindowAccessible { private get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0005254C File Offset: 0x0005074C
		public bool IsEncyclopediaWindowAccessAllowed
		{
			get
			{
				return (Game.Current.GameType.IsEncyclopediaWindowAccessibleAtMission || this._isScreenAccessAllowed) && this.IsEncyclopediaWindowAccessible;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0005256F File Offset: 0x0005076F
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x00052577 File Offset: 0x00050777
		public bool IsBannerWindowAccessible { private get; set; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00052580 File Offset: 0x00050780
		public bool IsBannerWindowAccessAllowed
		{
			get
			{
				return (Game.Current.GameType.IsBannerWindowAccessibleAtMission || this._isScreenAccessAllowed) && this.IsBannerWindowAccessible;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x000525A3 File Offset: 0x000507A3
		// (set) Token: 0x060018EA RID: 6378 RVA: 0x000525AB File Offset: 0x000507AB
		public bool DoesMissionRequireCivilianEquipment
		{
			get
			{
				return this._doesMissionRequireCivilianEquipment;
			}
			set
			{
				this._doesMissionRequireCivilianEquipment = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x000525B4 File Offset: 0x000507B4
		// (set) Token: 0x060018EC RID: 6380 RVA: 0x000525BC File Offset: 0x000507BC
		public Mission.MissionTeamAITypeEnum MissionTeamAIType { get; set; }

		// Token: 0x060018ED RID: 6381 RVA: 0x000525C8 File Offset: 0x000507C8
		public void MakeDeploymentPlanForSide(BattleSideEnum battleSide, DeploymentPlanType planType, float spawnPathOffset = 0f)
		{
			if (!this._deploymentPlan.IsPlanMadeForBattleSide(battleSide, planType))
			{
				this._deploymentPlan.PlanBattleDeployment(battleSide, planType, spawnPathOffset);
				bool isFirstPlan;
				if (this._deploymentPlan.IsPlanMadeForBattleSide(battleSide, out isFirstPlan, planType) && planType == DeploymentPlanType.Initial)
				{
					foreach (IMissionListener missionListener in this._listeners)
					{
						missionListener.OnInitialDeploymentPlanMade(battleSide, isFirstPlan);
					}
				}
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0005264C File Offset: 0x0005084C
		public void MakeDefaultDeploymentPlans()
		{
			for (int i = 0; i < 2; i++)
			{
				BattleSideEnum battleSide = (BattleSideEnum)i;
				this.MakeDeploymentPlanForSide(battleSide, DeploymentPlanType.Initial, 0f);
				this.MakeDeploymentPlanForSide(battleSide, DeploymentPlanType.Reinforcement, 0f);
			}
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00052681 File Offset: 0x00050881
		public ref readonly List<SiegeWeapon> GetAttackerWeaponsForFriendlyFirePreventing()
		{
			return ref this._attackerWeaponsForFriendlyFirePreventing;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00052689 File Offset: 0x00050889
		public void ClearDeploymentPlanForSide(BattleSideEnum battleSide, DeploymentPlanType planType)
		{
			this._deploymentPlan.ClearDeploymentPlanForSide(battleSide, planType);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00052698 File Offset: 0x00050898
		public void ClearAddedTroopsInDeploymentPlan(BattleSideEnum battleSide, DeploymentPlanType planType)
		{
			this._deploymentPlan.ClearAddedTroopsForBattleSide(battleSide, planType);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000526A7 File Offset: 0x000508A7
		public void SetDeploymentPlanSpawnWithHorses(BattleSideEnum side, bool spawnWithHorses)
		{
			this._deploymentPlan.SetSpawnWithHorsesForSide(side, spawnWithHorses);
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000526B6 File Offset: 0x000508B6
		public void UpdateReinforcementPlan(BattleSideEnum side)
		{
			this._deploymentPlan.UpdateReinforcementPlan(side);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x000526C4 File Offset: 0x000508C4
		public void AddTroopsToDeploymentPlan(BattleSideEnum side, DeploymentPlanType planType, FormationClass fClass, int footTroopCount, int mountedTroopCount)
		{
			this._deploymentPlan.AddTroopsForBattleSide(side, planType, fClass, footTroopCount, mountedTroopCount);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000526D8 File Offset: 0x000508D8
		public bool TryRemakeInitialDeploymentPlanForBattleSide(BattleSideEnum battleSide)
		{
			if (this._deploymentPlan.IsPlanMadeForBattleSide(battleSide, DeploymentPlanType.Initial))
			{
				float spawnPathOffsetForSide = this._deploymentPlan.GetSpawnPathOffsetForSide(battleSide, DeploymentPlanType.Initial);
				ValueTuple<int, int>[] array = new ValueTuple<int, int>[11];
				foreach (Agent agent2 in from agent in this._allAgents
				where agent.IsHuman && agent.Team != null && agent.Team.Side == battleSide && agent.Formation != null
				select agent)
				{
					int formationIndex = (int)agent2.Formation.FormationIndex;
					ValueTuple<int, int> valueTuple = array[formationIndex];
					array[formationIndex] = (agent2.HasMount ? new ValueTuple<int, int>(valueTuple.Item1, valueTuple.Item2 + 1) : new ValueTuple<int, int>(valueTuple.Item1 + 1, valueTuple.Item2));
				}
				if (!this._deploymentPlan.IsInitialPlanSuitableForFormations(battleSide, array))
				{
					this._deploymentPlan.ClearAddedTroopsForBattleSide(battleSide, DeploymentPlanType.Initial);
					this._deploymentPlan.ClearDeploymentPlanForSide(battleSide, DeploymentPlanType.Initial);
					for (int i = 0; i < 11; i++)
					{
						ValueTuple<int, int> valueTuple2 = array[i];
						int item = valueTuple2.Item1;
						int item2 = valueTuple2.Item2;
						if (item + item2 > 0)
						{
							this._deploymentPlan.AddTroopsForBattleSide(battleSide, DeploymentPlanType.Initial, (FormationClass)i, item, item2);
						}
					}
					this.MakeDeploymentPlanForSide(battleSide, DeploymentPlanType.Initial, spawnPathOffsetForSide);
					return this._deploymentPlan.IsPlanMadeForBattleSide(battleSide, DeploymentPlanType.Initial);
				}
			}
			return false;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0005286C File Offset: 0x00050A6C
		public void AutoDeployTeamUsingTeamAI(Team team, bool autoAssignDetachments = true)
		{
			List<Formation> list = team.FormationsIncludingEmpty.ToList<Formation>();
			bool allowAiTicking = this.AllowAiTicking;
			bool forceTickOccasionally = this.ForceTickOccasionally;
			bool isTeleportingAgents = this.IsTeleportingAgents;
			this.AllowAiTicking = true;
			this.ForceTickOccasionally = true;
			this.IsTeleportingAgents = true;
			OrderController orderController = team.IsPlayerTeam ? team.PlayerOrderController : team.MasterOrderController;
			orderController.SelectAllFormations(false);
			this.SetDefaultFormationOrders(orderController);
			team.ResetTactic();
			team.Tick(0f);
			for (int i = 0; i < list.Count; i++)
			{
				list[i].ApplyActionOnEachUnit(delegate(Agent agent)
				{
					agent.UpdateCachedAndFormationValues(false, false);
				}, null);
			}
			orderController.ClearSelectedFormations();
			if (autoAssignDetachments)
			{
				this.AutoAssignDetachmentsForDeployment(team);
			}
			this.IsTeleportingAgents = isTeleportingAgents;
			this.ForceTickOccasionally = forceTickOccasionally;
			this.AllowAiTicking = allowAiTicking;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00052950 File Offset: 0x00050B50
		public void AutoDeployTeamUsingDeploymentPlan(Team team)
		{
			List<Formation> list = team.FormationsIncludingEmpty.ToList<Formation>();
			if (list.Count > 0)
			{
				bool isTeleportingAgents = this.IsTeleportingAgents;
				this.IsTeleportingAgents = true;
				OrderController orderController = team.IsPlayerTeam ? team.PlayerOrderController : team.MasterOrderController;
				orderController.SelectAllFormations(false);
				this.SetDefaultFormationOrders(orderController);
				orderController.ClearSelectedFormations();
				this.DeployTeamUsingDeploymentPlan(team, list);
				using (List<Formation>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						enumerator.Current.ApplyActionOnEachUnit(delegate(Agent agent)
						{
							agent.UpdateCachedAndFormationValues(true, false);
						}, null);
					}
				}
				this.IsTeleportingAgents = isTeleportingAgents;
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00052A1C File Offset: 0x00050C1C
		public void AutoAssignDetachmentsForDeployment(Team team)
		{
			List<Formation> list = team.FormationsIncludingEmpty.ToList<Formation>();
			bool allowAiTicking = this.AllowAiTicking;
			bool isTeleportingAgents = this.IsTeleportingAgents;
			this.AllowAiTicking = true;
			this.IsTeleportingAgents = true;
			if (!team.DetachmentManager.Detachments.IsEmpty<ValueTuple<IDetachment, DetachmentData>>())
			{
				for (int i = 0; i < list.Count; i++)
				{
					list[i].ApplyActionOnEachUnit(delegate(Agent agent)
					{
						Formation formation2 = agent.Formation;
						if (formation2 == null)
						{
							return;
						}
						formation2.Team.DetachmentManager.TickAgent(agent);
					}, null);
				}
				int num = 0;
				int num2 = 0;
				foreach (ValueTuple<IDetachment, DetachmentData> valueTuple in team.DetachmentManager.Detachments)
				{
					num += valueTuple.Item1.GetNumberOfUsableSlots();
				}
				foreach (Formation formation in team.FormationsIncludingEmpty)
				{
					num2 += formation.CountOfDetachableNonplayerUnits;
				}
				for (int j = 0; j < MathF.Min(num, num2); j++)
				{
					team.DetachmentManager.TickDetachments();
				}
				for (int k = 0; k < list.Count; k++)
				{
					list[k].ApplyActionOnEachUnit(delegate(Agent agent)
					{
						if (agent.Detachment != null && !(agent.Detachment is UsableMachine))
						{
							agent.UpdateCachedAndFormationValues(false, false);
						}
					}, null);
				}
			}
			this.IsTeleportingAgents = isTeleportingAgents;
			this.AllowAiTicking = allowAiTicking;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00052BC0 File Offset: 0x00050DC0
		private void DeployTeamUsingDeploymentPlan(Team team, List<Formation> formations)
		{
			if (this._deploymentPlan.IsPlanMadeForBattleSide(team.Side, DeploymentPlanType.Initial))
			{
				using (List<Formation>.Enumerator enumerator = formations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Formation formation = enumerator.Current;
						IFormationDeploymentPlan formationPlan = this._deploymentPlan.GetFormationPlan(team.Side, formation.FormationIndex, DeploymentPlanType.Initial);
						WorldPosition worldPosition;
						Vec2 vec;
						this.GetFormationSpawnFrame(formation.Team.Side, formation.FormationIndex, false, out worldPosition, out vec);
						if (formationPlan.HasDimensions)
						{
							formation.FormOrder = FormOrder.FormOrderCustom(formationPlan.PlannedWidth);
						}
						formation.SetMovementOrder(MovementOrder.MovementOrderMove(worldPosition));
						formation.FacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
						formation.SetPositioning(new WorldPosition?(worldPosition), new Vec2?(vec), new int?(formation.ArrangementOrder.GetUnitSpacing()));
						formation.ApplyActionOnEachUnit(delegate(Agent agent)
						{
							agent.UpdateCachedAndFormationValues(true, false);
						}, null);
						formation.SetMovementOrder(MovementOrder.MovementOrderStop);
					}
					return;
				}
			}
			Debug.FailedAssert("Failed to deploy team. Initial deployment plan is not made yet.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "DeployTeamUsingDeploymentPlan", 1112);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x00052CFC File Offset: 0x00050EFC
		private void SetDefaultFormationOrders(OrderController orderController)
		{
			orderController.SetOrder(OrderType.AIControlOff);
			orderController.SetFormationUpdateEnabledAfterSetOrder(false);
			orderController.SetOrder(OrderType.Mount);
			orderController.SetOrder(OrderType.FireAtWill);
			orderController.SetOrder(OrderType.ArrangementLine);
			orderController.SetOrder(OrderType.StandYourGround);
			orderController.SetOrder(this.IsSiegeBattle ? OrderType.AIControlOn : OrderType.AIControlOff);
			orderController.SetFormationUpdateEnabledAfterSetOrder(true);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00052D52 File Offset: 0x00050F52
		public WorldPosition GetAlternatePositionForNavmeshlessOrOutOfBoundsPosition(Vec2 directionTowards, WorldPosition originalPosition, ref float positionPenalty)
		{
			return MBAPI.IMBMission.GetAlternatePositionForNavmeshlessOrOutOfBoundsPosition(this.Pointer, ref directionTowards, ref originalPosition, ref positionPenalty);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00052D69 File Offset: 0x00050F69
		public int GetNextDynamicNavMeshIdStart()
		{
			int nextDynamicNavMeshIdStart = this._nextDynamicNavMeshIdStart;
			this._nextDynamicNavMeshIdStart += 10;
			return nextDynamicNavMeshIdStart;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00052D80 File Offset: 0x00050F80
		public FormationClass GetAgentTroopClass(BattleSideEnum battleSide, BasicCharacterObject agentCharacter)
		{
			if (this.GetAgentTroopClass_Override != null)
			{
				return this.GetAgentTroopClass_Override(battleSide, agentCharacter);
			}
			FormationClass formationClass = agentCharacter.GetFormationClass();
			if (this.IsSiegeBattle || (this.IsSallyOutBattle && battleSide == BattleSideEnum.Attacker))
			{
				formationClass = formationClass.DismountedClass();
			}
			return formationClass;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00052DD0 File Offset: 0x00050FD0
		[UsedImplicitly]
		[MBCallback]
		public WorldPosition GetClosestFleePositionForAgent(Agent agent)
		{
			if (this.GetOverriddenFleePositionForAgent != null)
			{
				WorldPosition? worldPosition = this.GetOverriddenFleePositionForAgent(agent);
				if (worldPosition != null)
				{
					return worldPosition.Value;
				}
			}
			WorldPosition worldPosition2 = agent.GetWorldPosition();
			float maximumForwardUnlimitedSpeed = agent.MaximumForwardUnlimitedSpeed;
			Team team = agent.Team;
			BattleSideEnum side = BattleSideEnum.None;
			bool runnerHasMount = agent.MountAgent != null;
			if (team != null)
			{
				team.UpdateCachedEnemyDataForFleeing();
				side = team.Side;
			}
			MBReadOnlyList<FleePosition> availableFleePositions = (this.MissionTeamAIType == Mission.MissionTeamAITypeEnum.SallyOut && agent.IsMount) ? this.GetFleePositionsForSide(BattleSideEnum.Attacker) : this.GetFleePositionsForSide(side);
			return this.GetClosestFleePosition(availableFleePositions, worldPosition2, maximumForwardUnlimitedSpeed, runnerHasMount, (team != null) ? team.CachedEnemyDataForFleeing : null);
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00052E74 File Offset: 0x00051074
		public WorldPosition GetClosestFleePositionForFormation(Formation formation)
		{
			WorldPosition medianPosition = formation.QuerySystem.MedianPosition;
			float movementSpeedMaximum = formation.QuerySystem.MovementSpeedMaximum;
			bool runnerHasMount = formation.QuerySystem.IsCavalryFormation || formation.QuerySystem.IsRangedCavalryFormation;
			Team team = formation.Team;
			team.UpdateCachedEnemyDataForFleeing();
			MBReadOnlyList<FleePosition> fleePositionsForSide = this.GetFleePositionsForSide(team.Side);
			return this.GetClosestFleePosition(fleePositionsForSide, medianPosition, movementSpeedMaximum, runnerHasMount, team.CachedEnemyDataForFleeing);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00052EE4 File Offset: 0x000510E4
		private WorldPosition GetClosestFleePosition(MBReadOnlyList<FleePosition> availableFleePositions, WorldPosition runnerPosition, float runnerSpeed, bool runnerHasMount, MBReadOnlyList<ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool>> chaserData)
		{
			int num = (chaserData != null) ? chaserData.Count : 0;
			if (availableFleePositions.Count > 0)
			{
				float[] array = new float[availableFleePositions.Count];
				WorldPosition[] array2 = new WorldPosition[availableFleePositions.Count];
				for (int i = 0; i < availableFleePositions.Count; i++)
				{
					array[i] = 1f;
					array2[i] = new WorldPosition(this.Scene, UIntPtr.Zero, availableFleePositions[i].GetClosestPointToEscape(runnerPosition.AsVec2), false);
					array2[i].SetVec2(array2[i].AsVec2 - runnerPosition.AsVec2);
				}
				for (int j = 0; j < num; j++)
				{
					float item = chaserData[j].Item1;
					if (item > 0f)
					{
						ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool> valueTuple = chaserData[j];
						Vec2 asVec = valueTuple.Item2.AsVec2;
						int item2 = chaserData[j].Item3;
						Vec2 vec;
						if (item2 > 1)
						{
							Vec2 item3 = chaserData[j].Item4;
							Vec2 item4 = chaserData[j].Item5;
							vec = MBMath.GetClosestPointInLineSegmentToPoint(runnerPosition.AsVec2, item3, item4) - runnerPosition.AsVec2;
						}
						else
						{
							vec = asVec - runnerPosition.AsVec2;
						}
						for (int k = 0; k < availableFleePositions.Count; k++)
						{
							float num2 = vec.DotProduct(array2[k].AsVec2.Normalized());
							if (num2 > 0f)
							{
								float num3 = MathF.Max(MathF.Abs(vec.DotProduct(array2[k].AsVec2.LeftVec().Normalized())) / item, 1f);
								float num4 = MathF.Max(num2 / runnerSpeed, 1f);
								if (num4 > num3)
								{
									float num5 = num4 / num3;
									num5 /= num2;
									array[k] += num5 * (float)item2;
								}
							}
						}
					}
				}
				for (int l = 0; l < availableFleePositions.Count; l++)
				{
					WorldPosition worldPosition = new WorldPosition(this.Scene, UIntPtr.Zero, availableFleePositions[l].GetClosestPointToEscape(runnerPosition.AsVec2), false);
					float num6;
					if (this.Scene.GetPathDistanceBetweenPositions(ref runnerPosition, ref worldPosition, 0f, out num6))
					{
						array[l] *= num6;
					}
					else
					{
						array[l] = float.MaxValue;
					}
				}
				int num7 = -1;
				float num8 = float.MaxValue;
				for (int m = 0; m < availableFleePositions.Count; m++)
				{
					if (num8 > array[m])
					{
						num7 = m;
						num8 = array[m];
					}
				}
				if (num7 >= 0)
				{
					Vec3 closestPointToEscape = availableFleePositions[num7].GetClosestPointToEscape(runnerPosition.AsVec2);
					return new WorldPosition(this.Scene, UIntPtr.Zero, closestPointToEscape, false);
				}
			}
			float[] array3 = new float[4];
			for (int n = 0; n < num; n++)
			{
				ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool> valueTuple = chaserData[n];
				Vec2 asVec2 = valueTuple.Item2.AsVec2;
				int item5 = chaserData[n].Item3;
				Vec2 vec2;
				if (item5 > 1)
				{
					Vec2 item6 = chaserData[n].Item4;
					Vec2 item7 = chaserData[n].Item5;
					vec2 = MBMath.GetClosestPointInLineSegmentToPoint(runnerPosition.AsVec2, item6, item7) - runnerPosition.AsVec2;
				}
				else
				{
					vec2 = asVec2 - runnerPosition.AsVec2;
				}
				float num9 = vec2.Length;
				if (chaserData[n].Item6)
				{
					num9 *= 0.5f;
				}
				if (runnerHasMount)
				{
					num9 *= 2f;
				}
				float num10 = MBMath.ClampFloat(1f - (num9 - 40f) / 40f, 0.01f, 1f);
				Vec2 vec3 = vec2.Normalized();
				float num11 = 1.2f;
				float num12 = num10 * (float)item5 * num11;
				float num13 = num12 * MathF.Abs(vec3.x);
				float num14 = num12 * MathF.Abs(vec3.y);
				array3[(vec3.y < 0f) ? 0 : 1] -= num14;
				array3[(vec3.x < 0f) ? 2 : 3] -= num13;
				array3[(vec3.y < 0f) ? 1 : 0] += num14;
				array3[(vec3.x < 0f) ? 3 : 2] += num13;
			}
			float num15 = 0.04f;
			Vec3 vec4;
			Vec3 vec5;
			this.Scene.GetBoundingBox(out vec4, out vec5);
			Vec2 closestBoundaryPosition = this.GetClosestBoundaryPosition(new Vec2(runnerPosition.X, vec4.y));
			Vec2 closestBoundaryPosition2 = this.GetClosestBoundaryPosition(new Vec2(runnerPosition.X, vec5.y));
			Vec2 closestBoundaryPosition3 = this.GetClosestBoundaryPosition(new Vec2(vec4.x, runnerPosition.Y));
			Vec2 closestBoundaryPosition4 = this.GetClosestBoundaryPosition(new Vec2(vec5.x, runnerPosition.Y));
			float num16 = closestBoundaryPosition2.y - closestBoundaryPosition.y;
			float num17 = closestBoundaryPosition4.x - closestBoundaryPosition3.x;
			array3[0] += (num16 - (runnerPosition.Y - closestBoundaryPosition.y)) * num15;
			array3[1] += (num16 - (closestBoundaryPosition2.y - runnerPosition.Y)) * num15;
			array3[2] += (num17 - (runnerPosition.X - closestBoundaryPosition3.x)) * num15;
			array3[3] += (num17 - (closestBoundaryPosition4.x - runnerPosition.X)) * num15;
			Vec2 xy;
			if (array3[0] >= array3[1] && array3[0] >= array3[2] && array3[0] >= array3[3])
			{
				xy = new Vec2(closestBoundaryPosition.x, closestBoundaryPosition.y);
			}
			else if (array3[1] >= array3[2] && array3[1] >= array3[3])
			{
				xy = new Vec2(closestBoundaryPosition2.x, closestBoundaryPosition2.y);
			}
			else if (array3[2] >= array3[3])
			{
				xy = new Vec2(closestBoundaryPosition3.x, closestBoundaryPosition3.y);
			}
			else
			{
				xy = new Vec2(closestBoundaryPosition4.x, closestBoundaryPosition4.y);
			}
			return new WorldPosition(this.Scene, UIntPtr.Zero, new Vec3(xy, runnerPosition.GetNavMeshZ(), -1f), false);
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0005353A File Offset: 0x0005173A
		// (set) Token: 0x06001902 RID: 6402 RVA: 0x00053542 File Offset: 0x00051742
		public MissionTimeTracker MissionTimeTracker { get; private set; }

		// Token: 0x06001903 RID: 6403 RVA: 0x0005354C File Offset: 0x0005174C
		public MBReadOnlyList<FleePosition> GetFleePositionsForSide(BattleSideEnum side)
		{
			if (side == BattleSideEnum.NumSides)
			{
				Debug.FailedAssert("Flee position with invalid battle side field found!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "GetFleePositionsForSide", 1700);
				return null;
			}
			int num = (int)((side == BattleSideEnum.None) ? BattleSideEnum.Defender : (side + 1));
			return this._fleePositions[num];
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0005358B File Offset: 0x0005178B
		public void AddToWeaponListForFriendlyFirePreventing(SiegeWeapon weapon)
		{
			this._attackerWeaponsForFriendlyFirePreventing.Add(weapon);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0005359C File Offset: 0x0005179C
		public Mission(MissionInitializerRecord rec, MissionState missionState)
		{
			this.Pointer = MBAPI.IMBMission.CreateMission(this);
			this._spawnedItemEntitiesCreatedAtRuntime = new List<SpawnedItemEntity>();
			this._missionObjects = new MBList<MissionObject>();
			this._activeMissionObjects = new MBList<MissionObject>();
			this._mountsWithoutRiders = new MBList<KeyValuePair<Agent, MissionTime>>();
			this._addedEntitiesInfo = new MBList<Mission.DynamicallyCreatedEntity>();
			this._emptyRuntimeMissionObjectIds = new Stack<ValueTuple<int, float>>();
			this.Boundaries = new Mission.MBBoundaryCollection(this);
			this.InitializerRecord = rec;
			this.CurrentState = Mission.State.NewlyCreated;
			this.IsInventoryAccessible = false;
			this.IsQuestScreenAccessible = true;
			this.IsCharacterWindowAccessible = true;
			this.IsPartyWindowAccessible = true;
			this.IsKingdomWindowAccessible = true;
			this.IsClanWindowAccessible = true;
			this.IsBannerWindowAccessible = false;
			this.IsEncyclopediaWindowAccessible = true;
			this._missiles = new Dictionary<int, Mission.Missile>();
			this._activeAgents = new MBList<Agent>(256);
			this._allAgents = new MBList<Agent>(256);
			for (int i = 0; i < 3; i++)
			{
				this._fleePositions[i] = new MBList<FleePosition>(32);
			}
			for (int j = 0; j < 2; j++)
			{
				this._initialAgentCountPerSide[j] = 0;
				this._removedAgentCountPerSide[j] = 0;
			}
			this.MissionBehaviors = new List<MissionBehavior>();
			this.MissionLogics = new List<MissionLogic>();
			this._otherMissionBehaviors = new List<MissionBehavior>();
			this._missionState = missionState;
			this._battleSpawnPathSelector = new BattleSpawnPathSelector(this);
			this.Teams = new Mission.TeamCollection(this);
			this._deploymentPlan = new MissionDeploymentPlan(this);
			this.MissionTimeTracker = new MissionTimeTracker();
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x000537C0 File Offset: 0x000519C0
		private Lazy<MissionRecorder> _recorder
		{
			get
			{
				return new Lazy<MissionRecorder>(() => new MissionRecorder(this));
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x000537D3 File Offset: 0x000519D3
		public MissionRecorder Recorder
		{
			get
			{
				return this._recorder.Value;
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000537E0 File Offset: 0x000519E0
		public void AddFleePosition(FleePosition fleePosition)
		{
			BattleSideEnum side = fleePosition.GetSide();
			if (side == BattleSideEnum.NumSides)
			{
				Debug.FailedAssert("Flee position with invalid battle side field found!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "AddFleePosition", 1781);
				return;
			}
			if (side == BattleSideEnum.None)
			{
				for (int i = 0; i < this._fleePositions.Length; i++)
				{
					this._fleePositions[i].Add(fleePosition);
				}
				return;
			}
			int num = (int)(side + 1);
			this._fleePositions[num].Add(fleePosition);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0005384C File Offset: 0x00051A4C
		private void FreeResources()
		{
			this.MainAgent = null;
			this.Teams.ClearResources();
			this.SpectatorTeam = null;
			this._activeAgents = null;
			this._allAgents = null;
			if (GameNetwork.NetworkPeersValid)
			{
				foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkPeer.GetComponent<MissionPeer>();
					if (component != null)
					{
						component.ClearAllVisuals(true);
						networkPeer.RemoveComponent(component);
					}
					MissionRepresentativeBase component2 = networkPeer.GetComponent<MissionRepresentativeBase>();
					if (component2 != null)
					{
						networkPeer.RemoveComponent(component2);
					}
				}
			}
			if (GameNetwork.DisconnectedNetworkPeers != null)
			{
				Debug.Print("DisconnectedNetworkPeers.Clear()", 0, Debug.DebugColor.White, 17179869184UL);
				GameNetwork.DisconnectedNetworkPeers.Clear();
			}
			this._missionState = null;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0005391C File Offset: 0x00051B1C
		public void RetreatMission()
		{
			foreach (MissionLogic missionLogic in this.MissionLogics)
			{
				missionLogic.OnRetreatMission();
			}
			if (MBEditor.EditModeEnabled && MBEditor.IsEditModeOn)
			{
				MBEditor.LeaveEditMissionMode();
				return;
			}
			this.EndMission();
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00053988 File Offset: 0x00051B88
		public void SurrenderMission()
		{
			foreach (MissionLogic missionLogic in this.MissionLogics)
			{
				missionLogic.OnSurrenderMission();
			}
			if (MBEditor.EditModeEnabled && MBEditor.IsEditModeOn)
			{
				MBEditor.LeaveEditMissionMode();
				return;
			}
			this.EndMission();
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000539F4 File Offset: 0x00051BF4
		public bool HasMissionBehavior<T>() where T : MissionBehavior
		{
			return this.GetMissionBehavior<T>() != null;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00053A04 File Offset: 0x00051C04
		[UsedImplicitly]
		[MBCallback]
		internal void OnAgentAddedAsCorpse(Agent affectedAgent)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				for (int i = 0; i < affectedAgent.GetAttachedWeaponsCount(); i++)
				{
					if (affectedAgent.GetAttachedWeapon(i).Item.ItemFlags.HasAnyFlag(ItemFlags.CanBePickedUpFromCorpse))
					{
						this.SpawnAttachedWeaponOnCorpse(affectedAgent, i, -1);
					}
				}
				affectedAgent.ClearAttachedWeapons();
			}
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00053A58 File Offset: 0x00051C58
		public void SpawnAttachedWeaponOnCorpse(Agent agent, int attachedWeaponIndex, int forcedSpawnIndex)
		{
			agent.AgentVisuals.GetSkeleton().ForceUpdateBoneFrames();
			MissionWeapon attachedWeapon = agent.GetAttachedWeapon(attachedWeaponIndex);
			GameEntity attachedWeaponEntity = agent.AgentVisuals.GetAttachedWeaponEntity(attachedWeaponIndex);
			attachedWeaponEntity.CreateAndAddScriptComponent(typeof(SpawnedItemEntity).Name);
			SpawnedItemEntity firstScriptOfType = attachedWeaponEntity.GetFirstScriptOfType<SpawnedItemEntity>();
			if (forcedSpawnIndex >= 0)
			{
				firstScriptOfType.Id = new MissionObjectId(forcedSpawnIndex, true);
			}
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SpawnAttachedWeaponOnCorpse(agent.Index, attachedWeaponIndex, firstScriptOfType.Id.Id));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			this.SpawnWeaponAux(attachedWeaponEntity, attachedWeapon, Mission.WeaponSpawnFlags.AsMissile | Mission.WeaponSpawnFlags.WithStaticPhysics, Vec3.Zero, Vec3.Zero, false);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00053AFC File Offset: 0x00051CFC
		public void AddMountWithoutRider(Agent mount)
		{
			this._mountsWithoutRiders.Add(new KeyValuePair<Agent, MissionTime>(mount, MissionTime.Now));
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00053B14 File Offset: 0x00051D14
		public void RemoveMountWithoutRider(Agent mount)
		{
			for (int i = 0; i < this._mountsWithoutRiders.Count; i++)
			{
				if (this._mountsWithoutRiders[i].Key == mount)
				{
					this._mountsWithoutRiders.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00053B5C File Offset: 0x00051D5C
		[UsedImplicitly]
		[MBCallback]
		internal void OnAgentDeleted(Agent affectedAgent)
		{
			if (affectedAgent != null)
			{
				affectedAgent.State = AgentState.Deleted;
				foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
				{
					missionBehavior.OnAgentDeleted(affectedAgent);
				}
				this._allAgents.Remove(affectedAgent);
				affectedAgent.OnDelete();
				affectedAgent.SetTeam(null, false);
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00053BD4 File Offset: 0x00051DD4
		[UsedImplicitly]
		[MBCallback]
		internal void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			Mission.OnBeforeAgentRemovedDelegate onBeforeAgentRemoved = this.OnBeforeAgentRemoved;
			if (onBeforeAgentRemoved != null)
			{
				onBeforeAgentRemoved(affectedAgent, affectorAgent, agentState, killingBlow);
			}
			affectedAgent.State = agentState;
			if (affectorAgent != null && affectorAgent.Team != affectedAgent.Team)
			{
				affectorAgent.KillCount++;
			}
			Team team = affectedAgent.Team;
			if (team != null)
			{
				team.DeactivateAgent(affectedAgent);
			}
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnEarlyAgentRemoved(affectedAgent, affectorAgent, agentState, killingBlow);
			}
			foreach (MissionBehavior missionBehavior2 in this.MissionBehaviors)
			{
				missionBehavior2.OnAgentRemoved(affectedAgent, affectorAgent, agentState, killingBlow);
			}
			bool flag = this.MainAgent == affectedAgent;
			if (flag)
			{
				affectedAgent.OnMainAgentWieldedItemChange = null;
				this.MainAgent = null;
			}
			affectedAgent.OnAgentWieldedItemChange = null;
			affectedAgent.OnAgentMountedStateChanged = null;
			if (affectedAgent.Team != null && affectedAgent.Team.Side != BattleSideEnum.None)
			{
				this._removedAgentCountPerSide[(int)affectedAgent.Team.Side]++;
			}
			this._activeAgents.Remove(affectedAgent);
			affectedAgent.OnRemove();
			if (affectedAgent.IsMount && affectedAgent.RiderAgent == null)
			{
				this.RemoveMountWithoutRider(affectedAgent);
			}
			if (flag)
			{
				affectedAgent.Team.DelegateCommandToAI();
			}
			if (!GameNetwork.IsClientOrReplay && agentState != AgentState.Routed && affectedAgent.GetAgentFlags().HasAnyFlag(AgentFlag.CanWieldWeapon))
			{
				EquipmentIndex wieldedItemIndex = affectedAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
				if (wieldedItemIndex == EquipmentIndex.ExtraWeaponSlot)
				{
					WeaponComponentData currentUsageItem = affectedAgent.Equipment[wieldedItemIndex].CurrentUsageItem;
					if (currentUsageItem != null && currentUsageItem.WeaponClass == WeaponClass.Banner)
					{
						affectedAgent.DropItem(EquipmentIndex.ExtraWeaponSlot, WeaponClass.Undefined);
					}
				}
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00053DA0 File Offset: 0x00051FA0
		public void OnObjectDisabled(DestructableComponent destructionComponent)
		{
			UsableMachine firstScriptOfType = destructionComponent.GameEntity.GetFirstScriptOfType<UsableMachine>();
			if (firstScriptOfType != null)
			{
				firstScriptOfType.Disable();
			}
			if (destructionComponent != null)
			{
				destructionComponent.SetAbilityOfFaces(false);
			}
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnObjectDisabled(destructionComponent);
			}
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00053E14 File Offset: 0x00052014
		public MissionObjectId SpawnWeaponAsDropFromMissile(int missileIndex, MissionObject attachedMissionObject, in MatrixFrame attachLocalFrame, Mission.WeaponSpawnFlags spawnFlags, in Vec3 velocity, in Vec3 angularVelocity, int forcedSpawnIndex)
		{
			this.PrepareMissileWeaponForDrop(missileIndex);
			Mission.Missile missile = this._missiles[missileIndex];
			if (attachedMissionObject != null)
			{
				attachedMissionObject.AddStuckMissile(missile.Entity);
			}
			if (attachedMissionObject != null)
			{
				GameEntity entity = missile.Entity;
				MatrixFrame matrixFrame = attachedMissionObject.GameEntity.GetGlobalFrame();
				matrixFrame = matrixFrame.TransformToParent(attachLocalFrame);
				entity.SetGlobalFrame(matrixFrame);
			}
			else
			{
				missile.Entity.SetGlobalFrame(attachLocalFrame);
			}
			missile.Entity.CreateAndAddScriptComponent(typeof(SpawnedItemEntity).Name);
			SpawnedItemEntity firstScriptOfType = missile.Entity.GetFirstScriptOfType<SpawnedItemEntity>();
			if (forcedSpawnIndex >= 0)
			{
				firstScriptOfType.Id = new MissionObjectId(forcedSpawnIndex, true);
			}
			this.SpawnWeaponAux(missile.Entity, missile.Weapon, spawnFlags, velocity, angularVelocity, true);
			return firstScriptOfType.Id;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00053EE0 File Offset: 0x000520E0
		[UsedImplicitly]
		[MBCallback]
		internal void SpawnWeaponAsDropFromAgent(Agent agent, EquipmentIndex equipmentIndex, ref Vec3 velocity, ref Vec3 angularVelocity, Mission.WeaponSpawnFlags spawnFlags)
		{
			Vec3 velocity2 = agent.Velocity;
			if ((velocity - velocity2).LengthSquared > 100f)
			{
				Vec3 v = (velocity - velocity2).NormalizedCopy() * 10f;
				velocity = velocity2 + v;
			}
			this.SpawnWeaponAsDropFromAgentAux(agent, equipmentIndex, ref velocity, ref angularVelocity, spawnFlags, -1);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00053F4C File Offset: 0x0005214C
		public void SpawnWeaponAsDropFromAgentAux(Agent agent, EquipmentIndex equipmentIndex, ref Vec3 velocity, ref Vec3 angularVelocity, Mission.WeaponSpawnFlags spawnFlags, int forcedSpawnIndex)
		{
			agent.AgentVisuals.GetSkeleton().ForceUpdateBoneFrames();
			agent.PrepareWeaponForDropInEquipmentSlot(equipmentIndex, (spawnFlags & Mission.WeaponSpawnFlags.WithHolster) > Mission.WeaponSpawnFlags.None);
			GameEntity weaponEntityFromEquipmentSlot = agent.GetWeaponEntityFromEquipmentSlot(equipmentIndex);
			weaponEntityFromEquipmentSlot.CreateAndAddScriptComponent(typeof(SpawnedItemEntity).Name);
			SpawnedItemEntity firstScriptOfType = weaponEntityFromEquipmentSlot.GetFirstScriptOfType<SpawnedItemEntity>();
			if (forcedSpawnIndex >= 0)
			{
				firstScriptOfType.Id = new MissionObjectId(forcedSpawnIndex, true);
			}
			float maximumValue = CompressionMission.SpawnedItemVelocityCompressionInfo.GetMaximumValue();
			float maximumValue2 = CompressionMission.SpawnedItemAngularVelocityCompressionInfo.GetMaximumValue();
			if (velocity.LengthSquared > maximumValue * maximumValue)
			{
				velocity = velocity.NormalizedCopy() * maximumValue;
			}
			if (angularVelocity.LengthSquared > maximumValue2 * maximumValue2)
			{
				angularVelocity = angularVelocity.NormalizedCopy() * maximumValue2;
			}
			MissionWeapon weapon = agent.Equipment[equipmentIndex];
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SpawnWeaponAsDropFromAgent(agent.Index, equipmentIndex, velocity, angularVelocity, spawnFlags, firstScriptOfType.Id.Id));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			this.SpawnWeaponAux(weaponEntityFromEquipmentSlot, weapon, spawnFlags, velocity, angularVelocity, true);
			if (!GameNetwork.IsClientOrReplay)
			{
				for (int i = 0; i < weapon.GetAttachedWeaponsCount(); i++)
				{
					if (weapon.GetAttachedWeapon(i).Item.ItemFlags.HasAnyFlag(ItemFlags.CanBePickedUpFromCorpse))
					{
						this.SpawnAttachedWeaponOnSpawnedWeapon(firstScriptOfType, i, -1);
					}
				}
			}
			agent.OnWeaponDrop(equipmentIndex);
			Action<Agent, SpawnedItemEntity> onItemDrop = this.OnItemDrop;
			if (onItemDrop == null)
			{
				return;
			}
			onItemDrop(agent, firstScriptOfType);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000540CC File Offset: 0x000522CC
		public void SpawnAttachedWeaponOnSpawnedWeapon(SpawnedItemEntity spawnedWeapon, int attachmentIndex, int forcedSpawnIndex)
		{
			GameEntity child = spawnedWeapon.GameEntity.GetChild(attachmentIndex);
			child.CreateAndAddScriptComponent(typeof(SpawnedItemEntity).Name);
			SpawnedItemEntity firstScriptOfType = child.GetFirstScriptOfType<SpawnedItemEntity>();
			if (forcedSpawnIndex >= 0)
			{
				firstScriptOfType.Id = new MissionObjectId(forcedSpawnIndex, true);
			}
			this.SpawnWeaponAux(child, spawnedWeapon.WeaponCopy.GetAttachedWeapon(attachmentIndex), Mission.WeaponSpawnFlags.AsMissile | Mission.WeaponSpawnFlags.WithStaticPhysics, Vec3.Zero, Vec3.Zero, false);
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SpawnAttachedWeaponOnSpawnedWeapon(spawnedWeapon.Id, attachmentIndex, firstScriptOfType.Id.Id));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00054166 File Offset: 0x00052366
		public GameEntity SpawnWeaponWithNewEntity(ref MissionWeapon weapon, Mission.WeaponSpawnFlags spawnFlags, MatrixFrame frame)
		{
			return this.SpawnWeaponWithNewEntityAux(weapon, spawnFlags, frame, -1, null, false);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0005417C File Offset: 0x0005237C
		public GameEntity SpawnWeaponWithNewEntityAux(MissionWeapon weapon, Mission.WeaponSpawnFlags spawnFlags, MatrixFrame frame, int forcedSpawnIndex, MissionObject attachedMissionObject, bool hasLifeTime)
		{
			GameEntity gameEntity = GameEntityExtensions.Instantiate(this.Scene, weapon, spawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithHolster), true);
			gameEntity.CreateAndAddScriptComponent(typeof(SpawnedItemEntity).Name);
			SpawnedItemEntity firstScriptOfType = gameEntity.GetFirstScriptOfType<SpawnedItemEntity>();
			if (forcedSpawnIndex >= 0)
			{
				firstScriptOfType.Id = new MissionObjectId(forcedSpawnIndex, true);
			}
			if (attachedMissionObject != null)
			{
				attachedMissionObject.GameEntity.AddChild(gameEntity, false);
			}
			if (attachedMissionObject != null)
			{
				GameEntity gameEntity2 = gameEntity;
				MatrixFrame matrixFrame = attachedMissionObject.GameEntity.GetGlobalFrame();
				matrixFrame = matrixFrame.TransformToParent(frame);
				gameEntity2.SetGlobalFrame(matrixFrame);
			}
			else
			{
				gameEntity.SetGlobalFrame(frame);
			}
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SpawnWeaponWithNewEntity(weapon, spawnFlags, firstScriptOfType.Id.Id, frame, attachedMissionObject.Id, true, hasLifeTime));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				for (int i = 0; i < weapon.GetAttachedWeaponsCount(); i++)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new AttachWeaponToSpawnedWeapon(weapon.GetAttachedWeapon(i), firstScriptOfType.Id, weapon.GetAttachedWeaponFrame(i)));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
			Vec3 zero = Vec3.Zero;
			this.SpawnWeaponAux(gameEntity, weapon, spawnFlags, zero, zero, hasLifeTime);
			return gameEntity;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0005429C File Offset: 0x0005249C
		public void AttachWeaponWithNewEntityToSpawnedWeapon(MissionWeapon weapon, SpawnedItemEntity spawnedItem, MatrixFrame attachLocalFrame)
		{
			GameEntity gameEntity = GameEntityExtensions.Instantiate(this.Scene, weapon, false, true);
			spawnedItem.GameEntity.AddChild(gameEntity, false);
			gameEntity.SetFrame(ref attachLocalFrame);
			spawnedItem.AttachWeaponToWeapon(weapon, ref attachLocalFrame);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000542D8 File Offset: 0x000524D8
		private void SpawnWeaponAux(GameEntity weaponEntity, MissionWeapon weapon, Mission.WeaponSpawnFlags spawnFlags, Vec3 velocity, Vec3 angularVelocity, bool hasLifeTime)
		{
			SpawnedItemEntity firstScriptOfType = weaponEntity.GetFirstScriptOfType<SpawnedItemEntity>();
			bool flag = weapon.IsBanner();
			MissionWeapon weapon2 = weapon;
			bool hasLifeTime2 = !flag && hasLifeTime;
			Mission.WeaponSpawnFlags spawnFlags2 = spawnFlags;
			Vec3 vec = flag ? velocity : Vec3.Zero;
			firstScriptOfType.Initialize(weapon2, hasLifeTime2, spawnFlags2, vec);
			if (spawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithPhysics | Mission.WeaponSpawnFlags.WithStaticPhysics))
			{
				BodyFlags bodyFlags = BodyFlags.OnlyCollideWithRaycast | BodyFlags.DroppedItem;
				if (weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.CannotBePickedUp) || spawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.CannotBePickedUp))
				{
					bodyFlags |= BodyFlags.DoNotCollideWithRaycast;
				}
				weaponEntity.BodyFlag |= bodyFlags;
				WeaponData weaponData = weapon.GetWeaponData(true);
				this.RecalculateBody(ref weaponData, weapon.Item.ItemComponent, weapon.Item.WeaponDesign, ref spawnFlags);
				if (flag)
				{
					weaponEntity.AddPhysics(weaponData.BaseWeight, weaponData.CenterOfMassShift, weaponData.Shape, velocity, angularVelocity, PhysicsMaterial.GetFromIndex(weaponData.PhysicsMaterialIndex), true, 0);
					weaponData.DeinitializeManagedPointers();
				}
				else if (spawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithPhysics | Mission.WeaponSpawnFlags.WithStaticPhysics))
				{
					weaponEntity.AddPhysics(weaponData.BaseWeight, weaponData.CenterOfMassShift, weaponData.Shape, velocity, angularVelocity, PhysicsMaterial.GetFromIndex(weaponData.PhysicsMaterialIndex), spawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithStaticPhysics), 0);
				}
				weaponData.DeinitializeManagedPointers();
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00054400 File Offset: 0x00052600
		public void OnEquipItemsFromSpawnEquipmentBegin(Agent agent, Agent.CreationType creationType)
		{
			foreach (IMissionListener missionListener in this._listeners)
			{
				missionListener.OnEquipItemsFromSpawnEquipmentBegin(agent, creationType);
			}
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00054454 File Offset: 0x00052654
		public void OnEquipItemsFromSpawnEquipment(Agent agent, Agent.CreationType creationType)
		{
			foreach (IMissionListener missionListener in this._listeners)
			{
				missionListener.OnEquipItemsFromSpawnEquipment(agent, creationType);
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000544A8 File Offset: 0x000526A8
		[CommandLineFunctionality.CommandLineArgumentFunction("flee_enemies", "mission")]
		public static string MakeEnemiesFleeCheat(List<string> strings)
		{
			if (GameNetwork.IsClientOrReplay)
			{
				return "does not work in multiplayer";
			}
			if (Mission.Current != null && Mission.Current.Agents != null)
			{
				foreach (Agent agent2 in from agent in Mission.Current.Agents
				where agent.IsHuman && agent.IsEnemyOf(Agent.Main)
				select agent)
				{
					CommonAIComponent commonAIComponent = agent2.CommonAIComponent;
					if (commonAIComponent != null)
					{
						commonAIComponent.Panic();
					}
				}
				return "enemies are fleeing";
			}
			return "mission is not available";
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00054550 File Offset: 0x00052750
		[CommandLineFunctionality.CommandLineArgumentFunction("flee_team", "mission")]
		public static string MakeTeamFleeCheat(List<string> strings)
		{
			if (GameNetwork.IsClientOrReplay)
			{
				return "does not work in multiplayer";
			}
			if (Mission.Current == null || Mission.Current.Agents == null)
			{
				return "mission is not available";
			}
			string str = "Usage 1: flee_team [ Attacker | AttackerAlly | Defender | DefenderAlly ]\nUsage 2: flee_team [ Attacker | AttackerAlly | Defender | DefenderAlly ] [FormationNo]";
			if (strings.IsEmpty<string>() || strings[0] == "help")
			{
				return "makes an entire team or a team's formation flee battle.\n" + str;
			}
			if (strings.Count >= 3)
			{
				return "invalid number of parameters.\n" + str;
			}
			string text = strings[0];
			Team targetTeam = null;
			string a = text.ToLower();
			if (!(a == "attacker"))
			{
				if (!(a == "attackerally"))
				{
					if (!(a == "defender"))
					{
						if (a == "defenderally")
						{
							targetTeam = Mission.Current.DefenderAllyTeam;
						}
					}
					else
					{
						targetTeam = Mission.Current.DefenderTeam;
					}
				}
				else
				{
					targetTeam = Mission.Current.AttackerAllyTeam;
				}
			}
			else
			{
				targetTeam = Mission.Current.AttackerTeam;
			}
			if (targetTeam == null)
			{
				return "given team is not valid";
			}
			Formation targetFormation = null;
			if (strings.Count == 2)
			{
				int num = 8;
				int num2 = int.Parse(strings[1]);
				if (num2 < 0 || num2 >= num)
				{
					return "invalid formation index. formation index should be between [0, " + (num - 1) + "]";
				}
				FormationClass formationClass = (FormationClass)num2;
				targetFormation = targetTeam.GetFormation(formationClass);
			}
			if (targetFormation == null)
			{
				IEnumerable<Agent> agents = Mission.Current.Agents;
				Func<Agent, bool> <>9__0;
				Func<Agent, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((Agent agent) => agent.IsHuman && agent.Team == targetTeam));
				}
				foreach (Agent agent3 in agents.Where(predicate))
				{
					CommonAIComponent commonAIComponent = agent3.CommonAIComponent;
					if (commonAIComponent != null)
					{
						commonAIComponent.Panic();
					}
				}
				return "agents in team: " + text + " are fleeing";
			}
			IEnumerable<Agent> agents2 = Mission.Current.Agents;
			Func<Agent, bool> <>9__1;
			Func<Agent, bool> predicate2;
			if ((predicate2 = <>9__1) == null)
			{
				predicate2 = (<>9__1 = ((Agent agent) => agent.IsHuman && agent.Formation == targetFormation));
			}
			foreach (Agent agent2 in agents2.Where(predicate2))
			{
				CommonAIComponent commonAIComponent2 = agent2.CommonAIComponent;
				if (commonAIComponent2 != null)
				{
					commonAIComponent2.Panic();
				}
			}
			return string.Concat(new object[]
			{
				"agents in team: ",
				text,
				" and formation: ",
				(int)targetFormation.FormationIndex,
				" (",
				targetFormation.FormationIndex.ToString(),
				") are fleeing"
			});
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00054830 File Offset: 0x00052A30
		public void RecalculateBody(ref WeaponData weaponData, ItemComponent itemComponent, WeaponDesign craftedWeaponData, ref Mission.WeaponSpawnFlags spawnFlags)
		{
			WeaponComponent weaponComponent = (WeaponComponent)itemComponent;
			ItemObject item = weaponComponent.Item;
			if (spawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithHolster))
			{
				weaponData.Shape = (string.IsNullOrEmpty(item.HolsterBodyName) ? null : PhysicsShape.GetFromResource(item.HolsterBodyName, false));
			}
			else
			{
				weaponData.Shape = (string.IsNullOrEmpty(item.BodyName) ? null : PhysicsShape.GetFromResource(item.BodyName, false));
			}
			PhysicsShape physicsShape = weaponData.Shape;
			if (physicsShape == null)
			{
				Debug.FailedAssert("Item has no body! Applying a default body, but this should not happen! Check this!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "RecalculateBody", 2614);
				physicsShape = PhysicsShape.GetFromResource("bo_axe_short", false);
			}
			if (!weaponComponent.Item.ItemFlags.HasAnyFlag(ItemFlags.DoNotScaleBodyAccordingToWeaponLength))
			{
				if (spawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithHolster) || !item.RecalculateBody)
				{
					weaponData.Shape = physicsShape;
				}
				else
				{
					PhysicsShape physicsShape2 = physicsShape.CreateCopy();
					weaponData.Shape = physicsShape2;
					float num = (float)weaponComponent.PrimaryWeapon.WeaponLength * 0.01f;
					if (craftedWeaponData != null)
					{
						physicsShape2.Clear();
						physicsShape2.InitDescription();
						float num2 = 0f;
						float num3 = 0f;
						float z = 0f;
						for (int i = 0; i < craftedWeaponData.UsedPieces.Length; i++)
						{
							WeaponDesignElement weaponDesignElement = craftedWeaponData.UsedPieces[i];
							if (weaponDesignElement.IsValid)
							{
								float scaledPieceOffset = weaponDesignElement.ScaledPieceOffset;
								float num4 = craftedWeaponData.PiecePivotDistances[i];
								float num5 = num4 + scaledPieceOffset - weaponDesignElement.ScaledDistanceToPreviousPiece;
								float num6 = num4 - scaledPieceOffset + weaponDesignElement.ScaledDistanceToNextPiece;
								num2 = MathF.Min(num5, num2);
								if (num6 > num3)
								{
									num3 = num6;
									z = (num6 + num5) * 0.5f;
								}
							}
						}
						WeaponDesignElement weaponDesignElement2 = craftedWeaponData.UsedPieces[2];
						if (weaponDesignElement2.IsValid)
						{
							float scaledPieceOffset2 = weaponDesignElement2.ScaledPieceOffset;
							num2 -= scaledPieceOffset2;
						}
						physicsShape2.AddCapsule(new CapsuleData(0.035f, new Vec3(0f, 0f, craftedWeaponData.CraftedWeaponLength, -1f), new Vec3(0f, 0f, num2, -1f)));
						bool flag = false;
						if (craftedWeaponData.UsedPieces[1].IsValid)
						{
							float z2 = craftedWeaponData.PiecePivotDistances[1];
							physicsShape2.AddCapsule(new CapsuleData(0.05f, new Vec3(-0.1f, 0f, z2, -1f), new Vec3(0.1f, 0f, z2, -1f)));
							flag = true;
						}
						if (weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.OneHandedAxe || weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.TwoHandedAxe || weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.ThrowingAxe)
						{
							WeaponDesignElement weaponDesignElement3 = craftedWeaponData.UsedPieces[0];
							float num7 = craftedWeaponData.PiecePivotDistances[0];
							float z3 = num7 + weaponDesignElement3.CraftingPiece.Length * 0.8f;
							float z4 = num7 - weaponDesignElement3.CraftingPiece.Length * 0.8f;
							float z5 = num7 + weaponDesignElement3.CraftingPiece.Length;
							float z6 = num7 - weaponDesignElement3.CraftingPiece.Length;
							float bladeWidth = weaponDesignElement3.CraftingPiece.BladeData.BladeWidth;
							physicsShape2.AddCapsule(new CapsuleData(0.05f, new Vec3(0f, 0f, z3, -1f), new Vec3(-bladeWidth, 0f, z5, -1f)));
							physicsShape2.AddCapsule(new CapsuleData(0.05f, new Vec3(0f, 0f, z4, -1f), new Vec3(-bladeWidth, 0f, z6, -1f)));
							physicsShape2.AddCapsule(new CapsuleData(0.05f, new Vec3(-bladeWidth, 0f, z5, -1f), new Vec3(-bladeWidth, 0f, z6, -1f)));
							flag = true;
						}
						if (weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.TwoHandedPolearm || weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.Javelin)
						{
							float z7 = craftedWeaponData.PiecePivotDistances[0];
							physicsShape2.AddCapsule(new CapsuleData(0.025f, new Vec3(-0.05f, 0f, z7, -1f), new Vec3(0.05f, 0f, z7, -1f)));
							flag = true;
						}
						if (!flag)
						{
							physicsShape2.AddCapsule(new CapsuleData(0.025f, new Vec3(-0.05f, 0f, z, -1f), new Vec3(0.05f, 0f, z, -1f)));
						}
					}
					else
					{
						weaponData.Shape.Prepare();
						int num8 = physicsShape.CapsuleCount();
						if (num8 == 0)
						{
							Debug.FailedAssert("Item has 0 body parts. Applying a default body, but this should not happen! Check this!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "RecalculateBody", 2752);
							return;
						}
						switch (weaponComponent.PrimaryWeapon.WeaponClass)
						{
						case WeaponClass.Dagger:
						case WeaponClass.OneHandedSword:
						case WeaponClass.TwoHandedSword:
						case WeaponClass.ThrowingKnife:
						{
							CapsuleData capsuleData = default(CapsuleData);
							physicsShape2.GetCapsule(ref capsuleData, 0);
							float radius = capsuleData.Radius;
							Vec3 p = capsuleData.P1;
							Vec3 p2 = capsuleData.P2;
							physicsShape2.SetCapsule(new CapsuleData(radius, new Vec3(p.x, p.y, p.z * num, -1f), p2), 0);
							break;
						}
						case WeaponClass.OneHandedAxe:
						case WeaponClass.TwoHandedAxe:
						case WeaponClass.Mace:
						case WeaponClass.TwoHandedMace:
						case WeaponClass.OneHandedPolearm:
						case WeaponClass.TwoHandedPolearm:
						case WeaponClass.LowGripPolearm:
						case WeaponClass.Arrow:
						case WeaponClass.Bolt:
						case WeaponClass.Crossbow:
						case WeaponClass.ThrowingAxe:
						case WeaponClass.Javelin:
						case WeaponClass.Banner:
						{
							CapsuleData capsuleData2 = default(CapsuleData);
							physicsShape2.GetCapsule(ref capsuleData2, 0);
							float radius2 = capsuleData2.Radius;
							Vec3 p3 = capsuleData2.P1;
							Vec3 p4 = capsuleData2.P2;
							physicsShape2.SetCapsule(new CapsuleData(radius2, new Vec3(p3.x, p3.y, p3.z * num, -1f), p4), 0);
							for (int j = 1; j < num8; j++)
							{
								CapsuleData capsuleData3 = default(CapsuleData);
								physicsShape2.GetCapsule(ref capsuleData3, j);
								float radius3 = capsuleData3.Radius;
								Vec3 p5 = capsuleData3.P1;
								Vec3 p6 = capsuleData3.P2;
								physicsShape2.SetCapsule(new CapsuleData(radius3, new Vec3(p5.x, p5.y, p5.z * num, -1f), new Vec3(p6.x, p6.y, p6.z * num, -1f)), j);
							}
							break;
						}
						case WeaponClass.SmallShield:
						case WeaponClass.LargeShield:
							Debug.FailedAssert("Shields should not have recalculate body flag.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "RecalculateBody", 2826);
							break;
						}
					}
				}
			}
			weaponData.CenterOfMassShift = weaponData.Shape.GetWeaponCenterOfMass();
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00054EC8 File Offset: 0x000530C8
		[UsedImplicitly]
		[MBCallback]
		internal void OnPreTick(float dt)
		{
			this.waitTickCompletion();
			for (int i = this.MissionBehaviors.Count - 1; i >= 0; i--)
			{
				this.MissionBehaviors[i].OnPreMissionTick(dt);
			}
			this.TickDebugAgents();
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00054F0C File Offset: 0x0005310C
		[UsedImplicitly]
		[MBCallback]
		internal void ApplySkeletonScaleToAllEquippedItems(string itemName)
		{
			int count = this.Agents.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					EquipmentElement equipmentElement = this.Agents[i].SpawnEquipment[j];
					if (!equipmentElement.IsEmpty && equipmentElement.Item.StringId == itemName)
					{
						HorseComponent horseComponent = equipmentElement.Item.HorseComponent;
						if (((horseComponent != null) ? horseComponent.SkeletonScale : null) != null)
						{
							this.Agents[i].AgentVisuals.ApplySkeletonScale(equipmentElement.Item.HorseComponent.SkeletonScale.MountSitBoneScale, equipmentElement.Item.HorseComponent.SkeletonScale.MountRadiusAdder, equipmentElement.Item.HorseComponent.SkeletonScale.BoneIndices, equipmentElement.Item.HorseComponent.SkeletonScale.Scales);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00055010 File Offset: 0x00053210
		[CommandLineFunctionality.CommandLineArgumentFunction("set_facial_anim_to_agent", "mission")]
		public static string SetFacialAnimToAgent(List<string> strings)
		{
			Mission mission = Mission.Current;
			if (mission == null)
			{
				return "Mission could not be found";
			}
			if (strings.Count != 2)
			{
				return "Enter agent index and animation name please";
			}
			int num;
			if (int.TryParse(strings[0], out num) && num >= 0)
			{
				foreach (Agent agent in mission.Agents)
				{
					if (agent.Index == num)
					{
						agent.SetAgentFacialAnimation(Agent.FacialAnimChannel.High, strings[1], true);
						return "Done";
					}
				}
			}
			return "Please enter a valid agent index";
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000550B8 File Offset: 0x000532B8
		private void waitTickCompletion()
		{
			while (!this.tickCompleted)
			{
				Thread.Sleep(1);
			}
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000550CC File Offset: 0x000532CC
		public void TickAgentsAndTeamsImp(float dt)
		{
			foreach (Agent agent in this.AllAgents)
			{
				agent.Tick(dt);
			}
			foreach (Team team in this.Teams)
			{
				team.Tick(dt);
			}
			this.tickCompleted = true;
			foreach (MBSubModuleBase mbsubModuleBase in Module.GetInstance().SubModules)
			{
				mbsubModuleBase.AfterAsyncTickTick(dt);
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x000551A8 File Offset: 0x000533A8
		[CommandLineFunctionality.CommandLineArgumentFunction("formation_speed_adjustment_enabled", "ai")]
		public static string EnableSpeedAdjustmentCommand(List<string> strings)
		{
			if (!GameNetwork.IsSessionActive)
			{
				HumanAIComponent.FormationSpeedAdjustmentEnabled = !HumanAIComponent.FormationSpeedAdjustmentEnabled;
				string text = "Speed Adjustment ";
				if (HumanAIComponent.FormationSpeedAdjustmentEnabled)
				{
					text += "enabled";
				}
				else
				{
					text += "disabled";
				}
				return text;
			}
			return "Does not work on multiplayer.";
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x000551F8 File Offset: 0x000533F8
		public void OnTick(float dt, float realDt, bool updateCamera, bool doAsyncAITick)
		{
			this.ApplyGeneratedCombatLogs();
			if (this.InputManager == null)
			{
				this.InputManager = new EmptyInputContext();
			}
			this.MissionTimeTracker.Tick(dt);
			this.CheckMissionEnd(this.CurrentTime);
			if (this.IsFastForward && this.MissionEnded)
			{
				this.IsFastForward = false;
			}
			if (this.CurrentState == Mission.State.Continuing)
			{
				if (this._inMissionLoadingScreenTimer != null && this._inMissionLoadingScreenTimer.Check(this.CurrentTime))
				{
					this._inMissionLoadingScreenTimer = null;
					Action onLoadingEndedAction = this._onLoadingEndedAction;
					if (onLoadingEndedAction != null)
					{
						onLoadingEndedAction();
					}
					LoadingWindow.DisableGlobalLoadingWindow();
				}
				for (int i = this.MissionBehaviors.Count - 1; i >= 0; i--)
				{
					this.MissionBehaviors[i].OnPreDisplayMissionTick(dt);
				}
				if (!GameNetwork.IsDedicatedServer && updateCamera)
				{
					this._missionState.Handler.UpdateCamera(this, realDt);
				}
				this.tickCompleted = false;
				for (int j = this.MissionBehaviors.Count - 1; j >= 0; j--)
				{
					this.MissionBehaviors[j].OnMissionTick(dt);
				}
				for (int k = this._dynamicEntities.Count - 1; k >= 0; k--)
				{
					Mission.DynamicEntityInfo dynamicEntityInfo = this._dynamicEntities[k];
					if (dynamicEntityInfo.TimerToDisable.Check(this.CurrentTime))
					{
						dynamicEntityInfo.Entity.RemoveEnginePhysics();
						dynamicEntityInfo.Entity.Remove(79);
						this._dynamicEntities.RemoveAt(k);
					}
				}
				this.HandleSpawnedItems();
				DebugNetworkEventStatistics.EndTick(dt);
				if (this.CurrentState == Mission.State.Continuing && this.IsFriendlyMission && !this.IsInPhotoMode)
				{
					if (this.InputManager.IsGameKeyDown(4))
					{
						this.OnEndMissionRequest();
					}
					else
					{
						this._leaveMissionTimer = null;
					}
				}
				if (doAsyncAITick)
				{
					this.TickAgentsAndTeamsAsync(dt);
					return;
				}
				this.TickAgentsAndTeamsImp(dt);
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000553BB File Offset: 0x000535BB
		public void RemoveSpawnedItemsAndMissiles()
		{
			this.ClearMissiles();
			this._missiles.Clear();
			this.RemoveSpawnedMissionObjects();
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000553D4 File Offset: 0x000535D4
		public void AfterStart()
		{
			this._activeAgents.Clear();
			this._allAgents.Clear();
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.OnBeforeMissionBehaviorInitialize(this);
			}
			for (int i = 0; i < this.MissionBehaviors.Count; i++)
			{
				this.MissionBehaviors[i].OnBehaviorInitialize();
			}
			foreach (MBSubModuleBase mbsubModuleBase2 in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase2.OnMissionBehaviorInitialize(this);
			}
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.EarlyStart();
			}
			this._battleSpawnPathSelector.Initialize();
			this._deploymentPlan.CreateReinforcementPlans();
			foreach (MissionBehavior missionBehavior2 in this.MissionBehaviors)
			{
				missionBehavior2.AfterStart();
			}
			foreach (MissionObject missionObject in this.MissionObjects)
			{
				missionObject.AfterMissionStart();
			}
			if (MissionGameModels.Current.ApplyWeatherEffectsModel != null)
			{
				MissionGameModels.Current.ApplyWeatherEffectsModel.ApplyWeatherEffects();
			}
			this.CurrentState = Mission.State.Continuing;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000555A0 File Offset: 0x000537A0
		public void OnEndMissionRequest()
		{
			foreach (MissionLogic missionLogic in this.MissionLogics)
			{
				bool flag;
				InquiryData inquiryData = missionLogic.OnEndMissionRequest(out flag);
				if (!flag)
				{
					this._leaveMissionTimer = null;
					return;
				}
				if (inquiryData != null)
				{
					this._leaveMissionTimer = null;
					InformationManager.ShowInquiry(inquiryData, true, false);
					return;
				}
			}
			if (this._leaveMissionTimer != null)
			{
				if (this._leaveMissionTimer.ElapsedTime > 0.6f)
				{
					this._leaveMissionTimer = null;
					this.EndMission();
					return;
				}
			}
			else
			{
				this._leaveMissionTimer = new BasicMissionTimer();
			}
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00055648 File Offset: 0x00053848
		public float GetMissionEndTimeInSeconds()
		{
			return 0.6f;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0005564F File Offset: 0x0005384F
		public float GetMissionEndTimerValue()
		{
			if (this._leaveMissionTimer == null)
			{
				return -1f;
			}
			return this._leaveMissionTimer.ElapsedTime;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0005566C File Offset: 0x0005386C
		private void ApplyGeneratedCombatLogs()
		{
			if (!this._combatLogsCreated.IsEmpty)
			{
				CombatLogData logData;
				while (this._combatLogsCreated.TryDequeue(out logData))
				{
					CombatLogManager.GenerateCombatLog(logData);
				}
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x000556A0 File Offset: 0x000538A0
		public int GetMemberCountOfSide(BattleSideEnum side)
		{
			int num = 0;
			foreach (Team team in this.Teams)
			{
				if (team.Side == side)
				{
					num += team.ActiveAgents.Count;
				}
			}
			return num;
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00055708 File Offset: 0x00053908
		public Path GetInitialSpawnPath()
		{
			return this._battleSpawnPathSelector.InitialPath;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00055718 File Offset: 0x00053918
		public SpawnPathData GetInitialSpawnPathDataOfSide(BattleSideEnum battleSide)
		{
			SpawnPathData result;
			this._battleSpawnPathSelector.GetInitialPathDataOfSide(battleSide, out result);
			return result;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00055735 File Offset: 0x00053935
		public MBReadOnlyList<SpawnPathData> GetReinforcementPathsDataOfSide(BattleSideEnum battleSide)
		{
			return this._battleSpawnPathSelector.GetReinforcementPathsDataOfSide(battleSide);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00055744 File Offset: 0x00053944
		public void GetTroopSpawnFrameWithIndex(AgentBuildData buildData, int troopSpawnIndex, int troopSpawnCount, out Vec3 troopSpawnPosition, out Vec2 troopSpawnDirection)
		{
			Formation agentFormation = buildData.AgentFormation;
			BasicCharacterObject agentCharacter = buildData.AgentCharacter;
			troopSpawnPosition = Vec3.Invalid;
			WorldPosition worldPosition;
			Vec2 direction;
			if (buildData.AgentSpawnsIntoOwnFormation)
			{
				worldPosition = agentFormation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.GroundVec3);
				direction = agentFormation.Direction;
			}
			else
			{
				IAgentOriginBase agentOrigin = buildData.AgentOrigin;
				bool agentIsReinforcement = buildData.AgentIsReinforcement;
				BattleSideEnum side = buildData.AgentTeam.Side;
				if (buildData.AgentSpawnsUsingOwnTroopClass)
				{
					FormationClass agentTroopClass = this.GetAgentTroopClass(side, agentCharacter);
					this.GetFormationSpawnFrame(side, agentTroopClass, agentIsReinforcement, out worldPosition, out direction);
				}
				else if (agentCharacter.IsHero && agentOrigin != null && agentOrigin.BattleCombatant != null && agentCharacter == agentOrigin.BattleCombatant.General && this.GetFormationSpawnClass(side, FormationClass.NumberOfRegularFormations, agentIsReinforcement) == FormationClass.NumberOfRegularFormations)
				{
					this.GetFormationSpawnFrame(side, FormationClass.NumberOfRegularFormations, agentIsReinforcement, out worldPosition, out direction);
				}
				else
				{
					this.GetFormationSpawnFrame(side, agentFormation.FormationIndex, agentIsReinforcement, out worldPosition, out direction);
				}
			}
			bool isMountedFormation = !buildData.AgentNoHorses && agentFormation.HasAnyMountedUnit;
			WorldPosition? worldPosition2;
			Vec2? vec;
			agentFormation.GetUnitSpawnFrameWithIndex(troopSpawnIndex, worldPosition, direction, agentFormation.Width, troopSpawnCount, agentFormation.UnitSpacing, isMountedFormation, out worldPosition2, out vec);
			if (worldPosition2 != null && buildData.MakeUnitStandOutDistance != 0f)
			{
				worldPosition2.Value.SetVec2(worldPosition2.Value.AsVec2 + vec.Value * buildData.MakeUnitStandOutDistance);
			}
			if (worldPosition2 != null)
			{
				if (worldPosition2.Value.GetNavMesh() == UIntPtr.Zero)
				{
					troopSpawnPosition = this.Scene.GetLastPointOnNavigationMeshFromWorldPositionToDestination(ref worldPosition, worldPosition2.Value.AsVec2);
				}
				else
				{
					troopSpawnPosition = worldPosition2.Value.GetGroundVec3();
				}
			}
			if (!troopSpawnPosition.IsValid)
			{
				troopSpawnPosition = worldPosition.GetGroundVec3();
			}
			troopSpawnDirection = ((vec != null) ? vec.Value : direction);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00055934 File Offset: 0x00053B34
		public void GetFormationSpawnFrame(BattleSideEnum side, FormationClass formationClass, bool isReinforcement, out WorldPosition spawnPosition, out Vec2 spawnDirection)
		{
			DeploymentPlanType planType = isReinforcement ? DeploymentPlanType.Reinforcement : DeploymentPlanType.Initial;
			IFormationDeploymentPlan formationPlan = this._deploymentPlan.GetFormationPlan(side, formationClass, planType);
			spawnPosition = formationPlan.CreateNewDeploymentWorldPosition(WorldPosition.WorldPositionEnforcedCache.GroundVec3);
			spawnDirection = formationPlan.GetDirection();
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00055974 File Offset: 0x00053B74
		public WorldFrame GetBattleSideInitialSpawnPathFrame(BattleSideEnum battleSide, float pathOffset = 0f)
		{
			SpawnPathData initialSpawnPathDataOfSide = this.GetInitialSpawnPathDataOfSide(battleSide);
			if (initialSpawnPathDataOfSide.IsValid)
			{
				Vec2 vec;
				Vec2 vec2;
				initialSpawnPathDataOfSide.GetOrientedSpawnPathPosition(out vec, out vec2, pathOffset);
				Mat3 identity = Mat3.Identity;
				identity.RotateAboutUp(vec2.RotationInRadians);
				WorldPosition origin = new WorldPosition(this.Scene, UIntPtr.Zero, vec.ToVec3(0f), false);
				return new WorldFrame(identity, origin);
			}
			return WorldFrame.Invalid;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000559E0 File Offset: 0x00053BE0
		private void BuildAgent(Agent agent, AgentBuildData agentBuildData)
		{
			if (agent == null)
			{
				throw new MBNullParameterException("agent");
			}
			agent.Build(agentBuildData, this._agentCreationIndex);
			this._agentCreationIndex++;
			if (!agent.SpawnEquipment[EquipmentIndex.ArmorItemEndSlot].IsEmpty)
			{
				EquipmentElement equipmentElement = agent.SpawnEquipment[EquipmentIndex.ArmorItemEndSlot];
				if (equipmentElement.Item.HorseComponent.BodyLength != 0)
				{
					agent.SetInitialAgentScale(0.01f * (float)equipmentElement.Item.HorseComponent.BodyLength);
				}
			}
			agent.EquipItemsFromSpawnEquipment(true);
			agent.InitializeAgentRecord();
			agent.AgentVisuals.BatchLastLodMeshes();
			agent.PreloadForRendering();
			ActionIndexValueCache currentActionValue = agent.GetCurrentActionValue(0);
			if (currentActionValue != ActionIndexValueCache.act_none)
			{
				agent.SetActionChannel(0, currentActionValue, false, 0UL, 0f, 1f, -0.2f, 0.4f, MBRandom.RandomFloat * 0.8f, false, -0.2f, 0, true);
			}
			agent.InitializeComponents();
			if (agent.Controller == Agent.ControllerType.Player)
			{
				this.ResetFirstThirdPersonView();
			}
			this._activeAgents.Add(agent);
			this._allAgents.Add(agent);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00055B00 File Offset: 0x00053D00
		private Agent CreateAgent(Monster monster, bool isFemale, int instanceNo, Agent.CreationType creationType, float stepSize, int forcedAgentIndex, int weight, BasicCharacterObject characterObject)
		{
			AnimationSystemData animationSystemData = monster.FillAnimationSystemData(stepSize, false, isFemale);
			AgentCapsuleData agentCapsuleData = monster.FillCapsuleData();
			AgentSpawnData agentSpawnData = monster.FillSpawnData(null);
			Mission.AgentCreationResult creationResult = this.CreateAgentInternal(monster.Flags, forcedAgentIndex, isFemale, ref agentSpawnData, ref agentCapsuleData, ref animationSystemData, instanceNo);
			Agent agent = new Agent(this, creationResult, creationType, monster);
			agent.Character = characterObject;
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnAgentCreated(agent);
			}
			return agent;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00055B9C File Offset: 0x00053D9C
		public void SetBattleAgentCount(int agentCount)
		{
			if (this._agentCount == 0 || this._agentCount > agentCount)
			{
				this._agentCount = agentCount;
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00055BB8 File Offset: 0x00053DB8
		public Vec2 GetFormationSpawnPosition(BattleSideEnum side, FormationClass formationClass, bool isReinforcement)
		{
			DeploymentPlanType planType = isReinforcement ? DeploymentPlanType.Reinforcement : DeploymentPlanType.Initial;
			return this._deploymentPlan.GetFormationPlan(side, formationClass, planType).CreateNewDeploymentWorldPosition(WorldPosition.WorldPositionEnforcedCache.None).AsVec2;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00055BEC File Offset: 0x00053DEC
		public FormationClass GetFormationSpawnClass(BattleSideEnum side, FormationClass formationClass, bool isReinforcement)
		{
			DeploymentPlanType planType = isReinforcement ? DeploymentPlanType.Reinforcement : DeploymentPlanType.Initial;
			return this._deploymentPlan.GetFormationPlan(side, formationClass, planType).SpawnClass;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00055C14 File Offset: 0x00053E14
		public Agent SpawnAgent(AgentBuildData agentBuildData, bool spawnFromAgentVisuals = false)
		{
			BasicCharacterObject agentCharacter = agentBuildData.AgentCharacter;
			if (agentCharacter == null)
			{
				throw new MBNullParameterException("npcCharacterObject");
			}
			int forcedAgentIndex = -1;
			if (agentBuildData.AgentIndexOverriden)
			{
				forcedAgentIndex = agentBuildData.AgentIndex;
			}
			Agent agent = this.CreateAgent(agentBuildData.AgentMonster, agentBuildData.GenderOverriden ? agentBuildData.AgentIsFemale : agentCharacter.IsFemale, 0, Agent.CreationType.FromCharacterObj, agentCharacter.GetStepSize(), forcedAgentIndex, agentBuildData.AgentMonster.Weight, agentCharacter);
			agent.FormationPositionPreference = agentCharacter.FormationPositionPreference;
			float num = agentBuildData.AgeOverriden ? ((float)agentBuildData.AgentAge) : agentCharacter.Age;
			if (num == 0f)
			{
				agentBuildData.Age(29);
			}
			else if (MBBodyProperties.GetMaturityType(num) < BodyMeshMaturityType.Teenager && (this.Mode == MissionMode.Battle || this.Mode == MissionMode.Duel || this.Mode == MissionMode.Tournament || this.Mode == MissionMode.Stealth))
			{
				agentBuildData.Age(27);
			}
			if (agentBuildData.BodyPropertiesOverriden)
			{
				agent.UpdateBodyProperties(agentBuildData.AgentBodyProperties);
				if (!agentBuildData.AgeOverriden)
				{
					agent.Age = agentCharacter.Age;
				}
			}
			agent.BodyPropertiesSeed = agentBuildData.AgentEquipmentSeed;
			if (agentBuildData.AgeOverriden)
			{
				agent.Age = (float)agentBuildData.AgentAge;
			}
			if (agentBuildData.GenderOverriden)
			{
				agent.IsFemale = agentBuildData.AgentIsFemale;
			}
			agent.SetTeam(agentBuildData.AgentTeam, false);
			agent.SetClothingColor1(agentBuildData.AgentClothingColor1);
			agent.SetClothingColor2(agentBuildData.AgentClothingColor2);
			agent.SetRandomizeColors(agentBuildData.RandomizeColors);
			agent.Origin = agentBuildData.AgentOrigin;
			Formation agentFormation = agentBuildData.AgentFormation;
			if (agentFormation != null && !agentFormation.HasBeenPositioned)
			{
				this.SetFormationPositioningFromDeploymentPlan(agentFormation);
			}
			if (agentBuildData.AgentInitialPosition == null)
			{
				BattleSideEnum side = agentBuildData.AgentTeam.Side;
				Vec3 vec = Vec3.Invalid;
				Vec2 vec2 = Vec2.Invalid;
				if (agentCharacter == Game.Current.PlayerTroop && this._deploymentPlan.HasPlayerSpawnFrame(side))
				{
					WorldPosition worldPosition;
					Vec2 vec3;
					this._deploymentPlan.GetPlayerSpawnFrame(side, out worldPosition, out vec3);
					vec = worldPosition.GetGroundVec3();
					vec2 = vec3;
				}
				else if (agentFormation != null)
				{
					int num2;
					int num3;
					if (agentBuildData.AgentSpawnsIntoOwnFormation)
					{
						num2 = agentFormation.CountOfUnits;
						num3 = num2 + 1;
					}
					else if (agentBuildData.AgentFormationTroopSpawnIndex >= 0 && agentBuildData.AgentFormationTroopSpawnCount > 0)
					{
						num2 = agentBuildData.AgentFormationTroopSpawnIndex;
						num3 = agentBuildData.AgentFormationTroopSpawnCount;
					}
					else
					{
						num2 = agentFormation.GetNextSpawnIndex();
						num3 = num2 + 1;
					}
					if (num2 >= num3)
					{
						num3 = num2 + 1;
					}
					this.GetTroopSpawnFrameWithIndex(agentBuildData, num2, num3, out vec, out vec2);
				}
				else
				{
					WorldPosition worldPosition2;
					this.GetFormationSpawnFrame(side, FormationClass.NumberOfAllFormations, agentBuildData.AgentIsReinforcement, out worldPosition2, out vec2);
					vec = worldPosition2.GetGroundVec3();
				}
				agentBuildData.InitialPosition(vec).InitialDirection(vec2);
			}
			Agent agent2 = agent;
			Vec3 valueOrDefault = agentBuildData.AgentInitialPosition.GetValueOrDefault();
			Vec2 valueOrDefault2 = agentBuildData.AgentInitialDirection.GetValueOrDefault();
			agent2.SetInitialFrame(valueOrDefault, valueOrDefault2, agentBuildData.AgentCanSpawnOutsideOfMissionBoundary);
			if (agentCharacter.AllEquipments == null)
			{
				Debug.Print("characterObject.AllEquipments is null for \"" + agentCharacter.StringId + "\".", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			if (agentCharacter.AllEquipments != null)
			{
				if (agentCharacter.AllEquipments.Any((Equipment eq) => eq == null))
				{
					Debug.Print("Character with id \"" + agentCharacter.StringId + "\" has a null equipment in its AllEquipments.", 0, Debug.DebugColor.White, 17592186044416UL);
				}
			}
			if ((from eq in agentCharacter.AllEquipments
			where eq.IsCivilian
			select eq) == null)
			{
				agentBuildData.CivilianEquipment(false);
			}
			if (agentCharacter.IsHero)
			{
				agentBuildData.FixedEquipment(true);
			}
			Equipment equipment;
			if (agentBuildData.AgentOverridenSpawnEquipment != null)
			{
				equipment = agentBuildData.AgentOverridenSpawnEquipment.Clone(false);
			}
			else if (!agentBuildData.AgentFixedEquipment)
			{
				equipment = Equipment.GetRandomEquipmentElements(agent.Character, !Game.Current.GameType.IsCoreOnlyGameMode, agentBuildData.AgentCivilianEquipment, agentBuildData.AgentEquipmentSeed);
			}
			else
			{
				equipment = agentCharacter.GetFirstEquipment(agentBuildData.AgentCivilianEquipment).Clone(false);
			}
			Agent agent3 = null;
			if (agentBuildData.AgentNoHorses)
			{
				equipment[EquipmentIndex.ArmorItemEndSlot] = default(EquipmentElement);
				equipment[EquipmentIndex.HorseHarness] = default(EquipmentElement);
			}
			if (agentBuildData.AgentNoWeapons)
			{
				equipment[EquipmentIndex.WeaponItemBeginSlot] = default(EquipmentElement);
				equipment[EquipmentIndex.Weapon1] = default(EquipmentElement);
				equipment[EquipmentIndex.Weapon2] = default(EquipmentElement);
				equipment[EquipmentIndex.Weapon3] = default(EquipmentElement);
				equipment[EquipmentIndex.ExtraWeaponSlot] = default(EquipmentElement);
			}
			if (agentCharacter.IsHero)
			{
				ItemObject itemObject = null;
				ItemObject item = equipment[EquipmentIndex.ExtraWeaponSlot].Item;
				if (item != null && item.IsBannerItem && item.BannerComponent != null)
				{
					itemObject = item;
					equipment[EquipmentIndex.ExtraWeaponSlot] = default(EquipmentElement);
				}
				else if (agentBuildData.AgentBannerItem != null)
				{
					itemObject = agentBuildData.AgentBannerItem;
				}
				if (itemObject != null)
				{
					agent.SetFormationBanner(itemObject);
				}
			}
			else if (agentBuildData.AgentBannerItem != null)
			{
				equipment[EquipmentIndex.Weapon1] = default(EquipmentElement);
				equipment[EquipmentIndex.Weapon2] = default(EquipmentElement);
				equipment[EquipmentIndex.Weapon3] = default(EquipmentElement);
				if (agentBuildData.AgentBannerReplacementWeaponItem != null)
				{
					equipment[EquipmentIndex.WeaponItemBeginSlot] = new EquipmentElement(agentBuildData.AgentBannerReplacementWeaponItem, null, null, false);
				}
				else
				{
					equipment[EquipmentIndex.WeaponItemBeginSlot] = default(EquipmentElement);
				}
				equipment[EquipmentIndex.ExtraWeaponSlot] = new EquipmentElement(agentBuildData.AgentBannerItem, null, null, false);
				if (agentBuildData.AgentOverridenSpawnMissionEquipment != null)
				{
					agentBuildData.AgentOverridenSpawnMissionEquipment[EquipmentIndex.ExtraWeaponSlot] = new MissionWeapon(agentBuildData.AgentBannerItem, null, agentBuildData.AgentBanner);
				}
			}
			if (agentBuildData.AgentNoArmor)
			{
				equipment[EquipmentIndex.Gloves] = default(EquipmentElement);
				equipment[EquipmentIndex.Body] = default(EquipmentElement);
				equipment[EquipmentIndex.Cape] = default(EquipmentElement);
				equipment[EquipmentIndex.NumAllWeaponSlots] = default(EquipmentElement);
				equipment[EquipmentIndex.Leg] = default(EquipmentElement);
			}
			for (int i = 0; i < 5; i++)
			{
				if (!equipment[(EquipmentIndex)i].IsEmpty && equipment[(EquipmentIndex)i].Item.ItemFlags.HasAnyFlag(ItemFlags.CannotBePickedUp))
				{
					equipment[(EquipmentIndex)i] = default(EquipmentElement);
				}
			}
			agent.InitializeSpawnEquipment(equipment);
			agent.InitializeMissionEquipment(agentBuildData.AgentOverridenSpawnMissionEquipment, agentBuildData.AgentBanner);
			if (agent.RandomizeColors)
			{
				agent.Equipment.SetGlossMultipliersOfWeaponsRandomly(agentBuildData.AgentEquipmentSeed);
			}
			ItemObject item2 = equipment[EquipmentIndex.ArmorItemEndSlot].Item;
			if (item2 != null && item2.HasHorseComponent && item2.HorseComponent.IsRideable)
			{
				int forcedAgentMountIndex = -1;
				if (agentBuildData.AgentMountIndexOverriden)
				{
					forcedAgentMountIndex = agentBuildData.AgentMountIndex;
				}
				EquipmentElement mount = equipment[EquipmentIndex.ArmorItemEndSlot];
				EquipmentElement mountHarness = equipment[EquipmentIndex.HorseHarness];
				valueOrDefault = agentBuildData.AgentInitialPosition.GetValueOrDefault();
				valueOrDefault2 = agentBuildData.AgentInitialDirection.GetValueOrDefault();
				agent3 = this.CreateHorseAgentFromRosterElements(mount, mountHarness, valueOrDefault, valueOrDefault2, forcedAgentMountIndex, agentBuildData.AgentMountKey);
				Equipment equipment2 = new Equipment();
				equipment2[EquipmentIndex.ArmorItemEndSlot] = equipment[EquipmentIndex.ArmorItemEndSlot];
				equipment2[EquipmentIndex.HorseHarness] = equipment[EquipmentIndex.HorseHarness];
				Equipment spawnEquipment = equipment2;
				agent3.InitializeSpawnEquipment(spawnEquipment);
				agent.SetMountAgentBeforeBuild(agent3);
			}
			if (spawnFromAgentVisuals || !GameNetwork.IsClientOrReplay)
			{
				agent.Equipment.CheckLoadedAmmos();
			}
			if (!agentBuildData.BodyPropertiesOverriden)
			{
				agent.UpdateBodyProperties(agentCharacter.GetBodyProperties(equipment, agentBuildData.AgentEquipmentSeed));
			}
			if (GameNetwork.IsServerOrRecorder && agent.RiderAgent == null)
			{
				Vec3 valueOrDefault3 = agentBuildData.AgentInitialPosition.GetValueOrDefault();
				Vec2 valueOrDefault4 = agentBuildData.AgentInitialDirection.GetValueOrDefault();
				if (agent.IsMount)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new CreateFreeMountAgent(agent, valueOrDefault3, valueOrDefault4));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				else
				{
					bool flag = agentBuildData.AgentMissionPeer != null;
					NetworkCommunicator networkCommunicator;
					if (!flag)
					{
						MissionPeer owningAgentMissionPeer = agentBuildData.OwningAgentMissionPeer;
						networkCommunicator = ((owningAgentMissionPeer != null) ? owningAgentMissionPeer.GetNetworkPeer() : null);
					}
					else
					{
						networkCommunicator = agentBuildData.AgentMissionPeer.GetNetworkPeer();
					}
					NetworkCommunicator peer = networkCommunicator;
					bool flag2 = agent.MountAgent != null && agent.MountAgent.RiderAgent == agent;
					GameNetwork.BeginBroadcastModuleEvent();
					int index = agent.Index;
					BasicCharacterObject character = agent.Character;
					Monster monster = agent.Monster;
					Equipment spawnEquipment2 = agent.SpawnEquipment;
					MissionEquipment equipment3 = agent.Equipment;
					BodyProperties bodyPropertiesValue = agent.BodyPropertiesValue;
					int bodyPropertiesSeed = agent.BodyPropertiesSeed;
					bool isFemale = agent.IsFemale;
					Team team = agent.Team;
					int agentTeamIndex = (team != null) ? team.TeamIndex : -1;
					Formation formation = agent.Formation;
					int agentFormationIndex = (formation != null) ? formation.Index : -1;
					uint clothingColor = agent.ClothingColor1;
					uint clothingColor2 = agent.ClothingColor2;
					int mountAgentIndex = flag2 ? agent.MountAgent.Index : -1;
					Agent mountAgent = agent.MountAgent;
					GameNetwork.WriteMessage(new CreateAgent(index, character, monster, spawnEquipment2, equipment3, bodyPropertiesValue, bodyPropertiesSeed, isFemale, agentTeamIndex, agentFormationIndex, clothingColor, clothingColor2, mountAgentIndex, (mountAgent != null) ? mountAgent.SpawnEquipment : null, flag, valueOrDefault3, valueOrDefault4, peer));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
			MultiplayerMissionAgentVisualSpawnComponent missionBehavior = this.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>();
			if (missionBehavior != null && agentBuildData.AgentMissionPeer != null && agentBuildData.AgentMissionPeer.IsMine && agentBuildData.AgentVisualsIndex == 0)
			{
				missionBehavior.OnMyAgentSpawned();
			}
			if (agent3 != null)
			{
				this.BuildAgent(agent3, agentBuildData);
				foreach (MissionBehavior missionBehavior2 in this.MissionBehaviors)
				{
					missionBehavior2.OnAgentBuild(agent3, null);
				}
			}
			this.BuildAgent(agent, agentBuildData);
			if (agentBuildData.AgentMissionPeer != null)
			{
				agent.MissionPeer = agentBuildData.AgentMissionPeer;
			}
			if (agentBuildData.OwningAgentMissionPeer != null)
			{
				agent.OwningAgentMissionPeer = agentBuildData.OwningAgentMissionPeer;
			}
			foreach (MissionBehavior missionBehavior3 in this.MissionBehaviors)
			{
				Agent agent4 = agent;
				Banner banner;
				if ((banner = agentBuildData.AgentBanner) == null)
				{
					Team agentTeam = agentBuildData.AgentTeam;
					banner = ((agentTeam != null) ? agentTeam.Banner : null);
				}
				missionBehavior3.OnAgentBuild(agent4, banner);
			}
			agent.AgentVisuals.CheckResources(true);
			if (agent.IsAIControlled)
			{
				if (agent3 == null)
				{
					AgentFlag agentFlags = agent.GetAgentFlags() & ~AgentFlag.CanRide;
					agent.SetAgentFlags(agentFlags);
				}
				else if (agent.Formation == null)
				{
					agent.SetRidingOrder(RidingOrder.RidingOrderEnum.Mount);
				}
			}
			return agent;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0005664C File Offset: 0x0005484C
		public void SetInitialAgentCountForSide(BattleSideEnum side, int agentCount)
		{
			if (side >= BattleSideEnum.Defender && side < BattleSideEnum.NumSides)
			{
				this._initialAgentCountPerSide[(int)side] = agentCount;
				return;
			}
			Debug.FailedAssert("Cannot set initial agent count.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "SetInitialAgentCountForSide", 4146);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00056688 File Offset: 0x00054888
		public void SetFormationPositioningFromDeploymentPlan(Formation formation)
		{
			IFormationDeploymentPlan formationPlan = this._deploymentPlan.GetFormationPlan(formation.Team.Side, formation.FormationIndex, DeploymentPlanType.Initial);
			if (formationPlan.PlannedTroopCount > 0 && formationPlan.HasDimensions)
			{
				formation.FormOrder = FormOrder.FormOrderCustom(formationPlan.PlannedWidth);
			}
			formation.SetPositioning(new WorldPosition?(formationPlan.CreateNewDeploymentWorldPosition(WorldPosition.WorldPositionEnforcedCache.None)), new Vec2?(formationPlan.GetDirection()), null);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x000566FB File Offset: 0x000548FB
		public Agent SpawnMonster(ItemRosterElement rosterElement, ItemRosterElement harnessRosterElement, in Vec3 initialPosition, in Vec2 initialDirection, int forcedAgentIndex = -1)
		{
			return this.SpawnMonster(rosterElement.EquipmentElement, harnessRosterElement.EquipmentElement, initialPosition, initialDirection, forcedAgentIndex);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00056718 File Offset: 0x00054918
		public Agent SpawnMonster(EquipmentElement equipmentElement, EquipmentElement harnessRosterElement, in Vec3 initialPosition, in Vec2 initialDirection, int forcedAgentIndex = -1)
		{
			Agent agent = this.CreateHorseAgentFromRosterElements(equipmentElement, harnessRosterElement, initialPosition, initialDirection, forcedAgentIndex, MountCreationKey.GetRandomMountKeyString(equipmentElement.Item, MBRandom.RandomInt()));
			Equipment equipment = new Equipment();
			equipment[EquipmentIndex.ArmorItemEndSlot] = equipmentElement;
			equipment[EquipmentIndex.HorseHarness] = harnessRosterElement;
			Equipment spawnEquipment = equipment;
			agent.InitializeSpawnEquipment(spawnEquipment);
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new CreateFreeMountAgent(agent, initialPosition, initialDirection));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			this.BuildAgent(agent, null);
			return agent;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00056798 File Offset: 0x00054998
		public Agent SpawnTroop(IAgentOriginBase troopOrigin, bool isPlayerSide, bool hasFormation, bool spawnWithHorse, bool isReinforcement, int formationTroopCount, int formationTroopIndex, bool isAlarmed, bool wieldInitialWeapons, bool forceDismounted, Vec3? initialPosition, Vec2? initialDirection, string specialActionSetSuffix = null, ItemObject bannerItem = null, FormationClass formationIndex = FormationClass.NumberOfAllFormations, bool useTroopClassForSpawn = false)
		{
			BasicCharacterObject troop = troopOrigin.Troop;
			Team agentTeam = Mission.GetAgentTeam(troopOrigin, isPlayerSide);
			if (troop.IsPlayerCharacter && !forceDismounted)
			{
				spawnWithHorse = true;
			}
			AgentBuildData agentBuildData = new AgentBuildData(troop).Team(agentTeam).Banner(troopOrigin.Banner).ClothingColor1(agentTeam.Color).ClothingColor2(agentTeam.Color2).TroopOrigin(troopOrigin).NoHorses(!spawnWithHorse).CivilianEquipment(this.DoesMissionRequireCivilianEquipment).SpawnsUsingOwnTroopClass(useTroopClassForSpawn);
			if (hasFormation)
			{
				Formation formation;
				if (formationIndex == FormationClass.NumberOfAllFormations)
				{
					formation = agentTeam.GetFormation(this.GetAgentTroopClass(agentTeam.Side, troop));
				}
				else
				{
					formation = agentTeam.GetFormation(formationIndex);
				}
				agentBuildData.Formation(formation);
				agentBuildData.FormationTroopSpawnCount(formationTroopCount).FormationTroopSpawnIndex(formationTroopIndex);
			}
			if (!troop.IsPlayerCharacter)
			{
				agentBuildData.IsReinforcement(isReinforcement);
			}
			if (bannerItem != null)
			{
				if (bannerItem.IsBannerItem && bannerItem.BannerComponent != null)
				{
					agentBuildData.BannerItem(bannerItem);
					ItemObject bannerBearerReplacementWeapon = MissionGameModels.Current.BattleBannerBearersModel.GetBannerBearerReplacementWeapon(troop);
					agentBuildData.BannerReplacementWeaponItem(bannerBearerReplacementWeapon);
				}
				else
				{
					Debug.FailedAssert("Passed banner item with name: " + bannerItem.Name + " is not a proper banner item", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "SpawnTroop", 4253);
					Debug.Print("Invalid banner item: " + bannerItem.Name + " is passed to a troop to be spawned", 0, Debug.DebugColor.Yellow, 17592186044416UL);
				}
			}
			if (initialPosition != null)
			{
				AgentBuildData agentBuildData2 = agentBuildData;
				Vec3 value = initialPosition.Value;
				agentBuildData2.InitialPosition(value);
				AgentBuildData agentBuildData3 = agentBuildData;
				Vec2 value2 = initialDirection.Value;
				agentBuildData3.InitialDirection(value2);
			}
			if (spawnWithHorse)
			{
				agentBuildData.MountKey(MountCreationKey.GetRandomMountKeyString(troop.Equipment[EquipmentIndex.ArmorItemEndSlot].Item, troop.GetMountKeySeed()));
			}
			if (isPlayerSide && troop == Game.Current.PlayerTroop)
			{
				agentBuildData.Controller(Agent.ControllerType.Player);
			}
			Agent agent = this.SpawnAgent(agentBuildData, false);
			if (agent.Character.IsHero)
			{
				agent.SetAgentFlags(agent.GetAgentFlags() | AgentFlag.IsUnique);
			}
			if (agent.IsAIControlled && isAlarmed)
			{
				agent.SetWatchState(Agent.WatchState.Alarmed);
			}
			if (wieldInitialWeapons)
			{
				agent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
			}
			if (!string.IsNullOrEmpty(specialActionSetSuffix))
			{
				AnimationSystemData animationSystemData = agentBuildData.AgentMonster.FillAnimationSystemData(MBGlobals.GetActionSetWithSuffix(agentBuildData.AgentMonster, agentBuildData.AgentIsFemale, specialActionSetSuffix), agent.Character.GetStepSize(), false);
				agent.SetActionSet(ref animationSystemData);
			}
			return agent;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000569F0 File Offset: 0x00054BF0
		public Agent ReplaceBotWithPlayer(Agent botAgent, MissionPeer missionPeer)
		{
			if (!GameNetwork.IsClientOrReplay && botAgent != null)
			{
				if (GameNetwork.IsServer)
				{
					NetworkCommunicator networkPeer = missionPeer.GetNetworkPeer();
					if (!networkPeer.IsServerPeer)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						NetworkCommunicator peer = networkPeer;
						int index = botAgent.Index;
						float health = botAgent.Health;
						Agent mountAgent = botAgent.MountAgent;
						GameNetwork.WriteMessage(new ReplaceBotWithPlayer(peer, index, health, (mountAgent != null) ? mountAgent.Health : -1f));
						GameNetwork.EndModuleEventAsServer();
					}
				}
				if (botAgent.Formation != null)
				{
					botAgent.Formation.PlayerOwner = botAgent;
				}
				botAgent.OwningAgentMissionPeer = null;
				botAgent.MissionPeer = missionPeer;
				botAgent.Formation = missionPeer.ControlledFormation;
				AgentFlag agentFlags = botAgent.GetAgentFlags();
				if (!agentFlags.HasAnyFlag(AgentFlag.CanRide))
				{
					botAgent.SetAgentFlags(agentFlags | AgentFlag.CanRide);
				}
				int botsUnderControlAlive = missionPeer.BotsUnderControlAlive;
				missionPeer.BotsUnderControlAlive = botsUnderControlAlive - 1;
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new BotsControlledChange(missionPeer.GetNetworkPeer(), missionPeer.BotsUnderControlAlive, missionPeer.BotsUnderControlTotal));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				if (botAgent.Formation != null)
				{
					missionPeer.Team.AssignPlayerAsSergeantOfFormation(missionPeer, missionPeer.ControlledFormation.FormationIndex);
				}
				return botAgent;
			}
			return null;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00056B04 File Offset: 0x00054D04
		private Agent CreateHorseAgentFromRosterElements(EquipmentElement mount, EquipmentElement mountHarness, in Vec3 initialPosition, in Vec2 initialDirection, int forcedAgentMountIndex, string horseCreationKey)
		{
			HorseComponent horseComponent = mount.Item.HorseComponent;
			Agent agent = this.CreateAgent(horseComponent.Monster, false, 0, Agent.CreationType.FromHorseObj, 1f, forcedAgentMountIndex, (int)mount.Weight, null);
			agent.SetInitialFrame(initialPosition, initialDirection, false);
			agent.BaseHealthLimit = (float)mount.GetModifiedMountHitPoints();
			agent.HealthLimit = agent.BaseHealthLimit;
			agent.Health = agent.HealthLimit;
			agent.SetMountInitialValues(mount.GetModifiedItemName(), horseCreationKey);
			return agent;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00056B7C File Offset: 0x00054D7C
		public void OnAgentInteraction(Agent requesterAgent, Agent targetAgent)
		{
			if (requesterAgent == Agent.Main && targetAgent.IsMount)
			{
				Agent.Main.Mount(targetAgent);
				return;
			}
			if (targetAgent.IsHuman)
			{
				foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
				{
					missionBehavior.OnAgentInteraction(requesterAgent, targetAgent);
				}
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00056BF4 File Offset: 0x00054DF4
		[UsedImplicitly]
		[MBCallback]
		public void EndMission()
		{
			Debug.Print("I called EndMission", 0, Debug.DebugColor.White, 17179869184UL);
			this._missionEndTime = -1f;
			this.NextCheckTimeEndMission = -1f;
			this.MissionEnded = true;
			this.CurrentState = Mission.State.EndingNextFrame;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00056C30 File Offset: 0x00054E30
		private void EndMissionInternal()
		{
			MBDebug.Print("I called EndMissionInternal", 0, Debug.DebugColor.White, 17179869184UL);
			this._deploymentPlan.ClearAll();
			IMissionListener[] array = this._listeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnEndMission();
			}
			this.StopSoundEvents();
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnEndMissionInternal();
			}
			foreach (Agent agent in this.Agents)
			{
				agent.OnRemove();
			}
			foreach (Agent agent2 in this.AllAgents)
			{
				agent2.OnDelete();
				agent2.Clear();
			}
			this.Teams.Clear();
			foreach (MissionObject missionObject in this.MissionObjects)
			{
				missionObject.OnEndMission();
			}
			this.CurrentState = Mission.State.Over;
			this.FreeResources();
			this.FinalizeMission();
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00056DAC File Offset: 0x00054FAC
		private void StopSoundEvents()
		{
			if (this._ambientSoundEvent != null)
			{
				this._ambientSoundEvent.Stop();
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00056DC4 File Offset: 0x00054FC4
		public void AddMissionBehavior(MissionBehavior missionBehavior)
		{
			this.MissionBehaviors.Add(missionBehavior);
			missionBehavior.Mission = this;
			MissionBehaviorType behaviorType = missionBehavior.BehaviorType;
			if (behaviorType != MissionBehaviorType.Logic)
			{
				if (behaviorType == MissionBehaviorType.Other)
				{
					this._otherMissionBehaviors.Add(missionBehavior);
				}
			}
			else
			{
				this.MissionLogics.Add(missionBehavior as MissionLogic);
			}
			missionBehavior.OnCreated();
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00056E1C File Offset: 0x0005501C
		public T GetMissionBehavior<T>() where T : class, IMissionBehavior
		{
			using (List<MissionBehavior>.Enumerator enumerator = this.MissionBehaviors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T result;
					if ((result = (enumerator.Current as T)) != null)
					{
						return result;
					}
				}
			}
			return default(T);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00056E8C File Offset: 0x0005508C
		public void RemoveMissionBehavior(MissionBehavior missionBehavior)
		{
			missionBehavior.OnRemoveBehavior();
			MissionBehaviorType behaviorType = missionBehavior.BehaviorType;
			if (behaviorType != MissionBehaviorType.Logic)
			{
				if (behaviorType != MissionBehaviorType.Other)
				{
					Debug.FailedAssert("Invalid behavior type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "RemoveMissionBehavior", 4547);
				}
				else
				{
					this._otherMissionBehaviors.Remove(missionBehavior);
				}
			}
			else
			{
				this.MissionLogics.Remove(missionBehavior as MissionLogic);
			}
			this.MissionBehaviors.Remove(missionBehavior);
			missionBehavior.Mission = null;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00056F00 File Offset: 0x00055100
		public void JoinEnemyTeam()
		{
			if (this.PlayerTeam == this.DefenderTeam)
			{
				Agent leader = this.AttackerTeam.Leader;
				if (leader != null)
				{
					if (this.MainAgent != null && this.MainAgent.IsActive())
					{
						this.MainAgent.Controller = Agent.ControllerType.AI;
					}
					leader.Controller = Agent.ControllerType.Player;
					this.PlayerTeam = this.AttackerTeam;
					return;
				}
			}
			else if (this.PlayerTeam == this.AttackerTeam)
			{
				Agent leader2 = this.DefenderTeam.Leader;
				if (leader2 != null)
				{
					if (this.MainAgent != null && this.MainAgent.IsActive())
					{
						this.MainAgent.Controller = Agent.ControllerType.AI;
					}
					leader2.Controller = Agent.ControllerType.Player;
					this.PlayerTeam = this.DefenderTeam;
					return;
				}
			}
			else
			{
				Debug.FailedAssert("Player is neither attacker nor defender.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "JoinEnemyTeam", 4591);
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00056FD0 File Offset: 0x000551D0
		public void OnEndMissionResult()
		{
			MissionLogic[] array = this.MissionLogics.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnBattleEnded();
			}
			this.RetreatMission();
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00057008 File Offset: 0x00055208
		public bool IsAgentInteractionAllowed()
		{
			if (this.IsAgentInteractionAllowed_AdditionalCondition != null)
			{
				Delegate[] invocationList = this.IsAgentInteractionAllowed_AdditionalCondition.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					object obj;
					if ((obj = invocationList[i].DynamicInvoke(Array.Empty<object>())) is bool && !(bool)obj)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0005705C File Offset: 0x0005525C
		public bool IsOrderGesturesEnabled()
		{
			if (this.AreOrderGesturesEnabled_AdditionalCondition != null)
			{
				Delegate[] invocationList = this.AreOrderGesturesEnabled_AdditionalCondition.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					object obj;
					if ((obj = invocationList[i].DynamicInvoke(Array.Empty<object>())) is bool && !(bool)obj)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x000570B0 File Offset: 0x000552B0
		public List<EquipmentElement> GetExtraEquipmentElementsForCharacter(BasicCharacterObject character, bool getAllEquipments = false)
		{
			List<EquipmentElement> list = new List<EquipmentElement>();
			foreach (MissionLogic missionLogic in this.MissionLogics)
			{
				List<EquipmentElement> extraEquipmentElementsForCharacter = missionLogic.GetExtraEquipmentElementsForCharacter(character, getAllEquipments);
				if (extraEquipmentElementsForCharacter != null)
				{
					list.AddRange(extraEquipmentElementsForCharacter);
				}
			}
			return list;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00057114 File Offset: 0x00055314
		private bool CheckMissionEnded()
		{
			foreach (MissionLogic missionLogic in this.MissionLogics)
			{
				MissionResult missionResult = null;
				if (missionLogic.MissionEnded(ref missionResult))
				{
					Debug.Print("CheckMissionEnded::ended", 0, Debug.DebugColor.White, 17592186044416UL);
					this.MissionResult = missionResult;
					this.MissionEnded = true;
					this.MissionResultReady(missionResult);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0005719C File Offset: 0x0005539C
		private void MissionResultReady(MissionResult missionResult)
		{
			foreach (MissionLogic missionLogic in this.MissionLogics)
			{
				missionLogic.OnMissionResultReady(missionResult);
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x000571F0 File Offset: 0x000553F0
		private void CheckMissionEnd(float currentTime)
		{
			if (!GameNetwork.IsClient && currentTime > this.NextCheckTimeEndMission)
			{
				if (this.CurrentState == Mission.State.Continuing)
				{
					if (this.MissionEnded)
					{
						return;
					}
					this.NextCheckTimeEndMission += 0.1f;
					this.CheckMissionEnded();
					if (!this.MissionEnded)
					{
						return;
					}
					this._missionEndTime = currentTime + this.MissionCloseTimeAfterFinish;
					this.NextCheckTimeEndMission += 5f;
					using (List<MissionLogic>.Enumerator enumerator = this.MissionLogics.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MissionLogic missionLogic = enumerator.Current;
							missionLogic.ShowBattleResults();
						}
						return;
					}
				}
				if (currentTime > this._missionEndTime)
				{
					this.EndMissionInternal();
					return;
				}
				this.NextCheckTimeEndMission += 5f;
				return;
			}
			else if (this.CurrentState != Mission.State.Continuing && currentTime > this.NextCheckTimeEndMission)
			{
				this.EndMissionInternal();
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x000572EC File Offset: 0x000554EC
		public bool IsPlayerCloseToAnEnemy(float distance = 5f)
		{
			if (this.MainAgent == null)
			{
				return false;
			}
			Vec3 position = this.MainAgent.Position;
			float num = distance * distance;
			AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(this, position.AsVec2, distance, false);
			while (proximityMapSearchStruct.LastFoundAgent != null)
			{
				Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
				if (lastFoundAgent != this.MainAgent && lastFoundAgent.GetAgentFlags().HasAnyFlag(AgentFlag.CanAttack) && lastFoundAgent.Position.DistanceSquared(position) <= num && (!lastFoundAgent.IsAIControlled || lastFoundAgent.CurrentWatchState == Agent.WatchState.Alarmed) && lastFoundAgent.IsEnemyOf(this.MainAgent) && !lastFoundAgent.IsRetreating())
				{
					return true;
				}
				AgentProximityMap.FindNext(this, ref proximityMapSearchStruct);
			}
			return false;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00057394 File Offset: 0x00055594
		public Vec3 GetRandomPositionAroundPoint(Vec3 center, float minDistance, float maxDistance, bool nearFirst = false)
		{
			Vec3 v = new Vec3(-1f, 0f, 0f, -1f);
			v.RotateAboutZ(6.2831855f * MBRandom.RandomFloat);
			float num = maxDistance - minDistance;
			if (nearFirst)
			{
				for (int i = 4; i > 0; i--)
				{
					int num2 = 0;
					while ((float)num2 <= 10f)
					{
						v.RotateAboutZ(1.2566371f);
						Vec3 result = center + v * (minDistance + num / (float)i);
						if (this.Scene.GetNavigationMeshForPosition(ref result))
						{
							return result;
						}
						num2++;
					}
				}
			}
			else
			{
				for (int j = 1; j < 5; j++)
				{
					int num3 = 0;
					while ((float)num3 <= 10f)
					{
						v.RotateAboutZ(1.2566371f);
						Vec3 result2 = center + v * (minDistance + num / (float)j);
						if (this.Scene.GetNavigationMeshForPosition(ref result2))
						{
							return result2;
						}
						num3++;
					}
				}
			}
			return center;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00057488 File Offset: 0x00055688
		public WorldPosition FindBestDefendingPosition(WorldPosition enemyPosition, WorldPosition defendedPosition)
		{
			return this.GetBestSlopeAngleHeightPosForDefending(enemyPosition, defendedPosition, 10, 0.5f, 4f, 0.5f, 0.70710677f, 0.1f, 1f, 0.7f, 0.5f, 1.2f, 20f, 0.6f);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000574D6 File Offset: 0x000556D6
		public WorldPosition FindPositionWithBiggestSlopeTowardsDirectionInSquare(ref WorldPosition center, float halfSize, ref WorldPosition referencePosition)
		{
			return this.GetBestSlopeTowardsDirection(ref center, halfSize, ref referencePosition);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000574E4 File Offset: 0x000556E4
		public void AddCustomMissile(Agent shooterAgent, MissionWeapon missileWeapon, Vec3 position, Vec3 direction, Mat3 orientation, float baseSpeed, float speed, bool addRigidBody, MissionObject missionObjectToIgnore, int forcedMissileIndex = -1)
		{
			WeaponData weaponData = missileWeapon.GetWeaponData(true);
			GameEntity entity;
			int num;
			if (missileWeapon.WeaponsCount == 1)
			{
				WeaponStatsData weaponStatsDataForUsage = missileWeapon.GetWeaponStatsDataForUsage(0);
				num = this.AddMissileSingleUsageAux(forcedMissileIndex, false, shooterAgent, weaponData, weaponStatsDataForUsage, 0f, ref position, ref direction, ref orientation, baseSpeed, speed, addRigidBody, (missionObjectToIgnore != null) ? missionObjectToIgnore.GameEntity : null, false, out entity);
			}
			else
			{
				WeaponStatsData[] weaponStatsData = missileWeapon.GetWeaponStatsData();
				num = this.AddMissileAux(forcedMissileIndex, false, shooterAgent, weaponData, weaponStatsData, 0f, ref position, ref direction, ref orientation, baseSpeed, speed, addRigidBody, (missionObjectToIgnore != null) ? missionObjectToIgnore.GameEntity : null, false, out entity);
			}
			weaponData.DeinitializeManagedPointers();
			Mission.Missile value = new Mission.Missile(this, entity)
			{
				ShooterAgent = shooterAgent,
				Weapon = missileWeapon,
				MissionObjectToIgnore = missionObjectToIgnore,
				Index = num
			};
			this._missiles.Add(num, value);
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new CreateMissile(num, shooterAgent.Index, EquipmentIndex.None, missileWeapon, position, direction, speed, orientation, addRigidBody, missionObjectToIgnore.Id, false));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000575EC File Offset: 0x000557EC
		[UsedImplicitly]
		[MBCallback]
		internal void OnAgentShootMissile(Agent shooterAgent, EquipmentIndex weaponIndex, Vec3 position, Vec3 velocity, Mat3 orientation, bool hasRigidBody, bool isPrimaryWeaponShot, int forcedMissileIndex)
		{
			float damageBonus = 0f;
			MissionWeapon weapon;
			if (shooterAgent.Equipment[weaponIndex].CurrentUsageItem.IsRangedWeapon && shooterAgent.Equipment[weaponIndex].CurrentUsageItem.IsConsumable)
			{
				weapon = shooterAgent.Equipment[weaponIndex];
			}
			else
			{
				weapon = shooterAgent.Equipment[weaponIndex].AmmoWeapon;
				if (shooterAgent.Equipment[weaponIndex].CurrentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.HasString))
				{
					damageBonus = (float)shooterAgent.Equipment[weaponIndex].GetModifiedThrustDamageForCurrentUsage();
				}
			}
			weapon.Amount = 1;
			WeaponData weaponData = weapon.GetWeaponData(true);
			Vec3 direction = velocity;
			float speed = direction.Normalize();
			bool flag = GameNetwork.IsClient && forcedMissileIndex == -1;
			float baseSpeed = (float)shooterAgent.Equipment[shooterAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand)].GetModifiedMissileSpeedForCurrentUsage();
			GameEntity gameEntity;
			int num;
			if (weapon.WeaponsCount == 1)
			{
				WeaponStatsData weaponStatsDataForUsage = weapon.GetWeaponStatsDataForUsage(0);
				num = this.AddMissileSingleUsageAux(forcedMissileIndex, flag, shooterAgent, weaponData, weaponStatsDataForUsage, damageBonus, ref position, ref direction, ref orientation, baseSpeed, speed, hasRigidBody, null, isPrimaryWeaponShot, out gameEntity);
			}
			else
			{
				WeaponStatsData[] weaponStatsData = weapon.GetWeaponStatsData();
				num = this.AddMissileAux(forcedMissileIndex, flag, shooterAgent, weaponData, weaponStatsData, damageBonus, ref position, ref direction, ref orientation, baseSpeed, speed, hasRigidBody, null, isPrimaryWeaponShot, out gameEntity);
			}
			weaponData.DeinitializeManagedPointers();
			if (!flag)
			{
				Mission.Missile value = new Mission.Missile(this, gameEntity)
				{
					ShooterAgent = shooterAgent,
					Weapon = weapon,
					Index = num
				};
				gameEntity.ManualInvalidate();
				this._missiles.Add(num, value);
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new CreateMissile(num, shooterAgent.Index, weaponIndex, MissionWeapon.Invalid, position, direction, speed, orientation, hasRigidBody, MissionObjectId.Invalid, isPrimaryWeaponShot));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnAgentShootMissile(shooterAgent, weaponIndex, position, velocity, orientation, hasRigidBody, forcedMissileIndex);
			}
			if (shooterAgent != null)
			{
				shooterAgent.UpdateLastRangedAttackTimeDueToAnAttack(MBCommon.GetTotalMissionTime());
			}
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00057824 File Offset: 0x00055A24
		[UsedImplicitly]
		[MBCallback]
		internal AgentState GetAgentState(Agent affectorAgent, Agent agent, DamageTypes damageType, WeaponFlags weaponFlags)
		{
			float num;
			float agentStateProbability = MissionGameModels.Current.AgentDecideKilledOrUnconsciousModel.GetAgentStateProbability(affectorAgent, agent, damageType, weaponFlags, out num);
			AgentState agentState = AgentState.None;
			bool flag = false;
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				if (missionBehavior is IAgentStateDecider)
				{
					agentState = (missionBehavior as IAgentStateDecider).GetAgentState(agent, agentStateProbability, out flag);
					break;
				}
			}
			if (agentState == AgentState.None)
			{
				float randomFloat = MBRandom.RandomFloat;
				if (randomFloat < agentStateProbability)
				{
					agentState = AgentState.Killed;
					flag = true;
				}
				else
				{
					agentState = AgentState.Unconscious;
					if (randomFloat > 1f - num)
					{
						flag = true;
					}
				}
			}
			if (flag && affectorAgent != null && affectorAgent.Team != null && agent.Team != null && affectorAgent.Team == agent.Team)
			{
				flag = false;
			}
			for (int i = 0; i < this.MissionBehaviors.Count; i++)
			{
				this.MissionBehaviors[i].OnGetAgentState(agent, flag);
			}
			return agentState;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00057924 File Offset: 0x00055B24
		public void OnAgentMount(Agent agent)
		{
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnAgentMount(agent);
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00057978 File Offset: 0x00055B78
		public void OnAgentDismount(Agent agent)
		{
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnAgentDismount(agent);
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000579CC File Offset: 0x00055BCC
		public void OnObjectUsed(Agent userAgent, UsableMissionObject usableGameObject)
		{
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnObjectUsed(userAgent, usableGameObject);
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00057A20 File Offset: 0x00055C20
		public void OnObjectStoppedBeingUsed(Agent userAgent, UsableMissionObject usableGameObject)
		{
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnObjectStoppedBeingUsed(userAgent, usableGameObject);
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00057A74 File Offset: 0x00055C74
		public void InitializeStartingBehaviors(MissionLogic[] logicBehaviors, MissionBehavior[] otherBehaviors, MissionNetwork[] networkBehaviors)
		{
			foreach (MissionLogic missionBehavior in logicBehaviors)
			{
				this.AddMissionBehavior(missionBehavior);
			}
			foreach (MissionNetwork missionBehavior2 in networkBehaviors)
			{
				this.AddMissionBehavior(missionBehavior2);
			}
			foreach (MissionBehavior missionBehavior3 in otherBehaviors)
			{
				this.AddMissionBehavior(missionBehavior3);
			}
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00057AD9 File Offset: 0x00055CD9
		public Agent GetClosestEnemyAgent(Team team, Vec3 position, float radius)
		{
			return this.GetClosestEnemyAgent(team.MBTeam, position, radius);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00057AE9 File Offset: 0x00055CE9
		public Agent GetClosestAllyAgent(Team team, Vec3 position, float radius)
		{
			return this.GetClosestAllyAgent(team.MBTeam, position, radius);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00057AF9 File Offset: 0x00055CF9
		public int GetNearbyEnemyAgentCount(Team team, Vec2 position, float radius)
		{
			return this.GetNearbyEnemyAgentCount(team.MBTeam, position, radius);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00057B0C File Offset: 0x00055D0C
		public bool HasAnyAgentsOfSideInRange(Vec3 origin, float radius, BattleSideEnum side)
		{
			Team team = (side == BattleSideEnum.Attacker) ? this.AttackerTeam : this.DefenderTeam;
			return MBAPI.IMBMission.HasAnyAgentsOfTeamAround(this.Pointer, origin, radius, team.MBTeam.Index);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00057B4C File Offset: 0x00055D4C
		private void HandleSpawnedItems()
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				int num = 0;
				for (int i = this._spawnedItemEntitiesCreatedAtRuntime.Count - 1; i >= 0; i--)
				{
					SpawnedItemEntity spawnedItemEntity = this._spawnedItemEntitiesCreatedAtRuntime[i];
					if (!spawnedItemEntity.IsRemoved)
					{
						if (!spawnedItemEntity.IsDeactivated && !spawnedItemEntity.HasUser && spawnedItemEntity.HasLifeTime && !spawnedItemEntity.HasAIMovingTo && (num > 500 || spawnedItemEntity.IsReadyToBeDeleted()))
						{
							spawnedItemEntity.GameEntity.Remove(80);
						}
						else
						{
							num++;
						}
					}
					if (spawnedItemEntity.IsRemoved)
					{
						this._spawnedItemEntitiesCreatedAtRuntime.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00057BEC File Offset: 0x00055DEC
		public bool OnMissionObjectRemoved(MissionObject missionObject, int removeReason)
		{
			if (!GameNetwork.IsClientOrReplay && missionObject.CreatedAtRuntime)
			{
				this.ReturnRuntimeMissionObjectId(missionObject.Id.Id);
				if (GameNetwork.IsServerOrRecorder)
				{
					this.RemoveDynamicallySpawnedMissionObjectInfo(missionObject.Id);
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new RemoveMissionObject(missionObject.Id));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
			this._activeMissionObjects.Remove(missionObject);
			return this._missionObjects.Remove(missionObject);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00057C64 File Offset: 0x00055E64
		public bool AgentLookingAtAgent(Agent agent1, Agent agent2)
		{
			Vec3 v = agent2.Position - agent1.Position;
			float num = v.Normalize();
			float num2 = Vec3.DotProduct(v, agent1.LookDirection);
			return num2 < 1f && num2 > 0.86f && num < 4f;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00057CB7 File Offset: 0x00055EB7
		public Agent FindAgentWithIndex(int agentId)
		{
			return this.FindAgentWithIndexAux(agentId);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00057CC0 File Offset: 0x00055EC0
		public static Agent.UnderAttackType GetUnderAttackTypeOfAgents(IEnumerable<Agent> agents, float timeLimit = 3f)
		{
			float num = float.MinValue;
			float num2 = float.MinValue;
			timeLimit += MBCommon.GetTotalMissionTime();
			foreach (Agent agent in agents)
			{
				num = MathF.Max(num, agent.LastMeleeHitTime);
				num2 = MathF.Max(num2, agent.LastRangedHitTime);
				if (num2 >= 0f && num2 < timeLimit)
				{
					return Agent.UnderAttackType.UnderRangedAttack;
				}
				if (num >= 0f && num < timeLimit)
				{
					return Agent.UnderAttackType.UnderMeleeAttack;
				}
			}
			return Agent.UnderAttackType.NotUnderAttack;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00057D58 File Offset: 0x00055F58
		public static Team GetAgentTeam(IAgentOriginBase troopOrigin, bool isPlayerSide)
		{
			if (Mission.Current == null)
			{
				Debug.FailedAssert("Mission current is null", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "GetAgentTeam", 5282);
				return null;
			}
			Team result;
			if (troopOrigin.IsUnderPlayersCommand)
			{
				result = Mission.Current.PlayerTeam;
			}
			else if (isPlayerSide)
			{
				if (Mission.Current.PlayerAllyTeam != null)
				{
					result = Mission.Current.PlayerAllyTeam;
				}
				else
				{
					result = Mission.Current.PlayerTeam;
				}
			}
			else
			{
				result = Mission.Current.PlayerEnemyTeam;
			}
			return result;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00057DD4 File Offset: 0x00055FD4
		public static float GetBattleSizeOffset(int battleSize, Path path)
		{
			if (path != null && path.NumberOfPoints > 1)
			{
				float totalLength = path.GetTotalLength();
				float normalizationFactor = 800f / totalLength;
				float battleSizeFactor = Mission.GetBattleSizeFactor(battleSize, normalizationFactor);
				return -(0.44f * battleSizeFactor);
			}
			return 0f;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00057E20 File Offset: 0x00056020
		public void OnRenderingStarted()
		{
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnRenderingStarted();
			}
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00057E70 File Offset: 0x00056070
		public static float GetBattleSizeFactor(int battleSize, float normalizationFactor)
		{
			float num = -1f;
			if (battleSize > 0)
			{
				num = 0.04f + 0.08f * MathF.Pow((float)battleSize, 0.4f);
				num *= normalizationFactor;
			}
			return MBMath.ClampFloat(num, 0.15f, 1f);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00057EB8 File Offset: 0x000560B8
		public unsafe Agent.MovementBehaviorType GetMovementTypeOfAgents(IEnumerable<Agent> agents)
		{
			float totalMissionTime = MBCommon.GetTotalMissionTime();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (Agent agent in agents)
			{
				num++;
				if (agent.IsAIControlled)
				{
					if (!agent.IsRetreating())
					{
						if (agent.Formation == null)
						{
							goto IL_60;
						}
						MovementOrder movementOrder = *agent.Formation.GetReadonlyMovementOrderReference();
						if (movementOrder.OrderType != OrderType.Retreat)
						{
							goto IL_60;
						}
					}
					num2++;
				}
				IL_60:
				if (totalMissionTime - agent.LastMeleeAttackTime < 3f)
				{
					num3++;
				}
			}
			if ((float)num2 * 1f / (float)num > 0.3f)
			{
				return Agent.MovementBehaviorType.Flee;
			}
			if (num3 > 0)
			{
				return Agent.MovementBehaviorType.Engaged;
			}
			return Agent.MovementBehaviorType.Idle;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00057F7C File Offset: 0x0005617C
		public void ShowInMissionLoadingScreen(int durationInSecond, Action onLoadingEndedAction)
		{
			this._inMissionLoadingScreenTimer = new Timer(this.CurrentTime, (float)durationInSecond, true);
			this._onLoadingEndedAction = onLoadingEndedAction;
			LoadingWindow.EnableGlobalLoadingWindow();
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00057F9E File Offset: 0x0005619E
		public bool CanAgentRout(Agent agent)
		{
			return (agent.IsRunningAway || (agent.CommonAIComponent != null && agent.CommonAIComponent.IsRetreating)) && agent.RiderAgent == null && (this.CanAgentRout_AdditionalCondition == null || this.CanAgentRout_AdditionalCondition(agent));
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00057FDD File Offset: 0x000561DD
		internal bool CanGiveDamageToAgentShield(Agent attacker, WeaponComponentData attackerWeapon, Agent defender)
		{
			return MissionGameModels.Current.AgentApplyDamageModel.CanWeaponIgnoreFriendlyFireChecks(attackerWeapon) || !this.CancelsDamageAndBlocksAttackBecauseOfNonEnemyCase(attacker, defender);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00058000 File Offset: 0x00056200
		[UsedImplicitly]
		[MBCallback]
		internal void MeleeHitCallback(ref AttackCollisionData collisionData, Agent attacker, Agent victim, GameEntity realHitEntity, ref float inOutMomentumRemaining, ref MeleeCollisionReaction colReaction, CrushThroughState crushThroughState, Vec3 blowDir, Vec3 swingDir, ref HitParticleResultData hitParticleResultData, bool crushedThroughWithoutAgentCollision)
		{
			hitParticleResultData.Reset();
			bool flag = collisionData.CollisionResult == CombatCollisionResult.Parried || collisionData.CollisionResult == CombatCollisionResult.Blocked || collisionData.CollisionResult == CombatCollisionResult.ChamberBlocked;
			if (collisionData.IsAlternativeAttack && !flag && victim != null && victim.IsHuman && collisionData.CollisionBoneIndex != -1 && (collisionData.VictimHitBodyPart == BoneBodyPartType.ArmLeft || collisionData.VictimHitBodyPart == BoneBodyPartType.ArmRight) && victim.IsHuman)
			{
				colReaction = MeleeCollisionReaction.ContinueChecking;
			}
			if (colReaction != MeleeCollisionReaction.ContinueChecking)
			{
				bool flag2 = this.CancelsDamageAndBlocksAttackBecauseOfNonEnemyCase(attacker, victim);
				bool flag3 = victim != null && victim.CurrentMortalityState == Agent.MortalityState.Invulnerable;
				bool flag4;
				if (flag2)
				{
					collisionData.AttackerStunPeriod = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunPeriodAttackerFriendlyFire);
					flag4 = true;
				}
				else
				{
					flag4 = (flag3 || (flag && !collisionData.AttackBlockedWithShield));
				}
				int affectorWeaponSlotOrMissileIndex = collisionData.AffectorWeaponSlotOrMissileIndex;
				MissionWeapon missionWeapon = (affectorWeaponSlotOrMissileIndex >= 0) ? attacker.Equipment[affectorWeaponSlotOrMissileIndex] : MissionWeapon.Invalid;
				if (crushThroughState == CrushThroughState.CrushedThisFrame && !collisionData.IsAlternativeAttack)
				{
					this.UpdateMomentumRemaining(ref inOutMomentumRemaining, default(Blow), collisionData, attacker, victim, missionWeapon, true);
				}
				WeaponComponentData weaponComponentData = null;
				CombatLogData combatLogData = default(CombatLogData);
				if (!flag4)
				{
					this.GetAttackCollisionResults(attacker, victim, realHitEntity, inOutMomentumRemaining, missionWeapon, crushThroughState > CrushThroughState.None, flag4, crushedThroughWithoutAgentCollision, ref collisionData, out weaponComponentData, out combatLogData);
					if (!collisionData.IsAlternativeAttack && attacker.IsDoingPassiveAttack && !GameNetwork.IsSessionActive && ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.ReportDamage) > 0f)
					{
						if (attacker.HasMount)
						{
							if (attacker.IsMainAgent)
							{
								InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("ui_delivered_couched_lance_damage", null).ToString(), Color.ConvertStringToColor("#AE4AD9FF")));
							}
							else if (victim != null && victim.IsMainAgent)
							{
								InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("ui_received_couched_lance_damage", null).ToString(), Color.ConvertStringToColor("#D65252FF")));
							}
						}
						else if (attacker.IsMainAgent)
						{
							InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("ui_delivered_braced_polearm_damage", null).ToString(), Color.ConvertStringToColor("#AE4AD9FF")));
						}
						else if (victim != null && victim.IsMainAgent)
						{
							InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("ui_received_braced_polearm_damage", null).ToString(), Color.ConvertStringToColor("#D65252FF")));
						}
					}
					if (collisionData.CollidedWithShieldOnBack && weaponComponentData != null && victim != null && victim.IsMainAgent)
					{
						InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("ui_hit_shield_on_back", null).ToString(), Color.ConvertStringToColor("#FFFFFFFF")));
					}
				}
				else
				{
					collisionData.InflictedDamage = 0;
					collisionData.BaseMagnitude = 0f;
					collisionData.AbsorbedByArmor = 0;
					collisionData.SelfInflictedDamage = 0;
				}
				if (!crushedThroughWithoutAgentCollision)
				{
					Blow blow = this.CreateMeleeBlow(attacker, victim, collisionData, missionWeapon, crushThroughState, blowDir, swingDir, flag4);
					if (!flag && ((victim != null && victim.IsActive()) || realHitEntity != null))
					{
						this.RegisterBlow(attacker, victim, realHitEntity, blow, ref collisionData, missionWeapon, ref combatLogData);
					}
					this.UpdateMomentumRemaining(ref inOutMomentumRemaining, blow, collisionData, attacker, victim, missionWeapon, false);
					bool isFatalHit = victim != null && victim.Health <= 0f;
					bool isShruggedOff = (blow.BlowFlag & BlowFlags.ShrugOff) > BlowFlags.None;
					this.DecideAgentHitParticles(attacker, victim, blow, collisionData, ref hitParticleResultData);
					this.DecideWeaponCollisionReaction(blow, collisionData, attacker, victim, missionWeapon, isFatalHit, isShruggedOff, out colReaction);
				}
				else
				{
					colReaction = MeleeCollisionReaction.ContinueChecking;
				}
				foreach (MissionBehavior missionBehavior in Mission.Current.MissionBehaviors)
				{
					missionBehavior.OnMeleeHit(attacker, victim, flag4, collisionData);
				}
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00058388 File Offset: 0x00056588
		private bool HitWithAnotherBone(in AttackCollisionData collisionData, Agent attacker, in MissionWeapon attackerWeapon)
		{
			MissionWeapon missionWeapon = attackerWeapon;
			int num;
			if (missionWeapon.IsEmpty || attacker == null || !attacker.IsHuman)
			{
				num = -1;
			}
			else
			{
				Monster monster = attacker.Monster;
				missionWeapon = attackerWeapon;
				num = (int)monster.GetBoneToAttachForItemFlags(missionWeapon.Item.ItemFlags);
			}
			int weaponAttachBoneIndex = num;
			return MissionCombatMechanicsHelper.IsCollisionBoneDifferentThanWeaponAttachBone(collisionData, weaponAttachBoneIndex);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x000583DC File Offset: 0x000565DC
		private void DecideAgentHitParticles(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData, ref HitParticleResultData hprd)
		{
			if (victim != null && (blow.InflictedDamage > 0 || victim.Health <= 0f))
			{
				BlowWeaponRecord weaponRecord = blow.WeaponRecord;
				bool flag;
				if (weaponRecord.HasWeapon() && !blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.NoBlood))
				{
					AttackCollisionData attackCollisionData = collisionData;
					flag = attackCollisionData.IsAlternativeAttack;
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					MissionGameModels.Current.DamageParticleModel.GetMeleeAttackSweatParticles(attacker, victim, blow, collisionData, out hprd);
					return;
				}
				MissionGameModels.Current.DamageParticleModel.GetMeleeAttackBloodParticles(attacker, victim, blow, collisionData, out hprd);
			}
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0005846C File Offset: 0x0005666C
		private void DecideWeaponCollisionReaction(Blow registeredBlow, in AttackCollisionData collisionData, Agent attacker, Agent defender, in MissionWeapon attackerWeapon, bool isFatalHit, bool isShruggedOff, out MeleeCollisionReaction colReaction)
		{
			AttackCollisionData attackCollisionData = collisionData;
			if (attackCollisionData.IsColliderAgent)
			{
				attackCollisionData = collisionData;
				if (attackCollisionData.StrikeType == 1)
				{
					attackCollisionData = collisionData;
					if (attackCollisionData.CollisionHitResultFlags.HasAnyFlag(CombatHitResultFlags.HitWithStartOfTheAnimation))
					{
						colReaction = MeleeCollisionReaction.Staggered;
						return;
					}
				}
			}
			attackCollisionData = collisionData;
			if (!attackCollisionData.IsColliderAgent)
			{
				attackCollisionData = collisionData;
				if (attackCollisionData.PhysicsMaterialIndex != -1)
				{
					attackCollisionData = collisionData;
					if (PhysicsMaterial.GetFromIndex(attackCollisionData.PhysicsMaterialIndex).GetFlags().HasAnyFlag(PhysicsMaterialFlags.AttacksCanPassThrough))
					{
						colReaction = MeleeCollisionReaction.SlicedThrough;
						return;
					}
				}
			}
			attackCollisionData = collisionData;
			if (!attackCollisionData.IsColliderAgent || registeredBlow.InflictedDamage <= 0)
			{
				colReaction = MeleeCollisionReaction.Bounced;
				return;
			}
			attackCollisionData = collisionData;
			if (attackCollisionData.StrikeType == 1 && attacker.IsDoingPassiveAttack)
			{
				colReaction = MissionGameModels.Current.AgentApplyDamageModel.DecidePassiveAttackCollisionReaction(attacker, defender, isFatalHit);
				return;
			}
			if (this.HitWithAnotherBone(collisionData, attacker, attackerWeapon))
			{
				colReaction = MeleeCollisionReaction.Bounced;
				return;
			}
			MissionWeapon missionWeapon = attackerWeapon;
			WeaponClass weaponClass;
			if (missionWeapon.IsEmpty)
			{
				weaponClass = WeaponClass.Undefined;
			}
			else
			{
				missionWeapon = attackerWeapon;
				weaponClass = missionWeapon.CurrentUsageItem.WeaponClass;
			}
			WeaponClass weaponClass2 = weaponClass;
			missionWeapon = attackerWeapon;
			if (missionWeapon.IsEmpty || isFatalHit || !isShruggedOff)
			{
				missionWeapon = attackerWeapon;
				if (missionWeapon.IsEmpty && defender != null && defender.IsHuman)
				{
					attackCollisionData = collisionData;
					if (!attackCollisionData.IsAlternativeAttack)
					{
						attackCollisionData = collisionData;
						if (attackCollisionData.VictimHitBodyPart == BoneBodyPartType.Chest)
						{
							goto IL_1B2;
						}
						attackCollisionData = collisionData;
						if (attackCollisionData.VictimHitBodyPart == BoneBodyPartType.ShoulderLeft)
						{
							goto IL_1B2;
						}
						attackCollisionData = collisionData;
						if (attackCollisionData.VictimHitBodyPart == BoneBodyPartType.ShoulderRight)
						{
							goto IL_1B2;
						}
						attackCollisionData = collisionData;
						if (attackCollisionData.VictimHitBodyPart == BoneBodyPartType.Abdomen)
						{
							goto IL_1B2;
						}
						attackCollisionData = collisionData;
						if (attackCollisionData.VictimHitBodyPart == BoneBodyPartType.Legs)
						{
							goto IL_1B2;
						}
					}
				}
				if ((weaponClass2 != WeaponClass.OneHandedAxe && weaponClass2 != WeaponClass.TwoHandedAxe) || isFatalHit || (float)collisionData.InflictedDamage >= defender.HealthLimit * 0.5f)
				{
					missionWeapon = attackerWeapon;
					if (missionWeapon.IsEmpty)
					{
						attackCollisionData = collisionData;
						if (!attackCollisionData.IsAlternativeAttack)
						{
							attackCollisionData = collisionData;
							if (attackCollisionData.AttackDirection == Agent.UsageDirection.AttackUp)
							{
								goto IL_257;
							}
						}
					}
					attackCollisionData = collisionData;
					if (attackCollisionData.ThrustTipHit)
					{
						attackCollisionData = collisionData;
						if (attackCollisionData.DamageType == 1)
						{
							missionWeapon = attackerWeapon;
							if (!missionWeapon.IsEmpty)
							{
								attackCollisionData = collisionData;
								if (defender.CanThrustAttackStickToBone(attackCollisionData.VictimHitBodyPart))
								{
									goto IL_257;
								}
							}
						}
					}
					colReaction = MeleeCollisionReaction.SlicedThrough;
					goto IL_261;
				}
				IL_257:
				colReaction = MeleeCollisionReaction.Stuck;
				goto IL_261;
			}
			IL_1B2:
			colReaction = MeleeCollisionReaction.Bounced;
			IL_261:
			attackCollisionData = collisionData;
			if (!attackCollisionData.AttackBlockedWithShield)
			{
				attackCollisionData = collisionData;
				if (!attackCollisionData.CollidedWithShieldOnBack)
				{
					return;
				}
			}
			if (colReaction == MeleeCollisionReaction.SlicedThrough)
			{
				colReaction = MeleeCollisionReaction.Bounced;
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00058704 File Offset: 0x00056904
		private void RegisterBlow(Agent attacker, Agent victim, GameEntity realHitEntity, Blow b, ref AttackCollisionData collisionData, in MissionWeapon attackerWeapon, ref CombatLogData combatLogData)
		{
			b.VictimBodyPart = collisionData.VictimHitBodyPart;
			if (!collisionData.AttackBlockedWithShield)
			{
				if (collisionData.IsColliderAgent)
				{
					if (b.SelfInflictedDamage > 0 && attacker != null && attacker.IsActive() && attacker.IsFriendOf(victim))
					{
						Blow blow;
						AttackCollisionData attackCollisionData;
						attacker.CreateBlowFromBlowAsReflection(b, collisionData, out blow, out attackCollisionData);
						if (victim.IsMount && attacker.MountAgent != null)
						{
							attacker.MountAgent.RegisterBlow(blow, attackCollisionData);
						}
						else
						{
							attacker.RegisterBlow(blow, attackCollisionData);
						}
					}
					if (b.InflictedDamage > 0)
					{
						combatLogData.IsFatalDamage = (victim != null && victim.Health - (float)b.InflictedDamage < 1f);
						combatLogData.InflictedDamage = b.InflictedDamage - combatLogData.ModifiedDamage;
						this.PrintAttackCollisionResults(attacker, victim, realHitEntity, ref collisionData, ref combatLogData);
					}
					victim.RegisterBlow(b, collisionData);
				}
				else if (collisionData.EntityExists)
				{
					MissionWeapon missionWeapon = b.IsMissile ? this._missiles[b.WeaponRecord.AffectorWeaponSlotOrMissileIndex].Weapon : ((attacker != null && b.WeaponRecord.HasWeapon()) ? attacker.Equipment[b.WeaponRecord.AffectorWeaponSlotOrMissileIndex] : MissionWeapon.Invalid);
					this.OnEntityHit(realHitEntity, attacker, b.InflictedDamage, (DamageTypes)collisionData.DamageType, b.GlobalPosition, b.SwingDirection, missionWeapon);
					if (attacker != null && b.SelfInflictedDamage > 0)
					{
						Blow blow2;
						AttackCollisionData attackCollisionData2;
						attacker.CreateBlowFromBlowAsReflection(b, collisionData, out blow2, out attackCollisionData2);
						attacker.RegisterBlow(blow2, attackCollisionData2);
					}
				}
			}
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnRegisterBlow(attacker, victim, realHitEntity, b, ref collisionData, attackerWeapon);
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000588E8 File Offset: 0x00056AE8
		private void UpdateMomentumRemaining(ref float momentumRemaining, Blow b, in AttackCollisionData collisionData, Agent attacker, Agent victim, in MissionWeapon attackerWeapon, bool isCrushThrough)
		{
			float num = momentumRemaining;
			momentumRemaining = 0f;
			if (isCrushThrough)
			{
				momentumRemaining = num * 0.3f;
				return;
			}
			if (b.InflictedDamage > 0)
			{
				AttackCollisionData attackCollisionData = collisionData;
				if (!attackCollisionData.AttackBlockedWithShield)
				{
					attackCollisionData = collisionData;
					if (!attackCollisionData.CollidedWithShieldOnBack)
					{
						attackCollisionData = collisionData;
						if (attackCollisionData.IsColliderAgent)
						{
							attackCollisionData = collisionData;
							if (!attackCollisionData.IsHorseCharge)
							{
								if (attacker != null && attacker.IsDoingPassiveAttack)
								{
									momentumRemaining = num * 0.5f;
									return;
								}
								if (!this.HitWithAnotherBone(collisionData, attacker, attackerWeapon))
								{
									MissionWeapon missionWeapon = attackerWeapon;
									if (!missionWeapon.IsEmpty && b.StrikeType != StrikeType.Thrust)
									{
										missionWeapon = attackerWeapon;
										if (!missionWeapon.IsEmpty)
										{
											missionWeapon = attackerWeapon;
											if (missionWeapon.CurrentUsageItem.CanHitMultipleTargets)
											{
												momentumRemaining = num * (1f - b.AbsorbedByArmor / (float)b.InflictedDamage);
												momentumRemaining *= 0.5f;
												if (momentumRemaining < 0.25f)
												{
													momentumRemaining = 0f;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000589FC File Offset: 0x00056BFC
		private Blow CreateMissileBlow(Agent attackerAgent, in AttackCollisionData collisionData, in MissionWeapon attackerWeapon, Vec3 missilePosition, Vec3 missileStartingPosition)
		{
			Blow blow = new Blow((attackerAgent != null) ? attackerAgent.Index : -1);
			MissionWeapon missionWeapon = attackerWeapon;
			blow.BlowFlag = (missionWeapon.CurrentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.CanKnockDown) ? BlowFlags.KnockDown : BlowFlags.None);
			AttackCollisionData attackCollisionData = collisionData;
			blow.Direction = attackCollisionData.MissileVelocity.NormalizedCopy();
			blow.SwingDirection = blow.Direction;
			attackCollisionData = collisionData;
			blow.GlobalPosition = attackCollisionData.CollisionGlobalPosition;
			attackCollisionData = collisionData;
			blow.BoneIndex = attackCollisionData.CollisionBoneIndex;
			attackCollisionData = collisionData;
			blow.StrikeType = (StrikeType)attackCollisionData.StrikeType;
			attackCollisionData = collisionData;
			blow.DamageType = (DamageTypes)attackCollisionData.DamageType;
			attackCollisionData = collisionData;
			blow.VictimBodyPart = attackCollisionData.VictimHitBodyPart;
			sbyte b;
			if (attackerAgent == null)
			{
				b = -1;
			}
			else
			{
				Monster monster = attackerAgent.Monster;
				missionWeapon = attackerWeapon;
				b = monster.GetBoneToAttachForItemFlags(missionWeapon.Item.ItemFlags);
			}
			sbyte b2 = b;
			missionWeapon = attackerWeapon;
			ItemObject item = missionWeapon.Item;
			missionWeapon = attackerWeapon;
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			attackCollisionData = collisionData;
			int affectorWeaponSlotOrMissileIndex = attackCollisionData.AffectorWeaponSlotOrMissileIndex;
			sbyte weaponAttachBoneIndex = b2;
			attackCollisionData = collisionData;
			blow.WeaponRecord.FillAsMissileBlow(item, currentUsageItem, affectorWeaponSlotOrMissileIndex, weaponAttachBoneIndex, missileStartingPosition, missilePosition, attackCollisionData.MissileVelocity);
			blow.BaseMagnitude = collisionData.BaseMagnitude;
			blow.MovementSpeedDamageModifier = collisionData.MovementSpeedDamageModifier;
			blow.AbsorbedByArmor = (float)collisionData.AbsorbedByArmor;
			blow.InflictedDamage = collisionData.InflictedDamage;
			blow.SelfInflictedDamage = collisionData.SelfInflictedDamage;
			blow.DamageCalculated = true;
			return blow;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00058B98 File Offset: 0x00056D98
		[UsedImplicitly]
		[MBCallback]
		internal float OnAgentHitBlocked(Agent affectedAgent, Agent affectorAgent, ref AttackCollisionData collisionData, Vec3 blowDirection, Vec3 swingDirection, bool isMissile)
		{
			Blow blow;
			if (isMissile)
			{
				Mission.Missile missile = this._missiles[collisionData.AffectorWeaponSlotOrMissileIndex];
				MissionWeapon weapon = missile.Weapon;
				blow = this.CreateMissileBlow(affectorAgent, collisionData, weapon, missile.GetPosition(), collisionData.MissileStartingPosition);
			}
			else
			{
				int affectorWeaponSlotOrMissileIndex = collisionData.AffectorWeaponSlotOrMissileIndex;
				MissionWeapon missionWeapon = (affectorWeaponSlotOrMissileIndex >= 0) ? affectorAgent.Equipment[affectorWeaponSlotOrMissileIndex] : MissionWeapon.Invalid;
				blow = this.CreateMeleeBlow(affectorAgent, affectedAgent, collisionData, missionWeapon, CrushThroughState.None, blowDirection, swingDirection, true);
			}
			return this.OnAgentHit(affectedAgent, affectorAgent, blow, collisionData, true, 0f);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00058C20 File Offset: 0x00056E20
		private Blow CreateMeleeBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, in MissionWeapon attackerWeapon, CrushThroughState crushThroughState, Vec3 blowDirection, Vec3 swingDirection, bool cancelDamage)
		{
			Blow blow = new Blow(attackerAgent.Index);
			AttackCollisionData attackCollisionData = collisionData;
			blow.VictimBodyPart = attackCollisionData.VictimHitBodyPart;
			bool flag = this.HitWithAnotherBone(collisionData, attackerAgent, attackerWeapon);
			attackCollisionData = collisionData;
			MissionWeapon missionWeapon;
			if (attackCollisionData.IsAlternativeAttack)
			{
				missionWeapon = attackerWeapon;
				blow.AttackType = (missionWeapon.IsEmpty ? AgentAttackType.Kick : AgentAttackType.Bash);
			}
			else
			{
				blow.AttackType = AgentAttackType.Standard;
			}
			missionWeapon = attackerWeapon;
			sbyte b;
			if (!missionWeapon.IsEmpty)
			{
				Monster monster = attackerAgent.Monster;
				missionWeapon = attackerWeapon;
				b = monster.GetBoneToAttachForItemFlags(missionWeapon.Item.ItemFlags);
			}
			else
			{
				b = -1;
			}
			sbyte weaponAttachBoneIndex = b;
			missionWeapon = attackerWeapon;
			ItemObject item = missionWeapon.Item;
			missionWeapon = attackerWeapon;
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			attackCollisionData = collisionData;
			blow.WeaponRecord.FillAsMeleeBlow(item, currentUsageItem, attackCollisionData.AffectorWeaponSlotOrMissileIndex, weaponAttachBoneIndex);
			attackCollisionData = collisionData;
			blow.StrikeType = (StrikeType)attackCollisionData.StrikeType;
			missionWeapon = attackerWeapon;
			DamageTypes damageType;
			if (!missionWeapon.IsEmpty && !flag)
			{
				attackCollisionData = collisionData;
				if (!attackCollisionData.IsAlternativeAttack)
				{
					attackCollisionData = collisionData;
					damageType = (DamageTypes)attackCollisionData.DamageType;
					goto IL_122;
				}
			}
			damageType = DamageTypes.Blunt;
			IL_122:
			blow.DamageType = damageType;
			attackCollisionData = collisionData;
			blow.NoIgnore = attackCollisionData.IsAlternativeAttack;
			attackCollisionData = collisionData;
			blow.AttackerStunPeriod = attackCollisionData.AttackerStunPeriod;
			attackCollisionData = collisionData;
			blow.DefenderStunPeriod = attackCollisionData.DefenderStunPeriod;
			blow.BlowFlag = BlowFlags.None;
			attackCollisionData = collisionData;
			blow.GlobalPosition = attackCollisionData.CollisionGlobalPosition;
			attackCollisionData = collisionData;
			blow.BoneIndex = attackCollisionData.CollisionBoneIndex;
			blow.Direction = blowDirection;
			blow.SwingDirection = swingDirection;
			if (cancelDamage)
			{
				blow.BaseMagnitude = 0f;
				blow.MovementSpeedDamageModifier = 0f;
				blow.InflictedDamage = 0;
				blow.SelfInflictedDamage = 0;
				blow.AbsorbedByArmor = 0f;
			}
			else
			{
				blow.BaseMagnitude = collisionData.BaseMagnitude;
				blow.MovementSpeedDamageModifier = collisionData.MovementSpeedDamageModifier;
				blow.InflictedDamage = collisionData.InflictedDamage;
				blow.SelfInflictedDamage = collisionData.SelfInflictedDamage;
				blow.AbsorbedByArmor = (float)collisionData.AbsorbedByArmor;
			}
			blow.DamageCalculated = true;
			if (crushThroughState != CrushThroughState.None)
			{
				blow.BlowFlag |= BlowFlags.CrushThrough;
			}
			if (blow.StrikeType == StrikeType.Thrust)
			{
				attackCollisionData = collisionData;
				if (!attackCollisionData.ThrustTipHit)
				{
					blow.BlowFlag |= BlowFlags.NonTipThrust;
				}
			}
			attackCollisionData = collisionData;
			if (attackCollisionData.IsColliderAgent)
			{
				if (MissionGameModels.Current.AgentApplyDamageModel.DecideAgentShrugOffBlow(victimAgent, collisionData, blow))
				{
					blow.BlowFlag |= BlowFlags.ShrugOff;
				}
				if (victimAgent.IsHuman)
				{
					Agent mountAgent = victimAgent.MountAgent;
					if (mountAgent != null)
					{
						if (mountAgent.RiderAgent == victimAgent)
						{
							AgentApplyDamageModel agentApplyDamageModel = MissionGameModels.Current.AgentApplyDamageModel;
							missionWeapon = attackerWeapon;
							if (agentApplyDamageModel.DecideAgentDismountedByBlow(attackerAgent, victimAgent, collisionData, missionWeapon.CurrentUsageItem, blow))
							{
								blow.BlowFlag |= BlowFlags.CanDismount;
							}
						}
					}
					else
					{
						AgentApplyDamageModel agentApplyDamageModel2 = MissionGameModels.Current.AgentApplyDamageModel;
						missionWeapon = attackerWeapon;
						if (agentApplyDamageModel2.DecideAgentKnockedBackByBlow(attackerAgent, victimAgent, collisionData, missionWeapon.CurrentUsageItem, blow))
						{
							blow.BlowFlag |= BlowFlags.KnockBack;
						}
						AgentApplyDamageModel agentApplyDamageModel3 = MissionGameModels.Current.AgentApplyDamageModel;
						missionWeapon = attackerWeapon;
						if (agentApplyDamageModel3.DecideAgentKnockedDownByBlow(attackerAgent, victimAgent, collisionData, missionWeapon.CurrentUsageItem, blow))
						{
							blow.BlowFlag |= BlowFlags.KnockDown;
						}
					}
				}
				else if (victimAgent.IsMount)
				{
					AgentApplyDamageModel agentApplyDamageModel4 = MissionGameModels.Current.AgentApplyDamageModel;
					missionWeapon = attackerWeapon;
					if (agentApplyDamageModel4.DecideMountRearedByBlow(attackerAgent, victimAgent, collisionData, missionWeapon.CurrentUsageItem, blow))
					{
						blow.BlowFlag |= BlowFlags.MakesRear;
					}
				}
			}
			return blow;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00058FE0 File Offset: 0x000571E0
		internal float OnAgentHit(Agent affectedAgent, Agent affectorAgent, in Blow b, in AttackCollisionData collisionData, bool isBlocked, float damagedHp)
		{
			float shotDifficulty = -1f;
			bool isSiegeEngineHit = false;
			int affectorWeaponSlotOrMissileIndex = b.WeaponRecord.AffectorWeaponSlotOrMissileIndex;
			Blow blow = b;
			bool isMissile = blow.IsMissile;
			int inflictedDamage = b.InflictedDamage;
			blow = b;
			float hitDistance = blow.IsMissile ? (b.GlobalPosition - b.WeaponRecord.StartingPosition).Length : 0f;
			MissionWeapon missionWeapon;
			if (isMissile)
			{
				missionWeapon = this._missiles[affectorWeaponSlotOrMissileIndex].Weapon;
				isSiegeEngineHit = (this._missiles[affectorWeaponSlotOrMissileIndex].MissionObjectToIgnore != null);
			}
			else
			{
				missionWeapon = ((affectorAgent != null && affectorWeaponSlotOrMissileIndex >= 0) ? affectorAgent.Equipment[affectorWeaponSlotOrMissileIndex] : MissionWeapon.Invalid);
			}
			if (affectorAgent != null && isMissile)
			{
				shotDifficulty = this.GetShootDifficulty(affectedAgent, affectorAgent, b.VictimBodyPart == BoneBodyPartType.Head);
			}
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnAgentHit(affectedAgent, affectorAgent, missionWeapon, b, collisionData);
				missionBehavior.OnScoreHit(affectedAgent, affectorAgent, missionWeapon.CurrentUsageItem, isBlocked, isSiegeEngineHit, b, collisionData, damagedHp, hitDistance, shotDifficulty);
			}
			foreach (AgentComponent agentComponent in affectedAgent.Components)
			{
				agentComponent.OnHit(affectorAgent, inflictedDamage, missionWeapon);
			}
			affectedAgent.CheckToDropFlaggedItem();
			return (float)inflictedDamage;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0005916C File Offset: 0x0005736C
		[UsedImplicitly]
		[MBCallback]
		internal void MissileAreaDamageCallback(ref AttackCollisionData collisionDataInput, ref Blow blowInput, Agent alreadyDamagedAgent, Agent shooterAgent, bool isBigExplosion)
		{
			float num = isBigExplosion ? 2.8f : 1.2f;
			float num2 = isBigExplosion ? 1.6f : 1f;
			float num3 = 1f;
			if (collisionDataInput.MissileVelocity.LengthSquared < 484f)
			{
				num2 *= 0.8f;
				num3 = 0.5f;
			}
			AttackCollisionData attackCollisionData = collisionDataInput;
			blowInput.VictimBodyPart = collisionDataInput.VictimHitBodyPart;
			List<Agent> list = new List<Agent>();
			AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(this, blowInput.GlobalPosition.AsVec2, num, true);
			while (proximityMapSearchStruct.LastFoundAgent != null)
			{
				Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
				if (lastFoundAgent.CurrentMortalityState != Agent.MortalityState.Invulnerable && lastFoundAgent != shooterAgent && lastFoundAgent != alreadyDamagedAgent)
				{
					list.Add(lastFoundAgent);
				}
				AgentProximityMap.FindNext(this, ref proximityMapSearchStruct);
			}
			foreach (Agent agent in list)
			{
				Blow blow = blowInput;
				blow.DamageCalculated = false;
				attackCollisionData = collisionDataInput;
				float num4 = float.MaxValue;
				sbyte collisionBoneIndexForAreaDamage = -1;
				Skeleton skeleton = agent.AgentVisuals.GetSkeleton();
				sbyte boneCount = skeleton.GetBoneCount();
				MatrixFrame globalFrame = agent.AgentVisuals.GetGlobalFrame();
				for (sbyte b = 0; b < boneCount; b += 1)
				{
					float num5 = globalFrame.TransformToParent(skeleton.GetBoneEntitialFrame(b).origin).DistanceSquared(blowInput.GlobalPosition);
					if (num5 < num4)
					{
						collisionBoneIndexForAreaDamage = b;
						num4 = num5;
					}
				}
				if (num4 <= num * num)
				{
					float num6 = MathF.Sqrt(num4);
					float num7 = 1f;
					if (num6 > num2)
					{
						float num8 = MBMath.Lerp(1f, 3f, (num6 - num2) / (num - num2), 1E-05f);
						num7 = 1f / (num8 * num8);
					}
					num7 *= num3;
					attackCollisionData.SetCollisionBoneIndexForAreaDamage(collisionBoneIndexForAreaDamage);
					MissionWeapon weapon = this._missiles[attackCollisionData.AffectorWeaponSlotOrMissileIndex].Weapon;
					WeaponComponentData weaponComponentData;
					CombatLogData combatLogData;
					this.GetAttackCollisionResults(shooterAgent, agent, null, 1f, weapon, false, false, false, ref attackCollisionData, out weaponComponentData, out combatLogData);
					blow.BaseMagnitude = attackCollisionData.BaseMagnitude;
					blow.MovementSpeedDamageModifier = attackCollisionData.MovementSpeedDamageModifier;
					blow.InflictedDamage = attackCollisionData.InflictedDamage;
					blow.SelfInflictedDamage = attackCollisionData.SelfInflictedDamage;
					blow.AbsorbedByArmor = (float)attackCollisionData.AbsorbedByArmor;
					blow.DamageCalculated = true;
					blow.InflictedDamage = MathF.Round((float)blow.InflictedDamage * num7);
					blow.SelfInflictedDamage = MathF.Round((float)blow.SelfInflictedDamage * num7);
					combatLogData.ModifiedDamage = MathF.Round((float)combatLogData.ModifiedDamage * num7);
					this.RegisterBlow(shooterAgent, agent, null, blow, ref attackCollisionData, weapon, ref combatLogData);
				}
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00059448 File Offset: 0x00057648
		[UsedImplicitly]
		[MBCallback]
		internal void OnMissileRemoved(int missileIndex)
		{
			this._missiles.Remove(missileIndex);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00059458 File Offset: 0x00057658
		[UsedImplicitly]
		[MBCallback]
		internal bool MissileHitCallback(out int extraHitParticleIndex, ref AttackCollisionData collisionData, Vec3 missileStartingPosition, Vec3 missilePosition, Vec3 missileAngularVelocity, Vec3 movementVelocity, MatrixFrame attachGlobalFrame, MatrixFrame affectedShieldGlobalFrame, int numDamagedAgents, Agent attacker, Agent victim, GameEntity hitEntity)
		{
			Mission.Missile missile = this._missiles[collisionData.AffectorWeaponSlotOrMissileIndex];
			MissionWeapon weapon = missile.Weapon;
			WeaponFlags weaponFlags = weapon.CurrentUsageItem.WeaponFlags;
			float num = 1f;
			WeaponComponentData weaponComponentData = null;
			MissionGameModels.Current.AgentApplyDamageModel.DecideMissileWeaponFlags(attacker, missile.Weapon, ref weaponFlags);
			extraHitParticleIndex = -1;
			Mission.MissileCollisionReaction missileCollisionReaction = Mission.MissileCollisionReaction.Invalid;
			bool flag = !GameNetwork.IsSessionActive;
			bool missileHasPhysics = collisionData.MissileHasPhysics;
			PhysicsMaterial fromIndex = PhysicsMaterial.GetFromIndex(collisionData.PhysicsMaterialIndex);
			object obj = fromIndex.IsValid ? fromIndex.GetFlags() : PhysicsMaterialFlags.None;
			bool flag2 = (weaponFlags & WeaponFlags.AmmoSticksWhenShot) > (WeaponFlags)0UL;
			object obj2 = obj;
			bool flag3 = (obj2 & 1) == 0;
			bool flag4 = (obj2 & 8) != 0;
			MissionObject missionObject = null;
			if (victim == null && hitEntity != null)
			{
				GameEntity gameEntity = hitEntity;
				do
				{
					missionObject = gameEntity.GetFirstScriptOfType<MissionObject>();
					gameEntity = gameEntity.Parent;
				}
				while (missionObject == null && gameEntity != null);
				hitEntity = ((missionObject != null) ? missionObject.GameEntity : null);
			}
			Mission.MissileCollisionReaction missileCollisionReaction2;
			if (flag4)
			{
				missileCollisionReaction2 = Mission.MissileCollisionReaction.PassThrough;
			}
			else if (weaponFlags.HasAnyFlag(WeaponFlags.Burning))
			{
				missileCollisionReaction2 = Mission.MissileCollisionReaction.BecomeInvisible;
			}
			else if (!flag3 || !flag2)
			{
				missileCollisionReaction2 = Mission.MissileCollisionReaction.BounceBack;
			}
			else
			{
				missileCollisionReaction2 = Mission.MissileCollisionReaction.Stick;
			}
			bool flag5 = false;
			bool flag6 = victim != null && victim.CurrentMortalityState == Agent.MortalityState.Invulnerable;
			if (collisionData.MissileGoneUnderWater || collisionData.MissileGoneOutOfBorder || flag6)
			{
				missileCollisionReaction = Mission.MissileCollisionReaction.BecomeInvisible;
			}
			else if (victim == null)
			{
				if (hitEntity != null)
				{
					CombatLogData combatLogData;
					this.GetAttackCollisionResults(attacker, victim, hitEntity, num, weapon, false, false, false, ref collisionData, out weaponComponentData, out combatLogData);
					Blow b = this.CreateMissileBlow(attacker, collisionData, weapon, missilePosition, missileStartingPosition);
					this.RegisterBlow(attacker, null, hitEntity, b, ref collisionData, weapon, ref combatLogData);
				}
				missileCollisionReaction = missileCollisionReaction2;
			}
			else if (collisionData.AttackBlockedWithShield)
			{
				CombatLogData combatLogData;
				this.GetAttackCollisionResults(attacker, victim, hitEntity, num, weapon, false, false, false, ref collisionData, out weaponComponentData, out combatLogData);
				if (!collisionData.IsShieldBroken)
				{
					this.MakeSound(ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeStone, collisionData.CollisionGlobalPosition, false, false, -1, -1);
				}
				bool flag7 = false;
				if (weaponFlags.HasAnyFlag(WeaponFlags.CanPenetrateShield))
				{
					if (!collisionData.IsShieldBroken)
					{
						EquipmentIndex wieldedItemIndex = victim.GetWieldedItemIndex(Agent.HandIndex.OffHand);
						if ((float)collisionData.InflictedDamage > ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.ShieldPenetrationOffset) + ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.ShieldPenetrationFactor) * (float)victim.Equipment[wieldedItemIndex].GetGetModifiedArmorForCurrentUsage())
						{
							flag7 = true;
						}
					}
					else
					{
						flag7 = true;
					}
				}
				if (flag7)
				{
					victim.MakeVoice(SkinVoiceManager.VoiceType.Pain, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
					num *= 0.4f + MBRandom.RandomFloat * 0.2f;
					missileCollisionReaction = Mission.MissileCollisionReaction.PassThrough;
				}
				else
				{
					missileCollisionReaction = (collisionData.IsShieldBroken ? Mission.MissileCollisionReaction.BecomeInvisible : missileCollisionReaction2);
				}
			}
			else if (collisionData.MissileBlockedWithWeapon)
			{
				CombatLogData combatLogData;
				this.GetAttackCollisionResults(attacker, victim, hitEntity, num, weapon, false, false, false, ref collisionData, out weaponComponentData, out combatLogData);
				missileCollisionReaction = Mission.MissileCollisionReaction.BounceBack;
			}
			else
			{
				if (attacker != null && attacker.IsFriendOf(victim))
				{
					if (this.ForceNoFriendlyFire)
					{
						flag5 = true;
					}
					else if (!missileHasPhysics)
					{
						if (flag)
						{
							if (attacker.Controller == Agent.ControllerType.AI)
							{
								flag5 = true;
							}
						}
						else if ((MultiplayerOptions.OptionType.FriendlyFireDamageRangedFriendPercent.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0 && MultiplayerOptions.OptionType.FriendlyFireDamageRangedSelfPercent.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0) || this.Mode == MissionMode.Duel)
						{
							flag5 = true;
						}
					}
				}
				else if (victim.IsHuman && attacker != null && !attacker.IsEnemyOf(victim))
				{
					flag5 = true;
				}
				else if (flag && attacker != null && attacker.Controller == Agent.ControllerType.AI && victim.RiderAgent != null && attacker.IsFriendOf(victim.RiderAgent))
				{
					flag5 = true;
				}
				if (flag5)
				{
					if (flag && attacker != null && attacker == Agent.Main && attacker.IsFriendOf(victim))
					{
						InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("ui_you_hit_a_friendly_troop", null).ToString(), Color.ConvertStringToColor("#D65252FF")));
					}
					missileCollisionReaction = Mission.MissileCollisionReaction.BecomeInvisible;
				}
				else
				{
					bool flag8 = (weaponFlags & WeaponFlags.MultiplePenetration) > (WeaponFlags)0UL;
					CombatLogData combatLogData;
					this.GetAttackCollisionResults(attacker, victim, null, num, weapon, false, false, false, ref collisionData, out weaponComponentData, out combatLogData);
					Blow blow = this.CreateMissileBlow(attacker, collisionData, weapon, missilePosition, missileStartingPosition);
					if (collisionData.IsColliderAgent && flag8 && numDamagedAgents > 0)
					{
						blow.InflictedDamage /= numDamagedAgents;
						blow.SelfInflictedDamage /= numDamagedAgents;
						combatLogData.InflictedDamage = blow.InflictedDamage - combatLogData.ModifiedDamage;
					}
					if (collisionData.IsColliderAgent)
					{
						if (MissionGameModels.Current.AgentApplyDamageModel.DecideAgentShrugOffBlow(victim, collisionData, blow))
						{
							blow.BlowFlag |= BlowFlags.ShrugOff;
						}
						else if (victim.IsHuman)
						{
							Agent mountAgent = victim.MountAgent;
							if (mountAgent != null)
							{
								if (mountAgent.RiderAgent == victim && MissionGameModels.Current.AgentApplyDamageModel.DecideAgentDismountedByBlow(attacker, victim, collisionData, weapon.CurrentUsageItem, blow))
								{
									blow.BlowFlag |= BlowFlags.CanDismount;
								}
							}
							else
							{
								if (MissionGameModels.Current.AgentApplyDamageModel.DecideAgentKnockedBackByBlow(attacker, victim, collisionData, weapon.CurrentUsageItem, blow))
								{
									blow.BlowFlag |= BlowFlags.KnockBack;
								}
								if (MissionGameModels.Current.AgentApplyDamageModel.DecideAgentKnockedDownByBlow(attacker, victim, collisionData, weapon.CurrentUsageItem, blow))
								{
									blow.BlowFlag |= BlowFlags.KnockDown;
								}
							}
						}
					}
					if (victim.State == AgentState.Active)
					{
						this.RegisterBlow(attacker, victim, null, blow, ref collisionData, weapon, ref combatLogData);
					}
					extraHitParticleIndex = MissionGameModels.Current.DamageParticleModel.GetMissileAttackParticle(attacker, victim, blow, collisionData);
					if (flag8 && numDamagedAgents < 3)
					{
						missileCollisionReaction = Mission.MissileCollisionReaction.PassThrough;
					}
					else
					{
						missileCollisionReaction = missileCollisionReaction2;
						if (missileCollisionReaction2 == Mission.MissileCollisionReaction.Stick && !collisionData.CollidedWithShieldOnBack)
						{
							bool flag9 = this.CombatType == Mission.MissionCombatType.Combat;
							if (flag9)
							{
								bool flag10 = victim.IsHuman && collisionData.VictimHitBodyPart == BoneBodyPartType.Head;
								flag9 = (victim.State != AgentState.Active || !flag10);
							}
							if (flag9)
							{
								float managedParameter = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.MissileMinimumDamageToStick);
								float num2 = 2f * managedParameter;
								if ((float)blow.InflictedDamage < managedParameter && blow.AbsorbedByArmor > num2 && !GameNetwork.IsClientOrReplay)
								{
									missileCollisionReaction = Mission.MissileCollisionReaction.BounceBack;
								}
							}
							else
							{
								missileCollisionReaction = Mission.MissileCollisionReaction.BecomeInvisible;
							}
						}
					}
				}
			}
			if (collisionData.CollidedWithShieldOnBack && weaponComponentData != null && victim != null && victim.IsMainAgent)
			{
				InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("ui_hit_shield_on_back", null).ToString(), Color.ConvertStringToColor("#FFFFFFFF")));
			}
			bool isAttachedFrameLocal;
			MatrixFrame matrixFrame;
			if (!collisionData.MissileHasPhysics && missileCollisionReaction == Mission.MissileCollisionReaction.Stick)
			{
				matrixFrame = this.CalculateAttachedLocalFrame(attachGlobalFrame, collisionData, missile.Weapon.CurrentUsageItem, victim, hitEntity, movementVelocity, missileAngularVelocity, affectedShieldGlobalFrame, true, out isAttachedFrameLocal);
			}
			else
			{
				matrixFrame = attachGlobalFrame;
				matrixFrame.origin.z = Math.Max(matrixFrame.origin.z, -100f);
				missionObject = null;
				isAttachedFrameLocal = false;
			}
			Vec3 zero = Vec3.Zero;
			Vec3 zero2 = Vec3.Zero;
			if (missileCollisionReaction == Mission.MissileCollisionReaction.BounceBack)
			{
				WeaponFlags weaponFlags2 = weaponFlags & WeaponFlags.AmmoBreakOnBounceBackMask;
				if ((weaponFlags2 == WeaponFlags.AmmoCanBreakOnBounceBack && collisionData.MissileVelocity.Length > ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BreakableProjectileMinimumBreakSpeed)) || weaponFlags2 == WeaponFlags.AmmoBreaksOnBounceBack)
				{
					missileCollisionReaction = Mission.MissileCollisionReaction.BecomeInvisible;
					extraHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_broken_arrow");
				}
				else
				{
					missile.CalculateBounceBackVelocity(missileAngularVelocity, collisionData, out zero, out zero2);
				}
			}
			this.HandleMissileCollisionReaction(collisionData.AffectorWeaponSlotOrMissileIndex, missileCollisionReaction, matrixFrame, isAttachedFrameLocal, attacker, victim, collisionData.AttackBlockedWithShield, collisionData.CollisionBoneIndex, missionObject, zero, zero2, -1);
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnMissileHit(attacker, victim, flag5, collisionData);
			}
			return missileCollisionReaction != Mission.MissileCollisionReaction.PassThrough;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00059BD0 File Offset: 0x00057DD0
		public void HandleMissileCollisionReaction(int missileIndex, Mission.MissileCollisionReaction collisionReaction, MatrixFrame attachLocalFrame, bool isAttachedFrameLocal, Agent attackerAgent, Agent attachedAgent, bool attachedToShield, sbyte attachedBoneIndex, MissionObject attachedMissionObject, Vec3 bounceBackVelocity, Vec3 bounceBackAngularVelocity, int forcedSpawnIndex)
		{
			Mission.Missile missile = this._missiles[missileIndex];
			MissionObjectId missionObjectId = new MissionObjectId(-1, true);
			switch (collisionReaction)
			{
			case Mission.MissileCollisionReaction.Stick:
				missile.Entity.SetVisibilityExcludeParents(true);
				if (attachedAgent != null)
				{
					this.PrepareMissileWeaponForDrop(missileIndex);
					if (attachedToShield)
					{
						EquipmentIndex wieldedItemIndex = attachedAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
						attachedAgent.AttachWeaponToWeapon(wieldedItemIndex, missile.Weapon, missile.Entity, ref attachLocalFrame);
					}
					else
					{
						attachedAgent.AttachWeaponToBone(missile.Weapon, missile.Entity, attachedBoneIndex, ref attachLocalFrame);
					}
				}
				else
				{
					Vec3 zero = Vec3.Zero;
					missionObjectId = this.SpawnWeaponAsDropFromMissile(missileIndex, attachedMissionObject, attachLocalFrame, Mission.WeaponSpawnFlags.AsMissile | Mission.WeaponSpawnFlags.WithStaticPhysics, zero, zero, forcedSpawnIndex);
				}
				break;
			case Mission.MissileCollisionReaction.BounceBack:
				missile.Entity.SetVisibilityExcludeParents(true);
				missionObjectId = this.SpawnWeaponAsDropFromMissile(missileIndex, null, attachLocalFrame, Mission.WeaponSpawnFlags.AsMissile | Mission.WeaponSpawnFlags.WithPhysics, bounceBackVelocity, bounceBackAngularVelocity, forcedSpawnIndex);
				break;
			case Mission.MissileCollisionReaction.BecomeInvisible:
				missile.Entity.Remove(81);
				break;
			}
			bool flag = collisionReaction != Mission.MissileCollisionReaction.PassThrough;
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new HandleMissileCollisionReaction(missileIndex, collisionReaction, attachLocalFrame, isAttachedFrameLocal, attackerAgent.Index, (attachedAgent != null) ? attachedAgent.Index : -1, attachedToShield, attachedBoneIndex, (attachedMissionObject != null) ? attachedMissionObject.Id : MissionObjectId.Invalid, bounceBackVelocity, bounceBackAngularVelocity, missionObjectId.Id));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			else if (GameNetwork.IsClientOrReplay && flag)
			{
				this.RemoveMissileAsClient(missileIndex);
			}
			foreach (MissionBehavior missionBehavior in this.MissionBehaviors)
			{
				missionBehavior.OnMissileCollisionReaction(collisionReaction, attackerAgent, attachedAgent, attachedBoneIndex);
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00059D70 File Offset: 0x00057F70
		[UsedImplicitly]
		[MBCallback]
		internal void MissileCalculatePassbySoundParametersCallbackMT(int missileIndex, ref SoundEventParameter soundEventParameter)
		{
			this._missiles[missileIndex].CalculatePassbySoundParametersMT(ref soundEventParameter);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00059D84 File Offset: 0x00057F84
		[UsedImplicitly]
		[MBCallback]
		internal void ChargeDamageCallback(ref AttackCollisionData collisionData, Blow blow, Agent attacker, Agent victim)
		{
			if (victim.CurrentMortalityState != Agent.MortalityState.Invulnerable && (attacker.RiderAgent == null || attacker.IsEnemyOf(victim)))
			{
				WeaponComponentData weaponComponentData;
				CombatLogData combatLogData;
				this.GetAttackCollisionResults(attacker, victim, null, 1f, MissionWeapon.Invalid, false, false, false, ref collisionData, out weaponComponentData, out combatLogData);
				if (collisionData.CollidedWithShieldOnBack && weaponComponentData != null && victim != null && victim.IsMainAgent)
				{
					InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("ui_hit_shield_on_back", null).ToString(), Color.ConvertStringToColor("#FFFFFFFF")));
				}
				if ((float)collisionData.InflictedDamage > 0f)
				{
					blow.BaseMagnitude = collisionData.BaseMagnitude;
					blow.MovementSpeedDamageModifier = collisionData.MovementSpeedDamageModifier;
					blow.InflictedDamage = collisionData.InflictedDamage;
					blow.SelfInflictedDamage = collisionData.SelfInflictedDamage;
					blow.AbsorbedByArmor = (float)collisionData.AbsorbedByArmor;
					blow.DamageCalculated = true;
					if (MissionGameModels.Current.AgentApplyDamageModel.DecideAgentKnockedBackByBlow(attacker, victim, collisionData, null, blow))
					{
						blow.BlowFlag |= BlowFlags.KnockBack;
					}
					else
					{
						blow.BlowFlag &= ~BlowFlags.KnockBack;
					}
					if (MissionGameModels.Current.AgentApplyDamageModel.DecideAgentKnockedDownByBlow(attacker, victim, collisionData, null, blow))
					{
						blow.BlowFlag |= BlowFlags.KnockDown;
					}
					GameEntity realHitEntity = null;
					Blow b = blow;
					MissionWeapon missionWeapon = default(MissionWeapon);
					this.RegisterBlow(attacker, victim, realHitEntity, b, ref collisionData, missionWeapon, ref combatLogData);
				}
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00059ED8 File Offset: 0x000580D8
		[UsedImplicitly]
		[MBCallback]
		internal void FallDamageCallback(ref AttackCollisionData collisionData, Blow b, Agent attacker, Agent victim)
		{
			if (victim.CurrentMortalityState != Agent.MortalityState.Invulnerable)
			{
				WeaponComponentData weaponComponentData;
				CombatLogData combatLogData;
				this.GetAttackCollisionResults(attacker, victim, null, 1f, MissionWeapon.Invalid, false, false, false, ref collisionData, out weaponComponentData, out combatLogData);
				b.BaseMagnitude = collisionData.BaseMagnitude;
				b.MovementSpeedDamageModifier = collisionData.MovementSpeedDamageModifier;
				b.InflictedDamage = collisionData.InflictedDamage;
				b.SelfInflictedDamage = collisionData.SelfInflictedDamage;
				b.AbsorbedByArmor = (float)collisionData.AbsorbedByArmor;
				b.DamageCalculated = true;
				if (b.InflictedDamage > 0)
				{
					Agent riderAgent = victim.RiderAgent;
					GameEntity realHitEntity = null;
					Blow b2 = b;
					MissionWeapon missionWeapon = default(MissionWeapon);
					this.RegisterBlow(attacker, victim, realHitEntity, b2, ref collisionData, missionWeapon, ref combatLogData);
					if (riderAgent != null)
					{
						this.FallDamageCallback(ref collisionData, b, riderAgent, riderAgent);
					}
				}
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00059F90 File Offset: 0x00058190
		public void KillAgentsOnEntity(GameEntity entity, Agent destroyerAgent, bool burnAgents)
		{
			if (entity == null)
			{
				return;
			}
			int ownerId;
			sbyte attackBoneIndex;
			if (destroyerAgent != null)
			{
				ownerId = destroyerAgent.Index;
				attackBoneIndex = destroyerAgent.Monster.MainHandItemBoneIndex;
			}
			else
			{
				ownerId = -1;
				attackBoneIndex = -1;
			}
			Vec3 vec;
			Vec3 vec2;
			entity.GetPhysicsMinMax(true, out vec, out vec2, false);
			Vec2 vec3 = (vec2.AsVec2 + vec.AsVec2) * 0.5f;
			float searchRadius = (vec2.AsVec2 - vec.AsVec2).Length * 0.5f;
			Blow blow = new Blow(ownerId);
			blow.DamageCalculated = true;
			blow.BaseMagnitude = 2000f;
			blow.InflictedDamage = 2000;
			blow.Direction = new Vec3(0f, 0f, -1f, -1f);
			blow.DamageType = DamageTypes.Blunt;
			blow.BoneIndex = 0;
			blow.WeaponRecord.FillAsMeleeBlow(null, null, -1, 0);
			if (burnAgents)
			{
				blow.WeaponRecord.WeaponFlags = (blow.WeaponRecord.WeaponFlags | (WeaponFlags.AffectsArea | WeaponFlags.Burning));
				blow.WeaponRecord.CurrentPosition = blow.GlobalPosition;
				blow.WeaponRecord.StartingPosition = blow.GlobalPosition;
			}
			Vec2 asVec = entity.GetGlobalFrame().TransformToParent(vec3.ToVec3(0f)).AsVec2;
			List<Agent> list = new List<Agent>();
			AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(this, asVec, searchRadius, false);
			while (proximityMapSearchStruct.LastFoundAgent != null)
			{
				Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
				GameEntity gameEntity = lastFoundAgent.GetSteppedEntity();
				while (gameEntity != null && !(gameEntity == entity))
				{
					gameEntity = gameEntity.Parent;
				}
				if (gameEntity != null)
				{
					list.Add(lastFoundAgent);
				}
				AgentProximityMap.FindNext(this, ref proximityMapSearchStruct);
			}
			foreach (Agent agent in list)
			{
				blow.GlobalPosition = agent.Position;
				AttackCollisionData attackCollisionDataForDebugPurpose = AttackCollisionData.GetAttackCollisionDataForDebugPurpose(false, false, false, true, false, false, false, false, false, false, false, false, CombatCollisionResult.StrikeAgent, -1, 0, 2, blow.BoneIndex, BoneBodyPartType.Abdomen, attackBoneIndex, Agent.UsageDirection.AttackLeft, -1, CombatHitResultFlags.NormalHit, 0.5f, 1f, 0f, 0f, 0f, 0f, 0f, 0f, Vec3.Up, blow.Direction, blow.GlobalPosition, Vec3.Zero, Vec3.Zero, agent.Velocity, Vec3.Up);
				agent.RegisterBlow(blow, attackCollisionDataForDebugPurpose);
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0005A21C File Offset: 0x0005841C
		public void KillAgentCheat(Agent agent)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				Agent agent2 = this.MainAgent ?? agent;
				Blow blow = new Blow(agent2.Index);
				blow.DamageType = DamageTypes.Blunt;
				blow.BoneIndex = agent.Monster.HeadLookDirectionBoneIndex;
				blow.GlobalPosition = agent.Position;
				blow.GlobalPosition.z = blow.GlobalPosition.z + agent.GetEyeGlobalHeight();
				blow.BaseMagnitude = 2000f;
				blow.WeaponRecord.FillAsMeleeBlow(null, null, -1, -1);
				blow.InflictedDamage = 2000;
				blow.SwingDirection = agent.LookDirection;
				if (this.InputManager.IsGameKeyDown(2))
				{
					MatrixFrame frame = agent.Frame;
					blow.SwingDirection = frame.rotation.TransformToParent(new Vec3(-1f, 0f, 0f, -1f));
					blow.SwingDirection.Normalize();
				}
				else if (this.InputManager.IsGameKeyDown(3))
				{
					MatrixFrame frame = agent.Frame;
					blow.SwingDirection = frame.rotation.TransformToParent(new Vec3(1f, 0f, 0f, -1f));
					blow.SwingDirection.Normalize();
				}
				else if (this.InputManager.IsGameKeyDown(1))
				{
					MatrixFrame frame = agent.Frame;
					blow.SwingDirection = frame.rotation.TransformToParent(new Vec3(0f, -1f, 0f, -1f));
					blow.SwingDirection.Normalize();
				}
				else if (this.InputManager.IsGameKeyDown(0))
				{
					MatrixFrame frame = agent.Frame;
					blow.SwingDirection = frame.rotation.TransformToParent(new Vec3(0f, 1f, 0f, -1f));
					blow.SwingDirection.Normalize();
				}
				blow.Direction = blow.SwingDirection;
				blow.DamageCalculated = true;
				sbyte mainHandItemBoneIndex = agent2.Monster.MainHandItemBoneIndex;
				AttackCollisionData attackCollisionDataForDebugPurpose = AttackCollisionData.GetAttackCollisionDataForDebugPurpose(false, false, false, true, false, false, false, false, false, false, false, false, CombatCollisionResult.StrikeAgent, -1, 0, 2, blow.BoneIndex, BoneBodyPartType.Head, mainHandItemBoneIndex, Agent.UsageDirection.AttackLeft, -1, CombatHitResultFlags.NormalHit, 0.5f, 1f, 0f, 0f, 0f, 0f, 0f, 0f, Vec3.Up, blow.Direction, blow.GlobalPosition, Vec3.Zero, Vec3.Zero, agent.Velocity, Vec3.Up);
				agent.RegisterBlow(blow, attackCollisionDataForDebugPurpose);
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005A4A0 File Offset: 0x000586A0
		public bool KillCheats(bool killAll, bool killEnemy, bool killHorse, bool killYourself)
		{
			bool result = false;
			if (!GameNetwork.IsClientOrReplay)
			{
				if (killYourself)
				{
					if (this.MainAgent != null)
					{
						if (killHorse)
						{
							if (this.MainAgent.MountAgent != null)
							{
								Agent mountAgent = this.MainAgent.MountAgent;
								this.KillAgentCheat(mountAgent);
								result = true;
							}
						}
						else
						{
							Agent mainAgent = this.MainAgent;
							this.KillAgentCheat(mainAgent);
							result = true;
						}
					}
				}
				else
				{
					bool flag = false;
					int num = this.Agents.Count - 1;
					while (num >= 0 && !flag)
					{
						Agent agent = this.Agents[num];
						if (agent != this.MainAgent && agent.GetAgentFlags().HasAnyFlag(AgentFlag.CanAttack) && this.PlayerTeam != null)
						{
							if (killEnemy)
							{
								if (agent.Team.IsValid && this.PlayerTeam.IsEnemyOf(agent.Team))
								{
									if (killHorse && agent.HasMount)
									{
										if (agent.MountAgent != null)
										{
											this.KillAgentCheat(agent.MountAgent);
											if (!killAll)
											{
												flag = true;
											}
											result = true;
										}
									}
									else
									{
										this.KillAgentCheat(agent);
										if (!killAll)
										{
											flag = true;
										}
										result = true;
									}
								}
							}
							else if (agent.Team.IsValid && this.PlayerTeam.IsFriendOf(agent.Team))
							{
								if (killHorse)
								{
									if (agent.MountAgent != null)
									{
										this.KillAgentCheat(agent.MountAgent);
										if (!killAll)
										{
											flag = true;
										}
										result = true;
									}
								}
								else
								{
									this.KillAgentCheat(agent);
									if (!killAll)
									{
										flag = true;
									}
									result = true;
								}
							}
						}
						num--;
					}
				}
			}
			return result;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005A628 File Offset: 0x00058828
		private bool CancelsDamageAndBlocksAttackBecauseOfNonEnemyCase(Agent attacker, Agent victim)
		{
			if (victim == null || attacker == null)
			{
				return false;
			}
			bool flag = !GameNetwork.IsSessionActive || this.ForceNoFriendlyFire || (MultiplayerOptions.OptionType.FriendlyFireDamageMeleeFriendPercent.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0 && MultiplayerOptions.OptionType.FriendlyFireDamageMeleeSelfPercent.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0) || this.Mode == MissionMode.Duel || attacker.Controller == Agent.ControllerType.AI;
			bool flag2 = attacker.IsFriendOf(victim);
			return (flag && flag2) || (victim.IsHuman && !flag2 && !attacker.IsEnemyOf(victim));
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0005A6A1 File Offset: 0x000588A1
		public float GetDamageMultiplierOfCombatDifficulty(Agent victimAgent, Agent attackerAgent = null)
		{
			if (MissionGameModels.Current.MissionDifficultyModel != null)
			{
				return MissionGameModels.Current.MissionDifficultyModel.GetDamageMultiplierOfCombatDifficulty(victimAgent, attackerAgent);
			}
			return 1f;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0005A6C8 File Offset: 0x000588C8
		public float GetShootDifficulty(Agent affectedAgent, Agent affectorAgent, bool isHeadShot)
		{
			Vec2 vec = affectedAgent.MovementVelocity - affectorAgent.MovementVelocity;
			Vec3 va = new Vec3(vec.x, vec.y, 0f, -1f);
			Vec3 vb = affectedAgent.Position - affectorAgent.Position;
			float num = vb.Normalize();
			float num2 = va.Normalize();
			float length = Vec3.CrossProduct(va, vb).Length;
			float num3 = MBMath.ClampFloat(0.3f * ((4f + num) / 4f) * ((4f + length * num2) / 4f), 1f, 12f);
			if (isHeadShot)
			{
				num3 *= 1.2f;
			}
			return num3;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0005A780 File Offset: 0x00058980
		private MatrixFrame CalculateAttachedLocalFrame(in MatrixFrame attachedGlobalFrame, AttackCollisionData collisionData, WeaponComponentData missileWeapon, Agent affectedAgent, GameEntity hitEntity, Vec3 missileMovementVelocity, Vec3 missileRotationSpeed, MatrixFrame shieldGlobalFrame, bool shouldMissilePenetrate, out bool isAttachedFrameLocal)
		{
			isAttachedFrameLocal = false;
			MatrixFrame matrixFrame = attachedGlobalFrame;
			bool isNonZero = missileWeapon.RotationSpeed.IsNonZero;
			bool flag = affectedAgent != null && !collisionData.AttackBlockedWithShield && missileWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.AmmoSticksWhenShot);
			float managedParameter = ManagedParameters.Instance.GetManagedParameter(flag ? (isNonZero ? ManagedParametersEnum.RotatingProjectileMinPenetration : ManagedParametersEnum.ProjectileMinPenetration) : ManagedParametersEnum.ObjectMinPenetration);
			float managedParameter2 = ManagedParameters.Instance.GetManagedParameter(flag ? (isNonZero ? ManagedParametersEnum.RotatingProjectileMaxPenetration : ManagedParametersEnum.ProjectileMaxPenetration) : ManagedParametersEnum.ObjectMaxPenetration);
			Vec3 vec = missileMovementVelocity;
			float num = vec.Normalize();
			float num2 = MBMath.ClampFloat(flag ? ((float)collisionData.InflictedDamage / affectedAgent.HealthLimit) : (num / ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.ProjectileMaxPenetrationSpeed)), 0f, 1f);
			if (shouldMissilePenetrate)
			{
				float f = managedParameter + (managedParameter2 - managedParameter) * num2;
				matrixFrame.origin += vec * f;
			}
			if (missileRotationSpeed.IsNonZero)
			{
				float managedParameter3 = ManagedParameters.Instance.GetManagedParameter(flag ? ManagedParametersEnum.AgentProjectileNormalWeight : ManagedParametersEnum.ProjectileNormalWeight);
				Vec3 vec2 = missileWeapon.GetMissileStartingFrame().TransformToParent(missileRotationSpeed);
				Vec3 v = -collisionData.CollisionGlobalNormal;
				float num3 = vec2.x * vec2.x;
				float num4 = vec2.y * vec2.y;
				float num5 = vec2.z * vec2.z;
				int i = (num3 > num4 && num3 > num5) ? 0 : ((num4 > num5) ? 1 : 2);
				v -= v.ProjectOnUnitVector(matrixFrame.rotation[i]);
				Vec3 v2 = Vec3.CrossProduct(vec, v.NormalizedCopy());
				float num6 = v2.Normalize();
				matrixFrame.rotation.RotateAboutAnArbitraryVector(v2, num6 * managedParameter3);
			}
			if (!collisionData.AttackBlockedWithShield && affectedAgent != null)
			{
				float num7 = Vec3.DotProduct(collisionData.CollisionGlobalNormal, vec) + 1f;
				if (num7 > 0.5f)
				{
					matrixFrame.origin -= num7 * 0.1f * collisionData.CollisionGlobalNormal;
				}
			}
			matrixFrame = matrixFrame.TransformToParent(missileWeapon.GetMissileStartingFrame().TransformToParent(missileWeapon.StickingFrame));
			matrixFrame = matrixFrame.TransformToParent(missileWeapon.GetMissileStartingFrame());
			if (collisionData.AttackBlockedWithShield)
			{
				matrixFrame = shieldGlobalFrame.TransformToLocal(matrixFrame);
				isAttachedFrameLocal = true;
			}
			else if (affectedAgent != null)
			{
				if (flag)
				{
					MBAgentVisuals agentVisuals = affectedAgent.AgentVisuals;
					matrixFrame = agentVisuals.GetGlobalFrame().TransformToParent(agentVisuals.GetSkeleton().GetBoneEntitialFrameWithIndex(collisionData.CollisionBoneIndex)).GetUnitRotFrame(affectedAgent.AgentScale).TransformToLocalNonOrthogonal(ref matrixFrame);
					isAttachedFrameLocal = true;
				}
			}
			else if (hitEntity != null)
			{
				if (collisionData.CollisionBoneIndex >= 0)
				{
					matrixFrame = hitEntity.Skeleton.GetBoneEntitialFrameWithIndex(collisionData.CollisionBoneIndex).TransformToLocalNonOrthogonal(ref matrixFrame);
					isAttachedFrameLocal = true;
				}
				else
				{
					matrixFrame = hitEntity.GetGlobalFrame().TransformToLocalNonOrthogonal(ref matrixFrame);
					isAttachedFrameLocal = true;
				}
			}
			else
			{
				matrixFrame.origin.z = Math.Max(matrixFrame.origin.z, -100f);
			}
			return matrixFrame;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005AAB8 File Offset: 0x00058CB8
		[UsedImplicitly]
		[MBCallback]
		internal void GetDefendCollisionResults(Agent attackerAgent, Agent defenderAgent, CombatCollisionResult collisionResult, int attackerWeaponSlotIndex, bool isAlternativeAttack, StrikeType strikeType, Agent.UsageDirection attackDirection, float collisionDistanceOnWeapon, float attackProgress, bool attackIsParried, bool isPassiveUsageHit, bool isHeavyAttack, ref float defenderStunPeriod, ref float attackerStunPeriod, ref bool crushedThrough)
		{
			bool flag = false;
			MissionCombatMechanicsHelper.GetDefendCollisionResults(attackerAgent, defenderAgent, collisionResult, attackerWeaponSlotIndex, isAlternativeAttack, strikeType, attackDirection, collisionDistanceOnWeapon, attackProgress, attackIsParried, isPassiveUsageHit, isHeavyAttack, ref defenderStunPeriod, ref attackerStunPeriod, ref crushedThrough, ref flag);
			if ((crushedThrough || flag) && (attackerAgent.CanLogCombatFor || defenderAgent.CanLogCombatFor))
			{
				CombatLogData combatLog = new CombatLogData(false, attackerAgent.IsHuman, attackerAgent.IsMine, attackerAgent.RiderAgent != null, attackerAgent.RiderAgent != null && attackerAgent.RiderAgent.IsMine, attackerAgent.IsMount, defenderAgent.IsHuman, defenderAgent.IsMine, defenderAgent.Health <= 0f, defenderAgent.HasMount, defenderAgent.RiderAgent != null && defenderAgent.RiderAgent.IsMine, defenderAgent.IsMount, false, defenderAgent.RiderAgent == attackerAgent, crushedThrough, flag, 0f);
				this.AddCombatLogSafe(attackerAgent, defenderAgent, null, combatLog);
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005AB9C File Offset: 0x00058D9C
		private CombatLogData GetAttackCollisionResults(Agent attackerAgent, Agent victimAgent, GameEntity hitObject, float momentumRemaining, in MissionWeapon attackerWeapon, bool crushedThrough, bool cancelDamage, bool crushedThroughWithoutAgentCollision, ref AttackCollisionData attackCollisionData, out WeaponComponentData shieldOnBack, out CombatLogData combatLog)
		{
			AttackInformation attackInformation = new AttackInformation(attackerAgent, victimAgent, hitObject, ref attackCollisionData, ref attackerWeapon);
			shieldOnBack = attackInformation.ShieldOnBack;
			int num;
			MissionCombatMechanicsHelper.GetAttackCollisionResults(attackInformation, crushedThrough, momentumRemaining, attackerWeapon, cancelDamage, ref attackCollisionData, out combatLog, out num);
			float num2 = (float)attackCollisionData.InflictedDamage;
			if (num2 > 0f)
			{
				float num3 = MissionGameModels.Current.AgentApplyDamageModel.CalculateDamage(attackInformation, attackCollisionData, attackerWeapon, num2);
				combatLog.ModifiedDamage = MathF.Round(num3 - num2);
				attackCollisionData.InflictedDamage = MathF.Round(num3);
			}
			else
			{
				combatLog.ModifiedDamage = 0;
				attackCollisionData.InflictedDamage = 0;
			}
			if (!attackCollisionData.IsFallDamage && attackInformation.IsFriendlyFire)
			{
				if (!attackInformation.IsAttackerAIControlled && GameNetwork.IsSessionActive)
				{
					int num4 = attackCollisionData.IsMissile ? MultiplayerOptions.OptionType.FriendlyFireDamageRangedSelfPercent.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) : MultiplayerOptions.OptionType.FriendlyFireDamageMeleeSelfPercent.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
					attackCollisionData.SelfInflictedDamage = MathF.Round((float)attackCollisionData.InflictedDamage * ((float)num4 * 0.01f));
					int num5 = attackCollisionData.IsMissile ? MultiplayerOptions.OptionType.FriendlyFireDamageRangedFriendPercent.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) : MultiplayerOptions.OptionType.FriendlyFireDamageMeleeFriendPercent.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
					attackCollisionData.InflictedDamage = MathF.Round((float)attackCollisionData.InflictedDamage * ((float)num5 * 0.01f));
					combatLog.InflictedDamage = attackCollisionData.InflictedDamage;
				}
				combatLog.IsFriendlyFire = true;
			}
			if (attackCollisionData.AttackBlockedWithShield && attackCollisionData.InflictedDamage > 0 && (int)attackInformation.VictimShield.HitPoints - attackCollisionData.InflictedDamage <= 0)
			{
				attackCollisionData.IsShieldBroken = true;
			}
			if (!crushedThroughWithoutAgentCollision)
			{
				combatLog.BodyPartHit = attackCollisionData.VictimHitBodyPart;
				combatLog.IsVictimEntity = (hitObject != null);
			}
			return combatLog;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005AD3C File Offset: 0x00058F3C
		private void PrintAttackCollisionResults(Agent attackerAgent, Agent victimAgent, GameEntity hitEntity, ref AttackCollisionData attackCollisionData, ref CombatLogData combatLog)
		{
			if (attackCollisionData.IsColliderAgent && !attackCollisionData.AttackBlockedWithShield && attackerAgent != null && (attackerAgent.CanLogCombatFor || victimAgent.CanLogCombatFor) && victimAgent.State == AgentState.Active)
			{
				this.AddCombatLogSafe(attackerAgent, victimAgent, hitEntity, combatLog);
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005AD90 File Offset: 0x00058F90
		private void AddCombatLogSafe(Agent attackerAgent, Agent victimAgent, GameEntity hitEntity, CombatLogData combatLog)
		{
			combatLog.SetVictimAgent(victimAgent);
			if (GameNetwork.IsServerOrRecorder)
			{
				CombatLogNetworkMessage message = new CombatLogNetworkMessage(attackerAgent.Index, (victimAgent != null) ? victimAgent.Index : -1, hitEntity, combatLog);
				object obj = (attackerAgent == null) ? null : (attackerAgent.IsHuman ? attackerAgent : attackerAgent.RiderAgent);
				object obj2;
				if (obj == null)
				{
					obj2 = null;
				}
				else
				{
					MissionPeer missionPeer = obj.MissionPeer;
					obj2 = ((missionPeer != null) ? missionPeer.Peer.Communicator : null);
				}
				NetworkCommunicator networkCommunicator = obj2 as NetworkCommunicator;
				object obj3 = (victimAgent == null) ? null : (victimAgent.IsHuman ? victimAgent : victimAgent.RiderAgent);
				object obj4;
				if (obj3 == null)
				{
					obj4 = null;
				}
				else
				{
					MissionPeer missionPeer2 = obj3.MissionPeer;
					obj4 = ((missionPeer2 != null) ? missionPeer2.Peer.Communicator : null);
				}
				NetworkCommunicator networkCommunicator2 = obj4 as NetworkCommunicator;
				if (networkCommunicator != null && !networkCommunicator.IsServerPeer)
				{
					GameNetwork.BeginModuleEventAsServer(networkCommunicator);
					GameNetwork.WriteMessage(message);
					GameNetwork.EndModuleEventAsServer();
				}
				if (networkCommunicator2 != null && !networkCommunicator2.IsServerPeer && networkCommunicator2 != networkCommunicator)
				{
					GameNetwork.BeginModuleEventAsServer(networkCommunicator2);
					GameNetwork.WriteMessage(message);
					GameNetwork.EndModuleEventAsServer();
				}
			}
			this._combatLogsCreated.Enqueue(combatLog);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0005AE88 File Offset: 0x00059088
		public MissionObject CreateMissionObjectFromPrefab(string prefab, MatrixFrame frame)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				GameEntity gameEntity = GameEntity.Instantiate(this.Scene, prefab, frame);
				MissionObject firstScriptOfType = gameEntity.GetFirstScriptOfType<MissionObject>();
				List<MissionObjectId> list = new List<MissionObjectId>();
				using (IEnumerator<GameEntity> enumerator = gameEntity.GetChildren().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MissionObject firstScriptOfType2;
						if ((firstScriptOfType2 = enumerator.Current.GetFirstScriptOfType<MissionObject>()) != null)
						{
							list.Add(firstScriptOfType2.Id);
						}
					}
				}
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new CreateMissionObject(firstScriptOfType.Id, prefab, frame, list));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
					this.AddDynamicallySpawnedMissionObjectInfo(new Mission.DynamicallyCreatedEntity(prefab, firstScriptOfType.Id, frame, ref list));
				}
				return firstScriptOfType;
			}
			return null;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0005AF44 File Offset: 0x00059144
		public int GetNearbyAllyAgentsCount(Vec2 center, float radius, Team team)
		{
			return this.GetNearbyAgentsCountAux(center, radius, team.MBTeam, Mission.GetNearbyAgentsAuxType.Friend);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005AF55 File Offset: 0x00059155
		public MBList<Agent> GetNearbyAllyAgents(Vec2 center, float radius, Team team, MBList<Agent> agents)
		{
			agents.Clear();
			this.GetNearbyAgentsAux(center, radius, team.MBTeam, Mission.GetNearbyAgentsAuxType.Friend, agents);
			return agents;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0005AF71 File Offset: 0x00059171
		public MBList<Agent> GetNearbyEnemyAgents(Vec2 center, float radius, Team team, MBList<Agent> agents)
		{
			agents.Clear();
			this.GetNearbyAgentsAux(center, radius, team.MBTeam, Mission.GetNearbyAgentsAuxType.Enemy, agents);
			return agents;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0005AF8D File Offset: 0x0005918D
		public MBList<Agent> GetNearbyAgents(Vec2 center, float radius, MBList<Agent> agents)
		{
			agents.Clear();
			this.GetNearbyAgentsAux(center, radius, MBTeam.InvalidTeam, Mission.GetNearbyAgentsAuxType.All, agents);
			return agents;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0005AFA8 File Offset: 0x000591A8
		public bool IsFormationUnitPositionAvailable(ref WorldPosition formationPosition, ref WorldPosition unitPosition, ref WorldPosition nearestAvailableUnitPosition, float manhattanDistance, Team team)
		{
			if (!formationPosition.IsValid || formationPosition.GetNavMesh() == UIntPtr.Zero || !unitPosition.IsValid || unitPosition.GetNavMesh() == UIntPtr.Zero)
			{
				return false;
			}
			if (this.IsFormationUnitPositionAvailable_AdditionalCondition != null && !this.IsFormationUnitPositionAvailable_AdditionalCondition(unitPosition, team))
			{
				return false;
			}
			if (this.Mode == MissionMode.Deployment && this.DeploymentPlan.HasDeploymentBoundaries(team.Side))
			{
				IMissionDeploymentPlan deploymentPlan = this.DeploymentPlan;
				BattleSideEnum side = team.Side;
				Vec2 asVec = unitPosition.AsVec2;
				if (!deploymentPlan.IsPositionInsideDeploymentBoundaries(side, asVec))
				{
					return false;
				}
			}
			return this.IsFormationUnitPositionAvailableAux(ref formationPosition, ref unitPosition, ref nearestAvailableUnitPosition, manhattanDistance);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0005B054 File Offset: 0x00059254
		public bool IsOrderPositionAvailable(in WorldPosition orderPosition, Team team)
		{
			WorldPosition worldPosition = orderPosition;
			if (worldPosition.IsValid)
			{
				worldPosition = orderPosition;
				if (!(worldPosition.GetNavMesh() == UIntPtr.Zero))
				{
					if (this.IsFormationUnitPositionAvailable_AdditionalCondition != null && !this.IsFormationUnitPositionAvailable_AdditionalCondition(orderPosition, team))
					{
						return false;
					}
					worldPosition = orderPosition;
					return this.IsPositionInsideBoundaries(worldPosition.AsVec2);
				}
			}
			return false;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0005B0C0 File Offset: 0x000592C0
		public bool IsFormationUnitPositionAvailable(ref WorldPosition unitPosition, Team team)
		{
			WorldPosition worldPosition = unitPosition;
			float manhattanDistance = 1f;
			WorldPosition invalid = WorldPosition.Invalid;
			return this.IsFormationUnitPositionAvailable(ref worldPosition, ref unitPosition, ref invalid, manhattanDistance, team);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0005B0ED File Offset: 0x000592ED
		public bool HasSceneMapPatch()
		{
			return this.InitializerRecord.SceneHasMapPatch;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0005B0FC File Offset: 0x000592FC
		public bool GetPatchSceneEncounterPosition(out Vec3 position)
		{
			if (this.InitializerRecord.SceneHasMapPatch)
			{
				Vec2 patchCoordinates = this.InitializerRecord.PatchCoordinates;
				float northRotation = this.Scene.GetNorthRotation();
				Vec2 vec;
				Vec2 v;
				this.Boundaries.GetOrientedBoundariesBox(out vec, out v, northRotation);
				Vec2 side = Vec2.Side;
				side.RotateCCW(northRotation);
				Vec2 v2 = side.LeftVec();
				Vec2 vec2 = v - vec;
				Vec2 position2 = vec.x * side + vec.y * v2 + vec2.x * patchCoordinates.x * side + vec2.y * patchCoordinates.y * v2;
				position = position2.ToVec3(this.Scene.GetTerrainHeight(position2, true));
				return true;
			}
			position = Vec3.Invalid;
			return false;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0005B1E0 File Offset: 0x000593E0
		public bool GetPatchSceneEncounterDirection(out Vec2 direction)
		{
			if (this.InitializerRecord.SceneHasMapPatch)
			{
				float northRotation = this.Scene.GetNorthRotation();
				direction = this.InitializerRecord.PatchEncounterDir;
				direction.RotateCCW(northRotation);
				return true;
			}
			direction = Vec2.Invalid;
			return false;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0005B22C File Offset: 0x0005942C
		private void TickDebugAgents()
		{
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0005B230 File Offset: 0x00059430
		public void AddTimerToDynamicEntity(GameEntity gameEntity, float timeToKill = 10f)
		{
			Mission.DynamicEntityInfo item = new Mission.DynamicEntityInfo
			{
				Entity = gameEntity,
				TimerToDisable = new Timer(this.CurrentTime, timeToKill, true)
			};
			this._dynamicEntities.Add(item);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0005B269 File Offset: 0x00059469
		public void AddListener(IMissionListener listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0005B277 File Offset: 0x00059477
		public void RemoveListener(IMissionListener listener)
		{
			this._listeners.Remove(listener);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0005B288 File Offset: 0x00059488
		public void OnAgentFleeing(Agent agent)
		{
			for (int i = this.MissionBehaviors.Count - 1; i >= 0; i--)
			{
				this.MissionBehaviors[i].OnAgentFleeing(agent);
			}
			agent.OnFleeing();
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0005B2C8 File Offset: 0x000594C8
		public void OnAgentPanicked(Agent agent)
		{
			for (int i = this.MissionBehaviors.Count - 1; i >= 0; i--)
			{
				this.MissionBehaviors[i].OnAgentPanicked(agent);
			}
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0005B300 File Offset: 0x00059500
		public void OnDeploymentFinished()
		{
			foreach (Team team in this.Teams)
			{
				if (team.TeamAI != null)
				{
					team.TeamAI.OnDeploymentFinished();
				}
			}
			for (int i = this.MissionBehaviors.Count - 1; i >= 0; i--)
			{
				this.MissionBehaviors[i].OnDeploymentFinished();
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0005B388 File Offset: 0x00059588
		public void SetFastForwardingFromUI(bool fastForwarding)
		{
			this.IsFastForward = fastForwarding;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0005B391 File Offset: 0x00059591
		public bool CheckIfBattleInRetreat()
		{
			Func<bool> isBattleInRetreatEvent = this.IsBattleInRetreatEvent;
			return isBattleInRetreatEvent != null && isBattleInRetreatEvent();
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0005B3A4 File Offset: 0x000595A4
		public void AddSpawnedItemEntityCreatedAtRuntime(SpawnedItemEntity spawnedItemEntity)
		{
			this._spawnedItemEntitiesCreatedAtRuntime.Add(spawnedItemEntity);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0005B3B2 File Offset: 0x000595B2
		public void TriggerOnItemPickUpEvent(Agent agent, SpawnedItemEntity spawnedItemEntity)
		{
			Action<Agent, SpawnedItemEntity> onItemPickUp = this.OnItemPickUp;
			if (onItemPickUp == null)
			{
				return;
			}
			onItemPickUp(agent, spawnedItemEntity);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0005B3C8 File Offset: 0x000595C8
		[UsedImplicitly]
		[MBCallback]
		internal static void DebugLogNativeMissionNetworkEvent(int eventEnum, string eventName, int bitCount)
		{
			int eventType = eventEnum + CompressionBasic.NetworkComponentEventTypeFromServerCompressionInfo.GetMaximumValue() + 1;
			DebugNetworkEventStatistics.StartEvent(eventName, eventType);
			DebugNetworkEventStatistics.AddDataToStatistic(bitCount);
			DebugNetworkEventStatistics.EndEvent();
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0005B3F6 File Offset: 0x000595F6
		[UsedImplicitly]
		[MBCallback]
		internal void PauseMission()
		{
			this._missionState.Paused = true;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0005B404 File Offset: 0x00059604
		[CommandLineFunctionality.CommandLineArgumentFunction("toggleDisableDying", "mission")]
		public static string ToggleDisableDying(List<string> strings)
		{
			if (GameNetwork.IsSessionActive)
			{
				return "Does not work on multiplayer.";
			}
			int num = 0;
			if (strings.Count > 0 && !int.TryParse(strings[0], out num))
			{
				return "Please write the arguments in the correct format. Correct format is: 'toggleDisableDying [index]' or just 'toggleDisableDying' for making all agents invincible.";
			}
			if (Mission.Current == null)
			{
				return "No active mission found";
			}
			if (strings.Count == 0 || num == -1)
			{
				Mission.Current.DisableDying = !Mission.Current.DisableDying;
				if (Mission.Current.DisableDying)
				{
					return "Dying disabled for all";
				}
				return "Dying not disabled for all";
			}
			else
			{
				Agent agent = Mission.Current.FindAgentWithIndex(num);
				if (agent != null)
				{
					agent.ToggleInvulnerable();
					return "Disable Dying for agent " + num.ToString() + ": " + (agent.CurrentMortalityState == Agent.MortalityState.Invulnerable).ToString();
				}
				return "Invalid agent index " + num.ToString();
			}
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0005B4DC File Offset: 0x000596DC
		[CommandLineFunctionality.CommandLineArgumentFunction("toggleDisableDyingTeam", "mission")]
		public static string ToggleDisableDyingTeam(List<string> strings)
		{
			if (GameNetwork.IsSessionActive)
			{
				return "Does not work on multiplayer.";
			}
			int num = 0;
			if (strings.Count > 0 && !int.TryParse(strings[0], out num))
			{
				return "Please write the arguments in the correct format. Correct format is: 'toggleDisableDyingTeam [team_no]' for making all active agents of a team invincible.";
			}
			int num2 = 0;
			foreach (Agent agent in Mission.Current.AllAgents)
			{
				if (agent.Team != null && agent.Team.MBTeam.Index == num)
				{
					agent.ToggleInvulnerable();
					num2++;
				}
			}
			return string.Concat(new object[]
			{
				"Toggled invulnerability for active agents of team ",
				num.ToString(),
				", agent count: ",
				num2
			});
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005B5B4 File Offset: 0x000597B4
		[CommandLineFunctionality.CommandLineArgumentFunction("killAgent", "mission")]
		public static string KillAgent(List<string> strings)
		{
			if (GameNetwork.IsSessionActive)
			{
				return "Does not work on multiplayer.";
			}
			if (Mission.Current == null)
			{
				return "Current mission does not exist.";
			}
			int agentId;
			if (strings.Count == 0 || !int.TryParse(strings[0], out agentId))
			{
				return "Please write the arguments in the correct format. Correct format is: 'killAgent [index]'";
			}
			Agent agent = Mission.Current.FindAgentWithIndex(agentId);
			if (agent == null)
			{
				return "Agent " + agentId.ToString() + " not found.";
			}
			if (agent.State == AgentState.Active)
			{
				Mission.Current.KillAgentCheat(agent);
				return "Agent " + agentId.ToString() + " died.";
			}
			return "Agent " + agentId.ToString() + " already dead.";
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0005B668 File Offset: 0x00059868
		[CommandLineFunctionality.CommandLineArgumentFunction("set_battering_ram_speed", "mission")]
		public static string IncreaseBatteringRamSpeeds(List<string> strings)
		{
			if (GameNetwork.IsSessionActive)
			{
				return "Does not work on multiplayer.";
			}
			float num;
			if (strings.Count == 0 || !float.TryParse(strings[0], out num))
			{
				return "Please enter a speed value";
			}
			foreach (MissionObject missionObject in Mission.Current.ActiveMissionObjects)
			{
				if (missionObject.GameEntity.HasScriptOfType<BatteringRam>())
				{
					missionObject.GameEntity.GetFirstScriptOfType<BatteringRam>().MovementComponent.MaxSpeed = num;
					missionObject.GameEntity.GetFirstScriptOfType<BatteringRam>().MovementComponent.MinSpeed = num;
				}
			}
			return "Battering ram max speed increased.";
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0005B724 File Offset: 0x00059924
		[CommandLineFunctionality.CommandLineArgumentFunction("set_siege_tower_speed", "mission")]
		public static string IncreaseSiegeTowerSpeed(List<string> strings)
		{
			if (GameNetwork.IsSessionActive)
			{
				return "Does not work on multiplayer.";
			}
			float maxSpeed;
			if (strings.Count == 0 || !float.TryParse(strings[0], out maxSpeed))
			{
				return "Please enter a speed value";
			}
			foreach (MissionObject missionObject in Mission.Current.ActiveMissionObjects)
			{
				if (missionObject.GameEntity.HasScriptOfType<SiegeTower>())
				{
					missionObject.GameEntity.GetFirstScriptOfType<SiegeTower>().MovementComponent.MaxSpeed = maxSpeed;
				}
			}
			return "Siege tower max speed increased.";
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0005B7C8 File Offset: 0x000599C8
		[CommandLineFunctionality.CommandLineArgumentFunction("reload_managed_core_params", "game")]
		public static string LoadParamsDebug(List<string> strings)
		{
			if (!GameNetwork.IsSessionActive)
			{
				ManagedParameters.Instance.Initialize(ModuleHelper.GetXmlPath("Native", "managed_core_parameters"));
				return "Managed core parameters reloaded.";
			}
			return "Does not work on multiplayer.";
		}

		// Token: 0x0400077A RID: 1914
		public const int MaxRuntimeMissionObjects = 4095;

		// Token: 0x0400077E RID: 1918
		private int _lastSceneMissionObjectIdCount;

		// Token: 0x0400077F RID: 1919
		private int _lastRuntimeMissionObjectIdCount;

		// Token: 0x04000780 RID: 1920
		private bool _isMainAgentObjectInteractionEnabled = true;

		// Token: 0x04000781 RID: 1921
		private List<Mission.TimeSpeedRequest> _timeSpeedRequests = new List<Mission.TimeSpeedRequest>();

		// Token: 0x04000782 RID: 1922
		private bool _isMainAgentItemInteractionEnabled = true;

		// Token: 0x04000783 RID: 1923
		private readonly MBList<MissionObject> _activeMissionObjects;

		// Token: 0x04000784 RID: 1924
		private readonly MBList<MissionObject> _missionObjects;

		// Token: 0x04000785 RID: 1925
		private readonly List<SpawnedItemEntity> _spawnedItemEntitiesCreatedAtRuntime;

		// Token: 0x04000786 RID: 1926
		private readonly MBList<Mission.DynamicallyCreatedEntity> _addedEntitiesInfo;

		// Token: 0x04000787 RID: 1927
		private readonly Stack<ValueTuple<int, float>> _emptyRuntimeMissionObjectIds;

		// Token: 0x04000788 RID: 1928
		private static bool _isCameraFirstPerson = false;

		// Token: 0x0400078C RID: 1932
		private MissionMode _missionMode;

		// Token: 0x0400078D RID: 1933
		private float _cachedMissionTime;

		// Token: 0x0400078F RID: 1935
		private static readonly object GetNearbyAgentsAuxLock = new object();

		// Token: 0x04000790 RID: 1936
		public const int MaxNavMeshId = 1000000;

		// Token: 0x04000791 RID: 1937
		private const float NavigationMeshHeightLimit = 1.5f;

		// Token: 0x04000792 RID: 1938
		private const float SpeedBonusFactorForSwing = 0.7f;

		// Token: 0x04000793 RID: 1939
		private const float SpeedBonusFactorForThrust = 0.5f;

		// Token: 0x04000794 RID: 1940
		private const float _exitTimeInSeconds = 0.6f;

		// Token: 0x04000795 RID: 1941
		private const int MaxNavMeshPerDynamicObject = 10;

		// Token: 0x0400079A RID: 1946
		private readonly List<Mission.DynamicEntityInfo> _dynamicEntities = new List<Mission.DynamicEntityInfo>();

		// Token: 0x0400079B RID: 1947
		private Dictionary<int, Mission.Missile> _missiles;

		// Token: 0x0400079D RID: 1949
		private bool _missionEnded;

		// Token: 0x040007A1 RID: 1953
		public bool DisableDying;

		// Token: 0x040007A2 RID: 1954
		public bool ForceNoFriendlyFire;

		// Token: 0x040007A3 RID: 1955
		private int _nextDynamicNavMeshIdStart = 1000010;

		// Token: 0x040007A4 RID: 1956
		public bool IsFriendlyMission = true;

		// Token: 0x040007A5 RID: 1957
		public const int MaxDamage = 2000;

		// Token: 0x040007A6 RID: 1958
		public BasicCultureObject MusicCulture;

		// Token: 0x040007A7 RID: 1959
		private List<IMissionListener> _listeners = new List<IMissionListener>();

		// Token: 0x040007A8 RID: 1960
		private MissionState _missionState;

		// Token: 0x040007A9 RID: 1961
		private MissionDeploymentPlan _deploymentPlan;

		// Token: 0x040007AC RID: 1964
		private readonly object _lockHelper = new object();

		// Token: 0x040007AD RID: 1965
		private List<MissionBehavior> _otherMissionBehaviors;

		// Token: 0x040007AE RID: 1966
		private MBList<Agent> _activeAgents;

		// Token: 0x040007AF RID: 1967
		private BasicMissionTimer _leaveMissionTimer;

		// Token: 0x040007B0 RID: 1968
		private readonly MBList<KeyValuePair<Agent, MissionTime>> _mountsWithoutRiders;

		// Token: 0x040007B1 RID: 1969
		public bool IsOrderMenuOpen;

		// Token: 0x040007B2 RID: 1970
		public bool IsTransferMenuOpen;

		// Token: 0x040007B3 RID: 1971
		public bool IsInPhotoMode;

		// Token: 0x040007B4 RID: 1972
		private Agent _mainAgent;

		// Token: 0x040007B5 RID: 1973
		private Action _onLoadingEndedAction;

		// Token: 0x040007B6 RID: 1974
		private Timer _inMissionLoadingScreenTimer;

		// Token: 0x040007B7 RID: 1975
		public bool AllowAiTicking = true;

		// Token: 0x040007B8 RID: 1976
		private int _agentCreationIndex;

		// Token: 0x040007B9 RID: 1977
		private readonly MBList<FleePosition>[] _fleePositions = new MBList<FleePosition>[3];

		// Token: 0x040007BA RID: 1978
		private bool _doesMissionRequireCivilianEquipment;

		// Token: 0x040007BB RID: 1979
		public IAgentVisualCreator AgentVisualCreator;

		// Token: 0x040007BC RID: 1980
		private readonly int[] _initialAgentCountPerSide = new int[2];

		// Token: 0x040007BD RID: 1981
		private readonly int[] _removedAgentCountPerSide = new int[2];

		// Token: 0x040007BF RID: 1983
		private ConcurrentQueue<CombatLogData> _combatLogsCreated = new ConcurrentQueue<CombatLogData>();

		// Token: 0x040007C0 RID: 1984
		private MBList<Agent> _allAgents;

		// Token: 0x040007C2 RID: 1986
		private List<SiegeWeapon> _attackerWeaponsForFriendlyFirePreventing = new List<SiegeWeapon>();

		// Token: 0x040007CC RID: 1996
		private float _missionEndTime;

		// Token: 0x040007CD RID: 1997
		public float MissionCloseTimeAfterFinish = 30f;

		// Token: 0x040007CE RID: 1998
		private static Mission _current = null;

		// Token: 0x040007D1 RID: 2001
		public float NextCheckTimeEndMission = 10f;

		// Token: 0x040007D3 RID: 2003
		public int NumOfFormationsSpawnedTeamOne;

		// Token: 0x040007D4 RID: 2004
		private SoundEvent _ambientSoundEvent;

		// Token: 0x040007D5 RID: 2005
		private readonly BattleSpawnPathSelector _battleSpawnPathSelector;

		// Token: 0x040007D6 RID: 2006
		private int _agentCount;

		// Token: 0x040007D7 RID: 2007
		public int NumOfFormationsSpawnedTeamTwo;

		// Token: 0x040007E3 RID: 2019
		private bool tickCompleted = true;

		// Token: 0x020004C0 RID: 1216
		public class MBBoundaryCollection : IDictionary<string, ICollection<Vec2>>, ICollection<KeyValuePair<string, ICollection<Vec2>>>, IEnumerable<KeyValuePair<string, ICollection<Vec2>>>, IEnumerable, INotifyCollectionChanged
		{
			// Token: 0x06003719 RID: 14105 RVA: 0x000DE46F File Offset: 0x000DC66F
			IEnumerator IEnumerable.GetEnumerator()
			{
				int count = this.Count;
				int num;
				for (int i = 0; i < count; i = num + 1)
				{
					string boundaryName = MBAPI.IMBMission.GetBoundaryName(this._mission.Pointer, i);
					List<Vec2> boundaryPoints = this.GetBoundaryPoints(boundaryName);
					yield return new KeyValuePair<string, ICollection<Vec2>>(boundaryName, boundaryPoints);
					num = i;
				}
				yield break;
			}

			// Token: 0x0600371A RID: 14106 RVA: 0x000DE47E File Offset: 0x000DC67E
			public IEnumerator<KeyValuePair<string, ICollection<Vec2>>> GetEnumerator()
			{
				int count = this.Count;
				int num;
				for (int i = 0; i < count; i = num + 1)
				{
					string boundaryName = MBAPI.IMBMission.GetBoundaryName(this._mission.Pointer, i);
					List<Vec2> boundaryPoints = this.GetBoundaryPoints(boundaryName);
					yield return new KeyValuePair<string, ICollection<Vec2>>(boundaryName, boundaryPoints);
					num = i;
				}
				yield break;
			}

			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x0600371B RID: 14107 RVA: 0x000DE48D File Offset: 0x000DC68D
			public int Count
			{
				get
				{
					return MBAPI.IMBMission.GetBoundaryCount(this._mission.Pointer);
				}
			}

			// Token: 0x0600371C RID: 14108 RVA: 0x000DE4A4 File Offset: 0x000DC6A4
			public float GetBoundaryRadius(string name)
			{
				return MBAPI.IMBMission.GetBoundaryRadius(this._mission.Pointer, name);
			}

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x0600371D RID: 14109 RVA: 0x000DE4BC File Offset: 0x000DC6BC
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600371E RID: 14110 RVA: 0x000DE4C0 File Offset: 0x000DC6C0
			public void GetOrientedBoundariesBox(out Vec2 boxMinimum, out Vec2 boxMaximum, float rotationInRadians = 0f)
			{
				Vec2 side = Vec2.Side;
				side.RotateCCW(rotationInRadians);
				Vec2 vb = side.LeftVec();
				boxMinimum = new Vec2(float.MaxValue, float.MaxValue);
				boxMaximum = new Vec2(float.MinValue, float.MinValue);
				foreach (ICollection<Vec2> collection in this.Values)
				{
					foreach (Vec2 va in collection)
					{
						float num = Vec2.DotProduct(va, side);
						float num2 = Vec2.DotProduct(va, vb);
						boxMinimum.x = ((num < boxMinimum.x) ? num : boxMinimum.x);
						boxMinimum.y = ((num2 < boxMinimum.y) ? num2 : boxMinimum.y);
						boxMaximum.x = ((num > boxMaximum.x) ? num : boxMaximum.x);
						boxMaximum.y = ((num2 > boxMaximum.y) ? num2 : boxMaximum.y);
					}
				}
			}

			// Token: 0x0600371F RID: 14111 RVA: 0x000DE5F8 File Offset: 0x000DC7F8
			internal MBBoundaryCollection(Mission mission)
			{
				this._mission = mission;
			}

			// Token: 0x06003720 RID: 14112 RVA: 0x000DE607 File Offset: 0x000DC807
			public void Add(KeyValuePair<string, ICollection<Vec2>> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x06003721 RID: 14113 RVA: 0x000DE620 File Offset: 0x000DC820
			public void Clear()
			{
				foreach (KeyValuePair<string, ICollection<Vec2>> keyValuePair in this)
				{
					this.Remove(keyValuePair.Key);
				}
			}

			// Token: 0x06003722 RID: 14114 RVA: 0x000DE670 File Offset: 0x000DC870
			public bool Contains(KeyValuePair<string, ICollection<Vec2>> item)
			{
				return this.ContainsKey(item.Key);
			}

			// Token: 0x06003723 RID: 14115 RVA: 0x000DE680 File Offset: 0x000DC880
			public void CopyTo(KeyValuePair<string, ICollection<Vec2>>[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				foreach (KeyValuePair<string, ICollection<Vec2>> keyValuePair in this)
				{
					array[arrayIndex] = keyValuePair;
					arrayIndex++;
					if (arrayIndex >= array.Length)
					{
						throw new ArgumentException("Not enough size in array.");
					}
				}
			}

			// Token: 0x06003724 RID: 14116 RVA: 0x000DE6FC File Offset: 0x000DC8FC
			public bool Remove(KeyValuePair<string, ICollection<Vec2>> item)
			{
				return this.Remove(item.Key);
			}

			// Token: 0x17000956 RID: 2390
			// (get) Token: 0x06003725 RID: 14117 RVA: 0x000DE70C File Offset: 0x000DC90C
			public ICollection<string> Keys
			{
				get
				{
					List<string> list = new List<string>();
					foreach (KeyValuePair<string, ICollection<Vec2>> keyValuePair in this)
					{
						list.Add(keyValuePair.Key);
					}
					return list;
				}
			}

			// Token: 0x17000957 RID: 2391
			// (get) Token: 0x06003726 RID: 14118 RVA: 0x000DE764 File Offset: 0x000DC964
			public ICollection<ICollection<Vec2>> Values
			{
				get
				{
					List<ICollection<Vec2>> list = new List<ICollection<Vec2>>();
					foreach (KeyValuePair<string, ICollection<Vec2>> keyValuePair in this)
					{
						list.Add(keyValuePair.Value);
					}
					return list;
				}
			}

			// Token: 0x17000958 RID: 2392
			public ICollection<Vec2> this[string name]
			{
				get
				{
					if (name == null)
					{
						throw new ArgumentNullException("name");
					}
					List<Vec2> boundaryPoints = this.GetBoundaryPoints(name);
					if (boundaryPoints.Count == 0)
					{
						throw new KeyNotFoundException();
					}
					return boundaryPoints;
				}
				set
				{
					if (name == null)
					{
						throw new ArgumentNullException("name");
					}
					this.Add(name, value);
				}
			}

			// Token: 0x06003729 RID: 14121 RVA: 0x000DE806 File Offset: 0x000DCA06
			public void Add(string name, ICollection<Vec2> points)
			{
				this.Add(name, points, true);
			}

			// Token: 0x0600372A RID: 14122 RVA: 0x000DE814 File Offset: 0x000DCA14
			public void Add(string name, ICollection<Vec2> points, bool isAllowanceInside)
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				if (points == null)
				{
					throw new ArgumentNullException("points");
				}
				if (points.Count < 3)
				{
					throw new ArgumentException("At least three points are required.");
				}
				bool flag = MBAPI.IMBMission.AddBoundary(this._mission.Pointer, name, points.ToArray<Vec2>(), points.Count, isAllowanceInside);
				if (!flag)
				{
					throw new ArgumentException("An element with the same name already exists.");
				}
				if (flag)
				{
					NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
					if (collectionChanged != null)
					{
						collectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, name));
					}
				}
				foreach (Team team in Mission.Current.Teams)
				{
					foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
					{
						formation.ResetMovementOrderPositionCache();
					}
				}
			}

			// Token: 0x0600372B RID: 14123 RVA: 0x000DE920 File Offset: 0x000DCB20
			public bool ContainsKey(string name)
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				return this.GetBoundaryPoints(name).Count > 0;
			}

			// Token: 0x0600372C RID: 14124 RVA: 0x000DE940 File Offset: 0x000DCB40
			public bool Remove(string name)
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				bool flag = MBAPI.IMBMission.RemoveBoundary(this._mission.Pointer, name);
				if (flag)
				{
					NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
					if (collectionChanged != null)
					{
						collectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, name));
					}
				}
				foreach (Team team in Mission.Current.Teams)
				{
					foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
					{
						formation.ResetMovementOrderPositionCache();
					}
				}
				return flag;
			}

			// Token: 0x0600372D RID: 14125 RVA: 0x000DEA10 File Offset: 0x000DCC10
			public bool TryGetValue(string name, out ICollection<Vec2> points)
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				points = this.GetBoundaryPoints(name);
				return points.Count > 0;
			}

			// Token: 0x0600372E RID: 14126 RVA: 0x000DEA34 File Offset: 0x000DCC34
			private List<Vec2> GetBoundaryPoints(string name)
			{
				List<Vec2> list = new List<Vec2>();
				Vec2[] array = new Vec2[10];
				for (int i = 0; i < 1000; i += 10)
				{
					int num = -1;
					MBAPI.IMBMission.GetBoundaryPoints(this._mission.Pointer, name, i, array, 10, ref num);
					list.AddRange(array.Take(num));
					if (num < 10)
					{
						break;
					}
				}
				return list;
			}

			// Token: 0x140000A1 RID: 161
			// (add) Token: 0x0600372F RID: 14127 RVA: 0x000DEA94 File Offset: 0x000DCC94
			// (remove) Token: 0x06003730 RID: 14128 RVA: 0x000DEACC File Offset: 0x000DCCCC
			public event NotifyCollectionChangedEventHandler CollectionChanged;

			// Token: 0x04001ACA RID: 6858
			private readonly Mission _mission;
		}

		// Token: 0x020004C1 RID: 1217
		public class DynamicallyCreatedEntity
		{
			// Token: 0x06003731 RID: 14129 RVA: 0x000DEB01 File Offset: 0x000DCD01
			public DynamicallyCreatedEntity(string prefab, MissionObjectId objectId, MatrixFrame frame, ref List<MissionObjectId> childObjectIds)
			{
				this.Prefab = prefab;
				this.ObjectId = objectId;
				this.Frame = frame;
				this.ChildObjectIds = childObjectIds;
			}

			// Token: 0x04001ACC RID: 6860
			public string Prefab;

			// Token: 0x04001ACD RID: 6861
			public MissionObjectId ObjectId;

			// Token: 0x04001ACE RID: 6862
			public MatrixFrame Frame;

			// Token: 0x04001ACF RID: 6863
			public List<MissionObjectId> ChildObjectIds;
		}

		// Token: 0x020004C2 RID: 1218
		[Flags]
		[EngineStruct("Weapon_spawn_flag", false)]
		public enum WeaponSpawnFlags : uint
		{
			// Token: 0x04001AD1 RID: 6865
			None = 0U,
			// Token: 0x04001AD2 RID: 6866
			WithHolster = 1U,
			// Token: 0x04001AD3 RID: 6867
			WithoutHolster = 2U,
			// Token: 0x04001AD4 RID: 6868
			AsMissile = 4U,
			// Token: 0x04001AD5 RID: 6869
			WithPhysics = 8U,
			// Token: 0x04001AD6 RID: 6870
			WithStaticPhysics = 16U,
			// Token: 0x04001AD7 RID: 6871
			UseAnimationSpeed = 32U,
			// Token: 0x04001AD8 RID: 6872
			CannotBePickedUp = 64U
		}

		// Token: 0x020004C3 RID: 1219
		[EngineStruct("Mission_combat_type", false)]
		public enum MissionCombatType
		{
			// Token: 0x04001ADA RID: 6874
			Combat,
			// Token: 0x04001ADB RID: 6875
			ArenaCombat,
			// Token: 0x04001ADC RID: 6876
			NoCombat
		}

		// Token: 0x020004C4 RID: 1220
		public enum BattleSizeType
		{
			// Token: 0x04001ADE RID: 6878
			Battle,
			// Token: 0x04001ADF RID: 6879
			Siege,
			// Token: 0x04001AE0 RID: 6880
			SallyOut
		}

		// Token: 0x020004C5 RID: 1221
		[EngineStruct("Agent_creation_result", false)]
		internal struct AgentCreationResult
		{
			// Token: 0x04001AE1 RID: 6881
			internal int Index;

			// Token: 0x04001AE2 RID: 6882
			internal UIntPtr AgentPtr;

			// Token: 0x04001AE3 RID: 6883
			internal UIntPtr PositionPtr;

			// Token: 0x04001AE4 RID: 6884
			internal UIntPtr IndexPtr;

			// Token: 0x04001AE5 RID: 6885
			internal UIntPtr FlagsPtr;

			// Token: 0x04001AE6 RID: 6886
			internal UIntPtr StatePtr;
		}

		// Token: 0x020004C6 RID: 1222
		public struct TimeSpeedRequest
		{
			// Token: 0x17000959 RID: 2393
			// (get) Token: 0x06003732 RID: 14130 RVA: 0x000DEB27 File Offset: 0x000DCD27
			// (set) Token: 0x06003733 RID: 14131 RVA: 0x000DEB2F File Offset: 0x000DCD2F
			public float RequestedTimeSpeed { get; private set; }

			// Token: 0x1700095A RID: 2394
			// (get) Token: 0x06003734 RID: 14132 RVA: 0x000DEB38 File Offset: 0x000DCD38
			// (set) Token: 0x06003735 RID: 14133 RVA: 0x000DEB40 File Offset: 0x000DCD40
			public int RequestID { get; private set; }

			// Token: 0x06003736 RID: 14134 RVA: 0x000DEB49 File Offset: 0x000DCD49
			public TimeSpeedRequest(float requestedTime, int requestID)
			{
				this.RequestedTimeSpeed = requestedTime;
				this.RequestID = requestID;
			}
		}

		// Token: 0x020004C7 RID: 1223
		private enum GetNearbyAgentsAuxType
		{
			// Token: 0x04001AEA RID: 6890
			Friend = 1,
			// Token: 0x04001AEB RID: 6891
			Enemy,
			// Token: 0x04001AEC RID: 6892
			All
		}

		// Token: 0x020004C8 RID: 1224
		public static class MissionNetworkHelper
		{
			// Token: 0x06003737 RID: 14135 RVA: 0x000DEB5C File Offset: 0x000DCD5C
			public static Agent GetAgentFromIndex(int agentIndex, bool canBeNull = false)
			{
				Agent agent = Mission.Current.FindAgentWithIndex(agentIndex);
				if (!canBeNull && agent == null && agentIndex >= 0)
				{
					Debug.Print("Agent with index: " + agentIndex + " could not be found while reading reference from packet.", 0, Debug.DebugColor.White, 17592186044416UL);
					throw new MBNotFoundException("Agent with index: " + agentIndex + " could not be found while reading reference from packet.");
				}
				return agent;
			}

			// Token: 0x06003738 RID: 14136 RVA: 0x000DEBC1 File Offset: 0x000DCDC1
			public static MBTeam GetMBTeamFromTeamIndex(int teamIndex)
			{
				if (Mission.Current == null)
				{
					throw new Exception("Mission.Current is null!");
				}
				if (teamIndex < 0)
				{
					return MBTeam.InvalidTeam;
				}
				return new MBTeam(Mission.Current, teamIndex);
			}

			// Token: 0x06003739 RID: 14137 RVA: 0x000DEBEC File Offset: 0x000DCDEC
			public static Team GetTeamFromTeamIndex(int teamIndex)
			{
				if (Mission.Current == null)
				{
					throw new Exception("Mission.Current is null!");
				}
				if (teamIndex < 0)
				{
					return Team.Invalid;
				}
				MBTeam mbteamFromTeamIndex = Mission.MissionNetworkHelper.GetMBTeamFromTeamIndex(teamIndex);
				return Mission.Current.Teams.Find(mbteamFromTeamIndex);
			}

			// Token: 0x0600373A RID: 14138 RVA: 0x000DEC2C File Offset: 0x000DCE2C
			public static MissionObject GetMissionObjectFromMissionObjectId(MissionObjectId missionObjectId)
			{
				if (Mission.Current == null)
				{
					throw new Exception("Mission.Current is null!");
				}
				if (missionObjectId.Id < 0)
				{
					return null;
				}
				MissionObject missionObject = Mission.Current.MissionObjects.FirstOrDefault((MissionObject mo) => mo.Id == missionObjectId);
				if (missionObject == null)
				{
					MBDebug.Print(string.Concat(new object[]
					{
						"MissionObject with ID: ",
						missionObjectId.Id,
						" runtime: ",
						missionObjectId.CreatedAtRuntime.ToString(),
						" could not be found."
					}), 0, Debug.DebugColor.White, 17592186044416UL);
				}
				return missionObject;
			}

			// Token: 0x0600373B RID: 14139 RVA: 0x000DECE8 File Offset: 0x000DCEE8
			public static CombatLogData GetCombatLogDataForCombatLogNetworkMessage(CombatLogNetworkMessage message)
			{
				if (Mission.Current == null)
				{
					throw new Exception("Mission.Current is null!");
				}
				Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(message.AttackerAgentIndex, false);
				Agent agentFromIndex2 = Mission.MissionNetworkHelper.GetAgentFromIndex(message.VictimAgentIndex, true);
				bool flag = agentFromIndex != null;
				bool flag2 = flag && agentFromIndex.IsHuman;
				bool flag3 = flag && agentFromIndex.IsMine;
				bool flag4 = flag && agentFromIndex.RiderAgent != null;
				bool flag5 = flag4 && agentFromIndex.RiderAgent.IsMine;
				bool flag6 = flag && agentFromIndex.IsMount;
				bool flag7 = agentFromIndex2 != null && agentFromIndex2.Health <= 0f;
				bool isVictimRiderAgentSameAsAttackerAgent = agentFromIndex != null && ((agentFromIndex2 != null) ? agentFromIndex2.RiderAgent : null) == agentFromIndex;
				bool isVictimAgentSameAsAttackerAgent = agentFromIndex == agentFromIndex2;
				bool isAttackerAgentHuman = flag2;
				bool isAttackerAgentMine = flag3;
				bool doesAttackerAgentHaveRiderAgent = flag4;
				bool isAttackerAgentRiderAgentMine = flag5;
				bool isAttackerAgentMount = flag6;
				bool isVictimAgentHuman = agentFromIndex2 != null && agentFromIndex2.IsHuman;
				bool isVictimAgentMine = agentFromIndex2 != null && agentFromIndex2.IsMine;
				bool isVictimAgentDead = flag7;
				bool doesVictimAgentHaveRiderAgent = ((agentFromIndex2 != null) ? agentFromIndex2.RiderAgent : null) != null;
				bool? flag8;
				if (agentFromIndex2 == null)
				{
					flag8 = null;
				}
				else
				{
					Agent riderAgent = agentFromIndex2.RiderAgent;
					flag8 = ((riderAgent != null) ? new bool?(riderAgent.IsMine) : null);
				}
				CombatLogData result = new CombatLogData(isVictimAgentSameAsAttackerAgent, isAttackerAgentHuman, isAttackerAgentMine, doesAttackerAgentHaveRiderAgent, isAttackerAgentRiderAgentMine, isAttackerAgentMount, isVictimAgentHuman, isVictimAgentMine, isVictimAgentDead, doesVictimAgentHaveRiderAgent, flag8 ?? false, agentFromIndex2 != null && agentFromIndex2.IsMount, message.IsVictimEntity, isVictimRiderAgentSameAsAttackerAgent, message.CrushedThrough, message.Chamber, message.Distance);
				result.DamageType = message.DamageType;
				result.IsRangedAttack = message.IsRangedAttack;
				result.IsFriendlyFire = message.IsFriendlyFire;
				result.IsFatalDamage = message.IsFatalDamage;
				result.BodyPartHit = message.BodyPartHit;
				result.HitSpeed = message.HitSpeed;
				result.InflictedDamage = message.InflictedDamage;
				result.AbsorbedDamage = message.AbsorbedDamage;
				result.ModifiedDamage = message.ModifiedDamage;
				string text;
				if (agentFromIndex2 == null)
				{
					text = null;
				}
				else
				{
					MissionPeer missionPeer = agentFromIndex2.MissionPeer;
					text = ((missionPeer != null) ? missionPeer.DisplayedName : null);
				}
				string victimAgentName;
				if ((victimAgentName = text) == null)
				{
					victimAgentName = (((agentFromIndex2 != null) ? agentFromIndex2.Name : null) ?? "");
				}
				result.VictimAgentName = victimAgentName;
				return result;
			}
		}

		// Token: 0x020004C9 RID: 1225
		public class Missile : MBMissile
		{
			// Token: 0x0600373C RID: 14140 RVA: 0x000DEEF5 File Offset: 0x000DD0F5
			public Missile(Mission mission, GameEntity entity) : base(mission)
			{
				this.Entity = entity;
			}

			// Token: 0x1700095B RID: 2395
			// (get) Token: 0x0600373D RID: 14141 RVA: 0x000DEF05 File Offset: 0x000DD105
			// (set) Token: 0x0600373E RID: 14142 RVA: 0x000DEF0D File Offset: 0x000DD10D
			public GameEntity Entity { get; private set; }

			// Token: 0x1700095C RID: 2396
			// (get) Token: 0x0600373F RID: 14143 RVA: 0x000DEF16 File Offset: 0x000DD116
			// (set) Token: 0x06003740 RID: 14144 RVA: 0x000DEF1E File Offset: 0x000DD11E
			public MissionWeapon Weapon { get; set; }

			// Token: 0x1700095D RID: 2397
			// (get) Token: 0x06003741 RID: 14145 RVA: 0x000DEF27 File Offset: 0x000DD127
			// (set) Token: 0x06003742 RID: 14146 RVA: 0x000DEF2F File Offset: 0x000DD12F
			public Agent ShooterAgent { get; set; }

			// Token: 0x1700095E RID: 2398
			// (get) Token: 0x06003743 RID: 14147 RVA: 0x000DEF38 File Offset: 0x000DD138
			// (set) Token: 0x06003744 RID: 14148 RVA: 0x000DEF40 File Offset: 0x000DD140
			public MissionObject MissionObjectToIgnore { get; set; }

			// Token: 0x06003745 RID: 14149 RVA: 0x000DEF4C File Offset: 0x000DD14C
			public void CalculatePassbySoundParametersMT(ref SoundEventParameter soundEventParameter)
			{
				if (this.Weapon.CurrentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.CanPenetrateShield))
				{
					soundEventParameter.Update("impactModifier", 0.3f);
				}
			}

			// Token: 0x06003746 RID: 14150 RVA: 0x000DEF8C File Offset: 0x000DD18C
			public void CalculateBounceBackVelocity(Vec3 rotationSpeed, AttackCollisionData collisionData, out Vec3 velocity, out Vec3 angularVelocity)
			{
				Vec3 missileVelocity = collisionData.MissileVelocity;
				float num = (float)this.Weapon.CurrentUsageItem.WeaponLength * 0.01f * this.Weapon.Item.ScaleFactor;
				PhysicsMaterial fromIndex = PhysicsMaterial.GetFromIndex(collisionData.PhysicsMaterialIndex);
				float num2;
				float num3;
				if (fromIndex.IsValid)
				{
					num2 = fromIndex.GetDynamicFriction();
					num3 = fromIndex.GetRestitution();
				}
				else
				{
					num2 = 0.3f;
					num3 = 0.4f;
				}
				PhysicsMaterial fromName = PhysicsMaterial.GetFromName(this.Weapon.Item.PrimaryWeapon.PhysicsMaterial);
				float num4;
				float num5;
				if (fromName.IsValid)
				{
					num4 = fromName.GetDynamicFriction();
					num5 = fromName.GetRestitution();
				}
				else
				{
					num4 = 0.3f;
					num5 = 0.4f;
				}
				float num6 = (num2 + num4) * 0.5f;
				float num7 = (num3 + num5) * 0.5f;
				Vec3 vec = missileVelocity.Reflect(collisionData.CollisionGlobalNormal);
				float num8 = Vec3.DotProduct(vec, collisionData.CollisionGlobalNormal);
				Vec3 vec2 = collisionData.CollisionGlobalNormal.RotateAboutAnArbitraryVector(Vec3.CrossProduct(vec, collisionData.CollisionGlobalNormal).NormalizedCopy(), 1.5707964f);
				float num9 = Vec3.DotProduct(vec, vec2);
				velocity = collisionData.CollisionGlobalNormal * (num7 * num8) + vec2 * (num9 * num6);
				velocity += collisionData.CollisionGlobalNormal;
				angularVelocity = -Vec3.CrossProduct(collisionData.CollisionGlobalNormal, velocity);
				float lengthSquared = angularVelocity.LengthSquared;
				float weight = this.Weapon.GetWeight();
				WeaponClass weaponClass = this.Weapon.CurrentUsageItem.WeaponClass;
				float num10;
				if (weaponClass == WeaponClass.Arrow || weaponClass == WeaponClass.Bolt)
				{
					num10 = 0.25f * weight * 0.055f * 0.055f + 0.08333333f * weight * num * num;
				}
				else if (weaponClass == WeaponClass.ThrowingKnife)
				{
					num10 = 0.25f * weight * 0.2f * 0.2f + 0.08333333f * weight * num * num;
					num10 += 0.5f * weight * 0.2f * 0.2f;
					rotationSpeed * num3;
					MatrixFrame globalFrame = this.Entity.GetGlobalFrame();
					angularVelocity = globalFrame.rotation.TransformToParent(rotationSpeed * num3);
				}
				else if (weaponClass == WeaponClass.ThrowingAxe)
				{
					num10 = 0.25f * weight * 0.2f * 0.2f + 0.08333333f * weight * num * num;
					num10 += 0.5f * weight * 0.2f * 0.2f;
					rotationSpeed * num3;
					MatrixFrame globalFrame = this.Entity.GetGlobalFrame();
					angularVelocity = globalFrame.rotation.TransformToParent(rotationSpeed * num3);
				}
				else if (weaponClass == WeaponClass.Javelin)
				{
					num10 = 0.25f * weight * 0.155f * 0.155f + 0.08333333f * weight * num * num;
				}
				else if (weaponClass == WeaponClass.Stone)
				{
					num10 = 0.4f * weight * 0.1f * 0.1f;
				}
				else if (weaponClass == WeaponClass.Boulder)
				{
					num10 = 0.4f * weight * 0.4f * 0.4f;
				}
				else
				{
					Debug.FailedAssert("Unknown missile type!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Mission.cs", "CalculateBounceBackVelocity", 266);
					num10 = 0f;
				}
				float num11 = 0.5f * num10 * lengthSquared;
				float length = missileVelocity.Length;
				float num12 = MathF.Sqrt((0.5f * weight * length * length - num11) * 2f / weight);
				velocity *= num12 / length;
				float maximumValue = CompressionMission.SpawnedItemVelocityCompressionInfo.GetMaximumValue();
				float maximumValue2 = CompressionMission.SpawnedItemAngularVelocityCompressionInfo.GetMaximumValue();
				if (velocity.LengthSquared > maximumValue * maximumValue)
				{
					velocity = velocity.NormalizedCopy() * maximumValue;
				}
				if (angularVelocity.LengthSquared > maximumValue2 * maximumValue2)
				{
					angularVelocity = angularVelocity.NormalizedCopy() * maximumValue2;
				}
			}
		}

		// Token: 0x020004CA RID: 1226
		public struct SpectatorData
		{
			// Token: 0x1700095F RID: 2399
			// (get) Token: 0x06003747 RID: 14151 RVA: 0x000DF3A2 File Offset: 0x000DD5A2
			// (set) Token: 0x06003748 RID: 14152 RVA: 0x000DF3AA File Offset: 0x000DD5AA
			public Agent AgentToFollow { get; private set; }

			// Token: 0x17000960 RID: 2400
			// (get) Token: 0x06003749 RID: 14153 RVA: 0x000DF3B3 File Offset: 0x000DD5B3
			// (set) Token: 0x0600374A RID: 14154 RVA: 0x000DF3BB File Offset: 0x000DD5BB
			public IAgentVisual AgentVisualToFollow { get; private set; }

			// Token: 0x17000961 RID: 2401
			// (get) Token: 0x0600374B RID: 14155 RVA: 0x000DF3C4 File Offset: 0x000DD5C4
			// (set) Token: 0x0600374C RID: 14156 RVA: 0x000DF3CC File Offset: 0x000DD5CC
			public SpectatorCameraTypes CameraType { get; private set; }

			// Token: 0x0600374D RID: 14157 RVA: 0x000DF3D5 File Offset: 0x000DD5D5
			public SpectatorData(Agent agentToFollow, IAgentVisual agentVisualToFollow, SpectatorCameraTypes cameraType)
			{
				this.AgentToFollow = agentToFollow;
				this.CameraType = cameraType;
				this.AgentVisualToFollow = agentVisualToFollow;
			}
		}

		// Token: 0x020004CB RID: 1227
		public enum State
		{
			// Token: 0x04001AF5 RID: 6901
			NewlyCreated,
			// Token: 0x04001AF6 RID: 6902
			Initializing,
			// Token: 0x04001AF7 RID: 6903
			Continuing,
			// Token: 0x04001AF8 RID: 6904
			EndingNextFrame,
			// Token: 0x04001AF9 RID: 6905
			Over
		}

		// Token: 0x020004CC RID: 1228
		private class DynamicEntityInfo
		{
			// Token: 0x04001AFA RID: 6906
			public GameEntity Entity;

			// Token: 0x04001AFB RID: 6907
			public Timer TimerToDisable;
		}

		// Token: 0x020004CD RID: 1229
		public enum BattleSizeQualifier
		{
			// Token: 0x04001AFD RID: 6909
			Small,
			// Token: 0x04001AFE RID: 6910
			Medium
		}

		// Token: 0x020004CE RID: 1230
		public enum MissionTeamAITypeEnum
		{
			// Token: 0x04001B00 RID: 6912
			NoTeamAI,
			// Token: 0x04001B01 RID: 6913
			FieldBattle,
			// Token: 0x04001B02 RID: 6914
			Siege,
			// Token: 0x04001B03 RID: 6915
			SallyOut
		}

		// Token: 0x020004CF RID: 1231
		public enum MissileCollisionReaction
		{
			// Token: 0x04001B05 RID: 6917
			Invalid = -1,
			// Token: 0x04001B06 RID: 6918
			Stick,
			// Token: 0x04001B07 RID: 6919
			PassThrough,
			// Token: 0x04001B08 RID: 6920
			BounceBack,
			// Token: 0x04001B09 RID: 6921
			BecomeInvisible,
			// Token: 0x04001B0A RID: 6922
			Count
		}

		// Token: 0x020004D0 RID: 1232
		// (Invoke) Token: 0x06003750 RID: 14160
		public delegate void OnBeforeAgentRemovedDelegate(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow);

		// Token: 0x020004D1 RID: 1233
		public sealed class TeamCollection : List<Team>
		{
			// Token: 0x140000A2 RID: 162
			// (add) Token: 0x06003753 RID: 14163 RVA: 0x000DF3F4 File Offset: 0x000DD5F4
			// (remove) Token: 0x06003754 RID: 14164 RVA: 0x000DF42C File Offset: 0x000DD62C
			public event Action<Team, Team> OnPlayerTeamChanged;

			// Token: 0x17000962 RID: 2402
			// (get) Token: 0x06003755 RID: 14165 RVA: 0x000DF461 File Offset: 0x000DD661
			// (set) Token: 0x06003756 RID: 14166 RVA: 0x000DF469 File Offset: 0x000DD669
			public Team Attacker { get; private set; }

			// Token: 0x17000963 RID: 2403
			// (get) Token: 0x06003757 RID: 14167 RVA: 0x000DF472 File Offset: 0x000DD672
			// (set) Token: 0x06003758 RID: 14168 RVA: 0x000DF47A File Offset: 0x000DD67A
			public Team Defender { get; private set; }

			// Token: 0x17000964 RID: 2404
			// (get) Token: 0x06003759 RID: 14169 RVA: 0x000DF483 File Offset: 0x000DD683
			// (set) Token: 0x0600375A RID: 14170 RVA: 0x000DF48B File Offset: 0x000DD68B
			public Team AttackerAlly { get; private set; }

			// Token: 0x17000965 RID: 2405
			// (get) Token: 0x0600375B RID: 14171 RVA: 0x000DF494 File Offset: 0x000DD694
			// (set) Token: 0x0600375C RID: 14172 RVA: 0x000DF49C File Offset: 0x000DD69C
			public Team DefenderAlly { get; private set; }

			// Token: 0x17000966 RID: 2406
			// (get) Token: 0x0600375D RID: 14173 RVA: 0x000DF4A5 File Offset: 0x000DD6A5
			// (set) Token: 0x0600375E RID: 14174 RVA: 0x000DF4AD File Offset: 0x000DD6AD
			public Team Player
			{
				get
				{
					return this._playerTeam;
				}
				set
				{
					if (this._playerTeam != value)
					{
						this.SetPlayerTeamAux((value == null) ? -1 : base.IndexOf(value));
					}
				}
			}

			// Token: 0x17000967 RID: 2407
			// (get) Token: 0x0600375F RID: 14175 RVA: 0x000DF4CB File Offset: 0x000DD6CB
			// (set) Token: 0x06003760 RID: 14176 RVA: 0x000DF4D3 File Offset: 0x000DD6D3
			public Team PlayerEnemy { get; private set; }

			// Token: 0x17000968 RID: 2408
			// (get) Token: 0x06003761 RID: 14177 RVA: 0x000DF4DC File Offset: 0x000DD6DC
			// (set) Token: 0x06003762 RID: 14178 RVA: 0x000DF4E4 File Offset: 0x000DD6E4
			public Team PlayerAlly { get; private set; }

			// Token: 0x06003763 RID: 14179 RVA: 0x000DF4ED File Offset: 0x000DD6ED
			public TeamCollection(Mission mission) : base(new List<Team>())
			{
				this._mission = mission;
			}

			// Token: 0x06003764 RID: 14180 RVA: 0x000DF501 File Offset: 0x000DD701
			private MBTeam AddNative()
			{
				return new MBTeam(this._mission, MBAPI.IMBMission.AddTeam(this._mission.Pointer));
			}

			// Token: 0x06003765 RID: 14181 RVA: 0x000DF523 File Offset: 0x000DD723
			public new void Add(Team t)
			{
				MBDebug.ShowWarning("Pre-created Team can not be added to TeamCollection!");
			}

			// Token: 0x06003766 RID: 14182 RVA: 0x000DF530 File Offset: 0x000DD730
			public Team Add(BattleSideEnum side, uint color = 4294967295U, uint color2 = 4294967295U, Banner banner = null, bool isPlayerGeneral = true, bool isPlayerSergeant = false, bool isSettingRelations = true)
			{
				MBDebug.Print("----------Mission-AddTeam-" + side, 0, Debug.DebugColor.White, 17592186044416UL);
				Team team = new Team(this.AddNative(), side, this._mission, color, color2, banner);
				if (!GameNetwork.IsClientOrReplay)
				{
					team.SetPlayerRole(isPlayerGeneral, isPlayerSergeant);
				}
				base.Add(team);
				foreach (MissionBehavior missionBehavior in this._mission.MissionBehaviors)
				{
					missionBehavior.OnAddTeam(team);
				}
				if (isSettingRelations)
				{
					this.SetRelations(team);
				}
				if (side == BattleSideEnum.Attacker)
				{
					if (this.Attacker == null)
					{
						this.Attacker = team;
					}
					else if (this.AttackerAlly == null)
					{
						this.AttackerAlly = team;
					}
				}
				else if (side == BattleSideEnum.Defender)
				{
					if (this.Defender == null)
					{
						this.Defender = team;
					}
					else if (this.DefenderAlly == null)
					{
						this.DefenderAlly = team;
					}
				}
				this.AdjustPlayerTeams();
				foreach (MissionBehavior missionBehavior2 in this._mission.MissionBehaviors)
				{
					missionBehavior2.AfterAddTeam(team);
				}
				return team;
			}

			// Token: 0x06003767 RID: 14183 RVA: 0x000DF678 File Offset: 0x000DD878
			public Team Find(MBTeam mbTeam)
			{
				if (mbTeam.IsValid)
				{
					for (int i = 0; i < base.Count; i++)
					{
						Team team = base[i];
						if (team.MBTeam == mbTeam)
						{
							return team;
						}
					}
				}
				return Team.Invalid;
			}

			// Token: 0x06003768 RID: 14184 RVA: 0x000DF6BC File Offset: 0x000DD8BC
			public void ClearResources()
			{
				this.Attacker = null;
				this.AttackerAlly = null;
				this.Defender = null;
				this.DefenderAlly = null;
				this._playerTeam = null;
				this.PlayerEnemy = null;
				this.PlayerAlly = null;
				Team.Invalid = null;
			}

			// Token: 0x06003769 RID: 14185 RVA: 0x000DF6F8 File Offset: 0x000DD8F8
			public new void Clear()
			{
				foreach (Team team in this)
				{
					team.Clear();
				}
				base.Clear();
				this.ClearResources();
				MBAPI.IMBMission.ResetTeams(this._mission.Pointer);
			}

			// Token: 0x0600376A RID: 14186 RVA: 0x000DF764 File Offset: 0x000DD964
			private void SetRelations(Team team)
			{
				BattleSideEnum side = team.Side;
				for (int i = 0; i < base.Count; i++)
				{
					Team team2 = base[i];
					if (side.IsOpponentOf(team2.Side))
					{
						team.SetIsEnemyOf(team2, true);
					}
				}
			}

			// Token: 0x0600376B RID: 14187 RVA: 0x000DF7A8 File Offset: 0x000DD9A8
			private void SetPlayerTeamAux(int index)
			{
				Team playerTeam = this._playerTeam;
				this._playerTeam = ((index == -1) ? null : base[index]);
				this.AdjustPlayerTeams();
				Action<Team, Team> onPlayerTeamChanged = this.OnPlayerTeamChanged;
				if (onPlayerTeamChanged == null)
				{
					return;
				}
				onPlayerTeamChanged(playerTeam, this._playerTeam);
			}

			// Token: 0x0600376C RID: 14188 RVA: 0x000DF7F0 File Offset: 0x000DD9F0
			private void AdjustPlayerTeams()
			{
				if (this.Player == null)
				{
					this.PlayerEnemy = null;
					this.PlayerAlly = null;
					return;
				}
				if (this.Player != this.Attacker)
				{
					if (this.Player == this.Defender)
					{
						if (this.Attacker != null && this.Player.IsEnemyOf(this.Attacker))
						{
							this.PlayerEnemy = this.Attacker;
						}
						else
						{
							this.PlayerEnemy = null;
						}
						if (this.DefenderAlly != null && this.Player.IsFriendOf(this.DefenderAlly))
						{
							this.PlayerAlly = this.DefenderAlly;
							return;
						}
						this.PlayerAlly = null;
					}
					return;
				}
				if (this.Defender != null && this.Player.IsEnemyOf(this.Defender))
				{
					this.PlayerEnemy = this.Defender;
				}
				else
				{
					this.PlayerEnemy = null;
				}
				if (this.AttackerAlly != null && this.Player.IsFriendOf(this.AttackerAlly))
				{
					this.PlayerAlly = this.AttackerAlly;
					return;
				}
				this.PlayerAlly = null;
			}

			// Token: 0x17000969 RID: 2409
			// (get) Token: 0x0600376D RID: 14189 RVA: 0x000DF8EF File Offset: 0x000DDAEF
			private int TeamCountNative
			{
				get
				{
					return MBAPI.IMBMission.GetNumberOfTeams(this._mission.Pointer);
				}
			}

			// Token: 0x04001B0C RID: 6924
			private Mission _mission;

			// Token: 0x04001B11 RID: 6929
			private Team _playerTeam;
		}
	}
}
