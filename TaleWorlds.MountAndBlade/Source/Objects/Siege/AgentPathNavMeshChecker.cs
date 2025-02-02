using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Source.Objects.Siege
{
	// Token: 0x020003AC RID: 940
	public class AgentPathNavMeshChecker
	{
		// Token: 0x0600329B RID: 12955 RVA: 0x000D1CF8 File Offset: 0x000CFEF8
		public AgentPathNavMeshChecker(Mission mission, MatrixFrame pathFrameToCheck, float radiusToCheck, int navMeshId, BattleSideEnum teamToCollect, AgentPathNavMeshChecker.Direction directionToCollect, float maxDistanceCheck, float agentMoveTime)
		{
			this._mission = mission;
			this._pathFrameToCheck = pathFrameToCheck;
			this._radiusToCheck = radiusToCheck;
			this._navMeshId = navMeshId;
			this._teamToCollect = teamToCollect;
			this._directionToCollect = directionToCollect;
			this._maxDistanceCheck = maxDistanceCheck;
			this._agentMoveTime = agentMoveTime;
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000D1D54 File Offset: 0x000CFF54
		public void Tick(float dt)
		{
			float currentTime = this._mission.CurrentTime;
			if (this._tickOccasionallyTimer == null || this._tickOccasionallyTimer.Check(currentTime))
			{
				float dt2 = dt;
				if (this._tickOccasionallyTimer != null)
				{
					dt2 = this._tickOccasionallyTimer.ElapsedTime();
				}
				this._tickOccasionallyTimer = new Timer(currentTime, 0.1f + MBRandom.RandomFloat * 0.1f, true);
				this.TickOccasionally(dt2);
			}
			bool flag = false;
			foreach (Agent agent in this._nearbyAgents)
			{
				Vec3 position = agent.Position;
				if ((this._teamToCollect == BattleSideEnum.None || (agent.Team != null && agent.Team.Side == this._teamToCollect)) && agent.IsAIControlled)
				{
					if (agent.GetCurrentNavigationFaceId() == this._navMeshId)
					{
						flag = true;
						break;
					}
					if (this._isBeingUsed && position.DistanceSquared(this._pathFrameToCheck.origin) < this._radiusToCheck * this._radiusToCheck)
					{
						flag = true;
						break;
					}
					if (agent.MovementVelocity.LengthSquared > 0.01f)
					{
						Vec2 direction;
						if (this._directionToCollect == AgentPathNavMeshChecker.Direction.ForwardOnly)
						{
							direction = this._pathFrameToCheck.rotation.f.AsVec2;
						}
						else if (this._directionToCollect == AgentPathNavMeshChecker.Direction.BackwardOnly)
						{
							direction = -this._pathFrameToCheck.rotation.f.AsVec2;
						}
						else
						{
							direction = Vec2.Zero;
						}
						if (agent.HasPathThroughNavigationFaceIdFromDirection(this._navMeshId, direction))
						{
							float num = agent.GetPathDistanceToPoint(ref this._pathFrameToCheck.origin);
							if (num >= 100000f)
							{
								num = agent.Position.Distance(this._pathFrameToCheck.origin);
							}
							float maximumForwardUnlimitedSpeed = agent.MaximumForwardUnlimitedSpeed;
							if (num < this._radiusToCheck * 2f || num / maximumForwardUnlimitedSpeed < this._agentMoveTime)
							{
								flag = true;
							}
						}
					}
				}
			}
			if (flag)
			{
				this._isBeingUsed = true;
				this._setBeingUsedToFalseTimer = null;
			}
			else if (this._setBeingUsedToFalseTimer == null)
			{
				this._setBeingUsedToFalseTimer = new Timer(currentTime, 1f, true);
			}
			if (this._setBeingUsedToFalseTimer != null && this._setBeingUsedToFalseTimer.Check(currentTime))
			{
				this._setBeingUsedToFalseTimer = null;
				this._isBeingUsed = false;
			}
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000D1FC8 File Offset: 0x000D01C8
		public void TickOccasionally(float dt)
		{
			this._nearbyAgents = this._mission.GetNearbyAgents(this._pathFrameToCheck.origin.AsVec2, this._maxDistanceCheck, this._nearbyAgents);
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000D1FF7 File Offset: 0x000D01F7
		public bool HasAgentsUsingPath()
		{
			return this._isBeingUsed;
		}

		// Token: 0x040015E5 RID: 5605
		private BattleSideEnum _teamToCollect;

		// Token: 0x040015E6 RID: 5606
		private AgentPathNavMeshChecker.Direction _directionToCollect;

		// Token: 0x040015E7 RID: 5607
		private MatrixFrame _pathFrameToCheck;

		// Token: 0x040015E8 RID: 5608
		private float _radiusToCheck;

		// Token: 0x040015E9 RID: 5609
		private Mission _mission;

		// Token: 0x040015EA RID: 5610
		private int _navMeshId;

		// Token: 0x040015EB RID: 5611
		private Timer _tickOccasionallyTimer;

		// Token: 0x040015EC RID: 5612
		private MBList<Agent> _nearbyAgents = new MBList<Agent>();

		// Token: 0x040015ED RID: 5613
		private bool _isBeingUsed;

		// Token: 0x040015EE RID: 5614
		private Timer _setBeingUsedToFalseTimer;

		// Token: 0x040015EF RID: 5615
		private float _maxDistanceCheck;

		// Token: 0x040015F0 RID: 5616
		private float _agentMoveTime;

		// Token: 0x02000657 RID: 1623
		public enum Direction
		{
			// Token: 0x04002108 RID: 8456
			ForwardOnly,
			// Token: 0x04002109 RID: 8457
			BackwardOnly,
			// Token: 0x0400210A RID: 8458
			BothDirections
		}
	}
}
