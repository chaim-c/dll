using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Conversation;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000056 RID: 86
	public class MissionAgentLookHandler : MissionLogic
	{
		// Token: 0x06000389 RID: 905 RVA: 0x00018B1C File Offset: 0x00016D1C
		public MissionAgentLookHandler()
		{
			this._staticPointList = new List<MissionAgentLookHandler.PointOfInterest>();
			this._checklist = new List<MissionAgentLookHandler.LookInfo>();
			this._selectionDelegate = new MissionAgentLookHandler.SelectionDelegate(this.SelectRandomAccordingToScore);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00018B4C File Offset: 0x00016D4C
		public override void AfterStart()
		{
			this.AddStablePointsOfInterest();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00018B54 File Offset: 0x00016D54
		private void AddStablePointsOfInterest()
		{
			foreach (GameEntity gameEntity in base.Mission.Scene.FindEntitiesWithTag("point_of_interest"))
			{
				this._staticPointList.Add(new MissionAgentLookHandler.PointOfInterest(gameEntity.GetGlobalFrame()));
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00018BC0 File Offset: 0x00016DC0
		private void DebugTick()
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00018BC4 File Offset: 0x00016DC4
		public override void OnMissionTick(float dt)
		{
			if (Game.Current.IsDevelopmentMode)
			{
				this.DebugTick();
			}
			float currentTime = base.Mission.CurrentTime;
			foreach (MissionAgentLookHandler.LookInfo lookInfo in this._checklist)
			{
				if (lookInfo.Agent.IsActive() && !ConversationMission.ConversationAgents.Contains(lookInfo.Agent) && (!ConversationMission.ConversationAgents.Any<Agent>() || !lookInfo.Agent.IsPlayerControlled))
				{
					if (lookInfo.CheckTimer.Check(currentTime))
					{
						MissionAgentLookHandler.PointOfInterest pointOfInterest = this._selectionDelegate(lookInfo.Agent);
						if (pointOfInterest != null)
						{
							lookInfo.Reset(pointOfInterest, 5f);
						}
						else
						{
							lookInfo.Reset(null, 1f + MBRandom.RandomFloat);
						}
					}
					else if (lookInfo.PointOfInterest != null && (!lookInfo.PointOfInterest.IsActive || !lookInfo.PointOfInterest.IsVisibleFor(lookInfo.Agent)))
					{
						MissionAgentLookHandler.PointOfInterest pointOfInterest2 = this._selectionDelegate(lookInfo.Agent);
						if (pointOfInterest2 != null)
						{
							lookInfo.Reset(pointOfInterest2, 5f + MBRandom.RandomFloat);
						}
						else
						{
							lookInfo.Reset(null, MBRandom.RandomFloat * 5f + 5f);
						}
					}
					else if (lookInfo.PointOfInterest != null)
					{
						Vec3 targetPosition = lookInfo.PointOfInterest.GetTargetPosition();
						lookInfo.Agent.SetLookToPointOfInterest(targetPosition);
					}
				}
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00018D5C File Offset: 0x00016F5C
		private MissionAgentLookHandler.PointOfInterest SelectFirstNonAgent(Agent agent)
		{
			if (agent.IsAIControlled)
			{
				int num = MBRandom.RandomInt(this._staticPointList.Count);
				int num2 = num;
				MissionAgentLookHandler.PointOfInterest pointOfInterest;
				for (;;)
				{
					pointOfInterest = this._staticPointList[num2];
					if (pointOfInterest.GetScore(agent) > 0f)
					{
						break;
					}
					num2 = ((num2 + 1 == this._staticPointList.Count) ? 0 : (num2 + 1));
					if (num2 == num)
					{
						goto IL_53;
					}
				}
				return pointOfInterest;
			}
			IL_53:
			return null;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00018DC0 File Offset: 0x00016FC0
		private MissionAgentLookHandler.PointOfInterest SelectBestOfLimitedNonAgent(Agent agent)
		{
			int num = 3;
			MissionAgentLookHandler.PointOfInterest result = null;
			float num2 = -1f;
			if (agent.IsAIControlled)
			{
				int num3 = MBRandom.RandomInt(this._staticPointList.Count);
				int num4 = num3;
				do
				{
					MissionAgentLookHandler.PointOfInterest pointOfInterest = this._staticPointList[num4];
					float score = pointOfInterest.GetScore(agent);
					if (score > 0f)
					{
						if (score > num2)
						{
							num2 = score;
							result = pointOfInterest;
						}
						num--;
					}
					num4 = ((num4 + 1 == this._staticPointList.Count) ? 0 : (num4 + 1));
				}
				while (num4 != num3 && num > 0);
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00018E48 File Offset: 0x00017048
		private MissionAgentLookHandler.PointOfInterest SelectBest(Agent agent)
		{
			MissionAgentLookHandler.PointOfInterest result = null;
			float num = -1f;
			if (agent.IsAIControlled)
			{
				foreach (MissionAgentLookHandler.PointOfInterest pointOfInterest in this._staticPointList)
				{
					float score = pointOfInterest.GetScore(agent);
					if (score > 0f && score > num)
					{
						num = score;
						result = pointOfInterest;
					}
				}
				AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(base.Mission, agent.Position.AsVec2, 5f, false);
				while (proximityMapSearchStruct.LastFoundAgent != null)
				{
					MissionAgentLookHandler.PointOfInterest pointOfInterest2 = new MissionAgentLookHandler.PointOfInterest(proximityMapSearchStruct.LastFoundAgent);
					float score2 = pointOfInterest2.GetScore(agent);
					if (score2 > 0f && score2 > num)
					{
						num = score2;
						result = pointOfInterest2;
					}
					AgentProximityMap.FindNext(base.Mission, ref proximityMapSearchStruct);
				}
			}
			return result;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00018F2C File Offset: 0x0001712C
		private MissionAgentLookHandler.PointOfInterest SelectRandomAccordingToScore(Agent agent)
		{
			float num = 0f;
			List<KeyValuePair<float, MissionAgentLookHandler.PointOfInterest>> list = new List<KeyValuePair<float, MissionAgentLookHandler.PointOfInterest>>();
			if (agent.IsAIControlled)
			{
				foreach (MissionAgentLookHandler.PointOfInterest pointOfInterest in this._staticPointList)
				{
					float score = pointOfInterest.GetScore(agent);
					if (score > 0f)
					{
						list.Add(new KeyValuePair<float, MissionAgentLookHandler.PointOfInterest>(score, pointOfInterest));
						num += score;
					}
				}
				AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(Mission.Current, agent.Position.AsVec2, 5f, false);
				while (proximityMapSearchStruct.LastFoundAgent != null)
				{
					MissionAgentLookHandler.PointOfInterest pointOfInterest2 = new MissionAgentLookHandler.PointOfInterest(proximityMapSearchStruct.LastFoundAgent);
					float score2 = pointOfInterest2.GetScore(agent);
					if (score2 > 0f)
					{
						list.Add(new KeyValuePair<float, MissionAgentLookHandler.PointOfInterest>(score2, pointOfInterest2));
						num += score2;
					}
					AgentProximityMap.FindNext(Mission.Current, ref proximityMapSearchStruct);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			float num2 = MBRandom.RandomFloat * num;
			MissionAgentLookHandler.PointOfInterest value = list[list.Count - 1].Value;
			foreach (KeyValuePair<float, MissionAgentLookHandler.PointOfInterest> keyValuePair in list)
			{
				num2 -= keyValuePair.Key;
				if (num2 <= 0f)
				{
					value = keyValuePair.Value;
					break;
				}
			}
			return value;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000190A4 File Offset: 0x000172A4
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (agent.IsHuman)
			{
				this._checklist.Add(new MissionAgentLookHandler.LookInfo(agent, MBRandom.RandomFloat));
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000190C4 File Offset: 0x000172C4
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			for (int i = 0; i < this._checklist.Count; i++)
			{
				MissionAgentLookHandler.LookInfo lookInfo = this._checklist[i];
				if (lookInfo.Agent == affectedAgent)
				{
					this._checklist.RemoveAt(i);
					i--;
				}
				else if (lookInfo.PointOfInterest != null && lookInfo.PointOfInterest.IsRelevant(affectedAgent))
				{
					lookInfo.Reset(null, MBRandom.RandomFloat * 2f + 2f);
				}
			}
		}

		// Token: 0x040001A9 RID: 425
		private readonly List<MissionAgentLookHandler.PointOfInterest> _staticPointList;

		// Token: 0x040001AA RID: 426
		private readonly List<MissionAgentLookHandler.LookInfo> _checklist;

		// Token: 0x040001AB RID: 427
		private MissionAgentLookHandler.SelectionDelegate _selectionDelegate;

		// Token: 0x02000138 RID: 312
		private class PointOfInterest
		{
			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00052E3B File Offset: 0x0005103B
			public bool IsActive
			{
				get
				{
					return this._agent == null || this._agent.IsActive();
				}
			}

			// Token: 0x06000BED RID: 3053 RVA: 0x00052E54 File Offset: 0x00051054
			public PointOfInterest(Agent agent)
			{
				this._agent = agent;
				this._selectDistance = 5;
				this._releaseDistanceSquare = 36;
				this._ignoreDirection = false;
				CharacterObject characterObject = (CharacterObject)agent.Character;
				if (!agent.IsHuman)
				{
					this._priority = 1;
					return;
				}
				if (characterObject.IsHero)
				{
					this._priority = 5;
					return;
				}
				if (characterObject.Occupation == Occupation.HorseTrader || characterObject.Occupation == Occupation.Weaponsmith || characterObject.Occupation == Occupation.GoodsTrader || characterObject.Occupation == Occupation.Armorer || characterObject.Occupation == Occupation.Blacksmith)
				{
					this._priority = 3;
					return;
				}
				this._priority = 1;
			}

			// Token: 0x06000BEE RID: 3054 RVA: 0x00052EF0 File Offset: 0x000510F0
			public PointOfInterest(MatrixFrame frame)
			{
				this._frame = frame;
				this._selectDistance = 4;
				this._releaseDistanceSquare = 25;
				this._ignoreDirection = true;
				this._priority = 2;
			}

			// Token: 0x06000BEF RID: 3055 RVA: 0x00052F1C File Offset: 0x0005111C
			public float GetScore(Agent agent)
			{
				if (agent == this._agent || this.GetBasicPosition().DistanceSquared(agent.Position) > (float)(this._selectDistance * this._selectDistance))
				{
					return -1f;
				}
				Vec3 vec = this.GetTargetPosition() - agent.GetEyeGlobalPosition();
				float num = vec.Normalize();
				if (Vec2.DotProduct(vec.AsVec2, agent.GetMovementDirection()) < 0.7f)
				{
					return -1f;
				}
				float num2 = (float)(this._priority * this._selectDistance) / num;
				if (this.IsMoving())
				{
					num2 *= 5f;
				}
				if (!this._ignoreDirection)
				{
					MatrixFrame matrixFrame = this.GetTargetFrame();
					Vec2 asVec = matrixFrame.rotation.f.AsVec2;
					matrixFrame = agent.Frame;
					float num3 = Vec2.DotProduct(asVec, matrixFrame.rotation.f.AsVec2);
					if (num3 < -0.7f)
					{
						num2 *= 2f;
					}
					else if (MathF.Abs(num3) < 0.1f)
					{
						num2 *= 2f;
					}
				}
				return num2;
			}

			// Token: 0x06000BF0 RID: 3056 RVA: 0x00053021 File Offset: 0x00051221
			public Vec3 GetTargetPosition()
			{
				Agent agent = this._agent;
				if (agent == null)
				{
					return this._frame.origin;
				}
				return agent.GetEyeGlobalPosition();
			}

			// Token: 0x06000BF1 RID: 3057 RVA: 0x0005303E File Offset: 0x0005123E
			public Vec3 GetBasicPosition()
			{
				if (this._agent == null)
				{
					return this._frame.origin;
				}
				return this._agent.Position;
			}

			// Token: 0x06000BF2 RID: 3058 RVA: 0x00053060 File Offset: 0x00051260
			private bool IsMoving()
			{
				return this._agent == null || this._agent.GetCurrentVelocity().LengthSquared > 0.040000003f;
			}

			// Token: 0x06000BF3 RID: 3059 RVA: 0x00053091 File Offset: 0x00051291
			private MatrixFrame GetTargetFrame()
			{
				if (this._agent == null)
				{
					return this._frame;
				}
				return this._agent.Frame;
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x000530B0 File Offset: 0x000512B0
			public bool IsVisibleFor(Agent agent)
			{
				Vec3 basicPosition = this.GetBasicPosition();
				Vec3 position = agent.Position;
				if (agent == this._agent || position.DistanceSquared(basicPosition) > (float)this._releaseDistanceSquare)
				{
					return false;
				}
				Vec3 vec = basicPosition - position;
				vec.Normalize();
				return Vec2.DotProduct(vec.AsVec2, agent.GetMovementDirection()) > 0.4f;
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x00053110 File Offset: 0x00051310
			public bool IsRelevant(Agent agent)
			{
				return agent == this._agent;
			}

			// Token: 0x04000574 RID: 1396
			public const int MaxSelectDistanceForAgent = 5;

			// Token: 0x04000575 RID: 1397
			public const int MaxSelectDistanceForFrame = 4;

			// Token: 0x04000576 RID: 1398
			private readonly int _selectDistance;

			// Token: 0x04000577 RID: 1399
			private readonly int _releaseDistanceSquare;

			// Token: 0x04000578 RID: 1400
			private readonly Agent _agent;

			// Token: 0x04000579 RID: 1401
			private readonly MatrixFrame _frame;

			// Token: 0x0400057A RID: 1402
			private readonly bool _ignoreDirection;

			// Token: 0x0400057B RID: 1403
			private readonly int _priority;
		}

		// Token: 0x02000139 RID: 313
		private class LookInfo
		{
			// Token: 0x06000BF6 RID: 3062 RVA: 0x0005311B File Offset: 0x0005131B
			public LookInfo(Agent agent, float checkTime)
			{
				this.Agent = agent;
				this.CheckTimer = new Timer(Mission.Current.CurrentTime, checkTime, true);
			}

			// Token: 0x06000BF7 RID: 3063 RVA: 0x00053144 File Offset: 0x00051344
			public void Reset(MissionAgentLookHandler.PointOfInterest pointOfInterest, float duration)
			{
				if (this.PointOfInterest != pointOfInterest)
				{
					this.PointOfInterest = pointOfInterest;
					if (this.PointOfInterest != null)
					{
						this.Agent.SetLookToPointOfInterest(this.PointOfInterest.GetTargetPosition());
					}
					else if (this.Agent.IsActive())
					{
						this.Agent.DisableLookToPointOfInterest();
					}
				}
				this.CheckTimer.Reset(Mission.Current.CurrentTime, duration);
			}

			// Token: 0x0400057C RID: 1404
			public readonly Agent Agent;

			// Token: 0x0400057D RID: 1405
			public MissionAgentLookHandler.PointOfInterest PointOfInterest;

			// Token: 0x0400057E RID: 1406
			public readonly Timer CheckTimer;
		}

		// Token: 0x0200013A RID: 314
		// (Invoke) Token: 0x06000BF9 RID: 3065
		private delegate MissionAgentLookHandler.PointOfInterest SelectionDelegate(Agent agent);
	}
}
